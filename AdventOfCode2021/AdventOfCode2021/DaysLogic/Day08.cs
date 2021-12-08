using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.DaysLogic
{
    public class Day08 : Template<string>
    {
        public override string Part1(List<string> input)
        {
            int count = 0;
            foreach (string line in input)
            {
                string[] collection = line.Split(" | ");

                string[] displays = collection[1].Split(" ");

                foreach (string display in displays)
                {
                    switch (display.Length)
                    {
                        case 2:
                        case 4:
                        case 3:
                        case 7:
                            count++;
                            break;
                    }
                }
            }
            return count.ToString();
        }

        public override string Part2(List<string> input)
        {
            int displaySum = 0;
            foreach (string line in input)
            {
                string[] collection = line.Split(" | ");

                string[] segmentMap = new string[10];
                List<string> fivers = new List<string>();
                List<string> sixers = new List<string>();

                string[] numbers = collection[0].Split(" ");
                
                //Known number mapping
                foreach (string number in numbers)
                {
                    switch (number.Length)
                    {
                        case 2:
                            segmentMap[1] = number;
                            break;
                        case 4:
                            segmentMap[4] = number;
                            break;
                        case 3:
                            segmentMap[7] = number;
                            break;
                        case 7:
                            segmentMap[8] = number;
                            break;
                        case 6:
                            sixers.Add(number);
                            break;
                        case 5:
                            fivers.Add(number);
                            break;
                    }
                }

                segmentMap[3] = ParseOutNumber(fivers, segmentMap[1], 2);
                fivers.Remove(segmentMap[3]);

                segmentMap[5] = ParseOutNumber(fivers, segmentMap[4], 3);
                fivers.Remove(segmentMap[5]);

                segmentMap[2] = fivers[0];

                segmentMap[6] = ParseOutNumber(sixers, segmentMap[1], 1);
                sixers.Remove(segmentMap[6]);

                segmentMap[0] = ParseOutNumber(sixers, segmentMap[4], 3);
                sixers.Remove(segmentMap[0]);

                segmentMap[9] = sixers[0];

                string[] displays = collection[1].Split(" ");
                int displayValue = 0;
                foreach (string display in displays)
                {
                    for (int i = 0; i < segmentMap.Length; i++)
                    {
                        if (ContainsString(display, segmentMap[i]) == display.Length)
                        {
                            displayValue = displayValue * 10 + i;
                        }
                    }
                }
                displaySum += displayValue;
            }
            return displaySum.ToString();
        }

        private int ContainsString(string origin, string comparer)
        {
            int comparisonCount = 0;

            if (origin.Length < comparer.Length) return comparisonCount;

            foreach (char item in comparer)
            {
                if (origin.Contains(item)) comparisonCount++;
            }

            return comparisonCount;
        }

        private string ParseOutNumber(List<string> numbers, string comparer, int requiredLength)
        {
            foreach (string number in numbers)
            {
                if (ContainsString(number, comparer) == requiredLength)
                {
                    return number;
                }
            }
            return "";
        }
    }
}