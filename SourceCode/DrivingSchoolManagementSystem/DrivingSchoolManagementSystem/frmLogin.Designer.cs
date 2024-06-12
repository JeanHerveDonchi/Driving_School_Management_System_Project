namespace DrivingSchoolManagementSystem
{
    partial class frmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            txtPassword = new TextBox();
            label2 = new Label();
            txtUserName = new TextBox();
            label1 = new Label();
            btnLogin = new Button();
            btnCancel = new Button();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(txtPassword);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtUserName);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(85, 93);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(309, 197);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(38, 125);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(235, 23);
            txtPassword.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(38, 101);
            label2.Name = "label2";
            label2.Size = new Size(70, 17);
            label2.TabIndex = 2;
            label2.Text = "Password:";
            // 
            // txtUserName
            // 
            txtUserName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUserName.Location = new Point(38, 69);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(235, 23);
            txtUserName.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(38, 43);
            label1.Name = "label1";
            label1.Size = new Size(79, 17);
            label1.TabIndex = 0;
            label1.Text = "User Name:";
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.BackColor = Color.ForestGreen;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = SystemColors.ButtonHighlight;
            btnLogin.Location = new Point(85, 349);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(117, 41);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCancel.BackColor = Color.OrangeRed;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = SystemColors.ButtonHighlight;
            btnCancel.Location = new Point(277, 349);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(117, 41);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Login_Image;
            pictureBox1.Location = new Point(-1, 42);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(479, 406);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(477, 485);
            Controls.Add(btnCancel);
            Controls.Add(btnLogin);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Name = "frmLogin";
            Text = "Form1";
            Load += frmLogin_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtPassword;
        private Label label2;
        private TextBox txtUserName;
        private Label label1;
        private Button btnLogin;
        private Button btnCancel;
        private PictureBox pictureBox1;
    }
}
