namespace sudoku_solver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //var table = Parse(".5..83.17...1..4..3.4..56.8....3...9.9.8245....6....7...9....5...729..861.36.72.4");
            //var table = Parse(".....82..93.7.6.5..................5.63...72.5..................1.3.9.46..64.....");
            //var table = Parse("31.5.....4.5.2..6..82...1..8..96.....5.342.8.....78..3..8...43..4..8.2.5.....9.18");//A1
            var table = Parse("3.8..21.7.5.9.....2.1.364.8.2...78.9..7.9.2..9.68...7.1.965.7.2.....9.8.5.24..9.1");//A2
            var solution = Solver.Solve(table);
            if (!IsSolvedCorrectly(solution)) throw new Exception();
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
        static bool IsSolvedCorrectly(sbyte[,] table)
        {
            for (int i = 0; i < 9; i++)
            {
                var vertical = new HashSet<sbyte>();
                var horizontal = new HashSet<sbyte>();
                for(int j = 0; j < 9; j++)
                {
                    if (table[i, j] == -1) return false;
                    vertical.Add(table[i, j]);
                    horizontal.Add(table[j, i]);
                }
                for(sbyte j = 1;j <= 9; j++) if(!(vertical.Contains(j) && horizontal.Contains(j))) return false;
            }

            for(int i = 0; i < 9; i += 3)
            {
                for(int j = 0; j < 9; j += 3)
                {
                    var set = new HashSet<sbyte>();
                    for(int k = i; k < i + 3; k++)
                    {
                        for(int l = j; l < j + 3; l++)
                        {
                            set.Add(table[k, l]);
                        }
                    }
                    for (sbyte k = 1; k <= 9; k++) if (!set.Contains(k)) return false;
                }
            }

            return true;
        }
    }
}