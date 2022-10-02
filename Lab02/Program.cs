// See https://aka.ms/new-console-template for more information
using HemmingCoderLib;

Console.WriteLine("Hello, World!");
var random = new Random();
var coders = new int[] { 6, 7, 9, 11 };
var interleavers = new int[] { 3, 4, 5, 6, 7, 8 };
var scramblers = new int[] { 0, 1};

var dirFlags = new DirectoryInfo("Flags");
var files = dirFlags.GetFiles();
var number = 1;
foreach (var file in files)
{
    ProcessFile(file, coders[random.Next(0, 4)], interleavers[random.Next(0, 6)], scramblers[random.Next(0, 2)], number);
    number++;
}    


Console.ReadLine();







byte[] ProcessFile(FileInfo file, int n, int m, int s, int number)
{    
    
    var coder = HemmingCoderLib.Factory.CreateCoder(n);
    var mux = new HemmingCoderLib.Mux.MatrixInterleaver(m);
    var scr = HemmingCoderLib.Factory.CreateScrambler(s);

    var buffer = File.ReadAllBytes(file.FullName);
    Array.Resize(ref buffer, buffer.Length + 10);
    //добавим синхрокомбинацию
    for (var i = 0; i < 10; i++) buffer[i + buffer.Length - 10] = (byte)(i + 1);
    //закодируем пустой буфер    
    var coderBuffer = coder.Encode(buffer);

    //выполним перемежение и скремблирование
    var muxBuffer = mux.Mux(coderBuffer);
    var scramblerBuffer = scr.Scramble(muxBuffer);
    //внесем ошибки
    var errors = new byte[] { 1, 4, 8, 16, 32, 64, 128 };
    for (int i = 0; i < scramblerBuffer.Length; i += 20)
    {
        scramblerBuffer[i] ^= errors[random.Next(0, errors.Length)];
    }
    File.WriteAllBytes($@"{Path.GetDirectoryName(file.FullName)}\{Path.GetFileNameWithoutExtension(file.Name)}.dat", scramblerBuffer);
    File.WriteAllText($@"{Path.GetDirectoryName(file.FullName)}\{Path.GetFileNameWithoutExtension(file.Name)}.txt", $"номер: {number} файл: {file.Name} n: {n} m: {m} s: {s}");
    
    //выполним дескремблирование
    var descrambleBuffer = scr.DeScramble(scramblerBuffer);
    //выполним деперемежение
    var demuxBuffer = mux.DeMux(descrambleBuffer);
    //декодируем комбинцию
    var decodeBuffer = coder.Decode(demuxBuffer);
    //удалим синхрокомбинацию
    var position = AdditionalFunctions.FindCombination(decodeBuffer, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
    Array.Resize(ref decodeBuffer, position);
    Array.Resize(ref buffer, buffer.Length - 10);
    var check = AdditionalFunctions.FindCombination(decodeBuffer, buffer);
    if (check != 0)
    {
        var q = 0;
    }
    return decodeBuffer;
}


