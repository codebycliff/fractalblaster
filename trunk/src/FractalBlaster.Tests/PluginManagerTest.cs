﻿using FractalBlaster.Core.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FractalBlaster.Universe;
using System.Collections.Generic;
using FractalBlaster.Core.Runtime;
using System.IO;

namespace UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PluginManagerTest and is intended
    ///to contain all PluginManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PluginManagerTest
    {

        /// <summary>
        ///A test for AllPlugins, ensures the proper loading occurs
        ///</summary>
        [TestMethod()]
        public void AllPluginsTest()
        {
            int ManagedDLLs = 0;
            // All plugin types possible, they need counters

            string[] dlls = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Plugins\");

            int Total = 0;

            foreach (string s in dlls)
            {
                if (s.Contains("dll"))
                    Total++;
            }

            foreach (IPlugin p in FamilyKernel.Instance.Context.DefaultPlugins)
            {
                ManagedDLLs++;
            }

            Assert.IsTrue(ManagedDLLs >0);
        }
    }
}
