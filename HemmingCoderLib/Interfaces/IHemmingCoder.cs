using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemmingCoderLib.Interfaces
{
    public interface IHemmingCoder
    {
        int[,] G { get; }
        int[] Sindroms { get; }
        int[,] P { get; }
        byte[] Encode(byte[] buffer);
        byte[] Decode(byte[] buffer);
    }
}
