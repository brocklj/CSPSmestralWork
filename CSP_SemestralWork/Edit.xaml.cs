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
        private MeetingRoom room = null;
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
            EditMeetingcenter();
            

        }
        public Edit(MeetingRoom obj)
        {
            InitializeComponent();
            room = obj;
            InitializeComponent();
            EditBtns.Visibility = Visibility.Visible;
            tboxName.Text = room.Name;
            tboxCode.Text = room.Code;
            tboxDesc.Text = room.Description;
            //Extra dunction load elements in windows specific to Meeting center
            EditMeetingcenter();


        }
        private void EditMeetingcenter()
        {
          
            
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
            NewBtns.Visibility = Visibility.Visible;
            newroomcenter = obj;

        }


        private void save()
        {
            if(center != null)
            {
                center.Name = tboxName.Text;
                center.Code = tboxCode.Text;
                center.Description = tboxDesc.Text;
            }
            else if(room != null)
            {
                room.Name = tboxName.Text;
                room.Code = tboxCode.Text;
                room.Description = tboxDesc.Text;
            }
            else if(newcenter != null)
            {
                newcenter.Name = tboxName.Text;
                newcenter.Code = tboxName.Text;
                newcenter.Description = tboxDesc.Text;
                Data.MeetingCenters.Add(newcenter);
              
            }
            else if (newroomcenter != null)
            {
                MeetingRoom NewRoom = new MeetingRoom();
                NewRoom.Name = tboxName.Text;
                NewRoom.Code = tboxCode.Text;
                NewRoom.Description = tboxDesc.Text;
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
