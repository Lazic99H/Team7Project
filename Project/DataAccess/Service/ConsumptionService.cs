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
    private static IConsumptionDAO c = new ConsumptionDAOImpl();

    public class ConsumptionService
    {
        public List<Consumption> Read()
        {
            return consumptionDAO.
        }

    }
}
