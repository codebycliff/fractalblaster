using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using FractalBlaster.Universe;
using FractalBlaster.Core;

namespace FractalBlaster.Core.UI
{
    public partial class LibraryConfigurationDialog : FractalBlaster.Core.UI.ConfigurationDialog
    {

        Library library;
        public LibraryConfigurationDialog(Library library)
        {
            InitializeComponent();
            this.library = library;

            this.mAddDirectoryButton.Click += new EventHandler(mAddDirectoryButton_Click);
            this.mSaveButton.Click += new EventHandler(mSaveButton_Click);

            listBox1.KeyDown += new KeyEventHandler(listBox1_KeyDown);

            foreach (DirectoryInfo dir in library.Root)
            {
                listBox1.Items.Add(dir.FullName);
            }
        }

        void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                List<Object> toRemove = new List<Object>();
                foreach (Object item in listBox1.SelectedItems)
                {
                    toRemove.Add(item);
                }
                foreach (Object item in toRemove)
                {
                    listBox1.Items.Remove(item);
                }
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        void mAddDirectoryButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(dialog.SelectedPath);
            }
        }

        void mSaveButton_Click(object sender, EventArgs e)
        {
            library.Root.Clear();
            foreach (Object dir in listBox1.Items)
            {
                try
                {
                    library.Root.Add(new DirectoryInfo(dir.ToString()));
                }
                catch (Exception ex)
                {
                }
            }
            this.Close();
        }
    }
}
