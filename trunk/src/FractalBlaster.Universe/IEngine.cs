using System;
using System.IO;
using System.Collections.Generic;

namespace FractalBlaster.Universe {

    public interface IEngine {

        event MediaChangeHandler OnMediaChanged;

        event PlaylistChangeHandler OnPlaylistChanged;


        IEnumerable<IPlugin> AllPlugins { get; }

        IInputPlugin InputPlugin { get; }

        IOutputPlugin OutputPlugin { get; }

        IPlaybackTimer Timer { get; }

        MediaFile CurrentMedia { get; }
        
        Boolean IsMediaLoaded { get; }

        Playlist CurrentPlaylist { get; }

        void Load(MediaFile file);

        void Unload();

    }

}