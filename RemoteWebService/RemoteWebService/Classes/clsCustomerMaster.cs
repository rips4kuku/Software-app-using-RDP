using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MyDBManager;

namespace RemoteWebService
{
    public sealed partial class clsCustomerMaster : DBManager
    {
        static Int32 _id = 0;
        #region Constructors
        public clsCustomerMaster()
        {
        }

        public clsCustomerMaster(Boolean openConnection)
        {
            InitConnectionObject(clsSetting.GetConnectionString());
        }

        public clsCustomerMaster(string conn)
        {
            InitConnectionObject(conn);
        }

        #endregion

        #region Methods

        #region Insert
        /// <summary>
        /// Saves a record to the tblCustomerMaster table.
        /// </summary>
        public Int32 Insert(string customerName, string companyName, string address, string city, string state, string contactNo, string mobile, string userID, string password, DateTime expiryDate)
        {
            Prepare("SP_tblCustomerMasterInsert", CType.StoredProcedure);
            AddCmdParameter("@CustomerName", DType.VarChar, customerName, ParaDirection.In, true);
            AddCmdParameter("@CompanyName", DType.VarChar, companyName, ParaDirection.In, true);
            AddCmdParameter("@Address", DType.VarChar, address, ParaDirection.In, true);
            AddCmdParameter("@City", DType.NChar, city, ParaDirection.In, true);
            AddCmdParameter("@State", DType.NChar, state, ParaDirection.In, true);
            AddCmdParameter("@ContactNo", DType.VarChar, contactNo, ParaDirection.In, true);
            AddCmdParameter("@Mobile", DType.VarChar, mobile, ParaDirection.In, true);
            AddCmdParameter("@UserID", DType.VarChar, userID, ParaDirection.In, true);
            AddCmdParameter("@Password", DType.VarChar, password, ParaDirection.In, true);
            AddCmdParameter("@ExpiryDate", DType.DateTime, expiryDate, ParaDirection.In, true);
            AddCmdParameter("@Inserted", DType.Int, null, ParaDirection.Out, true);
            _id = Convert.ToInt32(ExecuteMyScalar());
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates a record in the tblCustomerMaster table.
        /// </summary>
        public int Update(decimal customerID, string customerName, string companyName, string address, string city, string state, string contactNo, string mobile, string userID, string password, DateTime expiryDate)
        {
            Prepare("SP_tblCustomerMasterUpdate", CType.StoredProcedure);
            AddCmdParameter("@CustomerID", DType.Decimal, customerID, ParaDirection.In, true);
            AddCmdParameter("@CustomerName", DType.VarChar, customerName, ParaDirection.In, true);
            AddCmdParameter("@CompanyName", DType.VarChar, companyName, ParaDirection.In, true);
            AddCmdParameter("@Address", DType.VarChar, address, ParaDirection.In, true);
            AddCmdParameter("@City", DType.NChar, city, ParaDirection.In, true);
            AddCmdParameter("@State", DType.NChar, state, ParaDirection.In, true);
            AddCmdParameter("@ContactNo", DType.VarChar, contactNo, ParaDirection.In, true);
            AddCmdParameter("@Mobile", DType.VarChar, mobile, ParaDirection.In, true);
            AddCmdParameter("@UserID", DType.VarChar, userID, ParaDirection.In, true);
            AddCmdParameter("@Password", DType.VarChar, password, ParaDirection.In, true);
            AddCmdParameter("@ExpiryDate", DType.DateTime, expiryDate, ParaDirection.In, true);
            _id = Convert.ToInt32(ExecuteMyScalar());
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Deletes a record from the tblCustomerMaster table by its primary key.
        /// </summary>
        public int Delete(decimal customerID)
        {
            ExecuteQuery("DELETE FROM [tblCustomerMaster] WHERE [CustomerID] = " + customerID);
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region GetAnyDataset
        /// <summary>
        /// Selects any records from the tblCustomerMaster table and bind with dataset
        /// </summary>
        public DataSet GetAnyDataset(string cmdName, CType cmdType)
        {
            DataSet ds = new DataSet();
            Prepare(cmdName, cmdType);
            ds = ExecuteDataset();
            ReleaseCommand();
            return ds;
        }
        #endregion

        #endregion

    }
}
