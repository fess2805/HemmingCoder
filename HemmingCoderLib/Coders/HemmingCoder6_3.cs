using HemmingCoderLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemmingCoderLib.Coders
{
    internal class HemmingCoder6_3: IHemmingCoder
    {
        private int[,] _G = { {1, 0, 0, 1, 1, 0},
                              {0, 1, 0, 1, 0, 1},
                              {0, 0, 1, 0, 1, 1}};

        private int[] _sindroms = { 6, 5, 3, 4, 2, 1 };

        private int[,] _P = { { 0, 1 }, { 0, 1 }, { 1, 2 } };
        private int k = 3;
        private int n = 6;


        public int[,] G => _G;
        public int[] Sindroms => _sindroms;
        public int[,] P => _P;

        public byte[] Encode(byte[] buffer)
        {
            var sizeBit = buffer.Length * 8;
            var resultBuffer = new byte[sizeBit * 2];
            var resultOffset = 0;
            for(int i=0; i< sizeBit; i+=k)
            {
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 1));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 1));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, (byte)(AdditionalFunctions.GetBit(buffer, i) ^ AdditionalFunctions.GetBit(buffer, i + 1)));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, (byte)(AdditionalFunctions.GetBit(buffer, i) ^ AdditionalFunctions.GetBit(buffer, i + 2)));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, (byte)(AdditionalFunctions.GetBit(buffer, i + 1) ^ AdditionalFunctions.GetBit(buffer, i + 2)));
            }

            return resultBuffer;
        }

        public byte[] Decode(byte[] buffer)
        {
            return buffer;
        }
    }
}
