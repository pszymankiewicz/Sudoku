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
        plansza pns = new plansza();
        Grid siatka = new Grid();
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
            pns.Rysuj(siatka);
            Grid.SetColumn(siatka, 1);
            Grid.SetRow(siatka, 0);
            Grid.SetRowSpan(siatka, 2);
            backgrd.Children.Add(siatka);
            nr1.Content = opisyElementowGUI.one;
            nr2.Content = opisyElementowGUI.two;
            nr3.Content = opisyElementowGUI.three;
            nr4.Content = opisyElementowGUI.four;
            nr5.Content = opisyElementowGUI.five;
            nr6.Content = opisyElementowGUI.six;
            nr7.Content = opisyElementowGUI.seven;
            nr8.Content = opisyElementowGUI.eight;
            nr9.Content = opisyElementowGUI.nine;
            button_save.Content = opisyElementowGUI.save;
            button_cancel.Content = opisyElementowGUI.cancel;
            button_clear.Content = opisyElementowGUI.clear;
            button_cancel.IsCancel = true;
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            pns.GenerateExt(siatka, sudoku, buffer);
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
                pns.ButtonContent(siatka, pns.active, buffer, out Row, out Column, out Number);
                pns.tab = buffer;
            }
        }
    }
}
