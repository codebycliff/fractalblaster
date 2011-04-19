using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Static class containing various extension methods. Each extension
    /// method in this class should be placed in a region that is labeled
    /// with the name of the class for which the method is extending.
    /// </remarks>
    public static class Extensions {

        #region [ System.IO.FileInfo ]

        /// <summary>
        /// Extension method that determines whether an instance of <see 
        /// cref="Info"/> is a dll based on whether or not it's file
        /// extension is "dll".
        /// </summary>
        /// <param name="file">
        /// The instance of Info this method is acting on.
        /// </param>
        /// <returns>
        /// Boolean value representing whether or not the file is a dll.
        /// </returns>
        public static Boolean IsDll(this FileInfo file) {
            return file.Extension.ToLower().CompareTo("dll") == 0;
        }

        /// <summary>
        /// Creates the media file from the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static MediaFile CreateMediaFile(this FileInfo file) {
            return new MediaFile(file.FullName);
        }

        #endregion

        #region [ System.Linq.IEnumerable<T> ]

        /// <summary>
        /// Shuffles the specified enumeration. Current implementation only
        /// reverses the enumeration. This was meant to be a method stub, and never
        /// was properly finished.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumeration) {
            return enumeration.Reverse().AsEnumerable();
        }

        #endregion

        #region [ System.Configuration.AppSettingsReader ]

        /// <summary>
        /// Gets the string value from the settings with specified key.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static String GetString(this AppSettingsReader settings, String key) {
            return settings.GetValue(key, typeof(String)) as String;
        }

        #endregion

        #region [ FractalBlaster.Universe.Metadata ]

        /// <summary>
        /// Gets the audio metadata from the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns></returns>
        public static AudioMetadata GetAudioMetadata(this Metadata metadata) {
            return new AudioMetadata() {
                Album = metadata["Album"].Value.ToString(),
                Artist = metadata["Artists"].Value.ToString()
            };
        }

        #endregion

        #region [ FractalBlaster.Universe.IPlugin ]

        /// <summary>
        /// Gets the plugin attribute instance from the specified plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns></returns>
        public static PluginAttribute GetInfo(this IPlugin plugin) {
            PluginAttribute attr = plugin.GetType().GetCustomAttributes(typeof(PluginAttribute), false).First() as PluginAttribute;
            if (attr == null) {
                attr = new PluginAttribute() {
                    Name = "n/a",
                    Author = "n/a",
                    Description = "n/a",
                    Version = "n/a"
                };
            }
            return attr;
        }

        #endregion
    
    }

}
