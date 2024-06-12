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
    public partial class frmBrowseStudents : Form
    {
        public frmBrowseStudents()
        {
            InitializeComponent();
        }

        private void frmBrowseStudent_Load(object sender, EventArgs e)
        {
            try
            {
                dgvStudents.Visible = false;
                dgvStudents.Visible = false;

                LoadStudents();
                DisplayStatus("Ready");
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void cmbStudent_SelectedValueChanged(object sender, EventArgs e)
        {
            ClearParentStatusStrip();
            DisplayStatus("Ready");
            try
            {
                string sql = $@"
                   	            SELECT 
		            Instructors.LastName + ', ' + Instructors.FirstName AS FullName,
		            Instructors.PhoneNumber, Lessons.LessonDate,
		            Lessons.LessonStartTime, Lessons.LessonEndTime,
		            Lessons.PickupLocation, Lessons.LessonType, 
		            Cars.Make + ' ' + Cars.Model AS CarName
	            FROM Students
	            INNER JOIN Lessons
	            ON Students.StudentID = Lessons.StudentID
	            INNER JOIN Instructors
	            ON Lessons.InstructorID = Instructors.InstructorID
	            INNER JOIN Cars
	            ON Lessons.CarID = Cars.CarID
            ";

                if (cmbStudents.SelectedIndex > 0)
                {
                    sql += $@"WHERE Students.StudentID = '{cmbStudents.SelectedValue}'";
                }
                else
                {
                    sql += $@"";
                }

                DataTable dt = DataAccess.GetData(sql);

                if (dt.Rows.Count > 0)
                {
                    dgvStudents.Visible = true;
                    dgvStudents.DataSource = dt;

                    dgvStudents.ReadOnly = true;
                    dgvStudents.AutoResizeColumns();

                    dgvStudents.Columns[0].HeaderText = "Instructor Name";
                    dgvStudents.Columns[1].HeaderText = "Instructor No";
                    dgvStudents.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvStudents.Columns[2].HeaderText = "Lesson Date";
                    dgvStudents.Columns[3].HeaderText = "Lesson Starts At";
                    dgvStudents.Columns[4].HeaderText = "Lesson Ends At";
                    dgvStudents.Columns[5].HeaderText = "Pickup Location";
                    dgvStudents.Columns[6].HeaderText = "Lesson Type";
                    dgvStudents.Columns[7].HeaderText = "Car";
                }
                else
                {
                    dgvStudents.DataSource = null;
                    dgvStudents.Visible = false;
                    MessageBox.Show("There were no records to display");
                }
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        private void dgvStudents_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string instructorName = Convert.ToString(dgvStudents.CurrentRow.Cells["FullName"].Value);

            string sql = $"select Email FROM Instructors WHERE LastName + ', ' + FirstName = '{instructorName}'";

            string email = Convert.ToString(DataAccess.GetValue(sql));
            MessageBox.Show($"The Instructor's email is: {email}");

        }

        private void LoadStudents()
        {
            string sqlStudents = "SELECT StudentID, LastName + ', '+ FirstName AS FullName " +
               "FROM Students ORDER BY LastName, FirstName";
            UIUtilities.Bind(cmbStudents, "FullName", "StudentID", DataAccess.GetData(sqlStudents), addEmptyRow: true, "-- Select a Student --");
        }

        private void DisplayExceptionMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, ex.GetType().ToString());
        }

        public void DisplayCurrentPositionOnStripLabel(int rowCount, int studentID)
        {
            string sqlQuery = $"SELECT LastName FROM Students WHERE StudentID = {studentID}";

            string lastName = Convert.ToString(DataAccess.GetValue(sqlQuery));

            this.DisplayParentStatusStripMessage($"current Student {lastName} has {rowCount} Instructors.");
        }
        public void DisplayStatus(string status)
        {
            this.DisplayParentStatusStripMessage($"{status}...");
        }
        private void ClearParentStatusStrip() => this.DisplayParentStatusStripMessage(string.Empty);

        private void dgvStudents_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ClearParentStatusStrip();
            if (cmbStudents.SelectedIndex > 0)
            {
                DisplayCurrentPositionOnStripLabel(dgvStudents.RowCount, Convert.ToInt32(cmbStudents.SelectedValue));
            }
            else
            {
                DisplayStatus("Displaying all Instructors");
            }
        }

        private void frmBrowseStudents_Leave(object sender, EventArgs e)
        {
            ClearParentStatusStrip();
        }
    }
}
