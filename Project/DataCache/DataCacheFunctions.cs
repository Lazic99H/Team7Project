using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DataCache
{
    public class DataCacheFunctions
    {
        public List<DataAccess.Model.Consumption> CheckForQueries(string startDate, string endDate, string geoArea)
        {
            Data.Query query = new Data.Query(startDate, endDate, geoArea);
            if (Data.Data.queries.ContainsKey(query))
            {
                return Data.Data.queries[query];
            }
            else
            {
                List<DataAccess.Model.Consumption> consumptions = ReadFromDataBase(startDate, endDate, geoArea);
                Data.Data.queries[query] = consumptions;
                return consumptions;
            }

        }

        /*public void WriteQuerieToCache(Data.Query query, List<DataAccess.Model.Consumption> consumptions)
        {
            Data.Data.queries[query] = consumptions;
        }*/

        public List<DataAccess.Model.Consumption> ReadFromDataBase(string startDate, string endDate, string geoArea)
        {
            //mora biti projera da li postoje dani
            //ako korisnik unese od 14 do 15 dobija samo 14 ispisan?
            string[] dates1 = startDate.Split('.');
            string[] dates2 = endDate.Split('.');
            List<DataAccess.Model.Consumption> consumptions = new List<DataAccess.Model.Consumption>();
            DateTime dateTime = DateTime.Parse(startDate);

            int n = int.Parse(dates2[0]) - int.Parse(dates1[0]);
            for (int i = 0; i < n; i++)
            {
                //consumptions.Add(DataAccess.Service.ConsumptionService.Read(geoArea, dateTime.AddDays(i)));
            }

            return consumptions;
        }

        public void DeleteCache()
        {
            foreach (Data.Query item in Data.Data.queries.Keys)
            {
                
            }
        }
    }
}
