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

    public partial class Load : Window
    {
        int[,] buffer, tab;
        Grid grid;
        public Load(Grid grid, int[,] buffer, int[,] tab)
        {
            InitializeComponent();
            button_cancel.Content = GUIElementsDescriptions.cancel;
            button_external.Content = GUIElementsDescriptions.external;
            button_file.Content = GUIElementsDescriptions.file;
            label_choose.Content = GUIElementsDescriptions.choose;
            button_cancel.IsCancel = true;
            this.grid = grid;
            this.buffer = buffer;
            this.tab = tab;
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_file_Click(object sender, RoutedEventArgs e)
        {
            Save load = new Save(grid, buffer, tab);
            load.Show();
            this.Close();
        }

        private void button_external_Click(object sender, RoutedEventArgs e)
        {
            External ext = new External(grid, buffer, tab);
            ext.Show();
            this.Close();
        }
    }
}
