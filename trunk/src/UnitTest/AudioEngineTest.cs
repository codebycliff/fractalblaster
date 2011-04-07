using FractalBlaster.Core.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        /// Uses the Core to load a media file
        /// File will be verified by checking ID3 / Other embedded tag information
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            ///AppContext ctx = null; // TODO: Initialize to an appropriate value
            //AudioEngine target = new AudioEngine(ctx); // TODO: Initialize to an appropriate value
            //MediaFile file = null; // TODO: Initialize to an appropriate value
            //target.Load(file);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
