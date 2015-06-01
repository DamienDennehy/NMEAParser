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

namespace NMEAParser.Tests.NMEA0183
{
    [TestClass]
    public class GPRMCTests
    {
        #region "ParseSentence Tests"

        [TestMethod]
        public void ParseSentence_Valid_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = (GPRMC)parser.ParseSentence(sentence);

            DateTime expectedDate = new DateTime(2010, 11, 23, 8, 47, 52, 000);
            double expectedLat = 51.873625;
            double expectedLon = -8.541805;
            double expectedBearing = 62.01;
            double expectedSpeed = 11.90;

            Assert.AreEqual(typeof(GPRMC), actual.GetType());
            Assert.AreEqual(expectedDate, actual.TimeStamp);
            Assert.AreEqual(expectedLat, actual.LatLon.Latitude);
            Assert.AreEqual(expectedLon, actual.LatLon.Longtitude);
            Assert.AreEqual(expectedBearing, actual.Bearing);
            Assert.AreEqual(expectedSpeed, actual.SpeedKnots);
            Assert.AreEqual(0, actual.MagVar);
        }

        [TestMethod]
        public void ParseSentence_InvalidChecksum_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void ParseSentence_InvalidDate_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,311110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void ParseSentence_InvalidTime_Pass()
        {
            string sentence = "$GPRMC,084752k00,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void ParseSentence_InvalidLatitude_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152k4175,N,00832.5083,W,11.90,62.01,231110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void ParseSentence_InvalidLatitudeHemisphere_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,X,00832.5083,W,11.90,62.01,231110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void ParseSentence_InvalidLongtitude_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832f5083,W,11.90,62.01,231110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void ParseSentence_InvalidLongtitudeHemisphere_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,X,11.90,62.01,231110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void ParseSentence_InvalidSpeed_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,x,62.01,231110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void ParseSentence_InvalidBearing_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,x,231110,,*13";

            GPRMCParser parser = new GPRMCParser();

            GPRMC actual = null;
            ArgumentException expected = null;

            try
            {
                actual = (GPRMC)parser.ParseSentence(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }
        #endregion

        #region "IsSentence Tests"
        [TestMethod]
        public void IsSentence_Valid_Pass()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsTrue(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidSentenceNullArgument_Fail()
        {
            string sentence = "";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidSentenceLength_Fail()
        {
            string sentence = "$GRMC,084752.000,A,5152.4";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidType_Fail()
        {
            string sentence = "$GRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidTimeCharacter_Fail()
        {
            string sentence = "$GPRMC,08475f.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidTimePrecision_Fail()
        {
            string sentence = "$GPRMC,400000.999,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidGPSSignalLock_Fail()
        {
            string sentence = "$GPRMC,08475.000,X,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidLatitudeCharacter_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,515j.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidLatitudePrecision_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,9999.4175,N,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidLatitudeHemisphere_Fail()
        {
            string sentence = "$GPRMC,08475.000,X,5152.4175,X,00832.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidLongtitudeCharacter_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,5152.4175,N,008j2.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidLongtitudePrecision_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,5152.4175,N,20000.5083,W,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidLongtitudeHemisphere_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,5152.4175,N,00832.5083,X,11.90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidSpeed_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,5152.4175,N,00832.5083,W,11.k90,62.01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidBearing_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,5152.4175,N,00832.5083,W,11.90,62.j01,231110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidDateCharacter_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,5152.4175,N,00832.5083,W,11.90,62.01,23i1110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }

        [TestMethod]
        public void IsSentence_InvalidDatePrecision_Fail()
        {
            string sentence = "$GPRMC,08475.000,A,5152.4175,N,00832.5083,W,11.90,62.01,991110,,*12";

            GPRMCParser parser = new GPRMCParser();

            Assert.IsFalse(parser.IsSentence(sentence));
        }
        #endregion
    }
}
