using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private MeetingCenter NewCenter = null;
        private MeetingCenter NewRoom_center = null;
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
            FormWindow.Title = "Edit Meeting Centre";
            EditBtns.Visibility = Visibility.Visible;
            tboxName.Text = EditCenter.Name;
            tboxCode.IsEnabled = false;
            tboxCode.Text = EditCenter.Code;
            tboxDesc.Text = EditCenter.Description;
            //Extra dunction load elements in windows specific to Meeting center          
        }
        //Edit MeetingRoom
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
        //Edit method - if varible room is included object type MeetingRoom throughout constructor
        private void EditMeetingRoom()
        {
            FormWindow.Title = "Edit Meeting Room";
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
            FormWindow.Title = "Add New Meeting Centre";
            NewBtns.Visibility = Visibility.Visible;
            NewCenter = new MeetingCenter();

        }
        //Method prepares environmet for a new MeetingCenter
        public void NewMeetingRoom(MeetingCenter obj)
        {
            FormWindow.Title = "Add New Meeting Room";
            NewRoom_center = obj;
            RoomTextBox.Visibility = Visibility.Visible;
            NewBtns.Visibility = Visibility.Visible;
            //Code of meeting centre in room detail
            CBoxEditCentre.IsEnabled = false;
            CBoxEditCentre.ItemsSource = Data.MeetingCenters;
            CBoxEditCentre.Text = NewRoom_center.Code;
        }

        //Method id called to save data
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
                EditRoom.Capacity = int.Parse(tboxCapacity.Text);
                EditRoom.VideoConference = MRVideoConferenceCheckBox.IsChecked.ToString() == "True" ? true : false;
                if ((CBoxEditCentre.SelectedItem as MeetingCenter).Code != EditRoom.Centre.Code)
                {
                    EditRoom.MoveToMeetingCenter((CBoxEditCentre.SelectedItem as MeetingCenter).Code);
                }
            }
            //Saving New MeetingCentre
            else if(NewCenter != null)
            {
                NewCenter.Name = tboxName.Text;
                NewCenter.Code = tboxCode.Text;
                NewCenter.Description = tboxDesc.Text;               
                Data.MeetingCenters.Add(NewCenter);
              
            }
            // Saving new Room in meeting centre
            else if (NewRoom_center != null)
            {
                MeetingRoom NewRoom = new MeetingRoom();
                NewRoom.Name = tboxName.Text;
                NewRoom.Code = tboxCode.Text;
                NewRoom.Description = tboxDesc.Text;
                NewRoom.VideoConference = MRVideoConferenceCheckBox.IsChecked.ToString() == "True" ? true : false;
                CBoxEditCentre.IsEnabled = false;
                try
                {
                    NewRoom.Capacity = int.Parse(tboxCapacity.Text);
                }
                catch
                {
                   MessageBox.Show("Error of capacity input type.");
                }

                NewRoom.Centre = NewRoom_center;
                NewRoom_center.MeetingRooms.Add(NewRoom);
            }
            //When data saved, let main window know to show "Savechages" on exit.
            MainWindow.DataChanged = true;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Validate data before saving
                if (validate())
                {
                    save();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }      
        }

        //This method does validation according to which type of form is used nad wich data are being saved
        private bool validate()
        {   //isvalid - a varible which indicates, whether the form is valid
            bool isvalid = true;

            Regex regex = null;
            Match match = null;
            //Validation of NewCenter and EditingCenter, NewRoom and EditRoom form - their common textboxes
            if (NewCenter != null || EditCenter != null || NewRoom_center !=null || EditRoom != null)
            {
                //Name textbox validation on edit or new center
                regex = new Regex(@"(.){2,200}");
                match = regex.Match(tboxName.Text);
                if (!match.Success)
                {
                    isvalid = false;
                    tboxName.Background = Brushes.LightPink;
                }
                else
                {
                    tboxName.Background = Brushes.White;
                }

                //Code textbox validation on editing or creating center
                 regex = new Regex(@"^(?:[a-zA-Z:._]){5,50}?$");
                 match = regex.Match(tboxCode.Text);
                if (!match.Success)
                {
                    isvalid = false;
                    tboxCode.Background = Brushes.LightPink;
                }
                else
                {
                    tboxCode.Background = Brushes.White;
                }

                //Description textbox validation on edit or new center
                regex = new Regex(@"(.){10,300}");
                match = regex.Match(tboxDesc.Text);
                if (!match.Success)
                {
                    isvalid = false;
                    tboxDesc.Background = Brushes.LightPink;
                }
                else
                {
                    tboxDesc.Background = Brushes.White;
                }

            }
            //**New Meeeting Centre is being created
           if(NewCenter != null) {
           //This Conditions checks duplicity of fille New Centre code
                if (Data.GetMeetingCenterByCode(tboxCode.Text) != null)
                {
                    isvalid = false;
                    tboxCode.Background = Brushes.LightPink;
                }
            
            }
            //**New / Edit a Meeting Room 
            if (NewRoom_center != null || EditRoom != null)
            {
             
                //Capacity textbox validation on edit or  new center
                regex = new Regex(@"^([1-9]|[1-9][0-9]|1[0]{2})$");
                match = regex.Match(tboxCapacity.Text);
                if (!match.Success)
                {
                    isvalid = false;
                    tboxCapacity.Background = Brushes.LightPink;
                }
                else
                {
                    tboxCapacity.Background = Brushes.White;
                }

            }



            //****Result of validation return true or false****
            if (isvalid == true) { return true; } else { return false; }

        }//end of validation function

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
        // If strted typing into form detects changes and validates them
        private void StackPanel_KeyUp(object sender, KeyEventArgs e)
        {
            validate();
        }
    }
}
