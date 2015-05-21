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
    public partial class frmJobList : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public frmJobList()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmJobList_Load(object sender, EventArgs e)
        {
            frmLogin objLogin = new frmLogin();
            objLogin.ShowDialog();
            if (objLogin.DialogResult == DialogResult.OK)
            {
                lblName.Text = "Welcome " + clsSetting._EmployeeName;
                fillGrid();
                timer1.Enabled = true;
            }
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {
                    Int64 _RequestID = Convert.ToInt64(grid.Rows[e.RowIndex].Cells["RequestID"].Value);
                    Int32 _CustomerID = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["CustomerID"].Value);
                    clsRemoteWebService objRM = new clsRemoteWebService();
                    objRM.startJob(_RequestID, Convert.ToInt16(clsSetting._EmployeeID));

                    string _CustomerIP = grid.Rows[e.RowIndex].Cells["RequestIP"].Value.ToString();
                    string _WinLoginName = grid.Rows[e.RowIndex].Cells["WinLoginName"].Value.ToString();
                    string _WinPassword = grid.Rows[e.RowIndex].Cells["WinPassword"].Value.ToString();
                    frmRemoteDesktop objRemote = new frmRemoteDesktop(_CustomerIP, _WinLoginName, _WinPassword);
                    objRemote.WindowState = FormWindowState.Maximized;
                    objRemote.ShowDialog();
                    if (objRemote.DialogResult == DialogResult.OK)
                    {
                        //Update Status here for Done Job
                        objRM.DoneJob(_RequestID);
                    }
                    objRM = null;
                    objRemote = null;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.ToString(), "Remote Services >> grid_CellDoubleClick()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void fillGrid()
        {
            try
            {
                clsRemoteWebService objRW = new clsRemoteWebService();
                grid.DataSource = objRW.getPendingJobList().Tables[0].DefaultView;
                grid.Columns[0].Visible = false;

                for (int i = 0; i < grid.Rows.Count ; i++)
                {
                    if (grid.Rows[i].Cells["ServiceStartTime"].Value.ToString() != "")
                        grid.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red ;
                    else
                    grid.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.ToString(), "Remote Services >> fillGrid()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void btnManageCustomer_Click(object sender, EventArgs e)
        {
            
            frmCustomerMaster objFrm = new frmCustomerMaster();
            objFrm.WindowState = FormWindowState.Maximized;
            objFrm.ShowDialog();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword objFrm = new frmChangePassword();
            //objFrm.WindowState = FormWindowState.Maximized;
            objFrm.StartPosition = FormStartPosition.CenterScreen;
            objFrm.OpType = "emp";
            objFrm.ShowDialog();
            objFrm = null;
        }

        private void btnManageEmployee_Click(object sender, EventArgs e)
        {   

            frmEmployeeMaster objFrm = new frmEmployeeMaster();
            objFrm.WindowState = FormWindowState.Maximized;
            objFrm.ShowDialog();
        }

      

    }
}