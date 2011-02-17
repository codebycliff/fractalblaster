namespace FractalBlaster.Core.UI {
    partial class ProductForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mNewPlaylistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mOpenPlaylistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mConfigureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mViewsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mEffectsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mHelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mTutorialsMenuDropDown = new System.Windows.Forms.ToolStripMenuItem();
            this.mTutorialsWritingPluginsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mStandardToolBar = new System.Windows.Forms.ToolStrip();
            this.mOpenToolBarDropDown = new System.Windows.Forms.ToolStripSplitButton();
            this.mOpenPlaylistToolBarButton = new System.Windows.Forms.ToolStripMenuItem();
            this.mNewToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mSaveToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mConfigureToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mExitToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mPlaylistTabControl = new System.Windows.Forms.TabControl();
            this.mAudioControlToolBar = new System.Windows.Forms.ToolStrip();
            this.mPlayToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mStopToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mPauseToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mSkipBackwardToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mSkipForwardToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mMenuStrip.SuspendLayout();
            this.mStandardToolBar.SuspendLayout();
            this.mPlaylistTabControl.SuspendLayout();
            this.mAudioControlToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mMenuStrip
            // 
            this.mMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFileMenu,
            this.mViewsMenu,
            this.mEffectsMenu,
            this.mHelpMenu});
            this.mMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mMenuStrip.Name = "mMenuStrip";
            this.mMenuStrip.Size = new System.Drawing.Size(287, 24);
            this.mMenuStrip.TabIndex = 0;
            this.mMenuStrip.Text = "menuStrip1";
            // 
            // mFileMenu
            // 
            this.mFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mNewPlaylistMenuItem,
            this.mOpenPlaylistMenuItem,
            this.toolStripSeparator2,
            this.mSaveMenuItem,
            this.mSaveAsMenuItem,
            this.toolStripSeparator3,
            this.mConfigureMenuItem,
            this.toolStripSeparator1,
            this.mExitMenuItem});
            this.mFileMenu.Name = "mFileMenu";
            this.mFileMenu.Size = new System.Drawing.Size(37, 20);
            this.mFileMenu.Text = "File";
            // 
            // mNewPlaylistMenuItem
            // 
            this.mNewPlaylistMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.document_new;
            this.mNewPlaylistMenuItem.Name = "mNewPlaylistMenuItem";
            this.mNewPlaylistMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mNewPlaylistMenuItem.Text = "New Playlist";
            // 
            // mOpenPlaylistMenuItem
            // 
            this.mOpenPlaylistMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.document_open;
            this.mOpenPlaylistMenuItem.Name = "mOpenPlaylistMenuItem";
            this.mOpenPlaylistMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mOpenPlaylistMenuItem.Text = "Open Playlist...";
            this.mOpenPlaylistMenuItem.Click += new System.EventHandler(this.openPlaylistToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // mSaveMenuItem
            // 
            this.mSaveMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.document_save;
            this.mSaveMenuItem.Name = "mSaveMenuItem";
            this.mSaveMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mSaveMenuItem.Text = "Save";
            // 
            // mSaveAsMenuItem
            // 
            this.mSaveAsMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.document_save_as;
            this.mSaveAsMenuItem.Name = "mSaveAsMenuItem";
            this.mSaveAsMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mSaveAsMenuItem.Text = "Save As...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // mConfigureMenuItem
            // 
            this.mConfigureMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.configure;
            this.mConfigureMenuItem.Name = "mConfigureMenuItem";
            this.mConfigureMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mConfigureMenuItem.Text = "Settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // mExitMenuItem
            // 
            this.mExitMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.application_exit;
            this.mExitMenuItem.Name = "mExitMenuItem";
            this.mExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mExitMenuItem.Text = "Exit";
            this.mExitMenuItem.Click += new System.EventHandler(this.mFileMenuExitItem_Click);
            // 
            // mViewsMenu
            // 
            this.mViewsMenu.Name = "mViewsMenu";
            this.mViewsMenu.Size = new System.Drawing.Size(49, 20);
            this.mViewsMenu.Text = "Views";
            // 
            // mEffectsMenu
            // 
            this.mEffectsMenu.Name = "mEffectsMenu";
            this.mEffectsMenu.Size = new System.Drawing.Size(54, 20);
            this.mEffectsMenu.Text = "Effects";
            // 
            // mHelpMenu
            // 
            this.mHelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTutorialsMenuDropDown});
            this.mHelpMenu.Name = "mHelpMenu";
            this.mHelpMenu.Size = new System.Drawing.Size(44, 20);
            this.mHelpMenu.Text = "Help";
            // 
            // mTutorialsMenuDropDown
            // 
            this.mTutorialsMenuDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mTutorialsWritingPluginsMenuItem});
            this.mTutorialsMenuDropDown.Image = global::FractalBlaster.Core.Properties.Resources.documentation;
            this.mTutorialsMenuDropDown.Name = "mTutorialsMenuDropDown";
            this.mTutorialsMenuDropDown.Size = new System.Drawing.Size(120, 22);
            this.mTutorialsMenuDropDown.Text = "Tutorials";
            // 
            // mTutorialsWritingPluginsMenuItem
            // 
            this.mTutorialsWritingPluginsMenuItem.Name = "mTutorialsWritingPluginsMenuItem";
            this.mTutorialsWritingPluginsMenuItem.Size = new System.Drawing.Size(155, 22);
            this.mTutorialsWritingPluginsMenuItem.Text = "Writing Plugins";
            // 
            // mStandardToolBar
            // 
            this.mStandardToolBar.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.mStandardToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpenToolBarDropDown,
            this.mNewToolBarButton,
            this.mSaveToolBarButton,
            this.mConfigureToolBarButton,
            this.mExitToolBarButton});
            this.mStandardToolBar.Location = new System.Drawing.Point(0, 24);
            this.mStandardToolBar.Name = "mStandardToolBar";
            this.mStandardToolBar.Size = new System.Drawing.Size(287, 55);
            this.mStandardToolBar.TabIndex = 2;
            this.mStandardToolBar.Text = "toolStrip1";
            // 
            // mOpenToolBarDropDown
            // 
            this.mOpenToolBarDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mOpenToolBarDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpenPlaylistToolBarButton});
            this.mOpenToolBarDropDown.Image = global::FractalBlaster.Core.Properties.Resources.document_open;
            this.mOpenToolBarDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mOpenToolBarDropDown.Name = "mOpenToolBarDropDown";
            this.mOpenToolBarDropDown.Size = new System.Drawing.Size(64, 52);
            this.mOpenToolBarDropDown.Text = "toolStripSplitButton1";
            // 
            // mOpenPlaylistToolBarButton
            // 
            this.mOpenPlaylistToolBarButton.Name = "mOpenPlaylistToolBarButton";
            this.mOpenPlaylistToolBarButton.Size = new System.Drawing.Size(152, 22);
            this.mOpenPlaylistToolBarButton.Text = "Open Playlist...";
            this.mOpenPlaylistToolBarButton.Click += new System.EventHandler(this.openPlaylistToolStripMenuItem1_Click);
            // 
            // mNewToolBarButton
            // 
            this.mNewToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mNewToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.document_new;
            this.mNewToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNewToolBarButton.Name = "mNewToolBarButton";
            this.mNewToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mNewToolBarButton.Text = "toolStripButton9";
            // 
            // mSaveToolBarButton
            // 
            this.mSaveToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSaveToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.document_save;
            this.mSaveToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSaveToolBarButton.Name = "mSaveToolBarButton";
            this.mSaveToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mSaveToolBarButton.Text = "toolStripButton7";
            // 
            // mConfigureToolBarButton
            // 
            this.mConfigureToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mConfigureToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.configure;
            this.mConfigureToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mConfigureToolBarButton.Name = "mConfigureToolBarButton";
            this.mConfigureToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mConfigureToolBarButton.Text = "toolStripButton8";
            this.mConfigureToolBarButton.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // mExitToolBarButton
            // 
            this.mExitToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mExitToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.application_exit;
            this.mExitToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mExitToolBarButton.Name = "mExitToolBarButton";
            this.mExitToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mExitToolBarButton.Text = "toolStripButton6";
            this.mExitToolBarButton.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(279, 156);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Queue";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mPlaylistTabControl
            // 
            this.mPlaylistTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mPlaylistTabControl.Controls.Add(this.tabPage1);
            this.mPlaylistTabControl.Location = new System.Drawing.Point(0, 82);
            this.mPlaylistTabControl.Name = "mPlaylistTabControl";
            this.mPlaylistTabControl.SelectedIndex = 0;
            this.mPlaylistTabControl.Size = new System.Drawing.Size(287, 182);
            this.mPlaylistTabControl.TabIndex = 3;
            // 
            // mAudioControlToolBar
            // 
            this.mAudioControlToolBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mAudioControlToolBar.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.mAudioControlToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mPlayToolBarButton,
            this.mStopToolBarButton,
            this.mPauseToolBarButton,
            this.mSkipBackwardToolBarButton,
            this.mSkipForwardToolBarButton});
            this.mAudioControlToolBar.Location = new System.Drawing.Point(0, 267);
            this.mAudioControlToolBar.Name = "mAudioControlToolBar";
            this.mAudioControlToolBar.Size = new System.Drawing.Size(287, 55);
            this.mAudioControlToolBar.TabIndex = 4;
            this.mAudioControlToolBar.Text = "toolStrip1";
            // 
            // mPlayToolBarButton
            // 
            this.mPlayToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPlayToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_playback_start;
            this.mPlayToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPlayToolBarButton.Name = "mPlayToolBarButton";
            this.mPlayToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mPlayToolBarButton.Text = "toolStripButton1";
            // 
            // mStopToolBarButton
            // 
            this.mStopToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mStopToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_playback_stop;
            this.mStopToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mStopToolBarButton.Name = "mStopToolBarButton";
            this.mStopToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mStopToolBarButton.Text = "toolStripButton2";
            // 
            // mPauseToolBarButton
            // 
            this.mPauseToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPauseToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_playback_pause;
            this.mPauseToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPauseToolBarButton.Name = "mPauseToolBarButton";
            this.mPauseToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mPauseToolBarButton.Text = "toolStripButton3";
            // 
            // mSkipBackwardToolBarButton
            // 
            this.mSkipBackwardToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSkipBackwardToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_seek_backward;
            this.mSkipBackwardToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSkipBackwardToolBarButton.Name = "mSkipBackwardToolBarButton";
            this.mSkipBackwardToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mSkipBackwardToolBarButton.Text = "toolStripButton4";
            // 
            // mSkipForwardToolBarButton
            // 
            this.mSkipForwardToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSkipForwardToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_skip_forward;
            this.mSkipForwardToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSkipForwardToolBarButton.Name = "mSkipForwardToolBarButton";
            this.mSkipForwardToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mSkipForwardToolBarButton.Text = "toolStripButton5";
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 322);
            this.Controls.Add(this.mAudioControlToolBar);
            this.Controls.Add(this.mPlaylistTabControl);
            this.Controls.Add(this.mStandardToolBar);
            this.Controls.Add(this.mMenuStrip);
            this.MainMenuStrip = this.mMenuStrip;
            this.Name = "ProductForm";
            this.Text = "ProductForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProductForm_FormClosed);
            this.mMenuStrip.ResumeLayout(false);
            this.mMenuStrip.PerformLayout();
            this.mStandardToolBar.ResumeLayout(false);
            this.mStandardToolBar.PerformLayout();
            this.mPlaylistTabControl.ResumeLayout(false);
            this.mAudioControlToolBar.ResumeLayout(false);
            this.mAudioControlToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mFileMenu;
        private System.Windows.Forms.ToolStripMenuItem mExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mViewsMenu;
        private System.Windows.Forms.ToolStripMenuItem mHelpMenu;
        private System.Windows.Forms.ToolStripMenuItem mOpenPlaylistMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip mStandardToolBar;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl mPlaylistTabControl;
        private System.Windows.Forms.ToolStripSplitButton mOpenToolBarDropDown;
        private System.Windows.Forms.ToolStripMenuItem mOpenPlaylistToolBarButton;
        private System.Windows.Forms.ToolStripButton mExitToolBarButton;
        private System.Windows.Forms.ToolStripMenuItem mConfigureMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mTutorialsMenuDropDown;
        private System.Windows.Forms.ToolStripMenuItem mTutorialsWritingPluginsMenuItem;
        private System.Windows.Forms.ToolStripButton mSaveToolBarButton;
        private System.Windows.Forms.ToolStripMenuItem mEffectsMenu;
        private System.Windows.Forms.ToolStripButton mNewToolBarButton;
        private System.Windows.Forms.ToolStrip mAudioControlToolBar;
        private System.Windows.Forms.ToolStripButton mConfigureToolBarButton;
        private System.Windows.Forms.ToolStripButton mPlayToolBarButton;
        private System.Windows.Forms.ToolStripButton mStopToolBarButton;
        private System.Windows.Forms.ToolStripButton mPauseToolBarButton;
        private System.Windows.Forms.ToolStripButton mSkipBackwardToolBarButton;
        private System.Windows.Forms.ToolStripButton mSkipForwardToolBarButton;
        private System.Windows.Forms.ToolStripMenuItem mNewPlaylistMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mSaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}