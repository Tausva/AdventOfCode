using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using AdventOfCode2020.Structures;

namespace AdventOfCode2020
{
    class LogicModule
    {
        public int CalculateDay1Task1(List<int> inputList)
        {
            int expectedSum = 2020;

            int[] input = inputList.ToArray();
            QuickSort(input, 0, input.Length - 1);

            int sumOfInput = 0;
            for (int j = input.Length - 1; j >= 0; j--)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    sumOfInput = input[i] + input[j];
                    if (sumOfInput == expectedSum)
                    {
                        return input[i] * input[j];
                    }
                    else if (sumOfInput > expectedSum)
                    {
                        break;
                    }
                }
            }

            return -1;
        }

        public int CalculateDay1Task2(List<int> inputList)
        {
            int expectedSum = 2020;

            int[] input = inputList.ToArray();
            QuickSort(input, 0, input.Length - 1);

            int sumOfInput = 0;
            for (int j = input.Length - 1; j >= 0; j--)
            {
                for (int iFirst = 0; iFirst < input.Length; iFirst++)
                {
                    if (input[iFirst] * 2 + input[j] > expectedSum)
                    {
                        break;
                    }
                    for (int iSecond = iFirst + 1; iSecond < input.Length; iSecond++)
                    {
                        sumOfInput = input[iFirst] + input[iSecond] + input[j];
                        if (sumOfInput == expectedSum)
                        {
                            return input[iFirst] * input[iSecond] * input[j];
                        }
                        else if (sumOfInput > expectedSum)
                        {
                            break;
                        }
                    }
                }
            }

            return -1;
        }

        public int CalculateDay2Task1(List<string> inputList)
        {
            int rightPassCount = 0;

            foreach (string passwordPolicy in inputList)
            {
                int minBound, maxBound;
                char targetChar;
                string password;

                SeperatePasswordPolicy(passwordPolicy, out minBound, out maxBound, out targetChar, out password);

                int currAmount = 0;
                foreach(char character in password)
                {
                    if (character == targetChar)
                    {
                        currAmount++;
                    }
                }
                if (currAmount >= minBound && currAmount <= maxBound)
                {
                    rightPassCount++;
                }
            }

            return rightPassCount;
        }

        public int CalculateDay2Task2(List<string> inputList)
        {
            int rightPassCount = 0;

            foreach (string passwordPolicy in inputList)
            {
                int position1, position2;
                char targetChar;
                string password;

                SeperatePasswordPolicy(passwordPolicy, out position1, out position2, out targetChar, out password);

                int currAmount = 0;
                if (password[position1 - 1] == targetChar)
                {
                    currAmount++;
                }
                if (password[position2 - 1] == targetChar)
                {
                    currAmount++;
                }

                if (currAmount == 1)
                {
                    rightPassCount++;
                }
            }

            return rightPassCount;
        }

        public int CalculateDay3Task1(List<string> inputList)
        {
            return CalculateLoopingDescent(inputList, 1, 3);
        }

        public long CalculateDay3Task2(List<string> inputList)
        {
            long answer = 1; //For multiplication purposes can't be zero

            int[,] slopes = new int[,] { { 1, 1}, { 3, 1}, { 5, 1}, { 7, 1}, { 1, 2} };

            for (int i = 0; i < slopes.GetLength(0); i++)
            {
                answer *= CalculateLoopingDescent(inputList, slopes[i, 1], slopes[i, 0]);
            }

            return answer;
        }

        public int CalculateDay4Task1(List<string> inputList)
        {
            return CalculateValidPassports(inputList, GetPassportInformation);
        }

        public int CalculateDay4Task2(List<string> inputList)
        {
            return CalculateValidPassports(inputList, GetValidatedPassportInformation);
        }

        public int CalculateDay5Task1(List<string> inputList)
        {
            int highestID = 0;

            foreach (string boardingPass in inputList)
            {
                string row = boardingPass.Substring(0, 7);
                string col = boardingPass.Substring(7);

                int currentID = ConvertLetterBinaryToDecimal(row, 'B') * 8 + ConvertLetterBinaryToDecimal(col, 'R');

                highestID = currentID > highestID ? currentID : highestID;
            }

            return highestID;
        }

        public int CalculateDay5Task2(List<string> inputList)
        {
            int[] seatsInThePlain = new int[807];

            foreach (string boardingPass in inputList)
            {
                string row = boardingPass.Substring(0, 7);
                string col = boardingPass.Substring(7);

                int currentID = ConvertLetterBinaryToDecimal(row, 'B') * 8 + ConvertLetterBinaryToDecimal(col, 'R');
                seatsInThePlain[currentID] = 1;
            }

            for (int i = 0; i < seatsInThePlain.Length; i++)
            {
                if (seatsInThePlain[i] == 0 &&
                    i - 1 >= 0 && seatsInThePlain[i - 1] == 1 && 
                    i + 1 < seatsInThePlain.Length && seatsInThePlain[i + 1] == 1)
                {
                    return i;
                }
            }

            return 0;
        }

        public int CalculateDay6Task1(List<string> inputList)
        {
            int answerCount = 0;

            Dictionary<char, char> answers = new Dictionary<char, char>();

            foreach (string line in inputList)
            {
                if (line == "")
                {
                    answerCount += answers.Count;
                    answers = new Dictionary<char, char>();
                }
                else
                {
                    foreach (char value in line)
                    {
                        if (!answers.ContainsKey(value))
                        {
                            answers.Add(value, value);
                        }
                    }
                }
            }

            return answerCount + answers.Count;
        }

        public int CalculateDay6Task2(List<string> inputList)
        {
            int answerCount = 0;

            Dictionary<char, int> answers = new Dictionary<char, int>();
            int groupSize = 0;

            foreach (string line in inputList)
            {
                if (line == "")
                {
                    answerCount += CountMaxValues(answers, groupSize);
                    groupSize = 0;
                    answers = new Dictionary<char, int>();
                }
                else
                {
                    groupSize++;
                    foreach (char value in line)
                    {
                        if (!answers.ContainsKey(value))
                        {
                            answers.Add(value, 1);
                        }
                        else
                        {
                            answers[value]++;
                        }
                    }
                }
            }

            return answerCount + CountMaxValues(answers, groupSize);
        }

        public int CalculateDay7Task1(List<string> inputList)
        {
            Dictionary<string, ColorBagNode> addedBags = CreateBagNodeTreeDictionary(inputList);

            int parentAmount = -1;
            Queue<ColorBagNode> parents = new Queue<ColorBagNode>();
            Dictionary<string, string> usedBags = new Dictionary<string, string>();

            ColorBagNode goldBag = addedBags["shiny gold"];
            parents.Enqueue(goldBag);

            while (parents.Count > 0)
            {
                ColorBagNode currentParent = parents.Dequeue();

                if (!usedBags.ContainsKey(currentParent.BagColor))
                {
                    parentAmount++;
                    usedBags.Add(currentParent.BagColor, "");

                    foreach (Tuple<int, ColorBagNode> node in currentParent.Parents)
                    {
                        parents.Enqueue(node.Item2);
                    }
                }
            }

            return parentAmount;
        }

        public int CalculateDay7Task2(List<string> inputList)
        {
            Dictionary<string, ColorBagNode> addedBags = CreateBagNodeTreeDictionary(inputList);

            Queue<ColorBagNode> childs = new Queue<ColorBagNode>();

            ColorBagNode goldBag = addedBags["shiny gold"];

            return GetBagAmountInChild(goldBag);
        }

        public int CalculateDay8Task1(List<string> inputList)
        {
            bool placeholder;
            return RunPSPConsoleComands(inputList, out placeholder);
        }

        public int CalculateDay8Task2(List<string> inputList)
        {
            int lineChangerIndex = 0;

            while (lineChangerIndex < inputList.Count)
            {
                string command = inputList[lineChangerIndex].Substring(0, inputList[lineChangerIndex].IndexOf(' '));

                if (command != "acc")
                {
                    List<string> modifiedCommands = new List<string>(inputList);
                    command = command == "jmp" ? "nop" : "jmp";
                    modifiedCommands[lineChangerIndex] = command + modifiedCommands[lineChangerIndex].Substring(inputList[lineChangerIndex].IndexOf(' '));

                    bool enteredLoop = true;
                    int value = RunPSPConsoleComands(modifiedCommands, out enteredLoop);

                    if (!enteredLoop)
                    {
                        return value;
                    }
                }
                lineChangerIndex++;
            }

            return -1;
        }

        public int CalculateDay9Task1(List<int> inputList)
        {
            int preambleLength = 25;
            int checkIndex = preambleLength;

            while (checkIndex < inputList.Count)
            {
                if (!CheckForSum(inputList.GetRange(checkIndex - preambleLength, preambleLength), inputList[checkIndex]))
                {
                    return inputList[checkIndex];
                }
                checkIndex++;
            }

            return -1;
        }

        public int CalculateDay9Task2(List<int> inputList)
        {
            int invalidSum = CalculateDay9Task1(inputList);

            int startingIndex = 0;
            int maxIndex = inputList.IndexOf(invalidSum);

            while (startingIndex < maxIndex)
            {
                int currentSum = 0;
                int currentIndex = startingIndex;
                List<int> addatives = new List<int>();

                while (currentSum < invalidSum)
                {
                    currentSum += inputList[currentIndex];
                    addatives.Add(inputList[currentIndex++]);

                    if (currentSum == invalidSum)
                    {
                        int[] output = addatives.ToArray();
                        QuickSort(output, 0, output.Length - 1);

                        return output[0] + output[output.Length - 1];
                    }
                }
                startingIndex++;
            }

            return -1;
        }

        public int CalculateDay10Task1(List<int> inputList)
        {
            int[] input = inputList.ToArray();
            QuickSort(input, 0, input.Length - 1);

            int diff1Jolt = 0;
            int diff3Jolt = 1;

            int index = 0;
            int currentJolts = 0;

            while (index < input.Length)
            {
                if (input[index] - currentJolts == 1)
                {
                    diff1Jolt++;
                }
                else if (input[index] - currentJolts == 3)
                {
                    diff3Jolt++;
                }
                else
                {
                    Console.WriteLine("Diff is different ({0} and {1})", input[index], currentJolts);
                }
                currentJolts = input[index++];
            }

            return diff1Jolt * diff3Jolt;
        }

        public long CalculateDay10Task2(List<int> inputList)
        {
            int[] input = inputList.ToArray();
            QuickSort(input, 0, input.Length - 1);

            inputList = new List<int>(input);
            inputList.Add(inputList[inputList.Count - 1] + 3);
            inputList.Insert(0, 0);

            List<long> combinationCount = new List<long>(new long[inputList.Count]);
            combinationCount[0] = 1;

            for (int i = 1; i < inputList.Count; i++)
            {
                if (i - 3 >= 0 && inputList[i - 3] + 3 == inputList[i])
                {
                    combinationCount[i] = combinationCount[i - 3] + combinationCount[i - 2] +combinationCount[i - 1];
                }
                else if (i - 2 >= 0 && inputList[i - 2] + 3 >= inputList[i])
                {
                    combinationCount[i] = combinationCount[i - 2] + combinationCount[i - 1];
                }
                else
                {
                    combinationCount[i] = combinationCount[i - 1];
                }
            }

            return combinationCount[combinationCount.Count - 1];
        }

        public int CalculateDay11Task1(List<string> inputList)
        {
            char[][] input = new char[inputList.Count][];
            for (int i = 0; i < inputList.Count; i++)
            {
                input[i] = inputList[i].ToCharArray();
            }

            int changeAmount = 0;
            bool firstLoop = true;

            int ocupiedSeatsCount = 0;

            while (firstLoop || changeAmount > 0)
            {
                firstLoop = false;
                changeAmount = 0;

                char[][] newInput = input.Select(x => x.ToArray()).ToArray();

                for (int y = 0; y < input.Length; y++)
                {
                    for (int x = 0; x < input[y].Length; x++)
                    {
                        if (input[y][x] == 'L')
                        {
                            int adjecentOcupiedSeatsCount = GetSeatsCount(input, y, x, '#');

                            if (adjecentOcupiedSeatsCount == 0)
                            {
                                newInput[y][x] = '#';
                                ocupiedSeatsCount++;
                                changeAmount++;
                            }
                        }
                        else if (input[y][x] == '#')
                        {
                            int adjecentOcupiedSeatsCount = GetSeatsCount(input, y, x, '#');

                            if (adjecentOcupiedSeatsCount >= 4)
                            {
                                newInput[y][x] = 'L';
                                ocupiedSeatsCount--;
                                changeAmount++;
                            }
                        }
                    }
                }
                input = newInput.Select(x => x.ToArray()).ToArray();
            }
            return ocupiedSeatsCount;
        }

        public int CalculateDay11Task2(List<string> inputList)
        {
            char[][] input = new char[inputList.Count][];
            for (int i = 0; i < inputList.Count; i++)
            {
                input[i] = inputList[i].ToCharArray();
            }

            int changeAmount = 0;
            bool firstLoop = true;

            int ocupiedSeatsCount = 0;

            while (firstLoop || changeAmount > 0)
            {
                firstLoop = false;
                changeAmount = 0;

                char[][] newInput = input.Select(x => x.ToArray()).ToArray();

                for (int y = 0; y < input.Length; y++)
                {
                    for (int x = 0; x < input[y].Length; x++)
                    {
                        if (input[y][x] == 'L')
                        {
                            int adjecentOcupiedSeatsCount = GetVisionSeatsCount(input, y, x, '#');

                            if (adjecentOcupiedSeatsCount == 0)
                            {
                                newInput[y][x] = '#';
                                ocupiedSeatsCount++;
                                changeAmount++;
                            }
                        }
                        else if (input[y][x] == '#')
                        {
                            int adjecentOcupiedSeatsCount = GetVisionSeatsCount(input, y, x, '#');

                            if (adjecentOcupiedSeatsCount >= 5)
                            {
                                newInput[y][x] = 'L';
                                ocupiedSeatsCount--;
                                changeAmount++;
                            }
                        }
                    }
                }
                input = newInput.Select(x => x.ToArray()).ToArray();

            }
            return ocupiedSeatsCount;
        }

        public int CalculateDay12Task1(List<string> inputList)
        {
            int northDirection = 0;
            int eastDirection = 0;

            Direction facingDirection = Direction.East;

            foreach (string input in inputList)
            {
                char action = input[0];
                int value = int.Parse(input.Substring(1));

                switch (action)
                {
                    case 'N':
                        northDirection += value;
                        break;

                    case 'S':
                        northDirection -= value;
                        break;

                    case 'E':
                        eastDirection += value;
                        break;

                    case 'W':
                        eastDirection -= value;
                        break;

                    case 'R':
                        facingDirection += (value / 90);
                        while ((int)facingDirection >= 4)
                        {
                            facingDirection -= 4;
                        }
                        break;

                    case 'L':
                        facingDirection -= (value / 90);
                        while ((int)facingDirection < 0)
                        {
                            facingDirection += 4;
                        }
                        break;

                    case 'F':
                        if (facingDirection == Direction.North)
                        {
                            northDirection += value;
                        }
                        else if (facingDirection == Direction.South)
                        {
                            northDirection -= value;
                        }
                        else if (facingDirection == Direction.East)
                        {
                            eastDirection += value;
                        }
                        else
                        {
                            eastDirection -= value;
                        }
                        break;

                    default:
                        Console.WriteLine("Error: " + action + " " + value);
                        break;

                }
            }
            return Math.Abs(northDirection) + Math.Abs(eastDirection);
        }

        public int CalculateDay12Task2(List<string> inputList)
        {
            int northDirection = 0;
            int eastDirection = 0;

            int waypointNorthDirection = 1;
            int waypointEastDirection = 10;

            foreach (string input in inputList)
            {
                char action = input[0];
                int value = int.Parse(input.Substring(1));

                switch (action)
                {
                    case 'N':
                        waypointNorthDirection += value;
                        break;

                    case 'S':
                        waypointNorthDirection -= value;
                        break;

                    case 'E':
                        waypointEastDirection += value;
                        break;

                    case 'W':
                        waypointEastDirection -= value;
                        break;

                    case 'R':
                        int rotateSteps = value / 90;

                        for (int i = 0; i < rotateSteps; i++)
                        {
                            int oldNorth = waypointNorthDirection;
                            waypointNorthDirection = -waypointEastDirection;
                            waypointEastDirection = oldNorth;
                        }
                        break;

                    case 'L':
                        rotateSteps = value / 90;

                        for (int i = 0; i < rotateSteps; i++)
                        {
                            int oldNorth = waypointNorthDirection;
                            waypointNorthDirection = waypointEastDirection;
                            waypointEastDirection = -oldNorth;
                        }
                        break;

                    case 'F':
                        northDirection += (waypointNorthDirection * value);
                        eastDirection += (waypointEastDirection * value);
                        break;

                    default:
                        Console.WriteLine("Error: " + action + " " + value);
                        break;

                }
            }
            return Math.Abs(northDirection) + Math.Abs(eastDirection);
        }

        public int CalculateDay13Task1(List<string> inputList)
        {
            int timestamp = int.Parse(inputList[0]);
            string[] busses = inputList[1].Split(',');

            int nearestBus = -1;
            int nearestBusTime = int.MaxValue;

            foreach (string bus in busses)
            {
                if (bus != "x")
                {
                    int busTimestamp = int.Parse(bus);

                    int nextDeparture = busTimestamp - (timestamp % busTimestamp);

                    if (nextDeparture < nearestBusTime)
                    {
                        nearestBusTime = nextDeparture;
                        nearestBus = busTimestamp;
                    }
                }
            }

            return nearestBus * nearestBusTime;
        }

        public long CalculateDay13Task2(List<string> inputList)
        {
            string[] busses = inputList[1].Split(',');
            int remainder = 0;

            List<int> numbers = new List<int>();
            List<int> remainders = new List<int>();

            foreach (string bus in busses)
            {
                if (bus != "x")
                {
                    numbers.Add(int.Parse(bus));
                    remainders.Add(int.Parse(bus) - remainder);
                }
                remainder++;
            }

            return Solve(numbers.ToArray(), remainders.ToArray());
        }

        public long CalculateDay14Task1(List<string> inputList)
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();
            string mask = "";
            int bitLenght = 36;

            foreach (string line in inputList)
            {
                if (line.Contains("mask"))
                {
                    mask = line.Substring(line.IndexOf('=') + 2);
                }
                else
                {
                    long index = long.Parse(line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1));
                    long value = long.Parse(line.Substring(line.IndexOf("= ") + 2));

                    //value change
                    char[] binary = ConvertNumberToBinary(value, bitLenght).ToArray();
                    string newNum = "";
                    for (int i = 0; i < bitLenght; i++)
                    {
                        if (mask[i] != 'X')
                        {
                            newNum += mask[i];
                        }
                        else
                        {
                            newNum += binary[i];
                        }
                    }
                    
                    value = ConvertLetterBinaryToDecimalLong(newNum, '1');

                    if (memory.ContainsKey(index))
                    {
                        memory[index] = value;
                    }
                    else
                    {
                        memory.Add(index, value);
                    }
                }
            }

            long answer = 0;
            foreach (KeyValuePair<long, long> value in memory)
            {
                answer += value.Value;
            }
            return answer;
        }

        public long CalculateDay14Task2(List<string> inputList)
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();
            string mask = "";
            int bitLenght = 36;

            foreach (string line in inputList)
            {
                if (line.Contains("mask"))
                {
                    mask = line.Substring(line.IndexOf('=') + 2);
                }
                else
                {
                    long index = long.Parse(line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1));
                    long value = long.Parse(line.Substring(line.IndexOf("= ") + 2));

                    //index change
                    char[] binary = ConvertNumberToBinary(index, bitLenght).ToArray();
                    string newNum = "";
                    for (int i = 0; i < bitLenght; i++)
                    {
                        if (mask[i] != '0')
                        {
                            newNum += mask[i];
                        }
                        else
                        {
                            newNum += binary[i];
                        }
                    }

                    List<long> indexes = ConvertLetterBinaryToDecimalLongList(newNum, '1', 'X', bitLenght - 1);

                    foreach (long newIndex in indexes)
                    {
                        if (memory.ContainsKey(newIndex))
                        {
                            memory[newIndex] = value;
                        }
                        else
                        {
                            memory.Add(newIndex, value);
                        }
                    }
                }
            }

            long answer = 0;
            foreach (KeyValuePair<long, long> value in memory)
            {
                answer += value.Value;
            }
            return answer;
        }

        public int CalculateDay15(List<int> inputList, int loopSize)
        {
            Dictionary<int, int> memoryGame = new Dictionary<int, int>();

            int prevGuess = inputList[0];
            for (int i = 1; i < loopSize; i++)
            {
                if (i < inputList.Count)
                {
                    memoryGame.Add(prevGuess, i - 1);
                    prevGuess = inputList[i];
                }
                else
                {
                    int newGuess = 0;
                    if (memoryGame.ContainsKey(prevGuess))
                    {
                        newGuess = (i - 1) - memoryGame[prevGuess];
                        memoryGame[prevGuess] = i - 1;
                    }
                    else
                    {
                        memoryGame.Add(prevGuess, i - 1);
                    }
                    prevGuess = newGuess;
                }
            }

            return prevGuess;
        }

        public int CalculateDay16Task1(List<string> inputList)
        {
            int errorRate = 0;

            List<ValidRanges> validRanges = new List<ValidRanges>();
            List<int> yourList = new List<int>();
            List<List<int>> nearbyTickets = new List<List<int>>();

            bool isRuleSet = true;
            bool isYourTicket = true;

            foreach (string line in inputList)
            {
                if (isRuleSet)
                {
                    if (line == "")
                    {
                        isRuleSet = false;
                    }
                    else
                    {
                        ValidRanges newRange;
                        newRange.fieldName = line.Substring(0, line.IndexOf(':'));
                        newRange.firstLowerRange = int.Parse(line.Substring(line.IndexOf(':') + 2, line.IndexOf('-') - (line.IndexOf(':') + 2)));
                        newRange.firstUpperRange = int.Parse(line.Substring(line.IndexOf('-') + 1, line.IndexOf(" or") - (line.IndexOf('-') + 1)));
                        newRange.secondLowerRange = int.Parse(line.Substring(line.IndexOf("or ") + 3, line.LastIndexOf('-') - (line.IndexOf("or ") + 3)));
                        newRange.secondUpperRange = int.Parse(line.Substring(line.LastIndexOf('-') + 1));

                        validRanges.Add(newRange);
                    }
                }
                else if (isYourTicket)
                {
                    if (line == "")
                    {
                        isYourTicket = false;
                    }
                    else if (line != "your ticket:")
                    {
                        string[] inputs = line.Split(',');
                        yourList = Array.ConvertAll(inputs, s => int.Parse(s)).ToList();
                    }

                }
                else
                {
                    if (line != "nearby tickets:")
                    {
                        string[] inputs = line.Split(',');
                        List<int> newList = Array.ConvertAll(inputs, s => int.Parse(s)).ToList();
                        nearbyTickets.Add(newList);
                    }
                }
            }

            foreach (List<int> ticket in nearbyTickets)
            {
                foreach (int field in ticket)
                {
                    bool validatedField = false;
                    foreach (ValidRanges range in validRanges)
                    {
                        if ((field >= range.firstLowerRange && field <= range.firstUpperRange) || (field >= range.secondLowerRange && field <= range.secondUpperRange))
                        {
                            validatedField = true;
                            break;
                        }
                    }

                    if (!validatedField)
                    {
                        errorRate += field;
                    }
                }
            }

            return errorRate;
        }

        public long CalculateDay16Task2(List<string> inputList)
        {
            List<ValidRanges> validRanges = new List<ValidRanges>();
            List<int> yourList = new List<int>();
            List<List<int>> nearbyTickets = new List<List<int>>();

            bool isRuleSet = true;
            bool isYourTicket = true;

            foreach (string line in inputList)
            {
                if (isRuleSet)
                {
                    if (line == "")
                    {
                        isRuleSet = false;
                    }
                    else
                    {
                        ValidRanges newRange;
                        newRange.fieldName = line.Substring(0, line.IndexOf(':'));
                        newRange.firstLowerRange = int.Parse(line.Substring(line.IndexOf(':') + 2, line.IndexOf('-') - (line.IndexOf(':') + 2)));
                        newRange.firstUpperRange = int.Parse(line.Substring(line.IndexOf('-') + 1, line.IndexOf(" or") - (line.IndexOf('-') + 1)));
                        newRange.secondLowerRange = int.Parse(line.Substring(line.IndexOf("or ") + 3, line.LastIndexOf('-') - (line.IndexOf("or ") + 3)));
                        newRange.secondUpperRange = int.Parse(line.Substring(line.LastIndexOf('-') + 1));

                        validRanges.Add(newRange);
                    }
                }
                else if (isYourTicket)
                {
                    if (line == "")
                    {
                        isYourTicket = false;
                    }
                    else if (line != "your ticket:")
                    {
                        string[] inputs = line.Split(',');
                        yourList = Array.ConvertAll(inputs, s => int.Parse(s)).ToList();
                    }

                }
                else
                {
                    if (line != "nearby tickets:")
                    {
                        string[] inputs = line.Split(',');
                        List<int> newList = Array.ConvertAll(inputs, s => int.Parse(s)).ToList();
                        nearbyTickets.Add(newList);
                    }
                }
            }

            for (int i = nearbyTickets.Count - 1; i >= 0; i--)
            {
                foreach (int field in nearbyTickets[i])
                {
                    bool validatedField = false;
                    foreach (ValidRanges range in validRanges)
                    {
                        if ((field >= range.firstLowerRange && field <= range.firstUpperRange) || (field >= range.secondLowerRange && field <= range.secondUpperRange))
                        {
                            validatedField = true;
                            break;
                        }
                    }

                    if (!validatedField)
                    {
                        nearbyTickets.RemoveAt(i);
                        break;
                    }
                }
            }

            List<int>[] fieldIndexes = new List<int>[validRanges.Count];
            for (int i = 0; i < fieldIndexes.Length; i++)
            {
                fieldIndexes[i] = new List<int>();
            }

            for (int j = 0; j < nearbyTickets[0].Count; j++)
            {
                for (int r = 0; r < validRanges.Count; r++)
                {
                    bool outOfRange = false;
                    for (int i = 0; i < nearbyTickets.Count; i++)
                    {
                        if (nearbyTickets[i][j] < validRanges[r].firstLowerRange ||
                            (nearbyTickets[i][j] > validRanges[r].firstUpperRange && nearbyTickets[i][j] < validRanges[r].secondLowerRange) ||
                            nearbyTickets[i][j] > validRanges[r].secondUpperRange)
                        {
                            outOfRange = true;
                            break;
                        }
                    }

                    if (!outOfRange)
                    {
                        fieldIndexes[r].Add(j);
                    }
                }
            }

            int[] finalFieldIndexes = new int[validRanges.Count];
            int currentIndexCount = 0;
            while (currentIndexCount < validRanges.Count)
            {
                for (int i = fieldIndexes.Length - 1; i >= 0; i--)
                {
                    if (fieldIndexes[i].Count == 1)
                    {
                        finalFieldIndexes[i] = fieldIndexes[i][0];
                        for (int j = 0; j < fieldIndexes.Length; j++)
                        {
                            fieldIndexes[j].Remove(finalFieldIndexes[i]);
                        }
                        currentIndexCount++;
                    }
                }
            }

            long multiplyOfDepartureTickets = 1;
            int ind = 0;
            for (int i = 0; i < finalFieldIndexes.Length; i++)
            {
                if (validRanges[i].fieldName.Contains("departure"))
                {
                    multiplyOfDepartureTickets *= yourList[finalFieldIndexes[i]];
                    ind++;
                }
            }

            return multiplyOfDepartureTickets;
        }

        public int CalculateDay17Task1(List<string> inputList)
        {
            int cycleCount = 6;
            char active = '#';
            int activeCubesCount = 0;

            char[,,] input = new char[1, inputList.Count, inputList[0].Length];
            for (int i = 0; i < inputList.Count; i++)
            {
                for (int j = 0; j < inputList[i].Length; j++)
                {
                    input[0, i, j] = inputList[i][j];
                }
            }

            for (int i = 0; i < cycleCount; i++)
            {
                char[,,] newInput = new char[input.GetLength(0) + 2, input.GetLength(1) + 2, input.GetLength(2) + 2];
                activeCubesCount = 0;

                for (int x = 0; x < newInput.GetLength(0); x++)
                {
                    for (int y = 0; y < newInput.GetLength(1); y++)
                    {
                        for (int z = 0; z < newInput.GetLength(2); z++)
                        {
                            int activeNeighbors = GetActiveCubeCount(input, x - 1, y - 1, z - 1, active);

                            if (x - 1 >= 0 && y - 1 >= 0 && z - 1 >= 0 &&
                                x - 1 < input.GetLength(0) && y - 1 < input.GetLength(1) && z - 1 < input.GetLength(2) && 
                                input[x - 1, y - 1, z - 1] == active)
                            {
                                if (activeNeighbors == 2 || activeNeighbors == 3)
                                {
                                    newInput[x, y, z] = active;
                                    activeCubesCount++;
                                }
                            }
                            else if (activeNeighbors == 3)
                            {
                                newInput[x, y, z] = active;
                                activeCubesCount++;
                            }
                        }
                    }
                }
                input = newInput;
            }
            return activeCubesCount;
        }

        public int CalculateDay17Task2(List<string> inputList)
        {
            int cycleCount = 6;
            char active = '#';
            int activeHyperCubesCount = 0;

            char[,,,] input = new char[1, 1, inputList.Count, inputList[0].Length];
            for (int i = 0; i < inputList.Count; i++)
            {
                for (int j = 0; j < inputList[i].Length; j++)
                {
                    input[0, 0, i, j] = inputList[i][j];
                }
            }

            for (int i = 0; i < cycleCount; i++)
            {
                char[,,,] newInput = new char[input.GetLength(0) + 2, input.GetLength(1) + 2, input.GetLength(2) + 2, input.GetLength(3) + 2];
                activeHyperCubesCount = 0;

                for (int x = 0; x < newInput.GetLength(0); x++)
                {
                    for (int y = 0; y < newInput.GetLength(1); y++)
                    {
                        for (int z = 0; z < newInput.GetLength(2); z++)
                        {
                            for (int w = 0; w < newInput.GetLength(3); w++)
                            {
                                int activeNeighbors = GetActiveHyperCubeCount(input, x - 1, y - 1, z - 1, w - 1, active);

                                if (x - 1 >= 0 && y - 1 >= 0 && z - 1 >= 0 && w - 1 >= 0 &&
                                    x - 1 < input.GetLength(0) && y - 1 < input.GetLength(1) && z - 1 < input.GetLength(2) && w - 1 < input.GetLength(3) &&
                                    input[x - 1, y - 1, z - 1, w - 1] == active)
                                {
                                    if (activeNeighbors == 2 || activeNeighbors == 3)
                                    {
                                        newInput[x, y, z, w] = active;
                                        activeHyperCubesCount++;
                                    }
                                }
                                else if (activeNeighbors == 3)
                                {
                                    newInput[x, y, z, w] = active;
                                    activeHyperCubesCount++;
                                }
                            }
                        }
                    }
                }
                input = newInput;
            }
            return activeHyperCubesCount;
        }

        public long CalculateDay18Task1(List<string> inputList)
        {
            long answerSum = 0;

            foreach (string line in inputList)
            {
                answerSum += GetOperationValue(line);
            }
            return answerSum;
        }

        public long CalculateDay18Task2(List<string> inputList)
        {
            long answerSum = 0;

            foreach (string line in inputList)
            {
                List<string> elements = new List<string>();
                foreach (char item in line)
                {
                    if (item != ' ')
                    {
                        elements.Add(item.ToString());
                    }
                }

                answerSum += GetAdvancedOperationValue(elements);
            }
            return answerSum;
        }

        public int CalculateDay19Task1(List<string> inputList)
        {
            List<string> messages;
            Dictionary<int, string> rules;
            ReadData(inputList, out messages, out rules);

            int matchingMessages = 0;

            foreach (string message in messages)
            {
                int index = MatchTheRule(ref rules, message, 0, 0);
                matchingMessages += message.Count() == index ? 1 : 0;
            }

            return matchingMessages;
        }

        public int CalculateDay19Task2(List<string> inputList)
        {
            List<string> messages;
            Dictionary<int, string> rules;
            ReadData(inputList, out messages, out rules);

            List<string> rule42 = GenerateRulePresets(ref rules, 42);
            List<string> rule31 = GenerateRulePresets(ref rules, 31);

            int matchingMessages = 0;

            foreach (string message in messages)
            {
                if (message.Length % rule42[0].Length == 0)
                {
                    int count = message.Length / rule42[0].Length;

                    int passed42Segments = 0;
                    int passed31Segments = 0;
                    for (int i = 0; i < count; i++)
                    {
                        if (rule42.Contains(message.Substring(i * rule42[0].Length, rule42[0].Length)))
                        {
                            passed42Segments = passed31Segments == 0 ? passed42Segments + 1 : 0;
                        }
                        else if (rule31.Contains(message.Substring(i * rule31[0].Length, rule31[0].Length)))
                        {
                            passed31Segments++;
                        }
                    }
                    if (passed42Segments > passed31Segments && passed31Segments > 0)
                    {
                        matchingMessages++;
                    }
                }  
            }

            return matchingMessages;
        }

        public long CalculateDay20Task1(List<string> inputList)
        {
            List<PictureTile> tileList = new List<PictureTile>();

            PictureTile newTile;
            int tileId = 0;
            List<char[]> tileData = default;
            foreach (string input in inputList)
            {
                if (input.Contains("Tile"))
                {
                    tileId = int.Parse(input.Substring(input.IndexOf(' '), input.IndexOf(':') - input.IndexOf(' ')));
                    tileData = new List<char[]>();
                }
                else if (input == "")
                {
                    newTile = new PictureTile(tileId, tileData.ToArray());
                    tileList.Add(newTile);
                }
                else
                {
                    tileData.Add(input.ToCharArray());
                }
            }
            newTile = new PictureTile(tileId, tileData.ToArray());
            tileList.Add(newTile);

            long answer = 1;
            for (int i = 0; i < tileList.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < tileList.Count; j++)
                {
                    if (i != j && tileList[j].HasMachingBorder(tileList[i].BorderArray, tileList[i].Id))
                    {
                        count++;
                    }
                }

                if (count == 2)
                {
                    answer *= tileList[i].Id;
                }
            }
            return answer;
        }

        public int CalculateDay20Task2(List<string> inputList)
        {
            Dictionary<int, PictureTile> tileList = new Dictionary<int, PictureTile>();

            PictureTile newTile;
            int tileId = 0;
            List<char[]> tileData = default;
            foreach (string input in inputList)
            {
                if (input.Contains("Tile"))
                {
                    tileId = int.Parse(input.Substring(input.IndexOf(' '), input.IndexOf(':') - input.IndexOf(' ')));
                    tileData = new List<char[]>();
                }
                else if (input == "")
                {
                    newTile = new PictureTile(tileId, tileData.ToArray());
                    tileList.Add(tileId, newTile);
                }
                else
                {
                    tileData.Add(input.ToCharArray());
                }
            }
            newTile = new PictureTile(tileId, tileData.ToArray());
            tileList.Add(tileId, newTile);

            for (int i = 0; i < tileList.Count; i++)
            {
                for (int j = 0; j < tileList.Count; j++)
                {
                    if (i != j)
                    {
                        tileList.ElementAt(j).Value.HasMachingBorder(tileList.ElementAt(i).Value.BorderArray, tileList.ElementAt(i).Value.Id);
                    }
                }
            }

            int lenght = (int)Math.Sqrt(tileList.Count);
            int pictureSize = lenght * (tileList.ElementAt(0).Value.BorderArray[0].Length - 2);
            char[,] picture = new char[pictureSize, pictureSize];

            Dictionary<int, PictureTile> tile2List = new Dictionary<int, PictureTile>();
            Dictionary<int, PictureTile> tile3List = new Dictionary<int, PictureTile>();
            Dictionary<int, PictureTile> tile4List = new Dictionary<int, PictureTile>();
            for (int i = 0; i < tileList.Count; i++)
            {
                int count = tileList.ElementAt(i).Value.BorderingTiles.Count;
                if (count == 4)
                {
                    tile4List.Add(tileList.ElementAt(i).Key, tileList.ElementAt(i).Value);
                }
                else if (count == 3)
                {
                    tile3List.Add(tileList.ElementAt(i).Key, tileList.ElementAt(i).Value);
                }
                else
                {
                    tile2List.Add(tileList.ElementAt(i).Key, tileList.ElementAt(i).Value);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < tile3List.Count; j++)
                {

                }
            }
            

                return -1;// answer;
        }

        public int CalculateDay21Task1(List<string> inputList)
        {
            List<List<string>> ingrediants = new List<List<string>>();
            List<List<string>> alergens = new List<List<string>>();
            HashSet<string> uniqueAlergens = new HashSet<string>();

            foreach (string input in inputList)
            {
                string[] inputSlice = input.Split('(');
                ingrediants.Add(inputSlice[0].Split(' ').ToList());
                ingrediants[ingrediants.Count - 1].RemoveAt(ingrediants[ingrediants.Count - 1].Count - 1);
                string alergensString = inputSlice[1].Substring(9, inputSlice[1].Length - 10);
                alergensString = alergensString.Replace(",", null);
                alergens.Add(alergensString.Split(' ').ToList());
            }

            foreach (List<string> itemList in alergens)
            {
                foreach (string item in itemList)
                {
                    if (!uniqueAlergens.Contains(item))
                    {
                        uniqueAlergens.Add(item);
                    }
                }
            }

            Dictionary<string, List<string>> potentialProducts = new Dictionary<string, List<string>>();
            foreach (string alergen in uniqueAlergens)
            {
                int ingrediantsAmount = 0;
                Dictionary<string, int> potentialAlergens = new Dictionary<string, int>();
                for (int i = 0; i < alergens.Count; i++)
                {
                    bool containAlergen = false;
                    for (int j = 0; j < alergens[i].Count; j++)
                    {
                        if (alergen == alergens[i][j])
                        {
                            containAlergen = true;
                        }
                    }

                    if (containAlergen)
                    {
                        ingrediantsAmount++;
                        for (int j = 0; j < ingrediants[i].Count; j++)
                        {
                            if (potentialAlergens.ContainsKey(ingrediants[i][j]))
                            {
                                potentialAlergens[ingrediants[i][j]]++;
                            }
                            else
                            {
                                potentialAlergens.Add(ingrediants[i][j], 1);
                            }
                        }
                    }
                }
                List<string> products = new List<string>();
                foreach (KeyValuePair<string, int> product in potentialAlergens)
                {
                    if (ingrediantsAmount == product.Value)
                    {
                        products.Add(product.Key);
                    }
                }
                potentialProducts.Add(alergen, products);
            }

            List<string> alergicProducts = new List<string>();
            int loopAmount = potentialProducts.Count;
            for (int i = 0; i < loopAmount; i++)
            {
                for (int j = 0; j < potentialProducts.Count; j++)
                {
                    if (potentialProducts.ElementAt(j).Value.Count == 1)
                    {
                        string newProduct = potentialProducts.ElementAt(j).Value[0];
                        alergicProducts.Add(newProduct);

                        for (int l = 0; l < potentialProducts.Count; l++)
                        {
                            potentialProducts.ElementAt(l).Value.Remove(newProduct);
                        }
                    }
                }
            }

            int answer = 0;
            foreach (List<string> itemList in ingrediants)
            {
                foreach (string item in itemList)
                {
                    answer += !alergicProducts.Contains(item) ? 1 : 0;
                }
            }
            return answer;
        }

        public string CalculateDay21Task2(List<string> inputList)
        {
            List<List<string>> ingrediants = new List<List<string>>();
            List<List<string>> alergens = new List<List<string>>();
            List<string> uniqueAlergens = new List<string>();

            foreach (string input in inputList)
            {
                string[] inputSlice = input.Split('(');
                ingrediants.Add(inputSlice[0].Split(' ').ToList());
                ingrediants[ingrediants.Count - 1].RemoveAt(ingrediants[ingrediants.Count - 1].Count - 1);
                string alergensString = inputSlice[1].Substring(9, inputSlice[1].Length - 10);
                alergensString = alergensString.Replace(",", null);
                alergens.Add(alergensString.Split(' ').ToList());
            }

            foreach (List<string> itemList in alergens)
            {
                foreach (string item in itemList)
                {
                    if (!uniqueAlergens.Contains(item))
                    {
                        uniqueAlergens.Add(item);
                    }
                }
            }

            Dictionary<string, List<string>> potentialProducts = new Dictionary<string, List<string>>();
            foreach (string alergen in uniqueAlergens)
            {
                int ingrediantsAmount = 0;
                Dictionary<string, int> potentialAlergens = new Dictionary<string, int>();
                for (int i = 0; i < alergens.Count; i++)
                {
                    bool containAlergen = false;
                    for (int j = 0; j < alergens[i].Count; j++)
                    {
                        if (alergen == alergens[i][j])
                        {
                            containAlergen = true;
                        }
                    }

                    if (containAlergen)
                    {
                        ingrediantsAmount++;
                        for (int j = 0; j < ingrediants[i].Count; j++)
                        {
                            if (potentialAlergens.ContainsKey(ingrediants[i][j]))
                            {
                                potentialAlergens[ingrediants[i][j]]++;
                            }
                            else
                            {
                                potentialAlergens.Add(ingrediants[i][j], 1);
                            }
                        }
                    }
                }
                List<string> products = new List<string>();
                foreach (KeyValuePair<string, int> product in potentialAlergens)
                {
                    if (ingrediantsAmount == product.Value)
                    {
                        products.Add(product.Key);
                    }
                }
                potentialProducts.Add(alergen, products);
            }

            Dictionary<string, string> alergicProducts = new Dictionary<string, string>();
            int loopAmount = potentialProducts.Count;
            for (int i = 0; i < loopAmount; i++)
            {
                for (int j = 0; j < potentialProducts.Count; j++)
                {
                    if (potentialProducts.ElementAt(j).Value.Count == 1)
                    {
                        string newProduct = potentialProducts.ElementAt(j).Value[0];
                        alergicProducts.Add(potentialProducts.ElementAt(j).Key, newProduct);

                        for (int l = 0; l < potentialProducts.Count; l++)
                        {
                            potentialProducts.ElementAt(l).Value.Remove(newProduct);
                        }
                    }
                }
            }

            uniqueAlergens.Sort();

            string answer = "";
            foreach (string alergent in uniqueAlergens)
            {
                answer += (alergicProducts[alergent] + ",");
            }

            return answer.Remove(answer.Length - 1);
        }


        //---------------------------------------------------------------------------------------------------------------------

        private void ReadData(List<string> inputList, out List<string> messages, out Dictionary<int, string> rules)
        {
            messages = new List<string>();
            rules = new Dictionary<int, string>();
            bool separatorReached = false;
            foreach (string item in inputList)
            {
                if (item == "")
                {
                    separatorReached = true;
                }
                else
                {
                    if (!separatorReached)
                    {
                        int index = int.Parse(item.Substring(0, item.IndexOf(':')));
                        string values = item.Substring(item.IndexOf(':') + 2);

                        rules.Add(index, values);
                    }
                    else
                    {
                        messages.Add(item);
                    }
                }
            }
        }

        private List<string> GenerateRulePresets(ref Dictionary<int, string> rules, int ruleIndex)
        {
            if (rules[ruleIndex].Contains('"'))
            {
                List<string> newList = new List<string>();
                newList.Add(rules[ruleIndex].Substring(1,1));
                return newList;
            }
            else
            {
                string[] ruleset = rules[ruleIndex].Split(" | ");
                List<string> finalList = new List<string>();

                foreach (string item in ruleset)
                {
                    int[] newRuleIdexes = Array.ConvertAll(item.Split(' '), s => int.Parse(s));
                    List<string> newList = new List<string>();


                    foreach (int index in newRuleIdexes)
                    {
                        List<string> newAditionList = GenerateRulePresets(ref rules, index);

                        if (newList.Count == 0)
                        {
                            newList = newAditionList;
                        }
                        else
                        {
                            List<string> combinedList = new List<string>();
                            foreach (string oldValue in newList)
                            {
                                foreach (string newValue in newAditionList)
                                {
                                    combinedList.Add(oldValue + newValue);
                                }
                            }
                            newList = combinedList;
                        }
                    }
                    finalList.AddRange(newList);
                }
                return finalList;
            }
        }

        private int MatchTheRule(ref Dictionary<int, string> rules, string message, int ruleIndex, int messageIndex)
        {
            if (rules[ruleIndex].Contains('"'))
            {
                char letter = rules[ruleIndex].Substring(1)[0];
                if (message.Length > messageIndex)
                {
                    return message[messageIndex] == letter ? ++messageIndex : -1;
                }
                return -1;
            }
            else
            {
                string[] ruleset = rules[ruleIndex].Split(" | ");

                int isMatch = -1;
                foreach (string item in ruleset)
                {
                    int[] newRuleIdexes = Array.ConvertAll(item.Split(' '), s => int.Parse(s));

                    int correctRules = 0;
                    int newMessageIndex = messageIndex;

                    foreach (int index in newRuleIdexes)
                    {
                        if (newMessageIndex >= 0)
                        {
                            newMessageIndex = MatchTheRule(ref rules, message, index, newMessageIndex);
                            correctRules += newMessageIndex >= 0 ? 1 : 0;


                        }
                        isMatch = correctRules == newRuleIdexes.Length ? newMessageIndex : isMatch;
                    }
                }
                return isMatch;
            }
        }

        private long GetOperationValue(string line)
        {
            long value = 0;

            long operand = 0;
            char operation = ' ';

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '(')
                {
                    string subOperation = "";
                    int entryAmount = 1;
                    for (int j = i + 1; j < line.Length; j++)
                    {
                        if (line[j] == '(')
                        {
                            entryAmount++;
                        }
                        else if (line[j] == ')')
                        {
                            entryAmount--;
                            if (entryAmount == 0)
                            {
                                i = j;
                                break;
                            }
                        }
                        subOperation += line[j];
                    }
                    operand = GetOperationValue(subOperation);
                    value = CalculateOperation(value, operation, operand);
                }
                else if (line[i] == '+' || line[i] == '*')
                {
                    operation = line[i];
                }
                else if (line[i] != ' ' && line[i] != ')')
                {
                    operand = int.Parse(line[i].ToString());
                    value = CalculateOperation(value, operation, operand);
                }
            }
            return value;
        }

        private long GetAdvancedOperationValue(List<string> elements)
        {
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                if (elements[i] == ")")
                {
                    List<string> subOperation = new List<string>();
                    int entryAmount = 1;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (elements[j] == ")")
                        {
                            entryAmount++;
                        }
                        else if (elements[j] == "(")
                        {
                            entryAmount--;
                            if (entryAmount == 0)
                            {
                                subOperation.Reverse();
                                elements[j] = GetAdvancedOperationValue(subOperation).ToString();
                                elements.RemoveAt(j + 1);
                                i = j;
                                break;
                            }
                        }
                        subOperation.Add(elements[j]);
                        elements.RemoveAt(j);
                    }
                }
            }

            for (int i = elements.Count - 1; i >= 0; i--)
            {
                if (elements[i] == "+")
                {
                    elements[i - 1] = CalculateOperation(long.Parse(elements[i - 1]), elements[i][0], long.Parse(elements[i + 1])).ToString();
                    elements.RemoveAt(i + 1);
                    elements.RemoveAt(i);
                }
            }

            long value = 1;
            foreach (string item in elements)
            {
                if (item != "*")
                {
                    value *= long.Parse(item);
                }
            }
            return value;
        }

        private long CalculateOperation(long leftOperand, char operation, long rightOperand)
        {
            if (operation == '*')
            {
                return leftOperand * rightOperand;
            }
            else
            {
                return leftOperand + rightOperand;
            }
        }

        private int GetActiveCubeCount(char[,,] charArray, int xCurrent, int yCurrent, int zCurrent, char activeType)
        {
            int cubeCount = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        if (!(x == 0 && y == 0 && z == 0))
                        {
                            if (xCurrent + x >= 0 && yCurrent + y >= 0 && zCurrent + z >= 0 &&
                            xCurrent + x < charArray.GetLength(0) && yCurrent + y < charArray.GetLength(1) && zCurrent + z < charArray.GetLength(2) &&
                            charArray[xCurrent + x, yCurrent + y, zCurrent + z] == activeType)
                            {
                                cubeCount++;
                            }
                        }
                    }
                }
            }
            return cubeCount;
        }

        private int GetActiveHyperCubeCount(char[,,,] charArray, int xCurrent, int yCurrent, int zCurrent, int wCurrent, char activeType)
        {
            int hyperCubeCount = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        for (int w = -1; w <= 1; w++)
                        {
                            if (!(x == 0 && y == 0 && z == 0 && w == 0))
                            {
                                if (xCurrent + x >= 0 && yCurrent + y >= 0 && zCurrent + z >= 0 && wCurrent + w >= 0 &&
                                xCurrent + x < charArray.GetLength(0) && yCurrent + y < charArray.GetLength(1) && zCurrent + z < charArray.GetLength(2) && wCurrent + w < charArray.GetLength(3) &&
                                charArray[xCurrent + x, yCurrent + y, zCurrent + z, wCurrent + w] == activeType)
                                {
                                    hyperCubeCount++;
                                }
                            }
                        }
                    }
                }
            }
            return hyperCubeCount;
        }

        private struct ValidRanges
        {
            public string fieldName;
            public int firstLowerRange;
            public int firstUpperRange;
            public int secondLowerRange;
            public int secondUpperRange;
        }

        private string ConvertNumberToBinary(long number, int length)
        {
            string binaryNumber = "";

            for (int i = 0; i < length; i++)
            {
                int bit = (int)(number / Math.Pow(2, length - i - 1));
                bit = bit >= 1 ? 1 : 0;
                number -= (int)(bit * Math.Pow(2, length - i - 1));

                binaryNumber += bit;
            }

            return binaryNumber;
        }

        private enum Direction
        {
            East,
            South,
            West,
            North
        }

        private long Solve(int[] n, int[] a)
        {
            long prod = 1;//n.Aggregate(1, (i, j) => i * j);

            for (int i = 0; i < n.Length; i++)
            {
                prod *= n[i];
            }

            long p;
            long sm = 0;
            for (int i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }

        private int GetSeatsCount(char[][] stringArray, int y, int x, char seatType)
        {
            int seatCount = 0;

            if (x - 1 >= 0)
            {
                if (y - 1 >= 0 && stringArray[y - 1][x - 1] == seatType)
                {
                    seatCount++;
                }
                if (stringArray[y][x - 1] == seatType)
                {
                    seatCount++;
                }
                if (y + 1 < stringArray.Length && stringArray[y + 1][x - 1] == seatType)
                {
                    seatCount++;
                }
            }

            if (x + 1 < stringArray[y].Length)
            {
                if (y - 1 >= 0 && stringArray[y - 1][x + 1] == seatType)
                {
                    seatCount++;
                }
                if (stringArray[y][x + 1] == seatType)
                {
                    seatCount++;
                }
                if (y + 1 < stringArray.Length && stringArray[y + 1][x + 1] == seatType)
                {
                    seatCount++;
                }
            }

            if (y - 1 >= 0 && stringArray[y - 1][x] == seatType)
            {
                seatCount++;
            }
            if (y + 1 < stringArray.Length && stringArray[y + 1][x] == seatType)
            {
                seatCount++;
            }
            return seatCount;
        }

        private int GetVisionSeatsCount(char[][] stringArray, int y, int x, char seatType)
        {
            int seatCount = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        int xAdjusted = x + i, yAdjusted = y + j;

                        while (yAdjusted >= 0 && yAdjusted < stringArray.Length && xAdjusted >= 0 && xAdjusted < stringArray[yAdjusted].Length)
                        {
                            if (stringArray[yAdjusted][xAdjusted] == seatType || stringArray[yAdjusted][xAdjusted] == 'L')
                            {
                                if (stringArray[yAdjusted][xAdjusted] == seatType)
                                {
                                    seatCount++;
                                }
                                break;
                            }
                            xAdjusted += i;
                            yAdjusted += j;
                        }
                    }
                }
            }
            return seatCount;
        }

        private bool CheckForSum(List<int> inputList, int sum)
        {
            int[] input = inputList.ToArray();
            QuickSort(input, 0, input.Length - 1);

            int sumOfInput = 0;
            for (int j = input.Length - 1; j >= 0; j--)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    sumOfInput = input[i] + input[j];
                    if (sumOfInput == sum)
                    {
                        return true;
                    }
                    else if (sumOfInput > sum)
                    {
                        break;
                    }
                }
            }

            return false;
        }

        private int GetBagAmountInChild(ColorBagNode bag)
        {
            int bagAmount = 0;

            foreach(Tuple<int, ColorBagNode> node in bag.Childs)
            {
                bagAmount += node.Item1 + node.Item1 * GetBagAmountInChild(node.Item2);
            }

            return bagAmount;
        }

        private int RunPSPConsoleComands(List<string> inputList, out bool enteredLoop)
        {
            int accumulativeValue = 0;
            string visitedLine = "Visited";
            enteredLoop = true;

            int currentLineIndex = 0;
            while (currentLineIndex < inputList.Count && inputList[currentLineIndex] != visitedLine)
            {
                string command = inputList[currentLineIndex].Substring(0, inputList[currentLineIndex].IndexOf(' '));
                int modifier = int.Parse(inputList[currentLineIndex].Substring(inputList[currentLineIndex].IndexOf(' ') + 1));

                inputList[currentLineIndex] = visitedLine;

                switch (command)
                {
                    case "acc":
                        accumulativeValue += modifier;
                        currentLineIndex++;
                        break;

                    case "jmp":
                        currentLineIndex += modifier;
                        break;

                    case "nop":
                        currentLineIndex++;
                        break;
                }

                enteredLoop = currentLineIndex >= inputList.Count ? false : true;
            }

            return accumulativeValue;
        }

        private Dictionary<string, ColorBagNode> CreateBagNodeTreeDictionary(List<string> inputList)
        {
            Dictionary<string, ColorBagNode> addedBags = new Dictionary<string, ColorBagNode>();

            foreach (string line in inputList)
            {
                string currentBag = line.Substring(0, line.IndexOf("bags") - 1);
                string[] containedBags = line.Substring(line.IndexOf("contain ") + 8).Split(", ");

                if (!addedBags.ContainsKey(currentBag))
                {
                    addedBags.Add(currentBag, new ColorBagNode(currentBag));
                }

                foreach (string innerBag in containedBags)
                {
                    int amount = 0;
                    if (int.TryParse(innerBag.Substring(0, innerBag.IndexOf(' ')), out amount))
                    {
                        string bagType = innerBag.Substring(innerBag.IndexOf(' ') + 1, innerBag.IndexOf(" bag") - (innerBag.IndexOf(' ') + 1));

                        if (!addedBags.ContainsKey(bagType))
                        {
                            addedBags.Add(bagType, new ColorBagNode(bagType));
                        }
                        addedBags[bagType].AddParent(amount, addedBags[currentBag]);
                        addedBags[currentBag].AddChild(amount, addedBags[bagType]);
                    }
                }
            }

            return addedBags;
        }

        private int CountMaxValues(Dictionary<char, int> answers, int requiredAmount)
        {
            int countSum = 0;

            foreach (KeyValuePair<char, int> value in answers)
            {
                if (value.Value == requiredAmount)
                {
                    countSum++;
                }
            }

            return countSum;
        }

        private int CalculateValidPassports(List<string> inputList, Func<List<string>, List<string>> GetPassportInformation)
        {
            int validPassports = 0;
            List<string> passportInformation = GetPassportInformation(inputList);

            string[] expectedFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            int requiredAmount = expectedFields.Length;

            foreach (string passport in passportInformation)
            {
                int fieldsPresent = 0;
                foreach (string field in expectedFields)
                {
                    if (passport.Contains(field))
                    {
                        fieldsPresent++;
                    }
                }

                validPassports += fieldsPresent == requiredAmount ? 1 : 0;
            }

            return validPassports;
        }

        private bool IsValidPassportField(string passportField)
        {
            string field = passportField.Substring(0, passportField.IndexOf(':'));
            string value = passportField.Substring(passportField.IndexOf(':') + 1);

            int intValue = 0;
            string[] eyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            switch (field)
            {
                case "byr":
                    intValue = 0;
                    int.TryParse(value, out intValue);
                    if (intValue >= 1920 && intValue <= 2002)
                    {
                        return true;
                    }
                    break;

                case "iyr":
                    intValue = 0;
                    int.TryParse(value, out intValue);
                    if (intValue >= 2010 && intValue <= 2020)
                    {
                        return true;
                    }
                    break;

                case "eyr":
                    intValue = 0;
                    int.TryParse(value, out intValue);
                    if (intValue >= 2020 && intValue <= 2030)
                    {
                        return true;
                    }
                    break;

                case "hgt":
                    if (value.Contains("cm"))
                    {
                        intValue = 0;
                        int.TryParse(value.Substring(0, value.IndexOf('c')), out intValue);
                        if (intValue >= 150 && intValue <= 193)
                        {
                            return true;
                        }
                    }
                    else if (value.Contains("in"))
                    {
                        intValue = 0;
                        int.TryParse(value.Substring(0, value.IndexOf('i')), out intValue);
                        if (intValue >= 59 && intValue <= 76)
                        {
                            return true;
                        }
                    }
                    break;

                case "hcl":
                    string colorExpression = @"[\#][0-9a-f]{6}";
                    if (Regex.IsMatch(value, colorExpression))
                    {
                        return true;
                    }
                    break;

                case "ecl":
                    foreach (string color in eyeColors)
                    {
                        if (color == value)
                        {
                            return true;
                        }
                    }
                    break;

                case "pid":
                    string pidExpression = @"[0-9]{9}";
                    if (Regex.IsMatch(value, pidExpression) && value.Length == 9)
                    {
                        Console.WriteLine(value);
                        return true;
                    }
                    break;

                default:
                    break;
            }

            return false;
        }

        private void SeperatePasswordPolicy(string passwordPolicy, out int minBound, out int maxBound, out char targetChar, out string password)
        {
            minBound = int.Parse(passwordPolicy.Substring(0, passwordPolicy.IndexOf('-')));
            maxBound = (passwordPolicy.IndexOf('-') + 1);
            maxBound = int.Parse(passwordPolicy.Substring(maxBound, passwordPolicy.IndexOf(' ') - maxBound));
            targetChar = passwordPolicy.Substring(passwordPolicy.IndexOf(' ') + 1, 1)[0];
            password = passwordPolicy.Substring(passwordPolicy.IndexOf(":") + 2);
        }

        private void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);

                QuickSort(array, low, partitionIndex - 1);
                QuickSort(array, partitionIndex + 1, high);
            }
        }

        private int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];

            int lowIndex = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    lowIndex++;

                    int temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            return lowIndex + 1;
        }

        private int CalculateLoopingDescent(List<string> inputList, int downSpeed, int rightSpeed)
        {
            int treesInThePath = 0;
            int colCount = inputList[0].Length;
            int colPosition = 0;
            char tree = '#';

            for (int i = downSpeed; i < inputList.Count; i += downSpeed)
            {
                colPosition += rightSpeed;
                colPosition = colPosition >= colCount ? colPosition - colCount : colPosition;

                treesInThePath += inputList[i][colPosition] == tree ? 1 : 0;
            }

            return treesInThePath;
        }
        
        private List<string> GetPassportInformation(List<string> inputList)
        {
            List<string> passportInformation = new List<string>();
            string containedFields = "";

            foreach (string line in inputList)
            {
                if (line == "")
                {
                    passportInformation.Add(containedFields);
                    containedFields = "";
                }
                else
                {
                    string[] values = line.Split(' ');
                    foreach (string value in values)
                    {
                        containedFields += (value.Substring(0, value.IndexOf(':')) + " ");
                    }
                }
            }
            passportInformation.Add(containedFields);
            return passportInformation;
        }

        private List<string> GetValidatedPassportInformation(List<string> inputList)
        {
            List<string> passportInformation = new List<string>();
            string containedFields = "";

            foreach (string line in inputList)
            {
                if (line == "")
                {
                    passportInformation.Add(containedFields);
                    containedFields = "";
                }
                else
                {
                    string[] values = line.Split(' ');
                    foreach (string value in values)
                    {
                        if (IsValidPassportField(value))
                        {
                            containedFields += (value.Substring(0, value.IndexOf(':')) + " ");
                        }
                    }
                }
            }
            passportInformation.Add(containedFields);
            return passportInformation;
        }

        private int ConvertLetterBinaryToDecimal(string binaryNumber, char oneValue)
        {
            int number = 0;

            for (int i = binaryNumber.Length - 1; i >= 0; i--)
            {
                if (binaryNumber[i] == oneValue)
                {
                    number += (int)Math.Pow(2, (binaryNumber.Length - i - 1));
                }
            }

            return number;
        }

        private long ConvertLetterBinaryToDecimalLong(string binaryNumber, char oneValue)
        {
            long number = 0;

            for (int i = binaryNumber.Length - 1; i >= 0; i--)
            {
                if (binaryNumber[i] == oneValue)
                {
                    number += (long)Math.Pow(2, (binaryNumber.Length - i - 1));
                }
            }

            return number;
        }

        private List<long> ConvertLetterBinaryToDecimalLongList(string binaryNumber, char oneValue, char wildValue, int index)
        {
            List<long> newList = new List<long>();
            List<long> prevList = new List<long>();
            if (index == 0)
            {
                prevList.Add(0);
            }
            else
            {
                prevList = ConvertLetterBinaryToDecimalLongList(binaryNumber, oneValue, wildValue, index - 1);
            }

            foreach (long element in prevList)
            {
                if (binaryNumber[index] == oneValue)
                {
                    newList.Add(element + (long)Math.Pow(2, (binaryNumber.Length - index - 1)));
                }
                else if (binaryNumber[index] == wildValue) 
                {
                    newList.Add(element + (long)Math.Pow(2, (binaryNumber.Length - index - 1)));
                    newList.Add(element);
                }
                else
                {
                    newList.Add(element);
                }
            }

            return newList;
        }
    }
}