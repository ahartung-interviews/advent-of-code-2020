using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day7 : ISolver
    {
        private IList<Bag> Bags { get; set; }
        public Day7(string inputFile)
        {
            Bags = Reader.ReadStrings(inputFile)
                .Select(line => new Bag(line))
                .ToList();
        }

        public void Part1()
        {
            var inputBag = "shinygold";

            var parentBags = FindParentBags(inputBag)
                .Select(bag => bag.Identifier)
                .Distinct();

            Console.WriteLine(string.Join(", ", parentBags));
            Console.WriteLine(parentBags.Count());
        }

        public void Part2()
        {
            var inputBag = "shinygold";

            var numChildBags = FindChildBagQuantities(inputBag);

            Console.WriteLine(numChildBags - 1);
            // childBags.ForEach(cb => Console.Write($"{cb.Quantity} {cb.Identifier}, "));
        }

        private IEnumerable<Bag> FindParentBags(string identifier)
        {
            var parentBags = Bags
                .Where(bag => bag.CanHold.Any(b => b.Identifier == identifier))
                .ToList();

            if (parentBags.Count == 0)
            {
                return Enumerable.Empty<Bag>();
            }

            return parentBags.Concat(parentBags.SelectMany(parentBag => FindParentBags(parentBag.Identifier)));
        }

        private int FindChildBagQuantities(string identifier)
        {
            var childBags = Bags
                .FirstOrDefault(b => b.Identifier == identifier)
                .CanHold;

            if (childBags.Count == 0)
            {
                Console.WriteLine($"{identifier} has no bags");
                return 1;
            }

            Console.WriteLine($"{identifier} has child bags:");
            childBags.ForEach(cb => Console.WriteLine($"    {cb.Quantity} {cb.Identifier}"));

            return 1 + childBags
                .Select(cb => cb.Quantity * FindChildBagQuantities(cb.Identifier))
                .Sum();
        }
    }

    internal class Bag
    {
        public string Identifier { get; set; }
        public List<ChildBagAndQuantity> CanHold { get; set; }

        public Bag() { }

        public Bag(string rule)
        {
            var splitRule = rule.Split("contain");

            var bagId = splitRule[0].TrimEnd().Split(" ").SkipLast(1);
            Identifier = string.Join("", bagId);

            CanHold = splitRule[1]
                .TrimStart()
                .Split(", ")
                .Select(content =>
                {
                    if (content == "no other bags.")
                    {
                        return null;
                    }

                    var splitContents = content.Split(" ").SkipLast(1);

                    var quantity = Int32.Parse(splitContents.First());
                    var identifier = string.Join("", splitContents.Skip(1));

                    return new ChildBagAndQuantity(identifier, quantity);
                })
                .Where(content => content != null)
                .ToList();
        }
    }

    internal class ChildBagAndQuantity
    {
        public string Identifier { get; set; }
        public int Quantity { get; set; }

        public ChildBagAndQuantity() { }

        public ChildBagAndQuantity(string identifier, int quantity)
        {
            Identifier = identifier;
            Quantity = quantity;
        }
    }
}