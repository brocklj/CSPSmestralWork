using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //Register and indicator whether data was changed every action changes DataChanged into true
        static bool DataChanged = false;
        Data load = new Data();

        public MainWindow()
        {          
            InitializeComponent();            
            load.LoadData();
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
                   
                OpenFileDialog filedialog = new OpenFileDialog();

                filedialog.DefaultExt = "csv";
                filedialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                bool? result = filedialog.ShowDialog();
                if (result.HasValue && result.Value == true)
                {                
                    Data.MeetingCenters.Clear();
                    Data.MeetingRooms.Clear();
                
                string path = filedialog.FileName;                  
                    Data.ImportData(path);
                DataChanged = true;
                }
            
        }

        private void BtDeleteMeetingRoom_Click(object sender, RoutedEventArgs e)
        {
            if(mRoomsList.SelectedItem != null)
            {
                string Message = @"Do you want really delete: "+(mRoomsList.SelectedItem as MeetingRoom).Name;
                if (MessageBox.Show(Message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MeetingCenter center = (mCentersList.SelectedItem as MeetingCenter);
                    MeetingRoom room = mRoomsList.SelectedItem as MeetingRoom;
                    center.MeetingRooms.Remove(mRoomsList.SelectedItem as MeetingRoom);
                    Data.MeetingRooms[center.Code].Remove(room);
                    DataChanged = true;
                }
            }
        }

        private void BtDeleteMeetingCenter_Click(object sender, RoutedEventArgs e)
        {
            if (mCentersList.SelectedItem != null)
            {
                if (MessageBox.Show("Do you want really delete?{0}", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string code = (mCentersList.SelectedItem as MeetingCenter).Code;
                    Data.MeetingCenters.Remove(mCentersList.SelectedItem as MeetingCenter);
                    
                    Data.MeetingRooms.Remove(code);
                 
                    DataChanged = true;
                }
            }
        }


        private void app_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataChanged == true)
            {
                MessageBoxResult dialog = MessageBox.Show("Do you wanna exit and save changes?", "Save changes", MessageBoxButton.YesNoCancel);
                if (dialog == MessageBoxResult.Yes)
                {
                    load.SaveData();
                }
                else if (dialog == MessageBoxResult.No)
                {

                }
                else if (dialog == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;

                }
            }
        }
            //Close application after presing a button exit in the File menu 
        private void btMenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btMenuSave_Click(object sender, RoutedEventArgs e)
        {
            load.SaveData();
            //Data already saved in file so DataChanged = false
            DataChanged = false;
        }
    }
}
