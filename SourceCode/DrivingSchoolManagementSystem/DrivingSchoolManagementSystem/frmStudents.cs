using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingSchoolManagementSystem
{
    public partial class frmStudents : Form
    {

        #region Private Fields

        private int currentId;
        private int firstId;
        private int lastId;
        private int? nextId;
        private int? previousId;
        private int rowNumber;
        private int totalRowCount;
        private FormState currentState;

        #endregion

        public frmStudents()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void Navigation_Handler(object sender, EventArgs e)
        {
            try
            {
                ClearParentStatusStrip();

                if (sender == btnFirst)
                {
                    currentId = firstId;
                }
                else if (sender == btnLast)
                {
                    currentId = lastId;
                }
                else if (sender == btnNext)
                {
                    if (nextId != null)
                        currentId = nextId.Value;
                    else
                        MessageBox.Show("The last  is currently displayed");

                }
                else if (sender == btnPrevious)
                {
                    if (previousId != null)
                        currentId = previousId.Value;
                    else
                        MessageBox.Show("The first  is currently displayed");
                }
                else
                {
                    return;
                }

                LoadCurrentPosition();
                DisplayCurrentStudent();
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void frmStudents_Load(object sender, EventArgs e)
        {
            try
            {
                SetState(FormState.View);
                LoadFirstStudent();
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SetState(FormState.Add);
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Student?", "Are you sure",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DeleteStudent();
                    LoadFirstStudent();
                }
                catch (SqlException ex)
                {
                    // for debugging
                    MessageBox.Show("Sql Related Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    DisplayExceptionMessage(ex);
                }
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
                    if (ValidateChildren())
                    {
                        if (currentState == FormState.Add)
                        {
                            //add
                            CreateStudent();
                        }
                        else
                        {
                            //edit
                            UpdateStudent();
                        }
                        LoadFirstStudent();
                        SetState(FormState.View);
                    }
                    else
                    {
                        MessageBox.Show("Please resolve your validation errors");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Sql Related Error: " + ex.Message + " - SQL Exception");
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                SetState(FormState.View);
                DisplayCurrentStudent();
                DisplayCurrentPositionOnStripLabel(rowNumber, totalRowCount);
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
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

        private void chkHasLearnersLicence_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHasLearnersLicence.Checked)
            {
                if (chkHasLearnersLicence.Enabled)
                {
                    txtLearnersLicenceNumber.ReadOnly = false;
                }
            }
            else
            {
                txtLearnersLicenceNumber.Clear();
                txtLearnersLicenceNumber.ReadOnly = true;
            }
        }

        #endregion

        #region Form State

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
                InputsReadOnly(true);
                NavigationButtonManagement();
                errorProvider1.Clear();
            }
            else
            {
                btnAdd.Enabled = false;
                btnCancel.Enabled = true;
                btnDelete.Enabled = false;
                btnSave.Text = "Save";
                InputsReadOnly(false);
                DisplayStatusRow("Updating");
                if (state == FormState.Add)
                {
                    DisplayStatusRow("Adding");
                    grpStudents.ClearChildControls(-1);
                }
                DisableNavigation();
            }
        }
        private void InputsReadOnly(bool v)
        {
            txtFirstName.ReadOnly = v;
            txtLastName.ReadOnly = v;
            dteBirth.Enabled = !v;
            dteAdmission.Enabled = !v;
            txtPhone.ReadOnly = v;
            txtEmail.ReadOnly = v;
            chkHasLearnersLicence.Enabled = !v;
            if (!v)
            {
                txtLearnersLicenceNumber.ReadOnly = !chkHasLearnersLicence.Checked;
            }
            //txtAge.ReadOnly = v;
            txtAddress.ReadOnly = v;
        }

        #endregion

        #region Load Data

        private void LoadFirstStudent()
        {
            currentId = GetFirstStudentId();
            LoadCurrentPosition();
            DisplayCurrentStudent();
        }

        private void LoadCurrentPosition()
        {
            DataRow positionInfoRow = GetPositionDataRow(currentId);
            LoadPositionInfo(positionInfoRow);
            DisplayCurrentPositionOnStripLabel(rowNumber, totalRowCount);
            NavigationButtonManagement();
        }

        private void LoadPositionInfo(DataRow positionInfoRow)
        {
            currentId = Convert.ToInt32(positionInfoRow["StudentId"]);
            firstId = Convert.ToInt32(positionInfoRow["FirstStudentId"]);
            lastId = Convert.ToInt32(positionInfoRow["LastStudentId"]);
            rowNumber = Convert.ToInt32(positionInfoRow["RowNumber"]);
            totalRowCount = Convert.ToInt32(positionInfoRow["TotalRowCount"]);

            nextId = positionInfoRow["NextStudentId"] != DBNull.Value ?
                Convert.ToInt32(positionInfoRow["NextStudentId"]) : null;

            previousId = positionInfoRow["PreviousStudentId"] != DBNull.Value ?
                Convert.ToInt32(positionInfoRow["PreviousStudentId"]) : null;
        }

        #endregion

        #region Navigation Management

        private void NavigationButtonManagement()
        {
            try
            {
                btnPrevious.Enabled = previousId != null;
                btnNext.Enabled = nextId != null;

                btnFirst.Enabled = currentId != firstId;
                btnLast.Enabled = currentId != lastId;
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void DisableNavigation()
        {
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnFirst.Enabled = false;
            btnLast.Enabled = false;
        }

        #endregion

        #region Display DataRow

        private void DisplayCurrentStudent()
        {
            DataRow currentStudentRow = GetStudentDataRow(currentId);
            DisplayStudent(currentStudentRow);
        }

        private void DisplayStudent(DataRow currentStudentRow)
        {
            txtId.Text = currentStudentRow["StudentID"].ToString();
            txtFirstName.Text = currentStudentRow["FirstName"].ToString();
            txtLastName.Text = currentStudentRow["LastName"].ToString();
            dteBirth.Value = (DateTime)currentStudentRow["DateOfBirth"];
            dteAdmission.Value = (DateTime)currentStudentRow["AdmissionDate"];
            txtPhone.Text = currentStudentRow["PhoneNumber"].ToString();
            txtEmail.Text = currentStudentRow["Email"].ToString();
            chkHasLearnersLicence.Checked = BinaryIntToBoolean(Convert.ToInt32(currentStudentRow["HasLearnersLicence"]));
            txtLearnersLicenceNumber.Text = NullableDBValueToString(currentStudentRow, "LearnersLicenceNumber");
            txtAge.Text = currentStudentRow["Age"].ToString();
            txtAddress.Text = currentStudentRow["Address"].ToString();
            txtDueFees.Text = DBDecimalToString(Convert.ToDecimal(currentStudentRow["DueFees"]));
        }

        #endregion

        #region CRUD operations(Send Data)

        private void DeleteStudent()
        {
            string sqlDelete = $"DELETE FROM Students WHERE StudentID = {txtId.Text}";

            int rowsAffected = DataAccess.SendData(sqlDelete);

            if (rowsAffected == 0)
            {
                MessageBox.Show("The database reported no rows affected, meaning the changes weren't saved");
            }
            else if (rowsAffected == 1)
            {
                MessageBox.Show("Student was deleted successfully");
            }
            else
            {
                // CONNECTION KIND OF ERROR
                MessageBox.Show("Error Occured: Something went wrong please verify your data");
            }
        }

        private void CreateStudent()
        {
            int age = HelperMethods.CalculateAge(dteBirth.Value);
            if (!Validator.ValidateStudentAge(age))
            {
                //this would not normally happen
                MessageBox.Show("Please check entered Date of Birth, a Student must be atleast 16 years of age", "Problem Occured");
                return;
            }
            string sqlInsertStudents = $@"INSERT INTO Students 
                        (
                            FirstName, LastName, DateOfBirth,
                            HasLearnersLicence, AdmissionDate,
                            PhoneNumber, Email,
                            LearnersLicenceNumber, age, address
                            )
                        VALUES (
                                '{DataAccess.SQLFix(txtFirstName.Text.Trim())}',
                                '{DataAccess.SQLFix(txtLastName.Text.Trim())}',
                                '{dteBirth.Value.Year}-{dteBirth.Value.Month}-{dteBirth.Value.Day}',
                                {BoolToBinaryInt(chkHasLearnersLicence.Checked)},
                                '{dteAdmission.Value.Year}-{dteAdmission.Value.Month}-{dteAdmission.Value.Day}',
                                '{txtPhone.Text}',
                                '{txtEmail.Text}',
                                {txtLearnersLicenceNumber.Text},
                                {age},
                                '{DataAccess.SQLFix(txtAddress.Text)}'
                                )";
            int rowsAffected = DataAccess.SendData(sqlInsertStudents);

            if (rowsAffected == 1)
            {
                MessageBox.Show("Student created.");
            }
            else if (rowsAffected == 0)
            {
                MessageBox.Show("Error occured: The database reported no rows affected, meaning the changes weren't saved");

            }
            else
            {
                MessageBox.Show("Error Occured: Something went wrong please verify your data");
            }
        }

        private void UpdateStudent()
        {
            int age = HelperMethods.CalculateAge(dteBirth.Value);
            if (!Validator.ValidateStudentAge(age))
            {
                //this would not normally happen
                MessageBox.Show("Please check entered Date of Birth, a Student must be atleast 16 years of age", "Problem Occured");
                return;
            }
            string sqlUpdateStudent = $@"UPDATE Students
                        SET FirstName = '{DataAccess.SQLFix(txtFirstName.Text.Trim())}', 
                            LastName = '{DataAccess.SQLFix(txtLastName.Text.Trim())}', 
                            DateOfBirth = '{dteBirth.Value.Year}-{dteBirth.Value.Month}-{dteBirth.Value.Day}',
                            HasLearnersLicence = {BoolToBinaryInt(chkHasLearnersLicence.Checked)},
                            AdmissionDate = '{dteAdmission.Value.Year}-{dteAdmission.Value.Month}-{dteAdmission.Value.Day}', 
                            PhoneNumber = '{txtPhone.Text}', 
                            Email = '{txtEmail.Text}', 
                            LearnersLicenceNumber = {txtLearnersLicenceNumber.Text}, 
                            Age = {age}, 
                            Address = '{DataAccess.SQLFix(txtAddress.Text)}'
                        WHERE StudentID = {txtId.Text}";


            int rowsAffected = DataAccess.SendData(sqlUpdateStudent);
            if (rowsAffected == 0)
            {
                MessageBox.Show("The database reported no rows affected, meaning the changes weren't saved");
            }
            else if (rowsAffected == 1)
            {
                MessageBox.Show("Instructor updated successfully");
            }
            else
            {
                // CONNECTION KIND OF ERROR
                MessageBox.Show("Error Occured: Something went wrong please verify your data");
            }
        }

        #endregion

        #region Get / Retrieve Data

        private int GetFirstStudentId()
        {
            int id = Convert.ToInt32(DataAccess.GetValue("SELECT TOP (1) StudentID FROM Students ORDER BY LastName"));
            return id;
        }

        #endregion

        #region Get Data Row

        private DataRow GetStudentDataRow(int currentId)
        {
            string sqlStudentById = $"SELECT * FROM Students WHERE StudentID = {currentId}";

            DataTable dt = DataAccess.GetData(sqlStudentById);

            return dt.Rows[0];
        }

        private DataRow GetPositionDataRow(int currentId)
        {
            string sqlNavigation = $@"
                SELECT 
                StudentID,
                (
                    SELECT TOP(1) StudentID as FirstStudentId FROM Students ORDER BY LastName
                ) as FirstStudentId,
                s.PreviousStudentId,
                s.NextStudentId,
                (
                    SELECT TOP(1) StudentID as LastStudentID FROM Students ORDER BY LastName Desc
                ) as LastStudentID,
                s.RowNumber,
                (
                    SELECT COUNT(*) FROM Students
                ) as [TotalRowCount]
                FROM
                (
                    SELECT StudentID, FirstName, LastName,
                    LEAD(StudentID) OVER(ORDER BY LastName) AS NextStudentId,
                    LAG(StudentID) OVER(ORDER BY LastName) AS PreviousStudentId,
                    ROW_NUMBER() OVER(ORDER BY LastName) AS 'RowNumber'
                    FROM Students
                ) AS s
                WHERE s.StudentID = {currentId}
                ORDER BY s.LastName
                ";

            DataTable dt = DataAccess.GetData(sqlNavigation);

            return dt.Rows[0];
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

        private void txt_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage = string.Empty;
            TextBox textBox = (TextBox)sender;
            if (!textBox.ReadOnly || textBox.Enabled)
            {
                if (!Validator.IsNotNullOrWhiteSpace(textBox.Text))
                {
                    errorMessage = $"{textBox.Tag} is required.";
                    e.Cancel = true;
                }

                if ((sender == txtFirstName || sender == txtLastName) && !Validator.ValidateName(textBox.Text))
                {
                    errorMessage = $"{textBox.Tag} is not valid (a valid name must start with an uppercase letter and can only contain" +
                        $" letters, spaces, hyphens, and apostrophes).";
                    e.Cancel = true;
                }
                if (sender == txtPhone && !Validator.ValidatePhoneNumber(textBox.Text))
                {
                    errorMessage = $"{textBox.Tag} is not valid (must be a 10 digit number without special characters or spaces, " +
                        $"must start with area code in {BusinessRulesConstants.COUNTRY}).";
                    e.Cancel = true;
                }
                if (sender == txtEmail && !Validator.ValidateEmail(textBox.Text))
                {
                    errorMessage = $"{textBox.Tag} is not valid email, please retry.";
                    e.Cancel = true;
                }
                if (sender == txtLearnersLicenceNumber && !Validator.ValidateLicenceNumber(textBox.Text) && chkHasLearnersLicence.Checked)
                {
                    errorMessage = $"{textBox.Tag} is not valid (must be a 8 digit number without special characters or spaces).";
                    e.Cancel = true;
                }
                if (sender == txtAddress && !Validator.ValidateAddress(textBox.Text))
                {
                    errorMessage = $"{textBox.Tag} is not a valid addres, address should be in format \'St Name. St. Address. Appt. No\'." +
                        $" (Appt No. is optional) - city and province can be added";
                    e.Cancel = true;
                }
                errorProvider1.SetError(textBox, errorMessage); 
            }
        }

        private void dte_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage = string.Empty;
            DateTimePicker dateTime = (DateTimePicker)sender;
            if (sender == dteAdmission && !Validator.ValidateAdmissionDate(dteAdmission.Value))
            {
                errorMessage = $"{dateTime.Tag} is not valid, cannot be more than actual date or " +
                     $"less than Year {BusinessRulesConstants.SCHOOL_HIRE_START_YEAR}.";
                e.Cancel = true;
            }
            if (sender == dteBirth)
            {
                int age = HelperMethods.CalculateAge(dteBirth.Value);
                if (!Validator.ValidateStudentAge(age))
                {
                    //this would not normally happen
                    errorMessage = $"{dateTime.Tag} is not valid, a Student must be atleast " +
                        $"{BusinessRulesConstants.MINIMUM_STUDENT_AGE} years of age.";
                    e.Cancel = true;
                }

            }
            errorProvider1.SetError(dateTime, errorMessage);
        }

        #endregion

        #region Other Methods
        private void DisplayExceptionMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, ex.GetType().ToString());
        }

        private bool BinaryIntToBoolean(int binaryValue)
        {
            if (binaryValue == 0)
            {
                return false;
            }
            else if (binaryValue == 1)
            {
                return true;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Given input is not a valid binary");
            }
        }

        private int BoolToBinaryInt(bool value)
        {
            if (value) return 1;
            else return 0;
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

        private string DBDecimalToString(decimal dbValueInDecimal)
        {
            return $"{dbValueInDecimal:c}";
        }

        #endregion

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
