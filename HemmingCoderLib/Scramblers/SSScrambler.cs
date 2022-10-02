using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemmingCoderLib.Scramblers
{
    public  class SSScrambler
    {
        private uint _registr = 58745;
        public SSScrambler()
        {

        }

        public byte[] Scr(byte[] buffer)
        {
            var sizeBit = buffer.Length * 8;
            var resultBuffer = new byte[buffer.Length];
            var resultOffset = 0;
            for (int i = 0; i < sizeBit; i++)
            {
                var bit = (byte)(_registr.GetBit(17)
                                ^ _registr.GetBit(22) 
                                ^ AdditionalFunctions.GetBit(buffer, i));
                AdditionalFunctions.SetBit(resultBuffer, resultOffset, bit);
                resultOffset++;

                _registr = (uint)(_registr << 1);
                _registr = _registr.SetBit(0, bit);
            }
            return resultBuffer;
        }
    }
}
