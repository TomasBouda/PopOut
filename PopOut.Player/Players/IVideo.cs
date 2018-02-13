namespace PopOut.Player.Players
{
	public interface IVideo
	{
		string Title { get; set; }
		string Url { get; set; }
		int PlayAt { get; set; }
	}
}
