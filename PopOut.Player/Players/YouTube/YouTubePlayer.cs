using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace PopOut.Player.Players.YouTube
{
    public class YouTubePlayer : IVideoPlayer
    {
        private ChromiumWebBrowser Browser { get; set; }
        private PlayerBoundObject BoundObject { get; set; }

        private readonly string PLAYER_HTML;
        private bool _playerInitialized = false;

        public string Title { get; set; }
        public ObservableCollection<Video> PlayList { get; set; } = new ObservableCollection<Video>();

        public YouTubePlayer(ChromiumWebBrowser browser)
        {
            PLAYER_HTML = File.ReadAllText("Player.html");

            Browser = browser;
            BoundObject = new PlayerBoundObject();
            browser.RegisterJsObject("bound", BoundObject);
            Browser.FrameLoadEnd += Browser_FrameLoadEnd;
        }

        private void Browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (!_playerInitialized)
            {
                Browser.LoadHtml(PLAYER_HTML, "http://example/");
                _playerInitialized = true;
            }
        }

        public void PlayOrQueue(string youtubeUrl)
        {
            if (PlayList?.Count == 0 && BoundObject.CurrentState == EYouTubePlayerState.UNSTARTED || BoundObject.CurrentState == EYouTubePlayerState.ENDED)
            {
                PlayVideo(youtubeUrl);
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
            Title = video.Title;
            PlayVideo(video.Url);
        }

        private void PlayVideo(string videoUrl)
        {
            if (Browser.CanExecuteJavascriptInMainFrame)
            {
                var videoId = Regex.Match(videoUrl, @"v=(.*)").Groups[1].Value;
                var playerHtml = PLAYER_HTML.Replace("$VIDEO_ID$", videoId);
                Browser.LoadHtml(playerHtml);

                Browser.ExecuteScriptAsync($"player.loadVideoById({videoId});");
            }
        }
    }
}
