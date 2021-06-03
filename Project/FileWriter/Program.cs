using DataAccess.Model;
using DataAccess.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWriter
{
    public class Program
    {
        private static readonly ConsumptionService consumptionService = new ConsumptionService();
        private static ExtractData extract = new ExtractData();
        private static Validation validation = new Validation();

        public string Write(string path,string time)
        {
            string ret = "good";
            DateTime day = validation.ValidateDate(time);//nemam tu provjera
            List<IConsumption> newDate = extract.ReadFile(path, day);

            if (newDate != null)
            {
                if (!consumptionService.Write(newDate))
                    ret = "dateExists";
            }
            else
            {
                ret = "fileFormat";
            }

            return ret;
        }

        public List<string> ReadAllCountrys()
        {
            return consumptionService.FindAllCountrys();
        }      
    }
}
