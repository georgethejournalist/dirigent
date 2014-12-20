﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Dirigent.Common;
using Dirigent.Agent.Core;

namespace Dirigent.Agent.Core
{
    [TestFixture]
    public class InitDetectorFactoryTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void testFactoryCreatesTimeOut()
        {
            AppDef appDef = new AppDef();
            AppState appState = new AppState();

            var f = new AppInitializedDetectorFactory();

            IAppInitializedDetector d = f.create( appDef, appState, 0, "timeout 0.1" );
            
            Assert.AreEqual(typeof(TimeOutInitDetector), d.GetType(), "correct detector type created");
        }

        [Test]
        [ExpectedException(typeof(UnknownAppInitDetectorType))]
        public void testFactoryFailsForUnknownType()
        {
            AppDef appDef = new AppDef();
            AppState appState = new AppState();

            var f = new AppInitializedDetectorFactory();

            var d = f.create( appDef, appState, 0, "unknown any-params" );
        }
    }
}
