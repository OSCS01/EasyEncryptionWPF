using System;
using System.Collections.Generic;
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
    /// Interaction logic for CreateGroup.xaml
    /// </summary>
    public partial class CreateGroup : Window
    {
        string username = Login.username;
        const string constring = @"Data Source=CEPHAS\SQLEXPRESS;Initial Catalog = EasyEncryption;Integrated Security = True";
        public CreateGroup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Groups WHERE GroupName like @newGroup", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@newGroup", newGroup.Text);
                    int groupCount = (int)cmd.ExecuteScalar();
                    if (groupCount > 0)
                    {
                        MessageBox.Show("Piss off");
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO Groups(GroupName) VALUES('" + newGroup.Text + "')", con);
                        cmd1.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("INSERT INTO UsersGroups(username, GroupName) VALUES('" + username + "' , '" + newGroup.Text + "')", con);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("New Group: " + newGroup.Text + "!");
                        this.Close();
                       
                    }

                }

            }
        }
    }
}
