using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
        EasyEncWS.MainService ms = new EasyEncWS.MainService();
        public static string username;
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

            string username = LoginField.Text;
            string password = PassField.Password;
        //    String s = PassField.Password;
        //    byte[] data = Encoding.UTF8.GetBytes(s);
        //    SHA256Managed alg = new SHA256Managed();
        //    byte[] hash = alg.ComputeHash(data);
        //    string hashString = string.Empty;
        //    foreach (byte x in hash)
        //    {
        //        hashString += String.Format("{0:x2}", x);
        //    }
            
            
        //    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Users where username ='" + LoginField.Text + "' and pass = '" + hashString + "'", constring);
        //    DataTable dt = new DataTable();
        //    sda.Fill(dt);
        //    if (dt.Rows[0][0].ToString() == "1")
        //    {
        //        username = LoginField.Text;
        //        this.Hide();
        //        Home h = new Home();
        //        h.Show();
                
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please check your username and password");
        //    }
            
            

        }
    }
}
