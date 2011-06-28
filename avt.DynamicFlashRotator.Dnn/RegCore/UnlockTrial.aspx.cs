﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using avt.DynamicFlashRotator.Net.RegCore;
using avt.DynamicFlashRotator.Net.RegCore.Storage;

namespace avt.DynamicFlashRotator.Net.RegCore.WebClient
{
    public partial class UnlockTrial : System.Web.UI.Page
    {
        IRegCoreClient _RegCoreClient;
        protected RegCoreApp _RegCoreApp;
        protected RegCoreServer _RegCoreServer;

        protected void Page_Load(object sender, EventArgs e)
        {
            // try to load data
            string returnUrl = Request.QueryString["rurl"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = ResolveUrl("~/");

            try {
                string strDataType = Request.QueryString["t"];
                Type dataType = Type.GetType(strDataType);

                if (!(bool)dataType.GetProperty("IsAdmin", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null)) {
                    throw new Exception("Access denied!");
                }

                _RegCoreClient = (IRegCoreClient)dataType.GetProperty("RegCore", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);

                _RegCoreServer = new RegCoreServer((string)dataType.GetProperty("RegCoreServer", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null));
                _RegCoreApp = new RegCoreApp();
                _RegCoreApp.FromController(dataType);

            } catch {
                // TODO: log this?
                try {
                    Response.Redirect(Request.QueryString["rurl"]);
                } catch {
                    Response.Redirect(ResolveUrl("~/"));
                }
                return;
            }

            // check activation
            if (Request.QueryString["_avta"] == "unlock-trial" && Request.QueryString["p"] == _RegCoreApp.ProductCode) {
                UnlockTrial1();
            }

            if (!Page.IsPostBack) {
               
                // determine which case is this
                if (_RegCoreClient.IsActivated(_RegCoreApp.ProductCode, _RegCoreApp.Version, Request.Url.Host))
                    Response.Redirect(Request.QueryString["rurl"]);

                if (_RegCoreClient.IsTrialExpired(_RegCoreApp.ProductCode, _RegCoreApp.Version, Request.Url.Host)) {
                    EnableTrialExpired();
                } else {
                    EnableFirstTime();
                }
            }
        }

        void EnableFirstTime()
        {
            pnlUnlockTrial.Visible = true;
            pnlTrialExpired.Visible = false;
        }

        void EnableTrialExpired()
        {
            pnlUnlockTrial.Visible = false;
            pnlTrialExpired.Visible = true;
        }


        #region Unlock Trial

        protected void OnUnlockTrial(object sender, EventArgs e)
        {
            Response.Redirect(string.Format(
                "{0}?trial=true&email={1}&rurl={2}&p={3}&v={4}&va={5}",
                _RegCoreServer.UnlockTrialScript, 
                tbTrialEmail.Text,
                HttpUtility.UrlEncode(ToAbsoluteUrl(Request.RawUrl)), 
                _RegCoreApp.ProductCode, 
                _RegCoreApp.Version,
                _RegCoreApp.Version
            ));
        }

        void UnlockTrial1()
        {
            ILicenseActivation l = _RegCoreClient.Activate(Request.QueryString["r"], _RegCoreApp.ProductCode, _RegCoreApp.Version, Request.QueryString["email"], _RegCoreApp.ProductKey, Request.QueryString["act"]);
            if (l.IsValid(_RegCoreApp.ProductCode, _RegCoreApp.Version)) {
                Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["rurl"]));
            }
        }

        #endregion



        public static string ToAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (relativeUrl.IndexOf("http") == 0)
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            relativeUrl = "/" + relativeUrl.TrimStart('/');
            //if (relativeUrl.StartsWith("/"))
            //    relativeUrl = relativeUrl.Insert(0, "~");
            //if (!relativeUrl.StartsWith("~/"))
            //    relativeUrl = relativeUrl.Insert(0, "~/");

            Uri url = HttpContext.Current.Request.Url;
            string port = url.Port != 80 ? (":" + url.Port) : string.Empty;
            string query = relativeUrl.IndexOf("?") != -1 ? relativeUrl.Substring(relativeUrl.IndexOf("?")) : "";
            relativeUrl = relativeUrl.IndexOf("?") != -1 ? relativeUrl.Substring(0, relativeUrl.IndexOf("?")) : relativeUrl;

            return string.Format("{0}://{1}{2}{3}{4}", url.Scheme, url.Host, port, relativeUrl, query);
        }
    }
}