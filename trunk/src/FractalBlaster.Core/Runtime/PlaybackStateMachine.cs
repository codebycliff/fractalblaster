using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.Runtime {

    /// <remarks>
    /// Enumeration of the possible playback states.
    /// </remarks>
    public enum PlaybackState {
        Stopped,
        Playing,
        Paused
    }

    /// <remarks>
    /// Controls the state of playback and ensures the proper functionality is invoked when each type of function is called
    /// </remarks>
    public class PlaybackStateMachine : IOutputPlugin {

        /// <summary>
        /// Gets the current state for the playback state machine.
        /// </summary>
        public PlaybackState State { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybackStateMachine"/> class
        /// taking in an input and output plugin to be used for playback.
        /// </summary>
        /// <param name="output">The output plugin used for outputting audio.</param>
        /// <param name="input">The input plugin used for inputting audio.</param>
        public PlaybackStateMachine(IOutputPlugin output, IInputPlugin input) {
            State = PlaybackState.Stopped;
            OutputStream = output;
            InputController = input;
            output.Volume = 100;
        }
        
        #region [ IOutputPlugin Implementation ]

        /// <summary>
        /// Initializes the playback state machine with the specified application context.
        /// </summary>
        /// <param name="context">The application context.</param>
        public void Initialize(AppContext context) {
            Context = context;
        }

        /// <summary>
        /// Method that can be called to begin outputting the media file that
        /// is currently loaded in the running <see cref="IEngine"/> instance.
        /// </summary>
        public void Play() {
            if (Context.Engine.IsMediaLoaded) {
                Context.Engine.PlaybackTimer.Timer.Start();
                switch (State) {
                case PlaybackState.Paused:
                    OutputStream.Resume();
                    State = PlaybackState.Playing;
                    break;
                case PlaybackState.Playing:
                    break;
                case PlaybackState.Stopped:
                    State = PlaybackState.Playing;
                    Context.Engine.PlaybackTimer.CurrentTime = 0;
                    OutputStream.Play();
                    break;
                }
            }
            else {
                //FamilyKernel.Log.Info("Attempt to Play in the PlaybackStateMachine without Engine being loaded with media.");
            }
        }

        /// <summary>
        /// Integer value representing the system output device volume
        /// setting in the running <see cref="IEngine"/> instance
        /// </summary>
        public Int32 Volume {
            get { return OutputStream.Volume; }
            set { OutputStream.Volume = value; }
        }

        /// <summary>
        /// Boolean value representing whether the media file that is loaded
        /// in the running <see cref="IEngine"/> instance is currently being
        /// outputted by this output plugin.
        /// </summary>
        public bool IsPlaying {
            get { return State == PlaybackState.Playing && OutputStream.IsPlaying; }
        }

        /// <summary>
        /// Method that can be called to pause the outputting of the media file
        /// that is currently loaded in the running <see cref="IEngine"/>
        /// instance.
        /// </summary>
        public void Pause() {
            if (!Context.Engine.IsMediaLoaded) {
                //FamilyKernel.Log.Info("Attempt to Play in the PlaybackStateMachine without Engine being loaded with media.");
                return;
            }
            Context.Engine.PlaybackTimer.Timer.Stop();
            switch (State) {
            case PlaybackState.Paused:
                OutputStream.Resume();
                State = PlaybackState.Playing;
                break;
            case PlaybackState.Playing:
                OutputStream.Pause();
                State = PlaybackState.Paused;
                break;
            case PlaybackState.Stopped:
                break;
            }
        }

        /// <summary>
        /// Boolean value representing whether the media file that is loaded
        /// in the running <see cref="IEngine"/> instance is currently paused
        /// by this output plugin.
        /// </summary>
        public bool IsPaused {
            get { return State == PlaybackState.Paused && OutputStream.IsPaused; }
        }

        /// <summary>
        /// Method that can be called to stop outputting the media file that
        /// is currently loaded in the running <see cref="IEngine"/> instance.
        /// </summary>
        public void Stop() {
            if (Context.Engine.IsMediaLoaded) {
                Context.Engine.PlaybackTimer.Timer.Stop();
                Context.Engine.PlaybackTimer.CurrentTime = 0;
                switch (State) {
                case PlaybackState.Paused:
                case PlaybackState.Playing:
                    OutputStream.Stop();
                    InputController.Seek(0);
                    State = PlaybackState.Stopped;

                    break;
                case PlaybackState.Stopped:
                    break;
                }
            }
            //FamilyKernel.Log.Info("Attempt to Play in the PlaybackStateMachine without Engine being loaded with media.");
        }

        /// <summary>
        /// Method that can be called to resume outputting of a previously
        /// paused media file that is currently loaded in the running
        /// <see cref="IEngine"/> instance.
        /// </summary>
        public void Resume() {
            Play();
        }
        
        #endregion

        #region [ Private ]

        /// <summary>
        /// Private instance property that holds the output plugin.
        /// </summary>
        /// <value>
        /// The output stream.
        /// </value>
        private IOutputPlugin OutputStream { get; set; }

        /// <summary>
        /// Private instance property that holds the input plugin.
        /// </summary>
        /// <value>
        /// The input controller.
        /// </value>
        private IInputPlugin InputController { get; set; }

        /// <summary>
        /// Private instance variable containing the applicatino context that this
        /// instance was initialized with.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        private AppContext Context { get; set; }
        
        #endregion
    
    }
}
