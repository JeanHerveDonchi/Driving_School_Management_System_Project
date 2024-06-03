namespace DrivingSchoolManagementSystem
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            lblProduct.Text = Application.ProductName;
            lblVersion.Text = Application.ProductVersion;
            lblCompanyName.Text = Application.CompanyName;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {  
            prgLoading.Value += 5;

            if (prgLoading.Value >= 99)
            {
                DialogResult = DialogResult.OK;
                timer1.Enabled = false;
            }
        }
    }
}
