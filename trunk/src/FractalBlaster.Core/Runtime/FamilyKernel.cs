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
//using log4net;

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

        //public ILog Log { get; set; }

        #region [ IRuntimeKernel ]

        public AppContext Context { get; private set; }

        public IProductModel Product { get; private set; }

        public bool IsProductLoaded { get; private set; }

        public void LoadProduct(IProductModel model) {
            Product = model;
            IsProductLoaded = true;
        }

        public ApplicationContext BuildContext() {
            ProductForm form = new ProductForm();
            
            form.Text = String.Format("FractalBlaster - {0} Edition", Product.Name);

            foreach(IEffectPlugin effect in Context.Plugins.OfType<IEffectPlugin>()) {
                effect.Enabled = false;
                form.AddEffectPlugin(effect);
            }

            foreach (IViewPlugin v in Context.Plugins.OfType<IViewPlugin>()) {
                form.AddViewPlugin(v);
            }

            foreach (IPlaylistPlugin p in Context.Plugins.OfType<IPlaylistPlugin>()) {
                form.AddPlaylistPlugin(p);
            }

            return new ApplicationContext(form);
        }

        #endregion

        #region [ Private ]

        private FamilyKernel() {
            Context = new AppContext();
            PluginManager.Refresh();
            Context.Plugins = PluginManager.AllPlugins;
            Context.DefaultPlugins = PluginManager.AllPlugins;
            Context.Settings = new AppSettingsReader();
            Context.Engine = new AudioEngine(Context);

            foreach (IPlugin plugin in Context.Plugins) {
                plugin.Initialize(Context);
            }
            MediaFile.MetadataPlugins = Context.Plugins.OfType<IMetadataPlugin>();
        }

        private static FamilyKernel mInstance;

        #endregion


    }
}
