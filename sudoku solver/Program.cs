namespace sudoku_solver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var table = Parse(".5..83.17...1..4..3.4..56.8....3...9.9.8245....6....7...9....5...729..861.36.72.4");
            //PrintTable(table);
            Solver.Solve(table);

            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
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