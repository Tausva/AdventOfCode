using System;
using System.Collections.Generic;

namespace AdventOfCode2021.DaysLogic
{
    public class Day03 : Template<string>
    {
        public override string Part1(List<string> input)
        {
            int numberCount = input[0].Length;

            int[] positiveSignalCount = new int[numberCount];
            int[] negativeSignalCount = new int[numberCount];

            foreach (string line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        positiveSignalCount[i]++;
                    }
                    else
                    {
                        negativeSignalCount[i]++;
                    }
                }
            }

            char[] gamma = new char[numberCount];
            char[] epsilon = new char[numberCount];

            for (int i = 0; i < numberCount; i++)
            {
                if (positiveSignalCount[i] > negativeSignalCount[i])
                {
                    gamma[i] = '1';
                    epsilon[i] = '0';
                }
                else
                {
                    gamma[i] = '0';
                    epsilon[i] = '1';
                }
            }

            string gammaString = new string(gamma);
            string epsilonString = new string(epsilon);

            return (ConvertFromBinaryToDecimal(gammaString) * ConvertFromBinaryToDecimal(epsilonString)).ToString();
        }

        public override string Part2(List<string> input)
        {
            string oxygenGenerator = CalculateRating(input, oxygenGeneratorCriteria);
            string co2Scrubber = CalculateRating(input, co2ScrubberCriteria);

            return (ConvertFromBinaryToDecimal(oxygenGenerator) * ConvertFromBinaryToDecimal(co2Scrubber)).ToString();
        }

        private string CalculateRating(List<string> input, Func<int, int, char> Criteria)
        {
            List<string> localInput = new List<string>(input);

            int position = 0;

            while (position < localInput[0].Length)
            {
                int positiveSignalCount = 0;
                int negativeSignalCount = 0;

                foreach (string line in localInput)
                {
                    if (line[position] == '1')
                    {
                        positiveSignalCount++;
                    }
                    else
                    {
                        negativeSignalCount++;
                    }
                }

                char criteria = Criteria(positiveSignalCount, negativeSignalCount);

                for (int i = localInput.Count - 1; i >= 0; i--)
                {
                    if (localInput[i][position] != criteria)
                    {
                        localInput.RemoveAt(i);
                    }
                }

                if (localInput.Count == 1)
                {
                    return localInput[0];
                }
                else
                {
                    position++;
                }
            }
            return "0";
        }

        private char oxygenGeneratorCriteria(int positiveSignalCount, int negativeSignalCount)
        {
            return negativeSignalCount > positiveSignalCount ? '0' : '1';
        }

        private char co2ScrubberCriteria(int positiveSignalCount, int negativeSignalCount)
        {
            return negativeSignalCount <= positiveSignalCount ? '0' : '1';
        }
    }
}