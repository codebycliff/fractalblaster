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

            // Stopped -> Playing -> Pause (Expecting Paused)

            target.Play();
            target.Pause();

            Assert.IsTrue(target.IsPaused);

            // Stopped -> Playing -> Stopped -> Pause (Expecting Stopped)
            // TODO

            // Stopped -> Playing -> Paused -> Pause (Excepting Playing)
            // TODO

            // Stopped -> Pause (Expecting Stopped)
            // TODO


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

            // Follow Example from Pause
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

            // Follow Example from Pause.

            // Should be always playing
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

            // Follow Example from Pause
        }
    }
}
