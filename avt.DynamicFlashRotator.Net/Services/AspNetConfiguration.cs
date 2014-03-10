using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net.Services.Authentication;
using System.Web.UI;
using System.Collections.Specialized;
using avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2;

namespace avt.DynamicFlashRotator.Net.Services
{
    public class AspNetConfiguration : IConfiguration
    {
        List<IAdminAuthentication> _Security = new List<IAdminAuthentication>();

        public AspNetConfiguration(string connStr, string dbOwner, string objQualifier, string allowRole, string allowIp, string allowInvokeType)
        {
            string controlId = HttpContext.Current.Request.QueryString["controlId"];

            if (ConfigurationManager.ConnectionStrings[connStr] == null) {
                throw new ArgumentException("Dynamic Rotator .NET could not find a connection string named "+ connStr +" in web.config!");
            }
                
            _ConnStr = ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            _DbOwner = dbOwner;
            _ObjQualifier = objQualifier;

            if (!string.IsNullOrEmpty(allowRole))
                _Security.Add(new AllowAspRole(allowRole, controlId));
            if (!string.IsNullOrEmpty(allowIp))
                _Security.Add(new AllowIps(allowIp, controlId));
            if (!string.IsNullOrEmpty(allowInvokeType))
                _Security.Add(new AllowInvokeType(allowInvokeType, controlId));
            
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
            return (HasAccess(controlId, _Security));
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

        public string LicenseFilePath
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public LicenseStatus LicenseStatus
        {
            get
            {
                return new LicenseStatus() {
                    Code = Dnn.DnnSf.Licensing.v2.LicenseStatus.eCode.Ok,
                    Type = Dnn.DnnSf.Licensing.v2.LicenseStatus.eType.Info
                };
                   
            }
        }


        #endregion

    }
}
