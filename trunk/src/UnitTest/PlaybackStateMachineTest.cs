using FractalBlaster.Core.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FractalBlaster.Universe;

namespace UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PlaybackStateMachineTest and is intended
    ///to contain all PlaybackStateMachineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PlaybackStateMachineTest
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
        ///A test for Pause
        ///</summary>
        [TestMethod()]
        public void PauseTest()
        {
            IOutputPlugin output = null; // TODO: Initialize to an appropriate value
            IInputPlugin input = null; // TODO: Initialize to an appropriate value
            PlaybackStateMachine target = new PlaybackStateMachine(output, input); // TODO: Initialize to an appropriate value
            target.Pause();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Play
        ///</summary>
        [TestMethod()]
        public void PlayTest()
        {
            IOutputPlugin output = null; // TODO: Initialize to an appropriate value
            IInputPlugin input = null; // TODO: Initialize to an appropriate value
            PlaybackStateMachine target = new PlaybackStateMachine(output, input); // TODO: Initialize to an appropriate value
            target.Play();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Resume
        ///</summary>
        [TestMethod()]
        public void ResumeTest()
        {
            IOutputPlugin output = null; // TODO: Initialize to an appropriate value
            IInputPlugin input = null; // TODO: Initialize to an appropriate value
            PlaybackStateMachine target = new PlaybackStateMachine(output, input); // TODO: Initialize to an appropriate value
            target.Resume();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Stop
        ///</summary>
        [TestMethod()]
        public void StopTest()
        {
            IOutputPlugin output = null; // TODO: Initialize to an appropriate value
            IInputPlugin input = null; // TODO: Initialize to an appropriate value
            PlaybackStateMachine target = new PlaybackStateMachine(output, input); // TODO: Initialize to an appropriate value
            target.Stop();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
