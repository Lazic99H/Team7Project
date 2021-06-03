using DataAccess.DAO;
using DataAccess.DAO.Implementation;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    [ExcludeFromCodeCoverage]
    public class ConsumptionService
    {
        private static readonly IConsumptionDAO consumptionDAO = new ConsumptionDAOImpl();
        //private static readonly ConsumptionService consumptionService = new ConsumptionService();

        public List<string> FindAllCountrys()
        {
            return consumptionDAO.FindAllCountrys();
        }

        public List<Consumption> ReadDay(string reg, DateTime day)//za jedan dan null ako nema, lista ako ima
        {
            return consumptionDAO.Read(reg, day);
        }

        public Dictionary<DateTime, List<Consumption>> Read(string reg, List<DateTime> days)//vraca null ako nema trazenih potrosnjih za odabrani opseg ili dictinary ako ima..
        {
            return consumptionDAO.Read(reg, days);
        }

        public bool Write(List<Consumption> newDate)//vraca true ako je upisano dobro ili false ako nije
        {
            if (consumptionDAO.Write(newDate))
                return true;
            else
                return false;
        }
        
        public void SaveError(string msg)//zapis u audit tabelu
        {
            consumptionDAO.SaveError(msg);
        }
    }
}
