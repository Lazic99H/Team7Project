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
            if(startDate==null || endDate==null || geoArea == null)
            {
                throw new ArgumentNullException("Arguments can't be null.");
            }
            if (startDate == "")
            {
                throw new ArgumentException("Starting date can't be empty.");
            }
            if (endDate == "")
            {
                throw new ArgumentException("Ending date can't be empty.");
            }
            if (geoArea == "")
            {
                throw new ArgumentException("Geographical area can't be empty.");
            }

            this.startDate = startDate;
            this.endDate = endDate;
            this.geoArea = geoArea;
            //this.time = new DateTime();
            this.timeSaved = DateTime.Now;
        }

       

        public DateTime TimeSaved
        {
            get { return timeSaved; }
            //set { TimeSaved = value; }
        }

        public string StartDate
        {
            get { return startDate; }
            //set { }
        }

        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public string GeoArea
        {
            get { return geoArea; }
            //set { }
        }
    }
}
