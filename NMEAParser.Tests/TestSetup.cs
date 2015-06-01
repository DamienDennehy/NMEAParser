using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace NMEAParser.Tests
{
    [TestClass]
    public class TestSetup
    {
        [AssemblyInitialize()]
        public static void ClassInit(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
        }
    }
}
