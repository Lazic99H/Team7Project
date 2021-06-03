using DataAccess.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest
{
    [TestFixture]
    public class ConsumptionTest
    {
        [Test]
        [TestCase(1,2300, "RUS", "2018-05-02")]
        [TestCase(21, 2300, "RUS", "2018-05-02")]

        public void ConstructorWithGoodParametars(int hour,int load, string region, DateTime day)
        {
            Consumption consumption = new Consumption(hour, load, region, day);

            Assert.AreEqual(consumption.Hour, hour);
            Assert.AreEqual(consumption.Load, load);
            Assert.AreEqual(consumption.Region, region);
            Assert.AreEqual(consumption.Day, day);

        }

        [Test]
        [TestCase(1, 2300, null, "2018-05-02")]
        [TestCase(21, 2300, "RUS", null)]

        public void ConstructorWithBadNullParametars(int hour, int load, string region, DateTime day)
        {
            Assert.Throws<ArgumentNullException>(
                   () =>
                   {
                       Consumption consumption = new Consumption(hour, load, region, day);
                   });
        }



        [Test]
        [TestCase(-23, 2300, "RUS", "2018-05-02")]
        [TestCase(26, 2300, "RUS", "2018-05-02")]
        [TestCase(11, -234, "RUS", "2018-05-02")]

        public void ConstructorOutOfRangeParametars(int hour, int load, string region, DateTime day)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                   () =>
                   {
                       Consumption consumption = new Consumption(hour, load, region, day);
                   });
        }

        [Test]
        [TestCase(1, 2300, "", "2018-05-02")]
        [TestCase(21, 2300, "       ", "2018-05-02")]

        public void ConstructorEmptyRegion(int hour, int load, string region, DateTime day)
        {
            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       Consumption consumption = new Consumption(hour, load, region, day);
                   });
        }
    }
}
