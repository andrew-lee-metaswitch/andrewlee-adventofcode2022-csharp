using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using static andrewlee_adventofcode2022_csharp.Day5;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day8 : Day
    {

        private bool IsVisible(Dictionary<string, List<int>> treeLines)
        {
            return treeLines.Values.Where(tl => VisibleFromOutside(tl)).Any();
        }

        private int ScenicScore(Dictionary<string, List<int>> treeLines)
        {
            return treeLines.Values.Select(tl => ScenicScoreInDirection(tl)).Aggregate(1, (a, b) => a * b);
        }

        private int ScenicScoreInDirection(List<int> trees)
        {
            // This wil be a list of integers [5, 6, 7, 2, 1], say,
            // where the FIRST integer represents the tree under test

            // Its secnic score is thr number of trees to the right of it, that it can see
            int mainTreeHeight = trees[0];
            trees.RemoveAt(0);

            int scenicScore = 0;
            foreach (var tree in trees)
            {
                if (tree < mainTreeHeight)
                {
                    scenicScore++;
                } else {
                    scenicScore++;
                    return scenicScore;
                }
            }
            return scenicScore;
        }

        private bool VisibleFromOutside(List<int> trees)
        {
            // This wil be a list of integers [5, 6, 7, 2, 1], say,
            // where the FIRST integer represents the tree under test

            // It is visible from the outside if this first number is greater than all other integers in the list
            int mainTreeHeight = trees[0];
            trees.RemoveAt(0);
            return trees.All(h => h < mainTreeHeight);
        }

        private List<int> NorthTrees(List<List<int>> grid, int x_coord, int y_coord)
        {
            List<int> northTrees = grid.Where((row, index) => index <= y_coord).Select(row => row[x_coord]).ToList();
            northTrees.Reverse();
            return northTrees;
        }

        private List<int> SouthTrees(List<List<int>> grid, int x_coord, int y_coord)
        {
            return grid.Where((row, index) => index >= y_coord).Select(row => row[x_coord]).ToList();
        }

        private List<int> WestTrees(List<List<int>> grid, int x_coord, int y_coord)
        {
            List<int> westTrees = grid[y_coord].GetRange(0, x_coord + 1);
            westTrees.Reverse();
            return westTrees;
        }

        private List<int> EastTrees(List<List<int>> grid, int x_coord, int y_coord)
        {
            return grid[y_coord].GetRange(x_coord, grid[y_coord].Count() - x_coord);
        }

        private Dictionary<(int, int), Dictionary<string, List<int>>> ParseArgs()
        {
            string[] inputs = LoadInputs("202208");
            List<List<int>> grid = new List<List<int>> { };
            foreach (string input in inputs)
            {
                grid.Add(input.ToCharArray().Select(c => int.Parse(c.ToString())).ToList());
            }
            Dictionary<(int, int), Dictionary<string, List<int>>> gridDict = new Dictionary<(int, int), Dictionary<string, List<int>>> { };

            for (int i = 0; i < grid.Count(); i++)
            {
                for (int j = 0; j < grid[0].Count(); j++)
                {
                    Dictionary<string, List<int>> ijdict = new Dictionary<string, List<int>> { };
                    ijdict.Add("north", NorthTrees(grid, j, i));
                    ijdict.Add("south", SouthTrees(grid, j, i));
                    ijdict.Add("west", WestTrees(grid, j, i));
                    ijdict.Add("east", EastTrees(grid, j, i));
                    gridDict.Add((j, i), ijdict);
                }
            }
            return gridDict;
        }

        public override int Part1Solve()
        {
            Dictionary<(int, int), Dictionary<string, List<int>>> gridDict = ParseArgs();
            return gridDict.Values.Where(treeLines => IsVisible(treeLines)).Count();
        }

        public override int Part2Solve()
        {
            Dictionary<(int, int), Dictionary<string, List<int>>> gridDict = ParseArgs();
            return gridDict.Values.Select(treeLines => ScenicScore(treeLines)).Max();
        }
     }
}
