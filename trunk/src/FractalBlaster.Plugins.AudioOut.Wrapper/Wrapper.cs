using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.AudioOut.Wrapper {

    public struct NativeBufferPair {
        public GCHandle handle;
        public IntPtr array;
    }

    public class Wrapper : IOutputPlugin {

        public MemoryStream Pcm { get; private set; }
        
        public IntPtr Stream { get; private set; }

        public AppContext Context { get; private set; }

        public static IPlugin Instance {
            get {
                if (instance == null) {
                    instance = new Wrapper();
                }
                return instance;
            }
        }

        public void Initialize(AppContext context) {
            instance.Context = context;
        }

        #region [ IPlugin ]

        public String Author {
            get { return "Fractal Blasters"; }
        }

        public Version Version {
            get { return new Version(); }
        }

        public String Id {
            get { return typeof(Wrapper).Assembly.FullName; }
        }

        #endregion
        
        #region [ IOutputPlugin ]

        public bool IsPlaying { get; private set; }

        public bool IsPaused { get; private set; }

        public void Play() {
            Stream = AudioOut.CreateOutputStream(Buffer, BufferSize, 2);
            AudioOut.ChangeOutputStream(Stream);
            IsPlaying = true;
        }

        public void Stop() {
            AudioOut.Stop();
            IsPlaying = false;
            // Clean up Remaining Native Arrays
            for (int i = 0; i < NativeBuffers.Count; i++) {
                NativeBuffers.First.Value.handle.Free();
                Marshal.FreeHGlobal(NativeBuffers.First.Value.array);
                NativeBuffers.RemoveFirst();
                GC.Collect();
            }
            AudioOut.WaveInterfaceInstance();
        }

        public void Pause() {
            AudioOut.Pause();
            IsPaused = true;
            IsPlaying = false;
        }

        public void Resume() {
            AudioOut.UnPause();
            IsPaused = false;
            IsPlaying = true;
        }

        #endregion

        #region [ Private ]
        
        private class AudioOut {
            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr WaveInterfaceInstance();

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr ChangeOutputStream(IntPtr OutputStream);

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr CreateOutputStream([MarshalAs(UnmanagedType.FunctionPtr)]BufferHandler bufferfill, [MarshalAs(UnmanagedType.FunctionPtr)]BufferSizeHandler buffersize, int channels);

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern void Pause();

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern void UnPause();

            [DllImport("AudioOut.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern void Stop();
        }

        private Wrapper() {
            BufferSize = new BufferSizeHandler(GetBufferSize);
            Buffer = new BufferHandler(GetBuffer);
            Stream = new IntPtr();
            NativeBuffers = new LinkedList<NativeBufferPair>();
        }

        private int GetBufferSize() {
            if (Pcm == null)
                return 0;
            return (int)Pcm.Length;
        }

        private IntPtr GetBuffer(Boolean gc) {
            
            if (gc) {
                NativeBuffers.First.Value.handle.Free();
                Marshal.FreeHGlobal(NativeBuffers.First.Value.array);
                NativeBuffers.RemoveFirst();
                GC.Collect();
            }

            Pcm = Engine.InputPlugin.ReadFrames(20);
            
            if (Pcm == null) {
                return IntPtr.Zero;
            }

            GCHandle dataHandle = GCHandle.Alloc(Pcm.ToArray(), GCHandleType.Pinned);
            IntPtr p = Marshal.AllocHGlobal((int)Pcm.Length);
            Marshal.Copy(Pcm.ToArray(), 0, p, (int)Pcm.Length);
            NativeBuffers.AddLast(new NativeBufferPair() {handle = dataHandle, array = p});

            return p;
        }

        private static Wrapper instance { get; set; }
        private IEngine Engine { get; set; }
        private BufferSizeHandler BufferSize { get; set; }
        private BufferHandler Buffer { get; set; }
        private LinkedList<NativeBufferPair> NativeBuffers { get; set; }
        
        #endregion

    }

}
