// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var buffer = new byte[1024];
var coder = HemmingCoderLib.HemmingCoder.CreateCoder(11, 7);
var result = coder.Encode(buffer);
var offset = 0;
for (int i =0; i< result.Length; i += 2)
{
    result[i] ^= (byte)(1 << offset);
    offset = offset > 7 ? 0 : offset + 1;

}
var decodeBuffer = coder.Decode(result);
Console.ReadLine();
