using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net.Services.Authentication;

namespace avt.DynamicFlashRotator.Net.Services
{
    public class AspNetConfiguration : IConfiguration
    {
        public AspNetConfiguration()
        {
            if (HttpContext.Current != null) {
                string sessionKey = "avt.DynamicRotator." + HttpContext.Current.Request.QueryString["controlId"];
                if (HttpContext.Current.Session[sessionKey] != null) {

                    Dictionary<string, string> settings = HttpContext.Current.Session[sessionKey] as Dictionary<string, string>;

                    _ConnStr = ConfigurationManager.ConnectionStrings[settings["DbConnectionString"]].ConnectionString;
                    if (settings.ContainsKey("DbOwner")) {
                        _DbOwner = settings["DbOwner"];
                    }
                    if (settings.ContainsKey("DbObjectQualifier")) {
                        _ObjQualifier = settings["DbObjectQualifier"];
                    }
                }
            }

            if (!string.IsNullOrEmpty(_ObjQualifier) && _ObjQualifier.EndsWith("_") == false) {
                _ObjQualifier += "_";
            }

            if (!string.IsNullOrEmpty(_DbOwner) && _DbOwner.EndsWith(".") == false) {
                _DbOwner += ".";
            }
        }

        public AspNetConfiguration(string connStr, string dbOwner, string objQualifier)
        {
            _ConnStr = ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            _DbOwner = dbOwner;
            _ObjQualifier = objQualifier;
            
            if (!string.IsNullOrEmpty(_DbOwner) && _DbOwner.IndexOf('.') != _DbOwner.Length - 1) {
                _DbOwner += ".";
            }
        }

        #region IConfiguration Members

        string _ConnStr;
        public string ConnStr { get { return _ConnStr; } }

        string _DbOwner = "[dbo].";
        public string DbOwner { get { return _DbOwner; } }

        string _ObjQualifier = "";
        public string ObjQualifier { get { return _ObjQualifier; } }

        public bool ShowManageLinks()
        {
            return true;
        }

        public bool HasAccess(string controlId)
        {
            if (HttpContext.Current.Request.Url.Host == "localhost")
                return true;

            // check authentication providers


            return false;
        }

        public bool HasAccess(string controlId, IList<IAdminAuthentication> authLayers)
        {
            foreach (IAdminAuthentication auth in authLayers) {
                if (!auth.HasAccess()) {
                    return false;
                }
            }
            return true;
        }

        public bool IsDebug()
        {
            return true;
        }


        public string FormatTitle(string controlId)
        {
            return controlId;
        }

        public string Tokenize(string controlId, string content)
        {
            return content;
        }

        public FileBrowser BrowseServerForResources { 
            get {
                string resPath = "~/";
                string pathName = "Website Root";
                string sessionKey = "avt.DynamicRotator." + HttpContext.Current.Request.QueryString["controlId"];

                if (HttpContext.Current.Session[sessionKey] != null) {

                    Dictionary<string, string> settings = HttpContext.Current.Session[sessionKey] as Dictionary<string, string>;

                    if (settings.ContainsKey("ResourceUrl")) {
                        resPath = settings["ResourceUrl"];
                        pathName = "Resources Folder";
                    }
                }
                return new FileBrowser(HttpContext.Current.Server.MapPath(resPath), pathName, "png", "jpg", "swf"); 
            }
        }

        #endregion

    }
}
