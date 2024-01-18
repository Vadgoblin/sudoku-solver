using System.Security.Cryptography.X509Certificates;

namespace sudoku_solver
{
    internal class TablePrinter
    {
        private sbyte[,] lastTable;
        private int lastX, lastY;
        public TablePrinter(sbyte[,] table)
        {
            lastX = -1;
            lastY = -1;

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

            lastTable = (sbyte[,])table.Clone();
        }
        public void Update(sbyte[,] newTable)
        {
            int updatedX, updatedY;
            (updatedX, updatedY) = GetUpdatedCell(newTable);

            if(updatedX != -1 && updatedY != -1)
            {
                if(lastX  != -1 && lastY != -1)
                {
                    SetCursorPosition(lastX, lastY);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(newTable[lastX, lastY]);
                    Console.ResetColor();
                }

                SetCursorPosition(updatedX, updatedY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(newTable[updatedX, updatedY]);
                Console.ResetColor();

                lastTable = (sbyte[,])newTable.Clone();
                lastX = updatedX;
                lastY = updatedY;
            }
        }
        private (int,int) GetUpdatedCell(sbyte[,] newTable)
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (lastTable[i, j] != newTable[i, j]) return (i, j);
                }
            }
            return (-1, -1);
        }
        private static void SetCursorPosition(int x, int y)
        {
            int actualX = (x + 1) * 2 + (x / 3) * 2;
            int actualY = y + 1 + (y / 3);

            Console.SetCursorPosition(actualX, actualY);
        }

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
