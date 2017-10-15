using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

		public string MyTitle { get; set; }
		public string VideoAddress { get; set; } = "default.html";

		public Visibility ControlsVisible { get; set; } = Visibility.Collapsed; // FIX

		public ObservableCollection<VideoInfo> PlayList { get; set; } = new ObservableCollection<VideoInfo>();

		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = this;
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

		public void ShowUI()
		{
			ControlsVisible = Visibility.Visible;
			txtVideoUrl.Visibility = Visibility.Visible;
			//grdPlayList.Visibility = Visibility.Visible;
		}

		public void HideUI()
		{
			ControlsVisible = Visibility.Collapsed;
			txtVideoUrl.Visibility = Visibility.Collapsed;
			//grdPlayList.Visibility = Visibility.Collapsed;
		}

		#endregion

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
			ShowUI();
		}


		private void cefBrowser_MouseLeave(object sender, MouseEventArgs e)
		{
			HideUI();
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
			ShowUI();
		}

		private void txtVideoUrl_MouseLeave(object sender, MouseEventArgs e)
		{
			HideUI();
		}

		private void grdPlayList_MouseLeave(object sender, MouseEventArgs e)
		{
			HideUI();
		}

		private void grdPlayList_MouseEnter(object sender, MouseEventArgs e)
		{
			ShowUI();
		}

		#endregion
	}
}
