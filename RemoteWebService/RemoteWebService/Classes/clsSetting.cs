using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MyDBManager;
using System.Data;

namespace RemoteWebService
{
    class clsSetting
    {
        internal static string GetConnectionString()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["CN"].ToString();
        }
    }
}
