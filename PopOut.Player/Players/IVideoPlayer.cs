using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PopOut.Player.Players
{
	public interface IVideoPlayer
	{
		IVideo CurrentVideo { get; }
		ObservableCollection<IVideo> PlayList { get; }

		void PlayOrQueue(string videoUrl);
		void PlayFromQueue(Video video);
		void Pause();
		void Play();
		void Toggle();
		void Stop();
		Task<int> GetCurrentTime();
	}
}
