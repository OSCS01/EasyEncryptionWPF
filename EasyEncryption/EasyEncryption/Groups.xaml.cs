using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Groups.xaml
    /// </summary>
    public partial class Groups : Window
    {
        string username = Login.username;
        const string constring = @"Data Source=CEPHAS\SQLEXPRESS;Initial Catalog = EasyEncryption;Integrated Security = True";
        public Groups()
        {
            InitializeComponent();
            getGroups(username);
        }
        //public DataTable Grouping(string username)
        //{
        //    using (SqlConnection con = new SqlConnection(constring))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SELECT GroupName FROM UsersGroups WHERE username =" + username))
        //        {
        //            cmd.Connection = con;
        //            cmd.Connection.Open();
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                sda.SelectCommand = cmd;
        //                DataTable dt = new DataTable();
        //                sda.Fill(dt);
        //                return dt;
        //            }
        //        }
        //    }
        //}
        private void getGroups(string username)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                //using (SqlCommand cmd = new SqlCommand("SELECT GroupName FROM UsersGroups WHERE username =" + username))
                using (SqlCommand cmd = new SqlCommand("SELECT GroupName FROM UsersGroups WHERE username = @user"))
                {
                    cmd.Parameters.AddWithValue("@user", username);
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
                            ui.group = dr["GroupName"].ToString();
                            uilist.Add(ui);
                        }
                        myGroups.ItemsSource = uilist;
                    }
                }

            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            CreateGroup cg = new CreateGroup();
            cg.Show();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
