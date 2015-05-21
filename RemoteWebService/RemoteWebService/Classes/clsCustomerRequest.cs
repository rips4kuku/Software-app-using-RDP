using System;
using System.Collections.Generic;
using System.Text;
using MyDBManager;
using System.Data;
namespace RemoteWebService
{
    class clsCustomerRequest:DBManager
    {
        #region Fields
        //private decimal customerID;
        //private string customerName;
        //private string companyName;
        //private string address;
        //private string city;
        //private string state;
        //private string contactNo;
        //private string mobile;
        //private string userID;
        //private string password;
        //private DateTime expiryDate;
		static Int32 _id = 0;
		#endregion
		
		#region Constructors
		public clsCustomerRequest()
		{
		}
		
		public clsCustomerRequest(Boolean openConnection)
		{
			InitConnectionObject(clsSetting.GetConnectionString());
		}

		
		#endregion
		
        //#region Properties
        ///// <summary>
        ///// Gets or sets the CustomerID value.
        ///// </summary>
        //public decimal CustomerID {
        //    get { return customerID; }
        //    set { customerID = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the CustomerName value.
        ///// </summary>
        //public string CustomerName {
        //    get { return customerName; }
        //    set { customerName = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the CompanyName value.
        ///// </summary>
        //public string CompanyName {
        //    get { return companyName; }
        //    set { companyName = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the Address value.
        ///// </summary>
        //public string Address {
        //    get { return address; }
        //    set { address = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the City value.
        ///// </summary>
        //public string City {
        //    get { return city; }
        //    set { city = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the State value.
        ///// </summary>
        //public string State {
        //    get { return state; }
        //    set { state = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the ContactNo value.
        ///// </summary>
        //public string ContactNo {
        //    get { return contactNo; }
        //    set { contactNo = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the Mobile value.
        ///// </summary>
        //public string Mobile {
        //    get { return mobile; }
        //    set { mobile = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the UserID value.
        ///// </summary>
        //public string UserID {
        //    get { return userID; }
        //    set { userID = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the Password value.
        ///// </summary>
        //public string Password {
        //    get { return password; }
        //    set { password = value; }
        //}
		
        ///// <summary>
        ///// Gets or sets the ExpiryDate value.
        ///// </summary>
        //public DateTime ExpiryDate {
        //    get { return expiryDate; }
        //    set { expiryDate = value; }
        //}

        //#endregion

        #region AuthenticateUser
        internal DataSet AuthenticateUser(string UserID, string Pwd)
        {
            return GetAnyDataset("select * from tblCustomerMaster where UserID = '" + UserID + "' and Password='" + Pwd + "'", CType.SQL);
        }
        #endregion

        #region Insert
        /// <summary>
        /// Saves a record to the tblCustomerRequest table.
        /// </summary>
        internal Int32 Insert(int employeeID, decimal customerID, DateTime requestTime, string requestIP, string winLoginName, string winPassword, DateTime? serviceStartTime, DateTime serviceEndTime, string remarks, bool isPending)
        {
            Prepare("SP_tblCustomerRequestInsert", CType.StoredProcedure);
            AddCmdParameter("@EmployeeID", DType.Int, employeeID, ParaDirection.In, true);
            AddCmdParameter("@CustomerID", DType.Decimal, customerID, ParaDirection.In, true);
            AddCmdParameter("@RequestTime", DType.DateTime, requestTime, ParaDirection.In, true);
            AddCmdParameter("@RequestIP", DType.VarChar, requestIP, ParaDirection.In, true);
            AddCmdParameter("@WinLoginName", DType.VarChar, winLoginName, ParaDirection.In, true);
            AddCmdParameter("@WinPassword", DType.VarChar, winPassword, ParaDirection.In, true);
            AddCmdParameter("@ServiceStartTime", DType.DateTime, serviceStartTime, ParaDirection.In, true);
            AddCmdParameter("@ServiceEndTime", DType.DateTime, serviceEndTime, ParaDirection.In, true);
            AddCmdParameter("@Remarks", DType.VarChar, remarks, ParaDirection.In, true);
            AddCmdParameter("@IsPending", DType.Bit, isPending, ParaDirection.In, true);
            AddCmdParameter("@Inserted", DType.Int, null, ParaDirection.Out, true);
            _id = Convert.ToInt32(ExecuteMyScalar());
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region getPendingJobList
        internal DataSet getPendingJobList()
        {
            return GetAnyDataset("select * from view_PendingJobList", CType.SQL);
        }
        #endregion

        #region GetAnyDataset
        public DataSet GetAnyDataset(string cmdName, CType cmdType)
        {
            DataSet ds = new DataSet();
            Prepare(cmdName, cmdType);
            ds = ExecuteDataset();
            ReleaseCommand();
            return ds;
        }
        #endregion
    }
}
