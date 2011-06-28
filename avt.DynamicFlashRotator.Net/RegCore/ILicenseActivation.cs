using System;
namespace avt.DynamicFlashRotator.Net.RegCore
{
    public interface ILicenseActivation
    {
        string ActivationCode { get; set; }
        string BaseProductCode { get; set; }
        string BaseProductVersion { get; set; }
        ILicenseActivation Clone();
        string Host { get; set; }
        bool IsValid(string productCode, string versionCode);
        string ProductKey { get; set; }
        RegCode RegCode { get; }
        string RegistrationCode { get; set; }
        string TmpKey { get; set; }
    }
}
