using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DnnSharp.DynamicRotator.Core.Data;
using System.Configuration;
using System.Data;
using DnnSharp.DynamicRotator.Core.Settings;
using System.Drawing;
using System.Xml;
using DnnSharp.DynamicRotator.Core.Services;
using System.Text;
using System.Collections.Specialized;
using DnnSharp.DynamicRotator.Core;

namespace DnnSharp.DynamicRotator
{
    public partial class ManageRotator : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            ctlManageRotator.Configuration = new DnnConfiguration();
            RotatorSettings.Init(ctlManageRotator.Configuration);
            ctlManageRotator.ReturnUrl = Server.UrlDecode(Request.QueryString["rurl"]);
            ctlManageRotator.BuyUrl = App.Info.BuyUrl;
            ctlManageRotator.ControllerType = typeof(DnnSharp.DynamicRotator.Core.DynamicRotatorController);
        }

    }
}