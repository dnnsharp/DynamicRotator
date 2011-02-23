
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
using System.IO;
using System.Reflection;


namespace avt.DynamicFlashRotator.Net
{
    internal class AvtRegCoreClient
    {
        Dictionary<string, AvtActivation> _initActivations;
        Dictionary<string, AvtActivation> _validActivations;
        AvtActivationDataSource _src;
        string _regCoreSrv;

        private AvtRegCoreClient(string regCoreSrv, AvtActivationDataSource src)
        {
            // fill activations
            _initActivations = src.GetActivations();
            _validActivations = new Dictionary<string, AvtActivation>();
            _src = src;
            _regCoreSrv = regCoreSrv;
        }

        static public AvtRegCoreClient Get(string regCoreSrv, string productCode, AvtActivationDataSource src, bool clearCache)
        {
            lock (typeof(AvtRegCoreClient)) {
                AvtRegCoreClient regCoreClient;
                if (clearCache == false && HttpRuntime.Cache["avt.RegCoreClient." + productCode] != null) {
                    regCoreClient = (AvtRegCoreClient)HttpRuntime.Cache["avt.RegCoreClient." + productCode];
                } else {

                    // clear cache, in case it's ignored
                    HttpRuntime.Cache.Remove("avt.RegCoreClient." + productCode);

                    // create new client
                    regCoreClient = new AvtRegCoreClient(regCoreSrv, src);
                    HttpRuntime.Cache.Insert("avt.RegCoreClient." + productCode, regCoreClient);
                }

                return regCoreClient;
            }
        }

        public Dictionary<string, AvtActivation> ValidActivations
        {
            get { return _validActivations; }
        }

        public Dictionary<string, AvtActivation> InitActivations
        {
            get { return _initActivations; }
        }

        public void ClearCache(string productCode)
        {
            lock (typeof(AvtRegCoreClient)) {
                HttpRuntime.Cache.Remove("avt.RegCoreClient." + productCode);
            }
        }

        public void ClearAll()
        {
            _src.RemoveAll();

            _initActivations = new Dictionary<string, AvtActivation>();
            _validActivations = new Dictionary<string, AvtActivation>();
        }


        public bool IsActivated(string productCode, string version, string minorVersion, string host, ref bool isTrial)
        {
            if (string.IsNullOrEmpty(host))
                return false;

            host = host.ToLower();

            // first, check if we have it in cache
            if (_validActivations.ContainsKey(host) && _validActivations[host].IsValid(productCode, version, minorVersion) && _validActivations[host].TmpKey == GetHash(host + _validActivations[host].ActivationCode)) {
                if (_validActivations[host].RegCode.VariantCode == "30DAY" || _validActivations[host].RegCode.VariantCode == "14DAY") {
                    isTrial = true;
                }
                return true;
            }

            //HttpContext.Current.Response.Write(GetHash("blacklinegroup.com"));
            //if (host.GetHashCode()

            string checkHost = host;
            // let's check the rest of activations
            foreach (AvtActivation act in _initActivations.Values) {

                if (!act.IsValid(productCode, version, minorVersion))
                    continue;

                if (act.RegCode.VariantCode == "30DAY" || act.RegCode.VariantCode == "14DAY") {
                    isTrial = true;
                    return true;
                }

                if (act.RegCode.VariantCode == "ENT")
                    return true;

                // remove www
                if (host.IndexOf("www.") == 0 || host.IndexOf("http://www.") == 0 || host.IndexOf("https://www.") == 0) host = host.Substring(host.IndexOf("www.") + 4);

                // for domain license, also remove dev. and staging. (we grant these free)
                if (act.RegCode.VariantCode == "DOM") {
                    if (host.IndexOf("dev.") == 0 || host.IndexOf("http://dev.") == 0 || host.IndexOf("https://dev.") == 0) host = host.Substring(host.IndexOf("dev.") + 4);
                    if (host.IndexOf("test.") == 0 || host.IndexOf("http://test.") == 0 || host.IndexOf("https://test.") == 0) host = host.Substring(host.IndexOf("test.") + 5);
                    if (host.IndexOf("staging.") == 0 || host.IndexOf("http://staging.") == 0 || host.IndexOf("https://staging.") == 0) host = host.Substring(host.IndexOf("staging.") + 8);
                }

                if (act.RegCode.VariantCode == "XDOM") {
                    // see if host is included in baseHost
                    if (host.IndexOf(act.Host) + act.Host.Length != host.Length) {
                        continue; // invalid
                    }
                    host = act.Host;
                }

                if ((act.RegCode.VariantCode == "SRV") && Regex.Match(host, ".*\\d+\\.\\d+\\.\\d+\\.\\d+.*").Length == 0) {
                    // we need to get IP of domain
                    try {
                        bool bFound = false;
                        foreach (IPAddress addr in System.Net.Dns.GetHostEntry(host).AddressList) {
                            if (addr.ToString() == act.Host) {
                                bFound = true;
                                break;
                            }
                        }
                        if (!bFound)
                            continue; // IP not found

                        host = act.Host;

                    } catch {
                        continue;
                    }
                }

                if (act.Host == host) {
                    // put in tmp key
                    AvtActivation actCpy = act.Clone();
                    actCpy.TmpKey = GetHash(host + actCpy.ActivationCode);
                    _validActivations[checkHost] = actCpy;
                    return true;
                }
            }

            return false;
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

        public AvtActivation Activate(string regCode, string productCode, string version, string minorVersion, string host, string productKey)
        {
            if (string.IsNullOrEmpty(host))
                throw new Exception("Invalid host");

            host = host.ToLower();

            AvtRegistrationCode r = new AvtRegistrationCode(regCode);

            // remove www
            if (host.IndexOf("www.") == 0 || host.IndexOf("http://www.") == 0 || host.IndexOf("https://www.") == 0) host = host.Substring(host.IndexOf("www.") + 4);

            // for domain license, also remove dev. and staging. (we grant these free)
            if (r.VariantCode == "DOM") {
                if (host.IndexOf("dev.") == 0 || host.IndexOf("http://dev.") == 0 || host.IndexOf("https://dev.") == 0) host = host.Substring(host.IndexOf("dev.") + 4);
                if (host.IndexOf("test.") == 0 || host.IndexOf("http://test.") == 0 || host.IndexOf("https://test.") == 0) host = host.Substring(host.IndexOf("test.") + 5);
                if (host.IndexOf("staging.") == 0 || host.IndexOf("http://staging.") == 0 || host.IndexOf("https://staging.") == 0) host = host.Substring(host.IndexOf("staging.") + 8);
            }

            Dictionary<string, string> data = new Dictionary<string, string>();
            Dictionary<string, string> prvData = new Dictionary<string, string>();
            data["product"] = productCode; // this is not encrypted because we need to extract the private key on the server side
            prvData["regcode"] = regCode;
            prvData["version"] = version;
            prvData["minorversion"] = minorVersion;
            prvData["hostname"] = host;

            XmlDocument xmlAct = new XmlDocument();
            try {
                xmlAct.LoadXml(SendData(_regCoreSrv + "?cmd=activate", productKey, data, prvData));
            } catch (Exception e) {
                throw new Exception("An error occured (" + e.Message + ")");
            }

            if (xmlAct["error"] != null) {
                throw new Exception(xmlAct["error"].InnerText);
            }

            AvtActivation act = new AvtActivation();
            act.RegistrationCode = regCode;
            act.Host = xmlAct.FirstChild["host"].InnerText;
            act.ActivationCode = xmlAct.FirstChild["activation_code"].InnerText;
            act.ProductKey = xmlAct.FirstChild["product_key"].InnerText;
            act.BaseProductCode = r.ProductCode;
            act.BaseProductVersion = xmlAct.FirstChild["version"].InnerText;

            if (!act.IsValid(productCode, version, minorVersion)) {
                throw new Exception("Invalid activation");
            }

            // add activation
            _src.AddActivation(act);
            _initActivations[act.Host] = act;
            _validActivations[act.Host] = act;

            return act;
        }


        public AvtActivation Activate(string regCode, string productCode, string version, string minorVersion, string host, string productKey, string actCode)
        {
            if (string.IsNullOrEmpty(host))
                throw new Exception("Invalid host");

            host = host.ToLower();

            AvtRegistrationCode r = new AvtRegistrationCode(regCode);

            // remove www
            if (host.IndexOf("www.") == 0 || host.IndexOf("http://www.") == 0 || host.IndexOf("https://www.") == 0) host = host.Substring(host.IndexOf("www.") + 4);

            // for domain license, also remove dev. and staging. (we grant these free)
            if (r.VariantCode == "DOM") {
                if (host.IndexOf("dev.") == 0 || host.IndexOf("http://dev.") == 0 || host.IndexOf("https://dev.") == 0) host = host.Substring(host.IndexOf("dev.") + 4);
                if (host.IndexOf("test.") == 0 || host.IndexOf("http://test.") == 0 || host.IndexOf("https://test.") == 0) host = host.Substring(host.IndexOf("test.") + 5);
                if (host.IndexOf("staging.") == 0 || host.IndexOf("http://staging.") == 0 || host.IndexOf("https://staging.") == 0) host = host.Substring(host.IndexOf("staging.") + 8);
            }

            AvtActivation act = new AvtActivation();
            act.RegistrationCode = regCode;
            act.Host = host;
            act.ActivationCode = actCode;
            act.ProductKey = productKey;
            act.BaseProductCode = r.ProductCode;
            act.BaseProductVersion = version;

            if (!act.IsValid(productCode, version, minorVersion)) {
                throw new Exception("Invalid activation");
            }

            // add activation
            _src.AddActivation(act);
            _initActivations[act.Host] = act;
            _validActivations[act.Host] = act;

            return act;
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
    }

    internal class AvtActivation
    {
        string _Host;
        public string Host
        {
            get { return string.IsNullOrEmpty(_Host) ? "" : _Host.ToLower(); }
            set { _Host = value; }
        }

        public string RegistrationCode;
        public string ActivationCode;
        public string ProductKey;
        public string BaseProductCode;
        public string BaseProductVersion;
        public string TmpKey = "";

        AvtRegistrationCode _RegCode = null;
        public AvtRegistrationCode RegCode
        {
            get
            {
                if (_RegCode == null) {
                    try {
                        _RegCode = new AvtRegistrationCode(RegistrationCode);
                    } catch {
                        _RegCode = null;
                    }
                }
                return _RegCode;
            }
        }

        public AvtActivation()
        {
        }

        public AvtActivation Clone()
        {
            AvtActivation act = new AvtActivation();
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



    internal interface AvtActivationDataSource
    {
        Dictionary<string, AvtActivation> GetActivations();
        void AddActivation(string regCode, string host, string actCode, string productKey, string baseProductCode, string baseVersionCode);
        void AddActivation(AvtActivation act);
        void Remove(AvtActivation act);
        void RemoveTrial();
        void RemoveAll();
    }


    internal class AvtActivationDataSourceDb : AvtActivationDataSource
    {
        string _conStr;
        string _dbo;
        string _qualifier;
        string _table;

        public AvtActivationDataSourceDb(string conStr, string dbo, string qualifier, string table)
        {
            _conStr = conStr;
            _dbo = dbo;
            _qualifier = qualifier;
            _table = table;
        }

        public Dictionary<string, AvtActivation> GetActivations()
        {
            SqlConnection conn = new SqlConnection(_conStr);
            SqlDataAdapter a = new SqlDataAdapter("select * from " + _dbo + _qualifier + _table, conn);
            DataSet s = new DataSet(); a.Fill(s);

            Dictionary<string, AvtActivation> activations = new Dictionary<string, AvtActivation>();
            foreach (DataRow dr in s.Tables[0].Rows) {
                AvtActivation act = new AvtActivation();
                act.Host = dr["Host"].ToString();
                act.RegistrationCode = dr["RegistrationCode"].ToString();
                act.ActivationCode = dr["ActivationCode"].ToString();
                act.ProductKey = dr["ProductKey"].ToString();
                act.BaseProductVersion = dr["BaseProductVersion"].ToString();
                act.BaseProductCode = dr["BaseProductCode"].ToString();
                //Console.WriteLine(dr[0].ToString());
                activations[act.Host] = act;
            }

            return activations;
        }

        public void AddActivation(string regCode, string host, string actCode, string productKey, string baseProductCode, string baseVersionCode)
        {
            string sqlF = "DELETE FROM {0} WHERE RegistrationCode = '{1}' AND Host = '{2}'; INSERT INTO {0} VALUES('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')";
            string sql = string.Format(sqlF, _dbo + _qualifier + _table, regCode, host.Replace("'", "''"), actCode.Replace("'", "''"), productKey.Replace("'", "''"), baseProductCode, baseVersionCode);
            SqlConnection conn = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void AddActivation(AvtActivation act)
        {
            AddActivation(act.RegistrationCode, act.Host, act.ActivationCode, act.ProductKey, act.BaseProductCode, act.BaseProductVersion);
        }


        public void Remove(string regCode, string host)
        {
            string sqlF = "DELETE FROM {0} WHERE RegistrationCode = '{1}' AND Host = '{2}'; ";
            string sql = string.Format(sqlF, _dbo + _qualifier + _table, regCode, host.Replace("'", "''"));
            SqlConnection conn = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void RemoveTrial()
        {
            string sqlF = "DELETE FROM {0} WHERE RegistrationCode LIKE 'DAY-'";
            string sql = string.Format(sqlF, _dbo + _qualifier + _table);
            SqlConnection conn = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Remove(AvtActivation act)
        {
            Remove(act.RegistrationCode, act.Host);
        }

        public void RemoveAll()
        {
            string sqlF = "DELETE FROM {0}";
            string sql = string.Format(sqlF, _dbo + _qualifier + _table);
            SqlConnection conn = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }


    internal class AvtActivationDataSourceFile : AvtActivationDataSource
    {

        public AvtActivationDataSourceFile()
        {

        }

        string LicenseFilePath
        {
            get
            {
                Assembly asm = Assembly.GetAssembly(GetType());
                string asmPath = asm.CodeBase;
                if (asmPath.IndexOf(System.AppDomain.CurrentDomain.BaseDirectory) == -1) {
                    // it's not in the bin folder
                    asmPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
                }
                return Path.Combine(asmPath, GetType().Namespace + ".lic");
            }
        }

        public Dictionary<string, AvtActivation> GetActivations()
        {
            lock (this) {
                Dictionary<string, AvtActivation> activations = new Dictionary<string, AvtActivation>();

                if (!File.Exists(LicenseFilePath)) {
                    return activations;
                }

                StringBuilder sbAct = new StringBuilder();
                sbAct.Append(File.ReadAllText(LicenseFilePath));

                StringReader srAct = new StringReader(File.ReadAllText(LicenseFilePath));

                string line;
                while ((line = srAct.ReadLine()) != null) {
                    if (line.Length == 0)
                        continue;

                    AvtActivation act = new AvtActivation();
                    act.RegistrationCode = line;
                    act.Host = srAct.ReadLine();
                    act.ActivationCode = srAct.ReadLine();
                    act.ProductKey = srAct.ReadLine();
                    act.BaseProductVersion = srAct.ReadLine();
                    act.BaseProductCode = srAct.ReadLine();
                    activations[act.Host] = act;
                }

                return activations;
            }
        }

        public void AddActivation(string regCode, string host, string actCode, string productKey, string baseProductCode, string baseVersionCode)
        {
            lock (this) {

                RemoveTrial();

                // first, check if it already exists
                HttpContext.Current.Response.Write(GetActivations().Values.Count);

                foreach (AvtActivation act in GetActivations().Values) {
                    if (act.RegistrationCode == regCode && act.Host == host) {
                        return;
                    }
                }

                StringBuilder sbAct = new StringBuilder();
                sbAct.AppendLine(); // 1 empty line separates activations
                sbAct.AppendLine(regCode);
                sbAct.AppendLine(host);
                sbAct.AppendLine(actCode);
                sbAct.AppendLine(productKey);
                sbAct.AppendLine(baseProductCode);
                sbAct.AppendLine(baseVersionCode);

                try {
                    File.AppendAllText(LicenseFilePath, sbAct.ToString());
                } catch (Exception e) {
                    string fileContents = "";
                    try { fileContents = File.ReadAllText(LicenseFilePath); } catch { }
                    fileContents += sbAct.ToString();
                    throw new Exception(fileContents, e);
                }

            }
        }

        void AppendActivation(AvtActivation act)
        {
            StringBuilder sbAct = new StringBuilder();
            sbAct.AppendLine(); // 1 empty line separates activations
            sbAct.AppendLine(act.RegistrationCode);
            sbAct.AppendLine(act.Host);
            sbAct.AppendLine(act.ActivationCode);
            sbAct.AppendLine(act.ProductKey);
            sbAct.AppendLine(act.BaseProductCode);
            sbAct.AppendLine(act.BaseProductVersion);

            File.AppendAllText(LicenseFilePath, sbAct.ToString());
        }

        public void AddActivation(AvtActivation act)
        {
            AddActivation(act.RegistrationCode, act.Host, act.ActivationCode, act.ProductKey, act.BaseProductCode, act.BaseProductVersion);
        }


        public void Remove(string regCode, string host)
        {
            lock (this) {
                Dictionary<string, AvtActivation> activations = GetActivations();
                bool bFound = false;

                foreach (string hostAct in activations.Keys) {
                    if (activations[hostAct].RegistrationCode == regCode && hostAct == host) {
                        bFound = true;
                        activations.Remove(hostAct);
                        break;
                    }
                }

                if (bFound) {
                    File.WriteAllText(LicenseFilePath, "");
                    foreach (AvtActivation act in activations.Values) {
                        AppendActivation(act);
                    }

                }
            }
        }

        public void RemoveTrial()
        {
            lock (this) {
                Dictionary<string, AvtActivation> activations = GetActivations();
                bool bFound = false;

                foreach (string hostAct in activations.Keys) {
                    if (activations[hostAct].RegistrationCode.IndexOf("DAY-") > 0) {
                        bFound = true;
                        activations.Remove(hostAct);
                        break;
                    }
                }

                if (bFound) {
                    File.WriteAllText(LicenseFilePath, "");
                    foreach (AvtActivation act in activations.Values) {
                        AppendActivation(act);
                    }

                }
            }
        }

        public void Remove(AvtActivation act)
        {
            Remove(act.RegistrationCode, act.Host);
        }

        public void RemoveAll()
        {
            lock (this) {
                File.WriteAllText(LicenseFilePath, "");
            }
        }

    }



    public class AvtRegistrationCode
    {
        static Random rGen = new Random();

        string _RegCode;
        string _prodCode;
        string _variantCode;
        string _hashCheck;
        string _custPart;
        string _randPart;

        DateTime _dateExpire = DateTime.MinValue;

        public bool HasTimeBomb
        {
            get { return _dateExpire != DateTime.MinValue; }
        }

        public string ProductCode
        {
            get { return _prodCode; }
        }

        public string VariantCode
        {
            get { return _variantCode; }
        }

        public DateTime DateExpire
        {
            get { return _dateExpire; }
        }

        public string R
        {
            get { return _randPart; }
        }

        public bool IsExpired()
        {
            return HasTimeBomb && _dateExpire < DateTime.Now;
        }

        public AvtRegistrationCode(string regCode)
        {
            _RegCode = regCode;

            // parse parts
            string[] parts = regCode.Split('-');
            int iPart = 0;
            _prodCode = parts[iPart++];
            _variantCode = parts[iPart++];
            if (parts.Length == 4) { // has timebomb
                DateTime centuryBegin = new DateTime(2001, 1, 1);
                _dateExpire = centuryBegin.AddDays(Convert.ToInt32(parts[iPart++]));
            }

            if (parts.Length < 3)
                throw new FormatException("Invalid Registration Code Format");

            _hashCheck = parts[iPart].Substring(0, 20);
            _custPart = parts[iPart].Substring(20);
            _randPart = parts[iPart].Substring(28);

            //HttpContext.Current.Response.Write(_hashCheck+"<Br />");
            //HttpContext.Current.Response.Write(_custPart + "<Br />");
            //HttpContext.Current.Response.Write(_randPart + "<Br />");

            // validate length
            if (_randPart.Length != 6)
                throw new FormatException("Invalid Registration Code Format");
        }

        private AvtRegistrationCode() // private constructor called via Generate
        {
        }


        public override string ToString()
        {
            return _RegCode;
        }

    }
}