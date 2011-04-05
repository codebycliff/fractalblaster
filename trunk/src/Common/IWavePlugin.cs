using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FractalBlaster.Universe
{
    public interface IWavePlugin : IPlugin
    {
        void ProcessStream(MemoryStream s);
        void Start();
        void Stop();
    }
}
