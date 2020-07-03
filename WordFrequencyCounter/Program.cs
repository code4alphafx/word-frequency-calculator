using System;
using System.IO;
using WordFrequencyCounter.Extensions;

namespace WordFrequencyCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter path to the text file to process: ");
            var path = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("Error: Invalid file or incorrect path, file must be *.txt");
                var x = Console.ReadKey();
                if (x != null)
                {
                    Environment.Exit(1);
                }
            }

            using (var sr = File.OpenText(path))
            {
                var kvPair = sr.GetMostFrequentUsedWords(5);
                foreach (var kv in kvPair)
                {
                    Console.WriteLine("Word: {0}, Occurence: {1}", kv.Key, kv.Value);
                }
            }

            Console.ReadKey();
        }

       }
}
