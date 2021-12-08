using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solvers
{
    class SolverDay7 : ISolver
    {
        private List<int> _initialState;
        private int _max;
        private int _min;

        public void InitInput(string content)
        {
            _initialState = content.Trim().Split(new string[] { "," }, StringSplitOptions.None).Select(int.Parse).ToList();
            _max = _initialState.Max();
            _min = _initialState.Min();
        }

        public string SolveFirstProblem()
        {
            var bestPos = _min - 1;
            var bestFuel = int.MaxValue;
            for (int i = _min; i <= _max; i++)
            {
                var score = _initialState.Sum(crab => Math.Abs(crab - i));
                if (score < bestFuel)
                {
                    bestFuel = score;
                    bestPos = i;
                }    
            }
            return bestFuel.ToString();
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            var bestPos = _min - 1;
            var bestFuel = int.MaxValue;
            for (int i = _min; i <= _max; i++)
            {
                var score = _initialState.Sum(crab => Sum1ToN(Math.Abs(crab - i)));
                if (score < bestFuel)
                {
                    bestFuel = score;
                    bestPos = i;
                }
            }
            return bestFuel.ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;

        private int Sum1ToN(int n) => n *(n + 1) / 2;
    }
}
