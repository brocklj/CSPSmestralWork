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
        public static bool DataChanged = false;
        Data load = new Data();

        public MainWindow()
        {          
            InitializeComponent();            
            load.LoadData();
           mCentersList.ItemsSource = Data.MeetingCenters;
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
                   
                    DataChanged = true;
                }
            }
        }

        private void BtDeleteMeetingCenter_Click(object sender, RoutedEventArgs e)
        {
            if (mCentersList.SelectedItem != null)
            {
                if (MessageBox.Show(("Do you want really delete? "+(mCentersList.SelectedItem as MeetingCenter).Name), "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string code = (mCentersList.SelectedItem as MeetingCenter).Code;
                    Data.MeetingCenters.Remove(mCentersList.SelectedItem as MeetingCenter);
                    
                    
                 
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


        //NEW EDIT DELETE - Meeting Center click buttons actions
        private void BtNewMeetingCenter_Click(object sender, RoutedEventArgs e)
        {           
                Edit edit = new Edit();
                 edit.NewMeetingCentre();
                edit.ShowDialog();           
        }

        private void BtEditMeetingCenter_Click(object sender, RoutedEventArgs e)
        {
            if (mCentersList.SelectedItem != null)
            {
                Edit edit = new Edit(mCentersList.SelectedItem as MeetingCenter);

                edit.ShowDialog();
            }
        }
        //When window is activate, refresh data in Lists in case of changes.
        private void app_Activated(object sender, EventArgs e)
        {
            mCentersList.Items.Refresh();
            mRoomsList.Items.Refresh();

        }

        // NEW EDIT DELETE - Meeting Rooms click buttons actions
        private void BtNewMeetingRoom_Click(object sender, RoutedEventArgs e)
        {
            if (mCentersList.SelectedItem != null) { 
                Edit edit = new Edit();
            edit.NewMeetingRoom(mCentersList.SelectedItem as MeetingCenter);
            edit.ShowDialog();
            }
        }
        private void BtEditMeetingRoom_Click(object sender, RoutedEventArgs e)
        {
            if (mRoomsList.SelectedItem != null)
            {
                Edit edit = new Edit(mRoomsList.SelectedItem as MeetingRoom);
                edit.ShowDialog();
            }
        }

        private void mRoomsList_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (mRoomsList.SelectedItem != null)
            {
                MRVideoConferenceCheckBox.IsChecked = (mRoomsList.SelectedItem as MeetingRoom).VideoConference;
            }
        }

     
    }
}
