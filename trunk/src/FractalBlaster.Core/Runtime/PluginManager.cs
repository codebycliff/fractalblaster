using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using FractalBlaster.Universe;
using System.Reflection;

namespace FractalBlaster.Core.Runtime {

    static class PluginManager {

        static PluginManager() {
            mPlugins = new List<IPlugin>();
            SearchPaths = new List<String>() { "Plugins" };
            LoadPlugins();
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

        #region [ Private ]

        private static void LoadPlugins() {
            foreach (String path in SearchPaths) {
                mPlugins.AddRange(GetPluginsFromDirectory(new DirectoryInfo(path)));
            }
        }

        private static List<IPlugin> GetPluginsFromDirectory(DirectoryInfo dir) {
            List<IPlugin> plugins = new List<IPlugin>();
            foreach (FileInfo f in dir.GetFiles("*.dll")) {
                try {
                    Assembly asm = Assembly.LoadFrom(f.FullName);
                    foreach (Type t in asm.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IPlugin)))) {
                        try {
                            IPlugin plugin = Activator.CreateInstance(t) as IPlugin;
                            
                            plugins.Add(plugin);
                        }
                        catch (Exception e) {
                        }
                    }
                }
                catch (BadImageFormatException ex) {
                    //Console.WriteLine("\n\nFAILED: {0}\n\t{1}: {2}", file.FullName, ex.GetType(), ex.Message);
                }
            }
            return plugins;
        }

        private static List<IPlugin> mPlugins;

        #endregion

    }

}
