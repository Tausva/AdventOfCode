using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.DaysLogic
{
    public class Day04 : Template<string>
    {
        public override string Part1(List<string> input)
        {
            string inputBigString = string.Join(Environment.NewLine, input);
            List<string> inputBlocks = inputBigString.Split(Environment.NewLine + Environment.NewLine).ToList();
            List<BingoBoard> boards = new List<BingoBoard>();
            List<int> luckyNumbers = GetSetup(inputBlocks, boards);

            for (int i = 0; i < luckyNumbers.Count; i++)
            {
                foreach (BingoBoard board in boards)
                {
                    if (board.PunchMark(luckyNumbers[i]))
                    {
                        return (board.GetScore() * luckyNumbers[i]).ToString();
                    }
                }
            }

            return "-1";
        }

        public override string Part2(List<string> input)
        {
            string inputBigString = string.Join(Environment.NewLine, input);
            List<string> inputBlocks = inputBigString.Split(Environment.NewLine + Environment.NewLine).ToList();
            List<BingoBoard> boards = new List<BingoBoard>();
            List<int> luckyNumbers = GetSetup(inputBlocks, boards);
            int participatingBoardsCount = boards.Count;

            for (int i = 0; i < luckyNumbers.Count; i++)
            {
                foreach (BingoBoard board in boards)
                {
                    if (board.PunchMark(luckyNumbers[i]) && --participatingBoardsCount == 0)
                    {

                        return (board.GetScore() * luckyNumbers[i]).ToString();
                    }
                }
            }

            return "-1";
        }

        private List<int> GetSetup(List<string> inputBlocks, List<BingoBoard> boards)
        {
            List<int> luckyNumbers = ConvertStringToIntList(inputBlocks[0], ",");
            inputBlocks.RemoveAt(0);

            foreach (string inputBlock in inputBlocks)
            {
                boards.Add(new BingoBoard(inputBlock));
            }

            return luckyNumbers;
        }
       
        private class BingoBoard
        {

            public List<BingoCell> cells;
            private int[] emptyRowCount;
            private int[] emptyColumnCount;

            public bool won;

            public BingoBoard(string boardInfo)
            {
                cells = new List<BingoCell>();
                emptyRowCount = new int[5];
                emptyColumnCount = new int[5];

                FillCells(boardInfo);
            }

            public void FillCells(string boardInfo)
            {
                List<string> rowInfo = boardInfo.Split(Environment.NewLine).ToList();

                for (int y = 0; y < rowInfo.Count; y++)
                {
                    if (rowInfo[y][0] == ' ')
                    {
                        rowInfo[y] = rowInfo[y].Substring(1);
                    }
                    List<string> columnInfo = rowInfo[y].Replace("  ", " ").Split(' ').ToList();

                    for (int x = 0; x < columnInfo.Count; x++)
                    {
                        cells.Add(new BingoCell(int.Parse(columnInfo[x]), y, x));
                        emptyRowCount[y]++;
                        emptyColumnCount[x]++;

                    }
                }
            }

            public bool PunchMark(int number)
            {
                if (won) return false;

                BingoCell cell = new BingoCell(number);
                int index = cells.IndexOf(cell);
                if (index >= 0)
                {
                    cells[index].IsCrossed = true;

                    emptyRowCount[cells[index].Row]--;
                    emptyColumnCount[cells[index].Collumn]--;

                    if (emptyRowCount[cells[index].Row] == 0 || emptyColumnCount[cells[index].Collumn] == 0)
                    {
                        won = true;
                        return true;
                    }
                }
                return false;
            }

            public int GetScore()
            {
                int sum = 0;
                foreach (BingoCell cell in cells)
                {
                    if (!cell.IsCrossed) sum += cell.Number;
                }

                return sum;
            }

            public class BingoCell
            {
                public BingoCell(int number, int collumn, int row)
                {
                    Number = number;
                    Collumn = collumn;
                    Row = row;
                    IsCrossed = false;
                }

                public BingoCell(int number)
                {
                    Number = number;
                }

                public int Number { get; set; }
                public int Collumn { get; set; }
                public int Row { get; set; }
                public bool IsCrossed { get; set; }

                public override bool Equals(object obj)
                {
                    BingoCell bingoCell = (BingoCell)obj;
                    return Number == bingoCell.Number;
                }

                public override int GetHashCode()
                {
                    return base.GetHashCode();
                }
            }
        }
    }
}