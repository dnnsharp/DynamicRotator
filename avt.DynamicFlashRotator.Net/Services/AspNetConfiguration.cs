using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;

namespace avt.DynamicFlashRotator.Net.Services
{
    public class AspNetConfiguration : IConfiguration
    {
        public AspNetConfiguration()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["connStr"])) {
                _ConnStr = ConfigurationManager.ConnectionStrings[HttpContext.Current.Request.QueryString["connStr"]].ConnectionString;
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["dbOwner"])) {
                    _DbOwner = HttpContext.Current.Request.QueryString["dbOwner"];
                }
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["objQualifier"])) {
                    _ObjQualifier = HttpContext.Current.Request.QueryString["objQualifier"];
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
            // TODO: implement somethign
            if (HttpContext.Current.Request.Url.Host == "localhost" || HttpContext.Current.Request.Url.Host == "193.254.62.222" || HttpContext.Current.Request.Url.Host == "192.168.0.199")
                return true;

            return false;
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
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["resPath"])) {
                    resPath = HttpContext.Current.Request.QueryString["resPath"];
                    pathName = "Resources Folder";
                }
                return new FileBrowser(HttpContext.Current.Server.MapPath(resPath), pathName, "png", "jpg", "swf"); 
            }
        }

        #endregion

    }
}
