using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;

namespace DataCache
{
    public class DataCacheFunctions
    {
        private static DataAccess.Service.ConsumptionService cs = new DataAccess.Service.ConsumptionService();

        public List<List<DataAccess.Model.Consumption>> CheckForQueries(string startDate, string endDate, string geoArea)
        {
            Data.Query query = new Data.Query(startDate, endDate, geoArea);
            if (Data.Data.queries.ContainsKey(query))
            {
                return Data.Data.queries[query].Values.ToList();
            }
            else
            {
                List<DateTime> days = new List<DateTime>();
                string[] starts = startDate.Split('/');
                string[] ends = endDate.Split('/');
                DateTime day1 = DateTime.Parse(startDate);
                DateTime day2 = DateTime.Parse(endDate);

                int n;
                if (int.Parse(starts[0]) == int.Parse(ends[0])) //isti mjesec
                {
                    n = int.Parse(ends[1]) - int.Parse(starts[1]) + 1;
                }
                else //u interfejsu je obezbjedjeno da mora biti krajnji datum veci od pocetnog
                {
                    string d = day2.Subtract(day1).ToString();
                    string[] dd = d.Split('.');
                    n = int.Parse(dd[0]) + 1;
                }

                bool found = false;
                foreach (Data.Query item in Data.Data.queries.Keys)
                {
                    List<List<DataAccess.Model.Consumption>> listToRet = new List<List<DataAccess.Model.Consumption>>();
                    if (DateTime.Compare(DateTime.Parse(item.StartDate), day1) <= 0 && DateTime.Compare(DateTime.Parse(item.EndDate), DateTime.Parse(endDate)) >= 0)
                    {
                        found = true;
                        for (int i=0; i < n; i++)
                        {
                            listToRet.Add(Data.Data.queries[item][day1.AddDays(i)]);
                        }
                        return listToRet;
                    }

                }
                

                if (!found)
                {
                    int day = int.Parse(starts[1]);
                    int month = int.Parse(starts[0]);
                    int year = int.Parse(starts[2]);
                    DateTime dayToAdd = new DateTime(year, month, day);
                    days.Add(dayToAdd);
                    for (int i = 1; i < n; i++)
                    {
                        days.Add(dayToAdd.AddDays(i));
                    }
                    List<List<DataAccess.Model.Consumption>> retList = ReadFromDataBase(geoArea, days, new Data.Query(startDate, endDate, geoArea), n);
                    return retList;
                }

                return null;
                
            }

        }

        /*public void WriteQuerieToCache(Data.Query query, List<DataAccess.Model.Consumption> consumptions)
        {
            Data.Data.queries[query] = consumptions;
        }*/

        public List<List<DataAccess.Model.Consumption>> ReadFromDataBase(string geoArea, List<DateTime> days, Data.Query query, int numOfDays)
        {
            Dictionary<DateTime, List<DataAccess.Model.Consumption>> dict = new Dictionary<DateTime, List<DataAccess.Model.Consumption>>();
            Dictionary<DateTime, List<DataAccess.Model.Consumption>> pomocni = new Dictionary<DateTime, List<DataAccess.Model.Consumption>>();

            dict = cs.Read(geoArea, days);
            bool isInBase = false;
            string retDate="";

            if(dict.Count==0 || dict == null)
            {
                retDate = "";
            }
            else if (dict.Count < numOfDays)
            {
                foreach (DateTime item in days)
                {
                    isInBase = false;
                    foreach (DateTime item2 in dict.Keys)
                    {
                        if(DateTime.Compare(item, item2) == 0)
                        {
                            isInBase = true;
                            pomocni.Add(item, dict[item]);
                            break;
                        }
                    }

                    if (!isInBase)
                    {
                        retDate = item.ToString();
                        break;
                    }
                }

                if (retDate != days[0].ToString())
                {
                    int p;
                    for (int i = 0; i < numOfDays; i++)
                    {
                        p = i + 1;
                        if(days[p].ToString() == retDate)
                        {
                            retDate = days[i].ToString();
                            break;
                        }
                    }
                }
            }
            else
            {
                retDate = days[numOfDays-1].ToString();
                pomocni = dict;
            }

            if (retDate == "")
            {
                return null;
            }
            else if(retDate == days[0].ToString())
            {
                List<List<DataAccess.Model.Consumption>> list = dict.Values.ToList();
                return list;
            }
            else
            {
                string[] strings = retDate.Split(' ');
                if (strings.Length == 3)
                {
                    retDate = strings[0];
                }
                query.EndDate = retDate;
                Data.Data.queries.Add(query, pomocni);
                List<List<DataAccess.Model.Consumption>> list = dict.Values.ToList();
                return list;
            }
            


                //consumptions.Add(DataAccess.Service.ConsumptionService.Read(geoArea, dateTime.AddDays(i)));
            


            //return list;
        }

        public static void DeleteCache()
        {
            foreach (Data.Query item in Data.Data.queries.Keys)
            {
                DateTime start = new DateTime();
                start = item.TimeSaved;
                if (item.TimeSaved >= start.AddMilliseconds(30000))
                {
                    Data.Data.queries.Remove(item);
                }
            }

            //Thread.Sleep(10800000);
            Thread.Sleep(60000);
        }
    }
}
