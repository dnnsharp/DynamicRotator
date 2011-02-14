using System;
using System.Collections.Generic;
using System.Text;

namespace avt.AllinOneRotator.Net.Services
{
    public interface IConfiguration
    {
        string ConnStr { get; }
        string DbOwner { get; }
        string ObjQualifier { get; }
    }
}
