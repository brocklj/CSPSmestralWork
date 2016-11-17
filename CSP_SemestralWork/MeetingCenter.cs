using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_SemestralWork
{
    class MeetingCenter
    {   //string length of 2..100 characters
        public string Name { get; set; }
        // string length of 5..50 chatacters can cantain capital letters, small letters and chars: [.,:,_]
        public string Code { get; set;}
        // 
        public string Description { get; set; }
        public List<MeetingRoom> MeetingRooms { get; set; }

        public MeetingCenter(string name, string code, string description)
        {
            Name = name;
            Code = code;
            Description = description;
        }
    

    }
}
