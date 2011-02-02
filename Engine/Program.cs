using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster;
using System.IO;
using System.Runtime.InteropServices;

namespace Engine
{
    class Program
    {
        MemoryStream pcm;
        AudioFile a;

        struct NativeBufferPair
        {
            public GCHandle handle;
            public IntPtr array;
        }

        LinkedList<NativeBufferPair> native = new LinkedList<NativeBufferPair>();

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

        public delegate int BufferSize();
        public delegate IntPtr Buffer(bool gc);

        static void Main(string[] args)
        {
            new Program();
        }
        public Program()
        {
            Console.Write("Enter an audio file path: ");
            string path = Console.ReadLine();
            a = new AudioFile(path);
            a.Open();

            BufferSize b = new BufferSize(getBufferSize);
            GC.KeepAlive(b);
            Buffer g = new Buffer(getBuffer);
            GC.KeepAlive(g);

            IntPtr stream = CreateOutputStream(g,b, a.info.Channels);
            IntPtr wave = WaveInterfaceInstance();

            ChangeOutputStream(stream);

            // Pause, UnPause, Stop Test Code
            //System.Threading.Thread.Sleep(10000);
            //Pause();
            //System.Threading.Thread.Sleep(10000);
            //UnPause();
            //System.Threading.Thread.Sleep(10000);
            //Stop();


            // Hold for 5 minutes before stopping
            System.Threading.Thread.Sleep((a.info.Duration+5)*1000);
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
            }
            pcm = a.ReadFrames(500);
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
