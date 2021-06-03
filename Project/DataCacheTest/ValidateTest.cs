using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCacheTest
{
    [TestFixture]
    class ValidateTest
    {
        [Test]
        public void CheckValidateEntry(string startDate, string endDate, string geoArea)
        {
            string goodStartDate = "3/21/2020";
            string goodEndDate = "4/1/2020";
            string goodArea = "SRB";

            DataCache.Validate validate = new DataCache.Validate();
            Assert.AreEqual("", validate.ValidateEntry(goodStartDate, goodEndDate, geoArea));

        }
    }
}
