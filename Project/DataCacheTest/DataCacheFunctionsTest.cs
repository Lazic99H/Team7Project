using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCacheTest
{
    [TestFixture]
    class DataCacheFunctionsTest
    {
        [Test]
        public void CheckGetN()
        {
            string startDate = "5/6/2018";
            string endDate = "5/16/2018";

            string startDateMonth = "5/29/2018";
            string endDateMonth = "6/6/2018";

            DataCache.DataCacheFunctions dataCacheFunctions = new DataCache.DataCacheFunctions();

            Assert.AreEqual(11, dataCacheFunctions.GetN(startDate, endDate));
            Assert.AreEqual(9, dataCacheFunctions.GetN(startDateMonth, endDateMonth));

        }

        //[Test]
        /*public List<List<DataAccess.Model.IConsumption>> CheckForQueries1(string startDate, string endDate, string geoArea, Data.IData data)
        {

        }*/
    }
}
