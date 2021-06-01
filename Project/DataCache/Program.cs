using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCache
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                /*Task.Run(() => { DataCacheFunctions.DeleteCache();});
                Thread.Sleep(10800000);*/
                Thread tesThread = new Thread(DataCacheFunctions.DeleteCache);
                tesThread.IsBackground = true;
                tesThread.Start();
            }
            
        }
    }
}
