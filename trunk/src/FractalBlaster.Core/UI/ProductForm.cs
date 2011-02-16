using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.UI {
    public partial class ProductForm : Form {
        public ProductForm() {
            InitializeComponent();
            Views = new List<Form>();
        }

        public void AddViewPlugin(IViewPlugin view) {
            
            ToolStripMenuItem item = new ToolStripMenuItem(view.GetInfo().Name);
            item.CheckOnClick = true;
            Form form = view.UserInterface;
            Views.Add(form);
            item.CheckedChanged += (o, e) => {
                ToolStripMenuItem viewItem = o as ToolStripMenuItem;
                if(viewItem.Checked) {
                    form.Show();
                }
                else {
                    form.Hide();
                }
            };
            mViewMenu.DropDownItems.Add(item);
        }

        private void ProductForm_FormClosing(object sender, FormClosingEventArgs e) {
            foreach (Form f in Views) {
                f.Close();
            }
        }

        private List<Form> Views { get; set; }
    }
}
