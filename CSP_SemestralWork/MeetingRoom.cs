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
        public string Name;
        public string Code;
        public string Description { get; set; }
        public int Capacity;
        public bool VideoConference { get; set; }
        public MeetingCenter Center;
        public MeetingRoom(string name, string code, int capacity)
        {
            Name = name;
            Code = code;
            Capacity = capacity;        
            
        }

        public MeetingRoom(string name, string code, int capacity,MeetingCenter meetingcenter)
        {
            Center = meetingcenter;
            Center.MeetingRooms.Add(new MeetingRoom("ds", "dssds", 2));


        }
        
    }
}
