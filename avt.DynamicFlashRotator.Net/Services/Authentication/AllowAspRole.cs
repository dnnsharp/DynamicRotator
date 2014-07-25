using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace DnnSharp.DynamicRotator.Core.Services.Authentication
{
    public class AllowAspRole : IAdminAuthentication
    {
        string _roleName;

        public AllowAspRole()
        {
        }

        public AllowAspRole(string roleName, string controlId)
        {
            Init(roleName, controlId);
        }

        #region IAdminAuthentication Members

        public void Init(string authToken, string controlId)
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
