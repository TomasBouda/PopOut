using System;

namespace PopOut.Player.Players.YouTube
{
	public class PlayerBoundObject
	{
		public event EventHandler Playing;
		public event EventHandler Paused;
		public event EventHandler Ended;

		public EYouTubePlayerState CurrentState { get; set; }

		public void OnPlayerStateChange(EYouTubePlayerState state)
		{
			CurrentState = state;
			switch (CurrentState)
			{
				case EYouTubePlayerState.PLAYING:
					Playing?.Invoke(this, new EventArgs());
					break;
				case EYouTubePlayerState.PAUSED:
					Paused?.Invoke(this, new EventArgs());
					break;
				case EYouTubePlayerState.ENDED:
					Ended?.Invoke(this, new EventArgs());
					break;
				default: break;
			}
		}

		public void onPlayerReady()
		{

		}
	}
}
