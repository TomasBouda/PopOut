using CefSharp;
using CefSharp.Wpf;
using PopOut.Player.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PopOut.Player.Players.YouTube
{
	public class YouTubePlayer : BaseViewModel, IVideoPlayer, INotifyPropertyChanged
	{
		private ChromiumWebBrowser Browser { get; set; }
		public PlayerBoundObject BoundObject { get; private set; }

		public EYouTubePlayerState State => BoundObject?.CurrentState ?? EYouTubePlayerState.UNSTARTED;

		public bool IsInitialized => (Browser?.CanExecuteJavascriptInMainFrame ?? false) && _playerInitialized;
		public bool PreLoaded { get; private set; }

		private readonly string PLAYER_HTML;
		private bool _playerInitialized = false;

		public IVideo CurrentVideo { get; private set; }
		public int PlayAt { get; set; }
		private bool _seek = false;
		public ObservableCollection<IVideo> PlayList { get; private set; } = new ObservableCollection<IVideo>();

		public Queue<Action> ActionsAfterPlay { get; set; } = new Queue<Action>();

		public YouTubePlayer(ChromiumWebBrowser browser)
		{
			PLAYER_HTML = File.ReadAllText("Player.html");

			Browser = browser;
			BoundObject = new PlayerBoundObject();
			browser.RegisterJsObject("bound", BoundObject);
			Browser.FrameLoadEnd += Browser_FrameLoadEnd;

			BoundObject.Playing += BoundObject_Playing;
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
			if (PlayList?.Count == 0 && (BoundObject.CurrentState == EYouTubePlayerState.UNSTARTED || BoundObject.CurrentState == EYouTubePlayerState.ENDED))
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
			if (PlayList.Contains(video))
			{
				PlayVideo(video);
				PlayList.Remove(video);
			}
		}

		private void PlayVideo(Video video)
		{
			if (Browser.CanExecuteJavascriptInMainFrame)
			{
				var ytVideo = video as YouTubeVideo;
				CurrentVideo = ytVideo;

				if (CurrentVideo.PlayAt > 0)
				{
					ActionsAfterPlay.Enqueue(() => SeekTo(CurrentVideo.PlayAt));
				}

				if (!PreLoaded)
				{
					var playerHtml = PLAYER_HTML.Replace("$VIDEO_ID$", ytVideo.VideoId);
					Browser.LoadHtml(playerHtml);
					PreLoaded = true;
				}
				else
				{
					Browser.ExecuteScriptAsync($"loadById('{ytVideo.VideoId}');");
				}
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

		public void Stop()
		{
			Browser.ExecuteScriptAsync($"stop();");
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

		public async Task<int> GetCurrentTime()
		{
			var task = Browser.GetMainFrame().EvaluateScriptAsync("getCurrentTime();", null);

			await task.ContinueWith(t =>
			{
				if (!t.IsFaulted)
				{
					var response = t.Result;
					return response.Success ? response.Result : -1;
				}

				return -1;
			}, TaskScheduler.FromCurrentSynchronizationContext());

			return -1;
		}

		public void SeekTo(int seconds)
		{
			Browser.ExecuteScriptAsync($"seekTo({seconds}, true);");
		}

		private void BoundObject_Playing(object sender, System.EventArgs e)
		{
			while (ActionsAfterPlay.Count > 0)
			{
				ActionsAfterPlay.Dequeue()?.Invoke();
			}
		}
	}
}
