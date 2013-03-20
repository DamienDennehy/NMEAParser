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

//Copyright 2011 Damien Dennehy.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMEAParser.Utils;

namespace NMEAParser.Tests.UtilTests
{
    [TestClass]
    public class DoubleUtilTests
    {
        #region "IsDouble Tests"
        public void IsDouble_ValidDouble_Pass()
        {
            string dec = "01.03232";

            Assert.IsTrue(DoubleUtil.IsDouble(dec));
        }
        public void IsDouble_InvalidDoubleCharacter_Fail()
        {
            string dec = "01.03f232";

            Assert.IsFalse(DoubleUtil.IsDouble(dec));
        }
        public void IsDouble_InvalidDoublePoint_Fail()
        {
            string dec = "01.03.232";

            Assert.IsFalse(DoubleUtil.IsDouble(dec));
        }
        #endregion
    }
}
