using System;
using System.Collections.Generic;
using System.Text;

namespace avt.DynamicFlashRotator.Net.RegCore
{
    internal interface IRegCoreClient
    {
        void Init(string regCoreServer, string productCode, string productKey, string version, string minorVersion);
    }
}
