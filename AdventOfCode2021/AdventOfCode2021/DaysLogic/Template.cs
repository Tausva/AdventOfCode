using System;
using System.Collections.Generic;

namespace AdventOfCode2021.DaysLogic
{
    public class Template<T>
    {
        public virtual string Part1 (List<T> input)
        {
            throw new NotImplementedException();
        }
        public virtual string Part2(List<T> input)
        {
            throw new NotImplementedException();
        }

        protected int ToInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                throw new FormatException(value + " is not an integer");
            }
        }

        protected long ToLong(string value)
        {
            try
            {
                return long.Parse(value);
            }
            catch
            {
                throw new FormatException(value + " is not an integer");
            }
        }

        protected int ConvertFromBinaryToDecimal(string binaryInput) 
        {
            int output = 0;

            for (int i = 0; i < binaryInput.Length; i++)
            {
                output += ToInt(binaryInput[i].ToString()) * (int)Math.Pow(2, binaryInput.Length - 1 - i);
            }

            return output;
        }

        protected List<int> ConvertStringToIntList(string line, string separator)
        {
            List<int> newList = new List<int>();

            string[] elements = line.Split(separator);
            foreach (string element in elements)
            {
                int number;
                if (int.TryParse(element, out number))
                {
                    newList.Add(number);
                }
            }

            return newList;
        }

        protected int[,] Expand2DArray(int[,] oldArray, int newXSize, int newYSize)
        {
            int[,] newArray = new int[newXSize, newYSize];
            for (int i = 0; i < newXSize; ++i)
            {
                for (int j = 0; j < newYSize; ++j)
                {
                    newArray[i, j] = i < oldArray.GetLength(0) && j < oldArray.GetLength(1) ? oldArray[i, j] : 0;
                }
            }
            return newArray;
        }
    }
}