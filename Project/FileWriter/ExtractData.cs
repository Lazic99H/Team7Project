﻿using DataAccess.Model;
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
        private static ConsumptionService consumptionService = new ConsumptionService();

        public List<Consumption> ReadFile(string path,DateTime time)
        {
            List<Consumption> consumptions = new List<Consumption>();
            //path = HostingEnvironment.MapPath(path); aha ovo je da pronadje putanju
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";

            int i = 1;

            while ((line = sr.ReadLine()) != null)
            {
                //mogao bi ovdje while ako je i > 24 buum eror 
                string[] tokens = line.Split(' ', '\t');
                string var = validation.ValidateString(i, tokens);
                if (var == "skip")
                {
                    i--; 
                }
                else if(var == "good")
                {
                    Consumption temp = new Consumption(i, Int32.Parse(tokens[1]), tokens[2], time);
                    consumptions.Add(temp);
                }
                else if (var == "hourMissing")
                {
                    consumptionService.SaveError("Hour is missing");
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
                return consumptions;
        }
    }
}
