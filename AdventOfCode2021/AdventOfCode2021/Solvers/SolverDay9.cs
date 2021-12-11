using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021.Solvers
{
    class SolverDay9 : ISolver
    {
        private Grid<int> _grid;
        public void InitInput(string content)
        {
            _grid = new Grid<int>(content, c => int.Parse(c.ToString()));
        }

        public string SolveFirstProblem()
        {
            var riskLevel = 0;
            foreach(var cell in _grid.All())
            {
                if (IsRisky(cell))
                    riskLevel += 1 + cell.Value;
            }

            //Print();
            return riskLevel.ToString();
        }

        private void Print()
        {
            _grid.Print(c =>
            {
                if (IsRisky(c))
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

        private bool IsRisky(Grid<int>.Cell<int> cell)
        {
            return _grid.Around(cell, false).All(other => other.Value > cell.Value);
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            var lowPoints = _grid.All().Where(c => IsRisky(c));
            List<int> bassins = new List<int>();
            Parallel.ForEach(lowPoints, point =>
            {
                var bassin = GetBassin(point, lowPoints);
                if (bassin != -1)
                    bassins.Add(bassin);
            });

            bassins = bassins.OrderByDescending(x => x).ToList();
            return (bassins[0] * bassins[1] * bassins[2]).ToString();
        }

        private int GetBassin(Grid<int>.Cell<int> point, IEnumerable<Grid<int>.Cell<int>> lowPoints)
        {
            HashSet<Grid<int>.Cell<int>> bassin = new HashSet<Grid<int>.Cell<int>>();
            HashSet<Grid<int>.Cell<int>> enqueuedOnce = new HashSet<Grid<int>.Cell<int>>();
            Queue<Grid<int>.Cell<int>> todo = new Queue<Grid<int>.Cell<int>>();
            todo.Enqueue(point);
            while (todo.Count > 0)
            {
                
                var current = todo.Dequeue();
                bassin.Add(current);
                foreach(var nextPt in _grid.Around(current, false))
                {
                    if (enqueuedOnce.Contains(nextPt)) continue;
                    if (nextPt.Value == 9) continue;
                    if (nextPt == point) continue;
                    if (lowPoints.Contains(nextPt)) return -1;
                    if (bassin.Contains(nextPt)) continue;
                    if (nextPt.Value < point.Value) continue;

                    enqueuedOnce.Add(nextPt);
                    todo.Enqueue(nextPt);
                }
            }
            return bassin.Count;
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
}
