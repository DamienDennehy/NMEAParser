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

namespace NMEAParser
{
    /// <summary>
    /// Simple struct for Latitude and Longitude.
    /// </summary>
    public struct LatLon
    {
        /// <summary>
        /// The Latitude of the GeoPoint.
        /// </summary>
        public double Latitude;

        /// <summary>
        /// The Longtitude of the GeoPoint.
        /// </summary>
        public double Longtitude;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longtitude"></param>
        public LatLon(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }
    }
}