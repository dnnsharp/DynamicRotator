using System;

namespace avt.DynamicFlashRotator.Net.RegCore
{
    public interface IRegCoreClient
    {
        ILicenseActivation Activate(string regCode, string productCode, string version, string host, string productKey);
        ILicenseActivation Activate(string regCode, string productCode, string version, string host, string productKey, string actCode);
        System.Collections.Generic.Dictionary<string, ILicenseActivation> AllActivations { get; }
        void ClearAll();
        void ClearCache(string productCode);
        ILicenseActivation GetValidActivation(string productCode, string version, string host);
        bool IsActivated(string productCode, string version, string host);
        bool IsTrial(string productCode, string version, string host);
        bool IsTrialExpired(string productCode, string version, string host);
        RegCoreServer RegCoreSrv { get; }
        void Upgrade(string productCode, string version, string productKey, bool bThrowErrors);
    }
}
