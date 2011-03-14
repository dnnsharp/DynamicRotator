using System;
using System.Collections.Generic;
using System.Text;

namespace avt.DynamicFlashRotator.Net.Services.Authentication
{
    public interface IAuthenticationProxy
    {
        bool HasAccess(string controlId);
    }
}
