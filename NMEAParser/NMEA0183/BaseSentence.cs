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

namespace NMEAParser.NMEA0183
{
    /// <summary>
    /// Abstract Builder class which all NMEA sentences should use.
    /// </summary>
    public abstract class BaseSentence
    {
        #region "Properties"
        /// <summary>
        /// The type of NMEA Sentence.
        /// </summary>
        public string SentenceType { get; private set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sentenceType"></param>
        protected BaseSentence(string sentenceType)
        {
            SentenceType = sentenceType;
        }
    }
}
