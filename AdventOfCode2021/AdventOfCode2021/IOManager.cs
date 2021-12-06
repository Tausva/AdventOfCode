using System;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    public static class IOManager
    {
        private const int BaseDirectoryShortcut = 17;
        private const string InputFolderPath = @"Inputs/";

        private static string inputFolderPath;

        public static void Instantiate()
        {
            inputFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            inputFolderPath = inputFolderPath.Substring(0, inputFolderPath.Length - BaseDirectoryShortcut);
        }

        public static List<int> ReadIntList(string filename)
        {
            List<string> elements = ReadStringList(filename);
            List<int> newList = new List<int>();

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

        public static List<string> ReadStringList(string filename)
        {
            string[] elements = System.IO.File.ReadAllLines(inputFolderPath + InputFolderPath + filename);

            return new List<string>(elements);
        }
    }
}