using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FractalBlaster.Core.Legacy {

    public interface IDLL {
        Type GetGenericType();
    }

    public interface IDecode {
        Decoder Generate(string filepath);
    }

    public abstract class Decoder {
        public Decoder(string filepath) { }

        #region IDecode Members

        public abstract void Open();
        public abstract void Close();
        public abstract void SeekBeginning();
        public abstract MemoryStream ReadFrame(int numFramesToRead);

        #endregion
    }

    public interface IOutput {
        Output Generate();
    }

    public abstract class Output {
        public abstract void Open(string path);
        public abstract void Play();
        public abstract void Stop();
        public abstract void Pause();
        public abstract void Resume();
    }

    public class DLL : IDLL, IOutput {

        #region IDLL Members

        public Type GetGenericType() {
            return typeof(Output);
        }

        #endregion

        #region IOutput Members

        public Output Generate() {
            return new Wrapper();
        }

        #endregion
    }

    public static class DLLMaster {
        static List<IDLL> ValidDLL = new List<IDLL>();

        static DLLMaster() {
            List<Assembly> dlls = new List<Assembly>();
            List<Type> interfaces = new List<Type>();
            FileInfo[] files = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*.dll");

            // Get all DLLs
            for (int i = 0; i < files.Length; i++) {
                try {
                    dlls.Add(Assembly.LoadFile(files[i].FullName));
                    interfaces.AddRange(dlls.Last().GetTypes());
                }
                catch (BadImageFormatException) {
                    Console.WriteLine("Skipping Loading UnManaged DLL " + files[i].FullName);
                }
                catch (Exception e) {
                    Console.WriteLine("DLL Loading Exception: " + e.Message);
                }
            }

            // Filter on ones that implement this interface
            List<Type> dllList = interfaces.FindAll(delegate(Type t) {
                List<Type> dllTypes = new List<Type>(t.GetInterfaces());
                return dllTypes.Contains(typeof(IDLL));
            });

            ValidDLL = dllList.ConvertAll<IDLL>(delegate(Type t) { return Activator.CreateInstance(t) as IDLL; });
        }

        public static Decoder getDecoder(string filepath) {
            Decoder rval = null;

            foreach (IDLL dll in ValidDLL) {
                if (dll.GetGenericType() == typeof(Decoder)) {
                    rval = ((IDecode)dll).Generate(filepath);
                    break;
                }
            }

            // TODO: Perform check, if still null, we're missing a DLL, throw an exception

            return rval;
        }

        public static Output getOutput() {
            Output rval = null;

            foreach (IDLL dll in ValidDLL) {
                if (dll.GetGenericType() == typeof(Output)) {
                    rval = ((IOutput)dll).Generate();
                    break;
                }
            }

            // TODO: Perform check, if still null, we're missing a DLL, throw an exception

            return rval;
        }
    }

    public class Wrapper : Output {
        internal class AudioOut {
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
        Decoder a;
        BufferSize b;
        Buffer g;
        IntPtr stream;
        string filepath;

        struct NativeBufferPair {
            public GCHandle handle;
            public IntPtr array;
        }

        public Wrapper() {
            b = new BufferSize(getBufferSize);
            g = new Buffer(getBuffer);
            stream = new IntPtr();
        }

        LinkedList<NativeBufferPair> native = new LinkedList<NativeBufferPair>();

        public delegate int BufferSize();
        public delegate IntPtr Buffer(bool gc);

        public override void Open(string path) {
            filepath = path;
            if (a != null) {
                AudioOut.Stop();
                a.Close();
                // Clean up Remaining Native Arrays
                for (int i = 0; i < native.Count; i++) {
                    native.First.Value.handle.Free();
                    Marshal.FreeHGlobal(native.First.Value.array);
                    native.RemoveFirst();
                    GC.Collect();
                }
            }
            a = DLLMaster.getDecoder(path);
            a.Open();
            IntPtr wave = AudioOut.WaveInterfaceInstance();
        }

        public override void Play() {
            stream = AudioOut.CreateOutputStream(g, b, 2);
            AudioOut.ChangeOutputStream(stream);
        }

        public override void Stop() {
            AudioOut.Stop();
            a.SeekBeginning();

            // Clean up Remaining Native Arrays
            for (int i = 0; i < native.Count; i++) {
                native.First.Value.handle.Free();
                Marshal.FreeHGlobal(native.First.Value.array);
                native.RemoveFirst();
                GC.Collect();
            }
        }

        public override void Pause() {
            AudioOut.Pause();
        }

        public override void Resume() {
            AudioOut.UnPause();
        }

        public int getBufferSize() {
            if (pcm == null)
                return 0;
            return (int)pcm.Length;
        }

        public IntPtr getBuffer(bool gc) {
            if (gc) {
                native.First.Value.handle.Free();
                Marshal.FreeHGlobal(native.First.Value.array);
                native.RemoveFirst();
                GC.Collect();
            }
            pcm = a.ReadFrame(500);
            if (pcm == null) {
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
