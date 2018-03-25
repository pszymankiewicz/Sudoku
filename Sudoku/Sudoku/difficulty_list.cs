using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku
{
    class difficulty_list
    {
        public static List<string> combo = new List<string>();
        public void Fill_Combo(ComboBox difficulty)
        {
            //combo.Add("Choose difficulty");
            combo.Add("Easy");
            combo.Add("Medium");
            combo.Add("Hard");
            foreach(var a in combo)
            {
                difficulty.Items.Add(a);
            }
            //difficulty.SelectedIndex = 0;
        }
    }
}
