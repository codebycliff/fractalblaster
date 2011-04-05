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
            this.components = new System.ComponentModel.Container();
            this.mMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFileToPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mCurrentSelectedMediaLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mAudioControlToolBar = new System.Windows.Forms.ToolStrip();
            this.mPlayToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mStopToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mPauseToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mSkipBackwardToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.mSkipForwardToolBarButton = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mCollectionTabControl = new System.Windows.Forms.TabControl();
            this.mLibraryCollectionTabPage = new System.Windows.Forms.TabPage();
            this.mFileSystemCollectionTabPage = new System.Windows.Forms.TabPage();
            this.mPlaylistTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SeekBarPanel = new System.Windows.Forms.Panel();
            this.seekBarRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.VolumeControl = new System.Windows.Forms.Panel();
            this.mMenuStrip.SuspendLayout();
            this.mStandardToolBar.SuspendLayout();
            this.mStatusStrip.SuspendLayout();
            this.mAudioControlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mCollectionTabControl.SuspendLayout();
            this.mPlaylistTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // mMenuStrip
            // 
            this.mMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFileMenu,
            this.mViewsMenu,
            this.mEffectsMenu,
            this.mHelpMenu});
            this.mMenuStrip.Location = new System.Drawing.Point(0, 27);
            this.mMenuStrip.Name = "mMenuStrip";
            this.mMenuStrip.Size = new System.Drawing.Size(192, 24);
            this.mMenuStrip.TabIndex = 0;
            this.mMenuStrip.Text = "menuStrip1";
            this.mMenuStrip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseDown);
            this.mMenuStrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseMove);
            this.mMenuStrip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseUp);
            // 
            // mFileMenu
            // 
            this.mFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.addFileToPlaylistToolStripMenuItem,
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
            // mNewPlaylistMenuItem
            // 
            this.mNewPlaylistMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.document_new;
            this.mNewPlaylistMenuItem.Name = "mNewPlaylistMenuItem";
            this.mNewPlaylistMenuItem.Size = new System.Drawing.Size(171, 22);
            this.mNewPlaylistMenuItem.Text = "New Playlist";
            // 
            // mOpenPlaylistMenuItem
            // 
            this.mOpenPlaylistMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.document_open;
            this.mOpenPlaylistMenuItem.Name = "mOpenPlaylistMenuItem";
            this.mOpenPlaylistMenuItem.Size = new System.Drawing.Size(171, 22);
            this.mOpenPlaylistMenuItem.Text = "Open Playlist...";
            this.mOpenPlaylistMenuItem.Click += new System.EventHandler(this.OpenPlaylist);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
            // 
            // mSaveMenuItem
            // 
            this.mSaveMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.document_save;
            this.mSaveMenuItem.Name = "mSaveMenuItem";
            this.mSaveMenuItem.Size = new System.Drawing.Size(171, 22);
            this.mSaveMenuItem.Text = "Save";
            // 
            // mSaveAsMenuItem
            // 
            this.mSaveAsMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.document_save_as;
            this.mSaveAsMenuItem.Name = "mSaveAsMenuItem";
            this.mSaveAsMenuItem.Size = new System.Drawing.Size(171, 22);
            this.mSaveAsMenuItem.Text = "Save As...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
            // 
            // mConfigureMenuItem
            // 
            this.mConfigureMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.configure;
            this.mConfigureMenuItem.Name = "mConfigureMenuItem";
            this.mConfigureMenuItem.Size = new System.Drawing.Size(171, 22);
            this.mConfigureMenuItem.Text = "Settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // mExitMenuItem
            // 
            this.mExitMenuItem.Image = global::FractalBlaster.Core.Properties.Resources.application_exit;
            this.mExitMenuItem.Name = "mExitMenuItem";
            this.mExitMenuItem.Size = new System.Drawing.Size(171, 22);
            this.mExitMenuItem.Text = "Exit";
            this.mExitMenuItem.Click += new System.EventHandler(this.ExitApplication);
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
            this.mStandardToolBar.Dock = System.Windows.Forms.DockStyle.None;
            this.mStandardToolBar.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.mStandardToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOpenToolBarDropDown,
            this.mNewToolBarButton,
            this.mSaveToolBarButton,
            this.mConfigureToolBarButton,
            this.mExitToolBarButton});
            this.mStandardToolBar.Location = new System.Drawing.Point(0, 51);
            this.mStandardToolBar.Name = "mStandardToolBar";
            this.mStandardToolBar.Size = new System.Drawing.Size(284, 55);
            this.mStandardToolBar.TabIndex = 2;
            this.mStandardToolBar.Text = "toolStrip1";
            this.mStandardToolBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseDown);
            this.mStandardToolBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseMove);
            this.mStandardToolBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseUp);
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
            this.mOpenToolBarDropDown.Text = "Open";
            // 
            // mOpenPlaylistToolBarButton
            // 
            this.mOpenPlaylistToolBarButton.Name = "mOpenPlaylistToolBarButton";
            this.mOpenPlaylistToolBarButton.Size = new System.Drawing.Size(152, 22);
            this.mOpenPlaylistToolBarButton.Text = "Open Playlist...";
            this.mOpenPlaylistToolBarButton.Click += new System.EventHandler(this.OpenPlaylist);
            // 
            // mNewToolBarButton
            // 
            this.mNewToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mNewToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.document_new;
            this.mNewToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mNewToolBarButton.Name = "mNewToolBarButton";
            this.mNewToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mNewToolBarButton.Text = "New Playlist";
            this.mNewToolBarButton.Click += new System.EventHandler(this.AddNewPlaylistTab);
            // 
            // mSaveToolBarButton
            // 
            this.mSaveToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSaveToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.document_save;
            this.mSaveToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSaveToolBarButton.Name = "mSaveToolBarButton";
            this.mSaveToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mSaveToolBarButton.Text = "Save";
            // 
            // mConfigureToolBarButton
            // 
            this.mConfigureToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mConfigureToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.configure;
            this.mConfigureToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mConfigureToolBarButton.Name = "mConfigureToolBarButton";
            this.mConfigureToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mConfigureToolBarButton.Text = "Options";
            // 
            // mExitToolBarButton
            // 
            this.mExitToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mExitToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.application_exit;
            this.mExitToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mExitToolBarButton.Name = "mExitToolBarButton";
            this.mExitToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mExitToolBarButton.Text = "Exit";
            this.mExitToolBarButton.Click += new System.EventHandler(this.ExitApplication);
            // 
            // mStatusStrip
            // 
            this.mStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCurrentSelectedMediaLabel});
            this.mStatusStrip.Location = new System.Drawing.Point(0, 481);
            this.mStatusStrip.Name = "mStatusStrip";
            this.mStatusStrip.Size = new System.Drawing.Size(679, 22);
            this.mStatusStrip.TabIndex = 5;
            this.mStatusStrip.Text = "statusStrip1";
            // 
            // mCurrentSelectedMediaLabel
            // 
            this.mCurrentSelectedMediaLabel.Name = "mCurrentSelectedMediaLabel";
            this.mCurrentSelectedMediaLabel.Size = new System.Drawing.Size(0, 17);
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
            this.mAudioControlToolBar.Location = new System.Drawing.Point(0, 426);
            this.mAudioControlToolBar.Name = "mAudioControlToolBar";
            this.mAudioControlToolBar.Size = new System.Drawing.Size(679, 55);
            this.mAudioControlToolBar.TabIndex = 6;
            this.mAudioControlToolBar.Text = "toolStrip1";
            // 
            // mPlayToolBarButton
            // 
            this.mPlayToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPlayToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_playback_start;
            this.mPlayToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPlayToolBarButton.Name = "mPlayToolBarButton";
            this.mPlayToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mPlayToolBarButton.Text = "Play";
            this.mPlayToolBarButton.Click += new System.EventHandler(this.PlayMedia);
            // 
            // mStopToolBarButton
            // 
            this.mStopToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mStopToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_playback_stop;
            this.mStopToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mStopToolBarButton.Name = "mStopToolBarButton";
            this.mStopToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mStopToolBarButton.Text = "Stop";
            this.mStopToolBarButton.Click += new System.EventHandler(this.StopMedia);
            // 
            // mPauseToolBarButton
            // 
            this.mPauseToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mPauseToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_playback_pause;
            this.mPauseToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mPauseToolBarButton.Name = "mPauseToolBarButton";
            this.mPauseToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mPauseToolBarButton.Text = "Pause";
            this.mPauseToolBarButton.Click += new System.EventHandler(this.PauseMedia);
            // 
            // mSkipBackwardToolBarButton
            // 
            this.mSkipBackwardToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSkipBackwardToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_skip_backward;
            this.mSkipBackwardToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSkipBackwardToolBarButton.Name = "mSkipBackwardToolBarButton";
            this.mSkipBackwardToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mSkipBackwardToolBarButton.Text = "Previous";
            this.mSkipBackwardToolBarButton.Click += new System.EventHandler(this.SkipMediaBackward);
            // 
            // mSkipForwardToolBarButton
            // 
            this.mSkipForwardToolBarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mSkipForwardToolBarButton.Image = global::FractalBlaster.Core.Properties.Resources.media_skip_forward;
            this.mSkipForwardToolBarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mSkipForwardToolBarButton.Name = "mSkipForwardToolBarButton";
            this.mSkipForwardToolBarButton.Size = new System.Drawing.Size(52, 52);
            this.mSkipForwardToolBarButton.Text = "Next";
            this.mSkipForwardToolBarButton.Click += new System.EventHandler(this.SkipMediaForward);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Fractal Blasters Media Player";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 109);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mCollectionTabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mPlaylistTabControl);
            this.splitContainer1.Size = new System.Drawing.Size(655, 314);
            this.splitContainer1.SplitterDistance = 217;
            this.splitContainer1.TabIndex = 8;
            // 
            // mCollectionTabControl
            // 
            this.mCollectionTabControl.Controls.Add(this.mLibraryCollectionTabPage);
            this.mCollectionTabControl.Controls.Add(this.mFileSystemCollectionTabPage);
            this.mCollectionTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mCollectionTabControl.Location = new System.Drawing.Point(0, 0);
            this.mCollectionTabControl.Name = "mCollectionTabControl";
            this.mCollectionTabControl.SelectedIndex = 0;
            this.mCollectionTabControl.Size = new System.Drawing.Size(217, 314);
            this.mCollectionTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.mCollectionTabControl.TabIndex = 0;
            // 
            // mLibraryCollectionTabPage
            // 
            this.mLibraryCollectionTabPage.Location = new System.Drawing.Point(4, 22);
            this.mLibraryCollectionTabPage.Name = "mLibraryCollectionTabPage";
            this.mLibraryCollectionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mLibraryCollectionTabPage.Size = new System.Drawing.Size(209, 288);
            this.mLibraryCollectionTabPage.TabIndex = 0;
            this.mLibraryCollectionTabPage.UseVisualStyleBackColor = true;
            // 
            // mFileSystemCollectionTabPage
            // 
            this.mFileSystemCollectionTabPage.Location = new System.Drawing.Point(4, 22);
            this.mFileSystemCollectionTabPage.Name = "mFileSystemCollectionTabPage";
            this.mFileSystemCollectionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mFileSystemCollectionTabPage.Size = new System.Drawing.Size(209, 288);
            this.mFileSystemCollectionTabPage.TabIndex = 1;
            this.mFileSystemCollectionTabPage.UseVisualStyleBackColor = true;
            // 
            // mPlaylistTabControl
            // 
            this.mPlaylistTabControl.AllowDrop = true;
            this.mPlaylistTabControl.Controls.Add(this.tabPage1);
            this.mPlaylistTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPlaylistTabControl.Location = new System.Drawing.Point(0, 0);
            this.mPlaylistTabControl.Name = "mPlaylistTabControl";
            this.mPlaylistTabControl.SelectedIndex = 0;
            this.mPlaylistTabControl.Size = new System.Drawing.Size(434, 314);
            this.mPlaylistTabControl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(426, 288);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Queue";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SeekBarPanel
            // 
            this.SeekBarPanel.BackColor = System.Drawing.Color.Transparent;
            this.SeekBarPanel.Location = new System.Drawing.Point(271, 442);
            this.SeekBarPanel.Name = "SeekBarPanel";
            this.SeekBarPanel.Size = new System.Drawing.Size(282, 22);
            this.SeekBarPanel.TabIndex = 10;
            // 
            // seekBarRefreshTimer
            // 
            this.seekBarRefreshTimer.Interval = 200;
            this.seekBarRefreshTimer.Tick += new System.EventHandler(this.seekBarRefreshTimer_Tick);
            // 
            // VolumeControl
            // 
            this.VolumeControl.BackColor = System.Drawing.Color.Transparent;
            this.VolumeControl.Location = new System.Drawing.Point(566, 438);
            this.VolumeControl.Name = "VolumeControl";
            this.VolumeControl.Size = new System.Drawing.Size(101, 40);
            this.VolumeControl.TabIndex = 11;
            this.VolumeControl.Paint += new System.Windows.Forms.PaintEventHandler(this.VolumeControl_Paint);
            this.VolumeControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VolumeControl_MouseDown);
            this.VolumeControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VolumeControl_MouseMove);
            this.VolumeControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VolumeControl_MouseUp);
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 503);
            this.Controls.Add(this.VolumeControl);
            this.Controls.Add(this.SeekBarPanel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mAudioControlToolBar);
            this.Controls.Add(this.mStatusStrip);
            this.Controls.Add(this.mStandardToolBar);
            this.Controls.Add(this.mMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mMenuStrip;
            this.Name = "ProductForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ProductForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExitApplication);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExitApplication);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ProductForm_MouseUp);
            this.mMenuStrip.ResumeLayout(false);
            this.mMenuStrip.PerformLayout();
            this.mStandardToolBar.ResumeLayout(false);
            this.mStandardToolBar.PerformLayout();
            this.mStatusStrip.ResumeLayout(false);
            this.mStatusStrip.PerformLayout();
            this.mAudioControlToolBar.ResumeLayout(false);
            this.mAudioControlToolBar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.mCollectionTabControl.ResumeLayout(false);
            this.mPlaylistTabControl.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripSplitButton mOpenToolBarDropDown;
        private System.Windows.Forms.ToolStripMenuItem mOpenPlaylistToolBarButton;
        private System.Windows.Forms.ToolStripButton mExitToolBarButton;
        private System.Windows.Forms.ToolStripMenuItem mConfigureMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mTutorialsMenuDropDown;
        private System.Windows.Forms.ToolStripMenuItem mTutorialsWritingPluginsMenuItem;
        private System.Windows.Forms.ToolStripButton mSaveToolBarButton;
        private System.Windows.Forms.ToolStripMenuItem mEffectsMenu;
        private System.Windows.Forms.ToolStripButton mNewToolBarButton;
        private System.Windows.Forms.ToolStripButton mConfigureToolBarButton;
        private System.Windows.Forms.ToolStripMenuItem mNewPlaylistMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mSaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.StatusStrip mStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel mCurrentSelectedMediaLabel;
        private System.Windows.Forms.ToolStrip mAudioControlToolBar;
        private System.Windows.Forms.ToolStripButton mPlayToolBarButton;
        private System.Windows.Forms.ToolStripButton mStopToolBarButton;
        private System.Windows.Forms.ToolStripButton mPauseToolBarButton;
        private System.Windows.Forms.ToolStripButton mSkipBackwardToolBarButton;
        private System.Windows.Forms.ToolStripButton mSkipForwardToolBarButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl mCollectionTabControl;
        private System.Windows.Forms.TabPage mLibraryCollectionTabPage;
        private System.Windows.Forms.TabPage mFileSystemCollectionTabPage;
        private System.Windows.Forms.TabControl mPlaylistTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFileToPlaylistToolStripMenuItem;
        private System.Windows.Forms.Panel SeekBarPanel;
        private System.Windows.Forms.Timer seekBarRefreshTimer;
        private System.Windows.Forms.Panel VolumeControl;
    }
}