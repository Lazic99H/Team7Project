using DataAccess.DAO;
using DataAccess.DAO.Implementation;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    

    public class ConsumptionService
    {
        private static readonly IConsumptionDAO consumptionDAO = new ConsumptionDAOImpl();

        public List<Consumption> Read(string reg, DateTime day)
        {
            return consumptionDAO.Read(reg, day);
        }

        public bool Write(List<Consumption> newDate)
        {
            if (consumptionDAO.Write(newDate))
                return true;
            else
                return false;
        }
        
        public void SaveError(string msg)
        {
            consumptionDAO.SaveError(msg);
        }
    }
}
