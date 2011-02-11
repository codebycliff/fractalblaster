using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Core;
using FractalBlaster.Engines;
using FractalBlaster.Products;

namespace FractalBlaster {

    public partial class ProductSelectionForm : Form {

        public enum ProductName {
            Express,
            Standard,
            Enterprise,
            None
        }

        public ProductSelectionForm() {
            InitializeComponent();
            ChosenProductName = ProductName.None;
        }

        public ProductName ChosenProductName { get; private set; }

        public void RunChosenProduct() {
            IProductModel product = null;
            
            switch (ChosenProductName) {
            case ProductName.Express:
                IEngine engine = null;
                
                String engineName = mExpressEngineComboBox.SelectedText;
                engineName = engineName.Replace("Engine", "").Trim();
                if (engineName.Equals("Video")) {
                    engine = VideoEngine.Instance;
                }
                else if (engineName.Equals("Photo")) {
                    engine = PhotoEngine.Instance;
                }
                else {
                    engine = AudioEngine.Instance;
                }
                product = new ExpressProductModel(engine);
                break;
            case ProductName.Standard:
                product = new StandardProductModel();
                break;
            case ProductName.Enterprise:
                product = new EnterpriseProductModel();
                break;
            }

            Application.EnableVisualStyles();
            
            if (FamilyKernel.Instance.LoadProduct(product)) {
                FamilyKernel.Instance.RunLoadedProduct();
            }
        }

        private void mExpressGroupBox_MouseCaptureChanged(object sender, EventArgs e) {
            mEnterpriseGroupBox.ForeColor = Color.White;
            mFullGroupBox.ForeColor = Color.White;
            mExpressGroupBox.ForeColor = Color.YellowGreen;
            ChosenProductName = ProductName.Express;
        }

        private void mFullGroupBox_MouseCaptureChanged(object sender, EventArgs e) {
            mExpressGroupBox.ForeColor = Color.White;
            mEnterpriseGroupBox.ForeColor = Color.White;
            mFullGroupBox.ForeColor = Color.YellowGreen;
            ChosenProductName = ProductName.Standard;
        }

        private void mEnterpriseGroupBox_MouseCaptureChanged(object sender, EventArgs e) {
            mFullGroupBox.ForeColor = Color.White;
            mExpressGroupBox.ForeColor = Color.White;
            mEnterpriseGroupBox.ForeColor = Color.YellowGreen;
            ChosenProductName = ProductName.Enterprise;
        }

        private void mTryItButton_Click(object sender, EventArgs e) {
            if (ChosenProductName == ProductName.None) {
                MessageBox.Show("You must select a product first!");
            }
            else if (ChosenProductName == ProductName.Express && mExpressEngineComboBox.SelectedIndex == -1) {
                MessageBox.Show("You must select an engine for the express edition!");
            }
            else { 
                RunChosenProduct();
            }
        }

        private void mGetProductButton_Click(object sender, EventArgs e) {
            MessageBox.Show("This option is not supported yet!");
        }

    }
}
