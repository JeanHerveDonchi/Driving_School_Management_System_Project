using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace DrivingSchoolManagementSystem
{
    public partial class frmInstructors : Form
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
        public frmInstructors()
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
                DisplayCurrentInstructor();
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void frmInstructors_Load(object sender, EventArgs e)
        {
            try
            {
                SetState(FormState.View);
                LoadFirstInstructor();
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
            if (MessageBox.Show("Are you sure you want to delete this Instructor?", "Are you sure",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DeleteInstructor();
                    LoadFirstInstructor();
                }
                catch (SqlException ex)
                {
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
                            CreateInstructor();
                        }
                        else
                        {
                            //edit
                            UpdateInstructor();
                        }
                        LoadFirstInstructor();
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
                DisplayCurrentInstructor();
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
                    grpInstructors.ClearChildControls(-1);
                }
                DisableNavigation();
            }
        }

        private void InputsReadOnly(bool v)
        {
            txtFirstName.ReadOnly = v;
            txtLastName.ReadOnly = v;
            dteBirth.Enabled = !v;
            dteHired.Enabled = !v;
            txtPhone.ReadOnly = v;
            txtEmail.ReadOnly = v;
            txtLicenceNumber.ReadOnly = v;
            //txtAge.ReadOnly = v;
            txtAddress.ReadOnly = v;
        }

        #endregion

        #region Load Data

        private void LoadFirstInstructor()
        {
            currentId = GetFirstInstructorId();
            LoadCurrentPosition();
            DisplayCurrentInstructor();
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
            currentId = Convert.ToInt32(positionInfoRow["InstructorId"]);
            firstId = Convert.ToInt32(positionInfoRow["FirstInstructorId"]);
            lastId = Convert.ToInt32(positionInfoRow["LastInstructorId"]);
            rowNumber = Convert.ToInt32(positionInfoRow["RowNumber"]);
            totalRowCount = Convert.ToInt32(positionInfoRow["TotalRowCount"]);

            nextId = positionInfoRow["NextInstructorId"] != DBNull.Value ?
                Convert.ToInt32(positionInfoRow["NextInstructorId"]) : null;

            previousId = positionInfoRow["PreviousInstructorId"] != DBNull.Value ?
                Convert.ToInt32(positionInfoRow["PreviousInstructorId"]) : null;
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

        private void DisplayCurrentInstructor()
        {
            DataRow currentInstructorRow = GetInstructorDataRow(currentId);
            DisplayInstructor(currentInstructorRow);
        }

        private void DisplayInstructor(DataRow currentInstructorRow)
        {
            txtId.Text = currentInstructorRow["InstructorID"].ToString();
            txtFirstName.Text = currentInstructorRow["FirstName"].ToString();
            txtLastName.Text = currentInstructorRow["LastName"].ToString();
            dteBirth.Value = (DateTime)currentInstructorRow["DateOfBirth"];
            dteHired.Value = (DateTime)currentInstructorRow["HiredDate"];
            txtPhone.Text = currentInstructorRow["PhoneNumber"].ToString();
            txtEmail.Text = currentInstructorRow["Email"].ToString();
            txtLicenceNumber.Text = currentInstructorRow["LicenceNumber"].ToString();
            txtAge.Text = currentInstructorRow["Age"].ToString();
            txtAddress.Text = currentInstructorRow["Address"].ToString();
        }

        #endregion

        #region CRUD operations(Send Data)
        private void DeleteInstructor()
        {
            string sqlDelete = $"DELETE FROM Instructors WHERE InstructorID = {txtId.Text}";

            int rowsAffected = DataAccess.SendData(sqlDelete);

            if (rowsAffected == 0)
            {
                MessageBox.Show("The database reported no rows affected, meaning the changes weren't saved");
            }
            else if (rowsAffected == 1)
            {
                MessageBox.Show("Instructor deleted successfully");
            }
            else
            {
                // CONNECTION KIND OF ERROR
                MessageBox.Show("Error Occured: Something went wrong please verify your data");
            }
        }
        private void UpdateInstructor()
        {
            int age = HelperMethods.CalculateAge(dteBirth.Value);
            if (!Validator.ValidateInstructorAge(age))
            {
                //this would not normally happen
                MessageBox.Show("Please check entered Date of Birth, an Instructor must be between 24 and 60 years of age", "Problem Occured");
                return;
            }
            string sqlUpdateInstructor = $@"UPDATE Instructors
                        SET FirstName = '{DataAccess.SQLFix(txtFirstName.Text.Trim())}', 
                            LastName = '{DataAccess.SQLFix(txtLastName.Text.Trim())}', 
                            DateOfBirth = '{dteBirth.Value.Year}-{dteBirth.Value.Month}-{dteBirth.Value.Day}', 
                            HiredDate = '{dteHired.Value.Year}-{dteHired.Value.Month}-{dteHired.Value.Day}', 
                            PhoneNumber = '{txtPhone.Text}', 
                            Email = '{txtEmail.Text}', 
                            LicenceNumber = {txtLicenceNumber.Text}, 
                            Age = {age}, 
                            Address = '{DataAccess.SQLFix(txtAddress.Text)}'
                        WHERE InstructorID = {txtId.Text}";


            int rowsAffected = DataAccess.SendData(sqlUpdateInstructor);
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

        private void CreateInstructor()
        {
            int age = HelperMethods.CalculateAge(dteBirth.Value);
            if (!Validator.ValidateInstructorAge(age))
            {
                //this would not normally happen
                MessageBox.Show("Please check entered Date of Birth, an Instructor must be between 24 and 60 years of age", "Problem Occured");
                return;
            }
            string sqlInsertInstructors = $@"INSERT INTO Instructors 
                        (FirstName, LastName,
                            DateOfBirth, HiredDate,
                            PhoneNumber, Email,
                            LicenceNumber, Age,
                            Address
                            )
                        VALUES (
                                '{DataAccess.SQLFix(txtFirstName.Text.Trim())}',
                                '{DataAccess.SQLFix(txtLastName.Text.Trim())}',
                                '{dteBirth.Value.Year}-{dteBirth.Value.Month}-{dteBirth.Value.Day}',
                                '{dteHired.Value.Year}-{dteHired.Value.Month}-{dteHired.Value.Day}',
                                '{txtPhone.Text}',
                                '{txtEmail.Text}',
                                {txtLicenceNumber.Text},
                                {age},
                                '{DataAccess.SQLFix(txtAddress.Text)}'
                                )";
            int rowsAffected = DataAccess.SendData(sqlInsertInstructors);

            if (rowsAffected == 1)
            {
                MessageBox.Show("Instructor created.");
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
        #endregion

        #region Get/Retrieve Data

        private int GetFirstInstructorId()
        {
            int id = Convert.ToInt32(DataAccess.GetValue("SELECT TOP (1) InstructorID FROM Instructors ORDER BY LastName"));
            return id;
        }

        #endregion

        #region Get Data Row

        private DataRow GetInstructorDataRow(int currentId)
        {
            string sqlInstructorById = $"SELECT * FROM Instructors WHERE InstructorID = {currentId}";

            DataTable dt = DataAccess.GetData(sqlInstructorById);

            return dt.Rows[0];
        }

        private DataRow GetPositionDataRow(int currentId)
        {
            string sqlNavigation = $@"
                SELECT 
                InstructorID,
                (
                    SELECT TOP(1) InstructorID as FirstInstructorId FROM Instructors ORDER BY LastName
                ) as FirstInstructorId,
                i.PreviousInstructorId,
                i.NextInstructorId,
                (
                    SELECT TOP(1) InstructorID as LastInstructorID FROM Instructors ORDER BY LastName Desc
                ) as LastInstructorID,
                i.RowNumber,
                (
	                SELECT COUNT(*) FROM Instructors
                ) as [TotalRowCount]
                FROM
                (
                    SELECT InstructorID, FirstName, LastName,
                    LEAD(InstructorID) OVER(ORDER BY LastName) AS NextInstructorId,
                    LAG(InstructorID) OVER(ORDER BY LastName) AS PreviousInstructorId,
                    ROW_NUMBER() OVER(ORDER BY LastName) AS 'RowNumber'
                    FROM Instructors
                ) AS i
                WHERE i.InstructorID = {currentId}
                ORDER BY i.LastName";

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
                if (sender == txtLicenceNumber && !Validator.ValidateLicenceNumber(textBox.Text))
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
            if (sender == dteHired && !Validator.ValidateHiredDate(dteHired.Value))
            {
                errorMessage = $"{dateTime.Tag} is not valid, cannot be more than actual date" +
                    $"or less than the year {BusinessRulesConstants.SCHOOL_HIRE_START_YEAR}.";
                e.Cancel = true;
            }
            if (sender == dteBirth)
            {
                int age = HelperMethods.CalculateAge(dteBirth.Value);
                if (!Validator.ValidateInstructorAge(age))
                {
                    //this would not normally happen
                    errorMessage = $"{dateTime.Tag} is not valid, an Instructor must be between " +
                        $"{BusinessRulesConstants.MINIMUM_INSTRUCTOR_AGE} and {BusinessRulesConstants.MAXIMUM_INSTRUCTOR_AGE}" +
                        $" years of age.";
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


        #endregion

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
