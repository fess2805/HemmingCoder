using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemmingCoderLib.Mux
{
    public class MatrixMux
    {
        private int _N;        
        public MatrixMux(int n)
        {
            _N = n;            
        }

        public byte[] Mux(byte[] buffer)
        {
            var sizeBit = buffer.Length * 8;
            var sizeOfMuxBuffer = _N * _N;
            var muxBuffer = new byte[sizeOfMuxBuffer];
            var muxPosition = 0;
            var numberBit = 0;
            var resultBuffer = new byte[ 2 * buffer.Length];
            var resultOffset = 0;
            while (true)
            {
                if (numberBit >= sizeBit && muxPosition == 0) break;
                muxBuffer[muxPosition] =  (byte)(numberBit < sizeBit ? AdditionalFunctions.GetBit(buffer, numberBit) : 0);
                muxPosition += _N;
                if (muxPosition >= sizeOfMuxBuffer) 
                { 
                    muxPosition = muxPosition % _N + 1;
                    if (muxPosition >= _N)
                    {
                        //сохраним данные в результирующий массив
                        for (int i = 0; i < sizeOfMuxBuffer; i++)
                        {
                            AdditionalFunctions.SetBit(resultBuffer, resultOffset, muxBuffer[i]);
                            resultOffset++;
                        }
                        muxPosition = 0;
                    }
                }                
                numberBit++;
            }
            var newLen = (int)Math.Ceiling((decimal)resultOffset / 8);
            Array.Resize(ref resultBuffer, newLen);
            return resultBuffer;
        }

        public byte[] DeMux(byte[] buffer)
        {
            var sizeBit = buffer.Length * 8;
            var sizeOfMuxBuffer = _N * _N;
            var muxBuffer = new byte[sizeOfMuxBuffer];
            var muxPosition = 0;
            var numberBit = 0;
            var resultBuffer = new byte[2 * buffer.Length];
            var resultOffset = 0;
            while (true)
            {
                if (numberBit >= sizeBit) break;
                muxBuffer[muxPosition] = (byte)(numberBit < sizeBit ? AdditionalFunctions.GetBit(buffer, numberBit) : 0);
                muxPosition++;
                if (muxPosition >= sizeOfMuxBuffer)
                {
                    muxPosition = 0;
                    //сохраним данные в результирующий массив
                    for (int i = 0; i < sizeOfMuxBuffer; i++)
                    {
                        AdditionalFunctions.SetBit(resultBuffer, resultOffset, muxBuffer[muxPosition]);
                        muxPosition = (muxPosition + _N) >= sizeOfMuxBuffer ? muxPosition % _N + 1 : muxPosition + _N;
                        resultOffset++;
                    }
                    muxPosition = 0;                    
                }
                numberBit++;
            }
            var newLen = (int)Math.Ceiling((decimal)resultOffset / 8);
            Array.Resize(ref resultBuffer, newLen);
            return resultBuffer;
        }


    }
}
