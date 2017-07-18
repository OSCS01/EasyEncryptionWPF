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

namespace EasyEncryption
{
    /// <summary>
    /// Interaction logic for ViewLog.xaml
    /// </summary>
    public partial class ViewLog : Window
    {
        public ViewLog()
        {
            InitializeComponent();
        }

        public ViewLog(string text)
        {
            InitializeComponent();
            textBox1.Text = text;
        }
    }
}
