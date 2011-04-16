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

namespace FractalBlaster.Core.UI
{

    public partial class FileSystemCollectionView : CollectionView
    {

        public const Int32 COMPUTER_ICON_INDEX = 0;
        public const Int32 FOLDER_ICON_INDEX = 1;
        public const Int32 OPEN_FOLDER_ICON_INDEX = 2;
        public const Int32 FILE_ICON_INDEX = 3;

        public override string Label { get { return "Browse"; } }

        public override bool HasConfiguration { get { return true; } }

        public override bool HasCustomToolStrip { get { return true; } }

        public override Form ConfigurationDialog { get { return new FileSystemConfigurationDialog(); } }

        public FileSystemCollectionView()
            : base()
        {
            InitializeComponent();
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


        void mTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (mTreeView.SelectedNode == null)
                return;

            TreeNode m = mTreeView.SelectedNode;
            if (m.Nodes.Count == 0)
            {
                string filename = m.Tag as string;
                if (filename != null)
                {
                    MediaFile media = new MediaFile(filename);
                    FamilyKernel.Instance.Context.Engine.CurrentPlaylist.AddItem(media);
                }
            }
        }

        void mTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (mTreeView.SelectedNode == null)
                    return;

                TreeNode m = mTreeView.SelectedNode;

                string filename = m.Tag as string;
                if (filename != null)
                {
                    MediaFile media = new MediaFile(filename);
                    FamilyKernel.Instance.Context.Engine.CurrentPlaylist.AddItem(media);
                }
                else
                {

                    RefreshNode(m);

                    foreach (TreeNode n in m.Nodes)
                    {
                        filename = n.Tag as string;
                        if (filename != null)
                        {
                            MediaFile media = new MediaFile(filename);
                            FamilyKernel.Instance.Context.Engine.CurrentPlaylist.AddItem(media);
                        }
                    }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        public override void RefreshItems(object sender, EventArgs args)
        {
            base.RefreshItems(sender, args);
        }

        public override void RefreshView(object sender, EventArgs args)
        {
            base.RefreshView(sender, args);
        }

        private void RefreshNode(TreeNode node)
        {
            node.Nodes.Clear();
            string[] formats = Config.getProperty("fileformats").Split(';');
            bool search_directory = false;
            try
            {
                DirectoryInfo rootdir = new DirectoryInfo(node.FullPath);
                foreach (DirectoryInfo dir in rootdir.GetDirectories())
                {
                    if ((dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        try
                        {

                            foreach (string type in formats)
                            {
                                if (dir.GetFiles(type).Length > 0)
                                    search_directory = true;
                            }
                            if (dir.GetDirectories().Length > 0 || search_directory)
                            {
                                TreeNode dirnode = new TreeNode(dir.Name, FOLDER_ICON_INDEX, FOLDER_ICON_INDEX);
                                node.Nodes.Add(dirnode);
                                dirnode.Nodes.Add("*");
                            }
                        }
                        catch (Exception ie)
                        {
                            continue;
                        }
                    }
                }

                List<FileInfo> files = new List<FileInfo>();
                foreach (string type in formats)
                {
                    files.AddRange(rootdir.GetFiles(type));
                }

                //node.Nodes.Clear();
                foreach (FileInfo file in files)
                {
                    TreeNode fileNode = new TreeNode(file.Name, FILE_ICON_INDEX, FILE_ICON_INDEX);
                    fileNode.Tag = file.FullName;
                    node.Nodes.Add(fileNode);
                }
            }
            catch (Exception e)
            {

            }
        }

        private void mTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                RefreshNode(e.Node);
            }
        }

    }

}
