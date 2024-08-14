using System.Text;

namespace sudoku_solver
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1) return -1;
            if (args[0].Length != 81) return -2;

            sbyte[,] table; 
            try
            {
                table = Parse(args[0]);
            }
            catch { return -2; }

            sbyte[,]? solution;
            try
            {
                solution = Solver.Solve(table);
            }
            catch{ return -3; }

            if (solution == null) return 1;
            
            if(!IsSolvedCorrectly(solution)) return -4;

            var sb = new StringBuilder();
            
            for(int i = 0; i < solution.Length; i++)
            {
                sb.Append(solution[i % 9, i / 9]);
            }
            Console.WriteLine(sb.ToString());

            return 0;
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