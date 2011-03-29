using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe
{
    public interface IOutput : IPlugin
    {
        void Play();
        void Stop();
        void Pause();
        void Resume();
        
        IInput input 
        {
            set; 
        }

        Int32 Volume
        {
            set;
        }

    }
}
