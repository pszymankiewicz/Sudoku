using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sudoku
{

    public partial class External : Window
    {
        Board pns = new Board();
        Grid grid = new Grid();
        Solver solver = new Solver();
        Grid sudoku;
        int[,] buffer, tab;
        public External(Grid sudoku, int[,] buffer, int[,] tab)
        {
            InitializeComponent();
            this.sudoku = sudoku;
            this.buffer = buffer;
            this.tab = tab;
            backgrd.ColumnDefinitions.Add(new ColumnDefinition());
            backgrd.ColumnDefinitions.Add(new ColumnDefinition());
            backgrd.RowDefinitions.Add(new RowDefinition());
            backgrd.RowDefinitions.Add(new RowDefinition());
            pns.Draw(grid);
            Grid.SetColumn(grid, 1);
            Grid.SetRow(grid, 0);
            Grid.SetRowSpan(grid, 2);
            backgrd.Children.Add(grid);
            nr1.Content = GUIElementsDescriptions.one;
            nr2.Content = GUIElementsDescriptions.two;
            nr3.Content = GUIElementsDescriptions.three;
            nr4.Content = GUIElementsDescriptions.four;
            nr5.Content = GUIElementsDescriptions.five;
            nr6.Content = GUIElementsDescriptions.six;
            nr7.Content = GUIElementsDescriptions.seven;
            nr8.Content = GUIElementsDescriptions.eight;
            nr9.Content = GUIElementsDescriptions.nine;
            button_save.Content = GUIElementsDescriptions.save;
            button_cancel.Content = GUIElementsDescriptions.cancel;
            button_clear.Content = GUIElementsDescriptions.clear;
            button_cancel.IsCancel = true;
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            pns.FromExt(grid, sudoku, buffer);
            solver.Put_in(tab, buffer);
            tab = solver.Solve(tab, buffer, 0, 0);
            this.Close();
        }

        private void button_nr_Click(object sender, RoutedEventArgs e)
        {
            if (pns.active != null)
            {
                Button number = sender as Button;
                pns.active.Content = number.Content;
                int Row, Column, Number;
                pns.ButtonContent(grid, pns.active, buffer, out Row, out Column, out Number);
                pns.tab = buffer;
            }
        }
    }
}
