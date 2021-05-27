using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface IConsumptionDAO
    {
        Dictionary<DateTime, Consumption> Read(string reg, List<DateTime> days);

        List<Consumption> Read(string reg, DateTime day);

        bool Write(List<Consumption> newData);

        void SaveError(string message);
    }
}
