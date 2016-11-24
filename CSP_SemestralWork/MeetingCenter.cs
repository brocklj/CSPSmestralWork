using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_SemestralWork
{
   public class MeetingCenter
    {   //string length of 2..100 characters
        public string Name { get; set; }
        // string length of 5..50 chatacters can cantain capital letters, small letters and chars: [.,:,_]
        public string Code { get; set;}
        // 
        public string Description { get; set; }
        public ObservableCollection<MeetingRoom> MeetingRooms { get; set; } = new ObservableCollection<MeetingRoom>();
        public MeetingCenter()
        {

        }

        public MeetingCenter(string name, string code, string description)
        {
            Name = name;
            Code = code;
            Description = description;
        }
    

    }
}
