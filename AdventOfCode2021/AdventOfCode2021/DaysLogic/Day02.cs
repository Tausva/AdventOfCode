using System.Collections.Generic;

namespace AdventOfCode2021.DaysLogic
{
    class Day02 : Template<string>
    {
        public override string Part1(List<string> input)
        {
            int position = 0;
            int depth = 0;

            foreach (string line in input)
            {
                string[] commands = line.Split(' ');

                switch (commands[0])
                {
                    case "forward":
                        position += int.Parse(commands[1]);
                        break;
                    case "up":
                        depth -= int.Parse(commands[1]);
                        break;
                    case "down":
                        depth += int.Parse(commands[1]);
                        break;
                }
            }

            return (position * depth).ToString();
        }

        public override string Part2(List<string> input)
        {
            int position = 0;
            int depth = 0;
            int aim = 0;

            foreach (string line in input)
            {
                string[] commands = line.Split(' ');

                switch (commands[0])
                {
                    case "forward":
                        int value = int.Parse(commands[1]);
                        position += value;
                        depth += aim * value;
                        break;
                    case "up":
                        aim -= int.Parse(commands[1]);
                        break;
                    case "down":
                        aim += int.Parse(commands[1]);
                        break;
                }
            }

            return (position * depth).ToString();
        }
    }
}