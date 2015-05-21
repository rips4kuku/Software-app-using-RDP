using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MSTSCLib;
using AxMSTSCLib;

namespace RemoteDesktop
{
    public partial class frmRemoteDesktop : ComponentFactory.Krypton.Toolkit.KryptonForm
    {

        public frmRemoteDesktop(string _ServerIP, string _WinLogin, string _WinPass)
        {
            InitializeComponent();
            rdp.Server = _ServerIP;
            rdp.UserName = _WinLogin;
            IMsTscNonScriptable secured = (IMsTscNonScriptable)rdp.GetOcx();
            secured.ClearTextPassword = _WinPass;
        }
        private void frmRemoteDesktop_Load(object sender, EventArgs e)
        {
            try
            {
                
               rdp.Connect();   
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.ToString());
            }
        }

        private void frmRemoteDesktop_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (rdp.Connected.ToString()=="1")
                rdp.Disconnect();
        }

        private void btnJobDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}