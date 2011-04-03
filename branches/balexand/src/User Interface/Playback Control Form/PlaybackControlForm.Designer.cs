namespace FractalBlaster.PlaybackControlForm
{
    partial class PlaybackControlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaybackControlForm));
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFileToPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VolumeControl = new System.Windows.Forms.Panel();
            this.SeekBarPanel = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.repeatButton = new System.Windows.Forms.Button();
            this.shuffleButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.seekBarRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fractal Blasters";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 25);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(181, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.addFileToPlaylistToolStripMenuItem,
            this.openPlaylistToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // addFileToPlaylistToolStripMenuItem
            // 
            this.addFileToPlaylistToolStripMenuItem.Name = "addFileToPlaylistToolStripMenuItem";
            this.addFileToPlaylistToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addFileToPlaylistToolStripMenuItem.Text = "Add File to Playlist";
            this.addFileToPlaylistToolStripMenuItem.Click += new System.EventHandler(this.addFileToPlaylistToolStripMenuItem_Click);
            // 
            // openPlaylistToolStripMenuItem
            // 
            this.openPlaylistToolStripMenuItem.Name = "openPlaylistToolStripMenuItem";
            this.openPlaylistToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openPlaylistToolStripMenuItem.Text = "Open Playlist";
            this.openPlaylistToolStripMenuItem.Click += new System.EventHandler(this.openPlaylistToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.CheckOnClick = true;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.CheckedChanged += new System.EventHandler(this.debugToolStripMenuItem_CheckedChanged);
            this.debugToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.debugToolStripMenuItem_Paint);
            // 
            // VolumeControl
            // 
            this.VolumeControl.Location = new System.Drawing.Point(288, 80);
            this.VolumeControl.Name = "VolumeControl";
            this.VolumeControl.Size = new System.Drawing.Size(101, 40);
            this.VolumeControl.TabIndex = 10;
            this.VolumeControl.Paint += new System.Windows.Forms.PaintEventHandler(this.VolumeControl_Paint);
            this.VolumeControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VolumeControl_MouseDown);
            this.VolumeControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VolumeControl_MouseMove);
            this.VolumeControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VolumeControl_MouseUp);
            // 
            // SeekBarPanel
            // 
            this.SeekBarPanel.Location = new System.Drawing.Point(12, 52);
            this.SeekBarPanel.Name = "SeekBarPanel";
            this.SeekBarPanel.Size = new System.Drawing.Size(376, 22);
            this.SeekBarPanel.TabIndex = 11;
            // 
            // closeButton
            // 
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Image = global::FractalBlaster.PlaybackControlForm.Properties.Resources.close;
            this.closeButton.Location = new System.Drawing.Point(375, 5);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 7;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // repeatButton
            // 
            this.repeatButton.FlatAppearance.BorderSize = 0;
            this.repeatButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.repeatButton.Image = global::FractalBlaster.PlaybackControlForm.Properties.Resources.repeat;
            this.repeatButton.Location = new System.Drawing.Point(242, 80);
            this.repeatButton.Name = "repeatButton";
            this.repeatButton.Size = new System.Drawing.Size(40, 40);
            this.repeatButton.TabIndex = 5;
            this.repeatButton.UseVisualStyleBackColor = true;
            this.repeatButton.Click += new System.EventHandler(this.repeatButton_Click);
            this.repeatButton.Paint += new System.Windows.Forms.PaintEventHandler(this.repeatButton_Paint);
            // 
            // shuffleButton
            // 
            this.shuffleButton.FlatAppearance.BorderSize = 0;
            this.shuffleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shuffleButton.Image = global::FractalBlaster.PlaybackControlForm.Properties.Resources.shuffle;
            this.shuffleButton.Location = new System.Drawing.Point(196, 80);
            this.shuffleButton.Name = "shuffleButton";
            this.shuffleButton.Size = new System.Drawing.Size(40, 40);
            this.shuffleButton.TabIndex = 4;
            this.shuffleButton.UseVisualStyleBackColor = true;
            this.shuffleButton.Click += new System.EventHandler(this.shuffleButton_Click);
            this.shuffleButton.Paint += new System.Windows.Forms.PaintEventHandler(this.shuffleButton_Paint);
            // 
            // nextButton
            // 
            this.nextButton.FlatAppearance.BorderSize = 0;
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextButton.Image = global::FractalBlaster.PlaybackControlForm.Properties.Resources.next;
            this.nextButton.Location = new System.Drawing.Point(150, 80);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(40, 40);
            this.nextButton.TabIndex = 3;
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            this.nextButton.Paint += new System.Windows.Forms.PaintEventHandler(this.nextButton_Paint);
            // 
            // playButton
            // 
            this.playButton.FlatAppearance.BorderSize = 0;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Image = global::FractalBlaster.PlaybackControlForm.Properties.Resources.play;
            this.playButton.Location = new System.Drawing.Point(104, 80);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(40, 40);
            this.playButton.TabIndex = 2;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            this.playButton.Paint += new System.Windows.Forms.PaintEventHandler(this.playButton_Paint);
            // 
            // previousButton
            // 
            this.previousButton.FlatAppearance.BorderSize = 0;
            this.previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousButton.Image = global::FractalBlaster.PlaybackControlForm.Properties.Resources.previous;
            this.previousButton.Location = new System.Drawing.Point(58, 80);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(40, 40);
            this.previousButton.TabIndex = 1;
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            this.previousButton.Paint += new System.Windows.Forms.PaintEventHandler(this.previousButton_Paint);
            // 
            // stopButton
            // 
            this.stopButton.FlatAppearance.BorderSize = 0;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.Location = new System.Drawing.Point(12, 80);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(40, 40);
            this.stopButton.TabIndex = 0;
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            this.stopButton.Paint += new System.Windows.Forms.PaintEventHandler(this.stopButton_Paint);
            // 
            // seekBarRefreshTimer
            // 
            this.seekBarRefreshTimer.Interval = 200;
            this.seekBarRefreshTimer.Tick += new System.EventHandler(this.seekBarRefreshTimer_Tick);
            // 
            // PlaybackControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 130);
            this.Controls.Add(this.SeekBarPanel);
            this.Controls.Add(this.VolumeControl);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.repeatButton);
            this.Controls.Add(this.shuffleButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PlaybackControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PlaybackControlForm";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaybackControlForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PlaybackControlForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlaybackControlForm_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button shuffleButton;
        private System.Windows.Forms.Button repeatButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPlaylistToolStripMenuItem;
        private System.Windows.Forms.Panel VolumeControl;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFileToPlaylistToolStripMenuItem;
        private System.Windows.Forms.Panel SeekBarPanel;
        private System.Windows.Forms.Timer seekBarRefreshTimer;

    }
}