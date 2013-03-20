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

using System.Collections.Generic;
using System.Linq;
using NMEAParser.NMEA0183;

namespace NMEAParser.Extensions
{
    public static class GPRMCExtensions
    {
        /// <summary>
        /// Converts a list of GPRMC sentences to a list of LatLon structs.
        /// </summary>
        /// <param name="route">A list of GeoPoints.</param>
        /// <returns>A list of LatLon structs.</returns>
        public static List<LatLon> ToLatLon(this List<GPRMC> route)
        {
            IEnumerable<LatLon> points = from latLon in route.OrderBy(x => x.TimeStamp)
                                         select latLon.LatLon;

            return points.ToList();
        }
    }
}
