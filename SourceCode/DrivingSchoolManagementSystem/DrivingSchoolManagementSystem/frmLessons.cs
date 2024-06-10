using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingSchoolManagementSystem
{
    public partial class frmLessons : Form
    {
        #region Private Fields

        //int currentStudentId;
        //int currentInstructorId;
        //int currentCarId;
        int currentLessonId;

        //int firstStudentId;
        //int firstInstructorId;
        //int firstCarId;
        int firstLessonId;

        //int lastStudentId;
        //int lastInstructorId;
        //int lastCarId;
        int lastLessonId;

        //int? previousStudentId;
        //int? previousInstructorId;
        //int? previousCarId;
        int? previousLessonId;

        //int? nextStudentId;
        //int? nextInstructorId;
        //int? nextCarId;
        int? nextLessonId;

        int rowNumber;
        int totalRowCount;

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
                            CreateAssignment();
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
                        MessageBox.Show("Please resolve your errors");
                    }
                }
            }
            catch (Exception ex) when (ex.Message == "Duplicate rows")
            {
                MessageBox.Show($"Cannot insert because an entry with these" +
                    $" values exist already", "Cannot insert entry");
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

            txtDuration.ReadOnly = !allowInput;
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
            UIUtilities.Bind(cmbInstructors, "FullName", "InstructorId", DataAccess.GetData(sqlInstructors));
        }

        private void LoadStudents()
        {
            string sqlStudents = "SELECT StudentID, LastName + ', '+ FirstName AS FullName FROM Students ORDER BY LastName, FirstName";
            UIUtilities.Bind(cmbStudents, "FullName", "StudentId", DataAccess.GetData(sqlStudents));
        }

        private void LoadCars()
        {
            string sqlCars = @"SELECT CarId , 
		                                    Make + ' ' + Model + ' ' +
		                                    CONVERT(VARCHAR, [Year]) +
		                                    ' (' + CONVERT(VARCHAR, LicensePlate)
		                                    + ')' AS CarDescription 
                                    FROM Cars";
            UIUtilities.Bind(cmbCars, "CarDescription", "CarId", DataAccess.GetData(sqlCars));
        }

        private void LoadFirstLesson()
        {
            DataTable firstLesson = DataAccess.GetData("SELECT TOP 1 LessonID, StudentID, InstructorID, CarID FROM" +
                " Lessons ORDER BY StudentID, InstructorId, CarID");

            if (firstLesson.Rows.Count > 0)
            {
                DataRow firstRow = firstLesson.Rows[0];

                //currentStudentId = Convert.ToInt32(firstRow["StudentID"]);
                //currentInstructorId = Convert.ToInt32(firstRow["InstructorID"]);
                //currentCarId = Convert.ToInt32(firstRow["CarID"]);
                currentLessonId = Convert.ToInt32(firstRow["LessonID"]);
               // rowNumber = Convert.ToInt32(firstRow["RowNumber"]);
               // totalRowCount = Convert.ToInt32(firstRow["TotalRowCount"]);



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
            //firstStudentId = Convert.ToInt32(dataRow["FirstStudentId"]);
            //firstInstructorId = Convert.ToInt32(dataRow["FirstInstructorId"]);
            //firstCarId = Convert.ToInt32(dataRow["FirstCarId"]);

            previousLessonId = dataRow.GetInteger("PreviousLessonId");
            //previousStudentId = dataRow.GetInteger("PreviousStudentId");
            //previousInstructorId = dataRow.GetInteger("PreviousInstructorId");
            //previousCarId = dataRow.GetInteger("PreviousCarId");

            nextLessonId = dataRow.GetInteger("NextLessonId");
            //nextStudentId = dataRow.GetInteger("NextStudentId");
            //nextInstructorId = dataRow.GetInteger("NextInstructorId");
            //nextCarId = dataRow.GetInteger("NextCarId");

            lastLessonId = Convert.ToInt32(dataRow["LastLessonId"]);
            //lastStudentId = Convert.ToInt32(dataRow["LastStudentId"]);
            //lastInstructorId = Convert.ToInt32(dataRow["LastInstructorId"]);
            //lastCarId = Convert.ToInt32(dataRow["LastCarId"]);

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
            int carId = Convert.ToInt32(dataRow["CarID"]);
            Time lessonStartTime = HelperMethods.From24HourString(dataRow["LessonStartTime"].ToString());
            Time lessonEndTime = HelperMethods.From24HourString(dataRow["LessonEndTime"].ToString());

            txtId.Text = dataRow["LessonID"].ToString();
            cmbStudents.SelectedValue = studentId;
            cmbInstructors.SelectedValue = instructorId;
            cmbCars.SelectedValue = carId;

            dteLessonDate.Value = (DateTime)dataRow["LessonDate"];

            txtStartTimeHour.Text = lessonStartTime.Hour.ToString("D2");
            txtStartTimeMin.Text = lessonStartTime.Minute.ToString("D2");

            txtEndTimeHour.Text = lessonEndTime.Hour.ToString("D2");
            txtEndTimeMin.Text = lessonEndTime.Minute.ToString("D2");

            if (lessonStartTime.Period == "AM")
            {
                rdoStartTimeAM.Checked = true;
                rdoStartTimePM.Checked = false;
            }
            else
            {
                rdoStartTimeAM.Checked = false;
                rdoStartTimePM.Checked = true;
            }

            if (lessonEndTime.Period == "AM")
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

            this.DisplayParentStatusStripMessage("Assignment Loaded");
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

            int studentId = Convert.ToInt32(cmbStudents.SelectedValue);
            int instructorId = Convert.ToInt32(cmbInstructors.SelectedValue);
            int carId = Convert.ToInt32(cmbCars.SelectedValue);
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

            int durationInMinutes = Convert.ToInt32(txtDuration.Text);

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

            string sql = $@"
            UPDATE Lessons
             SET
                StudentID = {studentId},
                InstructorID = {instructorId},
                CarID = {carId},
                LessonType = '{lessonType}',
                LessonMinutesDuration = {durationInMinutes},
                PickupLocation = '{(pickupLocation == string.Empty ? DBNull.Value : pickupLocation)}',
                LessonEndTime = '{endTime.ToDBFormatString()}',
                LessonStartTime = '{startTime.ToDBFormatString()}',
                LessonDate = '{lessonDate}';
                WHERE LessonID = {currentLessonId} 
            ";
            int rowsAffected = DataAccess.SendData(sql);

            if (rowsAffected == 1)
            {
                MessageBox.Show("Lesson was updated.");
            }
            else
            {
                MessageBox.Show("Failed to update Lesson");
            }

            LoadFirstLesson();
        }

        private void CreateAssignment()
        {
            int studentId = Convert.ToInt32(cmbStudents.SelectedValue);
            int instructorId = Convert.ToInt32(cmbInstructors.SelectedValue);
            int carId = Convert.ToInt32(cmbCars.SelectedValue);
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

            int durationInMinutes = Convert.ToInt32(txtDuration.Text);

            string lessonType;
            string pickupLocation = string.Empty;

            if (rdoPractical.Checked)
            {
                lessonType = "Practical";
                pickupLocation = txtPickupLocation.Text;
            }
            else
            {
                lessonType = "Theoric";
            }

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
                {carId},
                '{lessonType}',
                {durationInMinutes},
                '{(pickupLocation == string.Empty ? DBNull.Value : pickupLocation)}',
                '{endTime.ToDBFormatString()}',
                '{startTime.ToDBFormatString()}',
                '{lessonDate}'
            )
            ";

            int rowsAffected = DataAccess.SendData(sql);

            if (rowsAffected == 1)
            {
                MessageBox.Show("Lesson was created.");
            }
            else
            {
                MessageBox.Show("Failed to save Assignment");
            }

            LoadFirstLesson();
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
                    //currentStudentId = firstStudentId;
                    //currentInstructorId = firstInstructorId;
                    //currentCarId = firstCarId;
                }
                else if (sender == btnNext)
                {
                    currentLessonId = nextLessonId.Value;
                    //currentStudentId = nextStudentId.Value;   // because they are nullable
                    //currentInstructorId = nextInstructorId.Value;
                    //currentCarId = nextCarId.Value;
                }
                else if (sender == btnPrevious)
                {
                    currentLessonId = previousLessonId.Value;
                    //currentStudentId = previousStudentId.Value;   // because they are nullable
                    //currentInstructorId = previousInstructorId.Value;
                    //currentCarId = previousCarId.Value;
                }
                else if (sender == btnLast)
                {
                    currentLessonId = lastLessonId;
                    //currentStudentId = lastStudentId;
                    //currentInstructorId = lastInstructorId;
                    //currentCarId = lastCarId;
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
            
            if(nextLessonId != null)
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

        #region Other Methods

        private void DisplayExceptionMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, ex.GetType().ToString());
        }

        #endregion
    }
}
