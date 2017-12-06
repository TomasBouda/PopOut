using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopOut.Player.Players
{
    public interface IVideoPlayer
    {
        void PlayOrQueue(string videoUrl);
        void PlayFromQueue(Video video);
    }
}
