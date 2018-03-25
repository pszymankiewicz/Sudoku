﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Forms;

namespace Sudoku
{
    class Obsluga_Plikow 
    {
        public int[,] tab = new int[9, 9];
        public string buffer_text { get; private set; }
        static string Covertion(int[,] tabb)
        {
            string StringTab = "";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                        StringTab += tabb[i, j].ToString();
                }
            }
            return StringTab;
        }

        public string choose_file()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            return openFile.InitialDirectory + openFile.FileName;   
        }

        public string choose_folder()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
            return folderBrowser.SelectedPath;
        }

        public bool load_file(string path)
        {
            if (!File.Exists(path))
            {
                System.Windows.MessageBox.Show("File does not exist");
                return false;
            }
            else
            {
                if (path.EndsWith(".txt"))
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    byte[] read = new byte[81];
                    fs.Read(read, 0, read.Length);
                    buffer_text = System.Text.Encoding.Default.GetString(read);
                    fs.Close();
                    return true;
                }
                else
                {
                    System.Windows.MessageBox.Show("Chosen file is not a textfile.");
                    return false;
                }
            }
        }

        public void save_file(string path, int[,] array)
        {
            if (!Directory.Exists(path)) System.Windows.MessageBox.Show("Directory does not exist");
            else
            {
                if (!path.EndsWith(".txt"))
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        if (!File.Exists(path + "\\save" + i.ToString() + ".txt"))
                        {
                            path = path + "\\save" + i.ToString() + ".txt";
                            break;
                        }
                    }
                }
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                byte[] save = Encoding.ASCII.GetBytes(Covertion(array));
                fs.Write(save, 0, 81);
                fs.Close();
            }
        }
    }
}
