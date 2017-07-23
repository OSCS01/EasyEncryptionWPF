using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        const string constring = @"Data Source=CEPHAS\SQLEXPRESS;Initial Catalog = EasyEncryption;Integrated Security = True";
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Users where username ='" + LoginField.Text + "' and pass = '" + PassField.Password + "'", constring);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                //Home hm = new Home();
                //hm.Show();
            }
            else
            {
                MessageBox.Show("Please check your username and password");
            }

        }
    }
}
