﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;

namespace FractalBlaster.Core.UI {

    public partial class FileSystemCollectionView : CollectionView {

        public const Int32 COMPUTER_ICON_INDEX = 0;
        public const Int32 FOLDER_ICON_INDEX = 1;
        public const Int32 OPEN_FOLDER_ICON_INDEX = 2;
        public const Int32 FILE_ICON_INDEX = 3;

        public override string Label { get { return "Browse"; } }
        
        public override bool HasConfiguration { get { return true; } }
        
        public override bool HasCustomToolStrip { get { return true; } }
        
        public override Form ConfigurationDialog { get { return new FileSystemConfigurationDialog(); } }

        public FileSystemCollectionView() : base()  {
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
        }

        public override void RefreshItems(object sender, EventArgs args) {
            base.RefreshItems(sender, args);
        }

        public override void RefreshView(object sender, EventArgs args) {
            base.RefreshView(sender, args);
            switch (ViewMode) {
            case ViewMode.Tree:
                break;
            case ViewMode.Icon:
                break;
            case ViewMode.List:
                break;
            }
        }

        private void RefreshNode(TreeNode node) {
            try {
                DirectoryInfo rootdir = new DirectoryInfo(node.FullPath);
                foreach (DirectoryInfo dir in rootdir.GetDirectories()) {
                    try {
                        if (dir.GetDirectories().Length > 0 || dir.GetFiles("*.mp3").Length > 0) {
                            TreeNode dirnode = new TreeNode(dir.Name, FOLDER_ICON_INDEX, FOLDER_ICON_INDEX);
                            node.Nodes.Add(dirnode);
                            dirnode.Nodes.Add("*");
                        }
                    }
                    catch (Exception ie) {
                        continue;
                    }
                }
                foreach (FileInfo file in rootdir.GetFiles("*.mp3")) {
                    
                    TreeNode fileNode = new TreeNode(file.Name, FILE_ICON_INDEX, FILE_ICON_INDEX);
                    node.Nodes.Add(fileNode);
                }
            }
            catch(Exception e) {
                
            }
        }

        private void mTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            if (e.Node.Nodes[0].Text == "*") {
                e.Node.Nodes.Clear();
                RefreshNode(e.Node);
            }
        }

    }

}