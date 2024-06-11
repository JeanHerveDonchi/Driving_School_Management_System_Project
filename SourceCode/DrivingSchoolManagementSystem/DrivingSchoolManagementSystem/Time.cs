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
            return $"{Hour:D2}:{Minute:D2}:00";
        }

        public int CompareTo(Time other)
        {
            // Compare AM/PM first
            if (this.AmPm != other.AmPm)
            {
                return this.AmPm.CompareTo(other.AmPm);
            }

            // Compare hours
            int hourComparison = this.Hour.CompareTo(other.Hour);
            if (hourComparison != 0)
            {
                return hourComparison;
            }

            // Compare minutes
            return this.Minute.CompareTo(other.Minute);
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
