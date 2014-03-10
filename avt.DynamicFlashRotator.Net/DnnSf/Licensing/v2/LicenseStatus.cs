using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2
{
    public class LicenseStatus
    {
        public enum eType
        {
            Info,
            Warning,
            Error
        }

        public enum eCode
        {
            Unknown,
            Ok,
            Invalid,
            InvalidLicenseCount,
            WarningNotCovered,
            WarningExpire,
            Expired
        }

        public string Message { get; set; }

        public eType Type { get; set; }
        public eCode Code { get; set; }

        public Dictionary<string, object> Data { get; set; }

        public LicenseStatus()
        {
            Data = new Dictionary<string, object>();
        }

        public LicenseStatus AddData(string name, object val)
        {
            Data[name] = val;
            return this;
        }

    }
}
