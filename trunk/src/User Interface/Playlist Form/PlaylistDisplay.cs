using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.PlaylistForm
{
    public partial class PlaylistDisplay : UserControl
    {

        DataTable dataSource;
        PlaylistForm parent;

        public PlaylistDisplay()
        {
            InitializeComponent();
            dataSource = new DataTable();

            dataSource.Columns.Add(new DataColumn("Artist"));
            dataSource.Columns.Add(new DataColumn("Title"));

            dataGridView1.DataSource = dataSource;
            dataGridView1.Columns["Artist"].Width = 200;
            dataGridView1.Columns["Title"].Width = 210;
            dataGridView1.Refresh();
        }

        public PlaylistForm ParentForm
        {
            set { parent = value; }
        }

        public void clearRows()
        {
            dataSource.Rows.Clear();
        }

        public void addRow(string artist, string title)
        {
            DataRow newRow = dataSource.NewRow();
            newRow["Artist"] = artist;
            newRow["Title"] = title;
            dataSource.Rows.Add(newRow);
            dataGridView1.Refresh();
        }

        public void selectIndex(int i)
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Selected = false;
            }
            if ((i < 0) || (i >= dataGridView1.Rows.Count))
            {
                return;
            }
            dataGridView1.Rows[i].Selected = true;
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i != e.RowIndex)
                {
                    foreach (DataGridViewCell d in dataGridView1.Rows[i].Cells)
                        d.Style.BackColor = Color.White;
                }
                else
                {
                    foreach (DataGridViewCell d in dataGridView1.Rows[i].Cells)
                    {
                        d.Style.BackColor = Color.Blue;
                    }
                }
            }
            parent.selectIndex(e.RowIndex);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewCell d in dataGridView1.CurrentRow.Cells)
            {
                d.Selected = true;
            }
        }

    }
}
