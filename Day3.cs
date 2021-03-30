using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day3 : ISolver
    {
        public List<string> Entries { get; set; }
        public int MaxCol { get; set; }

        private int StartRow = 0;
        private int StartCol = 0;

        public Day3(string inputFile)
        {
            Entries = Reader.ReadStrings(inputFile);
            MaxCol = Entries.First().Length;
        }

        public void Part1()
        {
            var numTrees = NumberOfTrees(3, 1);

            Console.WriteLine(numTrees);
        }

        public void Part2()
        {
            var a = NumberOfTrees(1, 1);
            var b = NumberOfTrees(3, 1);
            var c = NumberOfTrees(5, 1);
            var d = NumberOfTrees(7, 1);
            var e = NumberOfTrees(1, 2);

            // NOTE not sure why this doesn't multiply properly
            Console.WriteLine(a * b * c * d * e);
        }

        private int NumberOfTrees(int moveRight, int moveDown)
        {
            var numTrees = 0;
            var col = StartCol;
            for (int row = StartRow; row < Entries.Count; row = row + moveDown)
            {
                // check tree
                if (Entries[row][col] == '#')
                {
                    numTrees++;
                }

                // move
                col = (col + moveRight) % MaxCol;
            }

            return numTrees;
        }
    }
}