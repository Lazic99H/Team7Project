﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.DAO;

namespace DataCache
{
    public class DataCacheFunctions : IDataCacheFunctions
    {
        private static DataAccess.Service.ConsumptionService cs = new DataAccess.Service.ConsumptionService();
        private static Mutex mutex = new Mutex();

        public int GetN(string startDate, string endDate)
        {
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
            return n;
        }

        public List<List<DataAccess.Model.IConsumption>> CheckForQueries(string startDate, string endDate, string geoArea, Data.IData data)
        {
            Data.Query query = new Data.Query(startDate, endDate, geoArea);

            List<DateTime> days = new List<DateTime>();

            int n = GetN(startDate, endDate);

            bool found = false;
            foreach (Data.Query item in data.queries.Keys)
            {
                List<List<DataAccess.Model.IConsumption>> listToRet = new List<List<DataAccess.Model.IConsumption>>();
                if (DateTime.Compare(DateTime.Parse(item.StartDate), DateTime.Parse(startDate)) <= 0 && DateTime.Compare(DateTime.Parse(item.EndDate), DateTime.Parse(endDate)) >= 0)
                {
                    found = true;
                    for (int i = 0; i < n; i++)
                    {
                        listToRet.Add(data.queries[item][DateTime.Parse(startDate).AddDays(i)]);
                    }
                    return listToRet;
                }

            }

            if (!found)
            {
                return null ;
            }

            return null;
        }

        public List<List<DataAccess.Model.IConsumption>> GetData(string startDate, string endDate, string geoArea, IConsumptionDAO consumptionDAO, Data.IData data)
        {
            List<List<DataAccess.Model.IConsumption>> listRet = new List<List<DataAccess.Model.IConsumption>>();
            listRet = CheckForQueries(startDate, endDate, geoArea, data);

            if (listRet == null)
            {
                List<DateTime> days = new List<DateTime>();

                string[] starts = startDate.Split('/');
                int day = int.Parse(starts[1]);
                int month = int.Parse(starts[0]);
                int year = int.Parse(starts[2]);

                DateTime dayToAdd = new DateTime(year, month, day);
                days.Add(dayToAdd);

                int n = GetN(startDate, endDate);
                

                for (int i = 1; i < n; i++)
                {
                    days.Add(dayToAdd.AddDays(i));
                }
                List<List<DataAccess.Model.IConsumption>> retList = ReadFromDataBase(geoArea, days, new Data.Query(startDate, endDate, geoArea), n, consumptionDAO, data);
                return retList;
            }
            else
            {
                return listRet;
            }


        }

        /*public void WriteQuerieToCache(Data.Query query, List<DataAccess.Model.Consumption> consumptions)
        {
            Data.Data.queries[query] = consumptions;
        }*/

        public List<List<DataAccess.Model.IConsumption>> ReadFromDataBase(string geoArea, List<DateTime> days, Data.Query query, int numOfDays, IConsumptionDAO consumptionDAO, Data.IData data)
        {
            Dictionary<DateTime, List<DataAccess.Model.IConsumption>> dict = new Dictionary<DateTime, List<DataAccess.Model.IConsumption>>();
            Dictionary<DateTime, List<DataAccess.Model.IConsumption>> pomocni = new Dictionary<DateTime, List<DataAccess.Model.IConsumption>>();

            dict = consumptionDAO.Read(geoArea, days);
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
            else if(retDate == days[0].ToString() && numOfDays!=1)
            {
                List<List<DataAccess.Model.IConsumption>> list = dict.Values.ToList();
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
                data.queries.Add(query, pomocni);
                //Task t = new Task(JustSleep);
                //Thread t = new Thread(new ParameterizedThreadStart(JustSleep));
                //t.Start(data);
                StartTheThread(data);

                List<List<DataAccess.Model.IConsumption>> list = dict.Values.ToList();
                return list;
            }
          
        }

        public Thread StartTheThread(Data.IData data)
        {
            var t = new Thread(() => JustSleep(data));
            t.Start();
            return t;
        }

        public void JustSleep(Data.IData data)
        {
            //   int k = (int)Task.CurrentId;
            //     Task.Delay(10000);
            //Thread.Sleep(10800000);
            Thread.Sleep(10000);
            DeleteCache(data);
        }

        public void DeleteCache(Data.IData data)
        {
            mutex.WaitOne();
            try
            {
                foreach (Data.Query item in data.queries.Keys)
                {
                    DateTime start = item.TimeSaved.AddMilliseconds(5000);
                    string vreme = start.Subtract(item.TimeSaved).ToString();
                    string[] sati = vreme.Split(':');
                    int i = int.Parse(sati[2]);

                    if (i >= 5 )
                    {
                    data.queries.Remove(item);
                    }
                }
            }
            catch
            {

            }
            mutex.ReleaseMutex();

            //Thread.Sleep(10800000);
            //Thread.Sleep(60000);
        }
    }
}
