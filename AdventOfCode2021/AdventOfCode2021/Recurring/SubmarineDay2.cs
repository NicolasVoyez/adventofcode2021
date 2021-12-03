using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Helpers
{
    public enum Direction
    {
        Forward,
        Down,
        Up
    }


    public class SubmarineDay2
    {
        public struct Instruction
        {
            public Direction Direction { get; set; }

            public Instruction(Direction direction, int distance) : this()
            {
                Direction = direction;
                Distance = distance;
            }

            public int Distance { get; set; }
        }
        public int XPos { get; set; }
        public int ZPos { get; set; }
        public int Aim { get; set; } = 0;
        public SubmarineDay2()
        {

        }

        public void Move (Instruction instruction)
        {
            switch (instruction.Direction)
            {
                case Direction.Down:
                    ZPos += instruction.Distance;
                    break;
                case Direction.Up:
                    ZPos -= instruction.Distance;
                    break;
                case Direction.Forward:
                    XPos += instruction.Distance;
                    break;
            }
        }

        public void MoveWithAim(Instruction instruction)
        {
            switch (instruction.Direction)
            {
                case Direction.Down:
                    Aim += instruction.Distance;
                    break;
                case Direction.Up:
                    Aim -= instruction.Distance;
                    break;
                case Direction.Forward:
                    XPos += instruction.Distance;
                    ZPos += instruction.Distance * Aim;
                    break;
            }
        }

        public override string ToString()
        {
            return (XPos * ZPos).ToString();
        }
    }
}
