using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections;

namespace CSP_SemestralWork
{
    //Class made for testing data in application
    class Data
    {
        public static ObservableCollection<MeetingCenter> MeetingCenters { get; set; } = new ObservableCollection<MeetingCenter>();
        public static List<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>();
      
  
        static Data()
        {
            

        }
        //Method LoadData loads data into collection MeetingCenters nad MeetingRooms
       public static void ImportData(string path)
        {
            StreamReader streamer = new StreamReader(path);
            
            // Helping Dictionary collection, which helps assign centers to rooms through code of each center
            // that is used like a key to item of dictionary type of MeetingCenter.
            Dictionary<string, MeetingCenter> centers = new Dictionary<string, MeetingCenter>();
            //Clear all collections from all data to start import
            MeetingCenters.Clear();
            MeetingRooms.Clear();
            StreamReader data = streamer;
            // helping variable used for switching between type of data of the line in CSV file
            int mode = 0;
            while (!data.EndOfStream)
            {
                var line = data.ReadLine();
                var values = line.Split(',');
                // if
                if (values[0] == "MEETING_CENTRES")
                {
                    mode = 1;
                    continue;                                     
                }
                else if (values[0] == "MEETING_ROOMS")
                {
                    mode = 2;
                    continue;
                }
               //Starting the process of making instataces into partucular collections MeetingCenters and Meetingrooms

                // if mod == 1 then start loading MeetingCenters
                if (mode == 1)
                {   // Values[0] - name, values[1] - code, values[2] - description
                    MeetingCenter center = new MeetingCenter(values[0], values[1], values[2]);
                    MeetingCenters.Add(center);
                    centers.Add(values[1], center);
                                       
                    
                }
                // if mod == 2 then start loading MeetingRooms
                if (mode == 2)
                {
                    //
                    MeetingRooms.Add(new MeetingRoom(values[0], values[1], values[2], Int32.Parse(values[3]), values[4], centers[values[5]]));
                }


            }
             
        }
    }
}