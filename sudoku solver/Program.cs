namespace sudoku_solver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var table = Parse(".5..83.17...1..4..3.4..56.8....3...9.9.8245....6....7...9....5...729..861.36.72.4");
            PrintTable(table);
            Solver.Solve(table);
        }
        static sbyte[,] Parse(string input)
        {
            var table = new sbyte[9,9];
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    char value = input[i*9+j];
                    if (value != '.') table[i, j] = sbyte.Parse(value.ToString());
                    else table[i, j] = -1;
                }
            }
            return table;
        }
        public static void PrintTable(sbyte[,] table)
        {
            Console.WriteLine("┌───────┬───────┬───────┐");
            for (int i = 0; i < 9; i++)
            {
                Console.Write("│ ");
                for (int j = 0; j < 9; j++)
                {
                    sbyte value = table[i,j];
                    Console.Write(value != -1 ? $"{value}" : " ");
                    if ((j + 1) % 3 == 0) Console.Write(" │ ");
                    else Console.Write(" ");
                }
                Console.WriteLine();
                if (i == 2 || i == 5) Console.WriteLine("├───────┼───────┼───────┤");
            }
            Console.WriteLine("└───────┴───────┴───────┘");
        }
    }
}