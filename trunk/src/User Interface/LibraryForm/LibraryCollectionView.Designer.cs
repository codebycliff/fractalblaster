namespace FractalBlaster.LibraryForm {
    partial class LibraryCollectionView {
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
            this.mMediaTreeView = new System.Windows.Forms.TreeView();
            this.ViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewPanel
            // 
            this.ViewPanel.Controls.Add(this.mMediaTreeView);
            this.ViewPanel.Size = new System.Drawing.Size(444, 114);
            // 
            // mMediaTreeView
            // 
            this.mMediaTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mMediaTreeView.Location = new System.Drawing.Point(0, 0);
            this.mMediaTreeView.Name = "mMediaTreeView";
            this.mMediaTreeView.Size = new System.Drawing.Size(444, 114);
            this.mMediaTreeView.TabIndex = 0;
            // 
            // LibraryCollectionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "LibraryCollectionView";
            this.Size = new System.Drawing.Size(450, 184);
            this.ViewPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView mMediaTreeView;
    }
}
