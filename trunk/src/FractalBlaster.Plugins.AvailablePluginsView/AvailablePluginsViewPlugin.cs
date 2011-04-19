using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.AvailablePluginsView {

    /// <remarks>
    /// Class that provides a plugin view that shows the currently loaded
    /// plugins.
    /// </remarks>
    [PluginAttribute(Name = "Available Plugins", Description = "Shows the currently loaded plugins",
       Author = "Cliff Braton @ Fractal Blasters",
       Version = "0.1beta"
   )]
    public class AvailablePluginsViewPlugin : IViewPlugin {

        /// <summary>
        /// The main user interface represented as an <see cref="System.Windows.Forms.IContainerControl"/> implementation. The
        /// control returned from this property will be added to a dialog
        /// and can be shown or hidden via the 'Views' menu in the main
        /// user interface.
        /// </summary>
        public Form UserInterface { get; private set; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        public AppContext Context { get; private set; }

        /// <summary>
        /// Initializes the plugin with the specified applicatino context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Initialize(AppContext context) {
            Context = context;
            PluginsView ui = new PluginsView();
            foreach (IPlugin plugin in Context.AllPlugins) {
                ui.AddPlugin(plugin);
            }
            UserInterface = ui;
        }
    
    }
}
