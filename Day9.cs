using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using System.Xml;
using static andrewlee_adventofcode2022_csharp.Day5;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day9 : Day
    {
        // The head(H) and tail(T) must always be touching(diagonally adjacent and even overlapping both count as touching)
        // If the head is ever two steps directly up, down, left, or right from the tail, the tail must also move one step in that direction so it remains close enough:
        // Otherwise, if the head and tail aren't touching and aren't in the same row or column, the tail always moves one step diagonally to keep up:
        // Assume the head and the tail both start at the same position, overlapping.

        public enum Direction
        {
            Left, Right, Up, Down
        }
        
        public class Instruction
        {
            public Instruction(string input)
            {
                this.Direction = input.Split(' ')[0] switch
                {
                    "R" => Direction.Right,
                    "L" => Direction.Left,
                    "U" => Direction.Up,
                    "D" => Direction.Down,
                    _ => throw new NotImplementedException()
                };
                this.Amount = int.Parse(input.Split(' ')[1]);
            }

            public Direction Direction;
            public int Amount;
 
            public (int, int) Process((int, int) hLocation)
            {
                if (Direction == Direction.Up)
                {
                    hLocation.Item2 += Amount;
                } else if (Direction == Direction.Down)
                {
                    hLocation.Item2 -= Amount;
                }
                else if (Direction == Direction.Left)
                {
                    hLocation.Item1 -= Amount;
                }
                else if (Direction == Direction.Right)
                {
                    hLocation.Item1 += Amount;
                }
                return hLocation;
            }
        }

        // The head(H) and tail(T) must always be touching(diagonally adjacent and even overlapping both count as touching)
        // If the head is ever two steps directly up, down, left, or right from the tail,
        // the tail must also move one step in that direction so it remains close enough:
        // Otherwise, if the head and tail aren't touching and aren't in the same row or column,
        // the tail always moves one step diagonally to keep up:
        public (int, int) MoveTailTowardsHead((int, int) hLocation, (int, int) tLocation)
        {
            if (hLocation.Item1 == tLocation.Item1 && hLocation.Item2 == tLocation.Item2)
            {
                return tLocation;
            } else if (hLocation.Item1 == tLocation.Item1 && hLocation.Item2 != tLocation.Item2)
            {
                if (hLocation.Item2 - tLocation.Item2 > 1) {
                    return (tLocation.Item1, tLocation.Item2 + 1);
                } else if (hLocation.Item2 - tLocation.Item2 < - 1) {
                    return (tLocation.Item1, tLocation.Item2 - 1);

                } else
                {
                    return tLocation;
                }
            }
            else if (hLocation.Item1 != tLocation.Item1 && hLocation.Item2 == tLocation.Item2)
            {
                if (hLocation.Item1 - tLocation.Item1 > 1)
                {
                    return (tLocation.Item1 + 1, tLocation.Item2);
                }
                else if (hLocation.Item1 - tLocation.Item1 < -1)
                {
                    return (tLocation.Item1 - 1, tLocation.Item2);

                }
                else
                {
                    return tLocation;
                }
            } else
            {
                int xdelta = (hLocation.Item1 - tLocation.Item1) / Math.Abs(hLocation.Item1 - tLocation.Item1);
                int ydelta = (hLocation.Item2 - tLocation.Item2) / Math.Abs(hLocation.Item2 - tLocation.Item2);

                if (Math.Abs((hLocation.Item1 - tLocation.Item1)) == 1 && Math.Abs((hLocation.Item2 - tLocation.Item2)) == 1) {
                    return tLocation;
                }

                return (tLocation.Item1 + xdelta, tLocation.Item2 + ydelta);
            }
        }

        public override int Part1Solve()
        {
            string[] inputs = LoadInputs("202209");
            List<Instruction> instructions = inputs.Select(i => new Instruction(i)).ToList();
            Dictionary<(int, int), int> gridDict = new Dictionary<(int, int), int> { };
            gridDict.Add((0, 0), 1);
            (int, int) hLocation = (0, 0);
            (int, int) tLocation = (0, 0);

            foreach (Instruction instruction in instructions) {
                hLocation = instruction.Process(hLocation);
                while (tLocation != MoveTailTowardsHead(hLocation, tLocation)) {
                    tLocation = MoveTailTowardsHead(hLocation, tLocation);
                    if (!gridDict.ContainsKey(tLocation))
                    {
                        gridDict.Add(tLocation, 1);
                    }
                    else
                    {
                        gridDict[tLocation]++;
                    }
                }
            }
            return gridDict.Count();
        }

        public override int Part2Solve()
        {
            string[] inputs = LoadInputs("202209");
            List<Instruction> instructions = inputs.Select(i => new Instruction(i)).ToList();
            Dictionary<(int, int), int> gridDict = new Dictionary<(int, int), int> { };
            gridDict.Add((0, 0), 1);
            List<(int, int)> ropeLocation = new List<(int, int)> { };
            for (int i=0; i < 10; i++)
            {
                ropeLocation.Add((0, 0));
            }
            foreach (Instruction instruction in instructions)
            {
                ropeLocation[0] = instruction.Process(ropeLocation[0]);

                int j = 1;
                
                while (j < 50)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if (ropeLocation[i - 1] != MoveTailTowardsHead(ropeLocation[i-1], ropeLocation[i])) {
                            ropeLocation[i] = MoveTailTowardsHead(ropeLocation[i-1], ropeLocation[i]);
                        }

                        if (i == 9)
                        {
                            if (!gridDict.ContainsKey(ropeLocation[i]))
                            {
                                gridDict.Add(ropeLocation[i], 1);
                            }
                            else
                            {
                                gridDict[ropeLocation[i]]++;
                            }
                        }
                    }
                    j++;
                }
            }
            return gridDict.Count();
        }
    }
}
