using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using FractalBlaster.Core.UI;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.Runtime {
    
    public class FamilyKernel : IRuntimeKernel {

        static FamilyKernel() {
            Instance = new FamilyKernel();
        }

        public static IRuntimeKernel Instance { get; private set; }

        #region [ IRuntimeKernel ]

        public IEngine Engine { get; private set; }

        public IEnumerable<IPlugin> GetPlugins() {
            return PluginManager.AllPlugins;
        }

        public IPlugin GetDefaultPlugin(Type type) {
            String key = Constants.GetDefaultPluginKey(type);
            return PluginManager.GetPlugins(type).Where(p =>
                p.Id.CompareTo(
                    Application.Settings.GetString(Constants.GetDefaultPluginKey(type))
                ) == 0
            ).First();
        }

        public IEnumerable<IPlugin> GetPlugins(Type type) {
            return PluginManager.GetPlugins(type);
        }

        #endregion

        #region [ Private ]

        private FamilyKernel() {
            Engine = AudioEngine.Instance;
            MediaFile.MetadataPlugins.AddRange(Engine.AllPlugins.OfType<IMetadataPlugin>());
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

        #endregion

    }
}
