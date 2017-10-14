using System;
using System.Collections.Generic;
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

		public string MyTitle { get; set; }
		public ObservableCollection<VideoInfo> PlayList { get; set; } = new ObservableCollection<VideoInfo>();

		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = this;

			vlcPlayer.MediaPlayer.VlcLibDirectory = new DirectoryInfo(@"C:\Program Files (x86)\VideoLAN\VLC");
			vlcPlayer.MediaPlayer.EndInit();
		}

		public void PlayOrQueue(string youtubeUrl)
		{
			IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(youtubeUrl);
			var video = videoInfos.OrderByDescending(v => v.Resolution).FirstOrDefault();

			if (video != null)
			{
				if (vlcPlayer.MediaPlayer.IsPlaying)
				{
					PlayList.Add(video);
				}
				else
				{
					PlayVideo(video);
				}
			}
		}

		private void PlayVideo(VideoInfo video)
		{
			MyTitle = video.Title;
			vlcPlayer.MediaPlayer.Play(new Uri(video.DownloadUrl));
		}

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

		private void txtVideoUrl_MouseEnter(object sender, MouseEventArgs e)
		{
			grdPlayList.Visibility = Visibility.Visible;
		}

		private void txtVideoUrl_MouseLeave(object sender, MouseEventArgs e)
		{
			grdPlayList.Visibility = Visibility.Collapsed;
		}

		private void ListBox_MouseEnter(object sender, MouseEventArgs e)
		{
			grdPlayList.Visibility = Visibility.Visible;
		}
	}
}
