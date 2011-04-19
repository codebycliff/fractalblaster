using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;

namespace FractalBlaster.Core.Runtime {

    /// <remarks>
    /// Class that acts as a decorator for the actual input plugin by 
    /// implementing <see cref="IInputPlugin"/> and delegating most of
    /// the method calls to the input plugin it wraps. This class is
    /// responsible for apply effects provided by effect plugins to the
    /// actual audio stream.
    /// </remarks>
    class EffectsProcessor : IInputPlugin {

        /// <summary>
        /// Gets an enumeration of the effects that are to be applied
        /// to the audio stream.
        /// </summary>
        public IEnumerable<IEffectPlugin> Effects { get; private set; }

        /// <summary>
        /// Gets the context the plugin was initialized with.
        /// </summary>
        public AppContext Context { get; private set; }

        /// <summary>
        /// Gets the input plugin for which this decorator is wrapping.
        /// </summary>
        public IInputPlugin InputStream { get; private set; }
        
        /// <summary>
        /// Constructor that takes in the input plugin that is actual
        /// responsible for the input of the audio and for which most
        /// method calls are delegated to.
        /// </summary>
        /// <param name="input">The input plugin this decorator wraps.</param>
        public EffectsProcessor(IInputPlugin input) {
            InputStream = input;
        }
        
        #region [ IInputPlugin Members ]

        /// <summary>
        /// Initializes the input plugin with the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Initialize(AppContext context) {
            Context = context;
            Effects = Context.AllPlugins.OfType<IEffectPlugin>();
        }

        /// <summary>
        /// Handles the opening of a media file and analyzing it.
        /// </summary>
        /// <param name="media">The media file to open and analyze.</param>
        public void OpenMedia(MediaFile media) {
            InputStream.OpenMedia(media);
        }

        /// <summary>
        /// Closes the media file that was previously opened by this input
        /// plugin.
        /// </summary>
        public void CloseMedia() {
            InputStream.CloseMedia();
        }

        /// <summary>
        /// Seeks the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public void Seek(int seconds) {
            InputStream.Seek(seconds);
        }

        /// <summary>
        /// Reads or decodes the specified number of frames from the media file
        /// currently beining inputted / decoded and returns the frames as a
        /// memory stream.
        /// </summary>
        /// <param name="numFramesToRead">The number of frames to be read from the media file.</param>
        /// <returns>
        /// Memory stream representing the frames that were read or decoded from
        /// the current media file.
        /// </returns>
        public MemoryStream ReadFrames(int numFramesToRead) {
            MemoryStream frames = InputStream.ReadFrames(numFramesToRead);
            if (frames == null)
            {
                return null;
            }
            foreach (IEffectPlugin effect in Effects.Where(e=>e.Enabled)) {
                frames.Seek(0, 0);
                effect.ProcessStream(frames);
            }
            frames.Seek(0, 0);
            return frames;
        }
        
        #endregion

    }

}
