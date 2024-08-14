namespace sudoku_solver
{
    internal class Solver
    {
        public static sbyte[,]? Solve(sbyte[,] table)
        {
            var (emptyCells, possibleValues) = Initialize(table);
            return asd(emptyCells,table,possibleValues);

        }

        private static sbyte[,]? asd(int emptyCells, sbyte[,] table, HashSet<sbyte>[,] possibleValues)
        {
            while (emptyCells > 0)
            {
                int x, y, possibleSolutionCount;
                (x, y, possibleSolutionCount) = GetNextCell(table, possibleValues);

                if (possibleSolutionCount == -1) return null;
                else
                {
                    var values = possibleValues[x, y].ToArray();
                    foreach (var value in values)
                    {
                        emptyCells--;
                        table[x, y] = value;
                        var removePositions = RemovePossibleValue(x, y, value, possibleValues);
                        
                        var result = asd(emptyCells, table, possibleValues);
                        if (result != null) return result;

                        RestorePossibleValue(removePositions, value, possibleValues);
                        table[x, y] = -1;
                        emptyCells++;
                    }
                    return null;
                }
            }
            return table;
        }

        private static (int, HashSet<sbyte>[,]) Initialize(sbyte[,] table)
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
                        RemovePossibleValue(i, j, value, possibleValues);
                    }
                }
            }
            return (emptyCells, possibleValues);
        }

        private static List<(int, int)> RemovePossibleValue(int x, int y,sbyte value, HashSet<sbyte>[,] possibleValues)
        {
            var removLocations = new List<(int, int)>();

            for (int i = 0; i < 9; i++)
            {
                if(possibleValues[i, y].Contains(value))
                {
                    possibleValues[i, y].Remove(value);
                    removLocations.Add((i, y));
                }
                if (possibleValues[x, i].Contains(value))
                {
                    possibleValues[x, i].Remove(value);
                    removLocations.Add((x, i));
                }
            }

            int xoffset = (x / 3) * 3;
            int yoffset = (y / 3) * 3;
            for(int i = xoffset; i < xoffset+3; i++) 
            {
                for(int j = yoffset; j < yoffset+3; j++)
                {
                    if(possibleValues[i, j].Contains(value))
                    {
                        possibleValues[i, j].Remove(value);
                        removLocations.Add((i, j));
                    }
                }
            }

            return removLocations;
        }
        private static void RestorePossibleValue(List<(int, int)> locations, sbyte value, HashSet<sbyte>[,] possibleValues)
        {
            foreach(var (x,y) in locations)
            {
                possibleValues[x,y].Add(value);
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
                    if (table[i, j] == -1)
                    {
                        if (possibleValues[i, j].Count == 1) return (i, j, 1);
                        else if (possibleValues[i, j].Count < minPossibleSolutionCount)
                        {
                            minPossibleSolutionCount = possibleValues[i, j].Count;
                            minX = i;
                            minY = j;
                        }
                    }
                }
            }
            if(minPossibleSolutionCount == 0 || minPossibleSolutionCount == 999) return (-1,-1,-1);
            else return(minX,minY, minPossibleSolutionCount);
        }

        private static bool CmpPossibleValues(HashSet<sbyte>[,] a, HashSet<sbyte>[,]  b) 
        {
            for(int x = 0; x < 9; x++)
            {
                for(int y = 0; y < 9; y++)
                {
                    for(sbyte i = 0; i < 9 ; i++)
                    {
                        if (a[x, y].Contains(i) != b[x, y].Contains(i)) return false;
                    }
                }
            }
            return true;
        }
    }
}
