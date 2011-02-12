using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Output
{
    public class Wrapper : Common.Output
    {
        internal class AudioOut
        {
            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr WaveInterfaceInstance();

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr ChangeOutputStream(IntPtr OutputStream);

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr CreateOutputStream([MarshalAs(UnmanagedType.FunctionPtr)]Buffer bufferfill, [MarshalAs(UnmanagedType.FunctionPtr)]BufferSize buffersize, int channels);

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern void Pause();

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern void UnPause();

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern void Stop();
        }

        MemoryStream pcm;
        Common.Decoder a;
        BufferSize b;
        Buffer g;
        IntPtr stream;
        string filepath;

        struct NativeBufferPair
        {
            public GCHandle handle;
            public IntPtr array;
        }

        public Wrapper()
        {
            b = new BufferSize(getBufferSize);
            g = new Buffer(getBuffer);
            stream = new IntPtr();
        }

        LinkedList<NativeBufferPair> native = new LinkedList<NativeBufferPair>();

        public delegate int BufferSize();
        public delegate IntPtr Buffer(bool gc);
        
        /*
        public override void Open(string path)
        {
            
        }
        */

        public override void setDecoder(Common.Decoder dec)
        {
            a = dec;
            AudioOut.WaveInterfaceInstance();
        }

        public override void Play()
        {
            stream = AudioOut.CreateOutputStream(g, b, 2);
            AudioOut.ChangeOutputStream(stream);
        }

        public override void Stop()
        {
            AudioOut.Stop();

            // Clean up Remaining Native Arrays
            for (int i = 0; i < native.Count; i++)
            {
                native.First.Value.handle.Free();
                Marshal.FreeHGlobal(native.First.Value.array);
                native.RemoveFirst();
                GC.Collect();
            }
        }

        public override void Pause()
        {
            AudioOut.Pause();
        }

        public override void Resume()
        {
            AudioOut.UnPause();
        }

        public int getBufferSize()
        {
            if (pcm == null)
                return 0;
            return (int)pcm.Length;
        }

        public IntPtr getBuffer(bool gc)
        {
            if (gc)
            {
                native.First.Value.handle.Free();
                Marshal.FreeHGlobal(native.First.Value.array);
                native.RemoveFirst();
                GC.Collect();
            }
            pcm = a.ReadFrame(500);
            if (pcm == null)
            {
                return IntPtr.Zero;
            }
            byte[] data = pcm.ToArray();
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr p = Marshal.AllocHGlobal((int)pcm.Length);
            Marshal.Copy(pcm.ToArray(), 0, p, (int)pcm.Length);

            NativeBufferPair pair = new NativeBufferPair();
            pair.array = p;
            pair.handle = dataHandle;
            native.AddLast(pair);

            return p;
        }
    }
}
