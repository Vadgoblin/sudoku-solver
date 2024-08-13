using System.Linq;

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

        public static void PrintPossibleValues(HashSet<sbyte>[,] possibe_values)
        {
            Console.Write("┏━━━━━━━┯━━━━━━━┯━━━━━━━┳━━━━━━━┯━━━━━━━┯━━━━━━━┳━━━━━━━┯━━━━━━━┯━━━━━━━┓\n");

            for (sbyte line = 0; line < 9; line++)
            {
                for (sbyte subline = 0; subline < 3; subline++)
                {
                    for (sbyte column = 0; column < 9; column++)
                    {
                        Console.Write("{0} ", (column % 3) == 0 ? "┃" : "│");
                        for (sbyte subcolumn = 0; subcolumn < 3; subcolumn++)
                        {
                            sbyte value = (sbyte)(subline * 3 + subcolumn + 1);
                            if (possibe_values[column, line].Contains(value))
                                Console.Write("{0} ", value);
                            else Console.Write("  ");
                        }
                    }
                    Console.Write("┃\n");
                }
                if (line < 8)
                {
                    if ((line + 1) % 3 == 0) Console.Write("┣━━━━━━━┿━━━━━━━┿━━━━━━━╋━━━━━━━┿━━━━━━━┿━━━━━━━╋━━━━━━━┿━━━━━━━┿━━━━━━━┫\n");
                    else Console.Write("┠───────┼───────┼───────╂───────┼───────┼───────╂───────┼───────┼───────┨\n");
                }

            }

            Console.Write("┗━━━━━━━┷━━━━━━━┷━━━━━━━┻━━━━━━━┷━━━━━━━┷━━━━━━━┻━━━━━━━┷━━━━━━━┷━━━━━━━┛\n");
        }
    }
}
