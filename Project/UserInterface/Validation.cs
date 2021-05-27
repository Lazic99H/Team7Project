using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    public class Validation
    {
        public bool ValidateFileName(string name)
        {
            string[] ime = name.Split('_');
            if (ime.Length < 4)
            {
                return false;
            }else if (int.Parse(ime[2])<1 && int.Parse(ime[2])>31)
            {
                return false;
            }
            else if (int.Parse(ime[3]) < 1 && int.Parse(ime[3]) > 12)
            {
                return false;
            }
            else if (int.Parse(ime[1]) < 2014 && int.Parse(ime[1]) > 2021)
            {
                return false;
            }

            return true;
        }
    }
}
