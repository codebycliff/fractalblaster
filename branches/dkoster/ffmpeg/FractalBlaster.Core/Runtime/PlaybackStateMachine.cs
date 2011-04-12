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
    /// Controls the state of playback and ensures the proper functionality is invoked when each type of function is called
    /// </summary>
    public class PlaybackStateMachine : IOutputPlugin {

        public PlaybackState State { get; private set; }

        public PlaybackStateMachine(IOutputPlugin output, IInputPlugin input) {
            State = PlaybackState.Stopped;
            OutputStream = output;
            InputController = input;
            output.Volume = 100;
        }
        
        #region [ IOutputPlugin ]

        public void Initialize(AppContext context) {
            Context = context;
        }

        public void Play() {
            if (Context.Engine.IsMediaLoaded) {
                Context.Engine.Timer.timerStart();
                switch (State) {
                case PlaybackState.Paused:
                    OutputStream.Resume();
                    State = PlaybackState.Playing;
                    break;
                case PlaybackState.Playing:
                    break;
                case PlaybackState.Stopped:
                    State = PlaybackState.Playing;
                    Context.Engine.Timer.currentTime = 0;
                    OutputStream.Play();
                    break;
                }
            }
            else {
                //FamilyKernel.Log.Info("Attempt to Play in the PlaybackStateMachine without Engine being loaded with media.");
            }
        }

        public int Volume
        {
            get { return OutputStream.Volume; }
            set { OutputStream.Volume = value; }
        }

        public bool IsPlaying {
            get { return State == PlaybackState.Playing && OutputStream.IsPlaying; }
        }

        public void Pause() {
            if (!Context.Engine.IsMediaLoaded) {
                //FamilyKernel.Log.Info("Attempt to Play in the PlaybackStateMachine without Engine being loaded with media.");
                return;
            }
            Context.Engine.Timer.timerStop();
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
                Context.Engine.Timer.timerStop();
                Context.Engine.Timer.currentTime = 0;
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

        public void Resume() {
            Play();
        }
        
        #endregion

        #region [ Private ]

        private IOutputPlugin OutputStream { get; set; }
        private IInputPlugin InputController { get; set; }
        private AppContext Context { get; set; }
        
        #endregion
    }
}
