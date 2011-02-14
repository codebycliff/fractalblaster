using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace FractalBlaster.Universe {
    
    /// <remarks>
    /// Class representing a media file (typically audio) and it's associated
    /// metadata. This class encapsulates the reading of a media file's 
    /// metadata by looping through the available metadata readers during
    /// the object's construction. The metadata readers are represented as
    /// instances of <see cref="IMetadataPlugin"/> and are found by querying
    /// the <see cref="IRuntimeKernel"/> instance passed into the Initialize()
    /// method
    /// </remarks>
    public class MediaFile {

        /// <summary>
        /// The media file's information represented as an instance of <see 
        /// cref="FileInfo"/>.
        /// </summary>
        public FileInfo Info { get; private set; }

        /// <summary>
        /// An enumeration representing the media file's metadata.
        /// </summary>
        public IEnumerable<MediaProperty> Metadata { get; private set; }

        /// <summary>
        /// Constructor that attempts to initialize a media file from the
        /// file located at the specified path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public MediaFile(String path) {
            Info = new FileInfo(path);
            Metadata = ReadMetadata();
        }

        /// <summary>
        /// Static method that is used to initialize the metadata handlers
        /// by querying the specified instance of <see cref="IRuntimeKernel"/>
        /// for available plugins of the type <see cref="IMetadataPlugin"/>.
        /// </summary>
        /// <param name="kernel">
        /// IRuntimeKernel instance that is queried for available plugins.
        /// </param>
        public static void Initialize(IRuntimeKernel kernel) {
            Kernel = kernel;
            MetadataPlugins = new List<IMetadataPlugin>();
            MetadataPlugins.AddRange(Kernel.GetPlugins(typeof(IMetadataPlugin)).Cast<IMetadataPlugin>());
        }

        #region [ Private ]

        /// <summary>
        /// Private helper method to loop through all the metadata plugins
        /// and return an enumeration of all metdata read using all the 
        /// plugins.
        /// </summary>
        /// <returns>
        /// Enumeration of media properties representing the metadata.
        /// </returns>
        private IEnumerable<MediaProperty> ReadMetadata() {
            List<MediaProperty> readprops = new List<MediaProperty>();
            foreach (IMetadataPlugin plugin in MetadataPlugins) {
                readprops.AddRange(plugin.Analyze(this));
            }
            return readprops;
        }

        /// <summary>
        /// Private property containing the list of available metdata plugins.
        /// </summary>
        private static List<IMetadataPlugin> MetadataPlugins { get; set; }

        /// <summary>
        /// Private property containing the kernel that is currently hosting
        /// the application environment where this class is being used.
        /// </summary>
        private static IRuntimeKernel Kernel { get; set; }
        
        #endregion

    }
}
