using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common;

namespace Engine
{
    public static class Engine
    {
        static MemoryStream PCMStream;
        static List<IPCMReceiver> PCMReceivers;

        static Engine()
        {
            PCMReceivers = new List<IPCMReceiver>();
        }

        public static void addPCMReceiver(IPCMReceiver p)
        {
            PCMReceivers.Add(p);
        }

        public static MemoryStream getNextFrameset()
        {
            PCMStream = PlaybackStateMachine.getDecoder().ReadFrame(500);
            for (int i = 0; i < PCMReceivers.Count; i++)
            {
                PCMStream.Seek(0, 0);
                PCMReceivers.ElementAt(i).receiveFrames(PCMStream);
            }
            PCMStream.Seek(0, 0);
            return PCMStream;
        }

        public static MemoryStream getCurrentFrameset()
        {
            PCMStream.Seek(0, 0);
            return PCMStream;
        }

    }
}
