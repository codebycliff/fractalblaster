using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FractalBlaster.Universe
{

    public interface ILibrary : IPlugin
    {

        /// <summary>
        /// The directory that contains the songs this Library was loaded from.
        /// </summary>
        DirectoryInfo Root { get; }

        /// <summary>
        /// The number of songs this Library contains.
        /// </summary>
        Int32 ItemCount { get; }

        /// <summary>
        /// A list of the names of all different artists this Library contains.
        /// </summary>
        IEnumerable<String> Artists { get; }

        /// <summary>
        /// A list of the titles of all different albums this Library contains.
        /// </summary>
        IEnumerable<String> Albums { get; }

        /// <summary>
        /// A list of all songs contained in this Library, sorted by when they were added.
        /// </summary>
        IEnumerable<MediaFile> AllMedia { get; }

        /// <summary>
        /// A method to accomodate the tree view structure of viewing the Library.
        /// </summary>
        /// <param name="key">The name of the artist to search for.</param>
        /// <returns>
        /// Returns a Dictionary containing a mapping of all album names by
        /// the given artist to a list of the songs in that album.
        /// </returns>
        Dictionary<String, List<MediaFile>> this[String key] { get; }

        /// <summary>
        /// Serializes the Library into an XML file.
        /// </summary>
        void Save();

        /// <summary>
        /// Tries to load/reload all of the mp3 files in the system defined
        /// music folder into the Library.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Searches the Library for a list of all songs from a given artist.
        /// </summary>
        /// <param name="artist">The name of an Artist</param>
        /// <returns>A list of all songs who's ID3 tags have the given artist name.</returns>
        IEnumerable<MediaFile> MediaForArtist(String artist);

        /// <summary>
        /// Searches the Library for a list of all songs from a given artist's album.
        /// </summary>
        /// <param name="artist">The name of an Artist.</param>
        /// <param name="album">The title of an album</param>
        /// <returns>A list of all the songs on the given album from the given artist</returns>
        IEnumerable<MediaFile> MediaForAlbum(String artist, String album);

        /// <summary>
        /// Lists all the songs contained in this Library in ascending order for a given ID3 tag.
        /// </summary>
        /// <param name="columnname">The name of an ID3 tag by which to sort.</param>
        /// <returns>A list of all songs sorted in ascending order by given tag.</returns>
        IEnumerable<MediaFile> Sort(String columnname);

        /// <summary>
        /// Lists all the songs contained in this Library by a given order and tag.
        /// </summary>
        /// <param name="columnname">The name of an ID3 tag by which to sort.</param>
        /// <param name="order">Either ASC for ascending order or DESC for descending.</param>
        /// <returns>A list of all songs sorted in the given order by the given tag.</returns>
        IEnumerable<MediaFile> Sort(String columnname, String order);

        /// <summary>
        /// Searches all songs over a given ID3 tag for those containing a given search term.
        /// </summary>
        /// <param name="columnname">The ID3 tag that will contain the search term.</param>
        /// <param name="searchterm">The term you wish to search for.</param>
        /// <returns>A list of all songs that have the given search term somewhere in the ID3 tag given, sorted in ascending order.</returns>
        IEnumerable<MediaFile> Search(String columnname, String searchterm);

        /// <summary>
        /// Searches all songs over a given ID3 tag for those containing a given search term.
        /// </summary>
        /// <param name="columnname">The ID3 tag that will contain the search term.</param>
        /// <param name="searchterm">The term you wish to search for.</param>
        /// <param name="order">Either ASC for ascending order or DESC for descending.</param>
        /// <returns>A list of all songs that have the given search term somewhere in the ID3 tag given, sorted in the given order.</returns>
        IEnumerable<MediaFile> Search(String columnname, String searchterm, String order);

        /// <summary>
        /// Searches all songs over a given ID3 tag for those which match a given search term.
        /// </summary>
        /// <param name="columnname">The ID3 tag that will contain the search term.</param>
        /// <param name="searchterm">The term you wish to search for.</param>
        /// <returns>A list of all songs whose given ID3 tag are equal to the search term</returns>
        IEnumerable<MediaFile> SearchStrict(String columnname, String searchterm);

        /// <summary>
        /// Searches all songs over a given ID3 tag for those which match a given search term.
        /// </summary>
        /// <param name="columnname">The ID3 tag that will contain the search term.</param>
        /// <param name="searchterm">The term you wish to search for.</param>
        /// <param name="order">Either ASC for ascending order or DESC for descending.</param>
        /// <returns>A list of all songs whose given ID3 tag are equal to the search term, sorted in the given order.</returns>
        IEnumerable<MediaFile> SearchStrict(String columnname, String searchterm, String order);

    }
}
