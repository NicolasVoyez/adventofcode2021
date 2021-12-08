using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Solvers
{
    class SolverDay2 : ISolver
    {
        List<SubmarineDay2.Instruction> _instructions = new List<SubmarineDay2.Instruction>();
        public void InitInput(string content)
        {
            var splitContent = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (var currentLine in splitContent)
            {
                var split = currentLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 2)
                {
                    _instructions.Add(new SubmarineDay2.Instruction(ToDirection(split[0]), int.Parse(split[1])));
                }
            }
        }

        private Direction ToDirection(string d)
        {
            switch (d)
            {
                case "down":
                    return Direction.Down;
                case "up":
                    return Direction.Up;
                case "forward":
                    return Direction.Forward;
            }

            throw new NotImplementedException();
        }

        public string SolveFirstProblem()
        {
            var submarine = new SubmarineDay2();
            foreach (var instruction in _instructions)
                submarine.Move(instruction);

            return submarine.ToString();
        }

        public string SolveSecondProblem(string firstProblemSolution)
        {
            var submarine = new SubmarineDay2();
            foreach (var instruction in _instructions)
                submarine.MoveWithAim(instruction);

            return submarine.ToString();
        }

        public bool Question2CodeIsDone { get; } = true;
        public bool TestOnly { get; } = false;
    }
}
