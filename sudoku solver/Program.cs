using System.Diagnostics;

namespace sudoku_solver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Benchmark();
                Console.WriteLine();
            }
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

        static void Benchmark()
        {
            foreach (var file in Directory.GetFiles("data"))
            {
                var tableList = new List<sbyte[,]>();

                var sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null || line.StartsWith("#") || line == "") continue;
                    tableList.Add( Parse(line));
                }

                int count = 0;
                var sw = new Stopwatch();
                sw.Start();
                foreach (var table in tableList)
                {
                    Solver.Solve(table);
                    count++;
                    //Console.Title = $"{file} {count}";
                }
                sw.Stop();

                Console.WriteLine($"{file}: {(sw.Elapsed.TotalMicroseconds / 1000000).ToString("0.000000")}");
            }
        }
    }
}