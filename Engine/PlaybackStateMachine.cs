using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public static class PlaybackStateMachine
    {
        enum PlaybackState { Playing, Paused, Stopped };

        static PlaybackState state;
        public static Engine engine;        

        static PlaybackStateMachine()
        {
            state = PlaybackState.Stopped;
        }

        public static void Open(string file)
        {
            engine.openFile(file);
            state = PlaybackState.Stopped;
        }

        public static void Play()
        {
            switch (state)
            {
                case PlaybackState.Paused:
                    Engine.UnPause();
                    state = PlaybackState.Playing;
                    break;
                case PlaybackState.Playing:
                    break;
                case PlaybackState.Stopped:
                    state = PlaybackState.Playing;
                    engine.playFile();
                    break;
            }
        }

        public static void Pause()
        {
            switch (state)
            {
                case PlaybackState.Paused:
                    Engine.UnPause();
                    state = PlaybackState.Playing;
                    break;
                case PlaybackState.Playing:
                    Engine.Pause();
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
                    engine.StopFile();
                    state = PlaybackState.Stopped;
                    break;
                case PlaybackState.Playing:
                    engine.StopFile();
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
    }
}
