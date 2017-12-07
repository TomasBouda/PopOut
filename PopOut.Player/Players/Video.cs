using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace PopOut.Player.Players
{
    public class Video : IVideo
    {
        public string Title { get; set; }
        public string Url { get; set; }

        public Video(string url, string title = null)
        {
            Url = url;
            Title = title;
        }
    }
}
