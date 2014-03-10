using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2
{
    public class NoLicense : LicenseBase
    {
        [ScriptIgnore]
        public string Message { get; set; }

        public override LicenseStatus Status
        {
            get
            {
                return new LicenseStatus() {
                    Type = LicenseStatus.eType.Error,
                    Code = LicenseStatus.eCode.Invalid,
                    Message = Message ?? "No license file present"
                };
            }
        }
    }
}
