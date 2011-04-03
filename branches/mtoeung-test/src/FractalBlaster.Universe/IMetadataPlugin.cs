using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {
    
    /// <remarks>
    /// Defines the members that must be implemented for any plugin that 
    /// wishes to read metadata for from a <see cref="MediaFile"/>.
    /// </remarks>
    public interface IMetadataPlugin : IPlugin {

        /// <summary>
        /// List of file extensions supported by this metadata handler.
        /// </summary>
        IEnumerable<String> SupportedFileExtensions { get; }

        /// <summary>
        /// Method that handles the reading of the metadata from a given media
        /// file and returns an enumeration of <see cref="MediaProperty"/>
        /// values representing the metadata.
        /// </summary>
        /// <param name="file">
        /// The media file whose metadata is to be read
        /// </param>
        /// <returns>
        /// An enumeration of <see cref="MediaProperty"/> values representing 
        /// the metadata that was read in.
        /// </returns>
        IEnumerable<MediaProperty> Analyze(MediaFile file);

    }

}
