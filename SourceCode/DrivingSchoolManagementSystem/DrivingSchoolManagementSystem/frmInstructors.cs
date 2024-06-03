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
        private FormState currentState;

        #endregion
        public frmInstructors()
        {
            InitializeComponent();
        }

        private void Navigation_Handler(object sender, EventArgs e)
        {
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
                i.RowNumber
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
                if (state == FormState.Add)
                {
                    //UIUtiltities.ClearControls(grpCourses.Controls);
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
            txtAge.ReadOnly = v;
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
                DeleteInstructor();
                LoadFirstInstructor();

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
                            CreateInstructor();
                            LoadFirstInstructor();
                        }
                        else
                        {
                            //edit
                            UpdateInstructor();
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
            string sqlUpdateInstructor = $@"UPDATE Instructors
                        SET FirstName = '{txtFirstName.Text.Trim()}', 
                            LastName = '{txtLastName.Text.Trim()}', 
                            DateOfBirth = '{dteBirth.Value.Year}-{dteBirth.Value.Month}-{dteBirth.Value.Day}', 
                            HiredDate = '{dteHired.Value.Year}-{dteHired.Value.Month}-{dteHired.Value.Day}', 
                            PhoneNumber = '{txtPhone.Text}', 
                            Email = '{txtEmail.Text}', 
                            LicenceNumber = {txtLicenceNumber.Text}, 
                            Age = {txtAge.Text}, 
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
                                {txtAge.Text},
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
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
