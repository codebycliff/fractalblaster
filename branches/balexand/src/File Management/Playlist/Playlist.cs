using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalBlaster.Universe;


namespace FractalBlaster.Playlist
{
    class Playlist : IPlaylist
    {
        List<MediaFile> currentList;
        int currentIndex;
        IPlaylistForm mPlaylistForm;

        public Playlist()
        {
            currentList = new List<MediaFile>();
        }

        #region IPlaylist Members

        public List<MediaFile> getList()
        {
            return currentList;
        }

        public MediaFile getCurrent()
        {
            return currentList.ElementAt(currentIndex);
        }

        public MediaFile getNext()
        {
            if (currentIndex < currentList.Count - 1)
            {
                return currentList.ElementAt(currentIndex + 1);
            }
            return null;
        }

        public MediaFile getPrevious()
        {
            if (currentIndex > 0)
            {
                return currentList.ElementAt(currentIndex - 1);
            }
            return null;
        }

        public MediaFile getIndex(int i)
        {
            if ((i > 0) && (i < currentList.Count))
            {
                return currentList.ElementAt(i);
            }
            return null;
        }

        public void add(MediaFile f)
        {
            currentList.Add(f);
            mPlaylistForm.form.Refresh();
        }

        public IPlaylistForm playlistForm
        {
            set { mPlaylistForm = value; }
        }
        
        #endregion
    }
}
