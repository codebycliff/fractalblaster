using System;
using System.IO;
using System.Collections.Generic;

namespace FractalBlaster.Universe {

    public interface IEngine {

        event MediaChangeHandler OnMediaChanged;

        event PlaylistChangeHandler OnPlaylistChanged;

        MediaFile CurrentMedia { get; }

        Boolean IsMediaLoaded { get; }

        IPlaylistPlugin CurrentPlaylist { get; }

        Boolean IsPlaylistLoaded { get; }

        IInputPlugin InputPlugin { get; }

        IOutputPlugin OutputPlugin { get; }

        IEnumerable<String> SupportedFileExtensions { get; }

        Boolean Initialize(IRuntimeKernel kernel);

        void LoadMedia(String file);

        void UnloadMedia();

        void LoadPlaylist(IPlaylistPlugin plugin);

        void UnloadPlaylist();

    }

}