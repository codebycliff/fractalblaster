using System;
using System.IO;
using System.Collections.Generic;

namespace FractalBlaster.Universe {

    public interface IEngine {

        event MediaChangeHandler OnMediaChanged;

        event PlaylistChangeHandler OnPlaylistChanged;


        MediaFile CurrentMedia { get; }

        Playlist CurrentPlaylist { get; }

        
        IEnumerable<IPlugin> AllPlugins { get; }

        IInputPlugin InputPlugin { get; }

        IOutputPlugin OutputPlugin { get; }

        
        Boolean IsMediaLoaded { get; }
        
        Boolean IsPlaylistLoaded { get; }


        void Load(MediaFile file);

        void Load(Playlist playlist);

        void LoadMedia(String file);
        
        void LoadPlaylist(String path);


        void UnloadMedia();

        void UnloadPlaylist();

    }

}