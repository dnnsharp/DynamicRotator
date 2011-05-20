using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using avt.DynamicFlashRotator.Net.RegCore.Cryptography;
using System.Security.Cryptography;

namespace avt.DynamicFlashRotator.Net.RegCore
{
    internal class LicenseActivation
    {
        public string Host;
        public string RegistrationCode;
        public string ActivationCode;
        public string ProductKey;
        public string BaseProductCode;
        public string BaseProductVersion;
        public string TmpKey = "";

        RegCode _RegCode = null;
        public RegCode RegCode
        {
            get
            {
                if (_RegCode == null) {
                    try {
                        _RegCode = new RegCode(RegistrationCode);
                    } catch {
                        _RegCode = null;
                    }
                }
                return _RegCode;
            }
        }

        public LicenseActivation()
        {
        }

        public LicenseActivation Clone()
        {
            LicenseActivation act = new LicenseActivation();
            act.ActivationCode = ActivationCode;
            act.RegistrationCode = RegistrationCode;
            act.Host = Host;
            act.ProductKey = ProductKey;
            act.BaseProductVersion = BaseProductVersion;
            act.BaseProductCode = BaseProductCode;
            return act;
        }

        public bool IsValid(string productCode, string versionCode, string minorVersion)
        {
            //System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            //timer.Start();

            // always pass BaseProductVerions in activation code IsValid
            if (RegCode == null || RegCode.IsExpired())
                return false;

            EZRSA ezrsa_public = new EZRSA(1024);
            ezrsa_public.FromXmlString(ProductKey);

            byte[] signatureBytes = new byte[128];
            for (int i = 0; i < 128; i++) {
                signatureBytes[i] = Convert.ToByte(ActivationCode.Substring(i * 2, 2), 16);
            }

            return ezrsa_public.VerifyData(Encoding.Unicode.GetBytes(Host + RegistrationCode + RegCode.R + productCode + minorVersion), new SHA1CryptoServiceProvider(), signatureBytes);
        }
    }
}