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
    /// Interaction logic for ReservationModalWindow.xaml
    /// </summary>
    public partial class ReservationModalWindow : Window
    {
        private MeetingRoom MeetingRoom;
        private DateTime From;
        private DateTime To;
        private Reservation EditedReservation;

        public ReservationModalWindow()
        {
            InitializeComponent();
        }
        public void CreateNewReservation(DateTime date, MeetingRoom room)
        {
            BtnCreateNew.Visibility = Visibility.Visible;
            MeetingRoom = room;
            From = date;
            To = date.AddHours(1);
        }

        public void EditReservation(Reservation reservation)
        {
            EditedReservation = reservation;
            LoadEditingData();
            BtnSave.Visibility = Visibility.Visible;
        }

        //###Data Processing functions
        private void Create()
        {
            From = From.Date.AddHours(int.Parse(ResHourFrom.Text)).AddMinutes(int.Parse(ResMinuteFrom.Text));
            To = To.Date.AddHours(int.Parse(ResHourTo.Text)).AddMinutes(int.Parse(ResMinuteTo.Text));
            string customername = CustomerName.Text;
            int personsnumber = int.Parse(PersonsCount.Text);
            bool? IsVideoConference = VideoConference.IsChecked;
            string note = ReservationNote.Text;
            MeetingRoom.Reservations.Add(new Reservation(From, To, personsnumber, customername, IsVideoConference, note));
        }
        private void Save()
        {
            From = From.Date.AddHours(int.Parse(ResHourFrom.Text)).AddMinutes(int.Parse(ResMinuteFrom.Text));
            To = To.Date.AddHours(int.Parse(ResHourTo.Text)).AddMinutes(int.Parse(ResMinuteTo.Text));
            string customername = CustomerName.Text;
            int personsnumber = int.Parse(PersonsCount.Text);
            bool? IsVideoConference = VideoConference.IsChecked;
            string note = ReservationNote.Text;
            EditedReservation.edit(From, To, personsnumber, customername, IsVideoConference, note);
        }
        private void LoadEditingData()
        {

            
            //Time from
            ResHourFrom.Text = EditedReservation.From.Hour.ToString();
            ResMinuteFrom.Text = EditedReservation.From.Minute.ToString();
            //Time to
            ResHourTo.Text = EditedReservation.To.Hour.ToString();
            ResMinuteTo.Text = EditedReservation.To.Minute.ToString();
            //cutomer
            CustomerName.Text = EditedReservation.customer;
            PersonsCount.Text = EditedReservation.PersonCount.ToString();
            VideoConference.IsChecked = EditedReservation.videoconference;
            ReservationNote.Text = EditedReservation.note;
            From = EditedReservation.From;
            To = EditedReservation.To;
        }

        
        //### Buttons Click Action Part
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            Save();
            this.Close();
        }

        private void BtnCreateNew_Click(object sender, RoutedEventArgs e)
        {
            Create();
            this.Close();
        }
    }
}
