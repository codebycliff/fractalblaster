using System;
using System.IO;

namespace Common
{
    public interface IDecode
    {
        Decoder Generate(string filepath);
    }

    public abstract class Decoder
    {
        public Decoder(string filepath) { }


        #region IDecode Members

        public abstract void Open();
        public abstract void Close();
        public abstract void SeekBeginning();
        public abstract MemoryStream ReadFrame(int numFramesToRead);

        #endregion
    }
}