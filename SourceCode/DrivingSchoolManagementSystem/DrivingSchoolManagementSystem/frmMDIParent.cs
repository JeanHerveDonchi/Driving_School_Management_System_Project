using System.Runtime.CompilerServices;

namespace DrivingSchoolManagementSystem
{
    public partial class frmMDIParent : Form
    {
        private int childFormNumber = 0;

        public frmMDIParent()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            //Form childForm = new Form();
            //childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            //childForm.Show();

            Form? childForm = null;
            ToolStripItem? item = sender as ToolStripItem;

            if (item == null)
                return;

            switch (item.Tag?.ToString()?.ToLower())
            {
                case "instructors":
                    childForm = new frmInstructors();
                    break;
                case "students":
                    childForm = new frmStudents();
                    break;
                case "lessons":
                    childForm = new frmLessons();
                    break;
                case "browseinstructors":
                    childForm = new frmBrowseInstructors();
                    break;
                case "browsestudents":
                    childForm = new frmBrowseStudents();
                    break;
                case "about":
                    childForm = new About();
                    break;
            }
            if (childForm != null)
            {
                //to ensure we can create only one form no matter how many times we click
                foreach (Form form in this.MdiChildren)
                {
                    if (form.GetType() == childForm.GetType())
                    {
                        form.Activate();
                        return;
                    }
                }
                childForm.MdiParent = this;
                childForm.Show();
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void frmMDIParent_Load(object sender, EventArgs e)
        {
            Splash splashScreen = new Splash();
            frmLogin loginScreen = new();
            splashScreen.ShowDialog();
            if (splashScreen.DialogResult != DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                loginScreen.ShowDialog();
            }

            if (loginScreen.DialogResult != DialogResult.OK)
            {
                this.Close();
            }

            tlstrpStatus.Text = "Ready...";
        }

        public void DisplayStatusMessage(string message)
        {
            DisplayStatusMessage(message, Color.Black);
        }

        public void DisplayStatusMessage(string message, Color color)
        {
            this.tlstrpStatus.Text = message;
            this.tlstrpStatus.ForeColor = color;
        }

        public void StartProgressBar(string startMessage, string endMessage)
        {
            toolStripProgressLabel.Text = startMessage;
            prgBar.Value = 0;
            this.statusStrip.Refresh();

            while (prgBar.Value < prgBar.Maximum)
            {
                Thread.Sleep(2);
                prgBar.Value += 1;
            }
            prgBar.Value = 100;

            toolStripProgressLabel.Text = endMessage;
        }
    }
}
