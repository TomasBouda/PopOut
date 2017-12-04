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
using System.Windows.Input;
using YoutubeExtractor;

namespace PopOut.Player.ViewModels
{
    public class PlayerViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ChromiumWebBrowser Browser { get; set; }
        public bool IsPlaying { get; set; }

        public string MyTitle { get; set; }
        public string VideoAddress { get; set; } = "default.html";
        public Visibility ControlsVisible { get; set; } = Visibility.Collapsed;
        public ObservableCollection<Video> PlayList { get; set; } = new ObservableCollection<Video>();

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

        public void PlayOrQueue(string youtubeUrl)
        {
            if (PlayList?.Count == 0 && !IsPlaying)
            {
                PlayVideo(youtubeUrl);
                IsPlaying = true;
            }
            else
            {
                IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(youtubeUrl);
                var video = videoInfos.OrderByDescending(v => v.Resolution).FirstOrDefault();
                PlayList.Add(new Video { Title = video.Title, Url = youtubeUrl });
            }
        }

        public void PlayFromQueue()
        {
            if (PlayList?.Count > 0)
            {
                var video = PlayList.FirstOrDefault();

                PlayVideo(video);
            }
        }

        public void PlayFromQueue(Video video)
        {
            if (PlayList?.Count > 0)
            {
                PlayList.Remove(video);

                PlayVideo(video);
            }
        }

        private void PlayVideo(Video video)
        {
            MyTitle = video.Title;
            PlayVideo(video.Url);
        }

        private void PlayVideo(string videoUrl)
        {
            var address = videoUrl.Replace("watch?v=", "embed/") + "?version=3&autoplay=1&enablejsapi=1";
            Browser.Load(address);
        }
    }
}
