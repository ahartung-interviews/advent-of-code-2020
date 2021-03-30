using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day8 : ISolver
    {
        private List<Instruction> Instructions { get; set; }

        private HashSet<int> ExecutedInstructions { get; set; }

        private HashSet<int> SwappedInstructions { get; set; }

        private int Accumulator { get; set; }

        public Day8(string inputFile)
        {
            Instructions = Reader.ReadStrings(inputFile)
                .Select(line => new Instruction(line))
                .ToList();

            ExecutedInstructions = new HashSet<int>();

            SwappedInstructions = new HashSet<int>();

            Accumulator = 0;
        }

        public void Part1()
        {
            ExecuteInstructions(0, -1);
        }

        public void Part2()
        {
            var noOperationInstructions = Instructions.Where(i => i.Type == InstructionTypes.NoOperation);
            var jumpInstructions = Instructions.Where(i => i.Type == InstructionTypes.Jump);

            foreach (var noOpInstruction in noOperationInstructions)
            {
                var index = Instructions.IndexOf(noOpInstruction);
                ExecuteInstructions(0, index);
                ExecutedInstructions = new HashSet<int>();
                Accumulator = 0;
            }

            foreach (var jumpInstruction in jumpInstructions)
            {
                var index = Instructions.IndexOf(jumpInstruction);
                ExecuteInstructions(0, index);
                ExecutedInstructions = new HashSet<int>();
                Accumulator = 0;
            }
        }

        private void ExecuteInstructions(int instructionNumber, int swapNumber)
        {
            if (ExecutedInstructions.Contains(instructionNumber))
            {
                // Console.WriteLine($"Looped: {Accumulator}");
                return;
            }

            if (instructionNumber == Instructions.Count)
            {
                Console.WriteLine($"Success: {Accumulator}");
                return;
            }

            ExecutedInstructions.Add(instructionNumber);

            var instruction = Instructions.ElementAt(instructionNumber);

            if (instruction.Type == InstructionTypes.NoOperation)
            {
                if (instructionNumber == swapNumber)
                {
                    SwappedInstructions.Add(instructionNumber);
                    ExecuteInstructions(instructionNumber + instruction.Value, swapNumber);
                }
                else
                {
                    ExecuteInstructions(instructionNumber + 1, swapNumber);
                }
            }
            else if (instruction.Type == InstructionTypes.Jump)
            {
                if (instructionNumber == swapNumber)
                {
                    SwappedInstructions.Add(instructionNumber);
                    ExecuteInstructions(instructionNumber + 1, swapNumber);
                }
                else
                {
                    ExecuteInstructions(instructionNumber + instruction.Value, swapNumber);
                }
            }
            else // if (instruction.Type == InstructionTypes.Accumulate)
            {
                Accumulator += instruction.Value;
                ExecuteInstructions(instructionNumber + 1, swapNumber);
            }
        }
    }

    internal class Instruction
    {
        public string Type { get; set; }
        public int Value { get; set; }

        public Instruction() { }

        public Instruction(string instructionLine)
        {
            var instructionPieces = instructionLine.Split(" ");

            Type = instructionPieces[0];

            Value = Int32.Parse(instructionPieces[1].Substring(1));

            if (instructionPieces[1][0] == InstructionTypes.Negative)
            {
                Value *= -1;
            }
        }
    }

    internal static class InstructionTypes
    {
        public const string Accumulate = "acc";

        public const string Jump = "jmp";

        public const string NoOperation = "nop";

        public const char Negative = '-';
    }
}