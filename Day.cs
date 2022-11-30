namespace andrewlee_adventofcode2022_csharp
{
    using System.Reflection;
    public abstract class Day
    {
        public string[] LoadInputs(string filename)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "Inputs", $"{filename}.txt");
            return File.ReadAllLines(path);
        }

        public abstract int Part1Solve();

        public abstract int Part2Solve();
    }
}