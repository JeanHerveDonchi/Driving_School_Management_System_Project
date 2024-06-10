namespace DrivingSchoolManagementSystem
{
    partial class frmLessons
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            grpLessons = new GroupBox();
            grpLessonType = new GroupBox();
            rdoPractical = new RadioButton();
            rdoTheoric = new RadioButton();
            cmbCars = new ComboBox();
            label8 = new Label();
            cmbInstructors = new ComboBox();
            label7 = new Label();
            cmbStudents = new ComboBox();
            label6 = new Label();
            grpEndTime = new GroupBox();
            rdoEndTimePM = new RadioButton();
            rdoEndTimeAM = new RadioButton();
            label4 = new Label();
            txtEndTimeMin = new TextBox();
            txtEndTimeHour = new TextBox();
            txtDuration = new TextBox();
            Age = new Label();
            txtPickupLocation = new TextBox();
            label11 = new Label();
            dteLessonDate = new DateTimePicker();
            label5 = new Label();
            txtId = new TextBox();
            label2 = new Label();
            grpStartTime = new GroupBox();
            rdoStartTimePM = new RadioButton();
            rdoStartTimeAM = new RadioButton();
            label3 = new Label();
            txtStartTimeMin = new TextBox();
            txtStartTimeHour = new TextBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnExit = new Button();
            btnCancel = new Button();
            btnFirst = new Button();
            btnPrevious = new Button();
            btnNext = new Button();
            btnLast = new Button();
            errorProvider1 = new ErrorProvider(components);
            grpLessons.SuspendLayout();
            grpLessonType.SuspendLayout();
            grpEndTime.SuspendLayout();
            grpStartTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(368, 20);
            label1.Name = "label1";
            label1.Size = new Size(96, 32);
            label1.TabIndex = 0;
            label1.Text = "Lessons";
            // 
            // grpLessons
            // 
            grpLessons.Controls.Add(grpLessonType);
            grpLessons.Controls.Add(cmbCars);
            grpLessons.Controls.Add(label8);
            grpLessons.Controls.Add(cmbInstructors);
            grpLessons.Controls.Add(label7);
            grpLessons.Controls.Add(cmbStudents);
            grpLessons.Controls.Add(label6);
            grpLessons.Controls.Add(grpEndTime);
            grpLessons.Controls.Add(txtDuration);
            grpLessons.Controls.Add(Age);
            grpLessons.Controls.Add(txtPickupLocation);
            grpLessons.Controls.Add(label11);
            grpLessons.Controls.Add(dteLessonDate);
            grpLessons.Controls.Add(label5);
            grpLessons.Controls.Add(txtId);
            grpLessons.Controls.Add(label2);
            grpLessons.Controls.Add(grpStartTime);
            grpLessons.Location = new Point(109, 55);
            grpLessons.Name = "grpLessons";
            grpLessons.Size = new Size(620, 672);
            grpLessons.TabIndex = 1;
            grpLessons.TabStop = false;
            grpLessons.Text = "Lesson Details";
            // 
            // grpLessonType
            // 
            grpLessonType.Controls.Add(rdoPractical);
            grpLessonType.Controls.Add(rdoTheoric);
            grpLessonType.Location = new Point(43, 510);
            grpLessonType.Name = "grpLessonType";
            grpLessonType.Size = new Size(534, 60);
            grpLessonType.TabIndex = 39;
            grpLessonType.TabStop = false;
            grpLessonType.Text = "Lesson Type:";
            // 
            // rdoPractical
            // 
            rdoPractical.AutoSize = true;
            rdoPractical.Location = new Point(146, 25);
            rdoPractical.Name = "rdoPractical";
            rdoPractical.Size = new Size(169, 19);
            rdoPractical.TabIndex = 1;
            rdoPractical.TabStop = true;
            rdoPractical.Text = "Practical(Behind the wheel)";
            rdoPractical.UseVisualStyleBackColor = true;
            // 
            // rdoTheoric
            // 
            rdoTheoric.AutoSize = true;
            rdoTheoric.Location = new Point(53, 25);
            rdoTheoric.Name = "rdoTheoric";
            rdoTheoric.Size = new Size(64, 19);
            rdoTheoric.TabIndex = 0;
            rdoTheoric.TabStop = true;
            rdoTheoric.Tag = "Theoric";
            rdoTheoric.Text = "Theoric";
            rdoTheoric.UseVisualStyleBackColor = true;
            // 
            // cmbCars
            // 
            cmbCars.FormattingEnabled = true;
            cmbCars.Location = new Point(43, 216);
            cmbCars.Name = "cmbCars";
            cmbCars.Size = new Size(537, 23);
            cmbCars.TabIndex = 38;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(43, 198);
            label8.Name = "label8";
            label8.Size = new Size(28, 15);
            label8.TabIndex = 37;
            label8.Text = "Car:";
            // 
            // cmbInstructors
            // 
            cmbInstructors.FormattingEnabled = true;
            cmbInstructors.Location = new Point(43, 162);
            cmbInstructors.Name = "cmbInstructors";
            cmbInstructors.Size = new Size(537, 23);
            cmbInstructors.TabIndex = 36;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(43, 144);
            label7.Name = "label7";
            label7.Size = new Size(61, 15);
            label7.TabIndex = 35;
            label7.Text = "Instructor:";
            // 
            // cmbStudents
            // 
            cmbStudents.FormattingEnabled = true;
            cmbStudents.Location = new Point(43, 109);
            cmbStudents.Name = "cmbStudents";
            cmbStudents.Size = new Size(537, 23);
            cmbStudents.TabIndex = 34;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(43, 90);
            label6.Name = "label6";
            label6.Size = new Size(51, 15);
            label6.TabIndex = 33;
            label6.Text = "Student:";
            // 
            // grpEndTime
            // 
            grpEndTime.Controls.Add(rdoEndTimePM);
            grpEndTime.Controls.Add(rdoEndTimeAM);
            grpEndTime.Controls.Add(label4);
            grpEndTime.Controls.Add(txtEndTimeMin);
            grpEndTime.Controls.Add(txtEndTimeHour);
            grpEndTime.Location = new Point(43, 375);
            grpEndTime.Name = "grpEndTime";
            grpEndTime.Size = new Size(537, 59);
            grpEndTime.TabIndex = 32;
            grpEndTime.TabStop = false;
            grpEndTime.Text = "Lesson End Time";
            // 
            // rdoEndTimePM
            // 
            rdoEndTimePM.AutoSize = true;
            rdoEndTimePM.Location = new Point(172, 25);
            rdoEndTimePM.Name = "rdoEndTimePM";
            rdoEndTimePM.Size = new Size(43, 19);
            rdoEndTimePM.TabIndex = 31;
            rdoEndTimePM.TabStop = true;
            rdoEndTimePM.Text = "PM";
            rdoEndTimePM.UseVisualStyleBackColor = true;
            // 
            // rdoEndTimeAM
            // 
            rdoEndTimeAM.AutoSize = true;
            rdoEndTimeAM.Location = new Point(122, 25);
            rdoEndTimeAM.Name = "rdoEndTimeAM";
            rdoEndTimeAM.Size = new Size(44, 19);
            rdoEndTimeAM.TabIndex = 30;
            rdoEndTimeAM.TabStop = true;
            rdoEndTimeAM.Text = "AM";
            rdoEndTimeAM.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(53, 27);
            label4.Name = "label4";
            label4.Size = new Size(10, 15);
            label4.TabIndex = 28;
            label4.Text = ":";
            // 
            // txtEndTimeMin
            // 
            txtEndTimeMin.Location = new Point(65, 23);
            txtEndTimeMin.Name = "txtEndTimeMin";
            txtEndTimeMin.PlaceholderText = "mm";
            txtEndTimeMin.Size = new Size(34, 23);
            txtEndTimeMin.TabIndex = 29;
            // 
            // txtEndTimeHour
            // 
            txtEndTimeHour.Location = new Point(17, 23);
            txtEndTimeHour.Name = "txtEndTimeHour";
            txtEndTimeHour.PlaceholderText = "hh";
            txtEndTimeHour.Size = new Size(34, 23);
            txtEndTimeHour.TabIndex = 27;
            // 
            // txtDuration
            // 
            txtDuration.Location = new Point(43, 467);
            txtDuration.Name = "txtDuration";
            txtDuration.ReadOnly = true;
            txtDuration.Size = new Size(534, 23);
            txtDuration.TabIndex = 21;
            // 
            // Age
            // 
            Age.AutoSize = true;
            Age.Location = new Point(46, 449);
            Age.Name = "Age";
            Age.Size = new Size(56, 15);
            Age.TabIndex = 20;
            Age.Text = "Duration:";
            // 
            // txtPickupLocation
            // 
            txtPickupLocation.Location = new Point(43, 611);
            txtPickupLocation.Name = "txtPickupLocation";
            txtPickupLocation.PlaceholderText = "St Number. St Name. (Appt No)";
            txtPickupLocation.Size = new Size(537, 23);
            txtPickupLocation.TabIndex = 19;
            txtPickupLocation.Tag = "Address";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(46, 593);
            label11.Name = "label11";
            label11.Size = new Size(95, 15);
            label11.TabIndex = 18;
            label11.Text = "Pickup Location:";
            // 
            // dteLessonDate
            // 
            dteLessonDate.Location = new Point(43, 276);
            dteLessonDate.MaxDate = new DateTime(2146, 12, 31, 0, 0, 0, 0);
            dteLessonDate.MinDate = new DateTime(1964, 1, 1, 0, 0, 0, 0);
            dteLessonDate.Name = "dteLessonDate";
            dteLessonDate.Size = new Size(200, 23);
            dteLessonDate.TabIndex = 7;
            dteLessonDate.Tag = "Lesson Date";
            dteLessonDate.Value = new DateTime(2000, 12, 31, 0, 0, 0, 0);
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(43, 256);
            label5.Name = "label5";
            label5.Size = new Size(73, 15);
            label5.TabIndex = 6;
            label5.Text = "Lesson Date:";
            // 
            // txtId
            // 
            txtId.Location = new Point(43, 51);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(537, 23);
            txtId.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 33);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 0;
            label2.Text = "Id:";
            // 
            // grpStartTime
            // 
            grpStartTime.Controls.Add(rdoStartTimePM);
            grpStartTime.Controls.Add(rdoStartTimeAM);
            grpStartTime.Controls.Add(label3);
            grpStartTime.Controls.Add(txtStartTimeMin);
            grpStartTime.Controls.Add(txtStartTimeHour);
            grpStartTime.Location = new Point(43, 310);
            grpStartTime.Name = "grpStartTime";
            grpStartTime.Size = new Size(537, 59);
            grpStartTime.TabIndex = 30;
            grpStartTime.TabStop = false;
            grpStartTime.Text = "Lesson Start Time";
            // 
            // rdoStartTimePM
            // 
            rdoStartTimePM.AutoSize = true;
            rdoStartTimePM.Location = new Point(172, 25);
            rdoStartTimePM.Name = "rdoStartTimePM";
            rdoStartTimePM.Size = new Size(43, 19);
            rdoStartTimePM.TabIndex = 31;
            rdoStartTimePM.TabStop = true;
            rdoStartTimePM.Text = "PM";
            rdoStartTimePM.UseVisualStyleBackColor = true;
            // 
            // rdoStartTimeAM
            // 
            rdoStartTimeAM.AutoSize = true;
            rdoStartTimeAM.Location = new Point(122, 25);
            rdoStartTimeAM.Name = "rdoStartTimeAM";
            rdoStartTimeAM.Size = new Size(44, 19);
            rdoStartTimeAM.TabIndex = 30;
            rdoStartTimeAM.TabStop = true;
            rdoStartTimeAM.Text = "AM";
            rdoStartTimeAM.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(53, 27);
            label3.Name = "label3";
            label3.Size = new Size(10, 15);
            label3.TabIndex = 28;
            label3.Text = ":";
            // 
            // txtStartTimeMin
            // 
            txtStartTimeMin.Location = new Point(65, 23);
            txtStartTimeMin.Name = "txtStartTimeMin";
            txtStartTimeMin.PlaceholderText = "mm";
            txtStartTimeMin.Size = new Size(34, 23);
            txtStartTimeMin.TabIndex = 29;
            // 
            // txtStartTimeHour
            // 
            txtStartTimeHour.Location = new Point(17, 23);
            txtStartTimeHour.Name = "txtStartTimeHour";
            txtStartTimeHour.PlaceholderText = "hh";
            txtStartTimeHour.Size = new Size(34, 23);
            txtStartTimeHour.TabIndex = 27;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(136, 748);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 33);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(261, 749);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 33);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(383, 749);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 32);
            btnSave.TabIndex = 4;
            btnSave.Text = "Update";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(629, 750);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 32);
            btnExit.TabIndex = 5;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(503, 750);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnFirst
            // 
            btnFirst.Location = new Point(275, 812);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(41, 32);
            btnFirst.TabIndex = 7;
            btnFirst.Text = "<<";
            btnFirst.UseVisualStyleBackColor = true;
            btnFirst.Click += Navigation_Handler;
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new Point(347, 812);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(41, 32);
            btnPrevious.TabIndex = 8;
            btnPrevious.Text = "<";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += Navigation_Handler;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(426, 812);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(41, 32);
            btnNext.TabIndex = 9;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += Navigation_Handler;
            // 
            // btnLast
            // 
            btnLast.Location = new Point(503, 812);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(41, 32);
            btnLast.TabIndex = 10;
            btnLast.Text = ">>";
            btnLast.UseVisualStyleBackColor = true;
            btnLast.Click += Navigation_Handler;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // frmLessons
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(845, 863);
            Controls.Add(btnLast);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(btnFirst);
            Controls.Add(btnCancel);
            Controls.Add(btnExit);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(grpLessons);
            Controls.Add(label1);
            Name = "frmLessons";
            Text = "frmLessons";
            Load += frmLessons_Load;
            grpLessons.ResumeLayout(false);
            grpLessons.PerformLayout();
            grpLessonType.ResumeLayout(false);
            grpLessonType.PerformLayout();
            grpEndTime.ResumeLayout(false);
            grpEndTime.PerformLayout();
            grpStartTime.ResumeLayout(false);
            grpStartTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox grpLessons;
        private Label label2;
        private TextBox txtId;
        private DateTimePicker dteLessonDate;
        private Label label5;
        private TextBox txtPickupLocation;
        private Label label11;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnSave;
        private Button btnExit;
        private Button btnCancel;
        private Button btnFirst;
        private Button btnPrevious;
        private Button btnNext;
        private Button btnLast;
        private ErrorProvider errorProvider1;
        private TextBox txtDuration;
        private Label Age;
        private GroupBox grpStartTime;
        private TextBox txtStartTimeMin;
        private Label label3;
        private TextBox txtStartTimeHour;
        private RadioButton rdoStartTimePM;
        private RadioButton rdoStartTimeAM;
        private GroupBox grpEndTime;
        private RadioButton rdoEndTimePM;
        private RadioButton rdoEndTimeAM;
        private Label label4;
        private TextBox txtEndTimeMin;
        private TextBox txtEndTimeHour;
        private Label label6;
        private ComboBox cmbCars;
        private Label label8;
        private ComboBox cmbInstructors;
        private Label label7;
        private ComboBox cmbStudents;
        private GroupBox grpLessonType;
        private RadioButton rdoPractical;
        private RadioButton rdoTheoric;
    }
}