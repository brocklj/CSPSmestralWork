using System;
using System.Collections.Generic;
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
        public MeetingCenter Center = new MeetingCenter();
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
            Center = meetingcenter;
            Capacity = capacity;
            //Meeting room is added into Meetingcenter instance, where belongs to
            Center.MeetingRooms.Add(this);
            Description = description;
            VideoConference = video!="NO" ? true : false;


        }
     

    
        
    }
}
