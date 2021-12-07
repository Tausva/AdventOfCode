using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.DaysLogic
{
    public class Day07 : Template<string>
    {
        public override string Part1(List<string> input)
        {
            List<int> crabPositions = GetInput(input);

            int median = GetMedian(crabPositions);

            int fuelUsed = 0;
            foreach (int position in crabPositions)
            {
                fuelUsed += Math.Abs(position - median);
            }

            return fuelUsed.ToString();
        }

        public override string Part2(List<string> input)
        {
            List<int> crabPositions = GetInput(input);

            int positionSum = 0;
            foreach (var crabPosition in crabPositions)
            {
                positionSum += crabPosition;
            }
            int average = positionSum / crabPositions.Count;

            int fuelUsed = 0;
            Dictionary<int, int> sequenceLibrary = new Dictionary<int, int>();
            sequenceLibrary.Add(0,0);

            foreach (int position in crabPositions)
            {
                int sequenceKey = Math.Abs(position - average);
                if (sequenceLibrary.ContainsKey(sequenceKey))
                {
                    fuelUsed += sequenceLibrary[sequenceKey];
                }
                else
                {
                    GetNewValue(sequenceLibrary, sequenceKey);
                    fuelUsed += sequenceLibrary[sequenceKey];
                }
            }
            
            return fuelUsed.ToString();
        }

        private void GetNewValue(Dictionary<int, int> sequenceLibrary, int sequenceKey)
        {
            if (!sequenceLibrary.ContainsKey(sequenceKey - 1))
            {
                GetNewValue(sequenceLibrary, sequenceKey - 1);
            }

            int newValue = sequenceLibrary[sequenceKey - 1] + sequenceKey;
            sequenceLibrary.Add(sequenceKey, newValue);
        }

        private static List<int> GetInput(List<string> input)
        {
            List<string> positionList = new List<string>();
            foreach (string line in input)
            {
                positionList.AddRange(line.Split(','));
            }

            List<int> crabPositions = new List<int>();
            foreach (string position in positionList)
            {
                crabPositions.Add(int.Parse(position));
            }

            return crabPositions;
        }
    }
}