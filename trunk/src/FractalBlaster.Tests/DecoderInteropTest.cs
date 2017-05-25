using FractalBlaster.Plugins.Decoder.FFMPEG;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTest
{
    
    
    /// <summary>
    ///This is a test class for DecoderInterop and is intended
    ///to contain all DecoderInterop Unit Tests
    ///</summary>
    [TestClass()]
    public class DecoderInteropTest
    {
        /// <summary>
        ///A test for RetrieveNextFrame, determines if the decoder is extracting PCM data from the audio resource
        ///</summary>
        [TestMethod()]
        public void RetrieveNextFrameTest()
        {
            // Initialize Decoder
            DecoderInterop target = new DecoderInterop();

            // Load an MP3 that conforms to the MP3 Spec
            target.OpenMedia(new FractalBlaster.Universe.MediaFile(Directory.GetCurrentDirectory() + @"\Popcorn.mp3"));

            // Retrieve PCM data from the decoder
            byte[] actual = target.RetrieveNextFrame();

            // Failure from the decoder will result in null being returned, 
            // if we have data the decoder is operating properly
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for ReadFrames, ensures the total number of audio frames read is correct given the file length
        ///</summary>
        [TestMethod()]
        public void ReadFramesTest()
        {
            // Initialize Decoder
            DecoderInterop target = new DecoderInterop();

            // Load an MP3 that conforms to the MP3 Spec
            target.OpenMedia(new FractalBlaster.Universe.MediaFile(Directory.GetCurrentDirectory() + @"\Popcorn.mp3"));

            // Read all audio frames
            MemoryStream actual = null;
            Int64 bytesRead = 0;
            do
            {
                actual = target.ReadFrames(1);
                if(actual != null)
                    bytesRead += actual.Length;
            }
            while (actual != null);

            // Popcorn has a 2:27 runtime
            // Allow 1 second variance

            // 147 seconds * 2 channels * 2 bytes per sample * 44100 samples/second
            Int64 bytesExpected = 147 * 2 * 2 * 44100;

            // 147 seconds +/- 1 second
            double varianceValue = bytesExpected * (1.0 / 147.0);

            Boolean pass;

            if(Math.Abs(bytesExpected - bytesRead) <= varianceValue)
            {
                pass = true;
            }
            else
            {
                pass = false;
            }

            Assert.IsTrue(pass);
        }
    }
}
