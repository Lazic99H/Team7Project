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
        List<Consumption> Read(string reg, DateTime day);

        bool Write(List<Consumption> newData);

        void SaveError(string message);
    }
}
