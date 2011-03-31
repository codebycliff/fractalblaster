using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe
{
    public interface IPlaylist : IPlugin
    {
        List<MediaFile> getList();
        MediaFile getCurrent();
        MediaFile getNext();
        MediaFile getPrevious();
        MediaFile getIndex(int i);
        void add(MediaFile f);
        void clear();
        void selectIndex(int i);
        void selectNext();
        void selectPrevious();
        int getCurrentIndex();

        IPlaylistForm playlistForm
        {
            set;
        }

    }
}
