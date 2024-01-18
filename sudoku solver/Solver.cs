namespace sudoku_solver
{
    internal class Solver
    {
        public static sbyte[,]? Solve(sbyte[,] table)
        {
            int emptyCells = 81;
            var possibleValues = new HashSet<sbyte>[9, 9];
            for (int i = 0; i < 81; i++) possibleValues[i % 9, i / 9] = new HashSet<sbyte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    var value = table[i, j];
                    if (value != -1)
                    {
                        emptyCells--;
                        possibleValues[i, j] = new HashSet<sbyte>();
                        RemovePossibleValue(i, j, value, possibleValues);
                    }
                }
            }

            while (emptyCells > 0)
            {
                int x, y, possibleSolutionCount;
                (x, y, possibleSolutionCount) = GetNextCell(table, possibleValues);

                if (possibleSolutionCount == 1)
                {
                    var value = possibleValues[x, y].First();
                    table[x, y] = value;
                    emptyCells--;
                    RemovePossibleValue(x, y, value, possibleValues);
                }
                else if (possibleSolutionCount == -1) return null;
                else
                {
                    foreach (var value in possibleValues[x, y])
                    {
                        var tmpTable = (sbyte[,])table.Clone();
                        tmpTable[x, y] = value;
                        var result = Solve(tmpTable);
                        if (result != null) return result;
                    }
                    return null;
                }


                Console.SetCursorPosition(0,0);
                TablePrinter.SimplePrint(table);
                Thread.Sleep(20);
            }

            Console.SetCursorPosition(0, 13);
            return table;
        }
        private static void RemovePossibleValue(int x, int y,sbyte value, HashSet<sbyte>[,] possibleValues)
        {
            for (int i = 0; i < 9; i++)
            {
                possibleValues[i, y].Remove(value);
                possibleValues[x, i].Remove(value);
            }

            int xoffset = (x / 3) * 3;
            int yoffset = (y / 3) * 3;
            for(int i = xoffset; i < xoffset+3; i++) 
            {
                for(int j = yoffset; j < yoffset+3; j++)
                {
                    possibleValues[i,j].Remove(value);
                }
            }
        }
        private static (int,int,int) GetNextCell(sbyte[,] table, HashSet<sbyte>[,] possibleValues)
        {
            int minPossibleSolutionCount = 999;
            int minX = -1;
            int minY = -1;

            for(int j = 0; j < 9; j++)
            {
                for(int i = 0; i < 9; i++)
                {
                    if (possibleValues[i, j].Count == 1) return (i, j,1);
                    else if (table[i, j] == -1 && possibleValues[i, j].Count < minPossibleSolutionCount)
                    {
                        minPossibleSolutionCount = possibleValues[i, j].Count;
                        minX = i;
                        minY = j;
                    }
                }
            }
            if(minPossibleSolutionCount == 0 || minPossibleSolutionCount == 999) return (-1,-1,-1);
            else return(minX,minY, minPossibleSolutionCount);
        }
    }
}
