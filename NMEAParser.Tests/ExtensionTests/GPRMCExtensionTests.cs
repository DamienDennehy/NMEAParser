using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMEAParser.NMEA0183;
using NMEAParser.Extensions;

namespace NMEAParser.Tests.ExtensionTests
{
    [TestClass]
    public class GPRMCExtensionTests
    {
        [TestMethod]
        public void ToLatLon_WithRoute_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();
            List<GPRMC> points = new List<GPRMC>();
            List<LatLon> latLons = null;

            points.Add((GPRMC)parser.ParseSentence(sentence));
            points.Add((GPRMC)parser.ParseSentence(sentence));
            points.Add((GPRMC)parser.ParseSentence(sentence));

            latLons = points.ToLatLon();

            Assert.AreEqual(points.Count, latLons.Count);
        }
    }
}
