using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.UI
{
    public partial class PlaylistForm : Form
    {
        private IEngine Engine { get; set; }
        private List<String> filenames;
        private List<String> displaynames;
        private Point mouse_offset;
        private string selected;
        private int length;
        private bool repeat;

        public PlaylistForm(Playlist playlist) {
            filenames = new List<String>();
            displaynames = new List<String>();
            length = 0;
            InitializeComponent();
        }

        public Playlist Playlist { get; private set; }
        public void addFile(string filename, string displayname)
        {
            filenames.Add(filename);
            displaynames.Add(displayname);
            length = length + 1;
            updateList();
        }

        public String getFilename()
        {
            return filenames.ElementAt(listBox1.SelectedIndex);
        }

        public void gotoIndex(int i)
        {
            if ((i < 0) || (i >= this.length)) return;
            listBox1.SelectedIndex = i;
        }

        public void gotoNext()
        {
            if (length == 0) return;
            if (listBox1.SelectedIndex == length - 1)
            {
                if (repeat)
                {
                    listBox1.SelectedIndex = 1;
                }
            }
            else
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;                
            }
        }

        public void gotoPrevious()
        {
            if (length == 0) return;
            if (listBox1.SelectedIndex == 0)
            {
                if (repeat)
                {
                    listBox1.SelectedIndex = length - 1;
                }
            }
            else
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
            }
        }

        private void updateList()
        {
            listBox1.BeginUpdate();
            listBox1.Enabled = false;
            listBox1.DataSource = null;
            listBox1.DataSource = displaynames;
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

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            selected = listBox1.SelectedItem.ToString();
            //MediaFile selectedFile = Engine.CurrentPlaylist.Where(f => f.Info.FullName.CompareTo(selected) == 0).First();
            //Engine.Load(selectedFile);
        }

    }
}
