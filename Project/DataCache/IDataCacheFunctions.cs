using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCache
{
    public interface IDataCacheFunctions
    {


        int GetN(string startDate, string endDate);

        List<List<DataAccess.Model.IConsumption>> CheckForQueries(string startDate, string endDate, string geoArea, Data.IData data);


        List<List<DataAccess.Model.IConsumption>> GetData(string startDate, string endDate, string geoArea, IConsumptionDAO consumptionDAO, Data.IData data);


        List<List<DataAccess.Model.IConsumption>> ReadFromDataBase(string geoArea, List<DateTime> days, Data.Query query, int numOfDays, IConsumptionDAO consumptionDAO, Data.IData data);


        Thread StartTheThread(Data.IData data);


        void JustSleep(Data.IData data);


        void DeleteCache(Data.IData data);
        
    }
}
