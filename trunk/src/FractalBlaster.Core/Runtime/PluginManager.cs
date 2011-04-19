using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using FractalBlaster.Universe;
using System.Reflection;

namespace FractalBlaster.Core.Runtime {

    /// <remarks>
    /// Static class that manages the loading of plugins.
    /// </remarks>
    public static class PluginManager {

        /// <summary>
        /// Static constructor that initializes the <see cref="PluginManager"/> class.
        /// </summary>
        static PluginManager() {
            mPlugins = new List<IPlugin>();
            SearchPaths = new List<String>() { "Plugins" };
        }

        /// <summary>
        /// Gets the list of search paths searched when looking for plugins.
        /// </summary>
        public static List<String> SearchPaths { get; private set; }

        /// <summary>
        /// Gets enumerable consisting of all the plugins that were found.
        /// </summary>
        public static IEnumerable<IPlugin> AllPlugins {
            get {
                return mPlugins.AsEnumerable();
            }
        }

        /// <summary>
        /// Gets an enumerable of plugins that match the specified type.
        /// </summary>
        /// <param name="t">The type to match for plugins returned.</param>
        /// <returns></returns>
        public static IEnumerable<IPlugin> GetPlugins(Type t) {
            return AllPlugins.Where(p =>
                p.GetType() == t
            ).AsEnumerable();
        }

        /// <summary>
        /// Gets an enumerable of plugins that match the specified type of interface.
        /// </summary>
        /// <param name="t">The type of interface to match.</param>
        /// <returns></returns>
        public static IEnumerable<IPlugin> GetInterfaces(Type t)
        {
            return AllPlugins.Where(p =>
                p.GetType().GetInterfaces().Contains(t)
             ).AsEnumerable();
        }

        /// <summary>
        /// Refreshes the list of plugins found by searching the 
        /// search paths again.
        /// </summary>
        public static void Refresh() {
            mPlugins.Clear();
            foreach (String path in SearchPaths) {
                mPlugins.AddRange(GetPluginsFromDirectory(new DirectoryInfo(path)));
            }
        }

        #region [ Private ]

        /// <summary>
        /// Private helper that gets the plugins from the specified directory.
        /// </summary>
        /// <param name="dir">The directory to look for plugins.</param>
        /// <returns></returns>
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
            foreach (DirectoryInfo directory in dir.GetDirectories())
            {
                plugins.AddRange(GetPluginsFromDirectory(directory));
            }
            return plugins;
        }

        /// <summary>
        /// Private memeber variable containing a list of all plugins.
        /// </summary>
        private static List<IPlugin> mPlugins;

        #endregion

    }

}
