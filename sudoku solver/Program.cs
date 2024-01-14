using System.Diagnostics;

namespace sudoku_solver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SolveBatch();
            Console.ReadLine();
            return;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var table = Parse(".5..83.17...1..4..3.4..56.8....3...9.9.8245....6....7...9....5...729..861.36.72.4");
            //PrintTable(table);
            Solver.Solve(table);
        }
        static void SolveBatch()
        {
            var sw = new Stopwatch();
            
            foreach(var file in Directory.GetFiles("data/"))
            {
                var elapsedlist = new List<long>();
                Console.Write(file);

                var lines = File.ReadLines(file).ToArray();
                for(int i = 0; i < lines.Length; i++)
                {
                    Console.Title = $"{i}/{lines.Length}";
                    if (!lines[i].StartsWith("#") && lines[i] != "")
                    {
                        var table = Parse(lines[i]);

                        sw.Restart();
                        Solver.Solve(table);
                        var elapsed = sw.ElapsedTicks;
                        elapsedlist.Add(elapsed);
                    }
                }

                double microseconds = (long)elapsedlist.Average() / (double)(TimeSpan.TicksPerMillisecond / 1000);
                Console.WriteLine($" {Math.Round(microseconds,1)} μs");
            }
        }
        static sbyte[,] Parse(string input)
        {
            var table = new sbyte[9,9];
            for(int y = 0; y < 9; y++)
            {
                for(int x = 0; x < 9; x++)
                {
                    char value = input[y * 9 + x];
                    if (value != '.') table[x, y] = sbyte.Parse(value.ToString());
                    else table[x, y] = -1;
                }
            }
            return table;
        }
        public static void PrintTable(sbyte[,] table)
        {
            Console.WriteLine("┌───────┬───────┬───────┐");
            for (int y = 0; y < 9; y++)
            {
                Console.Write("│ ");
                for (int x = 0; x < 9; x++)
                {
                    sbyte value = table[x,y];
                    Console.Write(value != -1 ? $"{value}" : " ");
                    if ((x + 1) % 3 == 0) Console.Write(" │ ");
                    else Console.Write(" ");
                }
                Console.WriteLine();
                if (y == 2 || y == 5) Console.WriteLine("├───────┼───────┼───────┤");
            }
            Console.WriteLine("└───────┴───────┴───────┘");
        }
    }
}