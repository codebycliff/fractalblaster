using System;

namespace FractalBlaster.Core {

    public interface IOutputPlugin : IPlugin {

        void Play();

        void Stop();

        void Pause();

        void Resume();

    }

}