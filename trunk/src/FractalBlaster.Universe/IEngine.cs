using System;
using System.IO;
using System.Collections.Generic;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Interface specifying the required events, properties and methods for
    /// any implementing class.
    /// </remarks>
    public interface IEngine {

        /// <summary>
        /// Occurs when [on media changed].
        /// </summary>
        event MediaChangeHandler OnMediaChanged;

        /// <summary>
        /// Occurs when [on playlist changed].
        /// </summary>
        event PlaylistChangeHandler OnPlaylistChanged;

        /// <summary>
        /// Gets all plugins.
        /// </summary>
        IEnumerable<IPlugin> AllPlugins { get; }

        /// <summary>
        /// Gets the input plugin.
        /// </summary>
        IInputPlugin InputPlugin { get;  }

        /// <summary>
        /// Gets the output plugin.
        /// </summary>
        IOutputPlugin OutputPlugin { get; }

        /// <summary>
        /// Gets the playback timer.
        /// </summary>
        IPlaybackTimer PlaybackTimer { get; }

        /// <summary>
        /// Gets the current media.
        /// </summary>
        MediaFile CurrentMedia { get; }

        /// <summary>
        /// Gets a value indicating whether media is currently loaded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if media is currently loaded; otherwise, <c>false</c>.
        /// </value>
        Boolean IsMediaLoaded { get; }

        /// <summary>
        /// Gets or sets the current playlist.
        /// </summary>
        /// <value>
        /// The current playlist.
        /// </value>
        Playlist CurrentPlaylist { get; set; }

        /// <summary>
        /// Loads the specified media file.
        /// </summary>
        /// <param name="file">The media file to be loaded.</param>
        void Load(MediaFile file);

        /// <summary>
        /// Unloads any previously loaded media files.
        /// </summary>
        void Unload();

    }

}