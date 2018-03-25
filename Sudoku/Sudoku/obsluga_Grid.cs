using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;


namespace Sudoku
{
    class obsluga_Grid
    {
        public int[,] tab = new int[9, 9];
        public Button active;
        private int Column, Row;
        Random rand = new Random();

        public void ButtonContent(Grid grid, Button button, int[,] buffer, out int Row, out int Column, out int number)
        {
            Row = Grid.GetRow(button);
            Column = Grid.GetColumn(button);
            number = Convert.ToInt32(button.Content);
            buffer[Row, Column] = number;
        }

        protected void b_Click(object sender, RoutedEventArgs e)
        {
            if (active != null)
            {
                active.Background = Brushes.RoyalBlue;
                if (active.Opacity == 0.71) active.Background = Brushes.Red;
            }
            active = sender as Button;
            Row = Grid.GetRow(active);
            Column = Grid.GetColumn(active);
            active.Background = Brushes.Violet;
        }

        public void Hard_Clear(Grid grid, int[,] buffer)
        {
            int j = 0, k = 0;
            for (int i = 0; i < grid.Children.Count; i++)
            {
                try
                {
                    UIElement e = grid.Children[i];
                    Button button = new Button();
                    if (e.GetType() == button.GetType())
                    {
                        button = e as Button;
                        j = Grid.GetRow(e);
                        k = Grid.GetColumn(e);
                        buffer[j, k] = 0;
                        button.Content = null;
                        button.IsEnabled = true;
                        button.ClearValue(Button.FontWeightProperty);
                    }

                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        public void Clear(Grid grid, int[,] buffer)
        {
            int j = 0, k = 0;
            for (int i = 0; i < grid.Children.Count; i++)
            {
                try
                {
                    UIElement e = grid.Children[i];
                    Button button = new Button();
                    if (e.GetType() == button.GetType())
                    {
                        button = e as Button;
                        j = Grid.GetRow(e);
                        k = Grid.GetColumn(e);
                        
                        if (button.IsEnabled == true)
                        {
                            button.Background = Brushes.RoyalBlue;
                            button.Content = null;
                            buffer[i, j] = 0;
                        }
                    }
                }
                catch { }
            }
        }
        
        public void Fill(Grid grid, int[,] tab, int difficulty_index, int[,] buffer)
        {
            rand.NextDouble();
            int j = 0, k = 0, n = 0;
            if(difficulty_index==0) n= 35;
            else if (difficulty_index == 1) n = 28;
            else if (difficulty_index == 2) n = 17;
            while (n > 0)
            {
                for (int i = 0; i < grid.Children.Count; i++)
                {
                    try
                    {
                        if (n > 0 && Convert.ToInt32(rand.NextDouble()) == 1)
                        {
                            UIElement e = grid.Children[i];
                            Button button = new Button();
                            if (e.GetType() == button.GetType())
                            {
                                button = e as Button;
                                j = Grid.GetRow(e);
                                k = Grid.GetColumn(e);
                                button.Content = tab[j, k];
                                buffer[j, k] = tab[j, k];
                                button.IsEnabled = false;
                                button.FontWeight = FontWeights.Black;
                                button.Opacity = 0.72;
                                n--;
                            }
                        }
                        else if(n==0) break;
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        public void Win(Grid grid)
        {
            int j, k;
            for (int i = 0; i < grid.Children.Count; i++)
            {
                try
                {
                    UIElement e = grid.Children[i];
                    Button button = new Button();
                    if (e.GetType() == button.GetType())
                    {
                        button = e as Button;
                        j = Grid.GetRow(e);
                        k = Grid.GetColumn(e);
                        button.IsEnabled = false;
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
            MessageBox.Show("Congratulations!\nYou WIN!");
        }

        public void ToPrint(Grid grid)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                try
                {
                    UIElement e = grid.Children[i];
                    Button button = new Button();
                    if (e.GetType() == button.GetType())
                    {
                        button = e as Button;
                        button.Background = Brushes.White;
                        button.Foreground = Brushes.Black;
                        button.IsEnabled = true;
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        public void Restore(Grid grid)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                try
                {
                    UIElement e = grid.Children[i];
                    Button button = new Button();
                    if (e.GetType() == button.GetType())
                    {
                        button = e as Button;
                        button.Background = Brushes.RoyalBlue;
                        if (button.Content != null && button.Opacity==0.72) button.IsEnabled = false;
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        public void FromExt(Grid ext, Grid sudoku, int[,] buffer)
        {
            int j = 0, k = 0;
            for (int i = 0; i < ext.Children.Count; i++)
            {
                try
                {
                    UIElement e = ext.Children[i];
                    Button button = new Button();
                    if (e.GetType() == button.GetType())
                    {
                        button = e as Button;
                        if (button.Content != null)
                        {
                            j = Grid.GetRow(e);
                            k = Grid.GetColumn(e);
                            buffer[j, k] = Convert.ToInt32(button.Content);
                            for (int l = 0; l < sudoku.Children.Count; l++)
                            {
                                UIElement f = sudoku.Children[l];
                                if (Grid.GetRow(f) == j && Grid.GetColumn(f) == k && f.GetType() == button.GetType())
                                {
                                    button = f as Button;
                                    button.Content = buffer[j, k];
                                    button.IsEnabled = false;
                                    button.FontWeight = FontWeights.Black;
                                    button.Opacity = 0.72;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        public void FromFile(Grid sudoku, int[,] buffer, string bufor)
        {
            int counter = 0;
            for (int i = 0; i < 9; i++)
            {
                for(int l=0; l<9; l++)
                {
                    buffer[i, l] = int.Parse(bufor[counter].ToString());
                    counter++;
                }
            }
            int j = 0, k = 0;
            for (int i = 0; i < sudoku.Children.Count; i++)
            {
                try
                {
                    UIElement e = sudoku.Children[i];
                    Button button = new Button();
                    if (e.GetType() == button.GetType())
                    {
                        button = e as Button;
                        j = Grid.GetRow(e);
                        k = Grid.GetColumn(e);
                        if (buffer[j,k] != 0)
                        {
                            button = e as Button;
                            button.Content = buffer[j, k];
                            button.FontWeight = FontWeights.Black;
                            button.Opacity = 0.72;
                        }
                    }

                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        public void Hint(Grid sudoku, int[,] tab, int[,] buffer)
        {
            int Row, Column;
            bool waiting = true;
            Button button = new Button();
            while (waiting)
            {

                Row = rand.Next(0, 9);
                Column = rand.Next(0, 9);
                if (buffer[Row, Column] != tab[Row, Column])
                {
                    for (int i = 0; i < sudoku.Children.Count; i++)
                    {
                        UIElement e = sudoku.Children[i];
                        if (Grid.GetRow(e) == Row && Grid.GetColumn(e) == Column && button.GetType() == e.GetType())
                        {
                            button = e as Button;
                            button.Content = tab[Row, Column];
                            buffer[Row, Column] = tab[Row, Column];
                            button.IsEnabled = false;
                            waiting = false;
                            break;
                        }
                    }
                }
            }
        }
    }
}
