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
    /// Interaction logic for indivGroup.xaml
    /// Add checkbox
    /// Display group name at label
    /// Show users in the group
    /// 
    /// </summary>
    public partial class indivGroup : Window
    {
        const string constring = @"Data Source=CEPHAS\SQLEXPRESS;Initial Catalog = EasyEncryption;Integrated Security = True";
        string group = Groups.group;
        public indivGroup()
        {
            InitializeComponent();
            groupTitle.Content = group;
        }
        private void getGroupMem(string username)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT name FROM Users WHERE GroupName = " + group))
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
                            ui.group = dr["name"].ToString();
                            uilist.Add(ui);
                        }
                        groupMembers.ItemsSource = uilist;
                    }
                }

            }
        }
        private void displayContacts()
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT name FROM Users"))
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
                            uilist.Add(ui);
                        }
                        addContacts.ItemsSource = uilist;
                    }
                }

            }

        }

        private void addMembers_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
