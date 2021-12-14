using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Solvers
{
    class SolverDay14 : ISolver
    {

        Dictionary<string, (string,string, char)> _polymerizationRules = new Dictionary<string, (string, string, char)>();
        Dictionary<char, BigInteger> _elementsCount = new Dictionary<char, BigInteger>();
        Dictionary<string, BigInteger> _elements = new Dictionary<string, BigInteger>();

        public void InitInput(string content)
        {
            var splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var initialPolymer = splitContent[0];
            foreach (var element in initialPolymer.Distinct())
                _elementsCount[element] = 0;
            for (int i = 0; i < initialPolymer.Length -1; i++)
            {
                var curr = initialPolymer.Substring(i, 2);
                _elementsCount[initialPolymer[i]]++;
                if (!_elements.ContainsKey(curr))
                    _elements[curr] = 1;
                else
                    _elements[curr]++;
            }
            _elementsCount[initialPolymer[initialPolymer.Length - 1]]++;


            foreach (var currentLine in splitContent.Skip(1))
            {
                var s = currentLine.SplitREE(" -> ");
                var end1 = s[0][0] + "" + s[1];
                var end2 = s[1] + s[0][1];
                _polymerizationRules[s[0]] = (end1, end2, s[1][0]);
            }
        }

        public string SolveFirstProblem()
        {
            for (int i = 0; i < 10; i ++)
                Polimerize();

            return (_elementsCount.Max(kvp => kvp.Value) - _elementsCount.Min(kvp => kvp.Value)).ToString();
        }

        private void Polimerize()
        {
            var newElements = new Dictionary<string, BigInteger>();

            foreach (var kvp in _elements)
            {
                var (newEnd1, newEnd2, addedChar) = _polymerizationRules[kvp.Key];
                if (!newElements.ContainsKey(newEnd1))
                    newElements[newEnd1] = 0;
                newElements[newEnd1] += kvp.Value;
                if (!newElements.ContainsKey(newEnd2))
                    newElements[newEnd2] = 0;
                newElements[newEnd2] += kvp.Value;
                if (!_elementsCount.ContainsKey(addedChar))
                    _elementsCount[addedChar] = 0;
                _elementsCount[addedChar] += kvp.Value;
            }
            _elements = newElements;
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            for (int i = 10; i < 40; i++)
                Polimerize();

            return (_elementsCount.Max(kvp => kvp.Value) - _elementsCount.Min(kvp => kvp.Value)).ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
}
