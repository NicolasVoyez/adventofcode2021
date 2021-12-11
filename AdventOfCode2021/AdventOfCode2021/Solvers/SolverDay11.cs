using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solvers
{
    class SolverDay11 : ISolver
    {
        private Grid<int> _grid;
        private Grid<int> _initialGrid;
        private int _currentRound = 1;
        public void InitInput(string content)
        {
            _grid = new Grid<int>(content, c => int.Parse(c.ToString()));
            _initialGrid = new Grid<int>(content, c => int.Parse(c.ToString()));
        }

        public string SolveFirstProblem()
        {
            int flashesCount = 0;
            for (_currentRound = 1; _currentRound <= 100; _currentRound++)
            {
                flashesCount += MakeOneRound();
                
                //if (i <= 10 || i %10 == 0)
                //    PrintGrid();
            }


            return flashesCount.ToString();
        }

        private void PrintGrid()
        {
            _grid.Print(c =>
            {
                if (c.Value == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(c.Value);
                    Console.ResetColor();

                }
                else
                    Console.Write(c.Value);
            });

            Console.ReadKey();
        }

        private int MakeOneRound()
        {
            var illuminated = 0;
            foreach (var cell in _grid.All())
            {
                _grid.Grow(cell);
            }
            foreach (var cell in _grid.All())
            {
                if (cell.Value > 9)
                {
                    illuminated++;
                    _grid.Set(cell.Y, cell.X, 0);
                }
            }
            return illuminated;
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            for (; _currentRound < int.MaxValue; _currentRound++)
            {
                if(MakeOneRound() == 100)
                {
                    return _currentRound.ToString();
                }
            }

            return "DAMNED !";
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }

    public static class IntCellExtensions
    {
        public static void Grow(this Grid<int> grid, Grid<int>.Cell<int> cell)
        {
            grid.Set(cell.Y, cell.X, cell.Value + 1);
            if (cell.Value == 9)
            {
                foreach (var a in grid.Around(cell))
                {
                    grid.Grow(a);
                }
            }
        }
    }
}
