using System;
using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    // TODO - Check the DrawMap function and a little bit more on the algorithm
    class Program
    {
        const int size = 10;
        static Block[,] map = new Block[size, size];
        static Stack<Block> _stack = new Stack<Block>();

        static void Main(string[] args)
        {
            Random rnd = new Random();

            for(int y = 0; y < size; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    map[x, y] = new Block(x, y);
                }
            }

            _stack.Push(map[0, 0]);
            
            while(_stack.Count > 0)
            {
                var current = _stack.First();
                current.Visited = true;

                List<Block> potential = new List<Block>();
                if (current.X - 1 >= 0 && !map[current.X - 1, current.Y].Visited)
                    potential.Add(map[current.X - 1, current.Y]);
                if (current.X + 1 < size && !map[current.X + 1, current.Y].Visited)
                    potential.Add(map[current.X + 1, current.Y]);
                if (current.Y - 1 >= 0 && !map[current.X, current.Y - 1].Visited)
                    potential.Add(map[current.X, current.Y - 1]);
                if (current.Y + 1 < size && !map[current.X, current.Y + 1].Visited)
                    potential.Add(map[current.X, current.Y + 1]);

                //Done?
                if (potential.Count == 0)
                {
                    _stack.Pop();
                    continue;
                }
                else
                {
                    var next = potential[rnd.Next(potential.Count)];
                    _stack.Push(next);

                    if (next.X > current.X)
                    {
                        current.Walls &= ~Walls.E;
                        next.Walls &= ~Walls.W;
                    }
                    else if (next.X < current.X)
                    {
                        current.Walls &= ~Walls.W;
                        next.Walls &= ~Walls.E;
                    }
                    else if (next.Y > current.Y)
                    {
                        current.Walls &= ~Walls.S;
                        next.Walls &= ~Walls.N;
                    }
                    else if (next.Y < current.Y)
                    {
                        current.Walls &= ~Walls.N;
                        next.Walls &= ~Walls.S;
                    }
                    DrawMap();
                    Console.WriteLine($"X={current.X} Y={current.Y}");
                    Console.WriteLine($"Next X={next.X} Y={next.Y}");
                    Console.ReadKey();
                }
            }
            DrawMap();
            Console.ReadKey();
        }

        static void DrawMap()
        {
            Console.Clear();
            // Draw map
            for (int y = 0; y < size; y++)
            {
                //Walls
                for (int x = 0; x < size; x++)
                {
                    if (map[x, y].Walls.HasFlag(Walls.N))
                        Console.Write("***");
                    else
                        if (map[x, y].Walls.HasFlag(Walls.W))
                        Console.Write("*  ");
                    else
                        Console.Write("   ");
                }

                Console.WriteLine("*");

                for (int x = 0; x < size; x++)
                {
                    if (map[x, y].Walls.HasFlag(Walls.W))
                    {
                        if (_stack.Any(b => b.X == x && b.Y == y))
                        {
                            Console.Write("*");
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("  ");
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.Write("*  ");
                        }

                    }
                    else
                    {
                        if (_stack.Any(b => b.X == x && b.Y == y))
                        {
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("  ");
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                            Console.Write("   ");
                    }
                }

                Console.WriteLine("*");
            }
            Console.WriteLine(new String('*', size * 3 + 1));

        }
    }
}
