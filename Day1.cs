// -----------------------------------------------------------------------------
// <copyright file="ICryptoService.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------

namespace andrewlee_adventofcode2022_csharp
{
    public class Day1 : Day
    {
        public override int Part1Solve()
        {
            string[] day1_inputs = LoadInputs("202101");
            int count_of_increases = 0;
            for (int i = 1; i < day1_inputs.Count(); i++)
            {
                if (Int32.Parse(day1_inputs[i]) > Int32.Parse(day1_inputs[i - 1]))
                {
                    count_of_increases++;
                }
                // Do something
            }
            return count_of_increases;

        }

        public override int Part2Solve()
        {
            string[] day1_inputs = this.LoadInputs("202101");
            int count_of_increases2 = 0;
            for (int i = 3; i < day1_inputs.Count(); i++)
            {
                if (Int32.Parse(day1_inputs[i]) > Int32.Parse(day1_inputs[i - 3]))
                {
                    count_of_increases2++;
                }
                // Do something
            }
            return count_of_increases2;

        }
    }
 }
