using DataAccess.Model;
using FileWriter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [TestCase("C:\\Users\\acopr\\OneDrive\\Документи\\GitHub\\Team7Project\\csv\\prog_2020_05_10.csv","2018-05-10")] //prazan

        public void CheckReadFile3(string path,DateTime date)
        {
            ExtractData extractData = new ExtractData();
            Assert.AreEqual(null, extractData.ReadFile(path, date));
        }

        [Test]

        [TestCase("C:\\Users\\acopr\\OneDrive\\Документи\\GitHub\\Team7Project\\csv\\prog_2020_05_03.csv", "2018-05-03")]//dobar

        public void CheckReadFile4(string path, DateTime date)
        {

            ExtractData extractData = new ExtractData();
            Assert.IsNotNull(extractData.ReadFile(path, date));
        }

        [Test]

        [TestCase("C:\\Users\\acopr\\OneDrive\\Документи\\GitHub\\Team7Project\\csv\\prog_2018_05_11.csv", "2018-05-11")] //fali sat
        [TestCase("C:\\Users\\acopr\\OneDrive\\Документи\\GitHub\\Team7Project\\csv\\prog_2020_05_12.csv", "2020-05-12")] //ima 23 sata 
        [TestCase("C:\\Users\\acopr\\OneDrive\\Документи\\GitHub\\Team7Project\\csv\\prog_2020_05_13.csv", "2020-05-13")] //umjesto broja sata je rijec

        public void CheckReadFile5(string path, DateTime date)
        {
            ExtractData extractData = new ExtractData();
            Assert.AreEqual(null, extractData.ReadFile(path, date));
        }

    }
}
