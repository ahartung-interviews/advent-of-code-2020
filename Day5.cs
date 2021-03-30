using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day5 : ISolver
    {
        public List<BoardingPass> BoardingPasses { get; set; }

        public Day5(string inputFile)
        {
            BoardingPasses = Reader.ReadStrings(inputFile)
                .Select(line => new BoardingPass(line))
                .ToList();
        }

        public void Part1()
        {
            var maxSeatId = BoardingPasses.Max(bp => bp.SeatId);

            Console.WriteLine(maxSeatId);
        }

        public void Part2()
        {
            var boardingPassesHash = BoardingPasses.ToDictionary(bp => bp.SeatId);

            var maxSeatId = BoardingPasses.Max(bp => bp.SeatId);

            for (int i = 0; i < maxSeatId; i++)
            {
                if (!boardingPassesHash.TryGetValue(i, out _)
                    && boardingPassesHash.TryGetValue(i + 1, out _)
                    && boardingPassesHash.TryGetValue(i - 1, out _))
                {
                    Console.WriteLine(i);
                }
            }
        }
    }

    public class BoardingPass
    {
        private string _binaryRow { get; set; }
        private string _binarySeatInRow { get; set; }
        public int Row { get; set; }
        public int SeatInRow { get; set; }
        public int SeatId
        {
            get => Row * 8 + SeatInRow;
        }

        public BoardingPass() { }

        public BoardingPass(string binaryId)
        {
            _binaryRow = binaryId.Substring(0, 7);
            _binarySeatInRow = binaryId.Substring(7, 3);

            var binaryRowNormalized = StandardizeBinaryString(_binaryRow, 'F', 'B');
            var binarySeatInRowNormalized = StandardizeBinaryString(_binarySeatInRow, 'L', 'R');

            Row = BinaryLocate(0, 128, binaryRowNormalized);
            SeatInRow = BinaryLocate(0, 8, binarySeatInRowNormalized);

            // Console.WriteLine($"Row = {Row}, SeatInRow = {SeatInRow}, SeatId = {SeatId}");
        }

        private List<int> StandardizeBinaryString(string binaryString, char zeroVal, char oneVal)
        {
            return binaryString
                .Replace(zeroVal, '0')
                .Replace(oneVal, '1')
                .ToList()
                .Select(r => Int32.Parse(r.ToString()))
                .ToList();
        }

        private int BinaryLocate(int lowerInclusive, int upperExclusive, IEnumerable<int> locator)
        {
            if (lowerInclusive + 1 == upperExclusive)
            {
                return lowerInclusive;
            }

            var middle = (lowerInclusive + upperExclusive) / 2;
            var instruction = locator.First();

            if (instruction == 0)
            {
                return BinaryLocate(lowerInclusive, middle, locator.Skip(1));
            }
            else if (instruction == 1)
            {
                return BinaryLocate(middle, upperExclusive, locator.Skip(1));
            }
            else return -1;
        }
    }
}