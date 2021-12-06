using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            IOManager iOManager = new IOManager();
            LogicModule logicModule = new LogicModule();

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Welcome to advent of code calender 2020");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Choose the task number:");

            string choice = Console.ReadLine();

            int choiceInt = 0;
            if (int.TryParse(choice, out choiceInt))
            {
                float answer = -1;
                long longAnswer = -1;
                string stringAnswer = "";

                List<int> inputIntList;
                List<string> inputStringList;

                switch (choiceInt)
                {
                    case 1:
                        inputIntList = iOManager.ReadIntList("Day01.txt");
                        answer = logicModule.CalculateDay1Task1(inputIntList);

                        Console.WriteLine("Day1 first task answer: " + answer);
                        break;

                    case 2:
                        inputIntList = iOManager.ReadIntList("Day01.txt");
                        answer = logicModule.CalculateDay1Task2(inputIntList);

                        Console.WriteLine("Day1 second task answer: " + answer);
                        break;

                    case 3:
                        inputStringList = iOManager.ReadStringList("Day02.txt");
                        answer = logicModule.CalculateDay2Task1(inputStringList);

                        Console.WriteLine("Day2 first task answer: " + answer);
                        break;

                    case 4:
                        inputStringList = iOManager.ReadStringList("Day02.txt");
                        answer = logicModule.CalculateDay2Task2(inputStringList);

                        Console.WriteLine("Day2 second task answer: " + answer);
                        break;

                    case 5:
                        inputStringList = iOManager.ReadStringList("Day03.txt");
                        answer = logicModule.CalculateDay3Task1(inputStringList);

                        Console.WriteLine("Day3 first task answer: " + answer);
                        break;

                    case 6:
                        inputStringList = iOManager.ReadStringList("Day03.txt");
                        answer = logicModule.CalculateDay3Task2(inputStringList);
                        string format = "0 000 000 000";

                        Console.WriteLine("Day3 second task answer: " + answer.ToString(format));
                        break;

                    case 7:
                        inputStringList = iOManager.ReadStringList("Day04.txt");
                        answer = logicModule.CalculateDay4Task1(inputStringList);

                        Console.WriteLine("Day4 first task answer: " + answer);
                        break;

                    case 8:
                        inputStringList = iOManager.ReadStringList("Day04.txt");
                        answer = logicModule.CalculateDay4Task2(inputStringList);

                        Console.WriteLine("Day4 second task answer: " + answer);
                        break;

                    case 9:
                        inputStringList = iOManager.ReadStringList("Day05.txt");
                        answer = logicModule.CalculateDay5Task1(inputStringList);

                        Console.WriteLine("Day5 first task answer: " + answer);
                        break;

                    case 10:
                        inputStringList = iOManager.ReadStringList("Day05.txt");
                        answer = logicModule.CalculateDay5Task2(inputStringList);

                        Console.WriteLine("Day5 second task answer: " + answer);
                        break;

                    case 11:
                        inputStringList = iOManager.ReadStringList("Day06.txt");
                        answer = logicModule.CalculateDay6Task1(inputStringList);

                        Console.WriteLine("Day 6 first task answer: " + answer);
                        break;

                    case 12:
                        inputStringList = iOManager.ReadStringList("Day06.txt");
                        answer = logicModule.CalculateDay6Task2(inputStringList);

                        Console.WriteLine("Day 6 second task answer: " + answer);
                        break;

                    case 13:
                        inputStringList = iOManager.ReadStringList("Day07.txt");
                        answer = logicModule.CalculateDay7Task1(inputStringList);

                        Console.WriteLine("Day 7 first task answer: " + answer);
                        break;

                    case 14:
                        inputStringList = iOManager.ReadStringList("Day07.txt");
                        answer = logicModule.CalculateDay7Task2(inputStringList);

                        Console.WriteLine("Day 7 second task answer: " + answer);
                        break;

                    case 15:
                        inputStringList = iOManager.ReadStringList("Day08.txt");
                        answer = logicModule.CalculateDay8Task1(inputStringList);

                        Console.WriteLine("Day 8 first task answer: " + answer);
                        break;

                    case 16:
                        inputStringList = iOManager.ReadStringList("Day08.txt");
                        answer = logicModule.CalculateDay8Task2(inputStringList);

                        Console.WriteLine("Day 8 second task answer: " + answer);
                        break;

                    case 17:
                        inputIntList = iOManager.ReadIntList("Day09.txt");
                        answer = logicModule.CalculateDay9Task1(inputIntList);

                        Console.WriteLine("Day 9 first task answer: " + answer);
                        break;

                    case 18:
                        inputIntList = iOManager.ReadIntList("Day09.txt");
                        answer = logicModule.CalculateDay9Task2(inputIntList);

                        Console.WriteLine("Day 9 second task answer: " + answer);
                        break;

                    case 19:
                        inputIntList = iOManager.ReadIntList("Day10.txt");
                        answer = logicModule.CalculateDay10Task1(inputIntList);

                        Console.WriteLine("Day 10 first task answer: " + answer);
                        break;

                    case 20:
                        inputIntList = iOManager.ReadIntList("Day10.txt");
                        longAnswer = logicModule.CalculateDay10Task2(inputIntList);

                        format = "0 000 000 000";

                        Console.WriteLine("Day 10 second task answer: " + longAnswer.ToString(format));
                        break;

                    case 21:
                        inputStringList = iOManager.ReadStringList("Day11.txt");
                        answer = logicModule.CalculateDay11Task1(inputStringList);

                        Console.WriteLine("Day 11 first task answer: " + answer);
                        break;

                    case 22:
                        inputStringList = iOManager.ReadStringList("Day11.txt");
                        answer = logicModule.CalculateDay11Task2(inputStringList);

                        Console.WriteLine("Day 11 second task answer: " + answer);
                        break;

                    case 23:
                        inputStringList = iOManager.ReadStringList("Day12.txt");
                        answer = logicModule.CalculateDay12Task1(inputStringList);

                        Console.WriteLine("Day 12 first task answer: " + answer);
                        break;

                    case 24:
                        inputStringList = iOManager.ReadStringList("Day12.txt");
                        answer = logicModule.CalculateDay12Task2(inputStringList);

                        Console.WriteLine("Day 12 second task answer: " + answer);
                        break;

                    case 25:
                        inputStringList = iOManager.ReadStringList("Day13.txt");
                        answer = logicModule.CalculateDay13Task1(inputStringList);

                        Console.WriteLine("Day 13 first task answer: " + answer);
                        break;

                    case 26:
                        inputStringList = iOManager.ReadStringList("Day13.txt");
                        longAnswer = logicModule.CalculateDay13Task2(inputStringList);

                        format = "0000000000";

                        Console.WriteLine("Day 10 second task answer: " + longAnswer.ToString(format));
                        break;

                    case 27:
                        inputStringList = iOManager.ReadStringList("Day14.txt");
                        longAnswer = logicModule.CalculateDay14Task1(inputStringList);

                        format = "0000000000";

                        Console.WriteLine("Day 14 first task answer: " + longAnswer.ToString(format));
                        break;

                    case 28:
                        inputStringList = iOManager.ReadStringList("Day14.txt");
                        longAnswer = logicModule.CalculateDay14Task2(inputStringList);

                        format = "0000000000";

                        Console.WriteLine("Day 14 second task answer: " + longAnswer.ToString(format));
                        break;

                    case 29:
                        inputIntList = new List<int>(new int[] { 2, 1, 10, 11, 0, 6 });
                        answer = logicModule.CalculateDay15(inputIntList, 2020);

                        Console.WriteLine("Day 15 first task answer: " + answer);
                        break;

                    case 30:
                        inputIntList = new List<int>(new int[] { 2, 1, 10, 11, 0, 6 });
                        answer = logicModule.CalculateDay15(inputIntList, 30000000);

                        Console.WriteLine("Day 15 second task answer: " + answer);
                        break;

                    case 31:
                        inputStringList = iOManager.ReadStringList("Day16.txt");
                        answer = logicModule.CalculateDay16Task1(inputStringList);

                        Console.WriteLine("Day 16 first task answer: " + answer);
                        break;

                    case 32:
                        inputStringList = iOManager.ReadStringList("Day16.txt");
                        longAnswer = logicModule.CalculateDay16Task2(inputStringList);

                        format = "0000000000";

                        Console.WriteLine("Day 16 second task answer: " + longAnswer.ToString(format));
                        break;

                    case 33:
                        inputStringList = iOManager.ReadStringList("Day17.txt");
                        answer = logicModule.CalculateDay17Task1(inputStringList);

                        Console.WriteLine("Day 17 first task answer: " + answer);
                        break;

                    case 34:
                        inputStringList = iOManager.ReadStringList("Day17.txt");
                        answer = logicModule.CalculateDay17Task2(inputStringList);

                        Console.WriteLine("Day 17 second task answer: " + answer);
                        break;

                    case 35:
                        inputStringList = iOManager.ReadStringList("Day18.txt");
                        longAnswer = logicModule.CalculateDay18Task1(inputStringList);

                        format = "0000000000";

                        Console.WriteLine("Day 18 first task answer: " + longAnswer.ToString(format));
                        break;

                    case 36:
                        inputStringList = iOManager.ReadStringList("Day18.txt");
                        longAnswer = logicModule.CalculateDay18Task2(inputStringList);

                        format = "0000000000";

                        Console.WriteLine("Day 18 second task answer: " + longAnswer.ToString(format));
                        break;

                    case 37:
                        inputStringList = iOManager.ReadStringList("Day19.txt");
                        answer = logicModule.CalculateDay19Task1(inputStringList);

                        Console.WriteLine("Day 19 first task answer: " + answer);
                        break;

                    case 38:
                        inputStringList = iOManager.ReadStringList("Day19.txt");
                        answer = logicModule.CalculateDay19Task2(inputStringList);

                        Console.WriteLine("Day 19 second task answer: " + answer);
                        break;

                    case 39:
                        inputStringList = iOManager.ReadStringList("Day20.txt");
                        longAnswer = logicModule.CalculateDay20Task1(inputStringList);
                        
                        format = "0000000000";

                        Console.WriteLine("Day 20 first task answer: " + longAnswer.ToString(format));
                        break;

                    case 40:
                        inputStringList = iOManager.ReadStringList("Day20.txt");
                        answer = logicModule.CalculateDay20Task2(inputStringList);

                        Console.WriteLine("Day 20 second task answer: " + answer);
                        break;

                    case 41:
                        inputStringList = iOManager.ReadStringList("Day21.txt");
                        answer = logicModule.CalculateDay21Task1(inputStringList);

                        Console.WriteLine("Day 21 first task answer: " + answer);
                        break;

                    case 42:
                        inputStringList = iOManager.ReadStringList("Day21.txt");
                        stringAnswer = logicModule.CalculateDay21Task2(inputStringList);

                        Console.WriteLine("Day 21 second task answer: [" + stringAnswer + "]");
                        break;

                    case 43:
                        inputStringList = iOManager.ReadStringList("Day22.txt");
                        answer = logicModule.CalculateDay22Task1(inputStringList);

                        Console.WriteLine("Day 22 first task answer: " + answer);
                        break;

                    case 44:
                        inputStringList = iOManager.ReadStringList("Day22.txt");
                        answer = logicModule.CalculateDay22Task2(inputStringList);

                        Console.WriteLine("Day 22 second task answer: " + answer);
                        break;

                    case 45:
                        stringAnswer = logicModule.CalculateDay23Task1("364297581", 100);

                        Console.WriteLine("Day 23 first task answer: " + stringAnswer);
                        break;

                    case 46:
                        stringAnswer = logicModule.CalculateDay23Task2("364297581", 10000000, 1000000);

                        Console.WriteLine("Day 23 second task answer: " + stringAnswer);
                        break;

                    default:
                        Console.WriteLine("Invalid or Not yet implemented task number!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Input is not an integer!");
            }
        }
    }
}