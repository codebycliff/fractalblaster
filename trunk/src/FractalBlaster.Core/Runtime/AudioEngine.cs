using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core;
using FractalBlaster.Universe;

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

        public MediaFile CurrentMedia { get; private set; }

        public IInputPlugin InputPlugin { get; private set; }

        public IOutputPlugin OutputPlugin { get; private set; }

        public IEnumerable<String> SupportedFileExtensions {
            get { return (Kernel as FamilyKernel).Product.SupportedFileExtensions.AsEnumerable(); }
        }

        public bool IsMediaLoaded { get; private set; }

        public IPlaylistPlugin CurrentPlaylist { get; private set; }

        public bool IsPlaylistLoaded { get; private set; }

        public bool Initialize(IRuntimeKernel kernel) {
            Kernel = kernel;
            InputPlugin = Kernel.GetDefaultPlugin(typeof(IInputPlugin)) as IInputPlugin;
            OutputPlugin = Kernel.GetDefaultPlugin(typeof(IOutputPlugin)) as IOutputPlugin;
            return true;
        }

        public void LoadMedia(String path) {
            CurrentMedia = InputPlugin.OpenMedia(path);
            IsMediaLoaded = true;
            OnMediaChanged(CurrentMedia);
        }

        public void UnloadMedia() {
            if (OutputPlugin.IsPlaying) {
                OutputPlugin.Stop();
            }
            InputPlugin.CloseMedia();
            CurrentMedia = null;
            IsMediaLoaded = false;
        }

        public void LoadPlaylist(IPlaylistPlugin plugin) {
            CurrentPlaylist = plugin;
            IsPlaylistLoaded = true;
        }

        public void UnloadPlaylist() {
            CurrentPlaylist = null;
            IsPlaylistLoaded = false;
        }
        
        #endregion
        
        #region [ Private ]

        public AudioEngine() {

        }

        private IRuntimeKernel Kernel { private get; private set; }
        private static AudioEngine mInstance;
        private MediaFile mMediaFile;
        
        #endregion

    }

}
