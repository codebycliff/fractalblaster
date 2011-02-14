using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FractalBlaster.Core.UI;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.Runtime {
    
    public class FamilyKernel : IRuntimeKernel {

        public IEngine Engine { get; private set; }

        public IProductModel Product { get; private set; }

        public Boolean IsProductLoaded { get; private set; }

        public Boolean LoadProduct(IProductModel product) {
            Product = product;
            
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
            mainwindow.Text = String.Format("FractalBlaster - {0} Edition", Product.Name);

            if (Application.OpenForms.Count == 0) {
                Application.Run(mainwindow);
            }
            else {
                mainwindow.Show();
            }
        }

        #region [ IRuntimeKernel ]

        public static FamilyKernel Instance {
            get {
                if (mInstance == null) {
                    mInstance = new FamilyKernel();
                }
                return mInstance;
            }
        }

        public IEnumerable<IPlugin> GetPlugins() {
            return PluginManager.AllPlugins;
        }

        public IPlugin GetDefaultPlugin(Type type) {
            String configKey = UserPreferences.GetDefaultPluginKey(type);
            return PluginManager.GetPlugins(type).Where(p =>
                p.Id.CompareTo(Preferences.GetString(configKey)) == 0
            ).First();
        }

        #endregion

        #region [ Private ]

        private UserPreferences Preferences { private get; private set; }

        private FamilyKernel() {
            Preferences = UserPreferences.Initialize(new IniPreferenceReader("FractalBlaster.ini"));
            
            Engine = AudioEngine.Instance;
            Engine.Initialize(this);

            MediaFile.Initialize(this);
        }

        private static class PluginManager {

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

        private static FamilyKernel mInstance;

        #endregion

    }
}
