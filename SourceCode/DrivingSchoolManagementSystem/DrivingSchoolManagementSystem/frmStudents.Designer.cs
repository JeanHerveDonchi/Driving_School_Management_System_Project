namespace DrivingSchoolManagementSystem
{
    partial class frmStudents
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
            grpStudents = new GroupBox();
            txtDueFees = new TextBox();
            label12 = new Label();
            txtLearnersLicenceNumber = new TextBox();
            label10 = new Label();
            chkHasLearnersLicence = new CheckBox();
            txtAge = new TextBox();
            Age = new Label();
            txtAddress = new TextBox();
            label11 = new Label();
            label9 = new Label();
            txtEmail = new TextBox();
            label8 = new Label();
            dteAdmission = new DateTimePicker();
            label7 = new Label();
            txtPhone = new TextBox();
            label6 = new Label();
            dteBirth = new DateTimePicker();
            label5 = new Label();
            txtLastName = new TextBox();
            label4 = new Label();
            txtFirstName = new TextBox();
            label3 = new Label();
            txtId = new TextBox();
            label2 = new Label();
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
            grpStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(368, 20);
            label1.Name = "label1";
            label1.Size = new Size(107, 32);
            label1.TabIndex = 0;
            label1.Text = "Students";
            // 
            // grpStudents
            // 
            grpStudents.Controls.Add(txtDueFees);
            grpStudents.Controls.Add(label12);
            grpStudents.Controls.Add(txtLearnersLicenceNumber);
            grpStudents.Controls.Add(label10);
            grpStudents.Controls.Add(chkHasLearnersLicence);
            grpStudents.Controls.Add(txtAge);
            grpStudents.Controls.Add(Age);
            grpStudents.Controls.Add(txtAddress);
            grpStudents.Controls.Add(label11);
            grpStudents.Controls.Add(label9);
            grpStudents.Controls.Add(txtEmail);
            grpStudents.Controls.Add(label8);
            grpStudents.Controls.Add(dteAdmission);
            grpStudents.Controls.Add(label7);
            grpStudents.Controls.Add(txtPhone);
            grpStudents.Controls.Add(label6);
            grpStudents.Controls.Add(dteBirth);
            grpStudents.Controls.Add(label5);
            grpStudents.Controls.Add(txtLastName);
            grpStudents.Controls.Add(label4);
            grpStudents.Controls.Add(txtFirstName);
            grpStudents.Controls.Add(label3);
            grpStudents.Controls.Add(txtId);
            grpStudents.Controls.Add(label2);
            grpStudents.Location = new Point(109, 55);
            grpStudents.Name = "grpStudents";
            grpStudents.Size = new Size(620, 699);
            grpStudents.TabIndex = 1;
            grpStudents.TabStop = false;
            grpStudents.Text = "Student Details";
            // 
            // txtDueFees
            // 
            txtDueFees.Location = new Point(43, 648);
            txtDueFees.Name = "txtDueFees";
            txtDueFees.ReadOnly = true;
            txtDueFees.Size = new Size(537, 23);
            txtDueFees.TabIndex = 26;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(45, 629);
            label12.Name = "label12";
            label12.Size = new Size(57, 15);
            label12.TabIndex = 25;
            label12.Text = "Due Fees:";
            // 
            // txtLearnersLicenceNumber
            // 
            txtLearnersLicenceNumber.Location = new Point(46, 486);
            txtLearnersLicenceNumber.Name = "txtLearnersLicenceNumber";
            txtLearnersLicenceNumber.Size = new Size(534, 23);
            txtLearnersLicenceNumber.TabIndex = 24;
            txtLearnersLicenceNumber.Validating += txt_Validating;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(45, 468);
            label10.Name = "label10";
            label10.Size = new Size(143, 15);
            label10.TabIndex = 23;
            label10.Text = "Leaner's Licence Number:";
            // 
            // chkHasLearnersLicence
            // 
            chkHasLearnersLicence.AutoSize = true;
            chkHasLearnersLicence.Location = new Point(48, 437);
            chkHasLearnersLicence.Name = "chkHasLearnersLicence";
            chkHasLearnersLicence.Size = new Size(15, 14);
            chkHasLearnersLicence.TabIndex = 22;
            chkHasLearnersLicence.UseVisualStyleBackColor = true;
            chkHasLearnersLicence.CheckedChanged += chkHasLearnersLicence_CheckedChanged;
            // 
            // txtAge
            // 
            txtAge.Location = new Point(46, 538);
            txtAge.Name = "txtAge";
            txtAge.ReadOnly = true;
            txtAge.Size = new Size(534, 23);
            txtAge.TabIndex = 21;
            // 
            // Age
            // 
            Age.AutoSize = true;
            Age.Location = new Point(46, 520);
            Age.Name = "Age";
            Age.Size = new Size(31, 15);
            Age.TabIndex = 20;
            Age.Text = "Age:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(43, 593);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "St Number. St Name. (Appt No)";
            txtAddress.Size = new Size(537, 23);
            txtAddress.TabIndex = 19;
            txtAddress.Tag = "Address";
            txtAddress.Validating += txt_Validating;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(46, 575);
            label11.Name = "label11";
            label11.Size = new Size(52, 15);
            label11.TabIndex = 18;
            label11.Text = "Address:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(43, 419);
            label9.Name = "label9";
            label9.Size = new Size(120, 15);
            label9.TabIndex = 14;
            label9.Text = "Has Learners Licence:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(43, 384);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "email@example.domain";
            txtEmail.Size = new Size(537, 23);
            txtEmail.TabIndex = 13;
            txtEmail.Tag = "Email";
            txtEmail.Validating += txt_Validating;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(43, 366);
            label8.Name = "label8";
            label8.Size = new Size(39, 15);
            label8.TabIndex = 12;
            label8.Text = "Email:";
            // 
            // dteAdmission
            // 
            dteAdmission.Location = new Point(43, 273);
            dteAdmission.MinDate = new DateTime(2016, 1, 1, 0, 0, 0, 0);
            dteAdmission.Name = "dteAdmission";
            dteAdmission.Size = new Size(200, 23);
            dteAdmission.TabIndex = 11;
            dteAdmission.Tag = "Hired Date";
            dteAdmission.Validating += dte_Validating;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(43, 255);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 10;
            label7.Text = "Admission Date";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(43, 328);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(537, 23);
            txtPhone.TabIndex = 9;
            txtPhone.Tag = "Phone Number";
            txtPhone.Validating += txt_Validating;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(43, 310);
            label6.Name = "label6";
            label6.Size = new Size(91, 15);
            label6.TabIndex = 8;
            label6.Text = "Phone Number:";
            // 
            // dteBirth
            // 
            dteBirth.Location = new Point(43, 217);
            dteBirth.MaxDate = new DateTime(2146, 12, 31, 0, 0, 0, 0);
            dteBirth.MinDate = new DateTime(1964, 1, 1, 0, 0, 0, 0);
            dteBirth.Name = "dteBirth";
            dteBirth.Size = new Size(200, 23);
            dteBirth.TabIndex = 7;
            dteBirth.Tag = "Date Of Birth";
            dteBirth.Value = new DateTime(2000, 12, 31, 0, 0, 0, 0);
            dteBirth.Validating += dte_Validating;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(43, 199);
            label5.Name = "label5";
            label5.Size = new Size(78, 15);
            label5.TabIndex = 6;
            label5.Text = "Date Of Birth:";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(43, 161);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(537, 23);
            txtLastName.TabIndex = 5;
            txtLastName.Tag = "Last Name";
            txtLastName.Validating += txt_Validating;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 143);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 4;
            label4.Text = "Last Name:";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(43, 104);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(537, 23);
            txtFirstName.TabIndex = 3;
            txtFirstName.Tag = "First Name";
            txtFirstName.Validating += txt_Validating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 86);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 2;
            label3.Text = "First Name:";
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
            // btnAdd
            // 
            btnAdd.Location = new Point(135, 776);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 33);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(260, 777);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 33);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(382, 777);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 32);
            btnSave.TabIndex = 4;
            btnSave.Text = "Update";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(628, 778);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 32);
            btnExit.TabIndex = 5;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(502, 778);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnFirst
            // 
            btnFirst.Location = new Point(274, 840);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(41, 32);
            btnFirst.TabIndex = 7;
            btnFirst.Text = "<<";
            btnFirst.UseVisualStyleBackColor = true;
            btnFirst.Click += Navigation_Handler;
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new Point(346, 840);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(41, 32);
            btnPrevious.TabIndex = 8;
            btnPrevious.Text = "<";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += Navigation_Handler;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(425, 840);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(41, 32);
            btnNext.TabIndex = 9;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += Navigation_Handler;
            // 
            // btnLast
            // 
            btnLast.Location = new Point(502, 840);
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
            // frmStudents
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(845, 908);
            Controls.Add(btnLast);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(btnFirst);
            Controls.Add(btnCancel);
            Controls.Add(btnExit);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(grpStudents);
            Controls.Add(label1);
            Name = "frmStudents";
            Text = "Add Students";
            Load += frmStudents_Load;
            grpStudents.ResumeLayout(false);
            grpStudents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox grpStudents;
        private Label label2;
        private TextBox txtLastName;
        private Label label4;
        private TextBox txtFirstName;
        private Label label3;
        private TextBox txtId;
        private TextBox txtPhone;
        private Label label6;
        private DateTimePicker dteBirth;
        private Label label5;
        private Label label8;
        private DateTimePicker dteAdmission;
        private Label label7;
        private TextBox txtEmail;
        private Label label9;
        private TextBox txtAddress;
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
        private TextBox txtAge;
        private Label Age;
        private CheckBox chkHasLearnersLicence;
        private Label label10;
        private TextBox txtLearnersLicenceNumber;
        private Label label12;
        private TextBox txtDueFees;
    }
}