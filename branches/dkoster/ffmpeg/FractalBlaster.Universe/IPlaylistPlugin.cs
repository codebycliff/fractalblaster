using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {
    
    /// <remarks>
    /// Defines the members that must be implemented by any plugin that wishes
    /// to provide support for a certain playlist format.
    /// </remarks>
    public interface IPlaylistPlugin : IPlugin {

        /// <summary>
        /// Determines the file extension this plugin can create a <see 
        /// cref="Playlist"/> instance from.
        /// </summary>
        IEnumerable<String> SupportedFileExtensions { get;  }

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

        /// <summary>
        /// Method used to write a given playlist to the path specified.
        /// </summary>
        /// <param name="playlist">The playlist to write.</param>
        /// <param name="path">The path to the file to written to.</param>
        void Write(Playlist playlist, String path);

    }

}
