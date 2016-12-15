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


        public ReservationModalWindow()
        {
            InitializeComponent();
        }
        public void CreateNewReservation(DateTime date, MeetingRoom room)
        {
            MeetingRoom = room;
            From = date;
            To = date.AddHours(1);
        }
        

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            MeetingRoom.Reservations.Add(new Reservation(From, To, 1, "Novy", false, "nova"));
        }
    }
}
