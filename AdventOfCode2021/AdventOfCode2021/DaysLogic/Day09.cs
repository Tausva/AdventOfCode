using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.DaysLogic
{
    public class Day09 : Template<string>
    {
        public override string Part1(List<string> input)
        {
            int sumRiskLevel = 0;

            int[,] heightMap = new int[input.Count, input[0].Length];
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    heightMap[i, j] = int.Parse(input[i][j].ToString());
                }
            }

            for (int i = 0; i < heightMap.GetLength(0); i++)
            {
                for (int j = 0; j < heightMap.GetLength(1); j++)
                {
                    int smallerNeighborCount = 0;

                    if (i - 1 >= 0 && heightMap[i, j] >= heightMap[i - 1, j]) smallerNeighborCount++;
                    if (j - 1 >= 0 && heightMap[i, j] >= heightMap[i, j - 1]) smallerNeighborCount++;
                    if (i + 1 < heightMap.GetLength(0) && heightMap[i, j] >= heightMap[i + 1, j]) smallerNeighborCount++;
                    if (j + 1 < heightMap.GetLength(1) && heightMap[i, j] >= heightMap[i, j + 1]) smallerNeighborCount++;

                    if (smallerNeighborCount == 0)
                    {
                        sumRiskLevel += 1 + heightMap[i, j];
                    }
                }
            }
            return sumRiskLevel.ToString();
        }

        public override string Part2(List<string> input)
        {
            Dictionary<Tuple<int, int>, int> unvisitedNodes = new Dictionary<Tuple<int, int>, int>();
            Queue<Tuple<int, int>> jobQueue = new Queue<Tuple<int, int>>();
            
            int[,] heightMap = new int[input.Count, input[0].Length];
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    heightMap[i, j] = int.Parse(input[i][j].ToString());
                    if (heightMap[i, j] < 9)
                    {
                        unvisitedNodes.Add(new Tuple<int, int>(i, j), heightMap[i, j]);
                    }
                }
            }
            unvisitedNodes = unvisitedNodes.OrderBy(node => node.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            List<int> basinSizes = new List<int>();
            int currentBasinSize = 0;
            while (unvisitedNodes.Count > 0)
            {
                Tuple<int, int> node;

                if (jobQueue.Count == 0)
                {
                    basinSizes.Add(currentBasinSize);
                    currentBasinSize = 0;
                    node = unvisitedNodes.ElementAt(0).Key;
                }
                else
                {
                    node = jobQueue.Dequeue();
                }

                unvisitedNodes.Remove(node);
                currentBasinSize++;

                int i = node.Item1;
                int j = node.Item2;

                if (i - 1 >= 0 && heightMap[i - 1, j] < 9 && unvisitedNodes.ContainsKey(new Tuple<int, int>(i - 1, j)))
                {
                    if (!jobQueue.Contains(new Tuple<int, int>(i - 1, j)))
                    {
                        jobQueue.Enqueue(new Tuple<int, int>(i - 1, j));
                    }
                }
                if (j - 1 >= 0 && heightMap[i, j - 1] < 9 && unvisitedNodes.ContainsKey(new Tuple<int, int>(i, j - 1)))
                {
                    if (!jobQueue.Contains(new Tuple<int, int>(i, j - 1)))
                    {
                        jobQueue.Enqueue(new Tuple<int, int>(i, j - 1));
                    }
                }
                if (i + 1 < heightMap.GetLength(0) && heightMap[i + 1, j] < 9 && unvisitedNodes.ContainsKey(new Tuple<int, int>(i + 1, j)))
                {
                    if (!jobQueue.Contains(new Tuple<int, int>(i + 1, j)))
                    {
                        jobQueue.Enqueue(new Tuple<int, int>(i + 1, j));
                    }
                }
                if (j + 1 < heightMap.GetLength(1) && heightMap[i, j + 1] < 9 && unvisitedNodes.ContainsKey(new Tuple<int, int>(i, j + 1)))
                {
                    if (!jobQueue.Contains(new Tuple<int, int>(i, j + 1)))
                    {
                        jobQueue.Enqueue(new Tuple<int, int>(i, j + 1));
                    }
                }
            }
            basinSizes.Add(currentBasinSize);
            basinSizes = basinSizes.OrderByDescending(i => i).ToList();
            return (basinSizes[0] * basinSizes[1] * basinSizes[2]).ToString();
        }
    }
}