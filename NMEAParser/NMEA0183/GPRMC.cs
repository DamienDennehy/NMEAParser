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
using NMEAParser;
using NMEAParser.Utils;

namespace NMEAParser.NMEA0183
{
    public class GPRMC : BaseSentence
    {
        #region "Fields"
        protected LatLon _latLon;
        protected double _bearing;
        protected double _speedKnots;
        protected double _magVar;
        protected DateTime _timeStamp;
        #endregion

        #region "Properties"
        public LatLon LatLon
        {
            get
            {
                return _latLon;
            }
        }

        public double Bearing
        {
            get
            {
                return _bearing;
            }
        }
        public double SpeedKnots
        {
            get
            {
                return _speedKnots;
            }
        }
        public double SpeedKM
        {
            get
            {
                return SpeedKnots * 1.852;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }
        }
        public double MagVar
        {
            get
            {
                return _magVar;
            }
        }
  
        #endregion

        internal GPRMC(LatLon latLon, double bearing, double speedKnots, DateTime timeStamp, double magVar) 
        {
            this._sentenceType = "GPRMC";
            this._latLon = latLon;
            this._bearing = bearing;
            this._speedKnots = speedKnots;
            this._timeStamp = timeStamp;
            this._magVar = magVar;
        }
    }

    public class GPRMCParser : IParser
    {
        /// <summary>
        /// Used to indicate if a sentence is a valid GPRMC string.
        /// </summary>
        /// <param name="sentence">The GPRMC string to be validated.</param>
        /// <returns>A boolean indicating if the sentence is valid or not.</returns>
        public bool IsSentence(string sentence)
        {
            //Check if the sentence is null
            if (String.IsNullOrWhiteSpace(sentence))
            {
                return false;
            }

            //Check if the sentence contains comma seperated values
            if (!sentence.Contains(","))
            {
                return false;
            }

            string[] fields = sentence.Split(',');

            //Check if the field length is valid for GPRMC
            if (fields.Length != 12)
            {
                return false;
            }

            //Check if the first field is a valid GPRMC sentence type
            if (fields[0].ToUpper() != "$GPRMC")
            {
                return false;
            }

            //Check if the second field is a valid time
            if (!DateTimeUtil.IsTime(fields[1]))
            {
                return false;
            }

            //Check if the fourth field is a valid latitude
            if (!ParserUtil.IsLatitude(fields[3]))
            {
                return false;
            }

            //Check if the fifth field is a valid latitude hemisphere
            if (!ParserUtil.IsLatitudeHemisphere(fields[4]))
            {
                return false;
            }

            //Check if the sixth field is a valid longtitude
            if (!ParserUtil.IsLongtitude(fields[5]))
            {
                return false;
            }

            //Check if the seventh field is a valid longtitude hemisphere
            if (!ParserUtil.IsLongtitudeHemisphere(fields[6]))
            {
                return false;
            }

            //Check if the eight field is a valid speed
            if (!DoubleUtil.IsDouble(fields[7]))
            {
                return false;
            }

            //Check if the ninth field is a valid bearing
            if (!DoubleUtil.IsDouble(fields[8]))
            {
                return false;
            }

            //Check if the tenth field is a valid date
            if (!DateTimeUtil.IsShortDate(fields[9]))
            {
                return false;
            }

            //Check if the eleventh field is a valid magnetic variation
            //Optional field, therefore only validate if it is supplied
            if (!String.IsNullOrWhiteSpace(fields[10]) && !DoubleUtil.IsDouble(fields[10]))
            {
                return false;
            }

            //Check if the twelfth field is a valid checksum
            string checksum = ParserUtil.GenerateChecksum(sentence);
            if (fields[11].Substring(1) != checksum)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Used to parse a sentence and convert it into a GPRMC GeoPoint.
        ///<exception cref="System.ArgumentNullException">Is thrown when the sentence is null.</exception>
        ///<exception cref="System.ArgumentException">Is thrown when the sentence is invalid.</exception>
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns>Base Sentence</returns>
        public BaseSentence ParseSentence(string sentence)
        {
            //Check if the sentence is null
            if (String.IsNullOrWhiteSpace(sentence))
            {
                throw new ArgumentNullException("No sentence provided to parse");
            }

            //Check if the sentence contains comma seperated values
            if (!sentence.Contains(","))
            {
                throw new ArgumentException("Sentence cannot be parsed");
            }

            string[] fields = sentence.Split(',');

            //Check if the field length is valid for GPRMC
            if (fields.Length != 12)
            {
                throw new ArgumentException("Invalid field length found in sentence");
            }

            //Check if the first field is a valid GPRMC sentence type
            if (fields[0].ToUpper() != "$GPRMC")
            {
                throw new ArgumentException("Invalid sentence type provided in sentence");
            }

            //Check if the second field is a valid time
            if (!DateTimeUtil.IsTime(fields[1]))
            {
                throw new ArgumentException("Invalid time provided in sentence");
            }

            //Check if the fourth field is a valid latitude
            if (!ParserUtil.IsLatitude(fields[3]))
            {
                throw new ArgumentException("Invalid latitude provided in sentence");
            }

            //Check if the fifth field is a valid latitude hemisphere
            if (!ParserUtil.IsLatitudeHemisphere(fields[4]))
            {
                throw new ArgumentException("Invalid latitude hemisphere provided in sentence");
            }

            //Check if the sixth field is a valid longtitude
            if (!ParserUtil.IsLongtitude(fields[5]))
            {
                throw new ArgumentException("Invalid longtitude provided in sentence");
            }

            //Check if the seventh field is a valid longtitude hemisphere
            if (!ParserUtil.IsLongtitudeHemisphere(fields[6]))
            {
                throw new ArgumentException("Invalid longtitude hemisphere provided in sentence");
            }

            //Check if the tenth field is a valid date
            if (!DateTimeUtil.IsShortDate(fields[9]))
            {
                throw new ArgumentException("Invalid date provided in sentence");
            }

            //Check if the eleventh field is a valid magnetic variation
            //Optional field, therefore only use it if it is supplied
            if (!String.IsNullOrWhiteSpace(fields[10]) && !DoubleUtil.IsDouble(fields[10]))
            {
                throw new ArgumentException("Invalid magnetic variation provided in sentence");
            }

            //Check if the twelfth field is a valid checksum
            string checksum = ParserUtil.GenerateChecksum(sentence);
            if (fields[11].Substring(1) != checksum)
            {
                throw new ArgumentException("Invalid checksum provided for sentence");
            }

            DateTime timestamp = DateTimeUtil.GetDate(fields[9], fields[1]);

            double latitude = ParserUtil.GetLatitude(fields[3], fields[4]);
            double longtitude = ParserUtil.GetLongtitude(fields[5], fields[6]);
            double speed = 0;
            double bearing = 0;
            double magVar = 0;

            

            //Only convert the speed field if it's actually used
            if (DoubleUtil.IsDouble(fields[7]))
            {
                speed = Double.Parse(fields[7]);
            }

            //Only convert the bearing field if it's actually used
            if (DoubleUtil.IsDouble(fields[8]))
            {
                bearing = Double.Parse(fields[8]);
            }

            //Only convert the magnetic variation field if it's actually used
            if (DoubleUtil.IsDouble(fields[10]))
            {
                magVar = Double.Parse(fields[10]);
            }

            return new GPRMC(new LatLon(latitude, longtitude), bearing, speed, timestamp, magVar);
        }
    }
}
