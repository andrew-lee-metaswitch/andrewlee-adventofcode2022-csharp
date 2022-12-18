using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using static andrewlee_adventofcode2022_csharp.Day5;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day6 : Day
    {
        public override int Part1Solve()
        {
            List<char> inputChars = LoadInputs("202206")[0].ToCharArray().ToList();
            int i = 0;
            while (inputChars.GetRange(i, 4).Distinct().Count() != 4) {
                i++;
            }
            return i+4;
        }

        public override int Part2Solve()
        {
            List<char> inputChars = LoadInputs("202206")[0].ToCharArray().ToList();
            int i = 0;
            while (inputChars.GetRange(i, 14).Distinct().Count() != 14)
            {
                i++;
            }
            return i + 14;
        }
    }
}
