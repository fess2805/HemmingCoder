﻿using HemmingCoderLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemmingCoderLib.Coders
{
    public class HemmingCoder6_3 : IHemmingCoder
    {
        public HemmingCoder6_3()
        {

        }

        private int[,] _G = { {1, 0, 0, 1, 1, 0},
                              {0, 1, 0, 1, 0, 1},
                              {0, 0, 1, 0, 1, 1}};

        private int[] _sindroms = { 6, 5, 3, 4, 2, 1 };

        //private int[,] _P = { { 0, 1 }, { 0, 2 }, { 1, 2 } };

        private int[,] _H = { { 1, 1, 0, 1, 0, 0},
                              { 1, 0, 1, 0, 1, 0},
                              { 0, 1, 1, 0, 0, 1 }};
        private int k = 3;
        private int n = 6;
        private int p = 3;


        public int[,] G => _G;
        public int[] Sindroms => _sindroms;
        //public int[,] P => _P;

        public byte[] Decode(byte[] buffer)
        {
            var sizeBit = buffer.Length * 8;
            var ostSizeBit = sizeBit % n;
            sizeBit -= ostSizeBit;
            var resultBuffer = new byte[sizeBit];
            var resultOffset = 0;
            for (int i = 0; i < sizeBit; i += n)
            {
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 1));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 2));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 3));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 4));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 5));
                resultOffset++;

                var sindrom = (byte)(AdditionalFunctions.GetBit(buffer, i)
                    ^ AdditionalFunctions.GetBit(buffer, i + 1)
                    ^ AdditionalFunctions.GetBit(buffer, i + 3)) << 2;
                sindrom += (byte)(AdditionalFunctions.GetBit(buffer, i) ^ AdditionalFunctions.GetBit(buffer, i + 2) ^ AdditionalFunctions.GetBit(buffer, i + 4)) << 1;
                sindrom += (byte)(AdditionalFunctions.GetBit(buffer, i + 1) ^ AdditionalFunctions.GetBit(buffer, i + 2) ^ AdditionalFunctions.GetBit(buffer, i + 5));

                if (sindrom != 0)
                {
                    var offsetCoderWorld = resultOffset - n;
                    for (var s = 0; s < n; s++)
                    {
                        if (_sindroms[s] == sindrom)
                        {
                            AdditionalFunctions.SetBit(resultBuffer, offsetCoderWorld + s, (byte)(AdditionalFunctions.GetBit(resultBuffer, offsetCoderWorld + s) ^ 1));
                            break;
                        }
                    }
                }
                resultOffset -= p;
            }
            var newLen = (int)Math.Ceiling((decimal)resultOffset / 8);
            Array.Resize(ref resultBuffer, newLen);
            return resultBuffer;
        }

        public byte[] Encode(byte[] buffer)
        {
            var sizeBit = buffer.Length * 8;
            var resultBuffer = new byte[sizeBit * 2];
            var resultOffset = 0;
            for (int i = 0; i < sizeBit; i += k)
            {
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 1));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, AdditionalFunctions.GetBit(buffer, i + 2));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, (byte)(AdditionalFunctions.GetBit(buffer, i) ^ AdditionalFunctions.GetBit(buffer, i + 1)));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, (byte)(AdditionalFunctions.GetBit(buffer, i) ^ AdditionalFunctions.GetBit(buffer, i + 2)));
                resultOffset++;
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, (byte)(AdditionalFunctions.GetBit(buffer, i + 1) ^ AdditionalFunctions.GetBit(buffer, i + 2)));
                resultOffset++;
            }
            var newLen = (int)Math.Ceiling((decimal)resultOffset / 8);
            Array.Resize(ref resultBuffer, newLen);
            return resultBuffer;
        }
    }
}
