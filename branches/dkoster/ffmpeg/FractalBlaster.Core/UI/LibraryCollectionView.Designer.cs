namespace FractalBlaster.Core.UI
{
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
             this.label1 = new System.Windows.Forms.Label();
             this.ViewPanel.SuspendLayout();
             this.SuspendLayout();
             // 
             // ViewPanel
             // 
             this.ViewPanel.Controls.Add(this.mMediaTreeView);
             // 
             // mMediaTreeView
             // 
             this.mMediaTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
             this.mMediaTreeView.Location = new System.Drawing.Point(0, 0);
             this.mMediaTreeView.Name = "mMediaTreeView";
             this.mMediaTreeView.Size = new System.Drawing.Size(194, 114);
             this.mMediaTreeView.TabIndex = 0;
             this.mMediaTreeView.Click += new System.EventHandler(this.mMediaTreeView_Click);
             this.mMediaTreeView.DoubleClick += new System.EventHandler(this.mMediaTreeView_DoubleClick);
             // 
             // label1
             // 
             this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
             this.label1.AutoSize = true;
             this.label1.Location = new System.Drawing.Point(3, 171);
             this.label1.Name = "label1";
             this.label1.Size = new System.Drawing.Size(0, 13);
             this.label1.TabIndex = 7;
             // 
             // LibraryCollectionView
             // 
             this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
             this.Controls.Add(this.label1);
             this.Name = "LibraryCollectionView";
             this.Controls.SetChildIndex(this.label1, 0);
             this.Controls.SetChildIndex(this.ViewPanel, 0);
             this.ViewPanel.ResumeLayout(false);
             this.ResumeLayout(false);
             this.PerformLayout();

         }

         #endregion

         private System.Windows.Forms.TreeView mMediaTreeView;
         private System.Windows.Forms.Label label1;
     }
 }