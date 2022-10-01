using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemmingCoderLib
{
    public class AdditionalFunctions
    {
        public static byte GetBit(byte[] buffer, int offset)
        {
            var numberByte = offset >> 3;
            var numberBit = offset % 8;
            if (numberByte >= buffer.Length) return 0;
            return buffer[numberByte].GetBit(numberBit);
        }

        public static void SetBit(byte[] buffer, int offset, byte value)
        {
            var numberByte = offset >> 3;
            var numberBit = offset % 8;
            buffer[numberByte] = buffer[numberByte].SetBit(numberBit, value);
        }
    }

    public static class AdditionalFunctionsExtension
    {
        public static byte GetBit(this byte @this, int offset)
        {
            if (offset > 7) return 0;
            return (byte)((@this >> offset) & 0x01);
        }

        public static byte SetBit(this byte @this, int offset, byte value)
        {
            if (offset > 7) return @this;
            if (@this.GetBit(offset) == value) return @this;
            return @this ^= (byte)(1 << offset);
        }
    }
}
