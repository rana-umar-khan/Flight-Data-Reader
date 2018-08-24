using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    class FlightDataReader
    {
        private string[] InputFiles;
        A320_213_Param [] parameters;

        public FlightDataReader(string [] files) { 
            InputFiles = files;
            parameters = (new MyFDAEntities().A320_213_Param.ToArray());
        }

        public void StartReading() {
            for (int i = 0; i < InputFiles.Length; i++)
            {
                ReadFile(InputFiles[i]);
            }
        }

        //public void ShowBits()
        //{
        //    BinaryReader bReader = new BinaryReader(File.Open("C:\\Users\\umer\\Downloads\\raw2.dat", FileMode.Open));
        //    int len = Convert.ToInt32(bReader.BaseStream.Length);
        //    int TotalFrames = (len * 8) / 49152;
        //    //get All bytes in Byte[] then make bitarray
        //    Byte[] byteArray = bReader.ReadBytes(len);
        //    BitArray b1 = new BitArray(byteArray);
        //    for (int i = 49152; i < (len*8); i++)
        //    {
        //        if (b1[i])
        //            Console.Write('1');
        //        else
        //            Console.Write('0');
        //    }
        //}

        private void ReadFile(string fileName)
        {
            BinaryFileReader b = new BinaryFileReader(fileName);
            int f= b.TotalFrames;
            
            for (int i = 1; i <f; i++)
            {
               b.ReadFrame(parameters, i);
            }
        }
    }
}
