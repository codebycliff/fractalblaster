using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Core;
using System.IO;

namespace FractalBlaster.Engines.Audio {
    
    public interface IAudioInput : IInputPlugin {

        void SeekBeginning();

        MemoryStream ReadFrame(Int32 numFramesToRead);

    }

}
