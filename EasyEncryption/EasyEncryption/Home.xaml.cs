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

        public Home()
        {
            InitializeComponent();
            //getMyFiles(username);
            //getNotification(username);
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
                    fil.Add(new FileItem() { Originalfilename = fi.Name, path = fi.FullName, Size = fi.Length });
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
                fi.Originalfilename = dr["Filename"].ToString();
                fi.Size = long.Parse(dr["Size"].ToString());
                fi.shared = dr["sharedGroup"].ToString();
                fi.owner = dr["Owner"].ToString();
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
            bool scanResult;
            if (selectedFiles.ItemsSource == null)
                System.Windows.MessageBox.Show("No selected files!");
            else
            {
                List<FileItem> fil = (List<FileItem>)selectedFiles.ItemsSource;
                foreach (FileItem fi in fil)
                {
                    FileInfo fileinfo = new FileInfo(fi.path);
                    string fileext = fileinfo.Extension;
                    string filename = fileinfo.Name.Substring(0, fileinfo.Name.Length - fileext.Length);
                    

                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                    {
                        string serverpub = ms.getPubkey();
                        rsa.FromXmlString(serverpub);
                        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                        {
                            byte[] key = new byte[32];
                            byte[] IV = new byte[16];
                            rng.GetBytes(key);
                            rng.GetBytes(IV);
                            using (RijndaelManaged aes = new RijndaelManaged())
                            {
                                aes.Mode = CipherMode.CBC;
                                aes.IV = IV;
                                aes.Key = key;
                                using (FileStream fsInput = new FileStream(fi.path, FileMode.Open, FileAccess.Read))
                                {
                                    using (FileStream fsEncrypted = new FileStream(encryptpath + filename + ".ee", FileMode.Create, FileAccess.Write))
                                    {
                                        ICryptoTransform encryptor = aes.CreateEncryptor();
                                        using (CryptoStream cryptostream = new CryptoStream(fsEncrypted, encryptor, CryptoStreamMode.Write))
                                        {
                                            int bytesread;
                                            byte[] buffer = new byte[16384];
                                            while (true)
                                            {
                                                bytesread = fsInput.Read(buffer, 0, 16384);
                                                if (bytesread == 0)
                                                    break;
                                                cryptostream.Write(buffer, 0, bytesread);
                                            }
                                            cryptostream.Close();
                                            byte[] data = getFileData(encryptpath + filename + ".ee");
                                            /*try
                                            {
                                                scanResult = scanFile(encryptpath + filename + ".ee");
                                            }
                                            catch (Exception ex)
                                            {
                                                textBox1.Text = ex.ToString();
                                                break;
                                            }
                                            if(scanResult == false)
                                            {
                                                byte[] data = getFileData(encryptpath + filename + ".ee");
                                                ms.uploadFiles(filename, fi.Size, "MSEC", username, filename, fileext, Convert.ToBase64String(rsa.Encrypt(aes.Key, false)), Convert.ToBase64String(aes.IV), data);
                                            }
                                            else
                                            {
                                                textBox1.Text = "The file you are uploading contains a virus!";
                                                break;
                                            }*/
                                            
                                            ms.uploadFiles(filename, fi.Size, "MSEC", username, filename, fileext, Convert.ToBase64String(rsa.Encrypt(aes.Key, false)), Convert.ToBase64String(aes.IV),data);
                                            selectedFiles.ItemsSource = null;
                                            getMyFiles(username);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
        
        //Don't know if this will work.
        public string scanFile(string filepath)
        {
            string result = "fak";
            Task.Run(async () =>
            {
                var clam = new ClamClient("localhost", 3310);
                var scanResult = await clam.ScanFileOnServerAsync(filepath);  //any file you would like!
                

                switch (scanResult.Result)
                {
                    case ClamScanResults.Clean:
                        Console.WriteLine("The file is clean!");
                        result = "clean";
                        break;
                    case ClamScanResults.VirusDetected:
                        Console.WriteLine("Virus Found!");
                        Console.WriteLine("Virus name: {0}", scanResult.InfectedFiles.First().VirusName);
                        result = "VIRUUUSS";
                        break;
                    case ClamScanResults.Error:
                        Console.WriteLine("Woah an error occured! Error: {0}", scanResult.RawResult);
                        result = scanResult.RawResult;
                        break;
                }
            }).Wait();
            return result;
        }

        

        private void SelectedPage(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                downloadBtn.Visibility = Visibility.Hidden;
                UploadBtn.Visibility = Visibility.Visible;
                ViewLogBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                downloadBtn.Visibility = Visibility.Visible;
                UploadBtn.Visibility = Visibility.Hidden;
                ViewLogBtn.Visibility = Visibility.Visible;
                DeleteBtn.Visibility = Visibility.Visible;
            }
        }

        private void ViewLogBtn_Click(object sender, RoutedEventArgs e)
        {
            FileItem item = (FileItem) myFiles.SelectedItem;
            List<string> fileinfo = new List<string>();
            fileinfo.Add(item.Originalfilename);
            fileinfo.Add(item.owner);
            fileinfo.Add(item.shared);
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
                            string[] fileinfo = ms.Download(username, item.Originalfilename, item.shared, item.owner);
                            byte[] deckey = rsa.Decrypt(Convert.FromBase64String(fileinfo[4]), false);
                            using (RijndaelManaged aes = new RijndaelManaged())
                            {
                                aes.Key = deckey;
                                aes.IV = Convert.FromBase64String(fileinfo[1]);
                                aes.Mode = CipherMode.CBC;

                                string encfilepath = encryptpath + fileinfo[0] + ".ee";
                                string decfilepath = decryptpath + fileinfo[2] + fileinfo[3];

                                byte[] file = Convert.FromBase64String(fileinfo[5]);

                                using (FileStream fsEncrypted = new FileStream(encfilepath, FileMode.Open, FileAccess.Read))
                                {
                                    using (FileStream fsDecrypted = new FileStream(decfilepath, FileMode.Create, FileAccess.Write))
                                    {
                                        ICryptoTransform decryptor = aes.CreateDecryptor();
                                        using (CryptoStream cryptostream = new CryptoStream(fsDecrypted, decryptor, CryptoStreamMode.Write))
                                        {
                                            //int bytesread;
                                            //byte[] buffer = new byte[16384];
                                            cryptostream.Write(file, 0, file.Length);
                                            /*
                                            while (true)
                                            {
                                                bytesread = fsEncrypted.Read(buffer, 0, 16384);
                                                if (bytesread == 0)
                                                    break;
                                                cryptostream.Write(buffer, 0, bytesread);
                                            }
                                            */
                                        }
                                    }
                                } 
                            }

                        }
                    }
                }
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            getMyFiles(username);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = scanFile(@"C:\Users\peter_000\Desktop\test.bat").ToString();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (FileItem item in myFiles.SelectedItems)
                {
                    if (item.owner == username)
                        ms.DeleteFile(item.Originalfilename, item.owner, item.shared, username);
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
    }
}