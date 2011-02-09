using System;

namespace Common
{
    public struct AudioMetadata
    {
        public string Artist;
        public string Title;
        public string Album;
        public string Codec;

        public int TrackNum;
        public int Year;
        public int Channels;
        public int SampleRate;
        public int BitRate;

        public TimeSpan Duration;
    }
}