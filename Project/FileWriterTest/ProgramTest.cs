using FileWriter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWriterTest
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]

        public void CheckWrite()
        {
            Program program = new Program();
            Assert.Throws < ArgumentNullException>(
                () =>
                {
                    program.Write("putanja", null);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "    ");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "");
                });

            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_2018_07_05.csv");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_07_05");
                });        
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "nesto_2018_07_05.xml");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_07_05.xml");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_ss_07_05.csv");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_ss_05.csv");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_07_ss.csv");
                });
            //gore sve provjere za datum e sad putanja;
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string endPath = startupPath.Replace("FileWriterTest", "UserInterface");
       //     Assert.AreEqual("good", program.Write(endPath + "\\prog_2020_05_16.csv", "prog_2020_05_16.csv"));
    //        Assert.AreEqual("dateExists", program.Write(endPath + "\\prog_2020_05_16.csv", "prog_2020_05_16.csv"));
            Assert.AreEqual("fileFormat", program.Write(endPath + "\\prog_2020_05_17.csv", "prog_2020_05_17.csv"));
        }

        [Test]
        
        public void CheckReadAllCountrys()
        {
            Program program = new Program();

            Assert.IsNotNull(program.ReadAllCountrys());
        }
    }
}
