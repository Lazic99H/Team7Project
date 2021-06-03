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
        public void CheckValidateEntry()
        {
            string goodStartDate = "3/21/2018";
            string goodEndDate = "4/1/2020";
            string goodArea = "SRB";

            string baadStartDateYear = "5/5/2020";
            string badEndDateYear = "7/5/2018";

            string baadStartDateMonth = "7/5/2020";
            string badEndDateMonth = "5/5/2020";

            string baadStartDateDay = "5/7/2020";
            string badEndDateDay = "5/5/2020";

            DataCache.Validate validate = new DataCache.Validate();
            Assert.AreEqual("", validate.ValidateEntry(goodStartDate, goodEndDate, goodArea));

            Assert.AreEqual("All fields must be filled in correctly", validate.ValidateEntry("", "", ""));
            Assert.AreEqual("All fields must be filled in correctly", validate.ValidateEntry(goodStartDate, goodEndDate, ""));
            Assert.AreEqual("All fields must be filled in correctly", validate.ValidateEntry("", goodEndDate, goodArea));
            Assert.AreEqual("All fields must be filled in correctly", validate.ValidateEntry(goodStartDate, "", goodArea));

            Assert.AreEqual("Starting date must be lower then ending date", validate.ValidateEntry(baadStartDateYear, badEndDateYear, goodArea));
            Assert.AreEqual("Starting date must be lower then ending date", validate.ValidateEntry(baadStartDateDay, badEndDateDay, goodArea));
            Assert.AreEqual("Starting date must be lower then ending date", validate.ValidateEntry(baadStartDateMonth, badEndDateMonth, goodArea));
            




        }
    }
}
