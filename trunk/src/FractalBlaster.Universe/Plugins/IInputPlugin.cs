using System;
using System.IO;

namespace FractalBlaster.Universe {
    
    /// <remarks>
    /// Defines the members for any plugin wishing to handle the input or
    /// decoding of a media file.
    /// </remarks>
    public interface IInputPlugin {
        
        /// <summary>
        /// Handles the opening of a media file from the specified path.
        /// </summary>
        /// <param name="path">
        /// The path to the file to be opened as a <see cref="MediaFile"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="MediaFile"/> that is ready to be 
        /// decoded by this plugin.
        /// </returns>
        MediaFile OpenMedia(String path);
        
        /// <summary>
        /// Closes the media file that was previously opened by this input
        /// plugin.
        /// </summary>
        void CloseMedia();
        
        /// <summary>
        /// Seeks to the beginning of the media file currently being inputted
        /// or decoded by this input plugin.
        /// </summary>
        void SeekBeginning();
        
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