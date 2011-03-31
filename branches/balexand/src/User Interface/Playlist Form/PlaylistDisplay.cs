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

        public PlaylistDisplay()
        {
            InitializeComponent();
            dataSource = new DataTable();

            dataSource.Columns.Add(new DataColumn("Artist"));
            dataSource.Columns.Add(new DataColumn("Title"));

            dataGridView1.DataSource = dataSource;
            dataGridView1.Columns["Artist"].Width = 200;
            dataGridView1.Columns["Title"].Width = 200;
            dataGridView1.Refresh();
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


    }
}
