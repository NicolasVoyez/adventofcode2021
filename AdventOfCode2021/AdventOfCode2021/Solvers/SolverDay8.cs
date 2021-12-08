using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solvers
{


    class SolverDay8 : ISolver
    {
        private List<SevenSectionDisplay> _displays = new List<SevenSectionDisplay>();
        public void InitInput(string content)
        {
            var splitContent = content.SplitREE("\r\n");

            foreach (var currentLine in splitContent)
            {
                var split = currentLine.SplitREE(" | ");
                _displays.Add(new SevenSectionDisplay(split[0].SplitREE(), split[1].SplitREE()));
            }
        }

        public string SolveFirstProblem()
        {
            return _displays.Sum(d => d.CountOneFourSevenAndEight()).ToString();
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            return _displays.Sum(d => d.GetOutPutValue()).ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
    class SevenSectionDisplay
    {
        public IEnumerable<string> InitializeDigits { get; }
        public IList<string> Display { get; }
        Dictionary<string, int> _stringToDigit = new Dictionary<string, int>();

        public SevenSectionDisplay(IEnumerable<string> initializeDigits, IEnumerable<string> display)
        {
            InitializeDigits = initializeDigits.Select(s => new string(s.OrderBy(c => c).ToArray())).ToList();
            Display = display.Select(s => new string(s.OrderBy(c => c).ToArray())).ToList();

            CalculateDigits();
        }

        private void CalculateDigits()
        {
            var one = InitializeDigits.FirstOrDefault(x => x.Length == 2);
            _stringToDigit[one] = 1;
            var four = InitializeDigits.FirstOrDefault(x => x.Length == 4);
            _stringToDigit[four] = 4;
            var seven = InitializeDigits.FirstOrDefault(x => x.Length == 3);
            _stringToDigit[seven] = 7;
            var eight = InitializeDigits.FirstOrDefault(x => x.Length == 7);
            _stringToDigit[eight] = 8;


            var f = one.First();
            var A = seven.FirstOrDefault(c => !one.Contains(c));
            var C = InitializeDigits.Count(s => s.Contains(f)) == 8 ? f : one[1];
            var F = C == f ? one[1] : f;

            char E = '1';
            char B = '1';
            foreach (var letter in eight)
            {
                if (letter == A || letter == C || letter == F)
                    continue;
                var cnt = InitializeDigits.Count(s => s.Contains(letter));
                if (cnt == 6)
                    B = letter;
                if (cnt == 4)
                    E = letter;
            }

            char D = four.FirstOrDefault(c => c != B && c != C && c != F);
            char G = eight.FirstOrDefault(c => c != A && c != B && c != C && c != D && c != E && c != F);

            _stringToDigit[GetOrderedString(A, B, C, E, F, G)] = 0;
            _stringToDigit[GetOrderedString(A, C,D, E, G)] = 2;
            _stringToDigit[GetOrderedString(A, C, D, F, G)] = 3;
            _stringToDigit[GetOrderedString(A, B, D, F, G)] = 5;
            _stringToDigit[GetOrderedString(A, B, D, E, F, G)] = 6;
            _stringToDigit[GetOrderedString(A, B, C, D, F, G)] = 9;
        }

        private string GetOrderedString(params char[] digits) => new string( digits.OrderBy(c => c).ToArray());

        public int CountOneFourSevenAndEight()
        {
            int count = 0;
            foreach (var stringDigit in Display)
            {
                if (_stringToDigit.TryGetValue(stringDigit, out int digit) && (digit == 1 || digit == 4 || digit == 7 || digit == 8))
                    count++;

            }
            return count;
        }

        public int GetOutPutValue()
        {
            return _stringToDigit[Display[0]] * 1000 + _stringToDigit[Display[1]] * 100 + _stringToDigit[Display[2]] * 10 + _stringToDigit[Display[3]];
        }
    }
}