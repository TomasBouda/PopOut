namespace YouPipe
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.wb = new System.Windows.Forms.WebBrowser();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listQueue = new System.Windows.Forms.ListBox();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// wb
			// 
			this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wb.Location = new System.Drawing.Point(1, 1);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.Size = new System.Drawing.Size(278, 27);
			this.wb.TabIndex = 0;
			this.wb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
			// 
			// txtAddress
			// 
			this.txtAddress.BackColor = System.Drawing.Color.Black;
			this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtAddress.ContextMenuStrip = this.contextMenuStrip1;
			this.txtAddress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtAddress.ForeColor = System.Drawing.SystemColors.Window;
			this.txtAddress.Location = new System.Drawing.Point(1, 28);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(278, 13);
			this.txtAddress.TabIndex = 1;
			this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
			this.txtAddress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtAddress_MouseDown);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.nextToolStripMenuItem,
            this.showListToolStripMenuItem,
            this.minimizeToolStripMenuItem,
            this.closeToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(129, 114);
			// 
			// playToolStripMenuItem
			// 
			this.playToolStripMenuItem.Name = "playToolStripMenuItem";
			this.playToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.playToolStripMenuItem.Text = "Play";
			this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
			// 
			// nextToolStripMenuItem
			// 
			this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
			this.nextToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.nextToolStripMenuItem.Text = "Next";
			this.nextToolStripMenuItem.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
			// 
			// showListToolStripMenuItem
			// 
			this.showListToolStripMenuItem.Name = "showListToolStripMenuItem";
			this.showListToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.showListToolStripMenuItem.Text = "Toggle list";
			this.showListToolStripMenuItem.Click += new System.EventHandler(this.showListToolStripMenuItem_Click);
			// 
			// minimizeToolStripMenuItem
			// 
			this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
			this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.minimizeToolStripMenuItem.Text = "Minimize";
			this.minimizeToolStripMenuItem.Click += new System.EventHandler(this.minimizeToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// listQueue
			// 
			this.listQueue.BackColor = System.Drawing.Color.LightGray;
			this.listQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listQueue.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listQueue.FormattingEnabled = true;
			this.listQueue.Location = new System.Drawing.Point(1, 41);
			this.listQueue.Name = "listQueue";
			this.listQueue.Size = new System.Drawing.Size(278, 143);
			this.listQueue.TabIndex = 2;
			this.listQueue.Visible = false;
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(280, 185);
			this.Controls.Add(this.wb);
			this.Controls.Add(this.txtAddress);
			this.Controls.Add(this.listQueue);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "YouPipe";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser wb;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showListToolStripMenuItem;
		private System.Windows.Forms.ListBox listQueue;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
	}
}

