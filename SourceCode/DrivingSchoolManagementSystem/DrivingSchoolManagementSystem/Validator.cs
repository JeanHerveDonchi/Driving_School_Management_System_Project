using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrivingSchoolManagementSystem
{
    public static class Validator
    {
        #region Validate Text Boxes
        public static bool IsNotNullOrWhiteSpace(string value)
        {
            return !string.IsNullOrWhiteSpace(value.Trim());
        }
        public static bool isNumeric(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (int.TryParse(value, out int _)) return true;
            return false; 
        }
        public static bool ValidateName(string firstName)
        { 
            string pattern = @"^[A-Z][a-zA-Z\s'-]*$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(firstName);
        }
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{10}$";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(phoneNumber))
            {
                // Extract the first three digits
                string areaCode = phoneNumber.Substring(0, 3);

                // Check if the area code is in the valid set
                return BusinessRulesConstants.CANADA_VALID_AREA_CODES.Contains(areaCode);
            }

            return false;
        }
        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }
        public static bool ValidateLicenceNumber(string phoneNumber)
        {
            string pattern = @"^\d{8}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
        }
        public static bool ValidateAddress(string address)
        {
            string pattern = @"^\d+\s+[A-Za-z\s]+\s*(?:\d+)?\s*[A-Za-z\s]*(?:\s*[A-Za-z\s]*)?$";
            Regex regex = new Regex(pattern, RegexOptions.Compiled);

            return regex.IsMatch(address);
        }

        #endregion

        #region Instructor Validations

        public static bool ValidateInstructorAge(int age)
        {
            return age >= BusinessRulesConstants.MINIMUM_INSTRUCTOR_AGE
                && age <= BusinessRulesConstants.MAXIMUM_INSTRUCTOR_AGE;
        }
        public static bool ValidateHiredDate(DateTime date)
        {
            return date <= DateTime.Today && date >= new DateTime(
                BusinessRulesConstants.SCHOOL_HIRE_START_YEAR, 1, 1);
        }

        #endregion

        #region Student Validations
        public static bool ValidateStudentAge(int age)
        {
            return age >= BusinessRulesConstants.MINIMUM_STUDENT_AGE;
        }

        public static bool ValidateAdmissionDate(DateTime date)
        {
            return date <= DateTime.Today && date >= new DateTime(
                BusinessRulesConstants.SCHOOL_ADMISSION_START_YEAR, 1, 1);
        }

        #endregion

        #region Lesson Validations

        public static bool ValidateLessonDate(DateTime lessonDate)
        {
            return (lessonDate >= DateTime.Now 
                    && lessonDate <= 
                    new DateTime(
                        DateTime.Now.Year + 
                            BusinessRulesConstants.MAX_LESSON_BOOKING_NUM_YEARS_SPAN,
                        1,
                        1
                        )
                    );
        }

        public static bool ValidateHour(string hour)
        {
            // Check if the hour is a valid integer
            if (int.TryParse(hour, out int hourValue))
            {
                // Check if the hour is within the range of 1 to 12
                if (hourValue >= 1 && hourValue <= 12)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ValidateMinute(string minute)
        {
            // Check if the minute is a valid integer
            if (int.TryParse(minute, out int minuteValue))
            {
                // Check if the minute is within the range of 0 to 59
                if (minuteValue >= 0 && minuteValue <= 59)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ValidateLessonEndTime(Time startTime, Time endTime)
        {
            // Compare the end time with the start time
            if (endTime.CompareTo(startTime) > 0)
            {
                return true; // End time is greater than start time
            }
            else
            {
                return false; // End time is less than or equal to start time
            }
        }
        #endregion

        #region BR Validations

        public static bool ValidateLessonInstructorDateAndTime(int instructorId, DateTime lessonDate, Time startTime, Time endTime, int lessonId = -1)
        {
            // checks all conditions for overlapping times, dates intructors - Lessons (all possible scenarios)

            string lessonIdCondition = lessonId != -1 ? $"AND LessonID != {lessonId}" : "";
            string query = $@"
                SELECT COUNT(*)
                FROM Lessons
                WHERE InstructorID = {instructorId}
                AND CAST(LessonDate AS DATE) = '{lessonDate.Year}-{lessonDate.Month}-{lessonDate.Day}'
                AND (
                    (CAST(LessonStartTime AS TIME(7)) <= '{startTime.ToDBFormatString()}' AND CAST(LessonEndTime AS TIME(7)) > '{startTime.ToDBFormatString()}')
                    OR (CAST(LessonStartTime AS TIME(7)) < '{endTime.ToDBFormatString()}' AND CAST(LessonEndTime AS TIME(7)) >= '{endTime.ToDBFormatString()}')
                    OR (CAST(LessonStartTime AS TIME(7)) >= '{startTime.ToDBFormatString()}' AND CAST(LessonEndTime AS TIME(7)) <= '{endTime.ToDBFormatString()}')
                    OR (CAST(LessonStartTime AS TIME(7)) = '{startTime.ToDBFormatString()}' AND CAST(LessonEndTime AS TIME(7)) = '{endTime.ToDBFormatString()}')
                    OR (CAST(LessonEndTime AS TIME(7)) = '{startTime.ToDBFormatString()}')
                    OR (CAST(LessonStartTime AS TIME(7)) = '{endTime.ToDBFormatString()}')
                )
                {lessonIdCondition}";

            int existingLessonsCount = Convert.ToInt32(DataAccess.GetValue(query));
            return existingLessonsCount == 0;
        }

        public static bool ValidateLessonStudentDate(int studentId, DateTime lessonDate, int lessonId = -1)
        {
            string lessonIdCondition = lessonId != -1 ? $"AND LessonID != {lessonId}" : "";
            string query = $@"
                SELECT COUNT(*)
                FROM Lessons
                WHERE StudentID = {studentId}
                AND CAST(LessonDate AS DATE) = '{lessonDate.Year}-{lessonDate.Month}-{lessonDate.Day}'
                {lessonIdCondition}";

            int existingLessonsCount = Convert.ToInt32(DataAccess.GetValue(query));
            return existingLessonsCount == 0;
        }


        public static string[] GetLessonErrorMessages(int instructorId, int studentId, DateTime lessonDate, Time startTime, Time endTime, int lessonId = -1)
        {
            var errorMessages = new List<string>();

            bool isInstructorAvailable =
                        lessonId != -1 ?
                        ValidateLessonInstructorDateAndTime(instructorId, lessonDate, startTime, endTime) :
                        ValidateLessonInstructorDateAndTime(instructorId, lessonDate, startTime, endTime, lessonId);

            if (!isInstructorAvailable)
            {
                errorMessages.Add("The instructor is not available at the specified time.");
            }

            bool isStudentAvailable =
                lessonId != -1 ?
                        ValidateLessonStudentDate(studentId, lessonDate) :
                        ValidateLessonStudentDate(studentId, lessonDate, lessonId);
            if (!isStudentAvailable)
            {
                errorMessages.Add("The student already has a lesson scheduled on this date.");
            }

            return errorMessages.ToArray();
        }

        public static string[] GetTimesErrorMessages(Time startTime, Time endTime, int durationInMinutes)
        {
            List<string> errors = new();
            if (startTime.CompareTo(BusinessRulesConstants.OPEN_TIME) < 0 ||
                    startTime.CompareTo(BusinessRulesConstants.CLOSE_TIME) > 0)  // less than open time or greater than closing time
            {
                errors.Add(BusinessRulesConstants.BR_ERROR_MESSAGE_DURATION_TIMES);
            }
            if (endTime.CompareTo(BusinessRulesConstants.OPEN_TIME) < 0 ||
                    endTime.CompareTo(BusinessRulesConstants.CLOSE_TIME) > 0)
            {
                errors.Add(BusinessRulesConstants.BR_ERROR_MESSAGE_DURATION_TIMES);
            }
            if (durationInMinutes <= 0)
            {
                errors.Add(BusinessRulesConstants.BR_ERROR_MESSAGE_DURATION_START_END);
            }
            if (durationInMinutes > 0 && (durationInMinutes < BusinessRulesConstants.MIN_LESSON_DURATION
                        || durationInMinutes > BusinessRulesConstants.MAX_LESSON_DURATION))
            {
                errors.Add(BusinessRulesConstants.BR_ERROR_MESSAGE_DURATION_MAX_MINUTES);
            }
            return errors.ToArray();
        }

        #endregion
    }
}
