using FractalBlaster.Plugins.Decoder.FFMPEG;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTest
{
    
    
    /// <summary>
    ///This is a test class for DecoderInteropTest and is intended
    ///to contain all DecoderInteropTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DecoderInteropTest
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for RetrieveNextFrame
        ///</summary>
        [TestMethod()]
        public void RetrieveNextFrameTest()
        {
            DecoderInterop target = new DecoderInterop(); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual = null;
            //actual = target.RetrieveNextFrame();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ReadFrames
        ///</summary>
        [TestMethod()]
        public void ReadFramesTest()
        {
            DecoderInterop target = new DecoderInterop(); // TODO: Initialize to an appropriate value
            target.OpenMedia(new FractalBlaster.Universe.MediaFile(@"C:\Users\David\Desktop\The Killers\The Killers - Day & Age (2008)\01 - Losing Touch.mp3"));
            int numFramesToRead = 10; // TODO: Initialize to an appropriate value
            MemoryStream expected = null; // TODO: Initialize to an appropriate value
            MemoryStream actual;
            actual = target.ReadFrames(numFramesToRead);
            Assert.AreNotEqual(expected, actual);
        }
    }
}
