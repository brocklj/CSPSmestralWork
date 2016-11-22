using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace CSP_SemestralWork
{
    //Class made for testing data in application
    class TestingData
    {
        public static ObservableCollection<MeetingCenter> MeetingCenters { get; set; } = new ObservableCollection<MeetingCenter>();
        public static List<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>();

        static TestingData()
        {
            MeetingCenters = new ObservableCollection<MeetingCenter>();
            MeetingRooms = new List<MeetingRoom>();
            // Data of intance of classes
            #region Data
            MeetingCenter MC1 = new MeetingCenter("Zabreh", "23ad", "Parukarka");
            MeetingCenter MC2 = new MeetingCenter("Opava", "3ad", "Parukarka");
            MeetingCenter MC3 = new MeetingCenter("Praha", "2wad", "Parukarka");

            MeetingRoom mr1 = new MeetingRoom("1.1","32:sd",10,MC1);
            MeetingRoom mr2 = new MeetingRoom("1.3", "32:sd", 10, MC1);
            MeetingRoom mr3 = new MeetingRoom("1.2", "32:sd", 10, MC2);
            MeetingCenters.Add(MC1);
            MeetingCenters.Add(MC2);
            MeetingCenters.Add(MC3);

            MeetingRooms.Add(mr1);
            MeetingRooms.Add(mr2);
            MeetingRooms.Add(mr3);
            
            #endregion


        }

       public static void LoadData(StreamReader file)
        {
           
        }
    }
}