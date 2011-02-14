using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core;
using FractalBlaster.Universe;
using System.IO;

namespace FractalBlaster.Core.Runtime {

    public class AudioEngine : IEngine {

        public static AudioEngine Instance {
            get {
                if (mInstance == null) {
                    mInstance = new AudioEngine();
                }
                return mInstance;
            }
        }

        #region [ IEngine ]

        public event MediaChangeHandler OnMediaChanged;

        public event PlaylistChangeHandler OnPlaylistChanged;

        public MediaFile CurrentMedia { get; private set; }

        public Playlist CurrentPlaylist { get; private set; }

        public IInputPlugin InputPlugin { get; private set; }

        public IOutputPlugin OutputPlugin { get; private set; }

        public IEnumerable<IPlugin> AllPlugins { get; private set; }

        public bool IsMediaLoaded { get; private set; }

        public bool IsPlaylistLoaded { get; private set; }

        public bool Initialize(IRuntimeKernel kernel) {
            InputPlugin = Application.Kernel.GetDefaultPlugin(typeof(IInputPlugin)) as IInputPlugin;
            OutputPlugin = Application.Kernel.GetDefaultPlugin(typeof(IOutputPlugin)) as IOutputPlugin;
            AllPlugins = Application.Kernel.GetPlugins();

            return true;
        }

        public void Load(MediaFile file) {
            CurrentMedia = file;
            IsMediaLoaded = true;
            OnMediaChanged(CurrentMedia);
        }

        public void Load(Playlist playlist) {
            CurrentPlaylist = playlist;
            IsPlaylistLoaded = true;
        }

        public void LoadMedia(String path) {
            CurrentMedia = InputPlugin.OpenMedia(path);
            IsMediaLoaded = true;
            OnMediaChanged(CurrentMedia);
        }

        public void LoadPlaylist(String path) {
            FileInfo file = new FileInfo(path);
            IPlaylistPlugin plugin = Application.Plugins.Select(i =>
                i as IPlaylistPlugin
            ).Where(p =>
                p.IsFileExtensionSupported(file.Extension)
            ).First();
            Load(plugin.Read(path));
        }

        public void UnloadMedia() {
            if (OutputPlugin.IsPlaying) {
                OutputPlugin.Stop();
            }
            InputPlugin.CloseMedia();
            CurrentMedia = null;
            IsMediaLoaded = false;
        }

        public void UnloadPlaylist() {
            CurrentPlaylist = null;
            IsPlaylistLoaded = false;
        }
        
        #endregion
        
        #region [ Private ]

        public AudioEngine() {

        }

        private static AudioEngine mInstance;
        private MediaFile mMediaFile;
        
        #endregion


    }

}
