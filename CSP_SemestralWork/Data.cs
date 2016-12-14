using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections;
using System.Windows;

namespace CSP_SemestralWork
{
    //Class made for testing data in application
    class Data
    {
        public static ObservableCollection<MeetingCenter> MeetingCenters { get; set; } = new ObservableCollection<MeetingCenter>();
        string FileBackup = "Data.csv";
        static Data()
        {       

        }
        //Method LoadData loads data into collection MeetingCenters nad MeetingRooms
       public static void ImportData(string path)
        {
            StreamReader streamer = null;
            try {
                Dictionary<string, List<MeetingRoom>> MeetingRooms = GetMeetingRooms();
                
                streamer = new StreamReader(path);

                // Helping Dictionary collection, which helps assign centers to rooms through code of each center
                // that is used like a key to item of dictionary type of MeetingCenter.
                Dictionary<string, MeetingCenter> centers = new Dictionary<string, MeetingCenter>();
                //Clear all collections from all data to start import, after import application will ask whether user likes to connect with old data
                // or import and keep only new data.              
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
                        if (MeetingRooms.ContainsKey(values[5]))
                        {
                            MeetingRooms[values[5]].Add(new MeetingRoom(values[0], values[1], values[2], Int32.Parse(values[3]), values[4], centers[values[5]]));
                        }
                        else
                        {
                           MeetingRoom room = new MeetingRoom(values[0], values[1], values[2], Int32.Parse(values[3]), values[4], centers[values[5]]);

                            MeetingRooms.Add(values[5], new List<MeetingRoom>());
                            MeetingRooms[values[5]].Add(room);

                        }
                    }                
            }
            
            }
            catch
            {
                MessageBox.Show("Importing data error");
            }
            finally
            {
                if(streamer != null)
                {
                    streamer.Close();
                }
                
            }
        }
        public void LoadData()
        {
            try
            {
                ImportData(FileBackup);
            }
            catch
            {
                MessageBox.Show("No data loaded, file missing","Warning", MessageBoxButton.OK ,MessageBoxImage.Warning);
            }
            
        }
        // Creates csv backup file
       public void SaveData()
        {

            StreamWriter fs = null;
            try
            {
                Dictionary<string, List<MeetingRoom>> MeetingRooms = GetMeetingRooms();

                fs = new StreamWriter(FileBackup);
                fs.WriteLine("MEETING_CENTRES,,,");
            foreach(MeetingCenter center in MeetingCenters)
                {
                    string line = center.Name+","+center.Code+","+center.Description;
                    fs.WriteLine(line);
                }
                fs.WriteLine("MEETING_ROOMS,,,");
                foreach(var rooms in MeetingRooms)
                {
                    foreach(var room in rooms.Value)
                    {
                        fs.WriteLine("{0},{1},{2},{3},{4},{5}", room.Name, room.Code, room.Description, room.Capacity, room.VideoConference == true ? "YES" : "NO", rooms.Key);

                    }
                }

            }
            catch
            {

            }
            finally
            {
                if(fs != null)
                {
                    fs.Close();
                }
            }
        }
        //Method returns List of MeetingRooms from under hash from each meeting centre
        public static Dictionary<string, List<MeetingRoom>> GetMeetingRooms()
        {
            Dictionary<string, List<MeetingRoom>> Rooms = new Dictionary<string, List<MeetingRoom>>();
            foreach(MeetingCenter Center in Data.MeetingCenters)
            {
                Rooms.Add(Center.Code, new List<MeetingRoom>());
              
                foreach(MeetingRoom Room in Center.MeetingRooms)
                {
                    Rooms[Center.Code].Add(Room);             
                }                
            }

            return Rooms;

        }
        // Method returns a Meeting Centre by its id code
        public static MeetingCenter GetMeetingCenterByCode(string code)
        {
            MeetingCenter Center = new MeetingCenter();

            foreach (MeetingCenter c in Data.MeetingCenters)
            {
                if(c.Code == code)
                {
                    Center = c;
                    break;
                }
                else
                {
                  Center = null;
                }
            }
            return Center;
        }
        public static void SaveReservations()
        {

        }

        public static void LoadReservations()
        {

        }
    }
}