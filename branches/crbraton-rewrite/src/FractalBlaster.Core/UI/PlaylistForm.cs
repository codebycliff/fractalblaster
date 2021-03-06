﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class PlaylistForm : Form
    {
        private List<String> filenames;
        private List<String> displaynames;
        private Point mouse_offset;
        private string selected;

        public PlaylistForm()
        {
            filenames = new List<String>();
            displaynames = new List<String>();
            InitializeComponent();
        }

        public void addFile(string filename, string displayname)
        {
            filenames.Add(filename);
            displaynames.Add(displayname);
            updateList();
        }

        private void updateList()
        {
            listBox1.BeginUpdate();
            listBox1.Enabled = false;
            listBox1.DataSource = null;
            listBox1.DataSource = displaynames;
            listBox1.SelectedIndex = filenames.Count - 1;
            listBox1.Enabled = true;
            listBox1.EndUpdate();
        }

        private void PlaylistForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void PlaylistForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset);
                this.Location = mousePos;
            }
        }

        public String getFilename()
        {
            return filenames.ElementAt(listBox1.SelectedIndex);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            selected = listBox1.SelectedItem.ToString();
            //((UI)this.Owner).PlaylistDoubleClicked();
        }

    }
}
