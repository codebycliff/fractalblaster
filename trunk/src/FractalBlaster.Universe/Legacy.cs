using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FractalBlaster.Universe.Legacy {


    public interface IDLL {
        Type GetGenericType();
    }

    public interface IDecode {
        Decoder Generate(string filepath);
    }

    public interface IOutput {
        Output Generate();
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

    public abstract class Output {
        public abstract void Open(string path);
        public abstract void Play();
        public abstract void Stop();
        public abstract void Pause();
        public abstract void Resume();
    }

}
