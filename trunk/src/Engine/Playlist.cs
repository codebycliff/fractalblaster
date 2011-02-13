using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

using Common;

namespace Engine
{
    class Playlist
    {
        private DataTable songlist;

        public Playlist()
        {
            songlist = new DataTable();

            DataColumn dc;

            foreach (KeyValuePair<String, Type> pair in Metadata.TAGS)
            {
                dc = new DataColumn();

                dc.ColumnName = pair.Key;
                dc.DataType = pair.Value;

                songlist.Columns.Add(dc);
            }
        }

        private DataRow convert(AudioMetadata song)
        {
            DataRow dr = songlist.NewRow();

            dr["Artist"] = song.Artist;
            dr["Title"] = song.Title;
            dr["Album"] = song.Album;
            dr["Codec"] = song.Codec;
            dr["File"] = song.File;

            dr["TrackNum"] = song.TrackNum;
            dr["Year"] = song.Year;
            dr["Channels"] = song.Channels;
            dr["SampleRate"] = song.SampleRate;
            dr["BitRate"] = song.BitRate;

            dr["Duration"] = song.Duration;

            return dr;
        }

        private AudioMetadata convert(DataRow row)
        {
            AudioMetadata song = new AudioMetadata();

            song.Artist = (String) row["Artist"];
            song.Title = (String) row["Title"];
            song.Album = (String) row["Album"];
            song.Codec = (String) row["Codec"];
            song.File = (String) row["File"];

            song.TrackNum = (int) row["TrackNum"];
            song.Year = (int) row["Year"];
            song.Channels = (int) row["Channels"];
            song.SampleRate = (int) row["SampleRate"];
            song.BitRate = (int) row["BitRate"];

            song.Duration = (TimeSpan) row["Duration"];

            return song;
        }

        public void addSong(AudioMetadata song)
        {
            DataRow dr = this.convert(song);

            songlist.Rows.Add(dr);
        }

        public void ExportOnClose()
        {
            songlist.WriteXml("library.xml");
        }

        public void ImportOnStart()
        {
            if (File.Exists("library.xml"))
            {
                songlist.ReadXml("library.xml");
            }
        }

        public AudioMetadata[] GetSongs(Boolean shuffle)
        {
            DataTable table = songlist.Clone();

            if (shuffle)
            {
                table.Columns.Add(new DataColumn("RandomNum", Type.GetType("System.Int32")));

                Random random = new Random();

                for (int i = 0; i < table.Rows.Count; i += 1)
                {
                    table.Rows[i]["RandomNum"] = random.Next(1, table.Rows.Count);
                }

                table.DefaultView.Sort = "RandomNum";
            }

            DataRow[] songs = songlist.Select();

            AudioMetadata[] output = new AudioMetadata[songs.Length];

            for (int i = 0; i < output.Length; i += 1)
            {
                output[i] = this.convert(songs[i]);
            }

            return output;
        }
    }
}
