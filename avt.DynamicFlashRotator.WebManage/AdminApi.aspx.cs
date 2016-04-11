using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Xml;
using System.Text;
using DnnSharp.DynamicRotator.Core.Settings;
using DnnSharp.DynamicRotator.Core.Services;

namespace avt.DynamicFlashRotator.Net.WebManage
{
    public partial class AdminApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            // TODO: fix this for DNN !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //// check settings
            //if (RotatorSettings.Configuration == null) {
            
            //    string connStr = "";
            //    string dbOwner = "";
            //    string objQualifier = "";
            //    string allowRole = "";
            //    string allowIp = "";
            //    string allowInvokeType = "";

            //    string controlId = Request.QueryString["controlId"];
            //    string sessionKey = "avt.DynamicRotator." + controlId;
            //    if (HttpContext.Current.Session[sessionKey] != null) {

            //        Dictionary<string, string> settings = Session[sessionKey] as Dictionary<string, string>;

            //        connStr = settings["DbConnectionString"];
            //        dbOwner = settings["DbOwner"];
            //        objQualifier = settings["DbObjectQualifier"];
            //        allowRole = settings["SecurityAllowAspRole"];
            //        allowIp = settings["SecurityAllowIps"];
            //        allowInvokeType = settings["SecurityAllowInvokeType"];

            //    }

            //    RotatorSettings.Init(new AspNetConfiguration(connStr, dbOwner, objQualifier, allowRole, allowIp, allowInvokeType));

            //    //if (!string.IsNullOrEmpty(Request.QueryString["connStr"])) {
            //    //    // TODO: RotatorSettings.Init(new AspNetConfiguration());
            //    //} else {
            //    //    // don't have it in the query string, let's go back to previous page
            //    //    Response.Write("{\"error\":\"Access Denied!\"}");
            //    //    return;
            //    //}
            //}

            if (!RotatorSettings.Configuration.HasAccess(Request.QueryString["controlId"])) {
                Response.Write("{\"error\":\"Access Denied!\"}");
                return;
            }

            if (string.IsNullOrEmpty(Request.QueryString["cmd"])) {
                Response.Write("{\"error\":\"Invalid Operation!\"}");
                return;
            }

            // We're good to go
            switch (Request.QueryString["cmd"]) {
                case "listfolders":
                    ListFoldersJson();
                    break;
                case "listfiles":
                    ListFilesJson();
                    break;
                case "checkupdate":
                    CheckUpdate();
                    break;
            }

        }

        void ListFoldersJson()
        {
            string relPath = Request.Params["relPath"];

            StringBuilder sbJson = new StringBuilder();
            sbJson.Append('[');
            foreach (FileBrowser_Folder folder in RotatorSettings.Configuration.BrowseServerForResources.ListFolders(relPath)) {
                sbJson.AppendFormat("{0},",folder.ToStringJson());
            }
            if (sbJson[sbJson.Length - 1] == ',') {
                sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append(']');

            Response.Write(sbJson.ToString());
        }

        void ListFilesJson()
        {
            string relPath = Request.Params["relPath"];

            StringBuilder sbJson = new StringBuilder();
            sbJson.Append('[');
            foreach (FileBrowser_File file in RotatorSettings.Configuration.BrowseServerForResources.ListFiles(relPath)) {
                sbJson.AppendFormat("{0},", file.ToStringJson());
            }
            if (sbJson[sbJson.Length - 1] == ',') {
                sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append(']');

            Response.Write(sbJson.ToString());
        }

        void CheckUpdate()
        {
            RotatorSettings settings = new RotatorSettings();
            if (settings.LoadFromDB(Request.Params["controlId"])) {
                Response.Write("{\"lastUpdate\":" + TotalMiliseconds(settings.LastUpdate) + "}");
            } else {
                Response.Write("{\"lastUpdate\":-1}");
            }
        }

        double TotalMiliseconds(DateTime date)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            TimeSpan ts = new TimeSpan(date.Ticks - d1.Ticks);

            return ts.TotalMilliseconds;
        }

        #region Helpers


        #endregion

    }
}