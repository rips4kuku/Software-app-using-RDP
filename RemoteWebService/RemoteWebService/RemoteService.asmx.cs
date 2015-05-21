using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using MyDBManager;


namespace RemoteWebService
{
    /// <summary>
    /// Summary description for clsRemoteWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class clsRemoteWebService : System.Web.Services.WebService
    {

        #region AuthenticateUser
        [WebMethod]
        public DataSet AuthenticateUser(string UserName, string Password)
        {
            clsCustomerRequest objCustomer = new clsCustomerRequest(true);
            return objCustomer.AuthenticateUser(UserName, Password);
        }
        #endregion

        #region SendCustomerRequest
        [WebMethod]
        public Int32 SendRequest(decimal customerID, string IPAddress, string winLoginName, string winPassword, string remarks)
        {
            //string IPAddress = "";
            if (IPAddress.Trim() == "")
                IPAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            clsCustomerRequest objCustomer = new clsCustomerRequest(true);
            return objCustomer.Insert(0, customerID, DateTime.Now.Date, IPAddress, winLoginName, winPassword, null, DateTime.Now.Date, remarks, true);
        }
        #endregion

        #region AuthenticateEmployee
        /// <summary>
        /// Authenticate Employee's User ID and Password
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet AuthenticateEmployee(string UserName, string Password)
        {
            clsEmployeeMaster objCustomer = new clsEmployeeMaster(true);
            return objCustomer.AuthenticateEmployee(UserName, Password);
        }
        #endregion

        #region getPendingJobList
        /// <summary>
        /// get All Pending Job List in Dataset
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataSet getPendingJobList()
        {
            clsCustomerRequest objCustomer = new clsCustomerRequest(true);
            return objCustomer.getPendingJobList();
        }
        #endregion

        #region StartJob
        /// <summary>
        /// set EmployeeID and Start Job Time
        /// </summary>
        [WebMethod]
        public void startJob(Int64 _RequestID, Int16 _EmployeeID)
        {
            clsCustomerRequest objReq = new clsCustomerRequest(true);
            objReq.ExecuteQuery("update tblCustomerRequest set [ServiceStartTime]=getDate(),[EmployeeID]=" + _EmployeeID + " where RequestID=" + _RequestID);
            objReq.ReleaseCommand();
            objReq = null;
        }
        #endregion

        #region DoneJob
        /// <summary>
        /// set EndServiceTime and update Pending Flag
        /// </summary>
        [WebMethod]
        public void DoneJob(Int64 _RequestID)
        {
            clsCustomerRequest objReq = new clsCustomerRequest(true);
            objReq.ExecuteQuery("update tblCustomerRequest set [ServiceEndTime]=getDate(), IsPending = 0 where RequestID=" + _RequestID);
            objReq.ReleaseCommand();
            objReq = null;
        }
        #endregion

        #region Customers Method

        #region CustomerInsert
        [WebMethod]
        public Int32 CustomerInsert(string customerName, string companyName, string address, string city, string state, string contactNo, string mobile, string userID, string password, DateTime expiryDate)
        {
            clsCustomerMaster objCustomer = new clsCustomerMaster(true);
            return objCustomer.Insert(customerName, companyName, address, city, state, contactNo, mobile, userID, password, expiryDate);

        }
        #endregion

        #region CustomerUpdate
        [WebMethod]
        public void CustomerUpdate(decimal customerID, string customerName, string companyName, string address, string city, string state, string contactNo, string mobile, string userID, string password, DateTime expiryDate)
        {
            clsCustomerMaster objCustomer = new clsCustomerMaster(true);
            objCustomer.Update(customerID, customerName, companyName, address, city, state, contactNo, mobile, userID, password, expiryDate);

        }
        #endregion

        #region CustomerDelete
        [WebMethod]
        public void CustomerDelete(decimal customerID)
        {
            clsCustomerMaster objCustomer = new clsCustomerMaster(true);
            objCustomer.Delete(customerID);

        }
        #endregion

        #region CustomerSelectCustom
        [WebMethod]
        public DataSet CustomerSelectCustom(string Where)
        {
            clsCustomerMaster objCustomer = new clsCustomerMaster(true);
            return objCustomer.GetAnyDataset("select * from tblCustomerMaster Where 1=1 " + Where, CType.SQL);
        }
        #endregion

        #endregion

        #region EmployeesMethod

        #region EmployeeInsert

        [WebMethod]
        public Int32 EmployeeInsert(string employeeName, string address, string city, string state, string mobile, string userID, string password, bool isActive, bool isFree)
        {
            clsEmployeeMaster objEmp = new clsEmployeeMaster(true);
            return objEmp.Insert(employeeName, address, city, state, mobile, userID, password, isActive, isFree);
        }
        #endregion

        #region EmployeeUpdate

        [WebMethod]
        public Int32 EmployeeUpdate(int employeeID, string employeeName, string address, string city, string state, string mobile, string userID, string password, bool isActive, bool isFree)
        {
            clsEmployeeMaster objEmp = new clsEmployeeMaster(true);
            return objEmp.Update(employeeID, employeeName, address, city, state, mobile, userID, password, isActive, isFree);
        }
        #endregion

        #region EmployeeDelete

        [WebMethod]
        public void EmployeeDelete(int employeeID)
        {
            clsEmployeeMaster objEmp = new clsEmployeeMaster(true);
            objEmp.Delete(employeeID);
        }
        #endregion

        #region EmployeeSelectCustom
        [WebMethod]
        public DataSet EmployeeSelectCustom(string where)
        {
            clsEmployeeMaster objEmp = new clsEmployeeMaster(true);
            return objEmp.GetAnyDataset("select * from tblEmployeeMaster where 1 =1 " + where, CType.SQL);
        }
        #endregion
        #endregion


        #region ResetPassword
        [WebMethod]
        public string ResetPassword(string type, string oldPass, string newPass, decimal ID)
        {
            string s = "";
            if (type.ToLower() == "emp")
            {
                clsEmployeeMaster objEmp = new clsEmployeeMaster(true);
                if (Convert.ToInt16(objEmp.ExecuteMyScalar("select count (*) from tblEmployeeMaster where EmployeeID =" + ID + " and Password='" + oldPass + "'")) > 0)
                {
                    objEmp.ExecuteQuery("Update tblEmployeeMaster set Password = '" + newPass + "' where EmployeeID = " + ID);
                    s = "Password Updated Successfully";
                }
                else
                    s = "InCorrect Old Password";
                objEmp.ReleaseCommand();
                objEmp = null;
            }
            else
            {
                clsCustomerMaster objCust = new clsCustomerMaster(true);
                if (Convert.ToInt16(objCust.ExecuteMyScalar("select count (*) from tblCustomerMaster where CustomerID =" + ID + " and Password='" + oldPass + "'")) > 0)
                {
                    objCust.ExecuteQuery("Update tblCustomerMaster set Password = '" + newPass + "' where CustomerID = " + ID);
                    s = "Password Updated Successfully";
                }
                else
                    s = "InCorrect Old Password";

                objCust.ReleaseCommand();
                objCust = null;
            }
            return s;
        }
        #endregion
        
    }
}
