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

            // check settings
            if (RotatorSettings.Configuration == null) {
                if (!string.IsNullOrEmpty(Request.QueryString["connStr"])) {
                    RotatorSettings.Init(new AspNetConfiguration());
                } else {
                    // don't have it in the query string, let's go back to previous page
                    Response.Write("{\"error\":\"Access Denied!\"}");
                    return;
                }
            }

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

        #region Helpers


        #endregion

    }
}