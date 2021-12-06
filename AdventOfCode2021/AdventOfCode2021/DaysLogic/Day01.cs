using System.Collections.Generic;

namespace AdventOfCode2021.DaysLogic
{
    class Day01 : Template<int>
    {
        public override string Part1(List<int> input)
        {
            int counter = 0;

            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] > input[i - 1])
                {
                    counter++;
                }
            }

            return counter.ToString();
        }

        public override string Part2(List<int> input)
        {
            int counter = 0;

            for (int i = 1; i < input.Count - 2; i++)
            {
                int firstWindow = input[i - 1] + input[i] + input[i + 1];
                int secondtWindow = input[i] + input[i + 1] + input[i + 2];

                if (secondtWindow > firstWindow)
                {
                    counter++;
                }
            }

            return counter.ToString();
        }
    }
}