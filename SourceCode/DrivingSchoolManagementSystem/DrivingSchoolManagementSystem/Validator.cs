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

    }
}
