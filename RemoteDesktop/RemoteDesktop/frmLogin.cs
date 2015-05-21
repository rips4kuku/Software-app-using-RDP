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
    public partial class frmLogin : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void buttonSpecHeaderGroup1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserID.Text.Trim() == "")
                {
                    KryptonMessageBox.Show("Please Enter UserID", "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUserID.Focus();
                    return;
                }
                if (txtPwd.Text.Trim() == "")
                {
                    KryptonMessageBox.Show("Please Enter Password", "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPwd.Focus();
                    return;
                }
                clsRemoteWebService objRW = new clsRemoteWebService();
                DataSet Ds = new DataSet();
                Ds = objRW.AuthenticateEmployee(txtUserID.Text, txtPwd.Text);
                if (Ds != null)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        if (Ds.Tables[0].Rows[0]["Password"].ToString() == txtPwd.Text)
                        {
                            clsSetting._EmployeeID = Convert.ToInt32(Ds.Tables[0].Rows[0]["EmployeeID"].ToString());
                            clsSetting._EmployeeName = Ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            KryptonMessageBox.Show("Invalid UserID or Password", "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                        KryptonMessageBox.Show("Invalid UserID or Password", "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                    KryptonMessageBox.Show("Invalid UserID or Password", "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                Ds = null;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.ToString(), "Remote Services", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}