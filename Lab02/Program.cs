// See https://aka.ms/new-console-template for more information
using HemmingCoderLib;

Console.WriteLine("Hello, World!");

var buffer = new byte[1024];
Array.Resize(ref buffer, buffer.Length + 10);
//добавим синхрокомбинацию
for (var i = 0; i < 10; i++) buffer[i + buffer.Length] = (byte)(i + 1);
var coder = HemmingCoderLib.Factory.CreateCoder(7, 4);
var mux = new HemmingCoderLib.Mux.MatrixInterleaver(3);
var scr = HemmingCoderLib.Factory.CreateScrambler(0);


//закодируем пустой буфер
buffer[0] = 5;
var coderBuffer = coder.Encode(buffer);

//выполним перемежение и скремблирование
var muxBuffer = mux.Mux(coderBuffer);
var scramblerBuffer = scr.Scramble(muxBuffer);
//внесем ошибки
var offset = 0;
for (int i = 0; i < scramblerBuffer.Length; i += 4)
{
    scramblerBuffer[i] ^= (byte)(1 << offset);
    offset = offset > 7 ? 0 : offset + 1;
}
//выполним дескремблирование
var descrambleBuffer = scr.DeScramble(scramblerBuffer);
//выполним деперемежение
var demuxBuffer = mux.DeMux(descrambleBuffer);
//удалим синхрокомбинацию
var position = AdditionalFunctions.FindCombination(demuxBuffer, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
Array.Resize(ref demuxBuffer, position);
//декодируем комбинцию

var decodeBuffer = coder.Decode(coderBuffer);
Console.ReadLine();
