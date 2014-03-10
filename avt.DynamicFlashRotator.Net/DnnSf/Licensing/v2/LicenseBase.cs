using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2
{
    public abstract class LicenseBase
    {
        public LicenseInfo License { get; set; }

        public string Type { get { return GetType().Name; } }

        public abstract LicenseStatus Status { get; }

        //public string BuyUrl { get { return App.BuyUrl; } }

        //public string DocUrl { get { return App.DocUrl; } }

        //public string Version { get { return App.Version; } }

    }
}
