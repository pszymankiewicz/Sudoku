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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Sudoku
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        plansza Plansza = new plansza();
        public int[,] tab = new int[9, 9];
        public int[,] tab_alt = new int[9, 9];
        public int[,] buffer = new int[9, 9];
        Solver solver = new Solver();
        difficulty_list list = new difficulty_list();
        int differences = 0;

        public MainWindow()
        {
            InitializeComponent();
            Plansza.Rysuj(siatka);
            nr1.Content = opisyElementowGUI.one;
            nr2.Content = opisyElementowGUI.two;
            nr3.Content = opisyElementowGUI.three;
            nr4.Content = opisyElementowGUI.four;
            nr5.Content = opisyElementowGUI.five;
            nr6.Content = opisyElementowGUI.six;
            nr7.Content = opisyElementowGUI.seven;
            nr8.Content = opisyElementowGUI.eight;
            nr9.Content = opisyElementowGUI.nine;
            button_clear.Content = opisyElementowGUI.clear;
            button_save.Content = opisyElementowGUI.save;
            button_load.Content = opisyElementowGUI.load;
            button_hint.Content = opisyElementowGUI.hint;
            combo_difficulty.Text = opisyElementowGUI.difficulty;
            list.Fill_Combo(combo_difficulty);
            button_print.Content = opisyElementowGUI.print;
            tab=solver.Solve_Empty(tab, 0, 0);
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            Zapis zapis = new Zapis(buffer);
            zapis.Show();
        }

        private void button_load_Click(object sender, RoutedEventArgs e)
        {
            Plansza.Hard_Clear(siatka, buffer);
            Wczytywanie wczytaj = new Wczytywanie(siatka, buffer, tab);
            wczytaj.Show();
        }

        private void button_print_Click(object sender, RoutedEventArgs e)
        {
            Plansza.ToPrint(siatka);
            var pd = new PrintDialog();
            Size printSize = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);
            siatka.Measure(printSize);
            siatka.Arrange(new Rect(printSize));
            pd.PrintVisual(siatka, "Sudoku");
            this.Width = this.Width + 1;
            Plansza.Restore(siatka);
        }

        private void button_nr_Click(object sender, RoutedEventArgs e)
        {
            if (Plansza.active != null)
            {
                Button number = sender as Button;
                Plansza.active.Content = number.Content;
                int Row, Column, Number;
                Plansza.ButtonContent(siatka, Plansza.active, buffer, out Row, out Column, out Number);
                if (!solver.Check(Row, Column, buffer, Number))
                {
                    Plansza.active.Opacity = 0.71;
                    Plansza.active.Background = Brushes.Red;
                }
                else
                {
                    if (tab[Row, Column] != buffer[Row, Column]) differences++;
                    Plansza.active.Opacity = 0.7;
                    Plansza.active.Background = Brushes.Violet;
                }
                if (solver.IfWin(buffer))
                {
                    Plansza.Win(siatka);
                    combo_difficulty.SelectedIndex = -1;
                }   
                if(differences>4)
                {
                    try
                    {
                        tab_alt = solver.Solve(tab_alt, buffer, 0, 0);
                        if (solver.IfWin(tab_alt)) solver.Put_in(tab, tab_alt);
                    }
                    catch { }
                    differences = 0;
                }
            }
        }

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            if(!solver.bufferEmpty(buffer)) Plansza.Clear(siatka, buffer);
        }

        private void combo_difficulty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (solver.IfWin(buffer)) solver.Solve_Empty(tab = new int[9, 9], 0, 0); 
            combo_difficulty.IsEditable = false;
            Plansza.Hard_Clear(siatka, buffer);
            Plansza.Fill(siatka, tab, combo_difficulty.SelectedIndex, buffer);

        }

        private void button_hint_Click(object sender, RoutedEventArgs e)
        {
            if (!solver.bufferEmpty(buffer))
            {
                Plansza.Hint(siatka, tab, buffer);
                if (solver.IfWin(buffer)) Plansza.Win(siatka);
            }
        }
    }
}
