using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using RemoteDesktop.RemoteWeb;
namespace RemoteDesktop
{
    public partial class frmChangePassword : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }
        public string OpType = "";
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                KryptonMessageBox.Show("Enter Password","Remote Services", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                txtPassword.Focus();
                return;
            }
            if (txtNewPassword.Text == "")
            {
                KryptonMessageBox.Show("Enter New Password", "Remote Services", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNewPassword.Focus();
                return;
            }
            clsRemoteWebService objRM = new clsRemoteWebService();
            KryptonMessageBox.Show(objRM.ResetPassword(OpType, txtPassword.Text, txtNewPassword.Text, clsSetting._EmployeeID), "Remote Services", MessageBoxButtons.OK, MessageBoxIcon.Information);
            objRM = null;
        }
    }
}