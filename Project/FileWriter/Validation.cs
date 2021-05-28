using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWriter
{
    public class Validation
    {
        public string ValidateString(int i,string[] row)
        {
            string ret = "good";
            if(row[0]== "Sat" && row[1] =="Load" && row[2] == "Oblast")
            {
                ret = "skip";
            }
            else 
            {
                try
                {
                    int hour = Int32.Parse(row[0]);
                    int power = Int32.Parse(row[1]);
                    if(hour != i)
                    {
                        ret = "hourMissing";//za ovaj slucaj treba upisati u audio
                    }
                }
                catch
                {
                    ret = "notNumbers";
                }
            }


            return ret;
        }
    }
}
