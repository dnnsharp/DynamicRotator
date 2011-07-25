
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Reflection;
using System.IO;
using avt.DynamicFlashRotator.Net.RegCore.Storage;
using avt.DynamicFlashRotator.Net.RegCore.Cryptography;


namespace avt.DynamicFlashRotator.Net.RegCore
{
    internal class RegCoreClient : IRegCoreClient
    {
        Dictionary<string, ILicenseActivation> _initActivations;
        public Dictionary<string, ILicenseActivation> AllActivations { get { return _initActivations; } }

        Dictionary<string, ILicenseActivation> _validActivations;
        IActivationDataStore _src;

        RegCoreServer _regCoreSrv;
        public RegCoreServer RegCoreSrv { get { return _regCoreSrv; } }

        static Random _R = new Random();

        private RegCoreClient(string regCoreSrv, IActivationDataStore src)
        {
            // fill activations
            _initActivations = src.GetActivations();
            _validActivations = new Dictionary<string, ILicenseActivation>();
            _src = src;
            _regCoreSrv = new RegCoreServer(regCoreSrv);
        }

        static public RegCoreClient Get(string regCoreSrv, string productCode, IActivationDataStore src, bool clearCache)
        {
            lock (typeof(RegCoreClient)) {
                RegCoreClient regCoreClient;
                if (clearCache == false && HttpRuntime.Cache["avt.DynamicFlashRotator.Net.RegCoreClient." + productCode] != null) {
                    regCoreClient = (RegCoreClient)HttpRuntime.Cache["avt.DynamicFlashRotator.Net.RegCoreClient." + productCode];
                } else {

                    // clear cache, in case it's ignored
                    HttpRuntime.Cache.Remove("avt.DynamicFlashRotator.Net.RegCoreClient." + productCode);

                    // create new client
                    regCoreClient = new RegCoreClient(regCoreSrv, src);
                    HttpRuntime.Cache.Insert("avt.DynamicFlashRotator.Net.RegCoreClient." + productCode, regCoreClient);
                }

                return regCoreClient;
            }
        }

        public static int GetRandom()
        {
            return _R.Next();
        }

        public void ClearCache(string productCode)
        {
            lock (typeof(RegCoreClient)) {
                HttpRuntime.Cache.Remove("avt.DynamicFlashRotator.Net.RegCoreClient." + productCode);
            }
        }

        public void ClearAll()
        {
            _src.RemoveAll();

            _initActivations = new Dictionary<string, ILicenseActivation>();
            _validActivations = new Dictionary<string, ILicenseActivation>();
        }


        public ILicenseActivation GetValidActivation(string productCode, string version, string host)
        {
            // first, check if we have it in cache
            if (_validActivations.ContainsKey(host) && _validActivations[host].IsValid(productCode, version) && _validActivations[host].TmpKey == GetHash(host + _validActivations[host].ActivationCode)) {
                return _validActivations[host];
            }

            string checkHost = host;

            // let's check the rest of activations
            foreach (LicenseActivation act in _initActivations.Values) {

                if (act.RegCode.IsTrial) {
                    continue; // check this later if no other license is found
                }

                if (!act.IsValid(productCode, version))
                    continue;

                if (act.RegCode.VariantCode == "ENT" || act.RegCode.VariantCode == "DEV")
                    return act;

                try {
                    // remove www, dev, staging, etc
                    host = SanitizeHost(host, act.Host, act.RegCode.VariantCode);
                    // if it's server type license, convert domain to IP
                    host = HostToIp(host, act.Host, act.RegCode.VariantCode);
                } catch (ArgumentException) {
                    continue;
                }

                if (act.Host == host) {
                    // put in tmp key
                    ILicenseActivation actCpy = act.Clone();
                    actCpy.TmpKey = GetHash(host + actCpy.ActivationCode);
                    _validActivations[checkHost] = actCpy;
                    return act;
                }
            }

            // let's check the rest of activations
            foreach (LicenseActivation act in _initActivations.Values) {
                if (act.RegCode.IsTrial) {
                    if (!act.IsValid(productCode, version))
                        continue;

                    return act;
                }
            }

            return null;
        }

        public bool IsActivated(string productCode, string version, string host)
        {
            if (host == "localhost")
                return true;

            ILicenseActivation act = GetValidActivation(productCode, version, host);
            return act != null;
        }

        public bool IsTrial(string productCode, string version, string host)
        {
            if (host == "localhost")
                return false;

            ILicenseActivation act = GetValidActivation(productCode, version, host);
            return act != null && act.RegCode.IsTrial;
        }

        public bool IsTrialExpired(string productCode, string version, string host)
        {
            if (host == "localhost")
                return false;

            if (AllActivations.Count == 0 || !IsTrial(productCode, version, host))
                return false;

            bool trialExpired = true;
            foreach (LicenseActivation act in AllActivations.Values) {
                if (act.RegCode.IsTrial && !act.RegCode.IsExpired()) {
                    trialExpired = false;
                }
            }

            return trialExpired;
        }

        public ILicenseActivation Activate(string regCode, string productCode, string version, string host, string productKey)
        {
            RegCode r = new RegCode(regCode);

            // remove www, dev, staging, etc
            host = SanitizeHost(host, "", r.VariantCode);

            Dictionary<string, string> data = new Dictionary<string, string>();
            Dictionary<string, string> prvData = new Dictionary<string, string>();
            data["product"] = productCode; // this is not encrypted because we need to extract the private key on the server side
            prvData["regcode"] = regCode;
            prvData["version"] = version;
            prvData["minorversion"] = version;
            prvData["hostname"] = host;

            XmlDocument xmlAct = new XmlDocument();
            try {
                xmlAct.LoadXml(SendData(_regCoreSrv.Address + "?cmd=activate", productKey, data, prvData));
            } catch (Exception e) {
                throw new Exception("An error occured (" + e.Message + ")");
            }

            if (xmlAct["error"] != null) {
                throw new Exception(xmlAct["error"].InnerText);
            }

            LicenseActivation act = new LicenseActivation();
            act.RegistrationCode = regCode;
            act.Host = xmlAct.FirstChild["host"].InnerText;
            act.ActivationCode = xmlAct.FirstChild["activation_code"].InnerText;
            act.ProductKey = xmlAct.FirstChild["product_key"].InnerText;
            act.BaseProductCode = r.ProductCode;
            act.BaseProductVersion = xmlAct.FirstChild["version"].InnerText;

            if (!act.IsValid(productCode, version)) {
                throw new Exception("Invalid activation");
            }

            // add activation
            _src.AddActivation(act);
            _initActivations[act.Host] = act;
            _validActivations[act.Host] = act;

            return act;
        }


        public ILicenseActivation Activate(string regCode, string productCode, string version, string host, string productKey, string actCode)
        {
            RegCode r = new RegCode(regCode);

            host = SanitizeHost(host, "", r.VariantCode);

            LicenseActivation act = new LicenseActivation();
            act.RegistrationCode = regCode;
            act.Host = host;
            act.ActivationCode = actCode;
            act.ProductKey = productKey;
            act.BaseProductCode = r.ProductCode;
            act.BaseProductVersion = version;

            if (!act.IsValid(productCode, version)) {
                throw new Exception("Invalid activation");
            }

            // add activation
            _src.AddActivation(act);
            _initActivations[act.Host] = act;
            _validActivations[act.Host] = act;

            return act;
        }

        public void Upgrade(string productCode, string version, string productKey, bool bThrowErrors)
        {
            List<LicenseActivation> activations = new List<LicenseActivation>();
            foreach (LicenseActivation act in _initActivations.Values) {
                activations.Add(act);
            }

            foreach (LicenseActivation act in activations) {
                try {
                    Activate(act.RegistrationCode, productCode, version, act.Host, productKey);
                } catch { 
                    if (bThrowErrors)
                        throw;
                }
            }
        }

        private string SendData(string url, string productKey, Dictionary<string, string> dataParams, Dictionary<string, string> prvDataParams)
        {
            // private params put in xml format and encrypt
            string prvData = null;
            if (prvDataParams.Count > 0) {
                prvData += "<data>";
                foreach (string paramName in prvDataParams.Keys) {
                    prvData += "<" + paramName + ">" + prvDataParams[paramName] + "</" + paramName + ">";
                }
                prvData += "</data>";

                EZRSA ezrsa_public = new EZRSA(1024);
                ezrsa_public.FromXmlString(productKey);

                string encrypted = "";
                int chunkSize = 40;
                for (int i = 0; i < prvData.Length / chunkSize; i++) {
                    byte[] encryptedBytes = ezrsa_public.Encrypt(Encoding.Unicode.GetBytes(prvData.Substring(i * chunkSize, chunkSize)), false);
                    // convert to hexa string
                    for (int j = 0; j < encryptedBytes.Length; ++j) {
                        encrypted += encryptedBytes[j].ToString("X2");
                    }
                }
                if (prvData.Length % chunkSize != 0) {
                    byte[] encryptedBytes = ezrsa_public.Encrypt(Encoding.Unicode.GetBytes(prvData.Substring(prvData.Length - prvData.Length % chunkSize)), false);
                    for (int j = 0; j < encryptedBytes.Length; ++j) {
                        encrypted += encryptedBytes[j].ToString("X2");
                    }
                }
                prvData = encrypted;
            }

            // fill post data (and include private data above)
            string postData = "";
            foreach (string paramName in dataParams.Keys) {
                postData += paramName + "=" + dataParams[paramName] + "&";
            }
            if (prvData != null) {
                postData += "prvdata=" + prvData;
            }
            if (postData[postData.Length - 1] == '&') postData = postData.Substring(0, postData.Length - 1);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.ContentLength = data.Length;
            httpRequest.Timeout = 20 * 1000;
            System.IO.Stream newStream = httpRequest.GetRequestStream();

            // Send the data.
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string responseText = reader.ReadToEnd();
            response.Close();

            return responseText.Trim();
        }


        #region Helpers

        string SanitizeHost(string host, string baseHost, string licenseType)
        {
            host = RemoveSubdomain(host, "www");
            host = RemoveSubdomain(host, "www2");

            // for domain license, also remove dev. and staging. (we grant these free)
            if (licenseType == "DOM" || licenseType == "3DOM" || licenseType == "10DOM") {
                host = RemoveSubdomain(host, "dev");
                host = RemoveSubdomain(host, "test");
                host = RemoveSubdomain(host, "staging");
            } 
            
            else if (licenseType == "XDOM") {
                
                // see if host is included in baseHost
                if (!string.IsNullOrEmpty(baseHost) && host.IndexOf(baseHost) + baseHost.Length != host.Length) {
                    throw new ArgumentException(); // invalid
                }

                if (!string.IsNullOrEmpty(baseHost))
                    host = baseHost;
            }

            return host;
        }

        string HostToIp(string host, string baseHost, string licenseType)
        {
            if ((licenseType == "SRV") && Regex.Match(host, ".*\\d+\\.\\d+\\.\\d+\\.\\d+.*").Length == 0) {
                // we need to get IP of domain
                try {
                    bool bFound = false;
                    foreach (IPAddress addr in System.Net.Dns.GetHostEntry(host).AddressList) {
                        if (addr.ToString() == baseHost) {
                            bFound = true;
                            break;
                        }
                    }
                    if (!bFound)
                        throw new ArgumentException(); // IP not found

                    host = baseHost;

                } catch {
                    throw new ArgumentException();
                }
            }

            return host;
        }

        string RemoveSubdomain(string host, string subdomain)
        {
            // fuzzy match first
            if (host.IndexOf(subdomain) == -1)
                return host;

            // match specific scenarios
            if (host.IndexOf(subdomain + ".") == 0 || host.IndexOf("http://" + subdomain + ".") == 0 || host.IndexOf("https://" + subdomain + ".") == 0)
                host = host.Substring(host.IndexOf(subdomain + ".") + subdomain.Length + 1);

            return host;
        }

        private string GetHash(string tk)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] hashBytes = sha1.ComputeHash(Encoding.Unicode.GetBytes(tk));
            string hash = "";
            for (int i = 0; i < hashBytes.Length / 2; ++i) {
                hash += hashBytes[i].ToString("X2");
            }

            return hash;
        }

        #endregion
    }

}