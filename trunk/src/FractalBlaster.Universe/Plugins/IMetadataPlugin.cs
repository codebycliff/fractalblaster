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
        /// Method for determining whether the specified <see cref="MediaFile"
        /// /> is supported for reading metadata.
        /// </summary>
        /// <param name="file">
        /// The media file that in question.
        /// </param>
        /// <returns>
        /// True if the specified media file is supported; false otherwise.
        /// </returns>
        Boolean IsFileSupported(MediaFile file);

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
