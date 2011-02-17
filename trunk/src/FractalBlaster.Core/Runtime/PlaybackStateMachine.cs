using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.Runtime {

    public enum PlaybackState {
        Stopped,
        Playing,
        Paused
    }

    /// <summary>
    /// TODO:  Add Forward() and Backward() methods.
    /// </summary>
    public class PlaybackStateMachine : IOutputPlugin {

        public PlaybackState State { get; private set; }
        
        public PlaybackStateMachine(IOutputPlugin output) {
            State = PlaybackState.Stopped;
            OutputStream = output;
        }
        
        #region [ IOutputPlugin ]

        public void Initialize(AppContext context) {
            Context = context;
        }

        public void Play() {
            if (Context.Engine.IsMediaLoaded) {
                switch (State) {
                case PlaybackState.Paused:
                    OutputStream.Resume();
                    State = PlaybackState.Playing;
                    break;
                case PlaybackState.Playing:
                    break;
                case PlaybackState.Stopped:
                    State = PlaybackState.Playing;
                    OutputStream.Play();
                    break;
                }
            }
            else {
                //FamilyKernel.Log.Info("Attempt to Play in the PlaybackStateMachine without Engine being loaded with media.");
            }
        }

        public bool IsPlaying {
            get { return State == PlaybackState.Playing && OutputStream.IsPlaying; }
        }

        public void Pause() {
            if (Context.Engine.IsMediaLoaded) {
                //FamilyKernel.Log.Info("Attempt to Play in the PlaybackStateMachine without Engine being loaded with media.");
                return;
            }
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

        public bool IsPaused {
            get { return State == PlaybackState.Paused && OutputStream.IsPaused; }
        }

        public void Stop() {
            if (Context.Engine.IsMediaLoaded) {
                switch (State) {
                case PlaybackState.Paused:
                case PlaybackState.Playing:
                    OutputStream.Stop();
                    State = PlaybackState.Stopped;
                    break;
                case PlaybackState.Stopped:
                    break;
                }
            }
            //FamilyKernel.Log.Info("Attempt to Play in the PlaybackStateMachine without Engine being loaded with media.");
        }

        public void Resume() {
            Play();
        }
        
        #endregion

        #region [ Private ]

        private IOutputPlugin OutputStream { get; set; }
        private AppContext Context { get; set; }
        
        #endregion
    }
}
