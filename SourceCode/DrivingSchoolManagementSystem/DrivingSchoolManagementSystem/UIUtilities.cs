using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolManagementSystem
{
    public static class UIUtilities
    {
        public static void ClearControls(this Control.ControlCollection constrolsCollection)
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
                    case ComboBox combo:
                        combo.SelectedIndex = 0;
                        break;
                    case GroupBox groupBox:
                        ClearControls(groupBox.Controls);
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
    }
    
}
