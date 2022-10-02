// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var buffer = new byte[102400];
var coder = HemmingCoderLib.HemmingCoder.CreateCoder(7, 4);
var mux = new HemmingCoderLib.Mux.MatrixMux(3);
var scr = new HemmingCoderLib.Scramblers.ADScrambler_0_14_15();
//buffer[0] = 5;
var coderBuffer = coder.Encode(buffer);
var offset = 0;
for (int i =0; i< coderBuffer.Length; i += 2)
{
    coderBuffer[i] ^= (byte)(1 << offset);
    offset = offset > 7 ? 0 : offset + 1;
}
var coderBufferLen = coderBuffer.Length;
Array.Resize(ref coderBuffer, coderBuffer.Length + 10);

for (var i = 0; i < 10; i++) coderBuffer[i + coderBufferLen] = (byte)(i + 1);
var muxBuffer = mux.Mux(coderBuffer);
var scramblerBuffer = scr.Scr(muxBuffer);

var demuxBuffer = mux.DeMux(muxBuffer);




var decodeBuffer = coder.Decode(coderBuffer);
Console.ReadLine();
