using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using YoutubeExtractor;

namespace PopOut.Player.Players.YouTube
{
	public class YouTubeVideo : Video, IVideo
	{
		public const string RGX_YOUTUBE_URL = "youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)(?<videoId>[a-zA-Z0-9-_]+)((?:[?|&](?:t=))(?<playAt>.*))?";

		public string VideoId { get; set; }

		public YouTubeVideo(string url) : base(url)
		{
			var match = Regex.Match(url, RGX_YOUTUBE_URL);
			VideoId = match.Groups["videoId"].Value;
			PlayAt = int.Parse(match.Groups["playAt"].Value.Replace("s", ""));

			try
			{
				IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url);
				var video = videoInfos.OrderByDescending(v => v.Resolution).FirstOrDefault();
				Title = video.Title;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
	}
}
