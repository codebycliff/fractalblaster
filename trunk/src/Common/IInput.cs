using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FractalBlaster.Universe
{
    public interface IInput : IPlugin
    {
        void Open(string filepath);
        void Seek(int seconds);
        MemoryStream GetFrames(int numFramesToRead);
        void Close();
    }


    public class InvalidFilePathException : ApplicationException
    {
        public InvalidFilePathException() { }
        public InvalidFilePathException(string message) { }
        public InvalidFilePathException(string message, System.Exception inner) { }

        protected InvalidFilePathException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

}
