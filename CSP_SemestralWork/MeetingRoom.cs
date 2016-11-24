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
        public MeetingCenter Centre = new MeetingCenter();
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
        public void MoveToMeetingCenter(string changecentrecode)
        {
            if (Data.MeetingRooms.ContainsKey(changecentrecode))
            {
                Data.MeetingRooms[changecentrecode].Add(this);
                Data.MeetingRooms[Centre.Code].Remove(this);
            }
            else
            {
                Data.MeetingRooms.Add(changecentrecode, new List<MeetingRoom>());
                Data.MeetingRooms[changecentrecode].Add(this);

            }

            foreach(MeetingCenter c in Data.MeetingCenters)
            {
                if(c.Code == changecentrecode)
                {
                    c.MeetingRooms.Add(this);
                    Centre.MeetingRooms.Remove(this);
                    break;
                }
            }


        }
     

    
        
    }
}
