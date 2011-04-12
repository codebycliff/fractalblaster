using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
namespace FractalBlaster.Core {

    public static class Constants {

        public const String DEFAULT_INPUT_PLUGIN = "Plugins.Input.Default";
        
        public const String DEFAULT_OUTPUT_PLUGIN = "Plugins.Output.Default";
        
        public const String NO_KEY_FOUND = "NO KEY FOUND";

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
