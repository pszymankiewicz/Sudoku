using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku
{
    class Solver : Checker
    {
        Random rnd = new Random();
        int bufor;
        List<int> list = new List<int>();
        int[] vector = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int licznik = 0, licznik2 = 0;
        bool goingBack = false;

        public int[,] Solve(int[,] tab, int[,] buffer, int Row, int Column)
        {
            licznik = licznik2;
            if (buffer[Row, Column] != 0)
            {
                if (!goingBack)
                {
                    if (Column < 8) Solve(tab, buffer, Row, Column + 1);
                    else if (Row < 8)
                    {
                        Solve(tab, buffer, Row + 1, 0);
                    }
                }
                else if (Column == 0 && Row != 0)
                {
                    licznik2 = Array.IndexOf(vector, tab[Row - 1, 8]) + 1;

                    Solve(tab, buffer, Row - 1, 8);
                }
                else if (Column != 0)
                {
                    licznik2 = Array.IndexOf(vector, tab[Row, Column - 1]) + 1;
                    Solve(tab, buffer, Row, Column - 1);
                }
            }
            else
            {
                goingBack = false;
                if (Row < 9 && Column < 9)
                {
                    while (licznik < 9)
                    {
                        if (Check(Row, Column, tab, vector[licznik]))
                        {
                            tab[Row, Column] = vector[licznik];
                            break;
                        }
                        else
                        {
                            licznik++;
                        }
                    }
                }
                if (Row == 8 && Column == 8)
                {
                    return tab;
                }
                if (Column != 8 && licznik < 9)
                {
                    licznik2 = 0;
                    Solve(tab, buffer, Row, Column + 1);
                }
                else if (Column != 0 && licznik == 9)
                {
                    licznik2 = Array.IndexOf(vector, tab[Row, Column - 1]) + 1;
                    tab[Row, Column] = 0;
                    goingBack = true;
                    Solve(tab, buffer, Row, Column - 1);
                }
                else if (Column == 0 && licznik == 9)
                {
                    licznik2 = Array.IndexOf(vector, tab[Row - 1, 8]) + 1;
                    tab[Row, Column] = 0;
                    goingBack = true;
                    Solve(tab, buffer, Row - 1, 8);
                }
                else if (Column == 8 && licznik < 9)
                {
                    licznik2 = Array.IndexOf(vector, tab[Row, Column - 1]) + 1;
                    licznik2 = 0;
                    Solve(tab, buffer, Row + 1, 0);
                }
            }
            return tab;
        }

        public int[,] Solve_Empty(int[,] tab, int Row, int Column)
        {
            ListAdd(list);
            while (list.Count != 0)
            {
                while (Row != 9)
                {
                    bufor = list[0];
                    if (Check(Row, Column, tab, bufor))
                    {
                        tab[Row, Column] = bufor;
                        Row++;
                        list.Remove(bufor);
                    }
                    else
                    {
                        ClearColumn(Column, tab);
                        Row = 0;
                        ListClear(list);
                        ListAdd(list);
                    }
                }
                if (Column == 8) { break; }
                if(Column!=9) Solve_Empty(tab, 0, Column + 1);
            }
            return tab;
        }

        public void ClearColumn(int y, int[,] tab)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == y)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        tab[j, i] = 0;
                    }
                    break;
                }
            }
        }

        public void ListAdd(List<int> list)
        {
            int x;
            while (list.Count != 9)
            {
                x = rnd.Next(1, 10);
                if (!list.Contains(x))
                {
                    list.Add(x);
                }
            }
        }

        public void ListClear(List<int> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                lista.Remove(lista[i]);
            }
        }

        public void Put_in(int[,] tab, int[,] buffer)
        {
            for(int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    tab[i, j] = buffer[i, j];
                }
            }
        }

        public bool bufferEmpty(int[,] buffer)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (buffer[i, j] != 0) return false;
            return true;
        }
    }
}
