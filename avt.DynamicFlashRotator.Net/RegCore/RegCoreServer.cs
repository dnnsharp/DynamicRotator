using System;
using System.Collections.Generic;
using System.Text;

namespace avt.DynamicFlashRotator.Net.RegCore
{
    public class RegCoreServer
    {
        string _Address;
        public string Address { get { return _Address; } set { _Address = value; } }

        string _ApiScript;
        public string ApiScript { get { return _ApiScript; } }

        string _UnlockTrialScript;
        public string UnlockTrialScript { get { return _UnlockTrialScript; } }


        public RegCoreServer(string serverAddress)
        {
            Address = serverAddress;
            _ApiScript = Address.Trim('/') + "/Api.aspx";
            _UnlockTrialScript = Address.Trim('/') + "/UnlockTrial.aspx";
        }


        public string GetBuyUrl(RegCoreApp app)
        {
            return ApiScript + "?cmd=buy&product=" + app.ProductCode + "&version=" + app.Version;
        }

        public string GetDocUrl(RegCoreApp app)
        {
            return ApiScript + "?cmd=doc&product=" + app.ProductCode + "&version=" + app.Version;
        }

        public string GetProductUrl(RegCoreApp app)
        {
            return ApiScript + "?cmd=info&product=" + app.ProductCode + "&version=" + app.Version;
        }

        public override string ToString()
        {
            return Address;
        }
    }
}
