using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSP_SemestralWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MeetingCenter MeetingCenter = new MeetingCenter();
        public MainWindow()
        {
            InitializeComponent();
           Data.ImportData("ImportData.csv");
            mCentersList.ItemsSource = Data.MeetingCenters;
           


        }

        private void BtNewMeetingCenter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mCentersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void mRoomsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //Action after clicking button btImport open file dialog
        private void btImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog filedialog = new OpenFileDialog();

                filedialog.DefaultExt = "csv";
                filedialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                bool? result = filedialog.ShowDialog();
                if (result.HasValue && result.Value == true)
                {
                    string path = filedialog.FileName;                  
                    Data.ImportData(path);
                }
            }
            catch
            {
                
            }
        } 
    }
}
