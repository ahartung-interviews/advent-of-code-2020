using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public static class Reader
    {
        public static List<string> ReadStrings(string inputFile)
        {
            var input = new List<string>();

            using (var sr = new StreamReader(inputFile))
            {
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    input.Add(line);
                }
            }

            return input;
        }
    }
}