using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolManagementSystem
{
    public static class HelperMethods
    {
        public static int CalculateAge(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;

            // If the birthday hasn't occurred yet this year, we subtract one from the age
            if (dob.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public static Time From24HourString(string poorFormatTime)
        {
            var timeParts = poorFormatTime.Split(':');
            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);
            string period = hour >= 12 ? "PM" : "AM";

            // Convert hour to 12-hour format
            if (hour > 12)
            {
                hour -= 12;
            }
            else if (hour == 0)
            {
                hour = 12; // Midnight case
            }

            return new Time(hour,minute,period);
        }

        public static int? GetInteger(this DataRow dataRow, string columnName)
        {
            return dataRow[columnName] != DBNull.Value ? Convert.ToInt32(dataRow[columnName]) : null;
        }
    }
}
