namespace FractalBlaster.Core.UI {
    partial class PlaylistControl {
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
            this.mPlaylistGridView = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mPlaylistGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mPlaylistGridView
            // 
            this.mPlaylistGridView.AllowUserToOrderColumns = true;
            this.mPlaylistGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mPlaylistGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Length});
            this.mPlaylistGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPlaylistGridView.Location = new System.Drawing.Point(0, 0);
            this.mPlaylistGridView.MultiSelect = false;
            this.mPlaylistGridView.Name = "mPlaylistGridView";
            this.mPlaylistGridView.ReadOnly = true;
            this.mPlaylistGridView.RowHeadersVisible = false;
            this.mPlaylistGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mPlaylistGridView.Size = new System.Drawing.Size(344, 269);
            this.mPlaylistGridView.TabIndex = 0;
            this.mPlaylistGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mPlaylistGridView_CellMouseDoubleClick);
            this.mPlaylistGridView.SelectionChanged += new System.EventHandler(this.mPlaylistGridView_SelectionChanged);
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // Length
            // 
            this.Length.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Length.HeaderText = "Length";
            this.Length.MaxInputLength = 6;
            this.Length.Name = "Length";
            this.Length.ReadOnly = true;
            this.Length.Width = 65;
            // 
            // PlaylistControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mPlaylistGridView);
            this.Name = "PlaylistControl";
            this.Size = new System.Drawing.Size(344, 269);
            ((System.ComponentModel.ISupportInitialize)(this.mPlaylistGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mPlaylistGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
    }
}
