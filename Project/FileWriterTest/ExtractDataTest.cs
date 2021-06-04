using DataAccess.DAO;
using DataAccess.Model;
using FileWriter;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileWriterTest
{

    [TestFixture]
    public class ExtractDataTest
    {
        IConsumptionDAO consumptionDAOSave;

        [SetUp]
        public void SetUp()
        {
            var moq = new Mock<IConsumptionDAO>();
            moq.Setup(t => t.SaveError("Hour is missing"));
            consumptionDAOSave = moq.Object;
        }

            [Test]

        [TestCase(null, "2018-07-05")]                              //1
        [TestCase("", "2018-07-05")]                                //2
        [TestCase("     ", "2018-07-05")]                           //3
        [TestCase("asdas", null)]                                   //4

        public void CheckReadFile1(string path,DateTime date)
        {
            ExtractData extractData = new ExtractData();
            Assert.AreEqual(null, extractData.ReadFile(path, date, consumptionDAOSave));// 1
            Assert.AreEqual(null, extractData.ReadFile(path, date, consumptionDAOSave));// 2
            Assert.AreEqual(null, extractData.ReadFile(path, date, consumptionDAOSave));// 3
            Assert.AreEqual(null, extractData.ReadFile(path, date, consumptionDAOSave));// 4
           
        }

        [Test]
        [TestCase("asdas", "2018-07-05")]                           //5

        public void CheckReadFile2(string path, DateTime date)
        {
            ExtractData extractData = new ExtractData();
            Assert.Throws<FileNotFoundException>(
                   () =>
                    {
                        extractData.ReadFile(path, date, consumptionDAOSave);
                    });
        }

        [Test]
        
        public void CheckReadFile3()
        {

            ExtractData extractData = new ExtractData();
           
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string endPath =startupPath.Replace("FileWriterTest", "UserInterface");
            DateTime time = new DateTime(2020, 5, 15);
            Assert.IsNotNull(extractData.ReadFile(endPath + "\\prog_2020_05_15.csv", time, consumptionDAOSave));
            DateTime time1 = new DateTime(2018, 5, 11);
            Assert.AreEqual(null, extractData.ReadFile(endPath + "\\prog_2018_05_11.csv", time1, consumptionDAOSave)); //fali sat
            DateTime time2 = new DateTime(2020, 5, 12);
            Assert.AreEqual(null, extractData.ReadFile(endPath + "\\prog_2020_05_12.csv", time2, consumptionDAOSave)); //ima 23 sata 
            DateTime time3 = new DateTime(2020, 5, 13);
            Assert.AreEqual(null, extractData.ReadFile(endPath + "\\prog_2020_05_13.csv", time3, consumptionDAOSave)); //umjesto broja sata je rijec
            DateTime time4 = new DateTime(2020, 5, 10);
            Assert.AreEqual(null, extractData.ReadFile(endPath + "\\prog_2020_05_10.csv", time4, consumptionDAOSave));//prazan
        }


    }
}
