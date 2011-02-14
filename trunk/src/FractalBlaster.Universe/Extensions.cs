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

        #region [ System.IO.Info ]

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

        #endregion

        #region [ System.Linq.IEnumerable<T> ]

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumeration) {
            return enumeration.Reverse().AsEnumerable();
        }

        #endregion

        #region [ System.Configuration.AppSettingsReader ]

        public static String GetString(this AppSettingsReader settings, String key) {
            return settings.GetValue(key, typeof(String)) as String;
        }

        #endregion

        #region [ FractalBlaster.Universe.Metadata ]

        public static AudioMetadata GetAudioMetadata(this Metadata metadata) {
            return new AudioMetadata() {
                Album = metadata["Album"].Value.ToString(),
                Artist = metadata["Artists"].Value.ToString()
            };
        }

        #endregion
    
    }

}
