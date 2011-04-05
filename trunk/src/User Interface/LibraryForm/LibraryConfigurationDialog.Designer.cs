namespace FractalBlaster.LibraryForm {
    partial class LibraryConfigurationDialog {
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
            this.mControlTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mLibraryLocationLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.mBrowseLocationButton = new System.Windows.Forms.Button();
            this.ControlPanel.SuspendLayout();
            this.mControlTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.mControlTableLayout);
            this.ControlPanel.Size = new System.Drawing.Size(362, 242);
            // 
            // mControlTableLayout
            // 
            this.mControlTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mControlTableLayout.ColumnCount = 3;
            this.mControlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.mControlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.77778F));
            this.mControlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.mControlTableLayout.Controls.Add(this.mLibraryLocationLabel, 0, 0);
            this.mControlTableLayout.Controls.Add(this.textBox1, 1, 0);
            this.mControlTableLayout.Controls.Add(this.mBrowseLocationButton, 2, 0);
            this.mControlTableLayout.Location = new System.Drawing.Point(8, 8);
            this.mControlTableLayout.Name = "mControlTableLayout";
            this.mControlTableLayout.RowCount = 1;
            this.mControlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.49587F));
            this.mControlTableLayout.Size = new System.Drawing.Size(351, 28);
            this.mControlTableLayout.TabIndex = 1;
            // 
            // mLibraryLocationLabel
            // 
            this.mLibraryLocationLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.mLibraryLocationLabel.AutoSize = true;
            this.mLibraryLocationLabel.Location = new System.Drawing.Point(3, 7);
            this.mLibraryLocationLabel.Name = "mLibraryLocationLabel";
            this.mLibraryLocationLabel.Size = new System.Drawing.Size(51, 13);
            this.mLibraryLocationLabel.TabIndex = 0;
            this.mLibraryLocationLabel.Text = "Location:";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(64, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 20);
            this.textBox1.TabIndex = 1;
            // 
            // mBrowseLocationButton
            // 
            this.mBrowseLocationButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mBrowseLocationButton.Location = new System.Drawing.Point(279, 3);
            this.mBrowseLocationButton.Name = "mBrowseLocationButton";
            this.mBrowseLocationButton.Size = new System.Drawing.Size(69, 22);
            this.mBrowseLocationButton.TabIndex = 2;
            this.mBrowseLocationButton.Text = "Browse";
            this.mBrowseLocationButton.UseVisualStyleBackColor = true;
            // 
            // LibraryConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(362, 84);
            this.Name = "LibraryConfigurationDialog";
            this.Text = "Settings | Library Collection";
            this.ControlPanel.ResumeLayout(false);
            this.mControlTableLayout.ResumeLayout(false);
            this.mControlTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mControlTableLayout;
        private System.Windows.Forms.Label mLibraryLocationLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button mBrowseLocationButton;

    }
}
