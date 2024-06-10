using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolManagementSystem
{
    public class Time
    {
        public Time(int hour, int minute, string period)
        {
            Hour = hour;
            Minute = minute;
            Period = period;
        }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public string Period { get; set; } // "AM" or "PM"

        public override string ToString()
        {
            return $"{Hour:D2}:{Minute:D2} {Period}";
        }

        public string ToDBFormatString()
        {
            return $"{Hour:D2}:{Minute:D2}:00";
        }
    }

}
