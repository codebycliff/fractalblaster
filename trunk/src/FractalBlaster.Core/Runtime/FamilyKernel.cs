using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using FractalBlaster.Core.UI;
using FractalBlaster.Universe;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace FractalBlaster.Core.Runtime {
    
    public class FamilyKernel : IRuntimeKernel {

        public static FamilyKernel Instance {
            get {
                if (mInstance == null) {
                    mInstance = new FamilyKernel();
                }
                return mInstance;
            }
        }

        #region [ IRuntimeKernel ]

        public AppContext Context { get; private set; }

        public IProductModel Product { get; private set; }

        public bool IsProductLoaded { get; private set; }

        public void LoadProduct(IProductModel model) {
            Product = model;
            IsProductLoaded = true;
        }

        public Form BuildProduct() {
            Form form = new ProductForm();
            form.Text = String.Format("FractalBlaster - {0} Edition", Product.Name);
            return form;
        }

        #endregion

        #region [ Private ]

        private FamilyKernel() {
            Context = new AppContext(); 
            Context.Plugins = PluginManager.AllPlugins;
            Context.DefaultPlugins = PluginManager.AllPlugins;
            Context.Settings = new AppSettingsReader();
            Context.Engine = new AudioEngine(Context);
            MediaFile.MetadataPlugins = Context.Plugins.OfType<IMetadataPlugin>();
        }

        private static FamilyKernel mInstance;

        #endregion


    }
}
