using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.DaysLogic
{
    class Day06 : Template<string>
    {
        public override string Part1(List<string> input)
        {
            return CalculateGrowth(input, 80);
        }

        public override string Part2(List<string> input)
        {
            return CalculateGrowth(input, 256);
        }

        private string CalculateGrowth(List<string> input, int epochCount)
        {
            long[] fishSchool = new long[9];

            List<string> initialState = new List<string>();
            foreach (string line in input)
            {
                initialState = line.Split(',').ToList();
            }
            foreach (string fish in initialState)
            {
                fishSchool[long.Parse(fish)]++;
            }

            for (int i = 0; i < epochCount; i++)
            {
                long[] newFishSchool = new long[9];
                for (int j = 0; j < fishSchool.Length; j++)
                {
                    if (j == 0 && fishSchool[j] > 0)
                    {
                        newFishSchool[6] += fishSchool[0];
                        newFishSchool[8] += fishSchool[0];
                    }
                    else if (j > 0)
                    {
                        newFishSchool[j - 1] += fishSchool[j];
                    }
                }
                fishSchool = newFishSchool;
            }

            long finalFishCount = 0;
            foreach (long fishCount in fishSchool)
            {
                finalFishCount += fishCount;
            }

            return finalFishCount.ToString();
        }
    }
}