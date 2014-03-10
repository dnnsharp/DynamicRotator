using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2
{
    public class TrialLicense : LicenseBase
    {
        public override LicenseStatus Status
        {
            get
            {
                if (!License.ExpirationDate.HasValue)
                    throw new Exception("Invalid trial expiration");

                if (License.ExpirationDate.Value.Date < DateTime.Now)
                    return new LicenseStatus() {
                        Type = LicenseStatus.eType.Error,
                        Code = LicenseStatus.eCode.Expired,
                        Message = "Your trial has expired on " + License.ExpirationDate.Value.ToShortDateString()
                    };

                var trialDaysLeft = Math.Ceiling((License.ExpirationDate.Value - DateTime.Now).TotalDays);
                return new LicenseStatus() {
                    Type = trialDaysLeft < 5 ? LicenseStatus.eType.Warning : LicenseStatus.eType.Info,
                    Code = trialDaysLeft < 5 ? LicenseStatus.eCode.WarningExpire : LicenseStatus.eCode.Ok,
                    Message = string.Format("You have {0} days of trial left.", trialDaysLeft)
                }.AddData("DaysLeft", trialDaysLeft);
            }
        }
    }
}
