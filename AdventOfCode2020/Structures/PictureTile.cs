using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Structures
{
    public class PictureTile
    {
        int id;
        char[][] tile;
        List<string> borderArray;
        List<int> borderingTiles;

        public List<string> BorderArray { get => borderArray; }
        public int Id { get => id; }
        public List<int> BorderingTiles { get => borderingTiles; }

        public PictureTile(int tileId, char[][] tiledata)
        {
            id = tileId;
            tile = tiledata;
            borderingTiles = new List<int>();

            GenerateBorderArray();
        }

        private void GenerateBorderArray()
        {
            borderArray = new List<string>();
            string borderA = "", borderB = "", borderC = "", borderD = "";

            for (int i = 0; i < tile.GetLength(0); i++)
            {
                borderA += tile[0][i];
                borderB += tile[i][tile.GetLength(0) - 1];
                borderC += tile[tile.GetLength(0) - 1][i];
                borderD += tile[i][0];
            }
            borderArray.Add(borderA);
            borderArray.Add(borderB);
            borderArray.Add(borderC);
            borderArray.Add(borderD);
        }

        public bool HasMachingBorder(List<string> borders, int tileIndex)
        {
            foreach (string border in borders)
            {
                if (borderArray.Contains(border))
                {
                    borderingTiles.Add(tileIndex);
                    return true;
                }

                List<char> reversedBorderChars = new List<char>(border.ToCharArray());
                reversedBorderChars.Reverse();
                string reversedBorder = new string(reversedBorderChars.ToArray());

                if (borderArray.Contains(reversedBorder))
                {
                    borderingTiles.Add(tileIndex);
                    return true;
                }
            }
            return false;
        }

        //public char[,] GetPicture()
        //{

        //}
    }
}
