using avt.DynamicFlashRotator.Dnn.DnnSf.Crypt;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2
{
    /// <summary>
    /// Represents license entry in the license file
    /// </summary>
    [XmlType("License")]
    public class LicenseInfo
    {
        public LicenseInfo()
        {

        }

        public LicenseInfo(string productCode, string versionCode, string licenseTypeCode)
        {
            ProductCode = productCode;
            VersionCode = versionCode;
            LicenseTypeCode = licenseTypeCode;
        }

        public string ProductCode { get; set; }

        public string VersionCode { get; set; }

        public string LicenseTypeCode { get; set; }

        /// <summary>
        /// This is only used by Portal license
        /// </summary>
        public int? LicenseCount { get; set; }

        /// <summary>
        /// apparently, XmlSerializer looks for this construct
        /// </summary>
        public bool LicenseCountSpecified { get { return LicenseCount != null; } }

        /// <summary>
        /// This is only used by the trial
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// apparently, XmlSerializer looks for this construct
        /// </summary>
        [ScriptIgnore]
        public bool ExpirationDateSpecified { get { return ExpirationDate != null; } }

        /// <summary>
        /// This is a signature hash for the original data
        /// </summary>
        public string Hash { get; set; }

        public void Sign(string productKey)
        {
            EZRSA ezrsa_private = new EZRSA(1024);
            ezrsa_private.FromXmlString(productKey);

            var sbSign = new StringBuilder();
            sbSign.Append(ProductCode);
            sbSign.Append(VersionCode);
            sbSign.Append(LicenseTypeCode);
            if (LicenseCount.HasValue)
                sbSign.Append(LicenseCount);
            if (ExpirationDate.HasValue)
                sbSign.Append(ExpirationDate.Value.ToFileTimeUtc());

            byte[] signatureBytes = ezrsa_private.SignData(Encoding.UTF8.GetBytes(sbSign.ToString()), new SHA1CryptoServiceProvider());

            // serialize to string
            var signature = new StringBuilder();
            for (int i = 0; i < signatureBytes.Length; ++i)
                signature.Append(signatureBytes[i].ToString("X2"));

            Hash = signature.ToString();
        }

        public void Save(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var fs = File.OpenWrite(filePath)) {
                Save(fs);
            }
        }

        public void Save(Stream stream)
        {
            var xs = new XmlSerializer(typeof(List<LicenseInfo>), new XmlRootAttribute("Licensing"));
            var settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            // new UTF8Encoding(false) prevents BOM characters which break reading
            var writer = XmlWriter.Create(stream, settings); // new XmlTextWriter(fs, new UTF8Encoding(false));

            //// this removes the namespaces
            //var ns = new XmlSerializerNamespaces();
            //ns.Add("", "");

            xs.Serialize(writer, new List<LicenseInfo>() { this }); //, ns);
        }

        public bool SignCheck(string productKey)
        {
            EZRSA ezrsa_public = new EZRSA(1024);
            ezrsa_public.FromXmlString(productKey);

            byte[] signatureBytes = new byte[128];
            for (int i = 0; i < 128; i++)
                signatureBytes[i] = Convert.ToByte(Hash.Substring(i * 2, 2), 16);

            var sbVerify = new StringBuilder();
            sbVerify.Append(ProductCode);
            sbVerify.Append(VersionCode);
            sbVerify.Append(LicenseTypeCode);
            if (LicenseCount.HasValue)
                sbVerify.Append(LicenseCount);
            if (ExpirationDate != null)
                sbVerify.Append(ExpirationDate.Value.ToFileTimeUtc());

            return ezrsa_public.VerifyData(Encoding.UTF8.GetBytes(sbVerify.ToString()), new SHA1CryptoServiceProvider(), signatureBytes);
        }

        public static IEnumerable<LicenseInfo> Load(string licenseFile)
        {
            var xs = new XmlSerializer(typeof(List<LicenseInfo>), new XmlRootAttribute("Licensing"));
            using (var fs = File.OpenRead(licenseFile)) {
                return (List<LicenseInfo>)xs.Deserialize(fs);
            }
        }

    }
}
