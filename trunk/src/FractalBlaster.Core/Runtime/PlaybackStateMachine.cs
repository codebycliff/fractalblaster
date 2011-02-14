using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core.Legacy;
namespace FractalBlaster.Core.Runtime
{
    public static class PlaybackStateMachine
    {
        enum PlaybackState { Playing, Paused, Stopped };

        static PlaybackState state;
        static Output output;
        static string CurrentlyPlaying;

        static PlaybackStateMachine()
        {
            state = PlaybackState.Stopped;
            output = DLLMaster.getOutput();
        }

        public static void Open(string file)
        {
            output.Open(file);
            state = PlaybackState.Stopped;
            CurrentlyPlaying = file;
        }

        public static void Play()
        {
            switch (state)
            {
                case PlaybackState.Paused:
                    output.Resume();
                    state = PlaybackState.Playing;
                    break;
                case PlaybackState.Playing:
                    break;
                case PlaybackState.Stopped:
                    state = PlaybackState.Playing;
                    output.Play();
                    break;
            }
        }

        public static void Pause()
        {
            switch (state)
            {
                case PlaybackState.Paused:
                    output.Resume();
                    state = PlaybackState.Playing;
                    break;
                case PlaybackState.Playing:
                    output.Pause();
                    state = PlaybackState.Paused;
                    break;
                case PlaybackState.Stopped:
                    break;
            }
        }

        public static void Stop()
        {
            switch (state)
            {
                case PlaybackState.Paused:
                    output.Stop();
                    state = PlaybackState.Stopped;
                    break;
                case PlaybackState.Playing:
                    output.Stop();
                    state = PlaybackState.Stopped;
                    break;
                case PlaybackState.Stopped:
                    break;
            }
        }

        public static void Back()
        {
        }

        public static void Forward()
        {
        }

        public static bool isPlaying()
        {
            return (state == PlaybackState.Playing);
        }

        public static string SongPlaying()
        {
            return CurrentlyPlaying;
        }
    }
}
