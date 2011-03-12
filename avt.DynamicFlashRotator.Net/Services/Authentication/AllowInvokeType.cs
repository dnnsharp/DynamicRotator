using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace avt.DynamicFlashRotator.Net.Services.Authentication
{
    public class AllowInvokeType : IAdminAuthentication
    {
        string _type;

        public AllowInvokeType()
        {
        }

        public AllowInvokeType(string invokeType)
        {
            Init(invokeType);
        }

        #region IAdminAuthentication Members

        public void Init(string authToken)
        {
            _type = authToken;
        }

        public bool HasAccess()
        {
            if (string.IsNullOrEmpty(_type)) {
                return true; // bypass this method
            }

            return CreateInstance(_type).HasAccess();
        }

        #endregion

        public IAdminAuthentication CreateInstance(string strDataType)
        {
            Type dataType = Type.GetType(strDataType);
            if (dataType == null) {
                dataType = Type.GetType(strDataType.Substring(0, strDataType.IndexOf(",") + 1) + typeof(IAdminAuthentication).Assembly.ToString());
            }

            return Activator.CreateInstance(dataType) as IAdminAuthentication;
        }
    }
}
