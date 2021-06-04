using DataAccess.DAO;
using DataAccess.Model;
using FileWriter;
using Moq;
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
        IConsumptionDAO consumptionDAOFalse;
        IConsumptionDAO consumptionDAOTrue;

        IConsumptionDAO consumptionDAOReadTrue;
        IConsumptionDAO consumptionDAOReadFalse;

        //      IConsumptionDAO 
        [SetUp]
        public void SetUp()
        {
            IConsumption consumption = new Consumption();
            List<IConsumption> List = new List<IConsumption>();
            List.Add(consumption);

            var moq = new Mock<IConsumptionDAO>();
            moq.Setup(t => t.Write(It.IsAny<List<IConsumption>>())).Returns(false);//ova lista me zeza
            consumptionDAOFalse = moq.Object;


            var moq2 = new Mock<IConsumptionDAO>();
            moq2.Setup(t => t.Write(It.IsAny<List<IConsumption>>())).Returns(true);//ova lista me jebe
            consumptionDAOTrue = moq2.Object;

            var moq3 = new Mock<IConsumptionDAO>();
            moq3.Setup(t => t.FindAllCountrys()).Returns(It.IsAny<List<string>>());
            consumptionDAOReadTrue = moq3.Object;

            var moq4 = new Mock<IConsumptionDAO>();
            moq4.Setup(t => t.FindAllCountrys()).Returns(new List<string>());
            consumptionDAOReadTrue = moq4.Object;
        }


        [Test]

        public void CheckWrite()
        {
            Program program = new Program();
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    program.Write("putanja", null, consumptionDAOFalse);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "    ", consumptionDAOFalse);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "", consumptionDAOFalse);
                });

            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_2018_07_05.csv", consumptionDAOFalse);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_07_05", consumptionDAOFalse);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "nesto_2018_07_05.xml", consumptionDAOFalse);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_07_05.xml", consumptionDAOFalse);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_ss_07_05.csv", consumptionDAOFalse);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_ss_05.csv", consumptionDAOFalse);
                });
            Assert.Throws<ArgumentException>(
                () =>
                {
                    program.Write("putanja", "prog_2018_07_ss.csv", consumptionDAOFalse);
                });

            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string endPath = startupPath.Replace("FileWriterTest", "UserInterface");
            Assert.AreEqual("good", program.Write(endPath + "\\prog_2020_05_16.csv", "prog_2020_05_16.csv", consumptionDAOTrue));
            Assert.AreEqual("dateExists", program.Write(endPath + "\\prog_2020_05_18.csv", "prog_2020_05_18.csv", consumptionDAOFalse));
            Assert.AreEqual("fileFormat", program.Write(endPath + "\\prog_2020_05_17.csv", "prog_2020_05_17.csv", consumptionDAOFalse));
        }

        [Test]

        public void CheckReadAllCountrys()
        {
            Program program = new Program();

            Assert.IsNotNull(program.ReadAllCountrys(consumptionDAOReadTrue));
            Assert.Throws<NullReferenceException>(
                () =>
                {
                    program.ReadAllCountrys(consumptionDAOReadFalse);
                });


        }
    }
}
