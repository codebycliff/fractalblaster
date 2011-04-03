using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FractalBlaster.Universe
{
    public interface IPlaybackControl : IPlugin
    {
        void Play();
        void Pause();
        void Stop();
        void Next();
        void Previous();
        MemoryStream GetFrames(int numFramesToRead);

        void PlaybackComplete();
        int getPlaybackTime();
        double getSongLength();

        IInput input
        {
            set;
        }

        IOutput output
        {
            set;
        }

        IPlaylist playlist
        {
            set;
        }

        IPlaybackControlForm playbackControlForm
        {
            set;
        }

        Int32 volume
        {
            set;
        }

    }
}
