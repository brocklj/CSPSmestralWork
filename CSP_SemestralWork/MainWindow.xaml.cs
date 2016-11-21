using System;
using System.Collections.Generic;
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
            mCentersList.ItemsSource = TestingData.MeetingCenters;
                   
           
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
    }
}
