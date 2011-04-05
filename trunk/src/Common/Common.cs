using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FractalBlaster.Universe {

    #region [ Delegates ]

    /// <summary>
    /// Delegate that represents a handler that is called when the currently
    /// running instance of <see cref="IEngine"/> changes the media being 
    /// outputted.
    /// </summary>
    /// <param name="file">
    /// The new media file.
    /// </param>
    public delegate void MediaChangeHandler(MediaFile file);

    /// <summary>
    /// Delegate that represents a handler that is called when the currently
    /// running instance of <see cref="IEngine"/> changes playlists.
    /// </summary>
    /// <param name="playlist">
    /// The new playlist.
    /// </param>
    public delegate void PlaylistChangeHandler(IPlaylist playlist);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public delegate Int32 BufferSizeHandler();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gc"></param>
    /// <returns></returns>
    public delegate IntPtr BufferHandler(Boolean gc);


    #endregion

    #region [ Structs ]

    /// <remarks>
    /// Structure the represents a set of items that are typically available
    /// as metadata for audio files.
    /// </remarks>
    public struct AudioMetadata {
        /// <summary>
        /// The artist of the audio file.
        /// </summary>
        public String Artist;
        /// <summary>
        /// The title of the audio file.
        /// </summary>
        public String Title;
        /// <summary>
        /// The album the audio file belongs to.
        /// </summary>
        public String Album;
        /// <summary>
        /// The audio file's codec.
        /// </summary>
        public String Codec;
        /// <summary>
        /// The year number with respect to the audio file's album.
        /// </summary>
        public Int32 TrackNumber;
        /// <summary>
        /// The year the audio file was released.
        /// </summary>
        public Int32 Year;
        /// <summary>
        /// The number of channels in the audio file.
        /// </summary>
        public Int32 Channels;
        /// <summary>
        /// The sample rate of the audio file.
        /// </summary>
        public Int32 SampleRate;
        /// <summary>
        /// The audio file's bit rate.
        /// </summary>
        public Int32 BitRate;
        /// <summary>
        /// The duration or length of the audio file.
        /// </summary>
        public TimeSpan Duration;
        /// <summary>
        /// The path where the audio file is located.
        /// </summary>
        public String Path;
    }

    /// <remarks>
    /// Structure representing a media file property (metadata item).
    /// </remarks>
    public struct MediaProperty {
        /// <summary>
        /// The name (key) of the media property.
        /// </summary>
        public String Name;
        /// <summary>
        /// The value of the media property.
        /// </summary>
        public Object Value;
        /// <summary>
        /// The type of the media property's value.
        /// </summary>
        public Type Type;
        /// <summary>
        /// Static method designed to provide a standard way of creating
        /// media properties that ensures that resulting structure contains all
        /// of the required values.
        /// </summary>
        /// <param name="name">The key of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="type">The type of the property's value.</param>
        /// <returns>
        /// MediaProperty structure with it's fields initialized to the 
        /// values passed in.
        /// </returns>
        public static MediaProperty Create(String name, Object value, Type type) {
            return new MediaProperty() {
                Name = name,
                Type = type,
                Value = value
            };
        }

    }

    #endregion

}
