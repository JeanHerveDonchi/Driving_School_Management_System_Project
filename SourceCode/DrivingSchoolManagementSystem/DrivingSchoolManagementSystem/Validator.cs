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
            string pattern = @"^\d{9}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
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
            string pattern = @"^[a-zA-Z0-9\s,'\-\.#]+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(address);
        }

        #endregion
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

        public static bool ValidateDateOfBirth(DateTime dateOfBirth)
        {
            int currentYear = DateTime.Now.Year;
            int age = currentYear - dateOfBirth.Year;

            // If the birthday hasn't occurred yet this year, subtract one from the age
            if (dateOfBirth > new DateTime(currentYear, dateOfBirth.Month, dateOfBirth.Day))
            {
                age--;
            }

            return age >= BusinessRulesConstants.MINIMUM_INSTRUCTOR_AGE 
                    && age <= BusinessRulesConstants.MAXIMUM_INSTRUCTOR_AGE;
        }
    }
}
