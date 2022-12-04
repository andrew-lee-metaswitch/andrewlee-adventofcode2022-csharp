using System.Diagnostics;
using System.Text.Unicode;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Channels;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day3 : Day
    {
        public override int Part1Solve()
        {
            string[] inputs = LoadInputs("202203");

            int prioritySum = 0;
            foreach (string input in inputs)
            {
                char[] rucksackChars = input.ToCharArray();

                (List<char>, List<char>) rucksackCompartments = (
                    rucksackChars.Take(rucksackChars.Length / 2).ToList(),
                    rucksackChars.Skip(rucksackChars.Length / 2).ToList());

                char commonChar = rucksackCompartments.Item1.Where(c => rucksackCompartments.Item2.Contains(c)).FirstOrDefault();
                prioritySum += charValue(commonChar);
            }
            return prioritySum;

        }

        private int charValue (char commonChar) {
            int returnValue = (int) commonChar;
            if (65 <= (int) commonChar && (int) commonChar <= 90)
            {
                // 65 = A -> 27
                returnValue -= 38;
            } else
            {
                // 97 = a -> 1
                returnValue -= 96;
            }
            return returnValue;
        }

        public override int Part2Solve()
        {
            string[] inputs = this.LoadInputs("202203");
            int prioritySum = 0;
            int index = 0;
            List<List<char>> rucksackCompartments = new List<List<char>> { };
            foreach (string input in inputs)
            {
                rucksackCompartments.Add(input.ToCharArray().ToList());
                if (index % 3 == 2)
                {
                    char commonChar = rucksackCompartments[0].Where(
                        c => rucksackCompartments[1].Contains(c)).Where(
                        c => rucksackCompartments[2].Contains(c)).FirstOrDefault();
                    prioritySum += charValue(commonChar);
                    rucksackCompartments = new List<List<char>> { };
                }
                index++;
            }
            return prioritySum;
        }
    }
}
