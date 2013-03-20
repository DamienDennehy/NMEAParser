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
using System.Collections.Generic;
using System.Linq;

namespace NMEAParser.Utils
{
    public class GeoUtil
    {
        /// <summary>
        /// Radius of the Earth in Kilometers.
        /// </summary>
        private const double EARTH_RADIUS_KM = 6371;

        /// <summary>
        /// Converts an angle to a radian.
        /// </summary>
        /// <param name="input">The angle that is to be converted.</param>
        /// <returns>The angle in radians.</returns>
        private static double ToRad(double input)
        {
            return input * (Math.PI / 180);
        }

        /// <summary>
        /// Calculates the distance between two geo-points in kilometers using the Haversine algorithm.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="point2">The second point.</param>
        /// <returns>A double indicating the distance between the points in KM.</returns>
        public static double GetDistanceKM(LatLon point1, LatLon point2)
        {
            double dLat = ToRad(point2.Latitude - point1.Latitude);
            double dLon = ToRad(point2.Longtitude - point1.Longtitude);

            double h = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Cos(ToRad(point1.Latitude)) * Math.Cos(ToRad(point2.Latitude)) *
                       Math.Pow(Math.Sin(dLon / 2), 2);

            h = 2 * Math.Asin(Math.Sqrt(h));

            double d = EARTH_RADIUS_KM * h;
            return d;
        }

        /// <summary>
        /// Gets the total distance for a route by comparing a route points distance to its previous point.
        /// </summary>
        /// <param name="route">The list of points to compare distance.</param>
        /// <returns>A double indicating the total distance between the points in KM.</returns>
        public static double GetDistanceKM(List<LatLon> route)
        {
            double distance = 0;
            LatLon? previous = null;

            foreach (LatLon p in route)
            {
                distance += previous != null ? GetDistanceKM(previous.Value, p) : 0;
                previous = p;
            }

            return distance;
        }

        /// <summary>
        /// Gets the total distance for a route by comparing a route points distance to its previous point.
        /// Ignores points that aren't a specified minimum distance apart.
        /// Route must be sorted before execution.
        /// </summary>
        /// <param name="route">The list of points to compare distance.</param>
        /// <param name="route">The minimum distance in KM that should between each point for comparison.</param>
        /// <returns>A double indicating the total distance between the points in KM.</returns>
        public static double GetDistanceKM(List<LatLon> route, double distanceKM)
        {
            double distance = 0;
            LatLon? previous = null;

            foreach (LatLon p in route)
            {
                double pointDistance = previous != null ? GetDistanceKM(previous.Value, p) : 0;
                distance += pointDistance >= distanceKM ? pointDistance : 0;
                previous = p;
            }

            return distance;
        }
  
        /// <summary>
        /// Checks if a LatLon point exists in a route, nearest to the distance provided.
        /// </summary>
        /// <param name="route">The list of points to compare distance.</param>
        /// <param name="point">The point to be searched for.</param>
        /// <param name="distanceKM">The maximum distance between the point to be searched and the route.</param>
        /// <returns>A boolean indicating the point exists or not.</returns>
        public static bool LatLonExists(List<LatLon> route, LatLon point, double distanceKM)
        {
            foreach (LatLon p in route)
            {
                if (GetDistanceKM(p, point) <= distanceKM)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gives a percentage indication of how much of routeB exists in routeA
        /// </summary>
        /// <param name="routeA">The first route</param>
        /// <param name="routeB">The second route</param>
        /// <param name="distanceKM">The minimum distance defined between points.</param>
        /// <returns>A percentage similarity.</returns>
        public static double RouteExists(List<LatLon> routeA, List<LatLon> routeB, double distanceKM)
        {
            //If no valid route data is provided, return 0
            if ((routeA == null || routeA.Count < 1) || (routeB == null || routeB.Count < 1)) { return 0; }

            //for each point in the shortest route, check if it
            //exists in the longest route, nearest to the distance provided
            int points = routeB.Sum(x => LatLonExists(routeA, x, distanceKM) ? 1 : 0);

            //return the number of points in routeB found in routeA
            return points / (double)routeB.Count * 100;
        }
    }
}
