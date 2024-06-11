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
    public partial class frmBrowseInstructors : Form
    {
        public frmBrowseInstructors()
        {
            InitializeComponent();
        }

        private void frmBrowseInstructors_Load(object sender, EventArgs e)
        {
            try
            {
                dgvInstructors.Visible = false;
                dgvInstructors.Visible = false;

                LoadInstructors();
                DisplayStatus("Ready");
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void cmbInstructors_SelectedValueChanged(object sender, EventArgs e)
        {
            ClearParentStatusStrip();
            DisplayStatus("Ready");
            try
            {
                string sql = $@"
                   SELECT 
	                    Students.LastName + ', ' + Students.FirstName AS FullName,
	                    Students.PhoneNumber, Lessons.LessonDate,
	                    Lessons.LessonStartTime, Lessons.LessonEndTime,
	                    Lessons.PickupLocation, Lessons.LessonType, 
	                    Cars.Make + ' ' + Cars.Model AS CarName
                    FROM Instructors
	                    INNER JOIN Lessons
	                    ON Instructors.InstructorID = Lessons.InstructorID
	                    INNER JOIN Students
	                    ON Lessons.StudentID = Students.StudentID
	                    INNER JOIN Cars
	                    ON Lessons.CarID = Cars.CarID
            ";

                if (cmbInstructors.SelectedIndex > 0)
                {
                    sql += $@"WHERE Instructors.InstructorID = '{cmbInstructors.SelectedValue}'";
                }
                else
                {
                    sql += $@"";
                }

                DataTable dt = DataAccess.GetData(sql);

                if (dt.Rows.Count > 0)
                {
                    dgvInstructors.Visible = true;
                    dgvInstructors.DataSource = dt;

                    dgvInstructors.ReadOnly = true;
                    dgvInstructors.AutoResizeColumns();

                    dgvInstructors.Columns[0].HeaderText = "Student Name";
                    dgvInstructors.Columns[1].HeaderText = "Student No";
                    dgvInstructors.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvInstructors.Columns[2].HeaderText = "Lesson Date";
                    dgvInstructors.Columns[3].HeaderText = "Order Starts At";
                    dgvInstructors.Columns[4].HeaderText = "Order Ends At";
                    dgvInstructors.Columns[5].HeaderText = "Pickup Location";
                    dgvInstructors.Columns[6].HeaderText = "Lesson Type";
                    dgvInstructors.Columns[7].HeaderText = "Car";
                }
                else
                {
                    dgvInstructors.DataSource = null;
                    dgvInstructors.Visible = false;
                    MessageBox.Show("There were no records to display");
                }
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void dgvInstructors_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string studentName = Convert.ToString(dgvInstructors.CurrentRow.Cells["FullName"].Value);

            string sql = $"select DueFees FROM Students WHERE LastName + ', ' + FirstName = '{studentName}'";

            decimal dueFees = Convert.ToDecimal(DataAccess.GetValue(sql));
            MessageBox.Show($"The student's due Fees is: {dueFees:c}");

        }

        private void LoadInstructors()
        {
            string sqlInstructors = "SELECT InstructorID, LastName + ', '+ FirstName AS FullName " +
               "FROM Instructors ORDER BY LastName, FirstName";
            UIUtilities.Bind(cmbInstructors, "FullName", "InstructorId", DataAccess.GetData(sqlInstructors), addEmptyRow: true, "-- Select an Instructor --");
        }

        private void DisplayExceptionMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, ex.GetType().ToString());
        }

        public void DisplayCurrentPositionOnStripLabel(int rowCount, int InstructorId)
        {
            string sqlQuery = $"SELECT LastName FROM Instructors WHERE InstructorID = {InstructorId}";

            string lastName = Convert.ToString(DataAccess.GetValue(sqlQuery));

            this.DisplayParentStatusStripMessage($"current Instructor {lastName} has {rowCount} Students.");
        }
        public void DisplayStatus(string status)
        {
            this.DisplayParentStatusStripMessage($"{status}...");
        }
        private void ClearParentStatusStrip() => this.DisplayParentStatusStripMessage(string.Empty);

        private void dgvInstructors_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ClearParentStatusStrip();
            if (cmbInstructors.SelectedIndex > 0)
            {
                DisplayCurrentPositionOnStripLabel(dgvInstructors.RowCount, Convert.ToInt32(cmbInstructors.SelectedValue)); 
            }
            else
            {
                DisplayStatus("Displaying all students");
            }
        }

        private void frmBrowseInstructors_Leave(object sender, EventArgs e)
        {
            ClearParentStatusStrip();
        }
    }
}
