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
    public class DateTimeUtilTests
    {
        #region "GetDate Tests"

        [TestMethod]
        public void GetDate_ValidDateTime_Pass()
        {
            string date = "231110";
            string time = "084752.123";

            DateTime? actual = null;
            DateTime expected = new DateTime(2010, 11, 23, 8, 47, 52, 123);

            actual = DateTimeUtil.GetDate(date, time);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetDate_InvalidDate_ThrowNewArgumentException()
        {
            string date = "4f1210";
            string time = "084752.123";

            DateTime? actual = null;
            ArgumentException expected = null;

            try
            {
                actual = DateTimeUtil.GetDate(date, time);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }
        [TestMethod]
        public void GetDate_InvalidTime_ThrowNewArgumentException()
        {
            string date = "011210";
            string time = "084752f123";

            DateTime? actual = null;
            ArgumentException expected = null;

            try
            {
                actual = DateTimeUtil.GetDate(date, time);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }
        [TestMethod]
        public void GetDate_InvalidTimePrecision_ThrowNewArgumentException()
        {
            string date = "011210";
            string time = "084752";

            DateTime? actual = null;
            ArgumentException expected = null;

            try
            {
                actual = DateTimeUtil.GetDate(date, time);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsInstanceOfType(expected, typeof(ArgumentException));
        }
        #endregion

        #region "IsDate Tests"
        public void IsDate_ValidDate_Pass()
        {
            string date = "01-Jan-2011";

            Assert.IsTrue(DateTimeUtil.IsDate(date));
        }

        public void IsDate_InvalidDateLength_Fail()
        {
            string date = "01-Jan-20118";

            Assert.IsFalse(DateTimeUtil.IsDate(date));
        }

        public void IsDate_InvalidDateCharacter_Fail()
        {
            string date = "01-Jjan-2011";

            Assert.IsFalse(DateTimeUtil.IsDate(date));
        }

        public void IsDate_InvalidDateDay_Fail()
        {
            string date = "32-Jan-2011";

            Assert.IsFalse(DateTimeUtil.IsDate(date));
        }

        public void IsDate_InvalidDateMonth_Fail()
        {
            string date = "01-jjj-2011";

            Assert.IsFalse(DateTimeUtil.IsDate(date));
        }

        public void IsDate_InvalidDateYear_Fail()
        {
            string date = "01-Jan-2011999";

            Assert.IsFalse(DateTimeUtil.IsDate(date));
        }
        #endregion

        #region "IsShortDate Tests"
        [TestMethod]
        public void IsShortDate_ValidDate_Pass()
        {
            string date = "010211";

            Assert.IsTrue(DateTimeUtil.IsShortDate(date));
        }

        [TestMethod]
        public void IsShortDate_InvalidDate_Fail()
        {
            string date = "0180211";

            Assert.IsFalse(DateTimeUtil.IsShortDate(date));
        }

        [TestMethod]
        public void IsShortDate_InvalidDateCharacter_Fail()
        {
            string date = "010h11";

            Assert.IsFalse(DateTimeUtil.IsShortDate(date));
        }
        #endregion

        #region "IsTime Tests"
        [TestMethod]
        public void IsTime_ValidTime_Pass()
        {
            string time = "080530.444";

            Assert.IsTrue(DateTimeUtil.IsTime(time));
        }
        [TestMethod]
        public void IsTime_InvalidTimeCharacter_Fail()
        {
            string time = "08053k0.444";

            Assert.IsFalse(DateTimeUtil.IsTime(time));
        }
        [TestMethod]
        public void IsTime_InvalidTimePrecision_Fail()
        {
            string time = "500000.444";

            Assert.IsFalse(DateTimeUtil.IsTime(time));
        }
        #endregion
    }
}
