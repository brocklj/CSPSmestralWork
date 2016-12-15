using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_SemestralWork
{
    public class Reservation
    {
        public DateTime From { get; set; } = new DateTime();
        public DateTime To { get; set; } = new DateTime();
        public int PersonCount { get; set; }
        public string customer { get; set; }
        public bool? videoconference { get; set; }
        public string note { get; set; }
        public string label { get; set; } 
       


        public Reservation(DateTime attfrom, DateTime atto, int attPersonCount, string attcustomer, bool? attvideoconference, string attnote)
        {
            From = attfrom;
            To = atto;
            PersonCount = attPersonCount;
            customer = attcustomer;
            videoconference = attvideoconference;
            note = attnote;
            label = FromTo();
           
        }
        public string FromTo()
        {
            return string.Format("{0}:{1} - {2}:{3}", From.Hour.ToString(), From.Minute, To.Hour, To.Minute);
        }

        public void edit(DateTime attfrom, DateTime atto, int attPersonCount, string attcustomer, bool? attvideoconference, string attnote)
        {
            From = attfrom;
            To = atto;
            PersonCount = attPersonCount;
            customer = attcustomer;
            videoconference = attvideoconference;
            note = attnote;
            label = FromTo();
        }
    }
}
