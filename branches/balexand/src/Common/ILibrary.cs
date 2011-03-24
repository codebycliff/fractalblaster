using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe
{

    public class LibraryFilter
    {
        string ArtistName;
        string AlbumName;
        string SongName;

        public LibraryFilter(string artist, string album, string song)
        {
            ArtistName = artist;
            AlbumName = album;
            SongName = song;
        }
    }

    public interface ILibrary : IPlugin
    {
        List<string> getArtistList(LibraryFilter f);
        List<string> getAlbumList(LibraryFilter f);
        List<string> getSongList(LibraryFilter f);
    }
}
