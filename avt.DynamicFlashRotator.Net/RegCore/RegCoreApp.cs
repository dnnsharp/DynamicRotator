using System;
using System.Collections.Generic;
using System.Text;

namespace avt.DynamicFlashRotator.Net.RegCore
{
    public class RegCoreApp
    {
        string _ProductCode;
        public string ProductCode { get { return _ProductCode; } set { _ProductCode = value; } }

        string _ProductName;
        public string ProductName { get { return _ProductName; } set { _ProductName = value; } }

        string _ProductKey;
        public string ProductKey { get { return _ProductKey; } set { _ProductKey = value; } }

        string _Version;
        public string Version { get { return _Version; } set { _Version = value; } }

        public RegCoreApp()
        {

        }

        public RegCoreApp(string productName, string productCode, string productKey, string version)
        {
            ProductName = productName;
            ProductCode = productCode;
            ProductKey = productKey;
            Version = version;
        }

        public void FromController(Type ctrl)
        {
            ProductCode = (string)ctrl.GetProperty("ProductCode", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);
            ProductName = (string)ctrl.GetProperty("ProductName", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);
            ProductKey = (string)ctrl.GetProperty("ProductKey", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);
            Version = (string)ctrl.GetProperty("Version", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);
        }
    }
}
