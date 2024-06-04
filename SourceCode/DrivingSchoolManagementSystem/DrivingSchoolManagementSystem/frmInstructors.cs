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
        private ToolStripStatusLabel toolStripLabel;
        private ToolStripProgressBar toolStripProgressBar;

        #endregion
        public frmInstructors()
        {
            InitializeComponent();
        }

        private void Navigation_Handler(object sender, EventArgs e)
        {
            UIUtilities.ClearStatusStripLabel(toolStripLabel);

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

        private DataRow GetInstructorDataRow(int currentId)
        {
            string sqlInstructorById = $"SELECT * FROM Instructors WHERE InstructorID = {currentId}";

            DataTable dt = DataAccess.GetData(sqlInstructorById);

            return dt.Rows[0];
        }

        private void LoadCurrentPosition()
        {
            DataRow positionInfoRow = GetPositionDataRow(currentId);
            LoadPositionInfo(positionInfoRow);
            DisplayCurrentPositionOnStripLabel(rowNumber, totalRowCount);
            NavigationButtonManagement();
        }

        private void NavigationButtonManagement()
        {
            btnPrevious.Enabled = previousId != null;
            btnNext.Enabled = nextId != null;

            btnFirst.Enabled = currentId != firstId;
            btnLast.Enabled = currentId != lastId;
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

        private void frmInstructors_Load(object sender, EventArgs e)
        {
            LoadToolStrip();
            SetState(FormState.View);
            LoadFirstInstructor();


        }

        private void LoadFirstInstructor()
        {
            currentId = GetFirstInstructorId();
            LoadCurrentPosition();
            DisplayCurrentInstructor();
        }

        private int GetFirstInstructorId()
        {
            int id = Convert.ToInt32(DataAccess.GetValue("SELECT TOP (1) InstructorID FROM Instructors ORDER BY LastName"));
            return id;
        }

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
                    grpInstructors.Controls.ClearControls();
                }
                DisableNavigation();
            }
        }

        private void DisableNavigation()
        {
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnFirst.Enabled = false;
            btnLast.Enabled = false;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetState(FormState.Add);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Instructor?", "Are you sure",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    LoadTaskBar();
                    DeleteInstructor();
                    ResetTaskBar();
                    LoadFirstInstructor();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Sql Related Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void DeleteInstructor()
        {
            string sqlDelete = $"DELETE FROM Instructors WHERE InstructorID = {txtId.Text}";

            int rowsAffected = DataAccess.SendData(sqlDelete);

            if (rowsAffected == 1)
            {
                MessageBox.Show("Instructor was deleted");
            }
            else
            {
                MessageBox.Show("The database reported no rows affected");
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
                        if (txtId.Text == string.Empty)
                        {
                            //add instructor
                            LoadTaskBar();
                            CreateInstructor();
                            ResetTaskBar();
                            LoadFirstInstructor();
                        }
                        else
                        {
                            //edit instructor
                            LoadTaskBar();
                            UpdateInstructor();
                            ResetTaskBar();
                            LoadFirstInstructor();
                        }
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
                MessageBox.Show("Sql Related Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void UpdateInstructor()
        {
            int age = HelperMethods.CalculateAge(new DateTime(dteBirth.Value.Year, dteBirth.Value.Month, dteBirth.Value.Day));
            if (!Validator.ValidateInstructorAge(age))
            {
                //this would not normally happen
                throw new InvalidOperationException("Error on age, must be between 24 and 60");
            }
            string sqlUpdateInstructor = $@"UPDATE Instructors
                        SET FirstName = '{txtFirstName.Text.Trim()}', 
                            LastName = '{txtLastName.Text.Trim()}', 
                            DateOfBirth = '{dteBirth.Value.Year}-{dteBirth.Value.Month}-{dteBirth.Value.Day}', 
                            HiredDate = '{dteHired.Value.Year}-{dteHired.Value.Month}-{dteHired.Value.Day}', 
                            PhoneNumber = '{txtPhone.Text}', 
                            Email = '{txtEmail.Text}', 
                            LicenceNumber = {txtLicenceNumber.Text}, 
                            Age = {age}, 
                            Address = '{txtAddress.Text}'
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
                // 
                MessageBox.Show("Something went wrong please verify your data");
            }
        }

        private void CreateInstructor()
        {
            int age = HelperMethods.CalculateAge(new DateTime(dteBirth.Value.Year, dteBirth.Value.Month, dteBirth.Value.Day));
            if (!Validator.ValidateInstructorAge(age))
            {
                //this would not normally happen
                throw new InvalidOperationException("Error on age, must be between 24 and 60");
            }
            string sqlInsertInstructors = $@"INSERT INTO Instructors 
                        (FirstName, LastName,
                            DateOfBirth, HiredDate,
                            PhoneNumber, Email,
                            LicenceNumber, Age,
                            Address
                            )
                        VALUES (
                                '{txtFirstName.Text.Trim()}',
                                '{txtLastName.Text.Trim()}',
                                '{dteBirth.Value.Year}-{dteBirth.Value.Month}-{dteBirth.Value.Day}',
                                '{dteHired.Value.Year}-{dteHired.Value.Month}-{dteHired.Value.Day}',
                                '{txtPhone.Text}',
                                '{txtEmail.Text}',
                                {txtLicenceNumber.Text},
                                {age},
                                '{txtAddress.Text}'
                                )";
            int rowsAffected = DataAccess.SendData(sqlInsertInstructors);

            if (rowsAffected == 1)
            {
                MessageBox.Show("Instructor created.");
            }
            else
            {
                MessageBox.Show("The database reported no rows affected.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetState(FormState.View);
            DisplayCurrentInstructor();
            DisplayCurrentPositionOnStripLabel(rowNumber, totalRowCount);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region ToolStrip Methods

        private void LoadToolStripLabel()
        {
            if (MdiParent is null)
            {
                throw new InvalidOperationException("Verify Mdi Parent form");
            }
            StatusStrip parentStatusStrip = (StatusStrip)MdiParent.Controls.Find("statusStrip", true).FirstOrDefault()!;
            if (parentStatusStrip is null)
            {
                throw new InvalidOperationException("Controls of mdi form does not contain tool" +
                    " strip with 'statusStrip' name");
            }
            ToolStripStatusLabel? mdiToolStripLabel = parentStatusStrip.Items.Find("tlstrpStatus", true).FirstOrDefault()
                                                        as ToolStripStatusLabel;

            if (mdiToolStripLabel is null)
            {
                throw new InvalidOperationException("Controls of mdi form does not contain tool" +
                    " strip label with 'tlstrpStatus' name");
            }
            toolStripLabel = mdiToolStripLabel;
        }
        private void LoadToolStripProgressBar()
        {
            if (MdiParent is null)
            {
                throw new InvalidOperationException("Verify Mdi Parent form");
            }
            StatusStrip parentStatusStrip = (StatusStrip)MdiParent.Controls.Find("statusStrip", true).FirstOrDefault()!;
            if (parentStatusStrip is null)
            {
                throw new InvalidOperationException("Controls of mdi form does not contain tool" +
                    " strip with 'statusStrip' name");
            }
            ToolStripProgressBar? mdiToolStripProgressBar = parentStatusStrip.Items.Find("tlstrpProgressBar", true).FirstOrDefault()
                                        as ToolStripProgressBar;


            if (mdiToolStripProgressBar is null)
            {
                throw new InvalidOperationException("Controls of mdi form does not contain tool" +
                    " strip label with 'tlstrpProgressBar' name");
            }
            toolStripProgressBar = mdiToolStripProgressBar;
        }
        private void LoadToolStrip()
        {
            LoadToolStripLabel();
            LoadToolStripProgressBar();
        }

        public void LoadTaskBar()
        {
            toolStripProgressBar.Step = 1;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.IsRunning)
            {
                toolStripProgressBar.PerformStep();
                if (stopwatch.ElapsedMilliseconds == 1000)
                {
                    stopwatch.Stop();
                }
            }

        }
        public void ResetTaskBar()
        {
            toolStripProgressBar.Value = 0;
        }
        public void DisplayCurrentPositionOnStripLabel(/*ToolStripStatusLabel label,*/ int rowNumber, int totalRowCount)
        {
            toolStripLabel.Text = $"{rowNumber} of {totalRowCount} records.";
        }
        public void DisplayStatusRow(/*ToolStripStatusLabel label,*/ string status)
        {
            toolStripLabel.Text = $"{status}...";
        }
        #endregion

        private void txt_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage = string.Empty;
            TextBox textBox = (TextBox)sender;
            if (!Validator.IsNotNullOrWhiteSpace(textBox.Text))
            {
                errorMessage = $"{textBox.Tag} is required.";
            }

            if ((sender == txtFirstName || sender == txtLastName) && !Validator.ValidateName(textBox.Text))
            {
                errorMessage = $"{textBox.Tag} is not valid (a valid name must start with an uppercase letter and can only contain" +
                    $" letters, spaces, hyphens, and apostrophes).";
            }
            if(sender == txtPhone && !Validator.ValidatePhoneNumber(textBox.Text))
            {
                errorMessage = $"{textBox.Tag} is not valid (must be a 9 digit number without special characters of spaces).";
            }
            if (sender == txtEmail && !Validator.ValidateEmail(textBox.Text))
            {
                errorMessage = $"{textBox.Tag} is not valid email, please retry.";
            }
            if (sender == txtLicenceNumber && !Validator.ValidateLicenceNumber(textBox.Text))
            {
                errorMessage = $"{textBox.Tag} is not valid (must be a 8 digit number without special characters of spaces).";
            }
            if (sender == txtAddress && !Validator.ValidateAddress(textBox.Text))
            {
                errorMessage = $"{textBox.Tag} is not a valid addres, please retry.";
            }
            errorProvider1.SetError(textBox, errorMessage);
        }

        private void dte_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage = string.Empty;
            DateTimePicker dateTime = (DateTimePicker)sender;
            if (sender == dteHired && !Validator.ValidateHiredDate(new DateTime(dteHired.Value.Year, dteHired.Value.Month, dteHired.Value.Day)))
            {
                errorMessage = $"{dateTime.Tag} is not valid, cannot be more than actual date.";
            }
            if (sender == dteBirth && !Validator.ValidateDateOfBirth(new DateTime(dteBirth.Value.Year, dteBirth.Value.Month, dteBirth.Value.Day)))
            {
                errorMessage = $"{dateTime.Tag} is not valid, an Instructor must be between 24 and 60 years of age.";
            }
            errorProvider1.SetError(dateTime, errorMessage);
        }

    }
}
