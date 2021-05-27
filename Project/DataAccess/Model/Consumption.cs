using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Consumption
    {
        public int Hour { get; set; }
        public int Load { get; set; }
        public string Region { get; set; }
        public DateTime Day { get; set; }

        public Consumption() { }

        public Consumption(int hour, int load, string region, DateTime day)
        {
            Hour = hour;
            Load = load;
            Region = region;
            Day = day;
        }
    }
}
