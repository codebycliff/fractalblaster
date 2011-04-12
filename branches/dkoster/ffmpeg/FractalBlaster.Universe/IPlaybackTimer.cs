using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe
{
    public interface IPlaybackTimer
    {
        int currentTime { get; set; }
        void timerStart();
        void timerStop();
    }
}
