using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solvers
{
    class BingoGrid
    {
        int[,] UnmarkedGrid { get; set; } = new int[5, 5];
        int[,] Grid { get; set; } = new int[5, 5];
        int[] RowFound { get; set; } = new int[5];
        int[] ColumnFound { get; set; } = new int[5];

        public BingoGrid()
        {

        }

        public void InitRow(int[] row, int index)
        {
            for (int i = 0; i < 5; i++)
            {
                Grid[index, i] = row[i];
                UnmarkedGrid[index, i] = row[i];
            }
        }

        public bool CheckNumber(int number)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (Grid[y, x] == number)
                    {
                        RowFound[x]++;
                        ColumnFound[y]++;
                        UnmarkedGrid[y, x] = 0;

                        if (RowFound[x] == 5)
                            return true;
                        if (ColumnFound[y] == 5)
                            return true;
                    }
                }
            }
            return false;
        }

        public int GetUnmarkedScore()
        {
            int sum = 0;
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    sum += UnmarkedGrid[y, x];
                }
            }
            return sum;
        }

    }

    class SolverDay4 : ISolver
    {
        List<int> _bingoNumbers = null;
        List<BingoGrid> _grids = new List<BingoGrid>();
        public void InitInput(string content)
        {
            var splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            BingoGrid grid = null;
            int index = 0;
            foreach (var currentLine in splitContent)
            {
                if (_bingoNumbers == null)
                    _bingoNumbers = currentLine.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                else if (string.IsNullOrWhiteSpace(currentLine))
                {
                    if (grid != null)
                        _grids.Add(grid);
                    grid = new BingoGrid();
                    index = 0;
                }
                else
                {
                    var nums = currentLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                    grid.InitRow(nums, index);
                    index++;
                }
            }
        }

        private int maxI = 0;
        public string SolveFirstProblem()
        {
            BingoGrid found = null;
            int lastNumber = 0;
            for (int i = 0 ; i < _bingoNumbers.Count; i++)
            {
                var number = _bingoNumbers[i];
                lastNumber = number;
                foreach (var grid in _grids)
                {
                    if (grid.CheckNumber(number))
                    {
                        found = grid;
                        maxI = i;
                    }
                }
                if (found != null)
                    break;
            }


            _grids.Remove(found);

            return (found.GetUnmarkedScore() * lastNumber).ToString();
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            BingoGrid found = null;
            int lastNumber = 0;
            for (int i = maxI +1; i < _bingoNumbers.Count; i++)
            {
                var number = _bingoNumbers[i];
                lastNumber = number;
                foreach (var grid in _grids.ToList())
                {
                    if (grid.CheckNumber(number))
                    {
                        if (_grids.Count > 1)
                            _grids.Remove(grid);
                        else
                        {
                            found = grid;
                            break;
                        }
                    }
                }
                if (found != null)
                    break;
            }

            return (found.GetUnmarkedScore() * lastNumber).ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
    }
}
