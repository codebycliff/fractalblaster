using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Core.UI;

namespace FractalBlaster.Core {
    public class FamilyKernel {

        public static FamilyKernel Instance {
            get {
                if (mInstance == null) {
                    mInstance = new FamilyKernel();
                }
                return mInstance;
            }
        }

        public IProductModel CurrentProduct { get; private set; }

        public Boolean IsProductLoaded { get; private set; }

        public IEngine CurrentEngine { get; private set; }

        public Boolean LoadProduct(IProductModel product) {
            CurrentProduct = product;
            try {
                //CurrentEngine = (IEngine)Activator.CreateInstance(product.EngineTypes.First());
                //HACK: Just hard coding the value of the engine to be audio since that's all we're using right now.
                
                IsProductLoaded = true;
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        public void RunLoadedProduct() {
            if(!IsProductLoaded) {
                throw new Exception("No Product Loaded");
            }

            ProductForm mainwindow = new ProductForm();
            mainwindow.Text = String.Format("FractalBlaster - {0} Edition", CurrentProduct.Name);

            if (Application.OpenForms.Count == 0) {
                Application.Run(mainwindow);
            }
            else {
                mainwindow.Show();
            }
        }

        public static class PluginManager {

            static PluginManager() {
                mPlugins = new List<IPlugin>();
            }

            public static IEnumerable<IPlugin> AllPlugins {
                get {
                    return mPlugins.AsEnumerable();
                }
            }

            public static IEnumerable<IPlugin> GetPlugins(Type t) {
                return AllPlugins.Where(p =>
                    p.GetType() == t
                ).AsEnumerable();
            }

            #region [ Private ]

            private static List<IPlugin> mPlugins;
            
            #endregion

        }

        #region [ Private ]

        private FamilyKernel() {

        }

        private static FamilyKernel mInstance; 

        #endregion

    }
}
