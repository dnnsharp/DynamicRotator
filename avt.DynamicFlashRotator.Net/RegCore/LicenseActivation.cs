using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using avt.DynamicFlashRotator.Net.RegCore.Cryptography;
using System.Security.Cryptography;

namespace avt.DynamicFlashRotator.Net.RegCore
{
    internal class LicenseActivation : ILicenseActivation
    {
        string _Host;
        public string Host { get { return _Host; } set { _Host = value; } }

        string _RegistrationCode;
        public string RegistrationCode { get { return _RegistrationCode; } set { _RegistrationCode = value; } }

        string _ActivationCode;
        public string ActivationCode { get { return _ActivationCode; } set { _ActivationCode = value; } }

        string _ProductKey;
        public string ProductKey { get { return _ProductKey; } set { _ProductKey = value; } }

        string _BaseProductCode;
        public string BaseProductCode { get { return _BaseProductCode; } set { _BaseProductCode = value; } }

        string _BaseProductVersion;
        public string BaseProductVersion { get { return _BaseProductVersion; } set { _BaseProductVersion = value; } }

        string _TmpKey = "";
        public string TmpKey { get { return _TmpKey; } set { _TmpKey = value; } }


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

        public ILicenseActivation Clone()
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

        public bool IsValid(string productCode, string versionCode)
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

            return ezrsa_public.VerifyData(Encoding.Unicode.GetBytes(Host + RegistrationCode + RegCode.R + productCode + versionCode), new SHA1CryptoServiceProvider(), signatureBytes);
        }
    }
}