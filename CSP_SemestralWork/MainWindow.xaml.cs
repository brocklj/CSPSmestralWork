﻿using Microsoft.Win32;
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
            //TabMeetingCentresRooms - Load data         
            mCentersList.ItemsSource = Data.MeetingCenters;

            //TabReservation - Load data
            ComboCentre.ItemsSource = Data.MeetingCenters;
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
            if (mRoomsList.SelectedItem != null)
            {
                string Message = @"Do you want really delete: " + (mRoomsList.SelectedItem as MeetingRoom).Name;
                if (MessageBox.Show(Message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MeetingCenter center = (mCentersList.SelectedItem as MeetingCenter);
                    MeetingRoom room = mRoomsList.SelectedItem as MeetingRoom;
                    center.MeetingRooms.Remove(mRoomsList.SelectedItem as MeetingRoom);
                    DataReload();
                    DataChanged = true;
                }
            }
        }
        // Method wich askes fot confirmation to delete an item
        private void BtDeleteMeetingCenter_Click(object sender, RoutedEventArgs e)
        {
            if (mCentersList.SelectedItem != null)
            {
                if (MessageBox.Show(("Do you want really delete? " + (mCentersList.SelectedItem as MeetingCenter).Name), "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string code = (mCentersList.SelectedItem as MeetingCenter).Code;
                    Data.MeetingCenters.Remove(mCentersList.SelectedItem as MeetingCenter);
                    DataReload();
                    DataChanged = true;
                }
            }
        }

        // While closing app ask for saving changes if any - and save chnages
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
        //When window is activate data are refreshed in the Lists and Boxes datails in case of changes.
        private void app_Activated(object sender, EventArgs e)
        {
            mCentersList.Items.Refresh();
            mRoomsList.Items.Refresh();
            mRoomsList.SelectedItem = null;
            Reservations.Items.Refresh();
            // Display and refresh new list of resevations
            DataReload();


        }

        // NEW  Meeting Room click buttons actions
        private void BtNewMeetingRoom_Click(object sender, RoutedEventArgs e)
        {
            if (mCentersList.SelectedItem != null) {
                Edit edit = new Edit();
                edit.NewMeetingRoom(mCentersList.SelectedItem as MeetingCenter);
                edit.ShowDialog();
            }
        }
        //Launch Editing dialog of meeting room by htting edit button
        private void BtEditMeetingRoom_Click(object sender, RoutedEventArgs e)
        {
            if (mRoomsList.SelectedItem != null)
            {
                Edit edit = new Edit(mRoomsList.SelectedItem as MeetingRoom);
                edit.ShowDialog();
            }
        }

        private void DataReload()
        {
            if (ComboRoom.SelectedItem != null && RezervationDatePicker.SelectedDate != null)
            {
                ReservationShowDetail();
                Reservations.ItemsSource = (ComboRoom.SelectedItem as MeetingRoom).GetReservationsByDate(RezervationDatePicker.SelectedDate.Value);

            }
        }
    

        //------Reservation Tab Features-------
        private void mRoomsList_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void TabReservation_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ComboCentre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboCentre.SelectedItem != null && (ComboCentre.SelectedItem as MeetingCenter).MeetingRooms.Count() != 0)
            {
                ComboRoom.IsEnabled = true;
                ComboRoom.ItemsSource = (ComboCentre.SelectedItem as MeetingCenter).MeetingRooms;
                Reservations.ItemsSource = null;
            }
            BtnNewReservation.IsEnabled = false;

        }

        private void RezervationDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RezervationDatePicker.SelectedDate != null && ComboRoom.SelectedItem != null)
            {
                BtnNewReservation.IsEnabled = true;
                Reservations.ItemsSource = (ComboRoom.SelectedItem as MeetingRoom).GetReservationsByDate(RezervationDatePicker.SelectedDate.Value);
            }
        }
        //Resevation Buttons
       //## New Reservation button
        private void NewReservation_Click(object sender, RoutedEventArgs e)
        {
            if (RezervationDatePicker.SelectedDate != null && ComboRoom.SelectedItem != null)
            {
                MeetingRoom room = (ComboRoom.SelectedItem as MeetingRoom);
                DateTime date = RezervationDatePicker.SelectedDate.Value;
                ReservationModalWindow window = new ReservationModalWindow();
                window.CreateNewReservation(date, room);
                window.ShowDialog();
            }

        }
        //## Edit reservation Button
        private void BtnEditReservation_Click(object sender, RoutedEventArgs e)
        {
            if (Reservations.SelectedItem != null)
            {
                MeetingRoom room = (ComboRoom.SelectedItem as MeetingRoom);
                DateTime date = RezervationDatePicker.SelectedDate.Value;
                ReservationModalWindow window = new ReservationModalWindow();
                window.EditReservation(Reservations.SelectedItem as Reservation);
                window.ShowDialog();
            }

        }



        private void BtnDeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            if (Reservations.SelectedItem != null)
            {
                string Message = @"Do you want really delete reservation?";
                if (MessageBox.Show(Message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Reservation reservation = (Reservations.SelectedItem as Reservation);
                    MeetingRoom room = ComboRoom.SelectedItem as MeetingRoom;
                    room.Reservations.Remove(reservation);
                    ReservationShowDetail();
                    DataChanged = true;
                    DataReload();
                }
            }
        }

        private void ComboRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RezervationDatePicker.SelectedDate != null && ComboRoom.SelectedItem != null)
            {
                BtnNewReservation.IsEnabled = true;
                Reservations.ItemsSource = (ComboRoom.SelectedItem as MeetingRoom).GetReservationsByDate(RezervationDatePicker.SelectedDate.Value);
            }
        }
        
        private void Reservations_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Reservations.SelectedItem != null)
            {
                ReservationShowDetail();
            }

        }
     

        private void ReservationShowDetail() {
           if( Reservations.SelectedItem != null ) { 
            TxtBoxCustomer.Text = (Reservations.SelectedItem as Reservation).customer;
            TxtBoxCustomer.Text = (Reservations.SelectedItem as Reservation).customer;
            DetFromH.Text = (Reservations.SelectedItem as Reservation).From.Hour.ToString();
            DetFromM.Text = (Reservations.SelectedItem as Reservation).From.Minute.ToString();
            DetToH.Text = (Reservations.SelectedItem as Reservation).To.Hour.ToString();
            DetToM.Text = (Reservations.SelectedItem as Reservation).To.Minute.ToString();
            DetPresonsNo.Text = (Reservations.SelectedItem as Reservation).PersonCount.ToString();           
            HasVideoCon.IsChecked = (Reservations.SelectedItem as Reservation).videoconference;
            DetNote.Text = (Reservations.SelectedItem as Reservation).note;
            }
        }

        private void Reservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            if (Reservations.SelectedItem != null)
            {
                BtnEditReservation.IsEnabled = true;
                BtnDeleteReservation.IsEnabled = true;
            }
           
        }

        private void Reservations_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            BtnEditReservation.IsEnabled = false;
        }
    }
}
