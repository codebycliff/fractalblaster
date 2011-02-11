using System;

namespace Common
{
    public interface IOutput
    {
        Output Generate();
    }

    public abstract class Output
    {
        public abstract void Open(string path);
        public abstract void Play();
        public abstract void Stop();
        public abstract void Pause();
        public abstract void Resume();
    }
}