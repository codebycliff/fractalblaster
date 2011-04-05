using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
using System.Windows.Forms;

namespace FractalBlaster.Plugins.AvailablePluginsView {

    [PluginAttribute(Name = "Available Plugins", Description = "Shows the currently loaded plugins")]
    public class AvailablePluginsViewPlugin : IViewPlugin {


        public Form UserInterface { get; private set; }
        
        public AppContext Context { get; private set; }

        public void Initialize(AppContext context) {
            Context = context;
            PluginsView ui = new PluginsView();
            foreach (IPlugin plugin in Context.Plugins) {
                ui.AddPlugin(plugin);
            }
            UserInterface = ui;
        }
    }
}
