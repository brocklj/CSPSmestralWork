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
        private MeetingCenter EditCenter = null;
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
            EditCenter = obj;
            InitializeComponent();
            EditBtns.Visibility = Visibility.Visible;
            tboxName.Text = EditCenter.Name;
            tboxCode.Text = EditCenter.Code;
            tboxDesc.Text = EditCenter.Description;
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
        //Edit method - if varible room is included object type MeetingRoom thgroughout constcructor
        private void EditMeetingRoom()
        {
            RoomTextBox.Visibility = Visibility.Visible;
            tboxCapacity.Text = EditRoom.Capacity.ToString();
            MRVideoConferenceCheckBox.IsChecked = EditRoom.VideoConference;
            //Method part prepared for editing function change of Meeting cetentre in a room
            CBoxEditCentre.ItemsSource = Data.MeetingCenters;
            //Varible EditRoom is passed through constructor by selescting meeting room nad pressing edit button in the main window
            CBoxEditCentre.IsEnabled = true;
            CBoxEditCentre.Text = EditRoom.Centre.Code;
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
            RoomTextBox.Visibility = Visibility.Visible;
            NewBtns.Visibility = Visibility.Visible;
            //Code of meeting centre in room detail
            CBoxEditCentre.IsEnabled = false;
            CBoxEditCentre.ItemsSource = Data.MeetingCenters;
            CBoxEditCentre.Text = newroomcenter.Code;




        }


        private void save()
        {
            if(EditCenter != null)
            {
                EditCenter.Name = tboxName.Text;
                EditCenter.Code = tboxCode.Text;
                EditCenter.Description = tboxDesc.Text;
            }
            //EditRoom Saving data
            else if(EditRoom != null)
            {
                EditRoom.Name = tboxName.Text;
                EditRoom.Code = tboxCode.Text;
                EditRoom.Description = tboxDesc.Text;
                EditRoom.VideoConference = MRVideoConferenceCheckBox.IsChecked.ToString() == "True" ? true : false;
                if ((CBoxEditCentre.SelectedItem as MeetingCenter).Code != EditRoom.Centre.Code)
                {
                    EditRoom.MoveToMeetingCenter((CBoxEditCentre.SelectedItem as MeetingCenter).Code);
                }
            }
            // Saving New MeetingCentre
            else if(newcenter != null)
            {
                newcenter.Name = tboxName.Text;
                newcenter.Code = tboxCode.Text;
                newcenter.Description = tboxDesc.Text;
               
                Data.MeetingCenters.Add(newcenter);
              
            }
            // SAving new Room in meeting centre
            else if (newroomcenter != null)
            {
                MeetingRoom NewRoom = new MeetingRoom();
                NewRoom.Name = tboxName.Text;
                NewRoom.Code = tboxCode.Text;
                NewRoom.Description = tboxDesc.Text;
                NewRoom.VideoConference = MRVideoConferenceCheckBox.IsChecked.ToString() == "True" ? true : false;
                CBoxEditCentre.IsEnabled = false;
                NewRoom.Capacity = int.Parse(tboxCapacity.Text);
                NewRoom.Centre = newroomcenter;
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
        {   //Validation varible which indicates, whether form about to save is valid
            bool isvalid = true;
            //Validation of NewCenter and EditingCenter, NewRoom and EditRoom form
            if (newcenter != null || EditCenter != null || newroomcenter !=null || EditRoom != null)
            {
                if(tboxName.Text == "")
                {
                    isvalid = false;
                    tboxName.Background = Brushes.Red;
                }
                else
                {
                    tboxName.Background = Brushes.White;
                }
                
                //newcenter.Code;
                //newcenter.Description;


            }

            if (isvalid == true) { return true; } else { return false; }
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

   

        private void StackPanel_KeyUp(object sender, KeyEventArgs e)
        {
            validate();
        }
    }
}
