using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ViewLog.xaml
    /// </summary>
   
    public partial class ViewLog : Window
    {
        EasyEncWS.MainService ms = new EasyEncWS.MainService();
        public ViewLog()
        {
            InitializeComponent();
        }

        public ViewLog(List<string> fileinfo)
        {
            InitializeComponent();
            retrieveLogs(fileinfo);
        }

        public void retrieveLogs(List<string> fileinfo)
        {
            string xml = ms.getLogs(fileinfo[0], fileinfo[1], fileinfo[2]);
            DataTable dt = new DataTable();
            StringReader xr = new StringReader(xml);
            dt.ReadXml(xr);
            List<LogItem> fil = new List<LogItem>();
            foreach (DataRow dr in dt.Rows)
            {
                LogItem fi = new LogItem();
                fi.Originalfilename = dr["Originalfilename"].ToString();
                fi.Owner = dr["Owner"].ToString();
                fi.shared = dr["sharedGroup"].ToString();
                fi.Date = dr["Date"].ToString();
                fi.UserDownload = dr["UserDownload"].ToString();
                fil.Add(fi);
            }
            LogView.ItemsSource = fil;
        }

        private void LogView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
