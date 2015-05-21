using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MyDBManager;

namespace RemoteWebService
{
    public sealed partial class clsEmployeeMaster : DBManager
    {
        #region Fields

        static Int32 _id = 0;
        #endregion

        #region Constructors
        public clsEmployeeMaster()
        {
        }

        public clsEmployeeMaster(Boolean openConnection)
        {
            InitConnectionObject(clsSetting.GetConnectionString());
        }

        public clsEmployeeMaster(string conn)
        {
            InitConnectionObject(conn);
        }

        #endregion

        #region Methods

        #region Insert
        /// <summary>
        /// Saves a record to the tblEmployeeMaster table.
        /// </summary>
        public Int32 Insert(string employeeName, string address, string city, string state, string mobile, string userID, string password, bool isActive, bool isFree)
        {
            Prepare("SP_tblEmployeeMasterInsert", CType.StoredProcedure);
            AddCmdParameter("@EmployeeName", DType.VarChar, employeeName, ParaDirection.In, true);
            AddCmdParameter("@Address", DType.VarChar, address, ParaDirection.In, true);
            AddCmdParameter("@City", DType.VarChar, city, ParaDirection.In, true);
            AddCmdParameter("@State", DType.VarChar, state, ParaDirection.In, true);
            AddCmdParameter("@Mobile", DType.VarChar, mobile, ParaDirection.In, true);
            AddCmdParameter("@UserID", DType.VarChar, userID, ParaDirection.In, true);
            AddCmdParameter("@Password", DType.VarChar, password, ParaDirection.In, true);
            AddCmdParameter("@IsActive", DType.Bit, isActive, ParaDirection.In, true);
            AddCmdParameter("@IsFree", DType.Bit, isFree, ParaDirection.In, true);
            AddCmdParameter("@Inserted", DType.Int, null, ParaDirection.Out, true);
            _id = Convert.ToInt32(ExecuteMyScalar());
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates a record in the tblEmployeeMaster table.
        /// </summary>
        public int Update(int employeeID, string employeeName, string address, string city, string state, string mobile, string userID, string password, bool isActive, bool isFree)
        {
            Prepare("SP_tblEmployeeMasterUpdate", CType.StoredProcedure);
            AddCmdParameter("@EmployeeID", DType.Int, employeeID, ParaDirection.In, true);
            AddCmdParameter("@EmployeeName", DType.VarChar, employeeName, ParaDirection.In, true);
            AddCmdParameter("@Address", DType.VarChar, address, ParaDirection.In, true);
            AddCmdParameter("@City", DType.VarChar, city, ParaDirection.In, true);
            AddCmdParameter("@State", DType.VarChar, state, ParaDirection.In, true);
            AddCmdParameter("@Mobile", DType.VarChar, mobile, ParaDirection.In, true);
            AddCmdParameter("@UserID", DType.VarChar, userID, ParaDirection.In, true);
            AddCmdParameter("@Password", DType.VarChar, password, ParaDirection.In, true);
            AddCmdParameter("@IsActive", DType.Bit, isActive, ParaDirection.In, true);
            AddCmdParameter("@IsFree", DType.Bit, isFree, ParaDirection.In, true);
            _id = Convert.ToInt32(ExecuteMyScalar());
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Deletes a record from the tblEmployeeMaster table by its primary key.
        /// </summary>
        public int Delete(int employeeID)
        {
            ExecuteQuery("DELETE FROM [tblEmployeeMaster] WHERE [EmployeeID] = " + employeeID);
            ReleaseCommand();
            return _id;
        }
        #endregion

        #region GetAnyDataset
        /// <summary>
        /// Selects any records from the tblEmployeeMaster table and bind with dataset
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

        #region AuthnticateEmployee
        internal DataSet AuthenticateEmployee(string UserID, string Password)
        {
            return GetAnyDataset("select * from tblEmployeeMaster where UserID='" + UserID + "' and Password= '" + Password + "' and IsActive = 1", CType.SQL);
        }
        #endregion


        #endregion

    }
}
