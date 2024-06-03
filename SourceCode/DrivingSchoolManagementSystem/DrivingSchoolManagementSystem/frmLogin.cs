using System.Configuration;

namespace DrivingSchoolManagementSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - Login";
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim()))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Login failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            string sqlLogin = $"SELECT COUNT(*) FROM Login WHERE Username = '{username}' AND PasswordHash = '{password}'";
            int result = Convert.ToInt32(DataAccess.GetValue(sqlLogin));
            return result > 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
