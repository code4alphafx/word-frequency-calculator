using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFrequencyCounter.Extensions
{
    public static class ExtensionMethods
    {
        public static Dictionary<string, int> GetMostFrequentUsedWords(this StreamReader sr, int topNWords = 10) 
        {
            if (topNWords < 1)
                throw new InvalidOperationException("Incorrect value for parameter topNWords, value must be greater than 0");

            Regex rx = new Regex(@"[A-Za-z]+",
              RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            var dictionary = new Dictionary<string, int>();

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().ToLower();
                var matches = rx.Matches(line);
                foreach (Match match in matches)
                {
                    if (!dictionary.ContainsKey(match.Groups["0"].Value))
                    {
                        dictionary.Add(match.Groups["0"].Value, 1);
                    }
                    else
                    {
                        dictionary[match.Groups["0"].Value] += 1;
                    }
                }
            }

            return dictionary.OrderByDescending(x => x.Value).Take(topNWords)
                    .ToDictionary(pair => pair.Key, pair => pair.Value); ;
        }
    }
}
