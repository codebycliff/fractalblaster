using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using FractalBlaster.Universe;
using System.Reflection;

namespace Generator {

    public static class PluginManager {

        static PluginManager() {
            mPlugins = new List<IPlugin>();
            mPluginPaths = new List<String>();
            SearchPaths = new List<String>() { "bin\\Plugins" };

            foreach (String s in SearchPaths)
            {
                DirectoryInfo dir = new DirectoryInfo(s);
                foreach (FileInfo f in dir.GetFiles("*.dll"))
                {
                    try
                    {
                        Assembly asm = Assembly.LoadFrom(f.FullName);
                        foreach (Type t in asm.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IPlugin))))
                        {
                            try
                            {
                                IPlugin plugin = Activator.CreateInstance(t) as IPlugin;
                                mPlugins.Add(plugin);
                                mPluginPaths.Add(f.Name);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                    }
                    catch (BadImageFormatException ex)
                    {
                        //Console.WriteLine("\n\nFAILED: {0}\n\t{1}: {2}", file.FullName, ex.GetType(), ex.Message);
                    }
                }
            }
        }

        public static List<String> SearchPaths { get; private set; }

        public static IEnumerable<IPlugin> AllPlugins {
            get {
                return mPlugins.AsEnumerable();
            }
        }

        public static IEnumerable<string> AllPluginPaths
        {
            get
            {
                return mPluginPaths.AsEnumerable();
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

        public static string GetPathsForPlugin(IPlugin p)
        {
            for (int i = 0; i < mPlugins.Count; i++)
            {
                if (mPlugins[i] == p)
                {
                    return mPluginPaths[i];
                }
            }
            return "";
        }

        #region [ Private ]

            


        private static List<IPlugin> mPlugins;
        private static List<string> mPluginPaths;

        #endregion

    }

}
