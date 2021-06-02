using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWriter
{
    public class Validation
    {
        public string ValidateString(int i,string[] row)//mozda jos dodati za public static string reg;
        {
            string ret = "good";
            if(i < 0 || i > 24) //jesam
            {
                throw new ArgumentOutOfRangeException("Number out of range");
            }
            //br dobar...
            if (row == null) //jesam
            {
                throw new ArgumentNullException("Row cant be null");
            }
            else if (row.Length != 3)//jesam
            {
                throw new ArgumentException("Row has to have 3 elements");
            }
            else if (i == 0 && row[0]== "Sat" && row[1] =="Load" && row[2] == "Oblast") //jesam
            {
                ret = "skip";
            }
            else if (i != 0 && row[0] == "Sat" && row[1] == "Load" && row[2] == "Oblast") //jesam
            {
                throw new ArgumentException("This cant be in this row");
            }
            else
            {

                try
                {
                    int hour = Int32.Parse(row[0]);
                    int power = Int32.Parse(row[1]);
                    if (hour != i)
                    {
                        ret = "hourMissing";//za ovaj slucaj treba upisati u audio
                    }
                    if (power < 0)
                    {
                        ret = "notNumbers";
                    }
                }
                catch
                {
                    ret = "notNumbers";
                }
                
                    
            }
            return ret;
        }
        

        public DateTime ValidateDate(string fileName)// 1."    " ,2."" ,3. null , 4. "asdasdasdasddasd" ,5. "123_123_123_123.csv"
        {
            if (fileName.Trim() == "")
            {
                throw new ArgumentException("Datum u obliku stringa mora da sadrzi karaktere");//ovo nikad nece uci jer mora posalti nesto
            }
            if(fileName == null)
            {
                throw new ArgumentNullException("Datum ne smije biti null"); //ovo nece nikad uci jer mora poslat nesto
            }

            string[] date = fileName.Split('_', '.');

            if(date.Length <= 4 || date.Length > 5)
            {
                throw new ArgumentException("Nakon splita date treba imati 5 clanova");
            }
            if (date[0] != "prog" && date[0] != "ostv")
            {
                throw new ArgumentException("Nije dobar file datoteka");
            }
            if (date[4] != "csv")
            {
                throw new ArgumentException("Nije csv datoteka");
            }
                
            string day = $"{date[1]}/{date[2]}/{date[3]}";

            DateTime time;

            if (!DateTime.TryParse(day,out time))
            {
                throw new ArgumentException("Nije dobar format datuma");
            }

            return time;
        }
    }
}
