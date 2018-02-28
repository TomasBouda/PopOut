using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CefSharp.Wpf;
using PopOut.Player.Players;
using PopOut.Player.Players.YouTube;
using PopOut.Player.ViewModels.Base;

namespace PopOut.Player.ViewModels
{
	public class PlayerViewModel : BaseViewModel, INotifyPropertyChanged
	{
		public ICommand ShowControlsCmd { get; set; }
		public ICommand HideControlsCmd { get; set; }
		public ICommand ShowPlaylistCmd { get; set; }
		public ICommand HidePlaylistCmd { get; set; }

		public Visibility ControlsVisible { get; set; } = Visibility.Collapsed;
		public Visibility PlaylistVisible { get; set; } = Visibility.Collapsed;

		public IVideoPlayer Player { get; private set; }

		public string VideoAddress { get; set; } = "default.html";

		public PlayerViewModel(ChromiumWebBrowser browser)
		{
			ShowControlsCmd = new RelayCommand(() => ControlsVisible = Visibility.Visible);
			HideControlsCmd = new RelayCommand(() => ControlsVisible = Visibility.Collapsed);

			ShowPlaylistCmd = new RelayCommand(() => PlaylistVisible = Visibility.Visible);
			HidePlaylistCmd = new RelayCommand(() => PlaylistVisible = Visibility.Collapsed);

			Player = new YouTubePlayer(browser);
		}
	}
}
