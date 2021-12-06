using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Solvers
{
    class SolverDay5 : ISolver
    {
        private List<Segment> segments = new List<Segment>();
        public void InitInput(string content)
        {
            var splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var currentLine in splitContent)
            {
                var s = currentLine.Split(new string[] { " -> "}, StringSplitOptions.RemoveEmptyEntries);
                var f = s[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var t = s[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                segments.Add(new Segment(f[0], f[1], t[0], t[1]));
            }
        }

        public string SolveFirstProblem()
        {
            var nonDiagonals = segments.Where(s => s.IsHorizontal || s.IsVertical).ToList();
            HashSet<Point> intersections = new HashSet<Point>();
            for (int s1 = 0; s1 < nonDiagonals.Count -1; s1++)
            {
                var segment = nonDiagonals[s1];
                for (int s2 = s1 +1; s2 < nonDiagonals.Count; s2++)
                {
                    foreach(var intersection in segment.GetIntersections(nonDiagonals[s2]))
                    {
                        intersections.Add(intersection);
                    }
                }
            }

            return intersections.Count.ToString();
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            Dictionary<Point, int> intersections = new Dictionary<Point, int>();
            
            foreach(var segment in segments)
            {
                foreach(var point in segment.GetAllPoints())
                {
                    if (intersections.ContainsKey(point))
                        intersections[point]++;
                    else
                        intersections[point] = 1;
                }
            }
            // 20733 is too high
            return intersections.Count(kvp=> kvp.Value > 1).ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
    }
}
