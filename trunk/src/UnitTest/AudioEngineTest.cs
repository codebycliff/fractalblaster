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

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        /// Uses the Core to load a media file
        /// File will be verified by checking ID3 / Other embedded tag information
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            AudioEngine target = new AudioEngine(new AppContext());
            MediaFile song = new MediaFile(Directory.GetCurrentDirectory() + @"\Popcorn.mp3");
            Metadata songData = target.Load(song);
            Assert.AreEqual(songData.Artist, "WHEEEEEEEEEEEEEE");
            Assert.AreEqual(songData.Album, "It's a series of concurent tests!");
            Assert.AreEqual(songData.BitRate, "Fill with the correct information.");
            Assert.AreEqual(songData.Channels, "Also, I know there's a problem with the load.");
            Assert.AreEqual(songData.Duration, "And you could probably do this all in like 5 seconds.");
            Assert.AreEqual(songData.Keys, "Tests.");
            Assert.AreEqual(songData.SampleRate, "Now that I think about it, not all of these should be assert equals.");
            Assert.AreEqual(songData.Title, "But I'm tired, and I'll do it tomorrow.");
            Assert.AreEqual(songData.Track, "Sleep...");
            Assert.AreEqual(songData.Values, "Zzzz");
            Assert.AreEqual(songData.Year, "ZZzzzzzz");
        }
    }
}
