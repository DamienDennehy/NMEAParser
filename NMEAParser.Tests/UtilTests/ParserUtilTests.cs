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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMEAParser.Utils;
namespace NMEAParser.Tests.UtilTests
{
    [TestClass]
    public class ParserUtilTests
    {
        #region "GenerateChecksum Tests"

        [TestMethod]
        public void GenerateChecksum_NullInput_ThrowArgumentNullException()
        {
            ArgumentNullException expected = null;

            try
            {
                ParserUtil.GenerateChecksum(null);
            }
            catch (ArgumentNullException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentNullException));
        }

        [TestMethod]
        public void GenerateChecksum_InvalidInputNoDollarSign_ThrowArgumentException()
        {
            string sentence = "GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";
            ArgumentException expected = null;

            try
            {
                ParserUtil.GenerateChecksum(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }

        [TestMethod]
        public void GenerateChecksum_InvalidInputNoAsterixSign_ThrowArgumentException()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,12";
            ArgumentException expected = null;

            try
            {
                ParserUtil.GenerateChecksum(sentence);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }
        [TestMethod]
        public void GenerateChecksum_ValidInput_MatchChecksum()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";
            string actual = ParserUtil.GenerateChecksum(sentence);

            Assert.AreEqual("12", actual);
        }
        [TestMethod]
        public void GenerateChecksum_ValidInput_FailChecksum()
        {
            string sentence = "$GPRMC,084752.000,A,5152.4175,N,00832.5083,W,11.90,62.01,231110,,*12";
            string actual = ParserUtil.GenerateChecksum(sentence);

            Assert.AreNotEqual("13", actual);
        }
        #endregion

        #region "GetLatitude Tests"
        [TestMethod]
        public void GetLatitude_ValidLatitude_Pass()
        {
            string latitude = "5152.4175";
            string hemisphere = "N";

            double actual = 51.873625;
            double expected = ParserUtil.GetLatitude(latitude, hemisphere);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetLatitude_ValidLatitude2_Pass()
        {
            string latitude = "5152.4175";
            string hemisphere = "S";

            double actual = -1 * 51.873625;
            double expected = ParserUtil.GetLatitude(latitude, hemisphere);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetLatitude_InvalidLatitude_ThrowNewArgumentException()
        {
            string latitude = "9152.4175";
            string hemisphere = "S";

            double expected = 0;

            ArgumentException expectedEx = null;

            try
            {
                expected = ParserUtil.GetLatitude(latitude, hemisphere);
            }
            catch (ArgumentException ex)
            {
                expectedEx = ex;
            }
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
        }
        [TestMethod]
        public void GetLatitude_InvalidLatitudePrecision_ThrowNewArgumentException()
        {
            string latitude = "51524175";
            string hemisphere = "S";

            double expected = 0;

            ArgumentException expectedEx = null;

            try
            {
                expected = ParserUtil.GetLatitude(latitude, hemisphere);
            }
            catch (ArgumentException ex)
            {
                expectedEx = ex;
            }
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
        }
        [TestMethod]
        public void GetLatitude_InvalidLatitudeCharacter_ThrowNewArgumentException()
        {
            string latitude = "515f.4175";
            string hemisphere = "S";

            double expected = 0;

            ArgumentException expectedEx = null;

            try
            {
                expected = ParserUtil.GetLatitude(latitude, hemisphere);
            }
            catch (ArgumentException ex)
            {
                expectedEx = ex;
            }
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
        }
        [TestMethod]
        public void GetLatitude_InvalidLatitudeHemisphere_ThrowNewArgumentException()
        {
            string latitude = "5152.4175";
            string hemisphere = "X";

            double expected = 0;

            ArgumentException expectedEx = null;

            try
            {
                expected = ParserUtil.GetLatitude(latitude, hemisphere);
            }
            catch (ArgumentException ex)
            {
                expectedEx = ex;
            }
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
        }
        #endregion

        #region "GetLongtitude Tests"
        [TestMethod]
        public void GetLongtitude_ValidLongtitude_Pass()
        {
            string longtitude = "00832.5083";
            string hemisphere = "W";

            double actual = -1 * 8.541805;
            double expected = ParserUtil.GetLongtitude(longtitude, hemisphere);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetLongtitude_ValidLongtitude2_Pass()
        {
            string longtitude = "00832.5083";
            string hemisphere = "W";

            double actual = -1 * 8.541805;
            double expected = ParserUtil.GetLongtitude(longtitude, hemisphere);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetLongtitude_InvalidLongtitude_ThrowNewArgumentException()
        {
            string longtitude = "19932.5083";
            string hemisphere = "W";

            double expected = 0;

            ArgumentException expectedEx = null;

            try
            {
                expected = ParserUtil.GetLongtitude(longtitude, hemisphere);
            }
            catch (ArgumentException ex)
            {
                expectedEx = ex;
            }
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
        }
        [TestMethod]
        public void GetLongtitude_InvalidLongtitudePrecision_ThrowNewArgumentException()
        {
            string longtitude = "008325083";
            string hemisphere = "W";

            double expected = 0;

            ArgumentException expectedEx = null;

            try
            {
                expected = ParserUtil.GetLongtitude(longtitude, hemisphere);
            }
            catch (ArgumentException ex)
            {
                expectedEx = ex;
            }
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
        }
        [TestMethod]
        public void GetLongtitude_InvalidLongtitudeCharacter_ThrowNewArgumentException()
        {
            string longtitude = "00832f5083";
            string hemisphere = "W";

            double expected = 0;

            ArgumentException expectedEx = null;

            try
            {
                expected = ParserUtil.GetLongtitude(longtitude, hemisphere);
            }
            catch (ArgumentException ex)
            {
                expectedEx = ex;
            }
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
        }
        [TestMethod]
        public void GetLongtitude_InvalidLongtitudeHemisphere_ThrowNewArgumentException()
        {
            string longtitude = "00832.5083";
            string hemisphere = "X";

            double expected = 0;

            ArgumentException expectedEx = null;

            try
            {
                expected = ParserUtil.GetLongtitude(longtitude, hemisphere);
            }
            catch (ArgumentException ex)
            {
                expectedEx = ex;
            }
            Assert.IsInstanceOfType(expectedEx, typeof(ArgumentException));
        }
        #endregion

        #region "IsGPSLock Tests"
        public void IsGPSLock_ValidLock_Pass()
        {
            string gpsLock = "A";

            Assert.IsTrue(ParserUtil.IsGPSLock(gpsLock));
        }
        public void IsGPSLock_InvalidLock_Pass()
        {
            string gpsLock = "V";

            Assert.IsFalse(ParserUtil.IsGPSLock(gpsLock));
        }
        #endregion

        #region "IsLatitude Tests"

        [TestMethod]
        public void IsLatitude_ValidLatitude_Pass()
        {
            string latitude = "5152.4175";

            Assert.IsTrue(ParserUtil.IsLatitude(latitude));
        }

        [TestMethod]
        public void IsLatitude_InvalidLatitudePrecision_Fail()
        {
            string latitude = "9152.4175";

            Assert.IsFalse(ParserUtil.IsLatitude(latitude));
        }

        [TestMethod]
        public void IsLatitude_InvalidLatitudeMissingDecimal_Fail()
        {
            string latitude = "51524175";

            Assert.IsFalse(ParserUtil.IsLatitude(latitude));
        }

        [TestMethod]
        public void IsLatitude_InvalidLatitudeCharacter_Fail()
        {
            string latitude = "515f.4175";

            Assert.IsFalse(ParserUtil.IsLatitude(latitude));
        }
        #endregion

        #region "IsLatitudeHemisphere Tests"
        public void IsLatitudeHemisphere_ValidHemisphere_Pass()
        {
            string hemisphere = "N";
            string hemisphere2 = "S";

            Assert.IsTrue(ParserUtil.IsLatitudeHemisphere(hemisphere));
            Assert.IsTrue(ParserUtil.IsLatitudeHemisphere(hemisphere2));
        }
        public void IsLatitudeHemisphere_InvalidHemisphere_Fail()
        {
            string hemisphere = "X";

            Assert.IsFalse(ParserUtil.IsLatitudeHemisphere(hemisphere));
        }
        #endregion

        #region "IsLongtitude Tests"
        public void IsLongtitude_ValidLongtitude_Pass()
        {
            string Longtitude = "00832.5083";

            Assert.IsTrue(ParserUtil.IsLongtitude(Longtitude));
        }

        [TestMethod]
        public void IsLongtitude_InvalidLongtitudePrecision_Fail()
        {
            string Longtitude = "19132.5083";

            Assert.IsFalse(ParserUtil.IsLongtitude(Longtitude));
        }

        [TestMethod]
        public void IsLongtitude_InvalidLongtitudeMissingDecimal_Fail()
        {
            string Longtitude = "008325083";

            Assert.IsFalse(ParserUtil.IsLongtitude(Longtitude));
        }

        [TestMethod]
        public void IsLongtitude_InvalidLongtitudeCharacter_Pass()
        {
            string Longtitude = "008h2.5083";

            Assert.IsFalse(ParserUtil.IsLongtitude(Longtitude));
        }
        #endregion

        #region "IsLongtitudeHemisphere Tests"
        public void IsLongtitudeHemisphere_ValidHemisphere_Pass()
        {
            string hemisphere = "E";
            string hemisphere2 = "W";

            Assert.IsTrue(ParserUtil.IsLongtitudeHemisphere(hemisphere));
            Assert.IsTrue(ParserUtil.IsLongtitudeHemisphere(hemisphere2));
        }
        public void IsLongtitudeHemisphere_InvalidHemisphere_Fail()
        {
            string hemisphere = "X";

            Assert.IsFalse(ParserUtil.IsLongtitudeHemisphere(hemisphere));
        }
        #endregion
    }
}
