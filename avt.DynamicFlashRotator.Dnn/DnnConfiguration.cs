using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using avt.DynamicFlashRotator.Net.Services;
using DotNetNuke.Security;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;

namespace avt.DynamicFlashRotator.Dnn
{
    public class DnnConfiguration : IConfiguration
    {
        private DotNetNuke.Framework.Providers.ProviderConfiguration _providerConfiguration = DotNetNuke.Framework.Providers.ProviderConfiguration.GetProviderConfiguration("data");

        public DnnConfiguration()
        {
            // Read the configuration specific information for this provider
            DotNetNuke.Framework.Providers.Provider objProvider = (DotNetNuke.Framework.Providers.Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider];

            // Read the attributes for this provider
            //Get Connection string from web.config
            _ConnStr = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            if (_ConnStr == "") {
                // Use connection string specified in provider
                _ConnStr = objProvider.Attributes["connectionString"];
            }

            //_providerPath = objProvider.Attributes["providerPath"];

            _ObjQualifier = objProvider.Attributes["objectQualifier"];
            if (_ObjQualifier != "" && _ObjQualifier.EndsWith("_") == false) {
                _ObjQualifier += "_";
            }

            _DbOwner = objProvider.Attributes["databaseOwner"];
            if (_DbOwner != "" && _DbOwner.EndsWith(".") == false) {
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
            return false;
        }

        public bool HasAccess(string controlId)
        {
            int moduleId = 1;
            try {
                moduleId = Convert.ToInt32(controlId);
            } catch { return false; }

            ModuleController modCtrl = new ModuleController();
            ModuleInfo modInfo = modCtrl.GetModule(moduleId, -1, false);
            if (modInfo == null)
                return false;

            return PortalSecurity.HasNecessaryPermission(SecurityAccessLevel.Edit, PortalController.GetCurrentPortalSettings(), modInfo);
        }

        public string FormatTitle(string controlId)
        {
            int moduleId = 1;
            try {
                moduleId = Convert.ToInt32(controlId);
            } catch { return controlId; }

            ModuleController modCtrl = new ModuleController();
            ModuleInfo modInfo = modCtrl.GetModule(moduleId, -1, false);
            if (modInfo == null)
                return controlId;

            return string.Format("{0} (Module ID: {1})", modInfo.ModuleTitle, modInfo.ModuleID);
        }

        #endregion
    }
}
