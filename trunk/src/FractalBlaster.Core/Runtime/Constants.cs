using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Core {

    /// <remarks>
    /// Static class containing constants and a method for working with them.
    /// </remarks>
    public static class Constants {

        /// <summary>
        /// Constant string for the default input plugin.
        /// </summary>
        public const String DEFAULT_INPUT_PLUGIN = "Plugins.Input.Default";

        /// <summary>
        /// Constant string for the default output plugin
        /// </summary>
        public const String DEFAULT_OUTPUT_PLUGIN = "Plugins.Output.Default";

        /// <summary>
        /// Constant representing no key was found.
        /// </summary>
        public const String NO_KEY_FOUND = "NO KEY FOUND";

        /// <summary>
        /// Gets the default plugin key represented as one of the constants in
        /// this class.
        /// </summary>
        /// <param name="t">The type for which the constant is for.</param>
        /// <returns></returns>
        public static String GetDefaultPluginKey(Type t) {
            if (t == typeof(IInputPlugin)) {
                return DEFAULT_INPUT_PLUGIN;
            }
            else if (t == typeof(IOutputPlugin)) {
                return DEFAULT_OUTPUT_PLUGIN;
            }
            else {
                return NO_KEY_FOUND;
            }
        }

    }

}
