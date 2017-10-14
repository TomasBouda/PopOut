using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouPipe
{
	public class Settings
	{
		public int Height { get; private set; }
		public int Width { get; private set; }
		public int PosX { get; private set; }
		public int PosY { get; private set; }
		public IEnumerable<string> Links { get; private set; }
		private static Settings _instance { get; set; }

		private Settings(int height, int width, IEnumerable<string> links)
		{
			Height = height;
			Width = width;

			Links = links?.Cast<string>().ToList();
		}

		public static Settings Load(bool raw = false)
		{
			if (_instance == null || raw)
				_instance = new Settings(
					Properties.Settings.Default.Height, 
					Properties.Settings.Default.Width, 
					Properties.Settings.Default.Links?.Cast<string>().ToList() );

			return _instance;
		}

		public static void Save(int height, int width, IEnumerable<string> links)
		{
			Properties.Settings.Default.Height = height;
			Properties.Settings.Default.Width = width;

			StringCollection collection = new StringCollection();
			collection.AddRange(links.ToArray());
			Properties.Settings.Default.Links = collection;

			Properties.Settings.Default.Save();
		}
	}
}
