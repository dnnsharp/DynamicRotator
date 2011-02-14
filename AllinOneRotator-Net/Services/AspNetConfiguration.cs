using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;

namespace avt.AllinOneRotator.Net.Services
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

            if (!string.IsNullOrEmpty(_DbOwner) && _DbOwner.IndexOf('.') != _DbOwner.Length - 1) {
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

        #endregion
    }
}
