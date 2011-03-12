using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using avt.DynamicFlashRotator.Net.Services;
using DotNetNuke.Security;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using System.Reflection;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net.Services.Authentication;

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

        public bool HasAccess(string controlId, IList<IAdminAuthentication> authLayers)
        {
            return HasAccess(controlId);
        }

        public bool IsDebug()
        {
            return PortalController.GetCurrentPortalSettings().UserMode == DotNetNuke.Entities.Portals.PortalSettings.Mode.Edit;
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

        public string Tokenize(string controlId, string content)
        {
            int moduleId = 1;
            try {
                moduleId = Convert.ToInt32(controlId);
            } catch { return content; }

            ModuleController modCtrl = new ModuleController();
            ModuleInfo modInfo = modCtrl.GetModule(moduleId, -1, false);
            if (modInfo == null)
                return content;

            return Tokenize(content, modInfo, false, true);
        }

        public FileBrowser BrowseServerForResources {
            get {
                string portalFolder;
                try {
                    PortalController portalCtrl = new PortalController();
                    portalFolder = portalCtrl.GetPortal(Convert.ToInt32(HttpContext.Current.Request.QueryString["portalid"])).HomeDirectory;
                } catch { 
                    portalFolder = PortalController.GetCurrentPortalSettings().HomeDirectory;
                }

                return new FileBrowser(HttpContext.Current.Server.MapPath("~/" + portalFolder), "Portal Root", "png", "jpg", "swf");
            }
        }

        #endregion



        public static string Tokenize(string strContent, ModuleInfo modInfo, bool forceDebug, bool bRevertToDnn)
        {
            string cacheKey_Lock = "avt.MyTokens2.Lock";
            string cacheKey_Installed = "avt.MyTokens2.Installed";
            string cacheKey_MethodReplace = "avt.MyTokens2.MethodReplace";

            string bMyTokensInstalled = "no";
            MethodInfo methodReplace = null;

            // first, determine if MyTokens is installed
            if (HttpRuntime.Cache.Get(cacheKey_Installed) == null) {

                // get global lock 
                if (HttpRuntime.Cache.Get(cacheKey_Lock) == null) {
                    HttpRuntime.Cache.Insert(cacheKey_Lock, new object());
                }

                lock (HttpRuntime.Cache.Get(cacheKey_Lock)) {

                    // check again, maybe current thread was locked by another which did all the work
                    if (HttpRuntime.Cache.Get(cacheKey_Installed) == null) {

                        // it's not in cache, let's determine if it's installed
                        try {
                            Type myTokensRepl = DotNetNuke.Framework.Reflection.CreateType("avt.MyTokens.MyTokensReplacer", true);
                            if (myTokensRepl == null)
                                throw new Exception(); // handled in catch

                            bMyTokensInstalled = "yes";

                            // we now know MyTokens is installed, get ReplaceTokensAll methods
                            methodReplace = myTokensRepl.GetMethod(
                                "ReplaceTokensAll",
                                BindingFlags.Public | BindingFlags.Static,
                                null,
                                CallingConventions.Any,
                                new Type[] { 
                                    typeof(string), 
                                    typeof(DotNetNuke.Entities.Users.UserInfo), 
                                    typeof(bool),
                                    typeof(ModuleInfo)
                                },
                                null
                            );

                            if (methodReplace == null) {
                                // this shouldn't really happen, we know MyTokens is installed
                                throw new Exception();
                            }

                        } catch {
                            bMyTokensInstalled = "no";
                        }

                        // cache values so next time the funciton is called the reflection logic is skipped
                        HttpRuntime.Cache.Insert(cacheKey_Installed, bMyTokensInstalled);
                        if (bMyTokensInstalled == "yes") {
                            HttpRuntime.Cache.Insert(cacheKey_MethodReplace, methodReplace);
                        }
                    }
                }
            }

            bMyTokensInstalled = HttpRuntime.Cache.Get(cacheKey_Installed).ToString();
            if (bMyTokensInstalled == "yes") {
                if (strContent.IndexOf("[") == -1)
                    return strContent;
                methodReplace = (MethodInfo)HttpRuntime.Cache.Get(cacheKey_MethodReplace);
            } else {
                // if it's not installed return string or tokenize with DNN replacer
                if (!bRevertToDnn) {
                    return strContent;
                } else {
                    DotNetNuke.Services.Tokens.TokenReplace dnnTknRepl = new DotNetNuke.Services.Tokens.TokenReplace();
                    dnnTknRepl.AccessingUser = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
                    dnnTknRepl.DebugMessages = !DotNetNuke.Common.Globals.IsTabPreview();
                    if (modInfo != null)
                        dnnTknRepl.ModuleInfo = modInfo;

                    // MyTokens is not installed, execution ends here
                    return dnnTknRepl.ReplaceEnvironmentTokens(strContent);
                }
            }

            // we have MyTokens installed, proceed to token replacement
            return (string)methodReplace.Invoke(
                null,
                new object[] {
                    strContent,
                    DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo(),
                    forceDebug ? true : !DotNetNuke.Common.Globals.IsTabPreview(),
                    null
                }
            );
        }
    }
}
