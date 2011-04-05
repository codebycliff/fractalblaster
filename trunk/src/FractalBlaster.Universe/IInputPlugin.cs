using System;
using System.IO;

namespace FractalBlaster.Universe {
    
    /// <remarks>
    /// Defines the members for any plugin wishing to handle the input or
    /// decoding of a media file.
    /// </remarks>
    public interface IInputPlugin : IPlugin {
        
        /// <summary>
        /// Handles the opening of a media file and analyzing it.
        /// </summary>
        /// <param name="media">The media file to open and analyze.</param>
        void OpenMedia(MediaFile media);
        
        /// <summary>
        /// Closes the media file that was previously opened by this input
        /// plugin.
        /// </summary>
        void CloseMedia();
        
        /// <summary>
        /// Seeks to the location in the file specified by the number of seconds
        /// </summary>
        void Seek(Int32 Seconds);

        /// <summary>
        /// Reads or decodes the specified number of frames from the media file
        /// currently beining inputted / decoded and returns the frames as a 
        /// memory stream.
        /// </summary>
        /// <param name="numFramesToRead">
        /// The number of frames to be read from the media file.
        /// </param>
        /// <returns>
        /// Memory stream representing the frames that were read or decoded from
        /// the current media file.
        /// </returns>
        MemoryStream ReadFrames(Int32 numFramesToRead);

    }

}