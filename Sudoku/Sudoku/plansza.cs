using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sudoku
{
    class plansza : obsluga_Grid
    {
        public int[,] tab = new int[9, 9];
        public void Rysuj(Grid element)
        {
            for (int i = 0; i < 9; i++)
            {
                element.ColumnDefinitions.Add(new ColumnDefinition());
                element.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < 9; j++)
                {
                    Border border = new Border();
                    border.BorderThickness = new System.Windows.Thickness(1);
                    border.BorderBrush = Brushes.Gray;
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    element.Children.Add(border);

                    Button b = new Button();
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    b.Opacity = 0.7;
                    b.Background = Brushes.RoyalBlue;

                    b.Click += b_Click;
                    element.Children.Add(b);
                }
            }
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(2);
                    border.BorderBrush = Brushes.Black;
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    Grid.SetRowSpan(border, 3);
                    Grid.SetColumnSpan(border, 3);
                    element.Children.Add(border);

                }
            }
            Border ramka = new Border();
            ramka.BorderThickness = new Thickness(4);
            ramka.BorderBrush = Brushes.Black;
            Grid.SetColumn(ramka, 0);
            Grid.SetRow(ramka, 0);
            Grid.SetRowSpan(ramka, 9);
            Grid.SetColumnSpan(ramka, 9);
            element.Children.Add(ramka);
        }
        
        
    }
}
