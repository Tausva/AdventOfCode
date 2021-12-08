using System;
using System.Collections.Generic;
using AdventOfCode2021.DaysLogic;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            IOManager.Instantiate();
            List<string> input;
            string answer;

            Template<string> currentDay = new Day08();

            //input = IOManager.ReadStringList("Test.txt");
            input = IOManager.ReadStringList("Day08.txt");
            //answer = currentDay.Part1(input);
            answer = currentDay.Part2(input);

            Console.WriteLine("Answer: " + answer);
        }

    }
}