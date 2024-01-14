namespace sudoku_solver
{
    internal class Solver
    {
        public static bool Solve(sbyte[,] table)
        {
            //var tp = new TablePrinter(table);
            //Console.ReadLine();

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

            uint round = 0;

            while (emptyCells > 0)
            {
                int x, y, possibleSolution;
                (x, y, possibleSolution) = GetNextCell(table, possibleValues);

                if (possibleSolution == 1)
                {
                    var value = possibleValues[x, y].First();
                    table[x, y] = value;
                    emptyCells--;
                    RemovePossibleValue(x, y, value, possibleValues);
                }
                else if (possibleSolution == -1) return false;
                else
                {
                    foreach (var value in possibleValues[x, y])
                    {
                        var tmpTable = (sbyte[,])table.Clone();
                        tmpTable[x, y] = value;
                        if (Solve(tmpTable))
                        {
                            table[x, y] = value;
                        }
                    }
                }


                round++;
                //tp.Update(table);
                //Console.ReadLine();
                //Thread.Sleep(200);
            }

            //Console.SetCursorPosition(0, 13);
            //if (!IsSolved(table)) throw new Exception();
            return true;
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
        private static void CheckInvalidState(sbyte[,] table, HashSet<sbyte>[,] possibleValues)
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (possibleValues[i, j].Count == 0 && table[i,j] == -1)
                    {
                        throw new Exception("rip");
                    }
                }
            }
        }
        private static (int,int,int) GetNextCell(sbyte[,] table, HashSet<sbyte>[,] possibleValues)
        {
            int minpossibleSolution = 999;
            int minX = -1;
            int minY = -1;

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (possibleValues[i, j].Count == 1) return (i, j,1);
                    else if(minpossibleSolution < possibleValues[i, j].Count)
                    {
                        minpossibleSolution = possibleValues[i, j].Count;
                        minX = i;
                        minY = j;
                    }
                }
            }
            if(minpossibleSolution == 0 || minpossibleSolution == 999) return (-1,-1,-1);
            else return(minX,minY, minpossibleSolution);
        }

        public static bool IsSolved(sbyte[,] table)
        {
            for(int i = 0; i < 9; i++)
            {
                var set = new HashSet<sbyte>();
                for(int j = 0; j < 9; j++)
                {
                    var value = table[i,j];
                    if(value != -1) set.Add(value);
                }
                if(set.Count != 9) return false;
            }

            for(int i = 0; i < 9; i++)
            {
                var set = new HashSet<sbyte>();
                for (int j = 0; j < 9; j++)
                {
                    var value = table[i, j];
                    if (value != -1) set.Add(value);
                }
                if (set.Count != 9) return false;
            }

            for(int i = 0; i < 9; i+=3)
            {
                for(int j = 0; j < 9; j+=3)
                {
                    var set = new HashSet<sbyte>();
                    for (int k = i; k < i+3; k++)
                    {
                        for(int l = j; l < j+3; l++)
                        {
                            var value = table[k, l];
                            if (value != -1) set.Add(value);
                        }
                    }
                    if (set.Count != 9) return false;
                }
            }

            return true;
        }
    }
}
