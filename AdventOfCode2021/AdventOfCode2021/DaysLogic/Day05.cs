using System;
using System.Collections.Generic;

namespace AdventOfCode2021.DaysLogic
{
    public class Day05 : Template<string>
    {
        public override string Part1(List<string> input)
        {
            int overlapCount = 0;

            int[,] gridMap = new int[0,0];

            foreach (string line in input)
            {
                GetCoordinates(line, out int x1, out int x2, out int y1, out int y2);
                gridMap = TryToExpandArray(gridMap, x1, x2, y1, y2);
                overlapCount = CreateVerticalAndHorizontalLines(overlapCount, gridMap, x1, x2, y1, y2);
            }

            return overlapCount.ToString();
        }

        public override string Part2(List<string> input)
        {
            int overlapCount = 0;

            int[,] gridMap = new int[0, 0];

            foreach (string line in input)
            {
                GetCoordinates(line, out int x1, out int x2, out int y1, out int y2);
                gridMap = TryToExpandArray(gridMap, x1, x2, y1, y2);

                if (x1 == x2 || y1 == y2)
                {
                    overlapCount = CreateVerticalAndHorizontalLines(overlapCount, gridMap, x1, x2, y1, y2);
                }
                else
                {
                    for (int i = 0; i <= Math.Abs(y1 - y2); i++)
                    {
                        int newX = x1 > x2 ? x1 - i : x1 + i;
                        int newY = y1 > y2 ? y1 - i : y1 + i;

                        if (++gridMap[newX, newY] == 2) overlapCount++;
                    }
                }
            }

            return overlapCount.ToString();
        }

        private void GetCoordinates(string line, out int x1, out int x2, out int y1, out int y2)
        {
            string[] coordinates = line.Split(" -> ");

            string[] startCoords = coordinates[0].Split(',');
            x1 = int.Parse(startCoords[0]);
            y1 = int.Parse(startCoords[1]);

            string[] endCoords = coordinates[1].Split(',');
            x2 = int.Parse(endCoords[0]);
            y2 = int.Parse(endCoords[1]);
        }

        private int[,] TryToExpandArray(int[,] gridMap, int x1, int x2, int y1, int y2)
        {
            if (Math.Max(x1, x2) + 1 > gridMap.GetLength(0) || Math.Max(y1, y2) + 1 > gridMap.GetLength(1))
            {
                gridMap = Expand2DArray(gridMap, Math.Max(gridMap.GetLength(0), Math.Max(x1, x2) + 1), Math.Max(gridMap.GetLength(1), Math.Max(y1, y2) + 1));
            }

            return gridMap;
        }

        private int CreateVerticalAndHorizontalLines(int overlapCount, int[,] gridMap, int x1, int x2, int y1, int y2)
        {
            if (x1 == x2)
            {
                for (int i = Math.Min(y1, y2); i <= Math.Max(y1, y2); i++)
                {
                    if (++gridMap[x1, i] == 2) overlapCount++;
                }
            }
            else if (y1 == y2)
            {
                for (int i = Math.Min(x1, x2); i <= Math.Max(x1, x2); i++)
                {
                    if (++gridMap[i, y1] == 2) overlapCount++;
                }
            }

            return overlapCount;
        }
    }
}