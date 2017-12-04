using CefSharp;
using PopOut.Player;
using PopOut.Player.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YoutubeExtractor;

namespace YouPipe.Player
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private HotKey _hotKey;
        private const string LANDING_PAGE = "default.html";
        private bool _showLP = true;

		public string MyTitle { get; set; }
        public string VideoAddress { get; set; } = "default.html";
		public Visibility ControlsVisible { get; set; } = Visibility.Collapsed;
		public ObservableCollection<VideoInfo> PlayList { get; set; } = new ObservableCollection<VideoInfo>();

        private PlayerViewModel VM { get; set; }

		public MainWindow()
		{
			InitializeComponent();

            VM = new PlayerViewModel(cefBrowser);
            this.DataContext = VM;
		}

		#region Public Methods

		public void PlayOrQueue(string youtubeUrl)
		{
			//IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(youtubeUrl);
			//var video = videoInfos.OrderByDescending(v => v.Resolution).FirstOrDefault();

			PlayVideo(youtubeUrl);
		}

		public void PlayFromQueue()
		{
			if (PlayList?.Count > 0)
			{
				var video = PlayList.FirstOrDefault();

				PlayVideo(video);
			}
		}

		public void PlayFromQueue(VideoInfo video)
		{
			if (PlayList?.Count > 0)
			{
				PlayList.Remove(video);

				PlayVideo(video);
			}
		}

		private void PlayVideo(VideoInfo video)
		{
			MyTitle = video.Title;
		}

		private void PlayVideo(string videoUrl)
		{
            var address = videoUrl.Replace("watch?v=", "embed/") + "?version=3&autoplay=1&enablejsapi=1";
            cefBrowser.Load(address);
        }

		#endregion

        private void Init()
        {
            _hotKey = new HotKey(Key.X, KeyModifier.Shift | KeyModifier.Ctrl, OnHotKeyHandler);
        }

        #region Event Handlers

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
				((TextBox)sender).Focus();
			}
		}

		private void TextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				PlayOrQueue(txtVideoUrl.Text);
			}
		}

		private void Window_Deactivated(object sender, EventArgs e)
		{
			Window window = (Window)sender;
			window.Topmost = true;
		}

		private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listPlayList.SelectedItem != null)
			{
				var video = listPlayList.SelectedItem as VideoInfo;

				PlayFromQueue(video);
			}
		}

		private void cefBrowser_MouseEnter(object sender, MouseEventArgs e)
		{
            VM.ShowControls();
		}


		private void cefBrowser_MouseLeave(object sender, MouseEventArgs e)
		{
            VM.HideControls();
        }

		private void cefBrowser_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}

		private void txtVideoUrl_MouseEnter(object sender, MouseEventArgs e)
		{
            VM.ShowControls();
        }

		private void txtVideoUrl_MouseLeave(object sender, MouseEventArgs e)
		{
            VM.HideControls();
        }

		private void grdPlayList_MouseLeave(object sender, MouseEventArgs e)
		{
            VM.HideControls();
        }

		private void grdPlayList_MouseEnter(object sender, MouseEventArgs e)
		{
            VM.ShowControls();
        }

        private void OnHotKeyHandler(HotKey hotKey)
        {
            if(WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Minimized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        #endregion

        private void cefBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            string htmlFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}{LANDING_PAGE}";
            if (File.Exists(htmlFilePath) && _showLP)
            {
                var html = File.ReadAllText(htmlFilePath);
                cefBrowser.LoadHtml(html, "http://example/");
                _showLP = false;
            }
        }
    }
}
