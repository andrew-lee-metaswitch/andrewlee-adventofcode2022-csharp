using System.Text.RegularExpressions;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day4 : Day
    {
        public override int Part1Solve()
        {
            string[] inputs = LoadInputs("202204");
            string pattern = @"^(\d+)-(\d+),(\d+)-(\d+)$";

            int countOfOverlaps = 0;
            foreach (string input in inputs)
            {
                var groups = Regex.Match(input, pattern).Groups.Values.ToList();
                if (((Int32.Parse(groups[1].Value) <= Int32.Parse(groups[3].Value)) && (Int32.Parse(groups[2].Value) >= Int32.Parse(groups[4].Value))) ||
                    ((Int32.Parse(groups[3].Value) <= Int32.Parse(groups[1].Value)) && (Int32.Parse(groups[4].Value) >= Int32.Parse(groups[2].Value))))
                {
                    countOfOverlaps++;
                }
            }
            return countOfOverlaps;
        }

        public override int Part2Solve()
        {
            string[] inputs = LoadInputs("202204");
            string pattern = @"^(\d+)-(\d+),(\d+)-(\d+)$";

            int countOfAnyOverlaps = 0;
            foreach (string input in inputs)
            {
                var groups = Regex.Match(input, pattern).Groups.Values.ToList();
                if (((Int32.Parse(groups[1].Value) <= Int32.Parse(groups[3].Value)) && (Int32.Parse(groups[2].Value) >= Int32.Parse(groups[3].Value))) ||
                    ((Int32.Parse(groups[3].Value) <= Int32.Parse(groups[1].Value)) && (Int32.Parse(groups[4].Value) >= Int32.Parse(groups[1].Value))))
                {
                    countOfAnyOverlaps++;
                }
            }
            return countOfAnyOverlaps;
        }
    }
}
