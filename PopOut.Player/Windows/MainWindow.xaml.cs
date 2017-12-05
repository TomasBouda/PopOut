using CefSharp;
using PopOut.Player;
using PopOut.Player.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YoutubeExtractor;

namespace YouPipe.Player
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private HotKey _hotKey;
        

        private PlayerViewModel VM { get; set; }

		public MainWindow()
		{
			InitializeComponent();

            VM = new PlayerViewModel(cefBrowser);
            this.DataContext = VM;
		}

		#region Public Methods

		#endregion

        private void Init()
        {
            _hotKey = new HotKey(Key.X, KeyModifier.Shift | KeyModifier.Ctrl, OnHotKeyHandler);
        }

        #region Event Handlers

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
				((TextBox)sender).Focus();
			}
		}

		private void TextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				VM.PlayOrQueue(txtVideoUrl.Text);
			}
		}

		private void Window_Deactivated(object sender, EventArgs e)
		{
			Window window = (Window)sender;
			window.Topmost = true;
		}

		private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listPlayList.SelectedItem != null)
			{
				var video = listPlayList.SelectedItem as Video;

                VM.PlayFromQueue(video);
			}
		}

        private void OnHotKeyHandler(HotKey hotKey)
        {
            if(WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Minimized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        #endregion
        
    }
}
