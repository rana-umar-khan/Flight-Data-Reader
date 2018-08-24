using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader();
        }
        static void Reader()
        {
            string[] inp = new string[1];
            inp[0] = "C:\\Users\\umer\\Downloads\\raw2.dat";
            
            FlightDataReader fdr = new FlightDataReader(inp);
            fdr.StartReading();
        }
    }
}
