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

        public static int FindCombination(byte[] buffer, byte[] combination)
        {
            var position = -1;
            for(int i=0; i<buffer.Length; i++)
            {
                if (buffer[i] == combination[0])
                {
                    position = 1;
                    for (int j = 1; j < combination.Length; j++)
                    {
                        if (buffer[i + j] != combination[j]) break;
                        position++;
                    }
                    if (position == combination.Length) return i;
                }
                
            }
            return -1;
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

        public static byte GetBit(this ushort @this, int offset)
        {
            if (offset > 15) return 0;
            return (byte)((@this >> offset) & 0x01);
        }

        public static ushort SetBit(this ushort @this, int offset, byte value)
        {
            if (offset > 15) return @this;
            if (@this.GetBit(offset) == value) return @this;
            return @this ^= (byte)(1 << offset);
        }

        public static byte GetBit(this uint @this, int offset)
        {
            if (offset > 31) return 0;
            return (byte)((@this >> offset) & 0x01);
        }

        public static uint SetBit(this uint @this, int offset, byte value)
        {
            if (offset > 31) return @this;
            if (@this.GetBit(offset) == value) return @this;
            return @this ^= (byte)(1 << offset);
        }
    }
}
