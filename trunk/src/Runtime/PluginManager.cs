using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using FractalBlaster.Universe;
using System.Reflection;

namespace FractalBlaster.Runtime {

    public static class PluginManager {

        static PluginManager() {
            mPlugins = new List<IPlugin>();
            SearchPaths = new List<String>() { "Plugins" };
        }

        public static List<String> SearchPaths { get; private set; }

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

        public static IEnumerable<IPlugin> GetInterfaces(Type t)
        {
            return AllPlugins.Where(p =>
                p.GetType().GetInterfaces().Contains(t)
             ).AsEnumerable();
        }

        public static void Refresh() {
            mPlugins.Clear();
            foreach (String path in SearchPaths) {
                mPlugins.AddRange(GetPluginsFromDirectory(new DirectoryInfo(path)));
            }
        }

        #region [ Private ]


        private static List<IPlugin> GetPluginsFromDirectory(DirectoryInfo dir) {
            List<IPlugin> plugins = new List<IPlugin>();
            foreach (FileInfo f in dir.GetFiles("*.dll")) {
                Debug.printline(f.Name);
                try {
                    Assembly asm = Assembly.LoadFrom(f.FullName);
                    foreach (Type t in asm.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IPlugin)))) {
                        try {
                            Debug.printline("Valid plugin type");
                            
                            //Calls default constructor
                            IPlugin plugin = Activator.CreateInstance(t) as IPlugin;
                            
                            plugins.Add(plugin);
                        }
                        catch (Exception e) {
                            Debug.printline("\n\nFAILED: " + f.FullName + "\n\t" + e.GetType().ToString() + " : " + e.Message);
                        }
                    }
                }
                catch (BadImageFormatException ex) {
                    Debug.printline("\n\nFAILED: " + f.FullName + "\n\t" + ex.GetType().ToString() + " : " + ex.Message);
                }
            }
            return plugins;
        }

        private static List<IPlugin> mPlugins;

        #endregion

    }

}
