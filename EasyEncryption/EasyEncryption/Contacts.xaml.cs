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
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class Contacts : Window
    {
        const string constring = @"Data Source=CEPHAS\SQLEXPRESS;Initial Catalog = EasyEncryption;Integrated Security = True";
        public Contacts()
        {
            InitializeComponent();
            //LoadContacts();
        }



        private void LoadContacts()
        {
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open();
                string query = "SELECT name FROM Users";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Users");
                sda.Fill(dt);
                ContactsGrid.ItemsSource = dt.DefaultView;
                sda.Update(dt);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
