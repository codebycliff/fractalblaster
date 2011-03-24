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
        MemoryStream GetFrames();
    }
}
