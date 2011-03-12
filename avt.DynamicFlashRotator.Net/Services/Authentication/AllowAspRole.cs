using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace avt.DynamicFlashRotator.Net.Services.Authentication
{
    public class AllowAspRole : IAdminAuthentication
    {
        string _roleName;

        public AllowAspRole()
        {
        }

        public AllowAspRole(string roleName)
        {
            Init(roleName);
        }

        #region IAdminAuthentication Members

        public void Init(string authToken)
        {
            _roleName = authToken;
        }
    
        public bool HasAccess()
        {
            if (string.IsNullOrEmpty(_roleName)) {
                return true; // bypass this method
            }

            try {
                return HttpContext.Current.User.IsInRole(_roleName);
            } catch { return false; }
        }

        #endregion
    }
}
