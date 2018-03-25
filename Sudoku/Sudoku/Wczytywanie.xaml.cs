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
    /// <summary>
    /// Logika interakcji dla klasy Wczytywanie.xaml
    /// </summary>
    public partial class Wczytywanie : Window
    {
        int[,] buffer, tab;
        Grid siatka;
        public Wczytywanie(Grid siatka, int[,] buffer, int[,] tab)
        {
            InitializeComponent();
            button_cancel.Content = opisyElementowGUI.cancel;
            button_external.Content = opisyElementowGUI.external;
            button_file.Content = opisyElementowGUI.file;
            label_choose.Content = opisyElementowGUI.choose;
            button_cancel.IsCancel = true;
            this.siatka = siatka;
            this.buffer = buffer;
            this.tab = tab;
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_file_Click(object sender, RoutedEventArgs e)
        {
            Zapis wczytywanie = new Zapis(siatka, buffer, tab);
            wczytywanie.Show();
            this.Close();
        }

        private void button_external_Click(object sender, RoutedEventArgs e)
        {
            External ext = new External(siatka, buffer, tab);
            ext.Show();
            this.Close();
        }
    }
}
