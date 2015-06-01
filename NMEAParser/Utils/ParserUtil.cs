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
using System.Globalization;

namespace NMEAParser.Utils
{
    /// <summary>
    /// Helper utilities for sentence parsing.
    /// </summary>
    public class ParserUtil
    {
        /// <summary>
        /// Validates a GPS lock stored as a string.
        /// </summary>
        /// <param name="input">The string to be validated as a GPS lock.</param>
        /// <returns>A boolean indicating if the string is a GPS lock or not.</returns>
        public static bool IsGPSLock(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            input = input.ToUpper().Trim();

            if (input != "A")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a latitudes hemisphere stored as a string.
        /// </summary>
        /// <param name="input">The string to be validated as a latitude hemisphere.</param>
        /// <returns>A boolean indicating if the string is a latitude hemisphere or not.</returns>
        public static bool IsLatitudeHemisphere(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            input = input.ToUpper().Trim();

            if (input != "N" && input != "S")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a longtitudes hemisphere stored as a string.
        /// </summary>
        /// <param name="input">The string to be validated as a longtitude hemisphere.</param>
        /// <returns>A boolean indicating if the string is a longtitude hemisphere or not.</returns>
        public static bool IsLongtitudeHemisphere(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            input = input.ToUpper().Trim();

            if (input != "E" && input != "W")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a latitude stored as a string format (HHMM.M).
        /// </summary>
        /// <param name="input">The string to be validated as a latitude.</param>
        /// <returns>A boolean indicating if the string is a latitude or not.</returns>
        public static bool IsLatitude(string input)
        {
            if (!DoubleUtil.IsDouble(input))
            {
                return false;
            }

            double latitude = Double.Parse(input, new CultureInfo("en-US"));

            if (latitude < 0)
            {
                latitude = latitude * -1;
            }

            if (latitude > 9000)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a longtitude stored as a string format (HHHMM.M).
        /// </summary>
        /// <param name="input">The string to be validated as a longtitude.</param>
        /// <returns>A boolean indicating if the string is a longtitude or not.</returns>
        public static bool IsLongtitude(string input)
        {
            if (!DoubleUtil.IsDouble(input))
            {
                return false;
            }

            double longtitude = Double.Parse(input, new CultureInfo("en-US"));

            if (longtitude < 0)
            {
                longtitude = longtitude * -1;
            }

            if (longtitude > 18000)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Used to generate the checksum of a NMEA sentence.
        /// </summary>
        /// <param name="sentence">The NMEA sentence used to generate the checksum.</param>
        /// <returns>The checkum generated from the NMEA sentence.</returns>
        ///<exception cref="System.ArgumentNullException">Is thrown when the sentence is null.</exception>
        ///<exception cref="System.ArgumentException">Is thrown when the sentence is invalid.</exception>
        public static string GenerateChecksum(string sentence)
        {
            if (String.IsNullOrWhiteSpace(sentence))
            {
                throw new ArgumentNullException("No sentence provided");
            }

            if (!sentence.Contains("$"))
            {
                throw new ArgumentException("Sentence is missing start character");
            }

            if (!sentence.Contains("*"))
            {
                throw new ArgumentException("Sentence is missing end character");
            }

            int startIndex = sentence.IndexOf('$');
            int endIndex = sentence.IndexOf('*');
            int checksum = Convert.ToByte(sentence[startIndex + 1]);

            for (int i = startIndex + 2; i < endIndex; i++)
            {
                checksum ^= Convert.ToByte(sentence[i]);
            }

            return checksum.ToString("X2");
        }

        /// <summary>
        /// Converts a string latitude stored in HHMM.M to a double
        /// </summary>
        /// <param name="latitude">The latitude stored as a string.</param>
        /// <param name="hemisphere">The hemisphere of the latitude.</param>
        /// <returns></returns>
       ///<exception cref="System.ArgumentException">Is thrown when the arguments are invalid.</exception>
        public static double GetLatitude(string latitude, string hemisphere)
        {
            double hours;
            double minutes;
            double lat = 0;

            hemisphere = hemisphere.ToUpper().Trim();

            if (!IsLatitude(latitude))
            {
                throw new ArgumentException("Invalid latitude provided");
            }

            if (!IsLatitudeHemisphere(hemisphere))
            {
                throw new ArgumentException("Invalid latitude hemisphere provided");
            }

            hours = double.Parse(latitude.Substring(0, 2), new CultureInfo("en-US"));
            minutes = double.Parse(latitude.Substring(2), new CultureInfo("en-US")) / 60;
            lat = hours + minutes;

            //If the latitude is south of the equator convert it to a minus number
            if (hemisphere == "S")
            {
                lat = lat * -1;
            }

            return lat;

        }

        /// <summary>
        /// Converts a string longtitude stored in HHHMM.M to a double
        /// </summary>
        /// <param name="longtitude">The longtitude stored as a string.</param>
        /// <param name="hemisphere">The hemisphere of the longtitude.</param>
        /// <returns></returns>
        ///<exception cref="System.ArgumentException">Is thrown when the arguments are invalid.</exception>
        public static double GetLongtitude(string longtitude, string hemisphere)
        {
            double hours;
            double minutes;
            double lon = 0;

            hemisphere = hemisphere.ToUpper().Trim();

            if (!IsLongtitude(longtitude))
            {
                throw new ArgumentException("Invalid longtitude provided");
            }

            if (!IsLongtitudeHemisphere(hemisphere))
            {
                throw new ArgumentException("Invalid longtitude hemisphere provided");
            }

            hours = double.Parse(longtitude.Substring(0, 3), new CultureInfo("en-US"));
            minutes = double.Parse(longtitude.Substring(3), new CultureInfo("en-US")) / 60;
            lon = hours + minutes;

            //If the longtitude is west of the Prime Meridian convert it to a minus number
            if (hemisphere == "W")
            {
                lon = lon * -1;
            }

            return lon;
        }
    }
}
