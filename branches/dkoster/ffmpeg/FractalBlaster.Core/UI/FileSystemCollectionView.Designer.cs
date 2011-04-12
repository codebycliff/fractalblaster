namespace FractalBlaster.Core.UI {
    partial class FileSystemCollectionView {
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
            this.mTreeView = new System.Windows.Forms.TreeView();
            this.ViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewPanel
            // 
            this.ViewPanel.Controls.Add(this.mTreeView);
            // 
            // mTreeView
            // 
            this.mTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTreeView.Location = new System.Drawing.Point(0, 0);
            this.mTreeView.Name = "mTreeView";
            this.mTreeView.Size = new System.Drawing.Size(194, 114);
            this.mTreeView.TabIndex = 0;
            this.mTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.mTreeView_BeforeExpand);
            // 
            // FileSystemCollectionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "FileSystemCollectionView";
            this.ViewPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView mTreeView;
    }
}
