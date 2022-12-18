using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using static andrewlee_adventofcode2022_csharp.Day5;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day7 : Day
    {
        public class File
        {
            public string name;
            public string path;
            public int size;
            public File (string name, string path, int size)
            {
                this.name = name;
                this.path = path;
                this.size = size;
            }   
        }

        public class Directory
        {
            public List<File> SubFiles;
            public string name;
            public string path;

            public int SizeOfFiles()
            {
                return SubFiles.Select(f => f.size).Sum();
            }

            public int Size(List<Directory> AllDirectories)
            {
                return AllDirectories.Where(d => d.path.Contains(this.path)).Select(d => d.SizeOfFiles()).Sum();
            }
            public Directory(string name, string path)
            {
                this.name = name;
                this.path = path;
                this.SubFiles= new List<File>();

            }

            public string parentDirectoryPath()
            {
                return string.Join("/", this.path.Split("/").SkipLast(2)) + "/";
            }

        }

        string cdDownRegex = @"^\$ cd (\w+)$";
        string cdUpRegex = @"^\$ cd \.\.$";
        string lsFileRegex = @"^(\d+) ([\.\w]+)$";
        string lsDirRegex = @"^dir ([\.\w]+)$";

        private List<Directory> ParseInputs()
        {
            string[] inputs = LoadInputs("202207");
            List<Directory> AllDirectories = new List<Directory>() { new Directory("/", "/") };
            Directory currentWorkingDirectory = AllDirectories[0];

            foreach (string input in inputs)
            {
                if (Regex.IsMatch(input, lsFileRegex))
                {
                    int size = Int32.Parse(Regex.Match(input, lsFileRegex).Groups[1].Value);
                    string name = Regex.Match(input, lsFileRegex).Groups[2].Value;
                    currentWorkingDirectory.SubFiles.Add(
                        new File(
                            name,
                            path: $"{currentWorkingDirectory.path}{name}",
                            size)
                        );
                }
                else if (Regex.IsMatch(input, lsDirRegex))
                {
                    string name = Regex.Match(input, lsDirRegex).Groups[1].Value;
                    AllDirectories.Add(
                        new Directory(
                            name,
                            path: $"{currentWorkingDirectory.path}{name}/")
                        );
                }
                else if (Regex.IsMatch(input, cdUpRegex))
                {
                    currentWorkingDirectory = AllDirectories.SingleOrDefault(d => d.path == currentWorkingDirectory.parentDirectoryPath());
                }
                else if (Regex.IsMatch(input, cdDownRegex))
                {
                    string name = Regex.Match(input, cdDownRegex).Groups[1].Value;
                    currentWorkingDirectory = AllDirectories.SingleOrDefault(d => d.path == $"{currentWorkingDirectory.path}{name}/");
                }
            }
            return AllDirectories;
        }

        public override int Part1Solve()
        {
            List<Directory> AllDirectories = ParseInputs();
            return AllDirectories.Where(d => d.Size(AllDirectories) <= 100000).Select(d => d.Size(AllDirectories)).Sum();
        }

        public override int Part2Solve()
        {
            List<Directory> AllDirectories = ParseInputs();
            int currentDirectorySize = AllDirectories.Select(d => d.Size(AllDirectories)).Max();
            int freeSpace = 70000000 - (currentDirectorySize);
            int spaceNeededForUpdate = 30000000 - freeSpace;
            Console.WriteLine($"Current space is {currentDirectorySize}, free space is {freeSpace}, so need {spaceNeededForUpdate} space");
            return AllDirectories.Select(d => d.Size(AllDirectories)).Where(s => s>= spaceNeededForUpdate).Min();
        }



    }
}
