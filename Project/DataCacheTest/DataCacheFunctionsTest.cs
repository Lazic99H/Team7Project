using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Moq;
using DataAccess.Model;
using DataCache.Data;
using DataCache;
using System.Threading;

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

        

        Mock<DataAccess.DAO.IConsumptionDAO> consumptionDAO = new Mock<DataAccess.DAO.IConsumptionDAO>();
        Mock<DataCache.Data.IData> data = new Mock<DataCache.Data.IData>();

        private DataCache.IDataCacheFunctions _dataCacheFunctions;// _product;

        [SetUp]
        public void SetUp()
        {
            //Mock<DataCache.IDataCacheFunctions> dataCacheDouble = new Mock<DataCache.IDataCacheFunctions>();
            //_dataCacheFunctions = dataCacheDouble.Object;
            _dataCacheFunctions = new DataCacheFunctions();
        }

        [Test]
        public void CheckToSeeRequests()
        {
            string startDate = "5/4/2020";
            string endDate = "5/4/2020";
            string geoArea = "RUS";

            List<IConsumption> consumptions = new List<IConsumption>();
            for (int i=1; i<=24; i++)
            {
                Consumption consumption = new Consumption(i, 1234 + (i*10), "RUS", DateTime.Parse("5/4/2020"));
                consumptions.Add(consumption);
            }

            Dictionary<DateTime, List<IConsumption>> dict = new Dictionary<DateTime, List<IConsumption>>();
            dict.Add(DateTime.Parse("5/4/2020"), consumptions);

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();

            Query q = new Query(startDate, endDate, geoArea);
            querys.Add(q, dict);

            List<List<IConsumption>> con = new List<List<IConsumption>>();
            con.Add(consumptions);

            //consumptionDAO.Setup(t => t.Read(geoArea, DateTime.Parse("5/4/2020"))).Returns(consumptions);
            data.SetupGet(m => m.queries).Returns(querys);
            Assert.AreEqual(con, _dataCacheFunctions.CheckForQueries(startDate, endDate, geoArea, data.Object));

        }

        [Test]
        public void CheckToSeeNoRequests()
        {
            //consumptionDAO.Reset();
            data.Reset();

            string startDate = "5/4/2020";
            string endDate = "5/4/2020";
            string geoArea = "RUS";

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();


            data.SetupGet(m => m.queries).Returns(querys);
            Assert.AreEqual(null, _dataCacheFunctions.CheckForQueries(startDate, endDate, geoArea, data.Object));
        }

        [Test]
        public void GetDataCheck()
        {
            consumptionDAO.Reset();
            data.Reset();

            string startDate = "5/4/2020";
            string endDate = "5/4/2020";
            string geoArea = "RUS";

            List<IConsumption> consumptions = new List<IConsumption>();
            for (int i = 1; i <= 24; i++)
            {
                Consumption consumption = new Consumption(i, 1234 + (i * 10), "RUS", DateTime.Parse("5/4/2020"));
                consumptions.Add(consumption);
            }

            Dictionary<DateTime, List<IConsumption>> dict = new Dictionary<DateTime, List<IConsumption>>();
            dict.Add(DateTime.Parse("5/4/2020"), consumptions);

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();

            Query q = new Query(startDate, endDate, geoArea);
            querys.Add(q, dict);

            List<List<IConsumption>> con = new List<List<IConsumption>>();
            con.Add(consumptions);

            List<DateTime> days = new List<DateTime>();
            days.Add(DateTime.Parse(startDate));

            consumptionDAO.Setup(t => t.Read(geoArea, days)).Returns(dict);
            data.SetupGet(m => m.queries).Returns(querys);

            Assert.AreEqual(con, _dataCacheFunctions.GetData(startDate, endDate, geoArea, consumptionDAO.Object, data.Object));

        }

        [Test]
        public void GetDataFromDataBase()
        {
            data.Reset();
            consumptionDAO.Reset();

            string startDate = "5/4/2020";
            string endDate = "5/4/2020";
            string geoArea = "RUS";

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();

            List<IConsumption> consumptions = new List<IConsumption>();
            for (int i = 1; i <= 24; i++)
            {
                Consumption consumption = new Consumption(i, 1234 + (i * 10), "RUS", DateTime.Parse("5/4/2020"));
                consumptions.Add(consumption);
            }

            Dictionary<DateTime, List<IConsumption>> dict = new Dictionary<DateTime, List<IConsumption>>();
            dict.Add(DateTime.Parse("5/4/2020"), consumptions);

            List<List<IConsumption>> con = new List<List<IConsumption>>();
            con.Add(consumptions);

            List<DateTime> days = new List<DateTime>();
            days.Add(DateTime.Parse(startDate));

            data.SetupGet(m => m.queries).Returns(querys);
            consumptionDAO.Setup(t => t.Read(geoArea, days)).Returns(dict);

            Assert.AreEqual(con, _dataCacheFunctions.GetData(startDate, endDate, geoArea,consumptionDAO.Object ,data.Object));
        }

        [Test]
        public void GetDataFromDataBaseSaViseDana()
        {
            data.Reset();
            consumptionDAO.Reset();

            string startDate = "5/4/2020";
            string endDate = "5/5/2020";
            string geoArea = "RUS";

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();

            List<IConsumption> consumptions = new List<IConsumption>();
            for (int i = 1; i <= 24; i++)
            {
                Consumption consumption = new Consumption(i, 1234 + (i * 10), "RUS", DateTime.Parse("5/4/2020"));
                consumptions.Add(consumption);
            }

            Dictionary<DateTime, List<IConsumption>> dict = new Dictionary<DateTime, List<IConsumption>>();
            dict.Add(DateTime.Parse("5/4/2020"), consumptions);

            List<List<IConsumption>> con = new List<List<IConsumption>>();
            con.Add(consumptions);

            consumptions.Clear();

            for (int i = 1; i <= 24; i++)
            {
                Consumption consumption = new Consumption(i, 1234 + (i * 10), "RUS", DateTime.Parse("5/5/2020"));
                consumptions.Add(consumption);
            }

            dict.Add(DateTime.Parse("5/5/2020"), consumptions);
            con.Add(consumptions);

            List<DateTime> days = new List<DateTime>();
            days.Add(DateTime.Parse(startDate));
            days.Add(DateTime.Parse(endDate));

            data.SetupGet(m => m.queries).Returns(querys);
            consumptionDAO.Setup(t => t.Read(geoArea, days)).Returns(dict);

            Assert.AreEqual(con, _dataCacheFunctions.GetData(startDate, endDate, geoArea, consumptionDAO.Object, data.Object));
        }

        [Test]
        public void GetDataFromDataBaseSaViseDanaKojiNisuSviUBazi()
        {
            data.Reset();
            consumptionDAO.Reset();

            string startDate = "5/3/2020";
            string endDate = "5/5/2020";
            string geoArea = "RUS";

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();
            List<List<IConsumption>> con = new List<List<IConsumption>>();
            Dictionary<DateTime, List<IConsumption>> dict = new Dictionary<DateTime, List<IConsumption>>();

            List<IConsumption> consumptions = new List<IConsumption>();
            for (int i = 4; i < 6; i++)
            {
                for (int j = 1; j <= 24; j++)
                {
                    Consumption consumption = new Consumption(i, 1234 + (i * 10), "RUS", DateTime.Parse("5/" + i + "/2020"));
                    consumptions.Add(consumption);
                }
                dict.Add(DateTime.Parse("5/" + i + "/2020"), consumptions);
                con.Add(consumptions);
                consumptions.Clear();
            }

            List<DateTime> days = new List<DateTime>();
            days.Add(DateTime.Parse(startDate));
            days.Add(DateTime.Parse("5/4/2020"));
            days.Add(DateTime.Parse(endDate));

            data.SetupGet(m => m.queries).Returns(querys);
            consumptionDAO.Setup(t => t.Read(geoArea, days)).Returns(dict);

            Assert.AreEqual(con, _dataCacheFunctions.GetData(startDate, endDate, geoArea, consumptionDAO.Object, data.Object));
        }

        [Test]
        public void GetDataFromDataBaseSaViseDanaPrvi()
        {
            data.Reset();
            consumptionDAO.Reset();

            string startDate = "5/3/2020";
            string endDate = "5/5/2020";
            string geoArea = "RUS";

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();
            List<List<IConsumption>> con = new List<List<IConsumption>>();
            Dictionary<DateTime, List<IConsumption>> dict = new Dictionary<DateTime, List<IConsumption>>();

            List<IConsumption> consumptions = new List<IConsumption>();
            for (int i = 3; i < 5; i++)
            {
                for (int j = 1; j <= 24; j++)
                {
                    Consumption consumption = new Consumption(i, 1234 + (i * 10), "RUS", DateTime.Parse("5/" + i + "/2020"));
                    consumptions.Add(consumption);
                }
                dict.Add(DateTime.Parse("5/" + i + "/2020"), consumptions);
                con.Add(consumptions);
                consumptions.Clear();
            }

            List<DateTime> days = new List<DateTime>();
            days.Add(DateTime.Parse(startDate));
            days.Add(DateTime.Parse("5/4/2020"));
            days.Add(DateTime.Parse(endDate));

            data.SetupGet(m => m.queries).Returns(querys);
            consumptionDAO.Setup(t => t.Read(geoArea, days)).Returns(dict);

            Assert.AreEqual(con, _dataCacheFunctions.GetData(startDate, endDate, geoArea, consumptionDAO.Object, data.Object));
        }

        [Test]
        public void GetDataFromDataBaseNema()
        {
            data.Reset();
            consumptionDAO.Reset();

            string startDate = "5/5/2020";
            string endDate = "5/5/2020";
            string geoArea = "RUS";

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();
            List<List<IConsumption>> con = new List<List<IConsumption>>();
            Dictionary<DateTime, List<IConsumption>> dict = new Dictionary<DateTime, List<IConsumption>>();

            List<DateTime> days = new List<DateTime>();
            days.Add(DateTime.Parse(startDate));

            data.SetupGet(m => m.queries).Returns(querys);
            consumptionDAO.Setup(t => t.Read(geoArea, days)).Returns(dict);

            Assert.AreEqual(null, _dataCacheFunctions.GetData(startDate, endDate, geoArea, consumptionDAO.Object, data.Object));
        }

        //private DataCache.IDataCacheFunctions novi;// _product;

        

        [Test]
        public void CheckDeleteCache()
        {
            data.Reset();
            consumptionDAO.Reset();
            Mock<DataCache.IDataCacheFunctions> dataCacheDouble = new Mock<DataCache.IDataCacheFunctions>();


            /*string startDate = "6/4/2021 09:15:25 AM";
            string endDate = "6/4/2021 09:15:45 AM";*/

            string startDate = "6/3/2021 03:40:25 AM";
            string endDate = "6/4/2021 09:15:45 AM";
            string geoArea = "RUS";

            Dictionary<Query, Dictionary<DateTime, List<IConsumption>>> querys = new Dictionary<Query, Dictionary<DateTime, List<IConsumption>>>();
            Dictionary<DateTime, List<IConsumption>> dict = new Dictionary<DateTime, List<IConsumption>>();
            List<List<IConsumption>> con = new List<List<IConsumption>>();

            List<IConsumption> consumptions = new List<IConsumption>();
            for (int i = 3; i < 5; i++)
            {
                for (int j = 1; j <= 24; j++)
                {
                    Consumption consumption = new Consumption(i, 1234 + (i * 10), "RUS", DateTime.Parse("6/" + i + "/2021"));
                    consumptions.Add(consumption);
                }
                dict.Add(DateTime.Parse("6/" + i + "/2021"), consumptions);
                con.Add(consumptions);
                consumptions.Clear();
            }

            Query q = new Query(startDate, endDate, geoArea);
            //querys.Add(q, dict);

            List<DateTime> days = new List<DateTime>();
            days.Add(DateTime.Parse("6/3/2021"));
            days.Add(DateTime.Parse("6/4/2021"));

            data.SetupGet(m => m.queries).Returns(querys);
            consumptionDAO.Setup(t => t.Read(geoArea, days)).Returns(dict);
            Assert.AreEqual(con, _dataCacheFunctions.GetData("6/3/2021", "6/4/2021", geoArea, consumptionDAO.Object, data.Object));
            _dataCacheFunctions.DeleteCache(data.Object);
            //Thread.Sleep(15000);
            //dataCacheDouble.Verify(t => t.DeleteCache(data.Object), Times.Once);



        }




    }
}
