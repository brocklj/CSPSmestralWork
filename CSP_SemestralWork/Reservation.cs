using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_SemestralWork
{
    public class Reservation
    {
        public DateTime From { get; } = new DateTime();
        public DateTime To { get; } = new DateTime();
        public int PersonCount { get; }
        public string customer { get; }
        public bool videoconference { get; }
        public string note { get; }

        public Reservation(DateTime attfrom, DateTime atto, int attPersonCount, string attcustomer, bool attvideoconference, string attnote)
        {
            From = attfrom;
            To = atto;
            PersonCount = attPersonCount;
            customer = attcustomer;
            videoconference = attvideoconference;
            note = attnote;
        }
    }
}
