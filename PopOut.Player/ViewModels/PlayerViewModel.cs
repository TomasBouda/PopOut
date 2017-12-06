using CefSharp;
using CefSharp.Wpf;
using PopOut.Player.Players;
using PopOut.Player.Players.YouTube;
using PopOut.Player.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TomLabs.Shadowgem.Extensions.String;
using YoutubeExtractor;

namespace PopOut.Player.ViewModels
{
    public class PlayerViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public ICommand ShowControlsCmd { get; set; }
        public ICommand HideControlsCmd { get; set; }

        public string VideoAddress { get; set; } = "default.html";
        public Visibility ControlsVisible { get; set; } = Visibility.Collapsed;
        
        public IVideoPlayer Player { get; private set; }

        public PlayerViewModel(ChromiumWebBrowser browser)
        {
            ShowControlsCmd = new RelayCommand(ShowControls);
            HideControlsCmd = new RelayCommand(HideControls);

            Player = new YouTubePlayer(browser);
        }

        private void ShowControls()
        {
            ControlsVisible = Visibility.Visible;
        }

        private void HideControls()
        {
            ControlsVisible = Visibility.Collapsed;
        }
    }
}
