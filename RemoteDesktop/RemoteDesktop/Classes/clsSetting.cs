using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MyDBManager;
using ComponentFactory.Krypton.Toolkit;
using System.Data;

namespace RemoteDesktop
{
    class clsSetting
    {
        internal static Int32 _CustomerID = 0;
        internal static Int32 _EmployeeID = 0;
        internal static string _EmployeeName = "";
        internal static string GetConnectionString()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["CN"].ToString();
        }
    }
}
