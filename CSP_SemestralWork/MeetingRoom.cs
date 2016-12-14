using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_SemestralWork
{
    public class MeetingRoom
    {
        // initila class variables
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool VideoConference { get; set; }
        public MeetingCenter Centre = new MeetingCenter();
        public ObservableCollection<Reservation> Reservations { get; } = new ObservableCollection<Reservation>();
        public MeetingRoom() { }
        public MeetingRoom(string name, string code, string description, int capacity)
        {
            Name = name;
            Code = code;
            Capacity = capacity;
            Description = description;
        }

        public MeetingRoom(string name, string code, string description, int capacity,string video, MeetingCenter meetingcenter)
        {
            Name = name;
            Code = code;
            Centre = meetingcenter;
            Capacity = capacity;
            //Meeting room is added into Meetingcenter instance, where belongs to
            Centre.MeetingRooms.Add(this);
            Description = description;
            VideoConference = video!="NO" ? true : false;


        }
        //Method which gets object type of MeetingCentre from code (string)
        public void MoveToMeetingCenter(string changed_centre_code)
        {
            string NewCode = changed_centre_code;
            string OldCode = Centre.Code;
            MeetingCenter NewCenter = Data.GetMeetingCenterByCode(NewCode);
            MeetingCenter OldCenter = Centre;
            NewCenter.MeetingRooms.Add(this);
            OldCenter.MeetingRooms.Remove(this);
            //Assign new changed meeting center to Class variabe "Center" of type MeetingCenter
            Centre = NewCenter;


        }
        //Method will create a new reservation and Add to a collection in specefic room
        public void CreateReservation(DateTime from, DateTime to, int PersonCount, string customer, bool videoconference, string note)
        {
            Reservations.Add(new Reservation(from, to, PersonCount, customer, videoconference, note));
        }

     //This method will retrun collection of reservation, selected by DateTime type
        public ObservableCollection<Reservation> GetReservationsByDate(DateTime date)
        {
            var Iselected = from res in Reservations
                           where res.From.Date == date.Date
                           select res;
            ObservableCollection<Reservation> selected = new ObservableCollection<Reservation>(Iselected);
            return selected;
        }


    }
}
