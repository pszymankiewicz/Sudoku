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
using System.Windows.Forms;


namespace Sudoku
{
    public partial class Zapis : Window
    {
        public int[,] tab = new int[9, 9];
        Obsluga_Plikow obsluga = new Obsluga_Plikow();
        Grid sudoku;
        int[,] buffer;
        plansza Plansza = new plansza();
        Solver solver = new Solver();

        public Zapis(Grid sudoku, int[,] buffer, int[,] tab)
        {
            InitializeComponent();
            button_browse.Content = opisyElementowGUI.browse;
            button_cancel.Content = opisyElementowGUI.cancel;
            button_cancel.IsCancel = true;
            button_saved.Content = opisyElementowGUI.load;
            textBox_destination.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.sudoku = sudoku;
            this.buffer = buffer;
            this.tab = tab;
        }
        public Zapis(int[,] tab)
        {
            InitializeComponent();
            button_browse.Content = opisyElementowGUI.browse;
            button_cancel.Content = opisyElementowGUI.cancel;
            button_saved.Content = opisyElementowGUI.save;
            button_cancel.IsCancel = true;
            textBox_destination.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.tab = tab;
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_browse_Click(object sender, RoutedEventArgs e)
        {
            if (button_saved.Content.ToString() == opisyElementowGUI.load)
            {
                textBox_destination.Text = obsluga.choose_file();
            }
            else if(button_saved.Content.ToString()==opisyElementowGUI.save)
            {
                textBox_destination.Text = obsluga.choose_folder();
            }
        }

        private void button_saved_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            if (button_saved.Content.ToString() == opisyElementowGUI.load)
            {
                if (obsluga.load_file(textBox_destination.Text))
                {
                    try
                    {
                        Plansza.GenerateFile(sudoku, buffer, obsluga.bufor);
                        for (int i = 0; i < 9; i++)
                            for (int j = 0; j < 9; j++)
                            {
                                if (buffer[i, j] != 0)
                                {
                                    if (!solver.Check(i, j, buffer, buffer[i, j]))
                                    {
                                        valid = false;
                                        Plansza.Hard_Clear(sudoku, buffer);
                                        System.Windows.MessageBox.Show("Invalid input file");
                                    }
                                }
                            }
                        if (valid)
                        {
                            solver.Put_in(tab, buffer);
                            tab = solver.Solve(tab, buffer, 0, 0);
                            this.Close();
                        }
                    }
                    catch (Exception c)
                    {
                        System.Windows.MessageBox.Show("Damaged file\n" + c);
                    }
                }
            }
            else if (button_saved.Content.ToString() == opisyElementowGUI.save)
            {
                obsluga.save_file(textBox_destination.Text, tab);
                this.Close();

            }
            
        }
    }
}
