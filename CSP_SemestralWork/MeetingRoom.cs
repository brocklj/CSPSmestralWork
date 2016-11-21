using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_SemestralWork
{
    class MeetingRoom
    {
        // initila class variables
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool VideoConference { get; set; }
        public MeetingCenter Center = new MeetingCenter();
        public MeetingRoom(string name, string code, int capacity)
        {
            Name = name;
            Code = code;
            Capacity = capacity;

        }

        public MeetingRoom(string name, string code, int capacity, MeetingCenter meetingcenter)
        {
            Name = name;
            Code = code;
            Center = meetingcenter;
            Capacity = capacity;
            Center.MeetingRooms.Add(this);
            Description = "popisek";


        }

    
        
    }
}
