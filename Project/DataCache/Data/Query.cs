using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache.Data
{
    public class Query
    {
        private string startDate;
        private string endDate;
        private string geoArea;
        private DateTime timeSaved;

        public Query(string startDate, string endDate, string geoArea)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.geoArea = geoArea;
            //this.time = new DateTime();
            this.timeSaved = DateTime.Now;
        }

        public DateTime TimeSaved
        {
            get { return timeSaved; }
            //set { }
        }
    }
}
