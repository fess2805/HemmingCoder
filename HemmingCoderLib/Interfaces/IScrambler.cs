using System;
namespace HemmingCoderLib.Interfaces
{
    public interface IScrambler
    {
        byte[] Scramble(byte[] buffer);
        byte[] DeScramble(byte[] buffer);
    }
}

