using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache
{
    public class Validate
    {
        public string ValidateEntry(string startDate, string endDate, string geoArea)
        {
            string returnMessage = "";
            if (endDate.Equals("") || startDate.Equals("") || geoArea.Equals(""))
            {
                returnMessage = "All fields must be filled in correctly";
            }
            else
            {
                string[] tos = startDate.Split('/');
                string[] ends = endDate.Split('/');
                if (int.Parse(tos[2]) > int.Parse(ends[2]))
                {
                    returnMessage = "Starting date must be lower then ending date";
                }
                else if (int.Parse(tos[2]) == int.Parse(ends[2]))
                {
                    if (int.Parse(tos[0]) > int.Parse(ends[0]))
                    {
                        returnMessage = "Starting date must be lower then ending date";
                    }
                    else if (int.Parse(tos[0]) == int.Parse(ends[0]) && int.Parse(tos[1]) > int.Parse(ends[1]))
                    {
                        returnMessage = "Starting date must be lower then ending date";
                    }

                }
            }
                return returnMessage;
        }
    }
}
