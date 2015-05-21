using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MyDBManager;
using RemoteDesktop.RemoteWeb;
using System.Net;

namespace RemoteDesktop
{
    public partial class frmSendRequest : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public frmSendRequest()
        {
            InitializeComponent();
           // LocalIPAddress();
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
                RemoteWeb.clsRemoteWebService objRW = new clsRemoteWebService();
                DataSet Ds = new DataSet();
                Ds = objRW.AuthenticateUser(txtUserID.Text, txtPwd.Text);
                if (Ds != null)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        if (Ds.Tables[0].Rows[0]["Password"].ToString() == txtPwd.Text)
                        {
                            clsSetting._CustomerID = Convert.ToInt32(Ds.Tables[0].Rows[0]["CustomerID"].ToString());
                            pnlLogin.Visible = false;
                            pnlSendRequest.Visible = true;
                            kryptonHeaderGroup1.ValuesSecondary.Heading = "Dear " + Ds.Tables[0].Rows[0]["CustomerName"].ToString() + "  Please Provide your windows Login details for sending request.";
                            txtWinLoginName.Text = System.Environment.UserName.ToString();
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

        private void buttonSpecHeaderGroup1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWinLoginName.Text.Trim() == "")
                {
                    KryptonMessageBox.Show("Please Enter Windows Login Name", "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtWinLoginName.Focus();
                    return;
                }
                if (txtWinPassword.Text.Trim() == "")
                {
                    KryptonMessageBox.Show("Please Enter Windows Password", "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtWinPassword.Focus();
                    return;
                }
                if (txtMessage.Text.Trim() == "")
                {
                    KryptonMessageBox.Show("Please Enter Your Message", "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMessage.Focus();
                    return;
                }

                RemoteWeb.clsRemoteWebService objRW = new clsRemoteWebService();
                Int32 RequestID = objRW.SendRequest(Convert.ToDecimal(clsSetting._CustomerID),LocalIPAddress(), txtWinLoginName.Text, txtWinPassword.Text, txtMessage.Text);
                KryptonMessageBox.Show("Your Request ID is " + RequestID.ToString() + System.Environment.NewLine + "Some one will connect you very soon.", "Remote Services", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.ToString(), "Remote Service", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #region GetLocalIPAddress
        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        #endregion

        private void buttonSpecHeaderGroup2_Click(object sender, EventArgs e)
        {

            frmChangePassword objFrm = new frmChangePassword();
            //objFrm.WindowState = FormWindowState.Maximized;
            objFrm.StartPosition = FormStartPosition.CenterScreen;
            objFrm.OpType = "cust";
            objFrm.ShowDialog();
            objFrm = null;
        }

    }
}