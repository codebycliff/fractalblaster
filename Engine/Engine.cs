using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster;
using System.IO;
using System.Runtime.InteropServices;

namespace Engine
{
    public class Engine
    {
        MemoryStream pcm;
        AudioFile a;
        BufferSize b;
        Buffer g;
        IntPtr stream;
        string filepath;

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

        public Engine()
        {
            b = new BufferSize(getBufferSize);
            g = new Buffer(getBuffer);
        }

        public void openFile(String path)
        {
            filepath = path;
            a = new AudioFile(path);
            a.Open(); 
            IntPtr wave = WaveInterfaceInstance();
        }

        public AudioMetadata getMetadata()
        {
            return a.getMetadata();
        }

        public void playFile()
        {
            stream = CreateOutputStream(g, b, a.info.Channels);
            ChangeOutputStream(stream);
        }

        public void StopFile()
        {
            Stop();
            a.Close();
            AudioFile b = new AudioFile(filepath);
            b.Open();
            a = b;
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
