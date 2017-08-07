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
        EasyEncWS.MainService ms = new EasyEncWS.MainService();
        public static string group;
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
            if (string.IsNullOrWhiteSpace(newGroup.Text))
            {
                MessageBox.Show("Please provide an input!");

            }
            else
            {
                bool checkgrp = ms.checkGroup(newGroup.Text);
                if (checkgrp == true)
                {
                    MessageBox.Show(newGroup.Text + " has already been taken!");
                }
                else
                {
                    ms.addGroup(username, newGroup.Text);
                    MessageBox.Show("New Group: " + newGroup.Text + " !");
                    this.Hide();
                    Groups g = new Groups();
                    g.Show();

                }

            }
            //    bool checkgrp = ms.checkGroup(newGroup.Text);
            //if (checkgrp == true)
            //{
            //    MessageBox.Show(newGroup.Text + " has already been taken!");
            //}
            //else
            //{
            //    ms.addGroup(username, newGroup.Text);
            //    MessageBox.Show("New Group: " + newGroup.Text + " !");
            //    this.Hide();
            //    Groups g = new Groups();
            //    g.Show();

            //}

            //using (SqlConnection con = new SqlConnection(constring))
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Groups WHERE GroupName like @newGroup", con))
            //    {
            //        con.Open();
            //        cmd.Parameters.AddWithValue("@newGroup", newGroup.Text);
            //        int groupCount = (int)cmd.ExecuteScalar();
            //        if (groupCount > 0)
            //        {
            //            MessageBox.Show(newGroup.Text + " has already been taken!");
            //        }
            //        else
            //        {
            //            SqlCommand cmd1 = new SqlCommand("INSERT INTO Groups(GroupName) VALUES('" + newGroup.Text + "')", con);
            //            cmd1.ExecuteNonQuery();
            //            SqlCommand cmd2 = new SqlCommand("INSERT INTO UsersGroups(username, GroupName) VALUES('" + username + "' , '" + newGroup.Text + "')", con);
            //            cmd2.ExecuteNonQuery();
            //            MessageBox.Show("New Group: " + newGroup.Text + "!");
            //            this.Hide();
            //            Groups g = new Groups();
            //            g.Show();
                       
            //        }
            //    }
            //}
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserItems ui = myGroups.SelectedItem as UserItems;
            string groupname = ui.group;
            //if (item != null && item.IsSelected)
            //{
                indivGroup ig = new indivGroup(groupname);
                ig.Show();
            //    group = item.ToString();
            //}
            
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
