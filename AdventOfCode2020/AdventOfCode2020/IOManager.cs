using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class IOManager
    {
        private const int BaseDirectoryShortcut = 24;
        private const string InputFolderPath = @"Inputs/";

        private string inputFolderPath;

        public IOManager()
        {
            inputFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            inputFolderPath = inputFolderPath.Substring(0, inputFolderPath.Length - BaseDirectoryShortcut);
        }

        public List<int> ReadIntList(string filename)
        {
            List<string> elements = ReadStringList(filename);
            List<int> newList = new List<int>();

            foreach(string element in elements)
            {
                int number;
                if (int.TryParse(element, out number))
                {
                    newList.Add(number);
                }
            }

            return newList;
        }

        public List<string> ReadStringList(string filename)
        {
            string[] elements = System.IO.File.ReadAllLines(inputFolderPath + InputFolderPath + filename);

            return new List<string>(elements);
        }
    }
}