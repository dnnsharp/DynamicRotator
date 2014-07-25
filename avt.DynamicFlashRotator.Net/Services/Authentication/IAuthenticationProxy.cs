using System;
using System.Collections.Generic;
using System.Text;

namespace DnnSharp.DynamicRotator.Core.Services.Authentication
{
    public interface IAuthenticationProxy
    {
        bool HasAccess(string controlId);
    }
}
