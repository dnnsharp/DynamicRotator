using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2
{
    public static class LicenseFactory
    {
        /// <summary>
        /// Loads best matching license from an XML license file
        /// </summary>
        /// <param name="licenseFile"></param>
        /// <param name="currentVersionCode"></param>
        /// <returns></returns>
        public static LicenseBase Get(string licenseFile, string currentVersionCode, string productKey)
        {
            if (!File.Exists(licenseFile))
                return new NoLicense();
                //throw new ArgumentException("Missing license file " + licenseFile);
                  
            var xs = new XmlSerializer(typeof(List<LicenseInfo>), new XmlRootAttribute("Licensing"));
            using (var fs = File.OpenRead(licenseFile)) {
                try {
                    var licenses = (List<LicenseInfo>)xs.Deserialize(fs);
                    return Get(licenses, currentVersionCode, productKey);
                } catch (Exception ex) {
                    return new NoLicense() { Message = "Could not parse license file." };
                }

            }
        }

        /// <summary>
        /// returns the license from a list of licenses
        /// </summary>
        /// <param name="licenses"></param>
        /// <param name="currentVersionCode"></param>
        /// <returns></returns>
        public static LicenseBase Get(IEnumerable<LicenseInfo> licenses, string currentVersionCode, string productKey)
        {
            // only take into account licenses for current version
            licenses = licenses.Where(x => x.VersionCode == currentVersionCode && x.SignCheck(productKey));

            // if we have a Host, Enterprise or Developer, return it
            var key = licenses.FirstOrDefault(x => x.LicenseTypeCode == "HOST" || x.LicenseTypeCode == "SRV" || x.LicenseTypeCode == "ENT" || x.LicenseTypeCode == "DEV");
            if (key != null)
                return new FullSiteLicense() { License = key };

            // look for a portal key
            key = licenses.FirstOrDefault(x => x.LicenseTypeCode == "PRTL" || x.LicenseTypeCode == "DOM" || x.LicenseTypeCode == "3DOM");
            if (key != null)
                return new PortalLicense() { License = key };

            // look for trial key
            key = licenses.FirstOrDefault(x => x.LicenseTypeCode == "30DAY" || x.LicenseTypeCode == "14DAY");
            if (key != null)
                return new TrialLicense() { License = key };

            // no license
            return new NoLicense();
        }



    }
}
