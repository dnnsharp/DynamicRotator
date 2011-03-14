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

namespace avt.DynamicFlashRotator.Net.WebManage
{
    public partial class ManageRotator : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = "";
            string dbOwner = "";
            string objQualifier = "";
            string allowRole = "";
            string allowIp = "";
            string allowInvokeType = "";

            string controlId = Request.QueryString["controlId"];
            string sessionKey = "avt.DynamicRotator." + controlId;
            if (HttpContext.Current.Session[sessionKey] != null) {

                Dictionary<string, string> settings = Session[sessionKey] as Dictionary<string, string>;

                connStr = settings["DbConnectionString"];
                dbOwner = settings["DbOwner"];
                objQualifier = settings["DbObjectQualifier"];
                allowRole = settings["SecurityAllowAspRole"];
                allowIp = settings["SecurityAllowIps"];
                allowInvokeType = settings["SecurityAllowInvokeType"];

            }

            if (string.IsNullOrEmpty(connStr)) {
                Response.Redirect(Server.UrlDecode(Request.QueryString["rurl"]));
                return;
            }

            ctlManageRotator.Configuration = new AspNetConfiguration(connStr, dbOwner, objQualifier, allowRole, allowIp, allowInvokeType);
            ctlManageRotator.ReturnUrl = Server.UrlDecode(Request.QueryString["rurl"]);
        }

    }
}