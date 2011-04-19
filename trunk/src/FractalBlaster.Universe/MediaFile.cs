using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;


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
    public class MediaFile : ISerializable {

        /// <summary>
        /// Static constructor that initializes the list of metadata plugins.
        /// </summary>
        static MediaFile() {
            MetadataPlugins = new List<IMetadataPlugin>();
        }

        /// <summary>
        /// Property containing the list of available metdata plugins.
        /// </summary>
        public static IEnumerable<IMetadataPlugin> MetadataPlugins { get; set; }

        /// <summary>
        /// The media file's information represented as an instance of <see 
        /// cref="FileInfo"/>.
        /// </summary>
        public FileInfo Info { get { return FileInfo; } private set { FileInfo = value; } }

        /// <summary>
        /// An enumeration representing the media file's metadata.
        /// </summary>
        public Metadata Metadata { get; private set; }

        /// <summary>
        /// Constructor that attempts to initialize a media file from the
        /// file located at the specified path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public MediaFile(String path) {
            Info = new FileInfo(path);
            Metadata = new Metadata();
            ReadMetadata();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="metadata">The metadata.</param>
        public MediaFile(String path, Metadata metadata) {
            this.Info = new FileInfo(path);
            this.Metadata = metadata;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile"/> class.
        /// </summary>
        /// <param name="FileInfo">The FileInfo.</param>
        /// <param name="context">The context.</param>
        protected MediaFile(SerializationInfo info, StreamingContext context) {
            Metadata = (Metadata)info.GetValue("metadata", typeof(Metadata));
            Info = new FileInfo((string)info.GetValue("filename", typeof(string)));
            ReadMetadata();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFile"/> class.
        /// </summary>
        protected MediaFile() { }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="FileInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("metadata", Metadata);
            info.AddValue("filename", Info.FullName);
        }

        #region [ Private ]

        /// <summary>
        /// Private helper method to loop through all the metadata plugins
        /// and add all the metadata found for each plugin.
        /// </summary>
        private void ReadMetadata() {
            List<MediaProperty> readprops = new List<MediaProperty>();
            foreach (IMetadataPlugin plugin in MetadataPlugins) {
                readprops.AddRange(plugin.Analyze(this));
            }
            foreach (MediaProperty p in readprops) {
                Metadata[p.Name] = p;
            }
        }

        /// <summary>
        /// Gets or sets the file info.
        /// </summary>
        /// <value>
        /// The file info.
        /// </value>
        private FileInfo FileInfo { get; set; }

        #endregion

    }
}
