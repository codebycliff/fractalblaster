using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FractalBlaster.Core.Runtime;
using FractalBlaster.Plugins.AudioOut;
using FractalBlaster.Plugins.Decoder.FFMPEG;
using FractalBlaster.Universe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{


    /// <summary>
    ///This is a test class for PlaybackStateMachine and is intended
    ///to contain all PlaybackStateMachine Unit Tests
    ///Specifically this excercises the UI buttons that control playback
    ///</summary>
    [TestClass()]
    public class PlaybackStateMachineTest
    {
        /// <summary>
        ///A test for Pause
        ///</summary>
        [TestMethod()]
        public void PauseTest()
        {
            // Hook up plugins and state machine
            IOutputPlugin target = FamilyKernel.Instance.Context.DefaultPlugins.OfType<IOutputPlugin>().First();
            FamilyKernel.Instance.Context.Engine.Load(new MediaFile(Directory.GetCurrentDirectory() + @"\Popcorn.mp3"));

            // Initial State = Stopped

            target.Stop(); //making sure the inital state is stopped.
            // Stopped -> Playing -> Pause (Expecting Paused)
            target.Play();
            target.Pause();

            Assert.IsTrue(target.IsPaused);

        }

        /// <summary>
        ///A test for Play
        ///</summary>
        [TestMethod()]
        public void PlayTest()
        {
            // Hook up plugins and state machine
            IOutputPlugin target = FamilyKernel.Instance.Context.DefaultPlugins.OfType<IOutputPlugin>().First();
            FamilyKernel.Instance.Context.Engine.Load(new MediaFile(Directory.GetCurrentDirectory() + @"\Popcorn.mp3"));

            // Inital state = stopped
            // Since playing "overrides" pause and stop, it should always play something, unless it's already playing. Then it does nothing.

            target.Stop(); //making sure that the inital state is stop
            // Stopped -> Play -> Stop -> Play (Expect playing)
            target.Play();
            target.Stop();
            target.Play();
            Assert.IsTrue(target.IsPlaying);

            target.Stop();
            // Stopped -> Play -> Play (Expecting playing)
            target.Play();
            target.Play();
            Assert.IsTrue(target.IsPlaying);

            target.Stop();
            // Stopped -> Play -> Pause -> Play (Expecting Playing)
            target.Play();
            target.Pause();
            target.Play();
            Assert.IsTrue(target.IsPlaying);

            target.Stop();
            // Stopped -> Pause -> Stop -> Play (Expecting Playing)
            target.Pause();
            target.Stop();
            target.Play();
            Assert.IsTrue(target.IsPlaying);
        }

        /// <summary>
        ///A test for Resume
        ///</summary>
        [TestMethod()]
        public void ResumeTest()
        {
            // Hook up plugins and state machine
            IOutputPlugin target = FamilyKernel.Instance.Context.DefaultPlugins.OfType<IOutputPlugin>().First();
            FamilyKernel.Instance.Context.Engine.Load(new MediaFile(Directory.GetCurrentDirectory() + @"\Popcorn.mp3"));

            //Inital state = stopped

            target.Stop();
            // Stopped -> Playing -> Pause -> Playing   (Expecting Playing)
            target.Play();
            target.Pause();
            target.Play();

            Assert.IsTrue(target.IsPlaying);

            target.Stop();
            // Stopped -> Pause -> Pause (Expecting stopped)
            target.Pause();
            target.Pause();

            Assert.IsTrue(target.IsPlaying != true);


        }

        /// <summary>
        ///A test for Stop
        ///</summary>
        [TestMethod()]
        public void StopTest()
        {
            // Hook up plugins and state machine
            IOutputPlugin target = FamilyKernel.Instance.Context.DefaultPlugins.OfType<IOutputPlugin>().First();
            FamilyKernel.Instance.Context.Engine.Load(new MediaFile(Directory.GetCurrentDirectory() + @"\Popcorn.mp3"));

            // Inital state = stopped 
            target.Stop();


            target.Stop();
            //Stopped -> play -> stop (Expect stopped)
            target.Play();
            target.Stop();
            Assert.IsTrue(target.IsPlaying != true);

            target.Stop();
            //Stopped -> pause -> play -> stop (Expect stopped)
            target.Pause();
            target.Play();
            target.Stop();
            Assert.IsTrue(target.IsPlaying != true);

            target.Stop();
            //Stopped -> play -> pause -> stop (Expecting stopped)
            target.Play();
            target.Pause();
            target.Stop();
            Assert.IsTrue(target.IsPlaying != true);
        }
    }
}
