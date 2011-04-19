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

    /// <remarks>
    /// The kernel for the application which is responsible for loading the
    /// plugins and setting the application up.
    /// </remarks>
    public class FamilyKernel : IRuntimeKernel {

        /// <summary>
        /// Gets the singleton instance of the family kernel.
        /// </summary>
        public static FamilyKernel Instance {
            get {
                if (mInstance == null) {
                    mInstance = new FamilyKernel();
                }
                return mInstance;
            }
        }

        #region [ IRuntimeKernel Implementation ]

        /// <summary>
        /// Gets the application for the currently running application.
        /// </summary>
        public AppContext Context { get; private set; }

        /// <summary>
        /// Gets the product model for the currently running application.
        /// </summary>
        public IProductModel Product { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the kernel has a product loaded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this kernel has a product loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsProductLoaded { get; private set; }

        /// <summary>
        /// Loads the product model into the kernel.
        /// </summary>
        /// <param name="model">The product model for the kernel.</param>
        public void LoadProduct(IProductModel model) {
            Product = model;
            IsProductLoaded = true;
        }

        /// <summary>
        /// Builds the windows forms application context that is used to run
        /// the actual program.
        /// </summary>
        /// <returns>
        /// Returns the application context that is used to run the program
        /// </returns>
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

        /// <summary>
        /// Prevents a default instance of the <see cref="FamilyKernel"/> class from being created.
        /// </summary>
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

        /// <summary>
        /// The singleton instance of the family kernel that is returned by 
        /// the Instance property.
        /// </summary>
        private static FamilyKernel mInstance;

        #endregion


    }
}
