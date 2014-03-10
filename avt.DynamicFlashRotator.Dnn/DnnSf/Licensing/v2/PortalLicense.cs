using DotNetNuke.Entities.Portals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2
{
    public class PortalLicense : LicenseBase
    {
        public override LicenseStatus Status
        {
            get
            {
                if (!License.LicenseCount.HasValue)
                    return new LicenseStatus() {
                        Type = LicenseStatus.eType.Error,
                        Code = LicenseStatus.eCode.InvalidLicenseCount,
                        Message = "Invalid license count"
                    };

                // TODO: maybe enforce to link a portal license to 1 portal?
                var portalCount = new PortalController().GetPortals().Count;
                if (portalCount > License.LicenseCount.Value)
                    return new LicenseStatus() {
                        Type = LicenseStatus.eType.Warning,
                        Code = LicenseStatus.eCode.WarningNotCovered,
                        Message = "You have more portals than licenses. The functionality may be limited on other portals."
                    };

                return new LicenseStatus() {
                    Type = LicenseStatus.eType.Info,
                    Code = LicenseStatus.eCode.Ok,
                    Message = "This copy is licensed for current portal."
                };
            }
        }
    }
}
