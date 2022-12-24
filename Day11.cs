using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day11 : Day
    {
        public class Monkey
        {
            public int id;
            public List<Int64> worries;
            public Func<Int64, Int64> worryOperator;
            public int divisiblityTestInt;
            public int testTrueMonkey;
            public int testFalseMonkey;
            public int inspectionsCarriedOut;

            public Monkey(int id, List<Int64> worries, Func<Int64, Int64> worryOperator, int divisiblityTestInt, int testTrueMonkey, int testFalseMonkey) {
                this.id = id;
                this.worries = worries;
                this.worryOperator = worryOperator;
                this.divisiblityTestInt = divisiblityTestInt;
                this.testTrueMonkey = testTrueMonkey;
                this.testFalseMonkey = testFalseMonkey;
                this.inspectionsCarriedOut = 0;
            }

            public override string ToString()
            {
                return $"Monkey: {id}, Worries {string.Join(", ", worries)}, div {divisiblityTestInt}, testT {testTrueMonkey}, testF {testFalseMonkey}";
            }
        }

        private List<Monkey> ParseArgs()
        {
            string divisiblityTestPattern = @"^  Test: divisible by (\d+)$";
            string testTrueMonkeyPattern = @"^    If true: throw to monkey (\d+)$";
            string testFalseMonkeyPattern = @"^    If false: throw to monkey (\d+)$";
            string startingItemsPattern = @"^  Starting items: [\d ,]+$";
            string operatorPattern = @"^  Operation: new = (.*)$";

            List<Monkey> monkeys = new List<Monkey> { };
            string[] inputs = LoadInputs("202211");
            int id = 0;
            List<Int64> worries = new List<Int64> { };
            Func<Int64, Int64> worryOperator = (x) => x;
            int divisiblityTestInt = 1;
            int testTrueMonkey = 0;
            int testFalseMonkey = 0;
            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    monkeys.Add(new Monkey(id, worries, worryOperator, divisiblityTestInt, testTrueMonkey, testFalseMonkey));
                    id++;
                } else if (Regex.IsMatch(input, startingItemsPattern))
                {
                    string items = input.Replace("Starting items: ", "");
                    worries = items.Split(", ").Select(w => Int64.Parse(w)).ToList();
                } else if (Regex.IsMatch(input, operatorPattern))
                {
                    string operatorString = Regex.Match(input, operatorPattern).Groups[1].Value;
                    worryOperator = operatorString switch
                    {
                        "old + 1" => (x) => x + 1,
                        "old + 6" => (x) => x + 6,
                        "old + 7" => (x) => x + 7,
                        "old + 3" => (x) => x + 3,
                        "old + 4" => (x) => x + 4,
                        "old + 8" => (x) => x + 8,
                        "old * 5" => (x) => x * 5,
                        "old * 11" => (x) => x * 11,
                        "old * 19" => (x) => x * 19,
                        "old * old" => (x) => x * x,
                        _ => throw new Exception("Unhandled operator")
                    };
                } else if (Regex.IsMatch(input, divisiblityTestPattern))
                {
                    divisiblityTestInt = int.Parse(Regex.Match(input, divisiblityTestPattern).Groups[1].Value);
                } else if (Regex.IsMatch(input, testTrueMonkeyPattern))
                {
                    testTrueMonkey = int.Parse(Regex.Match(input, testTrueMonkeyPattern).Groups[1].Value);
                } else if (Regex.IsMatch(input, testFalseMonkeyPattern))
                {
                    testFalseMonkey = int.Parse(Regex.Match(input, testFalseMonkeyPattern).Groups[1].Value);
                }
            }
            monkeys.Add(new Monkey(id, worries, worryOperator, divisiblityTestInt, testTrueMonkey, testFalseMonkey));
            return monkeys;
        }

        public override int Part1Solve()
        {
            List<Monkey> monkeys = ParseArgs();
            for (int i = 0; i < 20; i++) {
                foreach (Monkey m in monkeys)
                {
                    m.inspectionsCarriedOut += m.worries.Count();
                    foreach (Int64 worry in m.worries)
                    {
                        long newWorry = m.worryOperator(worry)/3;
                        if (newWorry % m.divisiblityTestInt == 0)
                        {
                            monkeys[m.testTrueMonkey].worries.Add(newWorry);
                        }
                        else
                        {
                            monkeys[m.testFalseMonkey].worries.Add(newWorry);
                        }
                    }
                    m.worries = new List<Int64> { };
                }
            }

                List<int> inspectedList = monkeys.Select(m => m.inspectionsCarriedOut).ToList();
            inspectedList.Sort();
            return inspectedList[inspectedList.Count - 1] * inspectedList[inspectedList.Count - 2];
        }

        public override int Part2Solve()
        {
            List<Monkey> monkeys = ParseArgs();

            // Do everything mod this number to stop us dealing with huge numbers.
            int modulo = monkeys.Select(m => m.divisiblityTestInt).Aggregate((a, b) => a * b);

            for (int i = 0; i < 10000; i++)
            {
                foreach (Monkey m in monkeys)
                {
                    m.inspectionsCarriedOut += m.worries.Count();
                    foreach (Int64 worry in m.worries)
                    {
                        Int64 newWorry = m.worryOperator(worry);
                        newWorry = newWorry % modulo;
                        if (newWorry % m.divisiblityTestInt == 0)
                        {
                            monkeys[m.testTrueMonkey].worries.Add(newWorry);
                        }
                        else
                        {
                            monkeys[m.testFalseMonkey].worries.Add(newWorry);
                        }
                    }
                    m.worries = new List<Int64> { };
                }
            }

            List<int> inspectedList = monkeys.Select(m => m.inspectionsCarriedOut).ToList();
            inspectedList.Sort();
            Console.WriteLine(((Int64)inspectedList[inspectedList.Count - 1]) * ((Int64)inspectedList[inspectedList.Count - 2]));
            return 0;
        }
    }
}
