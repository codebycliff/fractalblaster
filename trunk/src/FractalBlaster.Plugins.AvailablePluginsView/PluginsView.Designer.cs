namespace FractalBlaster.Plugins.AvailablePluginsView {
    partial class PluginsView {
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Output");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Input");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Effect");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("View");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Metadata");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Playlist");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("All Plugins", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.mPluginTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // mPluginTree
            // 
            this.mPluginTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPluginTree.Location = new System.Drawing.Point(0, 0);
            this.mPluginTree.Name = "mPluginTree";
            treeNode1.Name = "Output";
            treeNode1.Text = "Output";
            treeNode2.Name = "Input";
            treeNode2.Text = "Input";
            treeNode3.Name = "Effect";
            treeNode3.Text = "Effect";
            treeNode4.Name = "View";
            treeNode4.Text = "View";
            treeNode5.Name = "Metadata";
            treeNode5.Text = "Metadata";
            treeNode6.Name = "Playlist";
            treeNode6.Text = "Playlist";
            treeNode7.Name = "All Plugins";
            treeNode7.Text = "All Plugins";
            this.mPluginTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.mPluginTree.Size = new System.Drawing.Size(257, 397);
            this.mPluginTree.TabIndex = 0;
            // 
            // PluginsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 397);
            this.Controls.Add(this.mPluginTree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PluginsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Available Plugins";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView mPluginTree;
    }
}