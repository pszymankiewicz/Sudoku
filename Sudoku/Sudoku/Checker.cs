using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sudoku
{
    class Checker
    {
        public bool Check(int Row, int Column, int[,] tab, int number)
        {
            int i = 0, j = 0, n = 9, m = 9;
            for (; i < n; i++)
            {
                if (i != Row) if (tab[i, Column] == number) return false;
                if (i != Column) if (tab[Row, i] == number) return false;
            }
            if (Row < 3)
            {
                if (Column < 3)
                {
                    i = 0;
                    j = 0;
                    n = 3;
                    m = 3;
                }

                if (Column >= 3 && Column < 6)
                {
                    i = 0;
                    j = 3;
                    n = 3;
                    m = 6;
                }
                if (Column < 9 && Column >= 6)
                {
                    i = 0;
                    j = 6;
                    n = 3;
                    m = 9;
                }
            }
            else if (Row >= 3 && Row < 6)
            {
                if (Column < 3)
                {
                    i = 3;
                    j = 0;
                    n = 6;
                    m = 3;
                }
                if (Column >= 3 && Column < 6)
                {
                    i = 3;
                    j = 3;
                    n = 6;
                    m = 6;
                }
                if (Column >= 6)
                {
                    i = 3;
                    j = 6;
                    n = 6;
                    m = 9;
                }
            }
            else if (Row >= 6 && Row < 9)
            {
                if (Column < 3)
                {
                    i = 6;
                    j = 0;
                    n = 9;
                    m = 3;
                }
                if (Column >= 3 && Column < 6)
                {
                    i = 6;
                    j = 3;
                    n = 9;
                    m = 6;
                }
                if (Column >= 6 && Column < 9)
                {
                    i = 6;
                    j = 6;
                    n = 9;
                    m = 9;
                }
            }
            for (int y = i; y < n; y++)
            {
                for (int z = j; z < m; z++)
                {
                    if (y != Row || z != Column)
                    {
                        //Console.WriteLine("Checking {0} {1} containing value {3} with value {2}", y, z, number, tab[y, z]);
                        if (tab[y, z] == number) return false;
                    }
                }
            }
            return true;
        }

        public bool IfWin(int[,] buffer)
        {
            for(int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    if(!Check(i, j, buffer, buffer[i,j])||buffer[i,j]==0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
