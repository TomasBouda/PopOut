namespace PopOut.Player.Players
{
	public interface IVideoPlayer
	{
		void PlayOrQueue(string videoUrl);
		void PlayFromQueue(Video video);
		void Pause();
		void Play();
		void Toggle();
		void Stop();
	}
}
