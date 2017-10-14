using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace YouPipe
{
	public partial class MainForm : Form
	{
		#region Imports
		[DllImport("User32.dll")]
		public static extern bool ReleaseCapture();
		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		#endregion

		public const string YT_ADDRESS_RGX = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HTCAPTION = 0x2;
		private const int SIZE_INCREMENT = 10;

		private bool _minimized = false;
		private bool _listVisible = false;
		private string _clipboardCache;

		public MainForm()
		{
			InitializeComponent();

			InitHotkey();
		}

		#region Private methods
		private void InitHotkey()
		{
			Hotkey hk = new Hotkey();
			hk.KeyCode = Keys.X;
			hk.Control = true;
			hk.Shift = true;
			hk.Pressed += Hk_Pressed;
			hk.Register(this);
		}

		private void Play(string url)
		{
			string address = url.Replace("watch?v=", "embed/") + "?version=3&autoplay=1";
			wb.Navigate(address);
			wb.ScriptErrorsSuppressed = true;
		}

		private void TogglePause()
		{
			wb.Document.GetElementById("player").InvokeMember("click");
		}

		private void PlayFromQueue()
		{
			if (listQueue.Items.Count > 0)
			{
				var url = listQueue.Items[0].ToString();
				listQueue.Items.RemoveAt(0);
				txtAddress.Text = url;
				Play(url);
			}
		} 

		#endregion

		#region Event Handlers

		private void Hk_Pressed(object sender, HandledEventArgs e)
		{
			if (_minimized)
			{
				Show();
				TogglePause();
				_minimized = false;
			}
			else
			{
				Hide();
				TogglePause();
				_minimized = true;
			}
		}

		private void txtAddress_MouseDown(object sender, MouseEventArgs e)
		{
			// Form mouse drag
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
			}
		}

		private void playToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (txtAddress.Text != "")
				Play(txtAddress.Text);
			else
				PlayFromQueue();
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void txtAddress_KeyDown(object sender, KeyEventArgs e)
		{
			// Form resizing with arrow keys
			if (e.KeyCode == Keys.Down)
			{
				Height -= SIZE_INCREMENT;
				Top += SIZE_INCREMENT;
			}
			else if (e.KeyCode == Keys.Up)
			{
				Height += SIZE_INCREMENT;
				Top -= SIZE_INCREMENT;
			}
			else if (e.KeyCode == Keys.Right)
			{
				Width -= SIZE_INCREMENT;
				Left += SIZE_INCREMENT;
			}
			else if (e.KeyCode == Keys.Left)
			{
				Width += SIZE_INCREMENT;
				Left -= SIZE_INCREMENT;
			}
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
		}

		private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}

		private void showListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_listVisible = !_listVisible;
			listQueue.Visible = _listVisible;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			string clipboardtext = Clipboard.GetText();

			if (clipboardtext != _clipboardCache && Regex.Match(clipboardtext, YT_ADDRESS_RGX).Success)
			{
				listQueue.Items.Add(clipboardtext);
				_clipboardCache = clipboardtext;

				if(txtAddress.Text == "")
				{
					txtAddress.Text = clipboardtext;
					PlayFromQueue();
				}
			}
		}

		private void nextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PlayFromQueue();	
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Height = Settings.Load().Height;
			Width = Settings.Load().Width;
			Top = Settings.Load().PosX;
			Left = Settings.Load().PosY;

			if (Settings.Load().Links != null)
				listQueue.Items.AddRange(Settings.Load().Links.ToArray());
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Save(Height, Width, listQueue.Items.Cast<string>());
		}

		#endregion
	}
}
