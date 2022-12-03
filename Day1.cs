namespace andrewlee_adventofcode2022_csharp
{
    public class Day1 : Day
    {
        public override int Part1Solve()
        {
            string[] inputs = LoadInputs("202201");
            int caloriesCarriedByBiggestElf = 0;
            int caloriesCarriedByElf = 0;
            for (int i = 0; i < inputs.Count(); i++)
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
            caloriesCarriedByBiggestElf = Math.Max(caloriesCarriedByBiggestElf, caloriesCarriedByElf);

            return caloriesCarriedByBiggestElf;
        }

        public override int Part2Solve()
        {
            string[] inputs = this.LoadInputs("202201");
            int caloriesCarriedByElf = 0;
            List<int> caloriesByElf = new List<int> ();
            for (int i = 0; i < inputs.Count(); i++)
            {
                if (String.IsNullOrWhiteSpace(inputs[i])) {
                    caloriesByElf.Add(caloriesCarriedByElf);
                    caloriesCarriedByElf = 0;
                }
                else
                {
                    caloriesCarriedByElf += Int32.Parse(inputs[i]);
                }
               
            }
            caloriesByElf.Add(caloriesCarriedByElf);
            caloriesByElf.Sort();
            return caloriesByElf[caloriesByElf.Count - 1] + caloriesByElf[caloriesByElf.Count - 2] + caloriesByElf[caloriesByElf.Count - 3];
           // return caloriesCarriedByBiggestThreeElves.Sum();
        }
    }
}
