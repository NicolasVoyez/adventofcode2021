using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solvers
{
    class SolverDay13 : ISolver
    {
        private List<(char, int)> _foldInstructions = new List<(char, int)>();
        private PositionOnlyGrid _grid;
        public void InitInput(string content)
        {
            var splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var positions = new List<(int, int)>();
            foreach (var currentLine in splitContent)
            {
                if (currentLine.StartsWith("fold"))
                {
                    var s = currentLine.SplitREE("=");
                    _foldInstructions.Add((s[0][s[0].Length - 1], int.Parse(s[1])));
                }
                else
                {
                    var pos = currentLine.SplitAsInt(",").ToList();
                    positions.Add((pos[0], pos[1]));
                }
            }
            _grid = new PositionOnlyGrid(positions);
        }

        public string SolveFirstProblem()
        {
            Fold(0);
            return _grid.Count.ToString();
        }

        private void Fold(int index)
        {
            var (direction, pos) = _foldInstructions[index];
            if (direction == 'y')
                _grid.FoldY(pos);
            else
                _grid.FoldX(pos);
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            for (int i = 1; i < _foldInstructions.Count; i++)
                Fold(i);

            _grid.Print();
            return " \r\n\r\n It Ended well didn't it ? \r\n\r\n";
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
}
