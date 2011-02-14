using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {
    
    /// <remarks>
    /// Defines the members that must be implemented by any plugin that wishes
    /// to provide support for a certain playlist format.
    /// </remarks>
    public interface IPlaylistPlugin {

        /// <summary>
        /// Determines whether or not this plugin can create a <see 
        /// cref="Playlist"/> instance from the specified file extension.
        /// </summary>
        /// <param name="extension">
        /// String representing the file extension in question.
        /// </param>
        /// <returns>
        /// Boolean value representing whether or not the file extension
        /// is supported or not.
        /// </returns>
        Boolean IsFileExtensionSupported(String extension);

        /// <summary>
        /// Attempts to read in the file at the specified path and create an
        /// instance of <see cref="Playlist"/> based on it's contents.
        /// </summary>
        /// <param name="path">
        /// The path the file representing the playlist to be read in.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Playlist"/> based on the contents of the
        /// file at the specified path.
        /// </returns>
        Playlist Read(String path);

    }

}
