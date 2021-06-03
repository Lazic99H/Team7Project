using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCache;

namespace DataCacheTest
{
    [TestFixture]
    public class QueryTest
    {
        [Test]
        [TestCase("6/3/2021", "6/7/2021", "RUS")]
        [TestCase("5/29/2021", "6/3/2021", "USA")]
        public void QueryKonstruktorParametri(string startDate, string endDate, string geoArea)
        {
            DataCache.Data.Query query = new DataCache.Data.Query(startDate, endDate, geoArea);

            Assert.AreEqual(query.StartDate, startDate);
            Assert.AreEqual(query.EndDate, endDate);
            Assert.AreEqual(query.GeoArea, geoArea);
            Assert.AreEqual(query.TimeSaved.ToString(), DateTime.Now.ToString());
            query.EndDate = query.StartDate;
            Assert.AreEqual(query.EndDate, query.StartDate);

        }

        [Test]
        [TestCase("", "", "")]
        [TestCase("6/3/2021", "", "SRB")]
        [TestCase("6/3/2020", "6/3/2021", "")]
        [TestCase("", "6/3/2021", "RUS")]
        [ExpectedException(typeof(ArgumentException))]
        public void QueryKonstruktorLosiParametri(string startDate, string endDate, string geoArea)
        {
            DataCache.Data.Query query = new DataCache.Data.Query(startDate, endDate, geoArea);
            //Assert.AreEqual(query.TimeSaved.ToString(), DateTime.Now.ToString());
        }

        [Test]
        [TestCase(null, null, null)]
        [TestCase("6/3/2021", null, "RUS")]
        [TestCase(null, "6/3/2021", "SRB")]
        [TestCase("6/3/2020", "6/3/2021", null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QueryKonstruktorNullParametri(string startDate, string endDate, string geoArea)
        {
            DataCache.Data.Query query = new DataCache.Data.Query(startDate, endDate, geoArea);
            //Assert.AreEqual(query.TimeSaved.ToString(), DateTime.Now.ToString());

        }



        
    }
}
