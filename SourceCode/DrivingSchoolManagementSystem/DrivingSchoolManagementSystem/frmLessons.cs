using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DrivingSchoolManagementSystem
{
    public partial class frmLessons : Form
    {
        #region Private Fields

        int currentLessonId;
        int firstLessonId;
        int lastLessonId;
        int? previousLessonId;
        int? nextLessonId;

        int rowNumber;
        int totalRowCount;

        string[] errorMessages;
        string[] timesErrorMessages;

        private FormState currentState;
        #endregion
        public frmLessons()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void frmLessons_Load(object sender, EventArgs e)
        {
            try
            {
                LoadInstructors();
                LoadStudents();
                LoadCars();
                LoadFirstLesson();
                SetState(FormState.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // update to make sure we have the latest.
                LoadStudents();
                LoadInstructors();
                LoadCars();

                SetState(FormState.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to cancel this lesson", "Are you sure?",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteAssignment();
                    LoadFirstLesson();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentState == FormState.View)
                {
                    SetState(FormState.Edit);

                }
                else
                {
                    if (this.ValidateChildren())
                    {
                        if (currentState == FormState.Add)
                        {
                            CreateLesson();
                            //LoadFirstAssignment();
                        }
                        else
                        {
                            //edit
                            UpdateLesson();
                        }
                        SetState(FormState.View);
                    }
                    else
                    {
                        DisplayError("Please resolve your errors", "");
                    }
                }
                errorMessages = [];
                timesErrorMessages = [];
            }
            //Have to refactor this logic later -> Bad Design///
            catch (Exception ex) when (ex.Message == BusinessRulesConstants.BR_ERROR_MESSAGE_TIMES)
            {
                DisplayError($"Could not complete operation because of the following: \n" +
                    $"{string.Join(";\n", timesErrorMessages)}", "Could not complete operation");
                return;
            }
            catch (Exception ex) when (ex.Message == BusinessRulesConstants.BR_ERROR_MESSAGE)
            {
                DisplayError($"Could not complete operation because of the following: \n" +
                    $"{string.Join(";\n", errorMessages)}", "Could not complete operation");
                return;
            }
            catch (Exception ex) when (ex.Message == "Duplicate rows")
            {
                DisplayError("Cannot insert because an entry with these " +
                    "values exist already", "Cannot insert entry");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                SetState(FormState.View);
                DisplayCurrentLesson();
                DisplayCurrentPositionOnStripLabel(rowNumber, totalRowCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                ClearParentStatusStrip();
                this.Close();
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        #endregion

        #region FormState

        private void SetState(FormState state)
        {
            currentState = state;
            LoadState(state);
        }
        private void LoadState(FormState state)
        {
            if (state == FormState.View)
            {
                btnAdd.Enabled = true;
                btnCancel.Enabled = false;
                btnDelete.Enabled = true;
                btnSave.Text = "Edit";
                AllowInputs(false);
                errorProvider1.Clear();
            }
            else
            {
                btnAdd.Enabled = false;
                btnCancel.Enabled = true;
                btnDelete.Enabled = false;
                btnSave.Text = "Save";
                AllowInputs(true);
                DisplayStatusRow("Updating");
                if (state == FormState.Add)
                {
                    DisplayStatusRow("Adding");
                    grpLessons.ClearChildControls(-1);
                    rdoEndTimeAM.Checked = true;
                    rdoStartTimeAM.Checked = true;
                    rdoTheoric.Checked = true;

                    cmbCars.SelectedIndex = 0;
                    cmbInstructors.SelectedIndex = 0;
                    cmbStudents.SelectedIndex = 0;
                }

            }
            EnableNavigation(currentState == FormState.View);
        }
        private void AllowInputs(bool allowInput)
        {

            cmbStudents.Enabled = allowInput;
            cmbInstructors.Enabled = allowInput;
            cmbCars.Enabled = allowInput;

            dteLessonDate.Enabled = allowInput;

            rdoStartTimePM.Enabled = allowInput;
            rdoStartTimeAM.Enabled = allowInput;
            rdoEndTimePM.Enabled = allowInput;
            rdoEndTimeAM.Enabled = allowInput;

            rdoTheoric.Enabled = allowInput;
            rdoPractical.Enabled = allowInput;

            txtDuration.ReadOnly = true;
            txtEndTimeHour.ReadOnly = !allowInput;
            txtEndTimeMin.ReadOnly = !allowInput;
            txtPickupLocation.ReadOnly = !allowInput;

            txtStartTimeHour.ReadOnly = !allowInput;
            txtStartTimeMin.ReadOnly = !allowInput;
            txtEndTimeHour.ReadOnly = !allowInput;
            txtEndTimeMin.ReadOnly = !allowInput;
        }

        #endregion

        #region Load Data

        private void LoadInstructors()
        {
            string sqlInstructors = "SELECT InstructorID, LastName + ', '+ FirstName AS FullName " +
               "FROM Instructors ORDER BY LastName, FirstName";
            UIUtilities.Bind(cmbInstructors, "FullName", "InstructorId", DataAccess.GetData(sqlInstructors), addEmptyRow: true, "-- Select an Instructor --");
        }

        private void LoadStudents()
        {
            string sqlStudents = "SELECT StudentID, LastName + ', '+ FirstName AS FullName FROM Students ORDER BY LastName, FirstName";
            UIUtilities.Bind(cmbStudents, "FullName", "StudentId", DataAccess.GetData(sqlStudents), addEmptyRow: true, "-- Select a Student --");
        }

        private void LoadCars()
        {
            string sqlCars = @"SELECT CarId , 
		                                    Make + ' ' + Model + ' ' +
		                                    CONVERT(VARCHAR, [Year]) +
		                                    ' (' + CONVERT(VARCHAR, LicensePlate)
		                                    + ')' AS CarDescription 
                                    FROM Cars";
            UIUtilities.Bind(cmbCars, "CarDescription", "CarId", DataAccess.GetData(sqlCars), addEmptyRow: true, "-- Select a Car --");
        }

        private void LoadFirstLesson()
        {
            DataTable firstLesson = DataAccess.GetData("SELECT TOP 1 LessonID, StudentID, InstructorID, CarID FROM" +
                " Lessons ORDER BY StudentID, InstructorId, CarID");

            if (firstLesson.Rows.Count > 0)
            {
                DataRow firstRow = firstLesson.Rows[0];
                currentLessonId = Convert.ToInt32(firstRow["LessonID"]);
                LoadCurrentPosition();
                DisplayCurrentLesson();
            }
        }

        private void LoadCurrentPosition()
        {
            DataTable dt = GetLessonPositionDataTable();
            LoadCurrentPosition(dt.Rows[0]);
            DisplayCurrentPositionOnStripLabel(rowNumber, totalRowCount);
            EnableNavigation(currentState == FormState.View);
        }

        private void LoadCurrentPosition(DataRow dataRow)
        {
            firstLessonId = Convert.ToInt32(dataRow["FirstLessonId"]);
            previousLessonId = dataRow.GetInteger("PreviousLessonId");

            nextLessonId = dataRow.GetInteger("NextLessonId");

            lastLessonId = Convert.ToInt32(dataRow["LastLessonId"]);

            rowNumber = Convert.ToInt32(dataRow["RowNumber"]);
            totalRowCount = Convert.ToInt32(dataRow["TotalRowCount"]);
        }

        private void DisplayCurrentLesson()
        {
            DataTable dtLesson = GetLessonDataTable();
            if (dtLesson.Rows.Count == 1)
            {
                DisplayCurrentLesson(dtLesson.Rows[0]);
            }
        }

        private void DisplayCurrentLesson(DataRow dataRow)
        {
            int instructorId = Convert.ToInt32(dataRow["InstructorID"]);
            int studentId = Convert.ToInt32(dataRow["StudentID"]);
            int carId = dataRow["CarID"] == DBNull.Value ? 0 : Convert.ToInt32(dataRow["CarID"]);
            Time lessonStartTime = HelperMethods.From24HourString(dataRow["LessonStartTime"].ToString());
            Time lessonEndTime = HelperMethods.From24HourString(dataRow["LessonEndTime"].ToString());

            txtId.Text = dataRow["LessonID"].ToString();
            cmbStudents.SelectedValue = studentId;
            cmbInstructors.SelectedValue = instructorId;
            if (carId != 0) cmbCars.SelectedValue = carId;
            else cmbCars.SelectedIndex = 0;

            dteLessonDate.Value = (DateTime)dataRow["LessonDate"];

            txtStartTimeHour.Text = lessonStartTime.Hour.ToString("D2");
            txtStartTimeMin.Text = lessonStartTime.Minute.ToString("D2");

            txtEndTimeHour.Text = lessonEndTime.Hour.ToString("D2");
            txtEndTimeMin.Text = lessonEndTime.Minute.ToString("D2");

            if (lessonStartTime.AmPm == "AM")
            {
                rdoStartTimeAM.Checked = true;
                rdoStartTimePM.Checked = false;
            }
            else
            {
                rdoStartTimeAM.Checked = false;
                rdoStartTimePM.Checked = true;
            }

            if (lessonEndTime.AmPm == "AM")
            {
                rdoEndTimeAM.Checked = true;
                rdoEndTimePM.Checked = false;
            }
            else
            {
                rdoEndTimeAM.Checked = false;
                rdoEndTimePM.Checked = true;
            }

            txtDuration.Text = ConvertMinutesToTimeString(Convert.ToInt32(dataRow["LessonMinutesDuration"]));

            LessonTypeDBValueToFormRdo(dataRow["LessonType"].ToString());

            txtPickupLocation.Text = NullableDBValueToString(dataRow, "PickupLocation");
        }

        #endregion

        #region Retrieve Data

        private DataTable GetLessonPositionDataTable()
        {
            string sql = $@"
                    SELECT
	                    q.NextLessonId,
                        q.NextStudentId,
                        q.NextInstructorId,
                        q.NextCarId,
	                    q.PreviousLessonId,
                        q.PreviousStudentId,
                        q.PreviousInstructorId,
                        q.PreviousCarId,
                        q.RowNumber,

                    (SELECT TOP(1) LessonID from Lessons Order by StudentID) as FirstLessonId,
                    (SELECT Top(1) StudentID from Lessons Order by StudentID) as FirstStudentId,
                    (SELECT Top(1) InstructorID from Lessons Order by StudentID) as FirstInstructorId,
                    (SELECT Top(1) CarID from Lessons Order by StudentID) as FirstCarId,
                    (SELECT TOP(1) LessonID from Lessons Order by StudentID Desc) as lastLessonId,
                    (SELECT Top(1) StudentID from Lessons Order by StudentID Desc) as LastCourseId,
                    (SELECT Top(1) InstructorId from Lessons Order by StudentID Desc) as LastInstructorId,
                    (SELECT Top(1) CarID from Lessons Order by CarID Desc) as LastCarId,
                    (SELECT COUNT(*) FROM Lessons) as [TotalRowCount]
                    FROM
                    (
                    SELECT LessonID, StudentID, InstructorID, CarID,
                    LEAD(LessonID) OVER(Order By LessonID) as NextLessonId,
                    LEAD(StudentId) OVER(Order By StudentID) as NextStudentId,
                    LEAD(InstructorID) OVER(Order By StudentID) as NextInstructorId,
                    LEAD(CarID) OVER(Order By StudentID) as NextCarId,
                    LAG(LessonID) OVER(Order By LessonID) as PreviousLessonId,
                    LAG(StudentId) OVER(Order By StudentID) as PreviousStudentId,
                    LAG(InstructorID) OVER(Order By StudentID) as PreviousInstructorId,
                    LAG(CarID) OVER(Order By StudentID) as PreviousCarId,
                    ROW_NUMBER() OVER(Order By StudentID) as RowNumber
                    FROM Lessons) as q
                WHERE q.LessonID = {currentLessonId} 
                ORDER BY q.LessonID
                ";
            return DataAccess.GetData(sql);
        }

        private DataTable GetLessonDataTable()
        {
            string sql = $"SELECT * FROM Lessons WHERE LessonID = {currentLessonId}";
            return DataAccess.GetData(sql);
        }

        #endregion

        #region Send Data

        private void UpdateLesson()
        {
            #region Get the Data

            int lessonId = Convert.ToInt32(txtId.Text);
            int studentId = Convert.ToInt32(cmbStudents.SelectedValue);
            int instructorId = Convert.ToInt32(cmbInstructors.SelectedValue);
            int? carId = cmbCars.SelectedIndex > 0 ? Convert.ToInt32(cmbCars.SelectedValue) : null;
            DateTime lessonDate = dteLessonDate.Value;

            string startTimePeriod;
            string endTimePeriod;
            if (rdoStartTimeAM.Checked)
            {
                startTimePeriod = "AM";
            }
            else
            {
                startTimePeriod = "PM";
            }

            if (rdoEndTimeAM.Checked)
            {
                endTimePeriod = "AM";
            }
            else
            {
                endTimePeriod = "PM";
            }
            Time startTime = new Time(Convert.ToInt32(txtStartTimeHour.Text),
                Convert.ToInt32(txtStartTimeMin.Text), startTimePeriod);

            Time endTime = new Time(Convert.ToInt32(txtEndTimeHour.Text),
                    Convert.ToInt32(txtEndTimeMin.Text), endTimePeriod);

            int durationInMinutes = HelperMethods.CalculateDurationInMinutes(startTime, endTime);

            string lessonType;
            string pickupLocation = txtPickupLocation.Text;

            if (rdoPractical.Checked)
            {
                lessonType = "Practical";

            }
            else
            {
                lessonType = "Theoric";
            }

            if (startTime.CompareTo(endTime) > 0) durationInMinutes = 0;

            #endregion


            timesErrorMessages = Validator.GetTimesErrorMessages(startTime, endTime, durationInMinutes);
            if (timesErrorMessages.Length > 0)
            {
                throw new Exception(BusinessRulesConstants.BR_ERROR_MESSAGE_TIMES);
            }

            if (Validator.ValidateLessonInstructorDateAndTime(instructorId, lessonDate,
                            startTime, endTime, lessonId) &&
                    Validator.ValidateLessonStudentDate(studentId, lessonDate, lessonId))
            {
                string atCarId = "@Car_ID";
                string atPickupLocation = "@Pickup_Location";
                string sql = $@"
                    UPDATE Lessons
                     SET
                        StudentID = {studentId},
                        InstructorID = {instructorId},
                        CarID = {(carId == null ? atCarId : carId.Value)},
                        LessonType = '{lessonType}',
                        LessonMinutesDuration = {durationInMinutes},
                        PickupLocation = {(pickupLocation == string.Empty ? atPickupLocation : FormatToDB(pickupLocation))},
                        LessonEndTime = '{endTime.ToDBFormatString()}',
                        LessonStartTime = '{startTime.ToDBFormatString()}',
                        LessonDate = '{lessonDate}'
                        WHERE LessonID = {currentLessonId} 
                    ";

                int rowsAffected;
                if (carId == null && pickupLocation == string.Empty) rowsAffected = DataAccess.SendData(sql, atCarId, atPickupLocation);

                else if (carId == null) rowsAffected = DataAccess.SendData(sql, atCarId: atCarId);

                else if (pickupLocation == string.Empty) rowsAffected = DataAccess.SendData(sql, atPickupLocation: atPickupLocation);

                else rowsAffected = DataAccess.SendData(sql);

                if (rowsAffected == 1)
                {
                    MessageBox.Show("Lesson was updated.");
                    int oldDuration = HelperMethods.GetOldDuration(lessonId);
                    if (oldDuration != durationInMinutes)
                    {
                        int durationForFee = oldDuration > durationInMinutes ?
                                oldDuration - durationInMinutes : durationInMinutes - oldDuration;
                        HelperMethods.UpdateStudentFees(durationForFee, studentId);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to update Lesson");
                }

                LoadFirstLesson();
            }
            else
            {
                errorMessages = Validator.GetLessonErrorMessages(
                                instructorId, studentId, lessonDate, startTime, endTime, lessonId);

                throw new Exception(BusinessRulesConstants.BR_ERROR_MESSAGE);
            }

        }

        private void CreateLesson()
        {
            #region Get TheData
            int studentId = Convert.ToInt32(cmbStudents.SelectedValue);
            int instructorId = Convert.ToInt32(cmbInstructors.SelectedValue);
            int? carId = cmbCars.SelectedIndex > 0 ? Convert.ToInt32(cmbCars.SelectedValue) : null;
            DateTime lessonDate = dteLessonDate.Value;

            string startTimePeriod;
            string endTimePeriod;
            if (rdoStartTimeAM.Checked)
            {
                startTimePeriod = "AM";
            }
            else
            {
                startTimePeriod = "PM";
            }

            if (rdoEndTimeAM.Checked)
            {
                endTimePeriod = "AM";
            }
            else
            {
                endTimePeriod = "PM";
            }
            Time startTime = new Time(Convert.ToInt32(txtStartTimeHour.Text),
                Convert.ToInt32(txtStartTimeMin.Text), startTimePeriod);

            Time endTime = new Time(Convert.ToInt32(txtEndTimeHour.Text),
                    Convert.ToInt32(txtEndTimeMin.Text), endTimePeriod);

            int durationInMinutes = HelperMethods.CalculateDurationInMinutes(startTime, endTime);

            string lessonType;
            string pickupLocation = txtPickupLocation.Text;

            if (rdoPractical.Checked)
            {
                lessonType = "Practical";

            }
            else
            {
                lessonType = "Theoric";
            }
            if (startTime.CompareTo(endTime) > 0) durationInMinutes = 0;

            #endregion

            timesErrorMessages = Validator.GetTimesErrorMessages(startTime, endTime, durationInMinutes);
            if (timesErrorMessages.Length > 0)
            {
                throw new Exception(BusinessRulesConstants.BR_ERROR_MESSAGE_TIMES);
            }

            if (Validator.ValidateLessonInstructorDateAndTime(instructorId, lessonDate,
                            startTime, endTime) &&
                    Validator.ValidateLessonStudentDate(studentId, lessonDate))
            {
                string atCarId = "@CarID";
                string atPickupLocation = "@PickupLocation";
                string sql = $@"
                            Insert Into Lessons
                            (
                                StudentID,InstructorID,CarID,
                                LessonType,LessonMinutesDuration,
                                PickupLocation,LessonEndTime,
                                LessonStartTime,LessonDate
                            ) 
                            VALUES
                            (
                                {studentId},
                                {instructorId},
                                {(carId == null ? atCarId : carId.Value)},
                                '{lessonType}',
                                {durationInMinutes},
                                '{(pickupLocation == string.Empty ? atPickupLocation : pickupLocation)}',
                                '{endTime.ToDBFormatString()}',
                                '{startTime.ToDBFormatString()}',
                                '{lessonDate}'
                            )
                ";
                int rowsAffected;
                if (carId == null && pickupLocation == string.Empty) rowsAffected = DataAccess.SendData(sql, atCarId, atPickupLocation);

                else if (carId == null) rowsAffected = DataAccess.SendData(sql, atCarId);

                else if (pickupLocation == string.Empty) rowsAffected = DataAccess.SendData(sql, atPickupLocation);

                else rowsAffected = DataAccess.SendData(sql);

                if (rowsAffected == 1)
                {
                    MessageBox.Show("Lesson was created.");
                    HelperMethods.UpdateStudentFees(durationInMinutes, studentId);
                }
                else
                {
                    MessageBox.Show("Failed to save Assignment");
                }

                LoadFirstLesson();
            }
            else
            {
                errorMessages = Validator.GetLessonErrorMessages(
                                instructorId, studentId, lessonDate, startTime, endTime);

                throw new Exception(BusinessRulesConstants.BR_ERROR_MESSAGE);

            }

        }

        private void DeleteAssignment()
        {
            string sql = $@"
                    DELETE FROM Lessons
                    Where LessonID = {currentLessonId}
                ";

            int rowsAffected = DataAccess.SendData(sql);

            if (rowsAffected == 1)
            {
                MessageBox.Show("Lesson was canceled.");
            }
            else
            {
                MessageBox.Show("The database reports no rows affected, verify your data");
            }
        }

        #endregion

        #region Navigation

        private void Navigation_Handler(object sender, EventArgs e)
        {
            try
            {
                ClearParentStatusStrip();
                if (sender == btnFirst)
                {
                    currentLessonId = firstLessonId;
                }
                else if (sender == btnNext)
                {
                    currentLessonId = nextLessonId.Value;
                }
                else if (sender == btnPrevious)
                {
                    currentLessonId = previousLessonId.Value;
                }
                else if (sender == btnLast)
                {
                    currentLessonId = lastLessonId;
                }
                else return;

                LoadCurrentPosition();
                DisplayCurrentLesson();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void EnableNavigation(bool enable)
        {
            if (enable)
            {
                btnPrevious.Enabled = previousLessonId != null;

                btnNext.Enabled = nextLessonId != null;
            }
            else
            {
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
            }

            if (previousLessonId != null)
                btnFirst.Enabled = enable;
            else btnFirst.Enabled = !enable;

            if (nextLessonId != null)
                btnLast.Enabled = enable;
            else btnLast.Enabled = !enable;
        }

        #endregion

        #region Other Methods
        private string ConvertMinutesToTimeString(int totalMinutes)
        {
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;
            return $"{hours} hours and {minutes} minutes";
        }

        private void LessonTypeDBValueToFormRdo(string DBStringValue)
        {
            if (DBStringValue == "Theoric")
            {
                rdoTheoric.Checked = true;
            }
            else if (DBStringValue == "Practical")
            {
                rdoPractical.Checked = true;
            }
            else
            {
                throw new ArgumentException("passed lesson type is not valid");
            }
        }

        private string NullableDBValueToString(DataRow dt, string columnName)
        {
            if (dt[columnName] == DBNull.Value)
            {
                return "";
            }
            else
            {
                return dt[columnName].ToString()!;
            }
        }

        #endregion

        #region ToolStrip Methods

        public void DisplayCurrentPositionOnStripLabel(int rowNumber, int totalRowCount)
        {
            this.DisplayParentStatusStripMessage($"{rowNumber} of {totalRowCount} records.");
        }
        public void DisplayStatusRow(string status)
        {
            this.DisplayParentStatusStripMessage($"{status}...");
        }
        private void ClearParentStatusStrip() => this.DisplayParentStatusStripMessage(string.Empty);

        #endregion

        #region Validation

        private void Txt_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage = string.Empty;
            TextBox textBox = (TextBox)sender;

            if (sender == txtPickupLocation && txtPickupLocation.Text == string.Empty)
            {
                e.Cancel = false;
                return;
            }
            if (!Validator.IsNotNullOrWhiteSpace(textBox.Text))
            {
                if (!(sender == txtEndTimeMin ||
                    sender == txtStartTimeMin))
                {
                    errorMessage = $"{textBox.Tag} is required.";
                    e.Cancel = true;
                }
            }

            if (sender == txtStartTimeHour)
            {
                if (Validator.ValidateHour(txtStartTimeHour.Text) == false)
                {
                    errorMessage = $"{textBox.Tag} is not in correct format, must be of format 'HH'.";
                    e.Cancel = true;
                }
            }
            if (sender == txtEndTimeHour)
            {
                if (Validator.ValidateHour(txtEndTimeHour.Text) == false)
                {
                    errorMessage = $"{textBox.Tag} is not in correct format, must be of format 'HH'.";
                    e.Cancel = true;
                }
            }

            if (sender == txtEndTimeMin || sender == txtStartTimeMin)
            {
                if (txtStartTimeMin.Text == string.Empty)
                {
                    txtStartTimeMin.Text = "00";
                }
                if (txtEndTimeMin.Text == string.Empty)
                {
                    txtEndTimeMin.Text = "00";
                }

                if (!Validator.ValidateMinute(txtStartTimeMin.Text) ||
                    !Validator.ValidateMinute(txtEndTimeMin.Text))
                {
                    errorMessage = $"{textBox.Tag} is not in correct format, must be of format 'MM'.";
                    e.Cancel = true;
                }
            }

            if (sender == txtPickupLocation &&
                !Validator.ValidateAddress(txtPickupLocation.Text))
            {
                errorMessage = $"{textBox.Tag} is not a valid addres, address should be in format \'St Name. St. Address. Appt. No\'." +
                    $" (Appt No. is optional) - city and province can be added";
                e.Cancel = true;
            }



            errorProvider1.SetError(textBox, errorMessage);
        }

        private void Cmb_Validation(object sender, CancelEventArgs e)
        {
            string errorMessage = string.Empty;
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex <= 0)   // will change this
            {
                if (sender == cmbCars && comboBox.SelectedIndex == 0)
                {
                    return;
                }
                errorMessage = $"{comboBox.Tag} cannot be empty, please select a value";
                e.Cancel = true;
            }
            errorProvider1.SetError(comboBox, errorMessage);
        }

        private void Dte_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage = string.Empty;
            DateTimePicker dateTime = (DateTimePicker)sender;
            if (!Validator.ValidateLessonDate(dteLessonDate.Value))
            {
                errorMessage = $"{dateTime.Tag} is not valid, cannot be less than actual date or " +
                    $"more than {BusinessRulesConstants.MAX_LESSON_BOOKING_NUM_YEARS_SPAN} years from now.";
                e.Cancel = true;
            }
            errorProvider1.SetError(dateTime, errorMessage);
        }

        #endregion

        #region Other Methods

        public string FormatToDB(string value)
        {
            return $"'{value}'";
        }
        private void DisplayExceptionMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, ex.GetType().ToString());
        }

        private void DisplayError(string errorMessage, string errorTitle)
        {
            MessageBox.Show(errorMessage, errorTitle);
        }

        #endregion

        private void frmLessons_Leave(object sender, EventArgs e)
        {
            ClearParentStatusStrip();
        }
    }
}
