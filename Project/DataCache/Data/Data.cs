using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache.Data
{
    class Data
    {
        public static Dictionary<Query, Dictionary<DateTime, List<DataAccess.Model.Consumption>>> queries = new Dictionary<Query, Dictionary<DateTime, List<DataAccess.Model.Consumption>>>();
    }
}
