namespace sudoku_solver
{
    internal class Solver
    {
        public static sbyte[,]? Solve(sbyte[,] table)
        {
            var (emptyCells, possibleValues) = Initialise(table);


            return Solve(table, possibleValues, ref emptyCells);
        }

        private static sbyte[,]? Solve(sbyte[,] table, HashSet<sbyte>[,] possibleValues, ref int emptyCells)
        {
            while (emptyCells > 0)
            {
                var (x, y, possibleSolutionCount) = GetNextCell(table, possibleValues);

                if (possibleSolutionCount >= 1)
                {
                    foreach (var value in possibleValues[x, y].ToArray())
                    {
                        table[x, y] = value;
                        emptyCells--;
                        var deletedPossibleValues = RemovePossibleValues(x, y, value, possibleValues);

                        var result = Solve(table, possibleValues, ref emptyCells);
                        if (result != null) return result;

                        emptyCells++;
                        RestoreDeletedPossibleValues(possibleValues, deletedPossibleValues);
                        table[x, y] = -1;
                    }
                    return null;
                }
                else return null;
            }
            return table;
        }

        private static (int, HashSet<sbyte>[,]) Initialise(sbyte[,] table)
        {
            int emptyCells = 81;
            var possibleValues = new HashSet<sbyte>[9, 9];
            for (int i = 0; i < 81; i++) possibleValues[i % 9, i / 9] = new HashSet<sbyte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var value = table[i, j];
                    if (value != -1)
                    {
                        emptyCells--;
                        possibleValues[i, j] = new HashSet<sbyte>();
                        RemovePossibleValues(i, j, value, possibleValues);
                    }
                }
            }

            return (emptyCells, possibleValues);
        }
        private static List<(int, int, sbyte)> RemovePossibleValues(int x, int y,sbyte value, HashSet<sbyte>[,] possibleValues)
        {
            var deletedValues = new List<(int, int, sbyte)>();

            foreach(var v in possibleValues[x, y])
            {
                possibleValues[x,y].Remove(v);
                deletedValues.Add((x, y, v));
            }
            for (int i = 0; i < 9; i++)
            {
                if (possibleValues[i, y].Contains(value))
                {
                    possibleValues[i, y].Remove(value);
                    deletedValues.Add((i, y, value));
                }
                if (possibleValues[x, i].Contains(value))
                {
                    possibleValues[x, i].Remove(value);
                    deletedValues.Add((x,y, value));
                }
                
            }

            int xoffset = (x / 3) * 3;
            int yoffset = (y / 3) * 3;
            for(int i = xoffset; i < xoffset+3; i++) 
            {
                for(int j = yoffset; j < yoffset+3; j++)
                {
                    if (possibleValues[i, j].Contains(value))
                    {
                        possibleValues[i, j].Remove(value);
                        deletedValues.Add((i,j, value));
                    }
                }
            }

            return deletedValues;
        }

        private static void RestoreDeletedPossibleValues(HashSet<sbyte>[,] possibleValues, List<(int, int, sbyte)> deletedPossibleValues)
        {
            foreach(var (x,y, value) in deletedPossibleValues)
            {
                possibleValues[x, y].Add(value);
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
