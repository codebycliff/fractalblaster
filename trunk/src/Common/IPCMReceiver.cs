using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common
{
    public interface IPCMReceiver
    {
        void receiveFrames(MemoryStream PCM);
    }
}
