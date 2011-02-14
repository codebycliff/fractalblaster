using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.Runtime {
    
    enum PlaybackState { 
        Playing, 
        Paused, 
        Stopped 
    };

    public static class PlaybackStateMachine {
        

        private static PlaybackState State { get; set; }
        private static IOutputPlugin Output { get; set; }

        static PlaybackStateMachine() {
            State = PlaybackState.Stopped;
            Output = Application.Kernel.Engine.OutputPlugin;
            State = PlaybackState.Stopped;
        }

        public static void Play() {
            switch (State) {
            case PlaybackState.Paused:
                Output.Resume();
                State = PlaybackState.Playing;
                break;
            case PlaybackState.Playing:
                break;
            case PlaybackState.Stopped:
                State = PlaybackState.Playing;
                Output.Play();
                break;
            }
        }

        public static void Pause() {
            switch (State) {
            case PlaybackState.Paused:
                Output.Resume();
                State = PlaybackState.Playing;
                break;
            case PlaybackState.Playing:
                Output.Pause();
                State = PlaybackState.Paused;
                break;
            case PlaybackState.Stopped:
                break;
            }
        }

        public static void Stop() {
            switch (State) {
            case PlaybackState.Paused:
                Output.Stop();
                State = PlaybackState.Stopped;
                break;
            case PlaybackState.Playing:
                Output.Stop();
                State = PlaybackState.Stopped;
                break;
            case PlaybackState.Stopped:
                break;
            }
        }

        public static void Back() {
        }

        public static void Forward() {
        }

        public static bool isPlaying() {
            return (State == PlaybackState.Playing);
        }

    }
}
