using nClam;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for GroupSelect.xaml
    /// </summary>
    public partial class GroupSelect : Window
    {
        string user = Login.username;
        EasyEncWS.MainService ms = new EasyEncWS.MainService();
        List<FileInfo> fil;
        string encryptpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EncryptedTest\\";
       
        public GroupSelect(List<FileInfo> info)
        {
            InitializeComponent();
            fil = info;
            addGroups();
        }

        public void addGroups()
        {
            try
            {
                string[] group = ms.getGroups(user);
                foreach (string s in group)
                {
                    grouplist.Items.Add(s);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot connect to server", "Error");
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

        public string scanFile(string filepath)
        {
            string result = "Error!";
            try
            {
                Task.Run(async () =>
                {
                    var clam = new ClamClient("localhost", 3310);
                    var scanResult = await clam.ScanFileOnServerAsync(filepath);


                    switch (scanResult.Result)
                    {
                        case ClamScanResults.Clean:
                            result = "Clean";
                            break;
                        case ClamScanResults.VirusDetected:
                            result = "Malicious file detected, " + scanResult.InfectedFiles.First().VirusName;
                            break;
                        case ClamScanResults.Error:
                            result = "Error!";
                            break;
                    }
                }).Wait();
            }
            catch(Exception e)
            {
                return result;
            }
            return result;
        }

        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (FileInfo fileinfo in fil)
            {
                string scanResult = scanFile(fileinfo.FullName);
                //string scanResult = "Clean";
                if (scanResult.Equals("Clean"))
                {
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
                                using (FileStream fsInput = new FileStream(fileinfo.FullName, FileMode.Open, FileAccess.Read))
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

                                            bool result = ms.uploadFiles(fileinfo.Length, grouplist.SelectedItem as string, user, filename, fileext, Convert.ToBase64String(rsa.Encrypt(aes.Key, false)), Convert.ToBase64String(aes.IV), data);
                                            if (!result)
                                                MessageBox.Show(filename + " already exists!", "Error");
                                            File.Delete(encryptpath + filename + ".ee");
                                            MessageBox.Show(filename + " upload success!", "Success");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (scanResult.Equals("Error!"))
                    System.Windows.Forms.MessageBox.Show("An error has occurred");
                else
                    System.Windows.Forms.MessageBox.Show(scanResult);
            }
            this.Close();
        }
    }
}
