using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace FractalBlaster.Universe {

    /// <remarks>
    /// Class that represents a list (or collection) of media files and 
    /// provides operations for working with the collection as a whole,
    /// including serialization support.
    /// </remarks>
    public class Playlist : IEnumerable<MediaFile> {

        /// <summary>
        /// Event that is fired whenever the selected media file is changed.
        /// </summary>
        public event EventHandler SelectedChanged;

        /// <summary>
        /// Event that is fired whenever there is a request to play a media 
        /// file.
        /// </summary>
        public event MediaChangeHandler MediaRequested;

        /// <summary>
        /// Event that is fired whenever a media file is added to this 
        /// playlist.
        /// </summary>
        public event MediaChangeHandler MediaAdded;

        /// <summary>
        /// Enumeration of media files that represents the playlist's items.
        /// </summary>
        public IEnumerable<MediaFile> Items { get { return MediaItems.AsEnumerable();  } }

        /// <summary>
        /// The Title of the playlist.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// The currently selected index in the playlist. This represents a selection,
        /// not a double-click or request to play the item.
        /// </summary>
        public Int32 SelectedIndex {
            get {
                return SelectionIndex;
            }
            set {
                SelectionIndex = value;
                if (SelectedChanged != null) {
                    SelectedChanged(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Constructor that creates an empty playlist initialized with an
        /// empty collection of media files.
        /// </summary>
        public Playlist() {
            MediaItems = new List<MediaFile>();
            SelectionIndex = 0;
        }

        /// <summary>
        /// Method for adding the specified media file to the collection of
        /// media files that this playlist represents.
        /// </summary>
        /// <param name="media">
        /// The media file to be added to this playlist.
        /// </param>
        public void AddItem(MediaFile media) {
            MediaItems.Add(media);
            if (MediaAdded != null) {
                MediaAdded(media);
            }
        }

        /// <summary>
        /// Adds the media file at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="media">The media.</param>
        public void AddItem(int index, MediaFile media)
        {
            MediaItems.Insert(index, media);
            if (MediaAdded != null)
            {
                MediaAdded(media);
            }
        }

        /// <summary>
        /// Moves the media file at the from index to the to index.
        /// </summary>
        /// <param name="fromIndex">From index.</param>
        /// <param name="toIndex">To index.</param>
        public void MoveItem(int fromIndex, int toIndex) {
            MediaFile removed = MediaItems[fromIndex];
            MediaItems.RemoveAt(fromIndex);
            MediaItems.Insert(toIndex, removed);
        }

        /// <summary>
        /// Requests the media file at the specified index to be played.
        /// </summary>
        /// <param name="index">
        /// The index of the media file in the playlist.
        /// </param>
        public void RequestMediaAt(Int32 index) {
            if (MediaRequested != null && !(index >= MediaItems.Count) && (index >= 0)) {
                SelectedIndex = index;
                MediaRequested(MediaItems[index]);
            }
        }

        /// <summary>
        /// Removes the media at the specified index from the playlist.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public MediaFile RemoveMedia(int index)
        {
            MediaFile removed = MediaItems[index];
            MediaItems.RemoveAt(index);
            return removed;
        }

        /// <summary>
        /// Sorts the playlist using specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public void Sort(Comparison<MediaFile> comparer)
        {
            MediaItems.Sort(comparer);
        }

        #region [ IEnumerable ]

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MediaFile> GetEnumerator() {
            return MediaItems.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return MediaItems.GetEnumerator();
        }

        #endregion

        #region [ Private ]

        /// <summary>
        /// The currently selected index in the playlist.
        /// </summary>
        private Int32 SelectionIndex { get; set; }

        /// <summary>
        /// List of media files contained in this playlist.
        /// </summary>
        private List<MediaFile> MediaItems { get; set; }

        #endregion    
     
    }

}
