using CefSharp;
using CefSharp.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YoutubeExtractor;

namespace PopOut.Player.Players.YouTube
{
	public class YouTubePlayer : IVideoPlayer, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private ChromiumWebBrowser Browser { get; set; }
		private PlayerBoundObject BoundObject { get; set; }

		public EYouTubePlayerState State => BoundObject?.CurrentState ?? EYouTubePlayerState.UNSTARTED;

        public bool IsInitialized => (Browser?.CanExecuteJavascriptInMainFrame ?? false) && _playerInitialized;

        private readonly string PLAYER_HTML;
		private bool _playerInitialized = false;

        public string Title { get; private set; } = "test";
		public ObservableCollection<IVideo> PlayList { get; private set; } = new ObservableCollection<IVideo>();

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
				PlayVideo(new YouTubeVideo(youtubeUrl));
			}
			else
			{
				PlayList.Add(new YouTubeVideo(youtubeUrl));
			}
		}

		public void PlayFromQueue()
		{
			if (PlayList?.Count > 0)
			{
				var video = PlayList.FirstOrDefault() as YouTubeVideo;
				PlayList.Remove(video);
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
            if (Browser.CanExecuteJavascriptInMainFrame)
            {
                Title = video.Title;

                var videoId = Regex.Match(video.Url, @"v=(.*)").Groups[1].Value;
                var playerHtml = PLAYER_HTML.Replace("$VIDEO_ID$", videoId);
                Browser.LoadHtml(playerHtml);

                Browser.ExecuteScriptAsync($"loadById({videoId});");
            }
        }

        public void Pause()
		{
			Browser.ExecuteScriptAsync($"pause();");
		}

		public void Play()
		{
			Browser.ExecuteScriptAsync($"play();");
		}

		public void Toggle()
		{
			if (State == EYouTubePlayerState.PLAYING)
			{
				Pause();
			}
			else
			{
				Play();
			}
		}

		public void Stop()
		{
			Browser.ExecuteScriptAsync($"stop();");
		}
	}
}
