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
    /// SHOW ID
    /// </summary>
    public partial class Contacts : Window
    {
        const string constring = @"Data Source=CEPHAS\SQLEXPRESS;Initial Catalog = EasyEncryption;Integrated Security = True";
        public Contacts()
        {
            InitializeComponent();
            getContacts();
        }



        //private void LoadContacts()
        //{
        //    SqlConnection con = new SqlConnection(constring);
        //    try
        //    {
        //        con.Open();
        //        string query = "SELECT name FROM Users";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        cmd.ExecuteNonQuery();

        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable("Users");
        //        sda.Fill(dt);
        //        ContactsGrid.ItemsSource = dt.DefaultView;
        //        sda.Update(dt);

        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void getContacts()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT username, name FROM Users"))
                {

                    cmd.Connection = con;
                    cmd.Connection.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        List<UserItems> uilist = new List<UserItems>();

                        foreach (DataRow dr in dt.Rows)
                        {
                            UserItems ui = new UserItems();
                            ui.name = dr["name"].ToString();
                            ui.user = dr["username"].ToString();
                            uilist.Add(ui);
                        }
                        myContacts.ItemsSource = uilist;
                    }
                }

            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
