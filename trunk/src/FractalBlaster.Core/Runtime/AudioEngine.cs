using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core;
using FractalBlaster.Universe;
using System.IO;

namespace FractalBlaster.Core.Runtime {

    /// <remarks>
    /// Handles the communication between the input and output plugin, as well
    /// as the loading (playing) of audio files.
    /// </remarks>
    public class AudioEngine : IEngine {

        #region [ Events ]

        /// <summary>
        /// Fired when the media changes.
        /// </summary>
        public event MediaChangeHandler OnMediaChanged;

        /// <summary>
        /// Fires when the CurrentPlaylist changes.
        /// </summary>
        public event PlaylistChangeHandler OnPlaylistChanged;
        
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the context.
        /// </summary>
        public AppContext Context { get; private set; }

        /// <summary>
        /// Gets the input plugin that is currently active.
        /// </summary>
        public IInputPlugin InputPlugin { get; private set; }

        /// <summary>
        /// Gets the output plugin that is currently active.
        /// </summary>
        public IOutputPlugin OutputPlugin { get; private set; }

        /// <summary>
        /// Gets an enumeration of all loaded plugins.
        /// </summary>
        public IEnumerable<IPlugin> AllPlugins { get; private set; }

        /// <summary>
        /// Gets the playback timer.
        /// </summary>
        public IPlaybackTimer Timer { get; private set; }

        /// <summary>
        /// Gets the currently loaded media.
        /// </summary>
        public MediaFile CurrentMedia { get; private set; }

        /// <summary>
        /// Gets or sets the current CurrentPlaylist.
        /// 
        /// </summary>
        /// <value>
        /// The current CurrentPlaylist.
        /// </value>
        public Playlist CurrentPlaylist { get; set; }

        /// <summary>
        /// Determines whether the audio engine currently has media loaded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this this audio engine has media loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsMediaLoaded { get; private set; }
        
        #endregion

        /// <summary>
        /// Constructor taking in the application context for which the engine 
        /// should run in.
        /// </summary>
        /// <param name="ctx">The application context.</param>
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
            Timer = new PlaybackTimer();
        }
        
        /// <summary>
        /// Loads the specified media file.
        /// </summary>
        /// <param name="file">The media file to be loaded.</param>
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

        /// <summary>
        /// Unloads the currently loaded media file, if any.
        /// </summary>
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

    }

}
