using System;
using System.Collections.Generic;
using System.Web;

namespace avt.DynamicFlashRotator.Net.RegCore.Storage
{
    internal interface IActivationDataStore
    {
        Dictionary<string, ILicenseActivation> GetActivations();
        void AddActivation(string regCode, string host, string actCode, string productKey, string baseProductCode, string baseVersionCode);
        void AddActivation(ILicenseActivation act);
        void Remove(ILicenseActivation act);
        void RemoveTrial();
        void RemoveAll();
    }
}