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
            string[] inputs = LoadInputs("202201");
            int caloriesCarriedByBiggestElf = 0;
            int caloriesCarriedByElf = 0;
            for (int i = 1; i < inputs.Count(); i++)
            {
                if (String.IsNullOrWhiteSpace(inputs[i]))
                {
                    caloriesCarriedByBiggestElf = Math.Max(caloriesCarriedByBiggestElf, caloriesCarriedByElf);
                    caloriesCarriedByElf = 0;
                }
                else
                {
                    caloriesCarriedByElf += Int32.Parse(inputs[i]);
                }
            }
            return caloriesCarriedByBiggestElf;

        }

        public override int Part2Solve()
        {
            string[] inputs = this.LoadInputs("202201");
            int[] caloriesCarriedByBiggestThreeElves = new int[] { 0, 0, 0 };
            int caloriesCarriedByElf = 0;
            for (int i = 1; i < inputs.Count(); i++)
            {
                if (String.IsNullOrWhiteSpace(inputs[i]))
                {
                    if (caloriesCarriedByElf >= caloriesCarriedByBiggestThreeElves[2])
                    {
                        caloriesCarriedByBiggestThreeElves = new int[] {
                            caloriesCarriedByBiggestThreeElves[1],
                            caloriesCarriedByBiggestThreeElves[2],
                            caloriesCarriedByElf
                            };

                    }
                    else if (caloriesCarriedByElf >= caloriesCarriedByBiggestThreeElves[1])
                    {
                        caloriesCarriedByBiggestThreeElves = new int[] {
                            caloriesCarriedByBiggestThreeElves[1],
                            caloriesCarriedByElf,
                            caloriesCarriedByBiggestThreeElves[2]
                                };
                    }
                    else if (caloriesCarriedByElf >= caloriesCarriedByBiggestThreeElves[0])
                    {
                        caloriesCarriedByBiggestThreeElves[0] = caloriesCarriedByElf;

                    }
                    caloriesCarriedByElf = 0;
                }
                else
                {
                    caloriesCarriedByElf += Int32.Parse(inputs[i]);
                }
            }
            return caloriesCarriedByBiggestThreeElves.Sum();
        }
    }
}
