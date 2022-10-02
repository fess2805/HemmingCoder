using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemmingCoderLib.Scramblers
{
    public class ADScrambler_0_14_15
    {

        private ushort _registr = 169;
        public ADScrambler_0_14_15()
        {

        }

        public byte[] Scr(byte[] buffer)
        {
            var sizeBit = buffer.Length * 8;
            var resultBuffer = new byte[buffer.Length];
            var resultOffset = 0;
            for(int i=0; i < sizeBit; i++)
            {
                var bit = (byte)(_registr.GetBit(14)
                                ^ _registr.GetBit(13)
                                /*^ _registr.GetBit(0)*/);
                AdditionalFunctions.SetBit(resultBuffer, resultOffset,
                    (byte)(AdditionalFunctions.GetBit(buffer, i) 
                    ^ bit));
                resultOffset++;
                
            _registr = (ushort)(_registr << 1);
                _registr = _registr.SetBit(0, bit);
            }
            return resultBuffer;
        }
    }
}
