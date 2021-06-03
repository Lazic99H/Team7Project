using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache.Data
{
    public interface IData
    {
        Dictionary<Query, Dictionary<DateTime, List<DataAccess.Model.IConsumption>>> queries {  get; set; }
    }
}
