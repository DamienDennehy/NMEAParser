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

namespace NMEAParser.NMEA0183
{
    /// <summary>
    /// Interface for all sentence parsers.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Indicates if the sentence is a valid one.
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        bool IsSentence(string sentence);

        /// <summary>
        /// Convert the string to a valid sentence.
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        BaseSentence ParseSentence(string sentence);
    }
}
