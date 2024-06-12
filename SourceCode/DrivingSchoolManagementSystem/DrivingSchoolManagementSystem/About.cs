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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            rtxtAbout.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtxtAbout.Text = @"
                Overview: 

                A DSMS is a tool that streamlines the planning, organization, and execution of
                theoretical and practical driving lessons. 

 
                Features: 

                Authenticated users are able to: 

                - Enroll students in lessons 

                - Manage instructors 

                - Cancel, reschedule lessons. 

                Business Rules 

                - Students must be at least 16 years old to enroll in driving lessons 

                - Lessons with the same instructor must not have overlapping dates AND times. 

                - A student cannot have more than 1 lesson scheduled on the same day 

                - A fee of 60$ per hour is applied to students each time a lesson is booked for them. 
                    This will be added to the students' due; this also updates when they have a lesson cancelled
                    or changed in the duration 

                - Lessons should be between 30 minutes and 3HOURS 

                - Lessons using the same car must not have overlapping dates AND times. 
                ";
        }
    }
}
