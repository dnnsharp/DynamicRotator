using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace avt.DynamicFlashRotator.Net.Services.Authentication
{
    public class AllowIps : IAdminAuthentication
    {
        string _ips;

        public AllowIps()
        {
        }

        public AllowIps(string ips, string controlId)
        {
            Init(ips, controlId);
        }

        #region IAdminAuthentication Members

        public void Init(string authToken, string controlId)
        {
            _ips = authToken;
        }

        public bool HasAccess()
        {
            if (string.IsNullOrEmpty(_ips)) {
                return true; // bypass this method
            }

            foreach (string ipAddress in _ips.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) {
                if (UserIpAddress() == ipAddress.Trim()) {
                    return true;
                }
            }

            return false;
        }

        #endregion

        string UserIpAddress()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
                return "";

            string strIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null)
                strIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return strIpAddress;
        }
    }
}
