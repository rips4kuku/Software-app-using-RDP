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
    public partial class frmCustomerMaster : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public frmCustomerMaster()
        {
            InitializeComponent();
        }

        #region Declaration
        Int32 _Id;

        public Int32 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        #endregion

        #region Method

        #region fillgrid
        public void fillgrid(string wherecond)
        {
            clsRemoteWebService objRM = new clsRemoteWebService();
            grvdata.AutoGenerateColumns = false;
            grvdata.DataSource = objRM.CustomerSelectCustom(wherecond).Tables[0].DefaultView;
            objRM = null;
        }
        #endregion

        #region FillComboSearch
        public void FillComboSearch()
        {
            
            CmbSearchType.Items.Add("CustomerName");
            CmbSearchType.Items.Add("CompanyName");
            CmbSearchType.Items.Add("Mobile");
            CmbSearchType.SelectedIndex = 0;
        }
        #endregion

        private void Clear()
        {
            txtName.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtMobile.Text = "";
            txtPassword.Text = "";
            txtState.Text = "";
            txtUserID.Text = "";
            txtCity.Text = "";
            Id = 0;
            txtSearchValue.Text = "";
        }

        #endregion

        #region Event

        #region Frm_Load
        private void Frm_Load(object sender, EventArgs e)
        {
            try
            {
                kryptonSplitContainer1.Panel1Collapsed = false;
                kryptonSplitContainer1.Panel2Collapsed = true;
                // fillgrid("");

                FillComboSearch();
                CmdAdd.Focus();
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message, "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CmdAdd_Click
        private void CmdAdd_Click(object sender, EventArgs e)
        {
            kryptonSplitContainer1.Panel1Collapsed = true;
            kryptonSplitContainer1.Panel2Collapsed = false;
            txtName.Text = string.Empty;
            CmdDelete.Enabled = false;
            Id = 0;
            txtName.Focus();
        }
        #endregion

        #region CmdExit_Click
        private void CmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 1

        #region cmdSave_Click
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtName.Text.Trim() == string.Empty)
                {
                    KryptonMessageBox.Show("Please Enter the Customer Name ", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }
                clsRemoteWebService objRM = new clsRemoteWebService();
                if (Id == 0)
                {
                    objRM.CustomerInsert(txtName.Text, txtCompanyName.Text, txtAddress.Text, txtCity.Text, txtState.Text, txtContactNo.Text, txtMobile.Text, txtUserID.Text, txtPassword.Text, dtpExpiryDate.Value);
                    KryptonMessageBox.Show("Record Save Successfully", "Customer Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    objRM.CustomerUpdate(this.Id, txtName.Text, txtCompanyName.Text, txtAddress.Text, txtCity.Text, txtState.Text, txtContactNo.Text, txtMobile.Text, txtUserID.Text, txtPassword.Text, dtpExpiryDate.Value);
                    KryptonMessageBox.Show("Record Update Successfully", "Customer Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                objRM = null;
                Clear();
                Id = 0;
                txtName.Focus();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, "Customer Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CmdCancel_Click
        private void CmdCancel_Click(object sender, EventArgs e)
        {
            kryptonSplitContainer1.Panel2Collapsed = true;
            kryptonSplitContainer1.Panel1Collapsed = false;
            Clear();
            CmdExit.Focus();
        }
        #endregion

        #region CmdDelete_Click
        private void CmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (KryptonMessageBox.Show("Are you sure you want to delete this record", "Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    clsRemoteWebService objRM = new clsRemoteWebService();
                    objRM.CustomerDelete(Id);
                    KryptonMessageBox.Show("Record Deleted Successfully", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    Id = 0;
                    fillgrid("");
                    kryptonSplitContainer1.Panel1Collapsed = false;
                    kryptonSplitContainer1.Panel2Collapsed = true;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        #endregion

        #region grvdata_CellDoubleClick
        private void grvdata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                Id = Convert.ToInt32(grvdata.Rows[e.RowIndex].Cells["CustomerId"].Value);

                clsRemoteWebService obj = new clsRemoteWebService();
                DataSet Ds = new DataSet();
                Ds = obj.CustomerSelectCustom(" and CustomerID =" + Id);
                if (Ds != null)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        txtName.Text = Ds.Tables[0].Rows[0]["CustomerName"].ToString();
                        txtCompanyName.Text = Ds.Tables[0].Rows[0]["CompanyName"].ToString();
                        txtAddress.Text = Ds.Tables[0].Rows[0]["Address"].ToString(); ;
                        txtCity.Text = Ds.Tables[0].Rows[0]["City"].ToString();
                        txtState.Text = Ds.Tables[0].Rows[0]["State"].ToString();
                        txtContactNo.Text = Ds.Tables[0].Rows[0]["ContactNo"].ToString();
                        txtMobile.Text = Ds.Tables[0].Rows[0]["Mobile"].ToString();
                        txtUserID.Text = Ds.Tables[0].Rows[0]["UserID"].ToString();
                        txtPassword.Text = Ds.Tables[0].Rows[0]["Password"].ToString();
                        dtpExpiryDate.Value = Convert.ToDateTime(Ds.Tables[0].Rows[0]["ExpiryDate"]);
                        kryptonSplitContainer1.Panel1Collapsed = true;
                        kryptonSplitContainer1.Panel2Collapsed = false;
                        txtName.Focus();
                        CmdDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message, "Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region txtKeyPress
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
                SendKeys.Send("{tab}");
        }
        #endregion

        #region Frm_Activated
        private void Frm_Activated(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        #endregion

        #region txtSearchValue_TextChanged
        private void txtSearchValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fillgrid(" and " + CmbSearchType.Text + " like '" + txtSearchValue.Text + "%'");
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region cmbCity_KeyDown
        private void cmbCity_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
                SendKeys.Send("{tab}");
        }

        #endregion

        #region txtSearchValue_KeyPress
        private void txtSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // clsValidation.CharacterAndNumber(e);
        }
        #endregion

        private void grvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      #endregion
    }
}