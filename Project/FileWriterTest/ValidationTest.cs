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
    public class ValidationTest
    {
        [Test]

        public void CheckValidationDate()
        {
            Validation validation = new Validation();
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("    ");
                });
            Assert.Throws<NullReferenceException>(
                () =>
                {
                    validation.ValidateDate(null);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("prog_2018_2018_07_05.csv");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("prog_2018_07_05");
                });
            DateTime timegood = DateTime.Parse("2018-07-05 00:00:00.000");
            Assert.AreEqual(timegood, validation.ValidateDate("prog_2018_07_05.csv"));
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("nesto_2018_07_05.xml");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("prog_2018_07_05.xml");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("prog_ss_07_05.csv");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("prog_2018_ss_05.csv");
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    validation.ValidateDate("prog_2018_07_ss.csv");
                });
            
        }

        [Test]
        public void CheckValidationString()
        {
            Validation validation = new Validation();

            string[] row = { "Sat", "Load", "Oblast" };
            string[] row1 = null;
            string[] row2 = { "Dan", "Biba" };
            string[] row3 = { "Svaki", "Biba","Dobro","Lose"};
            string[] row4 = { "ASD", "2000", "SRB"};
            string[] row5 = { "2", "ss", "SRB"};
            string[] row6 = { "2", "2000", "SRB" };
            string[] row7 = { "2", "-12", "SRB" };
            Assert.AreEqual("skip", validation.ValidateString(0, row));
            Assert.Throws<ArgumentException>(
               () =>
               {
                   validation.ValidateString(2, row);
               });
            Assert.Throws<ArgumentOutOfRangeException>(
               () =>
               {
                   validation.ValidateString(-2, row);
               });
            
            Assert.Throws<ArgumentOutOfRangeException>(
               () =>
               {
                   validation.ValidateString(25, row);
               });
            Assert.Throws<ArgumentNullException>(
               () =>
               {
                   validation.ValidateString(2, row1);
               });
            Assert.Throws<ArgumentException>(
               () =>
               {
                   validation.ValidateString(3, row2);
               });
            Assert.Throws<ArgumentException>(
               () =>
               {
                   validation.ValidateString(3, row3);
               });
            Assert.AreEqual("hourMissing", validation.ValidateString(3, row6));
            Assert.AreEqual("notNumbers", validation.ValidateString(2, row4));
            Assert.AreEqual("notNumbers", validation.ValidateString(2, row5));
            Assert.AreEqual("notNumbers", validation.ValidateString(2, row5));
            Assert.AreEqual("good", validation.ValidateString(2, row6));
        }
    }
}
