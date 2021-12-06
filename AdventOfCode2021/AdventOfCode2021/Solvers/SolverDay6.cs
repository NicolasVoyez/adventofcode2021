using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Solvers
{
    class SolverDay6 : ISolver
    {
        private List<int> _initialState;
        private BigInteger[] _fishesStates = new BigInteger[9];
        public void InitInput(string content)
        {
            _initialState = content.Trim().Split(new string[] { "," }, StringSplitOptions.None).Select(int.Parse).ToList();
            foreach (var fish in _initialState)
                _fishesStates[fish]++;
        }

        public string SolveFirstProblem()
        {
            for (int i = 0; i < 80; i++)
            {
                var newFishes = new BigInteger[9];
                for (int state = 1; state < 9; state++)
                {
                    newFishes[state - 1] = _fishesStates[state];
                }
                newFishes[6] += _fishesStates[0];
                newFishes[8] += _fishesStates[0];
                _fishesStates = newFishes;
            }

            return _fishesStates.Aggregate((BigInteger)0, (x,y) => x+y).ToString();
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            for (int i = 80; i < 256; i++)
            {
                var newFishes = new BigInteger[9];
                for (int state = 1; state < 9; state++)
                {
                    newFishes[state - 1] = _fishesStates[state];
                }
                newFishes[6] += _fishesStates[0];
                newFishes[8] += _fishesStates[0];
                _fishesStates = newFishes;
            }

            return _fishesStates.Aggregate((BigInteger)0, (x, y) => x + y).ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
    }
}
