using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common
{
    public interface IPCMReceiver
    {
        PCMReceiver Generate();
    }

    public abstract class PCMReceiver
    {
        public abstract void receiveFrames(MemoryStream PCM);
    }
}
