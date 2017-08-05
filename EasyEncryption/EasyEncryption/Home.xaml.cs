using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using nClam;
using System.ComponentModel;

namespace EasyEncryption
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {

        EasyEncWS.MainService ms = new EasyEncWS.MainService();
        string encryptpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EncryptedTest\\";
        string username = "Adam";
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Descending;

        public Home()
        {
            InitializeComponent();
            try
            {
                getMyFiles(username);
                getNotification(username);
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(myFiles.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Group", ListSortDirection.Ascending));
                view.Filter = UserFilter;

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Cannot connect to server", "Error");
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(myFiles.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (_lastDirection == ListSortDirection.Ascending)
                        direction = ListSortDirection.Descending;
                    else
                        direction = ListSortDirection.Ascending;

                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void AddFiles_Click(object sender, RoutedEventArgs e)
        {
            var FD = new Microsoft.Win32.OpenFileDialog();
            FD.Multiselect = true;
            FD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (FD.ShowDialog() == true)
            {
                List<FileItem> fil = new List<FileItem>();
                if (selectedFiles.ItemsSource != null)
                    fil = (List<FileItem>)selectedFiles.ItemsSource;
                foreach (string path in FD.FileNames)
                {
                    FileInfo fi = new FileInfo(path);
                    fil.Add(new FileItem() { Filename = fi.Name, path = fi.FullName, Size = fi.Length });
                }
                selectedFiles.ItemsSource = null;
                selectedFiles.ItemsSource = fil;

            }
        }


        private void getMyFiles(string username)
        {
            string xml = ms.retrieve(username);
            StringReader xr = new StringReader(xml);
            DataTable dt = new DataTable();
            dt.ReadXml(xr);
            List<FileItem> fil = new List<FileItem>();

            foreach (DataRow dr in dt.Rows)
            {
                FileItem fi = new FileItem();
                fi.Filename = dr["Filename"].ToString();
                fi.Size = long.Parse(dr["Size"].ToString());
                fi.Group = dr["sharedGroup"].ToString();
                fi.Owner = dr["Owner"].ToString();
                fi.Downloaded = ms.getIsDownloaded(fi.Filename, username, fi.Group);
                fil.Add(fi);
            }
            myFiles.ItemsSource = fil;
        }

        private void getNotification(string username)
        {
            int i = ms.retrieveNotification(username);
            if (i > 0)
                NotificationLbl.Content = "You have " + i + " files not downloaded!";
            else
            {
                NotificationLbl.Visibility = Visibility.Hidden;
                CloseBtn.Visibility = Visibility.Hidden;
            }

        }
        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {

            if (selectedFiles.ItemsSource == null)
                System.Windows.MessageBox.Show("No selected files!");
            else
            {
                List<FileItem> fil = (List<FileItem>)selectedFiles.ItemsSource;
                List<FileInfo> fileinfo = new List<FileInfo>();
                foreach (FileItem fi in fil)
                {
                    FileInfo file = new FileInfo(fi.path);
                    fileinfo.Add(file);
                }
                GroupSelect gs = new GroupSelect(username, fileinfo);
                gs.Show();
                selectedFiles.ItemsSource = null;
            }
        }


        public byte[] getFileData(string filepath)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            byte[] fileData = new byte[fs.Length];
            fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();

            return fileData;
        }


        private void SelectedPage(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                downloadBtn.Visibility = Visibility.Hidden;
                UploadBtn.Visibility = Visibility.Visible;
                ViewLogBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
                RefreshBtn.Visibility = Visibility.Hidden;
                ClearBtn.Visibility = Visibility.Visible;
            }
            else
            {
                downloadBtn.Visibility = Visibility.Visible;
                UploadBtn.Visibility = Visibility.Hidden;
                ViewLogBtn.Visibility = Visibility.Visible;
                DeleteBtn.Visibility = Visibility.Visible;
                RefreshBtn.Visibility = Visibility.Visible;
                ClearBtn.Visibility = Visibility.Hidden;
            }
        }

        private void ViewLogBtn_Click(object sender, RoutedEventArgs e)
        {
            FileItem item = (FileItem)myFiles.SelectedItem;
            List<string> fileinfo = new List<string>();
            fileinfo.Add(item.Filename);
            fileinfo.Add(item.Owner);
            fileinfo.Add(item.Group);
            ViewLog vl = new ViewLog(fileinfo);
            vl.Show();
        }

        private void downloadBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var savedpath = new FolderBrowserDialog())
            {
                if (savedpath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string decryptpath = savedpath.SelectedPath + "\\";
                    CspParameters csp = new CspParameters();
                    csp.KeyContainerName = "MyEEKeys";
                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
                    {
                        foreach (FileItem item in myFiles.SelectedItems)
                        {
                            string[] fileinfo = ms.Download(username, item.Filename, item.Group, item.Owner);
                            byte[] deckey = rsa.Decrypt(Convert.FromBase64String(fileinfo[3]), false);
                            using (RijndaelManaged aes = new RijndaelManaged())
                            {
                                aes.Key = deckey;
                                aes.IV = Convert.FromBase64String(fileinfo[0]);
                                aes.Mode = CipherMode.CBC;

                                string filename = fileinfo[1] + fileinfo[2];

                                string decfilepath = decryptpath + filename;

                                byte[] file = Convert.FromBase64String(fileinfo[4]);

                                using (FileStream fsDecrypted = new FileStream(decfilepath, FileMode.Create, FileAccess.Write))
                                {
                                    ICryptoTransform decryptor = aes.CreateDecryptor();
                                    using (CryptoStream cryptostream = new CryptoStream(fsDecrypted, decryptor, CryptoStreamMode.Write))
                                    {
                                        cryptostream.Write(file, 0, file.Length);
                                    }
                                }
                            }
                        }
                        getMyFiles(username);
                    }
                }
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            getMyFiles(username);
        }


        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (FileItem item in myFiles.SelectedItems)
                {
                    if (item.Owner == username)
                        ms.DeleteFile(item.Filename, item.Owner, item.Group, username);
                    else
                        System.Windows.Forms.MessageBox.Show("You are not the owner of this file!");
                }
                getMyFiles(username);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            NotificationLbl.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;
        }

        private void showGroups_Click(object sender, RoutedEventArgs e)
        {
            Groups group = new Groups();
            group.Show();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedFiles.ItemsSource = null;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as FileItem).Filename.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(myFiles.ItemsSource).Refresh();
        }
    }
}