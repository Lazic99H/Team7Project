using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public interface IConsumption
    {
        int Hour { get; set; }
        int Load { get; set; }
        string Region { get; set; }
        DateTime Day { get; set; }


    }
}
