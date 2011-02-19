using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core;
using FractalBlaster.Universe;
using System.IO;

namespace FractalBlaster.Core.Runtime {

    public class AudioEngine : IEngine {

        public AppContext Context { get; private set; }

        #region [ IEngine ]

        public event MediaChangeHandler OnMediaChanged;

        public event PlaylistChangeHandler OnPlaylistChanged;


        public IInputPlugin InputPlugin { get; private set; }

        public IOutputPlugin OutputPlugin { get; private set; }

        public IEnumerable<IPlugin> AllPlugins { get; private set; }


        public MediaFile CurrentMedia { get; private set; }

        public bool IsMediaLoaded { get; private set; }

        public void Load(MediaFile file) {
            try {
                InputPlugin.OpenMedia(file);
                CurrentMedia = file;
                IsMediaLoaded = true;
                if (OnMediaChanged != null) {
                    OnMediaChanged(CurrentMedia);
                }
            }
            catch (FileLoadException fe) {
                //FamilyKernel.Log.Error("Could not load media file in engine", fe);
            }
            catch (Exception e) {
                //FamilyKernel.Log.Error("Could not load media file in engine", e);
            }
        }

        public void Unload() {
            if (IsMediaLoaded) {
                if (OutputPlugin.IsPlaying) {
                    OutputPlugin.Stop();
                }
                InputPlugin.CloseMedia();
                CurrentMedia = null;
                IsMediaLoaded = false;
            }
        }

        #endregion
        
        #region [ Private ]

        public AudioEngine(AppContext ctx) {
            Context = ctx;
            
            IInputPlugin input = Context.DefaultPlugins.OfType<IInputPlugin>().First();
            IOutputPlugin output = Context.DefaultPlugins.OfType<IOutputPlugin>().First();

            if (input == null || output == null) {
                return;
            }
            
            InputPlugin = new EffectsProcessor(input);
            InputPlugin.Initialize(Context);
            OutputPlugin = new PlaybackStateMachine(output, input);
            OutputPlugin.Initialize(Context);
            
        }

        private static AudioEngine mInstance;
        
        #endregion

    }

}
