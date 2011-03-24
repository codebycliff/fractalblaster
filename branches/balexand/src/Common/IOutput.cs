using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe
{
    public interface IOutput : IPlugin
    {
        void Start();
        void Stop();
    }
}
