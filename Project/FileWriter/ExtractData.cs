using DataAccess.DAO;
using DataAccess.Model;
using DataAccess.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace FileWriter
{
    public class ExtractData
    {
        public static Validation validation = new Validation();
       // private static ConsumptionService consumptionService = new ConsumptionService();//i ovo mjenjat

        public List<IConsumption> ReadFile(string path,DateTime time, IConsumptionDAO consumptionDAO)
        {
            if(path == null || path.Trim() == "")
            {
                return null;
            }

            if(time == new DateTime())
            {
                return null;
            }
            List<IConsumption> consumptions = new List<IConsumption>();
            //path = HostingEnvironment.MapPath(path); aha ovo je da pronadje putanju

            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";

            int i = 0;
            
            while ((line = sr.ReadLine()) != null)
            {
                //mogao bi ovdje while ako je i > 24 buum eror 
                string[] tokens = line.Split(' ', '\t');
                string var = validation.ValidateString(i, tokens);
                if (var == "skip")
                {
                     
                }
                else if(var == "good")
                {
                    Consumption temp = new Consumption(i, Int32.Parse(tokens[1]), tokens[2], time);
                    consumptions.Add(temp);
                }
                else if (var == "hourMissing")
                {
                    consumptionDAO.SaveError("Hour is missing");
                    consumptions = null;
                    return consumptions;
                }
                else
                {
                    consumptions = null;
                    return consumptions;
                }


                i++;
            }
            i--;
            if(i != 24)
            {
                consumptions = null;
            }

            return consumptions;
        }
    }
}
