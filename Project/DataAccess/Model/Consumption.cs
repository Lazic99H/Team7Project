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
            if(region == null || day == new DateTime())
            {
                throw new ArgumentNullException("Argumenti ne smeju biti null");
            }
            if(region.Trim() == "")
            {
                throw new ArgumentException("Region ne smije biti prazan string");
            }
            if (hour < 1 || hour > 24)
            {
                throw new ArgumentOutOfRangeException("Hour mora biti u opsegu od 1 do 24");
            }
            if (load < 1)
            {
                throw new ArgumentOutOfRangeException("load mora biti u opsegu od 1 do 24");
            }
            Hour = hour;
            Load = load;
            Region = region;
            Day = day;
        }
    }
}
