namespace sudoku_solver
{
    internal static class TablePrinter
    {
        public static void SimplePrint(sbyte[,] table)
        {
            Console.WriteLine("┌───────┬───────┬───────┐");
            for (int y = 0; y < 9; y++)
            {
                Console.Write("│ ");
                for (int x = 0; x < 9; x++)
                {
                    sbyte value = table[x, y];
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
