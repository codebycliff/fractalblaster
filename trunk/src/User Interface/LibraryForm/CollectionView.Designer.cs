namespace FractalBlaster.LibraryForm {
    partial class CollectionView {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mCustomToolBar = new System.Windows.Forms.ToolStrip();
            this.ViewPanel = new System.Windows.Forms.Panel();
            this.mRefreshButton = new System.Windows.Forms.ToolStripButton();
            this.mTreeViewButton = new System.Windows.Forms.ToolStripButton();
            this.mIconViewButton = new System.Windows.Forms.ToolStripButton();
            this.mListViewButton = new System.Windows.Forms.ToolStripButton();
            this.mConfigureButton = new System.Windows.Forms.ToolStripButton();
            this.mMainToolStrip = new System.Windows.Forms.ToolStrip();
            this.mMainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mCustomToolBar
            // 
            this.mCustomToolBar.BackColor = System.Drawing.SystemColors.Control;
            this.mCustomToolBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mCustomToolBar.Location = new System.Drawing.Point(0, 159);
            this.mCustomToolBar.Margin = new System.Windows.Forms.Padding(3);
            this.mCustomToolBar.Name = "mCustomToolBar";
            this.mCustomToolBar.Size = new System.Drawing.Size(450, 25);
            this.mCustomToolBar.TabIndex = 5;
            this.mCustomToolBar.Text = "mCustomToolBar";
            // 
            // ViewPanel
            // 
            this.ViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ViewPanel.Location = new System.Drawing.Point(3, 42);
            this.ViewPanel.Name = "ViewPanel";
            this.ViewPanel.Size = new System.Drawing.Size(444, 114);
            this.ViewPanel.TabIndex = 6;
            // 
            // mRefreshButton
            // 
            this.mRefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mRefreshButton.Image = global::FractalBlaster.LibraryForm.Resources.view_refresh_32x32;
            this.mRefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mRefreshButton.Name = "mRefreshButton";
            this.mRefreshButton.Size = new System.Drawing.Size(36, 36);
            this.mRefreshButton.Text = "Refresh";
            // 
            // mTreeViewButton
            // 
            this.mTreeViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mTreeViewButton.Image = global::FractalBlaster.LibraryForm.Resources.view_list_tree;
            this.mTreeViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mTreeViewButton.Name = "mTreeViewButton";
            this.mTreeViewButton.Size = new System.Drawing.Size(36, 36);
            this.mTreeViewButton.Text = "Tree View";
            // 
            // mIconViewButton
            // 
            this.mIconViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mIconViewButton.Image = global::FractalBlaster.LibraryForm.Resources.view_list_icons_32x32;
            this.mIconViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mIconViewButton.Name = "mIconViewButton";
            this.mIconViewButton.Size = new System.Drawing.Size(36, 36);
            this.mIconViewButton.Text = "Icon View";
            // 
            // mListViewButton
            // 
            this.mListViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mListViewButton.Image = global::FractalBlaster.LibraryForm.Resources.view_list_text_32x32;
            this.mListViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mListViewButton.Name = "mListViewButton";
            this.mListViewButton.Size = new System.Drawing.Size(36, 36);
            this.mListViewButton.Text = "List View";
            // 
            // mConfigureButton
            // 
            this.mConfigureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mConfigureButton.Image = global::FractalBlaster.LibraryForm.Resources.configure;
            this.mConfigureButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mConfigureButton.Name = "mConfigureButton";
            this.mConfigureButton.Size = new System.Drawing.Size(36, 36);
            this.mConfigureButton.Text = "Configure";
            // 
            // mMainToolStrip
            // 
            this.mMainToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.mMainToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mMainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mRefreshButton,
            this.mTreeViewButton,
            this.mIconViewButton,
            this.mListViewButton,
            this.mConfigureButton});
            this.mMainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mMainToolStrip.Name = "mMainToolStrip";
            this.mMainToolStrip.Size = new System.Drawing.Size(450, 39);
            this.mMainToolStrip.TabIndex = 4;
            this.mMainToolStrip.Text = "General";
            // 
            // CollectionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mCustomToolBar);
            this.Controls.Add(this.mMainToolStrip);
            this.Controls.Add(this.ViewPanel);
            this.Name = "CollectionView";
            this.Size = new System.Drawing.Size(450, 184);
            this.mMainToolStrip.ResumeLayout(false);
            this.mMainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mCustomToolBar;
        private System.Windows.Forms.ToolStripButton mRefreshButton;
        private System.Windows.Forms.ToolStripButton mTreeViewButton;
        private System.Windows.Forms.ToolStripButton mIconViewButton;
        private System.Windows.Forms.ToolStripButton mListViewButton;
        private System.Windows.Forms.ToolStripButton mConfigureButton;
        private System.Windows.Forms.ToolStrip mMainToolStrip;
        protected System.Windows.Forms.Panel ViewPanel;


    }
}
