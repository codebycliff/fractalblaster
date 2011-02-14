using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;
namespace FractalBlaster.Core {

    class UserPreferences {


        public static const String DEFAULT_INPUT_PLUGIN = "Plugins.Input.Default";
        public static const String DEFAULT_OUTPUT_PLUGIN = "Plugins.Output.Default";
        public static const String NO_KEY_FOUND = "NO KEY FOUND";

        public static UserPreferences Initialize(IPreferenceReader reader) {
            mInstance = new UserPreferences();
            mInstance.Preferences = reader.Read();
            return mInstance;
        }

        public Boolean IsInitialized { get; private set; }

        public String GetString(String key) {
            if (Preferences.ContainsKey(key)) {
                Object result = null;
                Preferences.TryGetValue(key, out result);
                if (result != null) {
                    return result.ToString();
                }
            }
            return null;
        }

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

        private Dictionary<String, Object> Preferences { private get; private set; }
        private static UserPreferences mInstance;

    }

}
