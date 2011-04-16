namespace FractalBlaster.Core.UI {
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
            this.ViewPanel = new System.Windows.Forms.Panel();
            this.mRefreshButton = new System.Windows.Forms.ToolStripButton();
            this.mMainToolStrip = new System.Windows.Forms.ToolStrip();
            this.mConfigureButton = new System.Windows.Forms.ToolStripButton();
            this.mMainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewPanel
            // 
            this.ViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ViewPanel.Location = new System.Drawing.Point(3, 42);
            this.ViewPanel.Name = "ViewPanel";
            this.ViewPanel.Size = new System.Drawing.Size(194, 114);
            this.ViewPanel.TabIndex = 6;
            // 
            // mRefreshButton
            // 
            this.mRefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mRefreshButton.Image = global::FractalBlaster.Core.Properties.Resources.view_refresh_32x32;
            this.mRefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mRefreshButton.Name = "mRefreshButton";
            this.mRefreshButton.Size = new System.Drawing.Size(36, 36);
            this.mRefreshButton.Text = "Refresh";
            // 
            // mMainToolStrip
            // 
            this.mMainToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.mMainToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mMainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mRefreshButton,
            this.mConfigureButton});
            this.mMainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mMainToolStrip.Name = "mMainToolStrip";
            this.mMainToolStrip.Size = new System.Drawing.Size(200, 39);
            this.mMainToolStrip.TabIndex = 4;
            this.mMainToolStrip.Text = "General";
            // 
            // mConfigureButton
            // 
            this.mConfigureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mConfigureButton.Image = global::FractalBlaster.Core.Properties.Resources.configure;
            this.mConfigureButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mConfigureButton.Name = "mConfigureButton";
            this.mConfigureButton.Size = new System.Drawing.Size(36, 36);
            this.mConfigureButton.Text = "Configure";
            // 
            // CollectionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mMainToolStrip);
            this.Controls.Add(this.ViewPanel);
            this.Name = "CollectionView";
            this.Size = new System.Drawing.Size(200, 184);
            this.mMainToolStrip.ResumeLayout(false);
            this.mMainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolStripButton mRefreshButton;
        private System.Windows.Forms.ToolStrip mMainToolStrip;
        protected System.Windows.Forms.Panel ViewPanel;
        protected System.Windows.Forms.ToolStripButton mConfigureButton;


    }
}
