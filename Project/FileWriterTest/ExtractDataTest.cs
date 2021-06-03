using DataAccess.Model;
using FileWriter;
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
        [Test]

        [TestCase(null, "2018-07-05")]                              //1
        [TestCase("", "2018-07-05")]                                //2
        [TestCase("     ", "2018-07-05")]                           //3
        [TestCase("asdas", null)]                                   //4

        public void CheckReadFile(string path,DateTime date)
        {
            ExtractData extractData = new ExtractData();
            Assert.AreEqual(null, extractData.ReadFile(path, date));// 1
            Assert.AreEqual(null, extractData.ReadFile(path, date));// 2
            Assert.AreEqual(null, extractData.ReadFile(path, date));// 3
            Assert.AreEqual(null, extractData.ReadFile(path, date));// 4
           
        }

        [Test]
        [TestCase("asdas", "2018-07-05")]                           //5

        public void CheckReadFile2(string path, DateTime date)
        {
            ExtractData extractData = new ExtractData();
            Assert.Throws<FileNotFoundException>(
                   () =>
                    {
                        extractData.ReadFile(path, date);
                    });
        }

        [Test]

        [TestCase("C:\\PerfLogs\\prog_2020_05_10.csv", "2018-05-10")] //prazan

        public void CheckReadFile3(string path,DateTime date)
        {
            ExtractData extractData = new ExtractData();
            Assert.AreEqual(null, extractData.ReadFile(path, date));
        }

        [Test]
        


        public void CheckReadFile4()
        {

            ExtractData extractData = new ExtractData();
            DateTime time = new DateTime(2020, 5, 15);
            Assert.IsNotNull(extractData.ReadFile("C:\\Users\\acopr\\OneDrive\\Документи\\GitHub\\Team7Project\\Project\\UserInterface\\bin\\Debug\\prog_2020_05_15.csv", time));
        }

        [Test]

        [TestCase("C:\\PerfLogs\\prog_2018_05_11.csv", "2018-05-11")] //fali sat
        [TestCase("C:\\PerfLogs\\prog_2020_05_12.csv", "2020-05-12")] //ima 23 sata 
        [TestCase("C:\\PerfLogs\\prog_2020_05_13.csv", "2020-05-13")] //umjesto broja sata je rijec

        public void CheckReadFile5(string path, DateTime date)
        {
            ExtractData extractData = new ExtractData();
            Assert.AreEqual(null, extractData.ReadFile(path, date));
        }

    }
}
