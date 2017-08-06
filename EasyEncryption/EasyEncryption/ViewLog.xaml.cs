using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Descending;
        public ViewLog()
        {
            InitializeComponent();
        }

        public ViewLog(List<string> fileinfo)
        {
            InitializeComponent();
            retrieveLogs(fileinfo);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LogView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
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
                fi.Filename = dr["Originalfilename"].ToString();
                fi.Owner = dr["Owner"].ToString();
                fi.Group = dr["sharedGroup"].ToString();
                fi.Date = dr["Date"].ToString();
                fi.Downloaded = dr["UserDownload"].ToString();
                fil.Add(fi);
            }
            LogView.ItemsSource = fil;
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(LogView.ItemsSource);

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
    }
}
