using System;
using System.Collections.Generic;
using System.IO; 

namespace RLE
{
	class Program
    {
        static byte[] RLECompress(byte[] binary)
		{
            List<byte> compressed = new List<byte>();
            int counter = 1;
            for (int i = 0; i < binary.Length; i++) {
                while (i + 1 < binary.Length && binary[i] == binary[i + 1]) {
                    counter++;
                    if (counter == 256) {
                        compressed.Add(0);
                        counter = 1;
                    }
                    i++;
                }
                compressed.Add(Convert.ToByte(counter));
                compressed.Add(binary[i]);
                counter = 1;
            }
            Console.WriteLine("Compressed list length: " + compressed.Count);
            return compressed.ToArray();
		}

		static byte[] RLEDecompress(byte[] compressed){
            List<byte> decompressed = new List<byte>();
            byte tmp = 0;
            int count = 0;
            for (int i = 0; i < compressed.Length; i++) {
                tmp = compressed[i];
                if (tmp == 0)
                {
                    count += 255;
                    continue;
                }
                count += tmp;
                for(int j=0; j<count; j++)
                {
                    decompressed.Add(compressed[i + 1]);
                }
                i++;
                count = 0;
            }
            return decompressed.ToArray();
		}

        static void Main(string[] args)
		{
            byte[] source = File.ReadAllBytes(@"C:\Users\Nick\Downloads\04_The_Devil_In_I.wav");
            Console.WriteLine("Source length: " + source.Length);
            byte[] compressed = RLECompress(source);
            Console.WriteLine("Compressed length: " + compressed.Length);
            byte[] decompressed = RLEDecompress(compressed);
            Console.WriteLine("Decompressed length: " + decompressed.Length);
            Console.WriteLine("Herman pidor");
            Console.Read();
        }
	}
}
