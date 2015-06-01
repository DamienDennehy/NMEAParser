//This file is part of NMEA Parser.

//NMEA Parser is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//NMEA Parser is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with NMEA Parser.  If not, see <http://www.gnu.org/licenses/>.

//Copyright 2015 Damien Dennehy.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMEAParser.NMEA0183;
using NMEAParser.Extensions;
using System.Threading;
using System.Globalization;

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
