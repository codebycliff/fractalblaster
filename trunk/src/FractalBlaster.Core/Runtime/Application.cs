using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using FractalBlaster.Core.UI;
using WinApplication = System.Windows.Forms.Application;
using System.Configuration;
using System.Windows.Forms;

namespace FractalBlaster.Core.Runtime {
    
    public static class Application {

        static Application() {
            IsProductLoaded = false;
            Kernel = FamilyKernel.Instance;
            Settings = new AppSettingsReader();
        }

        public static IEnumerable<IPlugin> Plugins { get { return Kernel.GetPlugins();  } }

        public static AppSettingsReader Settings { get; private set; }

        public static IRuntimeKernel Kernel { get; private set; }
        
        public static IProductModel Product { get; private set; }

        public static Boolean IsProductLoaded { get; private set; }

        public static void LoadProduct(IProductModel product) {
            Product = product;
            try {
                IsProductLoaded = true;
            }
            catch (Exception e) {
                //TODO: Log exception
            }
        }

        public static Form Start() {
            if (!IsProductLoaded) {
                throw new Exception("No Product Loaded");
            }

            ProductForm mainwindow = new ProductForm();
            mainwindow.Text = String.Format("FractalBlaster - {0} Edition", Product.Name);

            //if (WinApplication.OpenForms.Count == 0) {
            //    WinApplication.Run(mainwindow);
            //}
            //else {
            return mainwindow;
            //}
        }
    
    }
}
