using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Solvers
{
    class SolverDay10 : ISolver
    {
        private List<string> _lines = new List<string>();
        public void InitInput(string content)
        {
            _lines  = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public string SolveFirstProblem()
        {
            return _lines.Sum(l => GetLineFaultScore(l)).ToString();
        }

        private int GetLineFaultScore(string line)
        {
            Stack<char> openedStream = new Stack<char>();
            int score = 0;
            foreach (var c in line){
                if (OpeningChars.Contains(c))
                    openedStream.Push(c);
                else if (openedStream.Count == 0) // incomplete : too many closures
                    return 0; 
                else
                {
                    var prev = openedStream.Pop();
                    if (OpenToClose[prev] != c)
                        score += GetPoints(c);
                }
            }

            //if (openedStream.Count > 0)
                //return 0; // incomplete : too many openings

            return score;
        }
        private BigInteger GetAutoCompleteScore(string line)
        {
            Stack<char> openedStream = new Stack<char>();
            foreach (var c in line)
            {
                if (OpeningChars.Contains(c))
                    openedStream.Push(c);
                else
                {
                    var prev = openedStream.Pop();
                    if (OpenToClose[prev] != c)
                        return 0; // faulty line, ignore ?
                }
            }

            BigInteger score = 0;
            while (openedStream.Count != 0)
            {
                var c = openedStream.Pop();
                score *= 5;
                score += GetAutoCompleteScore(OpenToClose[c]);
            }
            
            return score;
        }


        private HashSet<char> OpeningChars = new HashSet<char> { '(', '[', '{', '<' };
        private HashSet<char> ClosingChars = new HashSet<char> { ')', ']', '}', '>' };
        private Dictionary<char, char> OpenToClose = new Dictionary<char, char> {
            { '(' , ')' },
            { '[' , ']' },
            { '{' , '}' },
            { '<' , '>' }
        };

        private int GetPoints(char c)
        {
            switch (c)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
            }
            return -1;
        }

        private int GetAutoCompleteScore(int c)
        {
            switch (c)
            {
                case ')':
                    return 1;
                case ']':
                    return 2;
                case '}':
                    return 3;
                case '>':
                    return 4;
            }
            return -1;
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            var scores = _lines.Select(l => GetAutoCompleteScore(l)).Where(s => s!= 0).OrderBy(x => x).ToList();
            return scores[(scores.Count - 1) / 2].ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
}
