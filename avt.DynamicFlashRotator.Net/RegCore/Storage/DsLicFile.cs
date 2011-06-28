using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using avt.DynamicFlashRotator.Net.RegCore;
using System.Text;
using System.Reflection;

namespace avt.DynamicFlashRotator.Net.RegCore.Storage
{
    internal class DsLicFile : IActivationDataStore
    {
        public DsLicFile()
        {

        }

        string LicenseFilePath {
            get {
                Assembly asm = Assembly.GetAssembly(GetType());
                string asmPath = asm.CodeBase.Replace("file:///", "").Replace('/', '\\');

                if (Path.GetExtension(asmPath).ToLower() == ".dll") {
                    asmPath = Path.GetDirectoryName(asmPath);
                }

                if (asmPath.IndexOf(System.AppDomain.CurrentDomain.BaseDirectory.Replace('/','\\')) == -1) {
                    // it's not in the bin folder
                    asmPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
                }

                return Path.Combine(asmPath, GetType().Namespace.Replace(".RegCore.Storage", "") + ".lic");
            }
        }

        Dictionary<string, ILicenseActivation> _Activations = null;
        Dictionary<string, ILicenseActivation> Activations
        {
            get {
                if (_Activations != null)
                    return _Activations;

                lock (typeof(DsLicFile)) {
                    _Activations = new Dictionary<string, ILicenseActivation>();

                    if (!File.Exists(LicenseFilePath)) {
                        return _Activations;
                    }

                    StringBuilder sbAct = new StringBuilder();
                    sbAct.Append(File.ReadAllText(LicenseFilePath));

                    StringReader srAct = new StringReader(File.ReadAllText(LicenseFilePath));

                    string line;
                    while ((line = srAct.ReadLine()) != null) {
                        if (line.Length == 0)
                            continue;

                        LicenseActivation act = new LicenseActivation();
                        act.RegistrationCode = line;
                        act.Host = srAct.ReadLine();
                        act.ActivationCode = srAct.ReadLine();
                        act.ProductKey = srAct.ReadLine();
                        act.BaseProductCode = srAct.ReadLine(); 
                        act.BaseProductVersion = srAct.ReadLine();
                        _Activations[act.Host] = act;
                    }

                    return _Activations;
                }
            }
        }

        public Dictionary<string, ILicenseActivation> GetActivations()
        {
            return Activations;
        }

        public void AddActivation(string regCode, string host, string actCode, string productKey, string baseProductCode, string baseVersionCode)
        {
            LicenseActivation act = new LicenseActivation();
            act.RegistrationCode = regCode;
            act.Host = host;
            act.ActivationCode = actCode;
            act.ProductKey = productKey;
            act.BaseProductCode = baseProductCode;
            act.BaseProductVersion = baseVersionCode;
            AddActivation(act);
        }

        public void AddActivation(ILicenseActivation act)
        {
            lock (typeof(DsLicFile)) {
                // first, check if it already exists
                foreach (LicenseActivation existingAct in Activations.Values) {
                    if (existingAct.RegistrationCode == act.RegistrationCode && existingAct.Host == act.Host) {
                        //if (existingAct.BaseProductCode == act.BaseProductCode && existingAct.BaseProductVersion == act.BaseProductVersion) {
                        //    return;
                        //} 

                        // different version? remove the previous
                        Activations.Remove(existingAct.Host);
                        break;
                    }
                }

                Activations[act.Host] = act;
                SaveAllActivations();
            }
        }


        public void Remove(string regCode, string host)
        {
            lock (typeof(DsLicFile)) {
                Dictionary<string, ILicenseActivation> activations = GetActivations();
                bool bFound = false;

                foreach (string hostAct in activations.Keys) {
                    if (activations[hostAct].RegistrationCode == regCode && hostAct == host) {
                        bFound = true;
                        activations.Remove(hostAct);
                        break;
                    }
                }

                if (bFound)
                    SaveAllActivations();
            }
        }

        public void RemoveTrial()
        {
            lock (typeof(DsLicFile)) {
                Dictionary<string, ILicenseActivation> activations = GetActivations();
                bool bFound = false;

                foreach (string hostAct in activations.Keys) {
                    if (activations[hostAct].RegistrationCode.IndexOf("DAY-") > 0) {
                        bFound = true;
                        activations.Remove(hostAct);
                        break;
                    }
                }

                if (bFound)
                    SaveAllActivations();
            }
        }

        public void Remove(ILicenseActivation act)
        {
            Remove(act.RegistrationCode, act.Host);
        }

        public void RemoveAll()
        {
            lock (typeof(DsLicFile)) {
                try {
                    File.Delete(LicenseFilePath);
                } catch {
                    File.WriteAllText(LicenseFilePath, "");
                }
            }
        }


        void SaveAllActivations()
        {
            File.WriteAllText(LicenseFilePath, "");
            foreach (ILicenseActivation act in Activations.Values) {
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
        }
    }
}