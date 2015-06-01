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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMEAParser.Utils;
using System.Collections.Generic;

namespace NMEAParser.Tests.UtilTests
{
    [TestClass]
    public class GeoUtilTests
    {
        #region "GetDistance Tests"

        [TestMethod]
        public void GetDistanceKM_NoCoordinates_Pass()
        {
            double actual = GeoUtil.GetDistanceKM(new LatLon(0, 0), new LatLon(0, 0));

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void GetDistanceKM_TotalDistance_Pass()
        {
            double actual = GeoUtil.GetDistanceKM(new LatLon(51.8983398377895, -8.47277440130711),
                                                    new LatLon(53.3437004685402, -6.24956980347633));

            Assert.AreEqual(219.87222525091778, actual);
        }

        [TestMethod]
        public void GetDistanceKM_TotalDistance_Fail()
        {
            double actual = GeoUtil.GetDistanceKM(new LatLon(51.8983398377895, -8.47277440130711),
                                                       new LatLon(53.3437004685402, -6.24956980347633));

            Assert.AreNotEqual(200, actual);
        }

        [TestMethod]
        public void GetDistanceKM_RouteDistance_Pass()
        {
            List<LatLon> route = new List<LatLon>();

            route.Add(new LatLon(51.8983398377895, -8.47277440130711));
            route.Add(new LatLon(51.8983398377895, -8.47277440));
            route.Add(new LatLon(51.8983398377895, -8.57277440130711));

            double actual = GeoUtil.GetDistanceKM(route);

            Assert.AreEqual(6.8613790676125754, actual);
        }

        [TestMethod]
        public void GetDistanceKM_EmptyRoute_Pass()
        {
            List<LatLon> route = new List<LatLon>();

            double actual = GeoUtil.GetDistanceKM(route);

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void GetDistanceKM_RouteDistance_Fail()
        {
            List<LatLon> route = new List<LatLon>();

            route.Add(new LatLon(51.8983398377895, -8.47277440130711));
            route.Add(new LatLon(51.8983398377895, -8.45277440130714));
            route.Add(new LatLon(51.8983398377895, -8.47278440130713));

            double actual = GeoUtil.GetDistanceKM(route);

            Assert.AreNotEqual(2.7452379003155, actual);
        }

        [TestMethod]
        public void GetDistanceKM_RouteDistanceWithMinimumDistance_Pass()
        {
            List<LatLon> route = new List<LatLon>();

            route.Add(new LatLon(51.8983398377895, -8.47277440130711));
            route.Add(new LatLon(51.8983398377895, -8.47277440));
            route.Add(new LatLon(51.8983398377895, -8.57277440130711));

            double actual = GeoUtil.GetDistanceKM(route, 0.005);

            Assert.AreEqual(6.8613789779268615, actual);
        }

        [TestMethod]
        public void GetDistanceKM_RouteDistanceWithMinimumDistance_Fail()
        {
            List<LatLon> route = new List<LatLon>();

            route.Add(new LatLon(51.8983398377895, -8.47277440130711));
            route.Add(new LatLon(51.8983398377895, -8.47277440));
            route.Add(new LatLon(51.8983398377895, -8.57277440130711));

            double actual = GeoUtil.GetDistanceKM(route, 000.1);

            Assert.AreNotEqual(6.8613790676125754, actual);
        }

        #endregion

        #region "CompareRoutes Tests"

        [TestMethod]
        public void CompareRoutes_NoPoint_Pass()
        {
            List<LatLon> routeA = new List<LatLon>();
            List<LatLon> routeB = new List<LatLon>();

            double actual = GeoUtil.RouteExists(routeA, routeB, 0.1);

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void CompareRoutes_NoMatch_Pass()
        {
            List<LatLon> routeA = new List<LatLon>();
            List<LatLon> routeB = new List<LatLon>();

            routeA.Add(new LatLon(51.8983398377895, -8.47277440130711));
            routeA.Add(new LatLon(51.8983398377895, -8.47277440));
            routeA.Add(new LatLon(51.8983398377895, -8.47277441));
            routeA.Add(new LatLon(52.8983398377895, -8.47277442));

            double actual = GeoUtil.RouteExists(routeA, routeB, 0.1);

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void CompareRoutes_FullMatch_Pass()
        {
            List<LatLon> routeA = new List<LatLon>();
            List<LatLon> routeB = new List<LatLon>();

            routeA.Add(new LatLon(51.8983398377895, -8.47277440130711));
            routeA.Add(new LatLon(51.8983398377895, -8.47277440));
            routeA.Add(new LatLon(51.8983398377895, -8.47277441));
            routeA.Add(new LatLon(52.8983398377895, -8.47277442));

            routeB.Add(new LatLon(51.8983398377895, -8.47277440130711));
            routeB.Add(new LatLon(52.8983398377895, -8.47277442));

            double actual = GeoUtil.RouteExists(routeA, routeB, 0.1);

            Assert.AreEqual(100, actual);
        }


        [TestMethod]
        public void CompareRoutes_PartialMatch2_Pass()
        {
            List<LatLon> routeA = new List<LatLon>();
            List<LatLon> routeB = new List<LatLon>();

            routeA.Add(new LatLon(51.8983398377895, -8.47277440130711));
            routeA.Add(new LatLon(51.8983398377895, -8.47277440));
            routeA.Add(new LatLon(51.8983398377895, -8.47277441));
            routeA.Add(new LatLon(52.8983398377895, -8.47277442));

            routeB.Add(new LatLon(51.8983398377895, -8.47277440130711));
            routeB.Add(new LatLon(51.8983398377895, -8.47277441));
            routeB.Add(new LatLon(52.8983398377895, -8.47277442));
            routeB.Add(new LatLon(55.8983398377895, -8.47277442));

            double actual = GeoUtil.RouteExists(routeA, routeB, 0.005);

            Assert.AreEqual(75, actual);
        }

        [TestMethod]
        public void CompareRoutes_FullMatch2_Pass()
        {
            List<LatLon> routeA = new List<LatLon>();
            List<LatLon> routeB = new List<LatLon>();

            routeA.Add(new LatLon(51.8983398377895, -8.47277440130711));
            routeA.Add(new LatLon(51.8983398377895, -8.47277440));
            routeA.Add(new LatLon(51.8983398377895, -8.47277441));
            routeA.Add(new LatLon(52.8983398377895, -8.47277442));

            routeB.Add(new LatLon(51.8983398377895, -8.47277440130711));
            routeB.Add(new LatLon(51.8983398377895, -8.47277440));
            routeB.Add(new LatLon(51.8983398377895, -8.47277441));
            routeB.Add(new LatLon(52.8983398377895, -8.47277442));

            double actual = GeoUtil.RouteExists(routeA, routeB, 0.1);

            Assert.AreEqual(100, actual);
        }

        #endregion

        #region "LatLonExists Tests"

        [TestMethod]
        public void LatLonExists_ExistingPoint_Pass()
        {
            List<LatLon> route = new List<LatLon>();

            route.Add(new LatLon(51.8983398377895, -8.47277440130711));
            route.Add(new LatLon(51.8983398377895, -8.47277440));
            route.Add(new LatLon(51.8983398377895, -8.57277440130711));

            bool actual = GeoUtil.LatLonExists(route, new LatLon(51.8983398377895, -8.47277440), 0.05);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void LatLonExists_NoPoint_Fail()
        {
            List<LatLon> route = new List<LatLon>();

            route.Add(new LatLon(51.8983398377895, -8.47277440130711));
            route.Add(new LatLon(51.8983398377895, -8.47277440));
            route.Add(new LatLon(51.8983398377895, -8.57277440130711));

            bool actual = GeoUtil.LatLonExists(route, new LatLon(-51.8983398377895, -8.47277440), 0.05);

            Assert.IsFalse(actual);
        }
        #endregion
    }
}
