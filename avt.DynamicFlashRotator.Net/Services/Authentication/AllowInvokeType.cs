using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace avt.DynamicFlashRotator.Net.Services.Authentication
{
    public class AllowInvokeType : IAdminAuthentication
    {
        string _type;
        string _controlId;

        public AllowInvokeType()
        {
        }

        public AllowInvokeType(string invokeType, string controlId)
        {
            Init(invokeType, controlId);
        }

        #region IAdminAuthentication Members

        public void Init(string authToken, string controlId)
        {
            _type = authToken;
            _controlId = controlId;
        }

        public bool HasAccess()
        {
            if (string.IsNullOrEmpty(_type)) {
                return true; // bypass this method
            }

            return CreateInstance(_type).HasAccess(_controlId);
        }

        #endregion

        public IAuthenticationProxy CreateInstance(string strDataType)
        {
            Type dataType = Type.GetType(strDataType);
            if (dataType == null) {
                dataType = Type.GetType(strDataType.Substring(0, strDataType.IndexOf(",") + 1) + typeof(IAuthenticationProxy).Assembly.ToString());
            }

            return Activator.CreateInstance(dataType) as IAuthenticationProxy;
        }
    }
}
