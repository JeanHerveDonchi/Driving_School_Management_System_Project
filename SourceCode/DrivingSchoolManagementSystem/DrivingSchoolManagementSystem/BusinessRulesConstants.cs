using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;

namespace DrivingSchoolManagementSystem
{
    public static class BusinessRulesConstants
    {
        public const int MINIMUM_INSTRUCTOR_AGE = 24;
        public const int MAXIMUM_INSTRUCTOR_AGE = 60;
        public const int SCHOOL_HIRE_START_YEAR = 2015;
        public const int MINIMUM_STUDENT_AGE = 16;
        public const int SCHOOL_ADMISSION_START_YEAR = 2016;
        public const string COUNTRY = "Canada";
        public const int MAX_LESSON_BOOKING_NUM_YEARS_SPAN = 3;
        public const int MIN_LESSON_DURATION = 30;
        public const int MAX_LESSON_DURATION = 180;

        #region Error messages
        public const string BR_ERROR_MESSAGE = "Error messages when creating lesson";
        public const string BR_ERROR_MESSAGE_TIMES = "Error messages in times when creating lesson";
        public const string BR_ERROR_MESSAGE_DURATION_START_END = "Error occured please check your start and end times";
        public const string BR_ERROR_MESSAGE_DURATION_MAX_MINUTES = "Cannot create lesson of less than 30 minutes or more than 3 hours";
        public static string BR_ERROR_MESSAGE_DURATION_TIMES = $"Cannot create lesson out of open hours";

        #endregion

        public static Time OPEN_TIME = new Time(8, 0, "AM");
        public static Time CLOSE_TIME = new Time(5,0,"PM");

        public static readonly HashSet<string> CANADA_VALID_AREA_CODES = new HashSet<string> 
        {
        "403","780","368","587","825","604","250","236","672","778","204","431","584",
        "902","506","428","709","879","782","416","613","519","705","807","905","226",
        "249","289","343","437","548","647","683","742","753","418","514","450","819",
        "263","354","367","438","468","579","581","873","306","474","639","867"
        };
    }
}
