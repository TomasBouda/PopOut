using CefSharp.Wpf;
using PopOut.Player.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YoutubeExtractor;

namespace PopOut.Player.ViewModels
{
    public class PlayerViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ChromiumWebBrowser Browser { get; set; }

        public string MyTitle { get; set; }
        public string VideoAddress { get; set; } = "default.html";
        public Visibility ControlsVisible { get; set; } = Visibility.Collapsed;
        public ObservableCollection<VideoInfo> PlayList { get; set; } = new ObservableCollection<VideoInfo>();

        public PlayerViewModel(ChromiumWebBrowser browser)
        {
            Browser = browser;
        }

        public void ShowControls()
        {
            ControlsVisible = Visibility.Visible;
        }

        public void HideControls()
        {
            ControlsVisible = Visibility.Collapsed;
        }
    }
}
