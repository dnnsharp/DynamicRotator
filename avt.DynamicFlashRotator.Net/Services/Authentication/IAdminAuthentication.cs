using System;
using System.Collections.Generic;
using System.Text;

namespace DnnSharp.DynamicRotator.Core.Services.Authentication
{
    public interface IAdminAuthentication
    {
        void Init(string authToken, string controlId);
        bool HasAccess();
    }
}
