using System;
using System.Collections.Generic;
using System.Text;

namespace Labyrinth
{
    public class Block
    {
        public Walls Walls { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; }

        public Block(int x, int y)
        {
            X = x;
            Y = y;
            Walls = Walls.All;
        }
    }

    [Flags]
    public enum Walls
    {
        none = 0,
        N = 1,
        E = 2,
        W = 4,
        S = 8,
        All = N | E | W | S
    }
}
