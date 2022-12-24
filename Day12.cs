using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Diagnostics.SymbolStore;
using System.Text.RegularExpressions;
using static andrewlee_adventofcode2022_csharp.Day5;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day12 : Day
    {
        // Current position(S)
        // Location that should get the best signal(E).

        public class Coord
        {
            public int x_coord;
            public int y_coord;
            public int z_coord;
            public bool visited;
            public int minStepsTo;
            public bool isStart;
            public bool isEnd;

            public Coord(int x_coord, int y_coord, int z_coord, bool isStart, bool isEnd) { 
                this.x_coord = x_coord;
                this.y_coord = y_coord;
                this.z_coord = z_coord;
                this.isStart = isStart;
                this.isEnd= isEnd;
                this.visited = false;
                if (isStart)
                {
                    this.minStepsTo = 0;
                    
                } else
                {
                    this.minStepsTo = 1000000;
                }
            }

            public List<(int, int)> AvailbleHopsUp(Dictionary<(int, int), Coord>  Grid)
            {
                List<(int, int)> returnHops = new List<(int, int)> ();

                // NORTH
                if (Grid.ContainsKey((x_coord, y_coord + 1)) && Grid[(x_coord, y_coord+1)].z_coord <= this.z_coord + 1) {
                    returnHops.Add((x_coord, y_coord + 1));
                }

                // SOUTH
                if (Grid.ContainsKey((x_coord, y_coord - 1)) && Grid[(x_coord, y_coord - 1)].z_coord <= this.z_coord + 1)
                {
                    returnHops.Add((x_coord, y_coord - 1));
                }

                // WEST
                if (Grid.ContainsKey((x_coord - 1, y_coord)) && Grid[(x_coord - 1, y_coord)].z_coord <= this.z_coord + 1)
                {
                    returnHops.Add((x_coord - 1, y_coord));
                }

                // EAST
                if (Grid.ContainsKey((x_coord + 1, y_coord)) && Grid[(x_coord + 1, y_coord)].z_coord <= this.z_coord + 1)
                {
                    returnHops.Add((x_coord + 1 , y_coord));
                }
                return returnHops;
            }

            public List<(int, int)> AvailbleHopsDown(Dictionary<(int, int), Coord> Grid)
            {
                List<(int, int)> returnHops = new List<(int, int)>();

                // NORTH
                if (Grid.ContainsKey((x_coord, y_coord + 1)) && Grid[(x_coord, y_coord + 1)].z_coord >= this.z_coord - 1)
                {
                    returnHops.Add((x_coord, y_coord + 1));
                }

                // SOUTH
                if (Grid.ContainsKey((x_coord, y_coord - 1)) && Grid[(x_coord, y_coord - 1)].z_coord >= this.z_coord - 1)
                {
                    returnHops.Add((x_coord, y_coord - 1));
                }

                // WEST
                if (Grid.ContainsKey((x_coord - 1, y_coord)) && Grid[(x_coord - 1, y_coord)].z_coord >= this.z_coord - 1)
                {
                    returnHops.Add((x_coord - 1, y_coord));
                }

                // EAST
                if (Grid.ContainsKey((x_coord + 1, y_coord)) && Grid[(x_coord + 1, y_coord)].z_coord >= this.z_coord - 1)
                {
                    returnHops.Add((x_coord + 1, y_coord));
                }
                return returnHops;
            }

        }

        public (Dictionary<(int, int), Coord> , (int, int), (int, int)) ParseArgs()
        {
            string[] inputs = LoadInputs("202212");
            Dictionary<(int, int), Coord> grid = new Dictionary<(int, int), Coord> { };
            (int, int) endCoord = (0, 0);
            (int, int) startingCoord = (0, 0);
            for (int y_coord = 0; y_coord < inputs.Length; y_coord++)
            {
                for (int x_coord = 0; x_coord < inputs[0].Length; x_coord++)
                {
                    char z_coord_char = inputs[y_coord][x_coord];
                    int z_coord = (int)z_coord_char - (int)'a';

                    bool IsStartCoord = z_coord_char == 'S';
                    if (IsStartCoord)
                    {
                        z_coord = 0;
                        startingCoord = (x_coord, y_coord);
                    }
                    bool IsEndCoord = z_coord_char == 'E';
                    if (IsEndCoord)
                    {
                        z_coord = 25;
                        endCoord = (x_coord, y_coord);
                    }
                    grid.Add((x_coord, y_coord), new Coord(x_coord, y_coord, z_coord, IsStartCoord, IsEndCoord));
                }
            }
            return (grid, startingCoord, endCoord);
        }



        public override int Part1Solve()
        {
            (Dictionary<(int, int), Coord> grid, _, (int, int) endCoord) = ParseArgs();

            while (!grid[endCoord].visited)
            {
                // All places we have got to by some method by haven't 'visited'
                List<Coord> unvisitedNodes = grid.Values.Where(c => c.minStepsTo != 1000000 && !c.visited).ToList();
                int highestheightVisited = unvisitedNodes.Select(c => c.z_coord).Max();
                // Find all the highest points we have visited
                
                foreach (Coord c in unvisitedNodes.Where(c => c.z_coord == highestheightVisited))
                {
                    Console.WriteLine($"Visiting coord ({c.x_coord}, {c.y_coord}) at height {c.z_coord}, it took us {c.minStepsTo} steps to get here");
                    foreach ((int x, int y) in c.AvailbleHopsUp(grid))
                    {
                        Console.WriteLine($" ({c.x_coord}, {c.y_coord})'s available hops are {string.Join(", ", c.AvailbleHopsUp(grid))}");
                        Coord hop = grid[(x, y)];
                        if (hop.minStepsTo == 1000000)
                        {
                            hop.minStepsTo = c.minStepsTo + 1;
                        }
                        else
                        {
                            hop.minStepsTo = Math.Max(hop.minStepsTo, c.minStepsTo + 1);
                        }
                    }
                    c.visited = true;
                }
            }

            return grid[endCoord].minStepsTo;
        }

        public override int Part2Solve()
        {
            (Dictionary<(int, int), Coord> grid, (int, int) startCoord, (int, int) endCoord) = ParseArgs();

            grid[startCoord].minStepsTo = 1000000;
            grid[endCoord].minStepsTo = 0;

            while (!grid.Values.Any(c => c.z_coord == 0 && c.visited))
                {
                    // All places we have got to by some method by haven't 'visited'
                    List<Coord> unvisitedNodes = grid.Values.Where(c => c.minStepsTo != 1000000 && !c.visited).ToList();
                    int highestheightVisited = unvisitedNodes.Select(c => c.z_coord).Min();
                    // Find all the highest points we have visited

                    foreach (Coord c in unvisitedNodes.Where(c => c.z_coord == highestheightVisited))
                    {
                        foreach ((int x, int y) in c.AvailbleHopsDown(grid))
                        {
                            Console.WriteLine($" ({c.x_coord}, {c.y_coord})'s available hops are {string.Join(", ", c.AvailbleHopsDown(grid))}");
                            Coord hop = grid[(x, y)];
                            if (hop.minStepsTo == 1000000)
                            {
                                hop.minStepsTo = c.minStepsTo + 1;
                            }
                            else
                            {
                                hop.minStepsTo = Math.Max(hop.minStepsTo, c.minStepsTo + 1);
                            }
                        }
                        c.visited = true;
                    }
                }
            return grid.Values.Where(c => c.z_coord == 0 && c.minStepsTo != 100000).Select(c => c.minStepsTo).Min();
        }
    }
}
