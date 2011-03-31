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
            currentIndex = -1;
        }

        #region IPlaylist Members

        public List<MediaFile> getList()
        {
            return currentList;
        }

        public MediaFile getCurrent()
        {
            if (currentIndex == -1)
            {
                return null;
            }
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
            if (currentIndex == -1)
            {
                currentIndex = 0;
            }
            currentList.Add(f);
            mPlaylistForm.form.Refresh();
        }

        public IPlaylistForm playlistForm
        {
            set { mPlaylistForm = value; }
        }

        public void clear()
        {
            currentList.Clear();
        }

        public void selectIndex(int i)
        {
            if ((i < 0) || (i >= currentList.Count))
            {
                throw new IndexOutOfRangeException();
            }
            currentIndex = i;
        }

        public void selectNext()
        {
            if (currentIndex < currentList.Count - 1)
            {
                currentIndex++;
            }
        }

        public void selectPrevious()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
        }

        public int getCurrentIndex()
        {
            return currentIndex;
        }
        
        #endregion
    }
}
