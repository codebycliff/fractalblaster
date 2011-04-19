using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using FractalBlaster.Core.Runtime;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.UI {

    /// <remarks>
    /// Class the implements the <see cref="CollectionView"/> interface to
    /// provide a view of the file system.
    /// </remarks>
    public partial class FileSystemCollectionView : CollectionView {
         
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemCollectionView"/> class.
        /// </summary>
        public FileSystemCollectionView() : base() {
            InitializeComponent();
            this.mConfigureButton.Visible = false;
            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.computer);
            imageList.Images.Add(Properties.Resources.folder);
            imageList.Images.Add(Properties.Resources.document_open_folder);
            imageList.Images.Add(Properties.Resources.text_xmcd);
            mTreeView.ImageList = imageList;

            TreeNode rootNode = new TreeNode(@"C:\", COMPUTER_ICON_INDEX, COMPUTER_ICON_INDEX);
            mTreeView.Nodes.Add(rootNode);
            RefreshNode(rootNode);
            mTreeView.Nodes[0].Expand();

            mTreeView.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(mTreeView_NodeMouseDoubleClick);
            mTreeView.KeyDown += new KeyEventHandler(mTreeView_KeyDown);
        }

        #region [ Constants ]

        /// <summary>
        /// Constant representing the index for the computer icon.
        /// </summary>
        public const Int32 COMPUTER_ICON_INDEX = 0;

        /// <summary>
        /// Constant representing the index of the folder icon.
        /// </summary>
        public const Int32 FOLDER_ICON_INDEX = 1;

        /// <summary>
        /// Constant representing the index of the open folder icon.
        /// </summary>
        public const Int32 OPEN_FOLDER_ICON_INDEX = 2;

        /// <summary>
        /// Constant representing the index of the file icon.
        /// </summary>
        public const Int32 FILE_ICON_INDEX = 3;

        #endregion
        
        #region [ CollectionView Overrides ]

        /// <summary>
        /// Gets the label for the collection view.
        /// </summary>
        public override string Label { get { return "Browse"; } }

        /// <summary>
        /// Gets a value indicating whether this collection view has configuration.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has configuration; otherwise, <c>false</c>.
        /// </value>
        public override bool HasConfiguration { get { return true; } }

        /// <summary>
        /// Gets a value indicating whether this collection view has custom tool strip.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has custom tool strip; otherwise, <c>false</c>.
        /// </value>
        public override bool HasCustomToolStrip { get { return true; } }

        /// <summary>
        /// Gets the configuration dialog for this collection view.
        /// </summary>
        public override Form ConfigurationDialog { get { return new FileSystemConfigurationDialog(); } }

        /// <summary>
        /// Event handler that refreshes the items in the collection view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public override void RefreshItems(object sender, EventArgs args) {
            base.RefreshItems(sender, args);
        }

        /// <summary>
        /// Event handler that refreshes the actual collection view.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public override void RefreshView(object sender, EventArgs args) {
            base.RefreshView(sender, args);
        }

        #endregion
       
        #region [ Private ]

        /// <summary>
        /// Handles the NodeMouseDoubleClick event of the mTreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeNodeMouseClickEventArgs"/> instance containing the event data.</param>
        private void mTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (mTreeView.SelectedNode == null)
                return;

            TreeNode m = mTreeView.SelectedNode;
            if (m.Nodes.Count == 0) {
                string filename = m.Tag as string;
                if (filename != null) {
                    MediaFile media = new MediaFile(filename);
                    FamilyKernel.Instance.Context.Engine.CurrentPlaylist.AddItem(media);
                }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the mTreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void mTreeView_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (mTreeView.SelectedNode == null)
                    return;

                TreeNode m = mTreeView.SelectedNode;

                string filename = m.Tag as string;
                if (filename != null) {
                    MediaFile media = new MediaFile(filename);
                    FamilyKernel.Instance.Context.Engine.CurrentPlaylist.AddItem(media);
                }
                else {

                    RefreshNode(m);

                    foreach (TreeNode n in m.Nodes) {
                        filename = n.Tag as string;
                        if (filename != null) {
                            MediaFile media = new MediaFile(filename);
                            FamilyKernel.Instance.Context.Engine.CurrentPlaylist.AddItem(media);
                        }
                    }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Refreshes the specified tree node.
        /// </summary>
        /// <param name="node">The node to be refreshed.</param>
        private void RefreshNode(TreeNode node) {
            node.Nodes.Clear();
            string[] formats = Config.GetProperty("fileformats").Split(';');
            bool search_directory = false;
            try {
                DirectoryInfo rootdir = new DirectoryInfo(node.FullPath);
                foreach (DirectoryInfo dir in rootdir.GetDirectories()) {
                    if ((dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) {
                        try {

                            foreach (string type in formats) {
                                if (dir.GetFiles(type).Length > 0)
                                    search_directory = true;
                            }
                            if (dir.GetDirectories().Length > 0 || search_directory) {
                                TreeNode dirnode = new TreeNode(dir.Name, FOLDER_ICON_INDEX, FOLDER_ICON_INDEX);
                                node.Nodes.Add(dirnode);
                                dirnode.Nodes.Add("*");
                            }
                        }
                        catch (Exception ie) {
                            continue;
                        }
                    }
                }

                List<FileInfo> files = new List<FileInfo>();
                foreach (string type in formats) {
                    files.AddRange(rootdir.GetFiles(type));
                }

                //node.Nodes.Clear();
                foreach (FileInfo file in files) {
                    TreeNode fileNode = new TreeNode(file.Name, FILE_ICON_INDEX, FILE_ICON_INDEX);
                    fileNode.Tag = file.FullName;
                    node.Nodes.Add(fileNode);
                }
            }
            catch (Exception e) {

            }
        }

        /// <summary>
        /// Handles the BeforeExpand event of the mTreeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void mTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            if (e.Node.Nodes[0].Text == "*") {
                e.Node.Nodes.Clear();
                RefreshNode(e.Node);
            }
        }
        
        #endregion
    
    }

}
