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
using System.Windows.Shapes;

namespace CSP_SemestralWork
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {   //varibale type of meeting center expecting an object
        private MeetingCenter center = null;
        private MeetingRoom EditRoom = null;
        private MeetingCenter newcenter = null;
        private MeetingCenter newroomcenter = null;
        public Edit()
        {
            InitializeComponent();         


        }
        // when class is instanciated with parameter type MeetingCenter edit window will start
        // object MeetingCenter editing mode
        public Edit(MeetingCenter obj)
        {
            center = obj;
            InitializeComponent();
            EditBtns.Visibility = Visibility.Visible;
            tboxName.Text = center.Name;
            tboxCode.Text = center.Code;
            tboxDesc.Text = center.Description;
            //Extra dunction load elements in windows specific to Meeting center
          
            

        }
        public Edit(MeetingRoom obj)
        {
            InitializeComponent();
            EditRoom = obj;
            InitializeComponent();
            EditBtns.Visibility = Visibility.Visible;
            tboxName.Text = EditRoom.Name;
            tboxCode.Text = EditRoom.Code;
            tboxDesc.Text = EditRoom.Description;
            //Extra function load elements in windows specific to Meeting center
            EditMeetingRoom();


        }
        //Editional method varible room is included obect type MeetingRoom
        private void EditMeetingRoom()
        {
            RoomTextBox.Visibility = Visibility.Visible;
            tboxCapacity.Text = EditRoom.Capacity.ToString();
            MRVideoConferenceCheckBox.IsChecked = EditRoom.VideoConference;
            CBoxEditCentre.ItemsSource = Data.MeetingCenters;

        }

        //Method creates a new MeetingCenter
        public void NewMeetingCentre()
        {
            NewBtns.Visibility = Visibility.Visible;
            newcenter = new MeetingCenter();

        }
        //Method prepares environmet for a new MeetingCenter
        public void NewMeetingRoom(MeetingCenter obj)
        {
            newroomcenter = obj;
            NewBtns.Visibility = Visibility.Visible;
            
            

        }


        private void save()
        {
            if(center != null)
            {
                center.Name = tboxName.Text;
                center.Code = tboxCode.Text;
                center.Description = tboxDesc.Text;
            }
            //EditRoom Saving data
            else if(EditRoom != null)
            {
                EditRoom.Name = tboxName.Text;
                EditRoom.Code = tboxCode.Text;
                EditRoom.Description = tboxDesc.Text;
                EditRoom.VideoConference = MRVideoConferenceCheckBox.IsChecked.ToString() == "True" ? true : false;
                EditRoom.MoveToMeetingCenter(CBoxEditCentre.SelectedItem as MeetingCenter);
            }
            else if(newcenter != null)
            {
                newcenter.Name = tboxName.Text;
                newcenter.Code = tboxCode.Text;
                newcenter.Description = tboxDesc.Text;
                Data.MeetingRooms.Add(newcenter.Code, new List<MeetingRoom>());
                Data.MeetingCenters.Add(newcenter);
              
            }
            else if (newroomcenter != null)
            {
                MeetingRoom NewRoom = new MeetingRoom();
                NewRoom.Name = tboxName.Text;
                NewRoom.Code = tboxCode.Text;
                NewRoom.Description = tboxDesc.Text;
                Data.MeetingRooms[newroomcenter.Code].Add(NewRoom);
                newroomcenter.MeetingRooms.Add(NewRoom);
              
            }
            //When data saved, let main window know to show "Savechages" on exit.
            MainWindow.DataChanged = true;          
         

        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (validate()){
                save();
                this.Close();
            }        
        }
        private bool validate()
        {
            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                save();
                this.Close();
            }
            

        }
    }
}
