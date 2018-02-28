using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TomLabs.Shadowgem.Extensions.String;
using YoutubeExtractor;

namespace PopOut.Player.Players.YouTube
{
	public class YouTubeVideo : Video, IVideo, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public const string RGX_YOUTUBE_URL = "youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)(?<videoId>[a-zA-Z0-9-_]+)((?:[?|&](?:t=))(?<playAt>.*))?";

		public string VideoId { get; set; }

		public YouTubeVideo(string url) : base(url)
		{
			var match = Regex.Match(url, RGX_YOUTUBE_URL);
			VideoId = match.Groups["videoId"].Value;
			PlayAt = match.Groups["playAt"].Value.Replace("s", "").ToInt(0);

			ResolveTitle();
		}

		public async void ResolveTitle()
		{
			try
			{
				await Task.Run(() =>
				{
					IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls($"http://www.youtube.com/watch?v={VideoId}");
					var video = videoInfos.OrderByDescending(v => v.Resolution).FirstOrDefault();
					Title = video.Title;

					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
				});
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
	}
}
