using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Numerics;
using System.Text.RegularExpressions;
using static andrewlee_adventofcode2022_csharp.Day5;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day10 : Day
    {
        private List<int> ParseArgs()
        {
            string[] inputs = LoadInputs("202210");

            // Start of Day 1. 
            List<int> xRegister = new List<int> { 1 };

            // idx N corresponds to Day N+1.
            // Hence idx + 1 means Day idx
            // xRegister[i] corresponds to value during day i+1
            // xRegister.Count()
            for (int idx = 0; idx < inputs.Count(); idx++)
            {
                xRegister.Add(xRegister[xRegister.Count() - 1]);

                if (inputs[idx] != "noop")
                {
                    int addValue = int.Parse(inputs[idx].Split(' ')[1]);
                    xRegister.Add(xRegister[xRegister.Count() - 1] + addValue);
                }
            }

            for (int i = 0; i < xRegister.Count(); i++)
            {
                Console.WriteLine($"During Day {i + 1}, value is {xRegister[i]}");
            }
            return xRegister;
        }
        public override int Part1Solve()
        {
            List<int> xRegister = ParseArgs();

            return (xRegister[19] * 20)
                + (xRegister[59] * 60)
                + (xRegister[99] * 100)
                + (xRegister[139] * 140)
                + (xRegister[179] * 180)
                + (xRegister[219] * 220);
        }

        public override int Part2Solve()
        {
            List<int> xRegister = ParseArgs();

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 40; x++)
                {
                    if (Math.Abs(xRegister[40*y + x] - x) <= 1)
                    {
                        Console.Write('#');
                    } else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            return 0;
        }
    }
}
