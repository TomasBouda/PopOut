using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace PopOut.Player.Players.YouTube
{
    public class YouTubeVideo : Video, IVideo
    {
        public YouTubeVideo(string url) : base(url)
        {
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
