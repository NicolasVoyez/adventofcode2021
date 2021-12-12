using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solvers
{
    public class Cave
    {
        public Cave(string name)
        {
            Name = name;
            IsSmall = name.ToLower() == name;
            LinkedCaves = new HashSet<Cave>();
        }

        public string Name { get; }
        public bool IsSmall { get; }
        public HashSet<Cave> LinkedCaves { get; }

        public override string ToString()
        {
            return Name;
        }

        public Cave Copy(Dictionary<string, Cave> copiedCaves)
        {
            if (copiedCaves.TryGetValue(Name, out var curr))
                return curr;
            curr = new Cave(Name);
            copiedCaves[Name] = curr;

            foreach (var cave in LinkedCaves)
            {
                if (!copiedCaves.TryGetValue(cave.Name, out var linked))
                {
                    linked = cave.Copy(copiedCaves);
                }
                curr.LinkedCaves.Add(linked);
            }

            return curr;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Cave cave))
                return false;
            return Equals(cave);
        }
        public bool Equals(Cave cave)
        {
            return Name.Equals(cave?.Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Cave cave1, Cave cave2)
        {
            if (ReferenceEquals(cave1, null) && ReferenceEquals(cave2, null))
                return true;
            if (ReferenceEquals(cave1, null) || ReferenceEquals(cave2, null))
                return false;
            return cave1.Equals(cave2);
        }
        public static bool operator !=(Cave cave1, Cave cave2)
        {
            return !(cave1 == cave2);
        }
    }

    class SolverDay12 : ISolver
    {
        private Dictionary<string,Cave> _initialCaves = new Dictionary<string,Cave>();
        private Cave _start;
        private Cave _end;
        public void InitInput(string content)
        {
            var splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var currentLine in splitContent)
            {
                var split = currentLine.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                if (!_initialCaves.TryGetValue(split[0], out var element))
                {
                    element = new Cave(split[0]);
                    _initialCaves[split[0]] = element;
                }
                if (!_initialCaves.TryGetValue(split[1], out var element2))
                {
                    element2 = new Cave(split[1]);
                    _initialCaves[split[1]] = element2;
                }
                element.LinkedCaves.Add(element2);
                element2.LinkedCaves.Add(element);
            }
            _start = _initialCaves["start"];
            _end = _initialCaves["end"];
        }

        public string SolveFirstProblem()
        {
            List<List<Cave>> paths = new List<List<Cave>>();
            Stack<(Cave, List<Cave>, HashSet<Cave>)> toDo = new Stack<(Cave, List<Cave>, HashSet<Cave>)> ();
            toDo.Push((_start, new List<Cave> { _start }, new HashSet<Cave> { _start }));
            while (toDo.Count != 0)
            {
                var (currentNode, currentVisit, alreadyVisited) = toDo.Pop();
                foreach (var node in currentNode.LinkedCaves)
                {
                    if (node == _start)
                        continue;
                    if (node.IsSmall && alreadyVisited.Contains(node))
                        continue;
                    if (node == _end)
                    {
                        currentVisit.Add(node);
                        paths.Add(currentVisit);
                        continue;
                    }
                    var newVisited = new List<Cave>(currentVisit);
                    newVisited.Add(node);
                    var newAlreadyVisited = new HashSet<Cave>(alreadyVisited);
                    newAlreadyVisited.Add(node);
                    toDo.Push((node, newVisited, newAlreadyVisited));
                }
            }
            return paths.Count.ToString();
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            List<string> paths = new List<string>();
            Stack<(Cave, List<Cave>, HashSet<Cave>, bool)> toDo = new Stack<(Cave, List<Cave>, HashSet<Cave>, bool)>();
            toDo.Push((_start, new List<Cave> { _start }, new HashSet<Cave> { _start }, false));
            while (toDo.Count != 0)
            {
                var (currentNode, currentVisit, alreadyVisited, hasDoubled) = toDo.Pop();
                foreach (var node in currentNode.LinkedCaves)
                {
                    if (node == _start)
                        continue;
                    if (node.IsSmall && alreadyVisited.Contains(node))
                        continue;
                    if (node == _end)
                    {
                        currentVisit.Add(node);
                        paths.Add(string.Join(",", currentVisit));
                        continue;
                    }

                    if (node.IsSmall && !hasDoubled)
                    {
                        var newVisitedDoubling = new List<Cave>(currentVisit);
                        newVisitedDoubling.Add(node);
                        var newAlreadyVisitedDoubling = new HashSet<Cave>(alreadyVisited);
                        toDo.Push((node, newVisitedDoubling, newAlreadyVisitedDoubling, true));
                    }

                    var newVisited = new List<Cave>(currentVisit);
                    newVisited.Add(node);
                    var newAlreadyVisited = new HashSet<Cave>(alreadyVisited);
                    newAlreadyVisited.Add(node);
                    toDo.Push((node, newVisited, newAlreadyVisited, hasDoubled));
                }
            }
            return paths.Distinct().OrderBy(x => x).Count().ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
}
