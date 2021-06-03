using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface IConsumptionDAO
    {
        Dictionary<DateTime, List<Consumption>> Read(string reg, List<DateTime> days);

        List<Consumption> Read(string reg, DateTime day);

        List<string> FindAllCountrys();

        bool Write(List<Consumption> newData);

        void SaveError(string message);
    }
}
