using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Solvers
{
    class SolverDay1 : ISolver
    {
        private List<int> _inputInts = new List<int>();
        public void InitInput(string content)
        {
            var splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (var currentLine in splitContent)
            {
                if (int.TryParse(currentLine.Trim(), out var current))
                {
                    _inputInts.Add(current);
                }
            }
        }

        public string SolveFirstProblem()
        {
            int ups = 0;
            int past = int.MaxValue;
            foreach (var i in _inputInts)
            {
                if (i > past)
                    ups++;
                past = i;
            }
            return ups.ToString();
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
             int ups = 0;
            int curr = _inputInts[2], minus1 = _inputInts[1], minus2 = _inputInts[0];
            int prevTotal = curr + minus1 + minus2;
            for (int i = 3; i < _inputInts.Count; i++)
            {
                minus2 = minus1;
                minus1 = curr;
                curr = _inputInts[i];

                var currTotal = curr + minus1 + minus2;
                if (currTotal > prevTotal)
                    ups++;
                prevTotal = currTotal;
            }
            return ups.ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
}
