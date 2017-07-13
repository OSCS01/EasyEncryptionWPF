using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EasyEncryption
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void AddFiles_Click(object sender, RoutedEventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult)
            {
                System.IO.FileInfo file = new System.IO.FileInfo(FD.FileName);
                addItems(file);
            }
        }
        private void addItems(FileInfo fi)
        {
            string[] row = { fi.Name, "" + fi.Length, fi.FullName };
            ListViewItem lvi = new ListViewItem(row);
            selectedFiles.Items.Add(lvi);
        }
    }
}
