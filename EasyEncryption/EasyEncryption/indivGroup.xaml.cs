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
    /// Interaction logic for indivGroup.xaml
    /// Add checkbox
    /// Display group name at label
    /// Show users in the group
    /// 
    /// </summary>
    public partial class indivGroup : Window
    {
        EasyEncWS.MainService ms = new EasyEncWS.MainService();
        const string constring = @"Data Source=CEPHAS\SQLEXPRESS;Initial Catalog = EasyEncryption;Integrated Security = True";
        string username = Login.username;
        string group;
        public indivGroup(string groupname)
        {
            InitializeComponent();
            group = groupTitle.ToString();
            getGroupMem(group);
            displayContacts();
        }
        private void getGroupMem(string group)
        {
            string xml = ms.retrieveGroupMem(group);
            StringReader xr = new StringReader(xml);
            DataTable dt = new DataTable();
            dt.ReadXml(xr);
            List<UserItems> uilist = new List<UserItems>();
            foreach (DataRow dr in dt.Rows)
            {
                UserItems ui = new UserItems();
                ui.user = dr["username"].ToString();
                uilist.Add(ui);
            }
            groupMembers.ItemsSource = uilist;

            //using (SqlConnection con = new SqlConnection(constring))
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT username FROM UsersGroups WHERE GroupName = @group"))
            //    {
            //        //cmd.Parameters.AddWithValue("@username", username);
            //        cmd.Parameters.AddWithValue("@group", group);
            //        cmd.Connection = con;
            //        cmd.Connection.Open();
            //        using (SqlDataAdapter sda = new SqlDataAdapter())
            //        {
            //            sda.SelectCommand = cmd;
            //            DataTable dt = new DataTable();
            //            sda.Fill(dt);
            //            List<UserItems> uilist = new List<UserItems>();

            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                UserItems ui = new UserItems();
            //                ui.name = dr["username"].ToString();
            //                uilist.Add(ui);
            //            }
            //            groupMembers.ItemsSource = uilist;
            //        }
            //    }

            //}
        }
        private void displayContacts()
        {
            string xml = ms.retrieveContacts();
            StringReader xr = new StringReader(xml);
            DataTable dt = new DataTable();
            dt.ReadXml(xr);
            List<ContactsItem> cilist = new List<ContactsItem>();
            foreach (DataRow dr in dt.Rows)
            {
                ContactsItem ci = new ContactsItem();
                ci.name = dr["name"].ToString();
                cilist.Add(ci);


            }
            addContacts.ItemsSource = cilist;
            //using (SqlConnection con = new SqlConnection(constring))
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT name FROM Users"))
            //    {
            //        cmd.Connection = con;
            //        cmd.Connection.Open();
            //        using (SqlDataAdapter sda = new SqlDataAdapter())
            //        {
            //            sda.SelectCommand = cmd;
            //            DataTable dt = new DataTable();
            //            sda.Fill(dt);

            //            List<ContactsItem> uilist = new List<ContactsItem>();

            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                ContactsItem ci = new ContactsItem();
            //                ci.name = dr["name"].ToString();
            //                uilist.Add(ci);
            //            }
            //            addContacts.ItemsSource = uilist; 
            //        }
            //    }

            //}

        }


        private void addMembers_Click(object sender, RoutedEventArgs e)
        {
            List<ContactsItem> uilist = (List<ContactsItem>)addContacts.ItemsSource;
            List<string> selectedNames = new List<string>();
            foreach (ContactsItem ci in uilist)
            {
                if (ci.checkbox)
                {
                    selectedNames.Add(ci.name);
                }
            }

            foreach (string s in selectedNames)
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO UsersGroups (username,GroupName) SELECT username, '" + group + "' FROM Users WHERE name = '" + s + "'");
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            getGroupMem(group);
            /*
            List<ContactsItem> uilist = new List<ContactsItem>();
            var SelectedList = new List<string>();
                for(int i = 0; i<addContacts.Items.Count; i++)
            {
                var item = addContacts.Items[i];
                var mycheckbox = addContacts.Columns[1].GetCellContent(item) as CheckBox;
                if((bool)mycheckbox.IsChecked)
                {
                    SelectedList.Add(item.);
                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        foreach (string name in SelectedList)
                        {
                            var cmd = new SqlCommand("INSERT INTO UsersGroups (username,GroupName) SELECT username, '" + group + "' FROM Users WHERE name = '" + name + "'");
                           cmd.ExecuteNonQuery();

                        }
                    }
                }
                
            }*/

        }
    }
}
