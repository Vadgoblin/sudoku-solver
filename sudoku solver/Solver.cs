using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace sudoku_solver
{
    internal class Solver
    {
        public static void Solve(sbyte[,] table)
        {
            var possibleValues = new List<sbyte>[9, 9];
            for (int i = 0; i < 81; i++) possibleValues[i / 9, i % 9] = new List<sbyte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            uint round = 0;

            while (true)
            {
                for (int i = 0; i < 9; i++) HorizontalEliminer(table, possibleValues, i);
                for (int i = 0; i < 9; i++) VerticalEliminer(table, possibleValues, i);
                for (int i = 0; i < 9; i++) BlockEliminer(table, possibleValues, i);
                UpdateTable(table, possibleValues);
                round++;
                Console.WriteLine();
                Program.PrintTable(table);
            }
        }
        private static void UpdateTable(sbyte[,] table, List<sbyte>[,] possibleValues)
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j =  0; j < 9; j++)
                {
                    if (table[i,j] == -1 && possibleValues[i,j].Count == 1) table[i,j] = possibleValues[i, j][0];
                }
            }
        }
        private static void HorizontalEliminer(sbyte[,] table, List<sbyte>[,] possibleValues,int line)
        {
            for (int i = 0; i < 9; i++)
            { 
                var value = table[i, line];
                if (value != -1)
                {
                    for(int j = 0; j < 9 && j != i; j++)
                    {
                        possibleValues[i, j].Remove(value);
                    }
                }
            }
        }
        private static void VerticalEliminer(sbyte[,] table, List<sbyte>[,] possibleValues, int row)
        {
            for (int i = 0; i < 9; i++)
            {
                var value = table[row, i];
                if (value != -1)
                {
                    for (int j = 0; j < 9 && j != i; j++)
                    {
                        possibleValues[j,i].Remove(value);
                    }
                }
            }
        }
        private static void BlockEliminer(sbyte[,] table, List<sbyte>[,] possibleValues, int blockId)
        {
            var xoffset = (blockId % 3) * 3;
            var yoffset = (blockId / 3) * 3;

            for (int i = xoffset; i < xoffset+3; i++)
            {
                for (int j = yoffset; j < yoffset+3; j++)
                {
                    var value = table[i, j];
                    if(value != -1)
                    {
                        for(int k = xoffset; k < xoffset+3; k++)
                        {
                            for(int l = yoffset; l < yoffset+3; l++)
                            {
                                possibleValues[k,l].Remove(value);//oh no worst case 91 call
                            }
                        }
                    }
                }
            }
        }
    }
}
