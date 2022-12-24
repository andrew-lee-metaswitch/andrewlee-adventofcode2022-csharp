using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using static andrewlee_adventofcode2022_csharp.Day5;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day5 : Day
    {

        ////[G]                 [D] [R]        
        ////[W]         [V]     [C] [T] [M]    
        ////[L]         [P] [Z] [Q] [F] [V]    
        ////[J]         [S] [D] [J] [M] [T] [V]
        ////[B]     [M] [H] [L] [Z] [J] [B] [S]
        ////[R] [C] [T] [C] [T] [R] [D] [R] [D]
        ////[T] [W] [Z] [T] [P] [B] [B] [H] [P]
        ////[D] [S] [R] [D] [G] [F] [S] [L] [Q]
        //// 1   2   3   4   5   6   7   8   9 

        ////move 1 from 3 to 5
        ////move 5 from 5 to 4
        ////move 6 from 7 to 3
        ////move 6 from 1 to 3 

        public class Column
        {
            public List<char> Crates;

            public Column()
            {
                Crates = new List<char> { };
            }

            public List<char> Remove(int n)
            {
                List<char> RemovedCrates = Crates.GetRange(0, n);
                Crates.RemoveRange(0, n);
                return RemovedCrates;
            }

            public void Add(List<char> newCrates)
            {
                Crates.Reverse();
                Crates.AddRange(newCrates);
                Crates.Reverse();
            }

            public void MultiCrateCraneAdd(List<char> newCrates)
            {
                newCrates.AddRange(Crates);
                Crates = newCrates;
            }

        }

        public class Instruction
        {
            public Instruction(string input)
            {
                string pattern = @"^move (\d+) from (\d+) to (\d+)$";
                var groups = Regex.Match(input, pattern).Groups.Values.ToList();
                AmountToMove = Int32.Parse(groups[1].Value);
                From = Int32.Parse(groups[2].Value);
                To = Int32.Parse(groups[3].Value);
            }

            public int AmountToMove;
            public int From;
            public int To;

            public List<Column> Part1Apply(List<Column> initialColumnState)
            {
                List<char> movedCrates = initialColumnState[From-1].Remove(AmountToMove);
                initialColumnState[To-1].Add(movedCrates);
                return initialColumnState;
            }

            public List<Column> Part2Apply(List<Column> initialColumnState)
            {
                List<char> movedCrates = initialColumnState[From - 1].Remove(AmountToMove);
                initialColumnState[To - 1].MultiCrateCraneAdd(movedCrates);
                return initialColumnState;
            }
        }


        public (List<Column>, List<Instruction>) ParseInputs()
        {
            string[] inputs = LoadInputs("202205");

            List<Instruction> instructions = new List<Instruction>();
            List<Column> columns = new List<Column>();

            // Parse columns
            for (int i = 0; i < 9; i++)
            {
                Column column = new Column();
                List<char> initialCrates = new List<char>();
                for (int j = 7; j >= 0; j--)
                {
                    if (inputs[j][(4 * i) + 1] != ' ')
                    {
                        initialCrates.Add(inputs[j][4 * i + 1]);
                    }
                }
                column.Add(initialCrates);
                columns.Add(column);
            }

            for (int i = 10; i < inputs.Count(); i++)
            {
                Console.WriteLine(inputs[i]);
                instructions.Add(new Instruction(inputs[i]));
            }

            return (columns, instructions);

        }

        public void LogState(List<Column> columns)
        {
            foreach (Column column in columns)
            {
                Console.WriteLine(string.Join("", column.Crates));
            }
            Console.WriteLine();
        }


        public override int Part1Solve()
        {
            (List<Column> columns, List<Instruction> instructions) = ParseInputs();

            LogState(columns);
            foreach (Instruction instruction in instructions)
            {
                columns = instruction.Part1Apply(columns);
                ////Console.WriteLine($"Instruction is Move {instruction.AmountToMove} from {instruction.From} to {instruction.To}");
                ////LogState(columns);
            }

            Console.WriteLine(string.Join("", columns.Select(c => c.Crates[0])));
            return 0;
        }

        public override int Part2Solve()
        {
            (List<Column> columns, List<Instruction> instructions) = ParseInputs();

            LogState(columns);
            foreach (Instruction instruction in instructions)
            {
                columns = instruction.Part2Apply(columns);
                Console.WriteLine($"Instruction is Move {instruction.AmountToMove} from {instruction.From} to {instruction.To}");
                LogState(columns);
            }

            Console.WriteLine(string.Join("", columns.Select(c => c.Crates[0])));
            return 0;
        }
    }
}
