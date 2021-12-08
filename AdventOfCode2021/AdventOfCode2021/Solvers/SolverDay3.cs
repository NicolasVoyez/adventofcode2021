using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solvers
{
    class SolverDay3 : ISolver
    {
        List<string> _input = new List<string>();
        char[] _mostCommon;
        public void InitInput(string content)
        {
            var splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (var currentLine in splitContent)
            {
                _input.Add(currentLine);
            }
            _mostCommon = new char[splitContent[0].Length];
        }

        public string SolveFirstProblem()
        {
            var counts = new int[_input[0].Length];
            foreach(var input in _input)
            {
                for (int pos = 0; pos < counts.Length; pos++)
                {
                    if (input[pos] == '1')
                        counts[pos]++;
                }
            }
            var med = (float)_input.Count / 2;
            var gammaR = "";
            var epsilonR = "";

            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > med)
                {
                    _mostCommon[i] = '1';
                    gammaR += "1";
                    epsilonR += "0";
                }
                else
                {
                    _mostCommon[i] = '0';
                    gammaR += "0";
                    epsilonR += "1";
                }
            }

            var gammaBase10 = Convert.ToInt32(gammaR, 2);
            var epsilonBase10 = Convert.ToInt32(epsilonR, 2);
            return (gammaBase10 * epsilonBase10).ToString() ;


        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            var remainingOxygen = _input.ToList();
            var remainingCO2 = _input.ToList();

            for (int i = 0; i < _mostCommon.Length; i++)
            {
                var currentlyCommon = GetMostCommon(remainingOxygen,i);
                if (remainingOxygen.Count > 1)
                    remainingOxygen.RemoveAll(o => o[i] != currentlyCommon);
            }

            for (int i = 0; i < _mostCommon.Length; i++)
            {
                var currentlyCommon = GetMostCommon(remainingCO2, i);
                if (remainingCO2.Count > 1)
                    remainingCO2.RemoveAll(o => o[i] == currentlyCommon);
            }

            var oxygenBase10 = Convert.ToInt32(remainingOxygen[0], 2);
            var co2Base10 = Convert.ToInt32(remainingCO2[0], 2);
            return (oxygenBase10 * co2Base10).ToString();
        }

        private char GetMostCommon(List<string> list, int i)
        {
            return list.Count(e => e[i] == '1') >= ((float)list.Count / 2) ? '1' : '0';
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
}
