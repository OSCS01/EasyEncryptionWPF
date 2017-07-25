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
    /// Interaction logic for indivGroup.xaml
    /// </summary>
    public partial class indivGroup : Window
    {
        string group = Groups.group;
        public indivGroup()
        {
            InitializeComponent();
            groupTitle.Content = group;
        }
    }
}
