using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopOut.Player.ViewModels
{
    public enum YouTubePlayerState
    {
        UNSTARTED = -1,
        ENDED = 0,
        PLAYING = 1,
        PAUSED = 2,
        BUFFERING = 3,
        CUED = 5
    }

    public class PlayerBoundObject
    {
        public event EventHandler Playing;
        public event EventHandler Paused;
        public event EventHandler Ended;

        public YouTubePlayerState CurrentState { get; set; }

        public void OnPlayerStateChange(YouTubePlayerState state)
        {
            CurrentState = state;
            switch (CurrentState)
            {
                case YouTubePlayerState.PLAYING:
                    Playing?.Invoke(this, new EventArgs());
                    break;
                case YouTubePlayerState.PAUSED:
                    Paused?.Invoke(this, new EventArgs());
                    break;
                case YouTubePlayerState.ENDED:
                    Ended?.Invoke(this, new EventArgs());
                    break;
                default: break;
            }
        }
    }
}
