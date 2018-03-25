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

        Board board = new Board();
        public int[,] tab = new int[9, 9];
        public int[,] tab_alt = new int[9, 9];
        public int[,] buffer = new int[9, 9];
        Solver solver = new Solver();
        DifficultyList list = new DifficultyList();
        int differences = 0;

        public MainWindow()
        {
            InitializeComponent();
            board.Draw(siatka);
            nr1.Content = GUIElementsDescriptions.one;
            nr2.Content = GUIElementsDescriptions.two;
            nr3.Content = GUIElementsDescriptions.three;
            nr4.Content = GUIElementsDescriptions.four;
            nr5.Content = GUIElementsDescriptions.five;
            nr6.Content = GUIElementsDescriptions.six;
            nr7.Content = GUIElementsDescriptions.seven;
            nr8.Content = GUIElementsDescriptions.eight;
            nr9.Content = GUIElementsDescriptions.nine;
            button_clear.Content = GUIElementsDescriptions.clear;
            button_save.Content = GUIElementsDescriptions.save;
            button_load.Content = GUIElementsDescriptions.load;
            button_hint.Content = GUIElementsDescriptions.hint;
            combo_difficulty.Text = GUIElementsDescriptions.difficulty;
            list.Fill_Combo(combo_difficulty);
            button_print.Content = GUIElementsDescriptions.print;
            tab=solver.Solve_Empty(tab, 0, 0);
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            Save zapis = new Save(buffer);
            zapis.Show();
        }

        private void button_load_Click(object sender, RoutedEventArgs e)
        {
            board.Hard_Clear(siatka, buffer);
            Load wczytaj = new Load(siatka, buffer, tab);
            wczytaj.Show();
        }

        private void button_print_Click(object sender, RoutedEventArgs e)
        {
            board.ToPrint(siatka);
            var pd = new PrintDialog();
            Size printSize = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);
            siatka.Measure(printSize);
            siatka.Arrange(new Rect(printSize));
            pd.PrintVisual(siatka, "Sudoku");
            this.Width = this.Width + 1;
            board.Restore(siatka);
        }

        private void button_nr_Click(object sender, RoutedEventArgs e)
        {
            if (board.active != null)
            {
                Button number = sender as Button;
                board.active.Content = number.Content;
                int Row, Column, Number;
                board.ButtonContent(siatka, board.active, buffer, out Row, out Column, out Number);
                if (!solver.Check(Row, Column, buffer, Number))
                {
                    board.active.Opacity = 0.71;
                    board.active.Background = Brushes.Red;
                }
                else
                {
                    if (tab[Row, Column] != buffer[Row, Column]) differences++;
                    board.active.Opacity = 0.7;
                    board.active.Background = Brushes.Violet;
                }
                if (solver.IfWin(buffer))
                {
                    board.Win(siatka);
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
            if(!solver.bufferEmpty(buffer)) board.Clear(siatka, buffer);
        }

        private void combo_difficulty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (solver.IfWin(buffer)) solver.Solve_Empty(tab = new int[9, 9], 0, 0); 
            combo_difficulty.IsEditable = false;
            board.Hard_Clear(siatka, buffer);
            board.Fill(siatka, tab, combo_difficulty.SelectedIndex, buffer);

        }

        private void button_hint_Click(object sender, RoutedEventArgs e)
        {
            if (!solver.bufferEmpty(buffer))
            {
                board.Hint(siatka, tab, buffer);
                if (solver.IfWin(buffer)) board.Win(siatka);
            }
        }
    }
}
