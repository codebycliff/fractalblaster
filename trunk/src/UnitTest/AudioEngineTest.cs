using FractalBlaster.Core.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using FractalBlaster.Universe;

namespace UnitTest
{
    
    
    /// <summary>
    ///This is a test class for AudioEngineTest and is intended
    ///to contain all AudioEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AudioEngineTest
    {
        /// <summary>
        /// Uses the Core to load a media file
        /// File will be verified by checking ID3 / Other embedded tag information
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            MediaFile song = new MediaFile(Directory.GetCurrentDirectory() + @"\Popcorn.mp3");
            Assert.AreEqual(song.Metadata.Album, "Futurism");
            Assert.AreEqual(song.Metadata.Artist, "Muse");
            Assert.AreEqual(song.Metadata.BitRate, 64); //Check
            Assert.AreEqual(song.Metadata.Channels, 2); //Check. I'm pretty sure this is 2, as it's sterio
            Assert.AreEqual(song.Metadata.Duration, new TimeSpan(0,2,20));
            Assert.AreEqual(song.Metadata.SampleRate, 32); //Check
            Assert.AreEqual(song.Metadata.Title, "Popcorn");
            Assert.AreEqual(song.Metadata.Track, 4); //Check
            Assert.AreEqual(song.Metadata.Year, 2003); //check 
        }
    }
}
