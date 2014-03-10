using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2
{
    public class FullSiteLicense : LicenseBase
    {
        //public string LicenseName { get; set; }

        public FullSiteLicense() //string licenseName)
        {
            //LicenseName = licenseName;
        }

        public override LicenseStatus Status
        {
            get
            {
                return new LicenseStatus() {
                    Type = LicenseStatus.eType.Info,
                    Code = LicenseStatus.eCode.Ok,
                    //Message = string.Format("{0} license is active.", LicenseName)
                    Message = "This copy is licensed for all portals in current DNN instance."
                };
            }
        }
    }
}
