using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingSchoolManagementSystem
{
    public static class UIUtilities
    {
        public static void ClearChildControls(this Control control, int defaultSelectedIndex = 0)
        {
            ClearControls(control.Controls, defaultSelectedIndex);
        }
        public static void ClearControls(this Control.ControlCollection constrolsCollection, int defaultSelectedIndex = 0)
        {
            foreach (Control control in constrolsCollection)
            {
                switch (control)
                {
                    case TextBox textBox:
                        textBox.Text = string.Empty;
                        break;
                    case CheckBox checkBox:
                        checkBox.Checked = false;
                        break;
                    case RadioButton radioButton:
                        radioButton.Checked = false;
                        break;
                    case ComboBox combo:
                        combo.SelectedIndex = defaultSelectedIndex;
                        break;
                    case ListBox listBox:
                        listBox.SelectedIndex = defaultSelectedIndex;
                        break;
                    case GroupBox groupBox:
                        ClearControls(groupBox.Controls);
                        break;
                    case Panel panel:
                        ClearControls(panel.Controls);
                        break;
                }
            }
        }
        public static void Bind(this ComboBox cmb, string emptyColumn, string nullColumn, object? dataSource)
        {
            cmb.DataSource = dataSource;
            cmb.ValueMember = nullColumn;
            cmb.DisplayMember = emptyColumn;
        }
        public static void AddEmptyRow(this DataTable dt, string displayMember, string valueMember)
        {
            DataRow dr = dt.NewRow();
            dr[displayMember] = string.Empty;
            dr[valueMember] = DBNull.Value;
            dt.Rows.InsertAt(dr, 0);
        }

        public static void ClearStatusStripLabel(ToolStripStatusLabel label)
        {
            label.Text = string.Empty;
        }

        public static void DisplayParentStatusStripMessage(this Form form, string message, Color? color = null)
        {
            frmMDIParent? parentMdi = form.MdiParent as frmMDIParent;

            if (parentMdi != null)
            {
                if (color == null)
                {
                    parentMdi.DisplayParentStatusStripMessage(message);
                }
                else
                {
                     parentMdi.DisplayStatusMessage(message, color.Value);
                }
            }
        }
    }
    
}
