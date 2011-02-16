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
            this.mFileMenuExitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mEditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mEditMenuOptionsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mHelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mMenuStrip
            // 
            this.mMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFileMenu,
            this.mEditMenu,
            this.mViewMenu,
            this.mHelpMenu});
            this.mMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mMenuStrip.Name = "mMenuStrip";
            this.mMenuStrip.Size = new System.Drawing.Size(784, 24);
            this.mMenuStrip.TabIndex = 0;
            this.mMenuStrip.Text = "menuStrip1";
            // 
            // mFileMenu
            // 
            this.mFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFileMenuExitItem});
            this.mFileMenu.Name = "mFileMenu";
            this.mFileMenu.Size = new System.Drawing.Size(37, 20);
            this.mFileMenu.Text = "File";
            // 
            // mFileMenuExitItem
            // 
            this.mFileMenuExitItem.Name = "mFileMenuExitItem";
            this.mFileMenuExitItem.Size = new System.Drawing.Size(92, 22);
            this.mFileMenuExitItem.Text = "Exit";
            // 
            // mEditMenu
            // 
            this.mEditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mEditMenuOptionsItem});
            this.mEditMenu.Name = "mEditMenu";
            this.mEditMenu.Size = new System.Drawing.Size(39, 20);
            this.mEditMenu.Text = "Edit";
            // 
            // mEditMenuOptionsItem
            // 
            this.mEditMenuOptionsItem.Name = "mEditMenuOptionsItem";
            this.mEditMenuOptionsItem.Size = new System.Drawing.Size(116, 22);
            this.mEditMenuOptionsItem.Text = "Options";
            // 
            // mViewMenu
            // 
            this.mViewMenu.Name = "mViewMenu";
            this.mViewMenu.Size = new System.Drawing.Size(44, 20);
            this.mViewMenu.Text = "View";
            // 
            // mHelpMenu
            // 
            this.mHelpMenu.Name = "mHelpMenu";
            this.mHelpMenu.Size = new System.Drawing.Size(44, 20);
            this.mHelpMenu.Text = "Help";
            // 
            // mStatusStrip
            // 
            this.mStatusStrip.Location = new System.Drawing.Point(0, 581);
            this.mStatusStrip.Name = "mStatusStrip";
            this.mStatusStrip.Size = new System.Drawing.Size(784, 22);
            this.mStatusStrip.TabIndex = 1;
            this.mStatusStrip.Text = "statusStrip1";
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 603);
            this.Controls.Add(this.mStatusStrip);
            this.Controls.Add(this.mMenuStrip);
            this.MainMenuStrip = this.mMenuStrip;
            this.Name = "ProductForm";
            this.Text = "ProductForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductForm_FormClosing);
            this.mMenuStrip.ResumeLayout(false);
            this.mMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mFileMenu;
        private System.Windows.Forms.ToolStripMenuItem mFileMenuExitItem;
        private System.Windows.Forms.ToolStripMenuItem mEditMenu;
        private System.Windows.Forms.ToolStripMenuItem mEditMenuOptionsItem;
        private System.Windows.Forms.ToolStripMenuItem mViewMenu;
        private System.Windows.Forms.ToolStripMenuItem mHelpMenu;
        private System.Windows.Forms.StatusStrip mStatusStrip;
    }
}