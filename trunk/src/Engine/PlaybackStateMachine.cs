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
        static Common.Output output;
        static Common.Decoder decoder;
        static string CurrentlyPlaying;

        static PlaybackStateMachine()
        {
            state = PlaybackState.Stopped;
            output = Common.DLLMaster.getOutput();
        }

        public static void Open(string file)
        {
            output.Stop();
            if (decoder != null)
            {
                decoder.Close();
            }
            decoder = Common.DLLMaster.getDecoder(file);
            decoder.Open();
            decoder.SeekBeginning();
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
                    decoder.SeekBeginning();
                    state = PlaybackState.Stopped;
                    break;
                case PlaybackState.Playing:
                    output.Stop();
                    decoder.SeekBeginning();
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

        public static Common.Decoder getDecoder()
        {
            return decoder;
        }
    }
}
