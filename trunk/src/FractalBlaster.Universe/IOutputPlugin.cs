using System;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Defines the members that must implemented by a plugin that wishes
    /// to handle the output / playing of a <see cref="MediaFile"/>.
    /// </remarks>
    public interface IOutputPlugin : IPlugin {

        /// <summary>
        /// Boolean value representing whether the media file that is loaded 
        /// in the running <see cref="IEngine"/> instance is currently being 
        /// outputted by this output plugin.
        /// </summary>
        Boolean IsPlaying { get; }

        /// <summary>
        /// Boolean value representing whether the media file that is loaded 
        /// in the running <see cref="IEngine"/> instance is currently paused
        /// by this output plugin.
        /// </summary>
        Boolean IsPaused { get; }

        /// <summary>
        /// Method that can be called to begin outputting the media file that 
        /// is currently loaded in the running <see cref="IEngine"/> instance.
        /// </summary>
        void Play();

        /// <summary>
        /// Method that can be called to stop outputting the media file that 
        /// is currently loaded in the running <see cref="IEngine"/> instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Method that can be called to pause the outputting of the media file 
        /// that is currently loaded in the running <see cref="IEngine"/> 
        /// instance.
        /// </summary>
        void Pause();

        /// <summary>
        /// Method that can be called to resume outputting of a previously
        /// paused media file that is currently loaded in the running 
        /// <see cref="IEngine"/> instance.
        /// </summary>
        void Resume();

    }

}