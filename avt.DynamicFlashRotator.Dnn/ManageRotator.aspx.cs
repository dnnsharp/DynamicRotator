using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avt.DynamicFlashRotator.Net.Data;
using System.Configuration;
using System.Data;
using avt.DynamicFlashRotator.Net.Settings;
using System.Drawing;
using System.Xml;
using avt.DynamicFlashRotator.Net.Services;
using System.Text;
using System.Collections.Specialized;

namespace avt.DynamicFlashRotator.Dnn
{
    public partial class ManageRotator : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            ctlManageRotator.Configuration = new DnnConfiguration();
            RotatorSettings.Init(ctlManageRotator.Configuration);
            ctlManageRotator.ReturnUrl = Server.UrlDecode(Request.QueryString["rurl"]);
            ctlManageRotator.BuyUrl = RotatorSettings.BuyLink;
        }

    }
}