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
        private static DataAccess.Service.ConsumptionService cs = new DataAccess.Service.ConsumptionService();

        public void CheckForQueries(string startDate, string endDate, string geoArea)
        {
            Data.Query query = new Data.Query(startDate, endDate, geoArea);
            if (Data.Data.queries.ContainsKey(query))
            {
                //return Data.Data.queries[query];
            }
            else
            {
                List<DateTime> days = new List<DateTime>();
                string[] dates1 = startDate.Split('.');
                string[] dates2 = endDate.Split('.');
                int n = int.Parse(dates2[0]) - int.Parse(dates1[0]);

                DateTime day = DateTime.Parse(startDate);
                days.Add(day);
                for (int i = 1; i < n+1; i++)
                {
                   // DateTime novi = new DateTime(int.Parse(dates1[0])+i, int.Parse(dates1[1]), int.Parse(dates1[2]));
                    days.Add(new DateTime(int.Parse(dates1[0]) + i, int.Parse(dates1[1]), int.Parse(dates1[2])));
                }
                ReadFromDataBase(geoArea, days, new Data.Query(startDate, endDate, geoArea));
                /*Data.Data.queries[query] = consumptions;
                return consumptions;*/
            }

        }

        /*public void WriteQuerieToCache(Data.Query query, List<DataAccess.Model.Consumption> consumptions)
        {
            Data.Data.queries[query] = consumptions;
        }*/

        public void ReadFromDataBase(string geoArea, List<DateTime> days, Data.Query query)
        {
            //mora biti projera da li postoje dani
            //PROVJERITI DA LI DICT VRATI NULL I KOLIKO CLANOVA IMA, TJ DA LI IMA SVE DANE
            //ako korisnik unese od 14 do 15 dobija samo 14 ispisan?
            /*string[] dates1 = startDate.Split('.');
            string[] dates2 = endDate.Split('.');
            List<DataAccess.Model.Consumption> consumptions = new List<DataAccess.Model.Consumption>();
            DateTime dateTime = DateTime.Parse(startDate);

            int n = int.Parse(dates2[0]) - int.Parse(dates1[0]);
            for (int i = 0; i < n; i++)
            {
                consumptions.Add(DataAccess.Service.ConsumptionService.Read(geoArea, dateTime.AddDays(i)));
            }*/

            Dictionary<DateTime, List<DataAccess.Model.Consumption>> dict = new Dictionary<DateTime, List<DataAccess.Model.Consumption>>();

            dict = cs.Read(geoArea, days);
            //Dictionary<Data.Query, Dictionary<DateTime, List<DataAccess.Model.Consumption>>> kez = new Dictionary<Data.Query, Dictionary<DateTime, List<DataAccess.Model.Consumption>>>();
            //List<List<DataAccess.Model.Consumption>> list = dict.ToList<List<DataAccess.Model.Consumption>>();
            Data.Data.queries.Add(query, dict);


            //return list;
        }

        public void DeleteCache()
        {
            foreach (Data.Query item in Data.Data.queries.Keys)
            {
                DateTime start = new DateTime();
                start = item.TimeSaved;
                if (item.TimeSaved >= start.AddHours(3))
                {
                    Data.Data.queries.Remove(item);
                }
            }
        }
    }
}
