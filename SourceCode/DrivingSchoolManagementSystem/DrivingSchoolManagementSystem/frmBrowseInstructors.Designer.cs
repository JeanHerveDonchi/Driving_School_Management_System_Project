namespace DrivingSchoolManagementSystem
{
    partial class frmBrowseInstructors
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
            label1 = new Label();
            groupBox1 = new GroupBox();
            cmbInstructors = new ComboBox();
            label2 = new Label();
            dgvInstructors = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInstructors).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(247, 29);
            label1.Name = "label1";
            label1.Size = new Size(196, 30);
            label1.TabIndex = 0;
            label1.Text = "Browse Instructors";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(cmbInstructors);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(143, 72);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(442, 89);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Choose an instructor";
            // 
            // cmbInstructors
            // 
            cmbInstructors.FormattingEnabled = true;
            cmbInstructors.Location = new Point(76, 36);
            cmbInstructors.Name = "cmbInstructors";
            cmbInstructors.Size = new Size(338, 23);
            cmbInstructors.TabIndex = 1;
            cmbInstructors.SelectedValueChanged += cmbInstructors_SelectedValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 39);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 0;
            label2.Text = "Name:";
            // 
            // dgvInstructors
            // 
            dgvInstructors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvInstructors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInstructors.Location = new Point(30, 241);
            dgvInstructors.Name = "dgvInstructors";
            dgvInstructors.Size = new Size(644, 205);
            dgvInstructors.TabIndex = 2;
            dgvInstructors.RowEnter += dgvInstructors_RowEnter;
            dgvInstructors.RowHeaderMouseClick += dgvInstructors_RowHeaderMouseClick;
            // 
            // frmBrowseInstructors
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(709, 470);
            Controls.Add(dgvInstructors);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "frmBrowseInstructors";
            Text = "Browse Instructors";
            Load += frmBrowseInstructors_Load;
            Leave += frmBrowseInstructors_Leave;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInstructors).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private ComboBox cmbInstructors;
        private Label label2;
        private DataGridView dgvInstructors;
    }
}