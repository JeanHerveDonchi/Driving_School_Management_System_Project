using System;
using System.Collections.Generic;
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
    }
}
