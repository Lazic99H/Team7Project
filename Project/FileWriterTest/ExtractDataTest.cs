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

        public void CheckReadFile1(string path,DateTime date)
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
        
        public void CheckReadFile3()
        {

            ExtractData extractData = new ExtractData();
           
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string endPath =startupPath.Replace("FileWriterTest", "UserInterface");
            DateTime time = new DateTime(2020, 5, 15);
            Assert.IsNotNull(extractData.ReadFile(endPath + "\\prog_2020_05_15.csv", time));
            DateTime time1 = new DateTime(2018, 5, 11);
            Assert.AreEqual(null, extractData.ReadFile(endPath + "\\prog_2018_05_11.csv", time1)); //fali sat
            DateTime time2 = new DateTime(2020, 5, 12);
            Assert.AreEqual(null, extractData.ReadFile(endPath + "\\prog_2020_05_12.csv", time2)); //ima 23 sata 
            DateTime time3 = new DateTime(2020, 5, 13);
            Assert.AreEqual(null, extractData.ReadFile(endPath + "\\prog_2020_05_13.csv", time3)); //umjesto broja sata je rijec
            DateTime time4 = new DateTime(2020, 5, 10);
            Assert.AreEqual(null, extractData.ReadFile(endPath + "\\prog_2020_05_10.csv", time4));//prazan
        }


    }
}
