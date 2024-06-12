using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolManagementSystem
{
    public class Time : IComparable<Time>, IEquatable<Time>
    {
        public Time(int hour, int minute, string period)
        {
            Hour = hour;
            Minute = minute;
            AmPm = period;
        }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public string AmPm { get; set; } // "AM" or "PM"

        public override string ToString()
        {
            return $"{Hour:D2}:{Minute:D2} {AmPm}";
        }

        public string ToDBFormatString()
        {
            return $"{this.To24HourFormat():D2}:{Minute:D2}:00";
        }

        public int To24HourFormat()
        {
            int hour24 = (Hour == 12 && AmPm == "PM")? 12 : Hour % 12;
            if (AmPm == "PM" && Hour != 12)
            {
                hour24 += 12;
            }
            if (AmPm == "AM" && Hour == 12)
            {
                hour24 = 0;
            }
            return hour24;
        }
        public int CompareTo(Time other)
        {
            // Convert the current time to 24-hour format
            int thisHour24 = (this.Hour == 12 && this.AmPm == "PM") ? 12 : this.Hour % 12; // Convert 12 AM/PM to 0/12 for calculation
            if (this.AmPm == "PM" && this.Hour != 12) thisHour24 += 12;
            if (this.AmPm == "AM" && this.Hour == 12) thisHour24 = 0;

            // Convert the other time to 24-hour format
            int otherHour24 = (other.Hour == 12 && other.AmPm == "PM") ? 12 : other.Hour % 12; // Convert 12 AM/PM to 0/12 for calculation
            if (other.AmPm == "PM" && other.Hour != 12) otherHour24 += 12;
            if (other.AmPm == "AM" && other.Hour == 12) otherHour24 = 0;

            // Compare the hours
            if (thisHour24 > otherHour24) return 1;
            if (thisHour24 < otherHour24) return -1;

            // If hours are the same, compare the minutes
            if (this.Minute > other.Minute) return 1;
            if (this.Minute < other.Minute) return -1;

            // If both hours and minutes are the same, the times are equal
            return 0;
        }


        // Equatable implementation
        public bool Equals(Time other)
        {
            if (other == null) return false;
            return this.Hour == other.Hour && this.Minute == other.Minute && this.AmPm == other.AmPm;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return Equals(obj as Time);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hour, Minute, AmPm);
        }
    }

}
