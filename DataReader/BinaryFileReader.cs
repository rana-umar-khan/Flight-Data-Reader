using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    class BinaryFileReader
    {
        string fileName;
        string outPutFile;
        BitArray bitArray;
        public int TotalFrames;
        StreamWriter fs;

        public BinaryFileReader(string file)
        {
            fileName = file;
            bitArray = MakeBitArray(fileName);
            outPutFile = "output.csv";
            (outPutFile, FileMode.Create);
        }
      
        private BitArray MakeBitArray(string file)
        {
            BinaryReader bReader = new BinaryReader(File.Open(file, FileMode.Open));
            int len = Convert.ToInt32(bReader.BaseStream.Length);
            TotalFrames = (len * 8) / 65536;
            //get All bytes in Byte[] then make bitarray
            Byte[] byteArray = bReader.ReadBytes(len);
            BitArray b1 = new BitArray(byteArray);

            return b1;
        }

        public void ReadFrame(A320_213_Param[] param, int frameNumber)
        {
            for (int i = 0; i < param.Length; i++)
            {
                fs.Write(param[i].MNEMONIC, 0, 20);
            }


            int pos = ((frameNumber-1)*65536);
            //
            for (int i = 0; i < 4; i++)
            {
                BitArray ba = ExtractValue(bitArray, pos, 12);
                int identifier = ConvertBinaryToInt(ba);
                Console.WriteLine(identifier);
                pos += 16384;
            }

            return;


            for (int i = 0; i < 4; i++)
            {
                ReadSubframe(param, frameNumber,i);
            }
        }

        public void ReadSubframe(A320_213_Param[] p, int frame, int subframe)
        {
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].REC_SUBFRAME == subframe || p[i].REC_SUBFRAME==0 || ((p[i].REC_SUBFRAME)-4)==subframe || ((p[i].REC_SUBFRAME) - 2) == subframe)
                {
                    if (p[i].DATA_FORMAT == "BNR")
                    {

                    }
                    else if (p[i].DATA_FORMAT == "INT")
                    {

                    }
                    else if (p[i].DATA_FORMAT == "DIS")
                    {

                    }
                }
            }
        }
        private void WriteInOutputFile(string message) { }

        public int ReadInteger(A320_213_Param param, int frameNumber)
        {

            return 1;
        }

        //helper
        public int CheckSubframesIdentifier(int frameNumber)
        {
            int pos = ((frameNumber - 1) * 65536);
            int[] s = new int[4];
            for (int i = 0; i < 4; i++)
            {
                BitArray ba = ExtractValue(bitArray, pos, 12);
                s[i] = ConvertBinaryToInt(ba);
                pos += 16384;
                //if (n == 0)
                //{
                //    pos =pos+ 12288;
                //}
                //else
                //{
                //    Console.WriteLine(ConvertBinaryToInt(ba));
                //    pos = pos + 12288;
                //}
            }
            if (s[0] != 583)
                return 1;
            if (s[1] != 1464)
                return 2;
            if (s[2] != 2631)
                return 3;
            if (s[3] != 3512)
                return 4;
            return 0;
        }
        private BitArray ExtractValue(BitArray b, int pos, int len)
        {
            BitArray ret = new BitArray(len);
            int k = 0;
            for (int i = pos; i < pos+len; i++)
            {
                ret[k] = b[i];
                k++;
            }
            //second byte first
            //BitArray r1 = new BitArray(len);
            //for (int l = 0; l < 8; l++)
            //{
            //    r1[l] = ret[l + 8];
            //}
            //for (int m = 8; m < 16; m++)
            //{
            //    r1[m] = ret[m - 8];
            //}

            return ret;
        }
        private int ConvertBinaryToInt(BitArray b)
        {
            StringBuilder builder = new StringBuilder
            {
                Length = b.Length
            };
            //int k = 8;
            //for (int i = 15; i > 7; i--)
            //{
            //    if (b[i])
            //        builder[k] = '1';
            //    else
            //        builder[k] = '0';
            //    k++;
            //}
            //k = 0;
            //for (int i = 7; i > -1; i--)
            //{
            //    if (b[i])
            //        builder[k] = '1';
            //    else
            //        builder[k] = '0';
            //    k++;
            //}

            int k = 0;
            for (int i = (b.Length-1); i >-1; i--)
            {
                if (b[i])
                {
                    builder[k] = '1';
                }
                else
                {
                    builder[k] = '0';
                }
                k++;
            }

            return Convert.ToInt32(builder.ToString(),2);
        }
    }
}