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

namespace NMEAParser.Utils
{
    public class DoubleUtil
    {
        /// <summary>
        /// Validates a double stored as a string.
        /// </summary>
        /// <param name="input">A string to be validated as a double.</param>
        /// <returns>A boolean indicating if the string is a double or not.</returns>
        public static bool IsDouble(string input)
        {
            Double dec;
            return Double.TryParse(input, out dec);
        }
    }
}
