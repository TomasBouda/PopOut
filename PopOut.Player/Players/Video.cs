﻿namespace PopOut.Player.Players
{
	public abstract class Video : IVideo
	{
		public string Title { get; set; }
		public string Url { get; set; }
		public int PlayAt { get; set; }

		public Video(string url, string title = null)
		{
			Url = url;
			Title = title;
		}
	}
}
