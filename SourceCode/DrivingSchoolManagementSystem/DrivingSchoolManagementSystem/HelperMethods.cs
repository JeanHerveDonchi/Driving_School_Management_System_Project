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
        public static int CalculateDurationInMinutes(Time startTime, Time endTime)
        {
            // Convert start time to 24-hour format minutes
            int startHour24 = (startTime.Hour == 12 && startTime.AmPm == "PM") ? 12 : startTime.Hour % 12; // Convert 12 AM/PM to 0/12 for calculation
            if (startTime.AmPm == "PM" && startTime.Hour != 12) startHour24 += 12;
            if (startTime.AmPm == "AM" && startTime.Hour == 12) startHour24 = 0;
            int startMinutes = startHour24 * 60 + startTime.Minute;

            // Convert end time to 24-hour format minutes
            int endHour24 = (endTime.Hour == 12 && endTime.AmPm == "PM") ? 12 : endTime.Hour % 12; // Convert 12 AM/PM to 0/12 for calculation
            if (endTime.AmPm == "PM" && endTime.Hour != 12) endHour24 += 12;
            if (endTime.AmPm == "AM" && endTime.Hour == 12) endHour24 = 0;
            int endMinutes = endHour24 * 60 + endTime.Minute;

            // Calculate the difference
            return endMinutes - startMinutes;
        }

        public static decimal CalculateCharge(int minutes)
        {
            const decimal ratePerHour = 60m; // $60 per hour
            decimal ratePerMinute = ratePerHour / 60m; // Calculate the rate per minute
            decimal charge = minutes * ratePerMinute; // Calculate the total charge
            return charge;
        }

        public static void UpdateStudentFees(int lessonDuration, int studentId)
        {
            decimal fees = CalculateCharge(lessonDuration);

            string query = $@"UPDATE Students
                SET DueFees = DueFees + {fees}
                WHERE StudentID = {studentId}";

            int _ = DataAccess.SendData(query);
        }

        //public static void CancelLessonStudentFee(int lessonId, int studentId)
        //{
        //    string queryDuration = $@"SELECT LessonMinutesDuration FROM Lessons WHERE LessonID = {lessonId}";
        //    int durationToDeduce = Convert.ToInt32(DataAccess.GetValue(queryDuration));
        //    decimal feesToDeduce = CalculateCharge(durationToDeduce);
        //    string queryFees = $@"UPDATE Students SET DueFees = DueFees - {feesToDeduce} WHERE StudentID = {studentId}";
        //    int _ = DataAccess.SendData(queryFees);
        //}

        public static int GetOldDuration(int lessonId)
        {
            string sql = $@"SELECT LessonMinutesDuration FROM Lessons WHERE LessonID = {lessonId}";
            object scalar = DataAccess.GetValue(sql);
            if (scalar == DBNull.Value || scalar == null)
            {
                //this would not normally happen
                throw new Exception("Error when getting data -> null data returned");
            }
            else
            {
                //this methos is a bad design, have to refactor
                return Convert.ToInt32(scalar);
            }
        }
    }
}
