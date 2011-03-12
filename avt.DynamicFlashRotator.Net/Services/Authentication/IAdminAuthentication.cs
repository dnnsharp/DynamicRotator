using System;
using System.Collections.Generic;
using System.Text;

namespace avt.DynamicFlashRotator.Net.Services.Authentication
{
    public interface IAdminAuthentication
    {
        void Init(string authToken);
        bool HasAccess();
    }
}
