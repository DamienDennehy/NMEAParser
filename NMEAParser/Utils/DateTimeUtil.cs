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
    public class DateTimeUtil
    {
        /// <summary>
        /// Validates a date stored as a string.
        /// </summary>
        /// <param name="input">A string to be validated.</param>
        /// <returns>A boolean indicating if the string is a valid date.</returns>
        public static bool IsDate(string input)
        {
            DateTime date;
            return DateTime.TryParse(input, out date);
        }

        /// <summary>
        /// Validates a date stored in DDMMYY format.
        /// </summary>
        /// <param name="input">A string to be validated.</param>
        /// <returns>A boolean indicating if the string is a valid date.</returns>
        public static bool IsShortDate(string input)
        {
            if (!DoubleUtil.IsDouble(input))
            {
                return false;
            }

            if (input.Length != 6)
            {
                return false;
            }

            if (Int32.Parse(input) < 010101 || Int32.Parse(input) > 311299)
            {
                return false;
            }

            int year = 2000 + Int32.Parse(input.Substring(4, 2));

            bool isDate = IsDate(String.Format("{0}-{1}-{2}", year, input.Substring(2, 2), input.Substring(0, 2)));

            return isDate;
        }

        /// <summary>
        /// Validates a time stored in HHMMSS.XXX format.
        /// </summary>
        /// <param name="input">A string to be validated.</param>
        /// <returns>A boolean indicating if the string is a valid time.</returns>
        public static bool IsTime(string input)
        {
            if (!DoubleUtil.IsDouble(input))
            {
                return false;
            }

            if (input.Length != 10)
            {
                return false;
            }

            double time = Double.Parse(input);

            if (time < 0 || time > 235959.999)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts a date stored in a MMDDYY format and a time stored in HHMMSS.XXX format to a datetime object.
        /// </summary>
        /// <param name="input">A string to be converted.</param>
        /// <returns>A DateTime representing the converted string.</returns>
        ///<exception cref="System.ArgumentException">Is thrown when the date or time arguments are invalid.</exception>
        public static DateTime GetDate(string date, string time)
        {
            if (!IsShortDate(date))
            {
                throw new ArgumentException("Invalid date argument");
            }

            if (!IsTime(time))
            {
                throw new ArgumentException("Invalid time argument");
            }

            int day = Int32.Parse(date.Substring(0, 2));
            int month = Int32.Parse(date.Substring(2, 2));
            int year = 2000 + Int32.Parse(date.Substring(4, 2));
            int hour = Int32.Parse(time.Substring(0, 2));
            int minute = Int32.Parse(time.Substring(2, 2));
            int second = Int32.Parse(time.Substring(4, 2));
            int millisecond = Int32.Parse(time.Substring(7, 3));

            DateTime dateTime = new DateTime(year, month, day, hour, minute, second, millisecond);
            return dateTime;
        }
    }
}
