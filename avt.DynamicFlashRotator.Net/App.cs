using DnnSharp.Common;
using DnnSharp.Common.Licensing.v1;
using DnnSharp.Common.Logging;
using DotNetNuke.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DnnSharp.DynamicRotator.Core;
using DnnSharp.Common.Logging.Target;
using DotNetNuke.Entities.Portals;
using DnnSharp.DynamicRotator.Core.Settings;
using DnnSharp.DynamicRotator.Core.Properties;
using System.Globalization;

namespace DnnSharp.DynamicRotator.Core
{
    public class App
    {
        public static AppInfo Info { get; private set; }


        #region Environment

        /// <summary>
        /// The root file path where DNN is physically installed
        /// </summary>
        public static string RootPath { get { return Globals.ApplicationMapPath; } }

        /// <summary>
        /// The root file path where DNN is physically installed
        /// </summary>
        public static string RootUrl { get { return HttpRuntime.AppDomainAppVirtualPath.TrimEnd('/'); } }

        #endregion


        #region Singleton

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static App Instance { get; private set; }

        /// <summary>
        /// Singleton initialization
        /// </summary>
        static App()
        {
            Instance = new App();
        }

        #endregion


        #region Licensing and Registration
        public static DateTime BuildDate {
            get {
                return DateTime.Parse(Resources.date, new CultureInfo("en-us"));
            }
        }
        public static bool IsAdmin { get { return DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo().IsInRole(DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().AdministratorRoleName); } }
        public static string RegCoreServer { get { return "http://www.dnnsharp.com/DesktopModules/RegCore/"; } }

        public static List<System.Web.UI.WebControls.ListItem> Hosts
        {
            get
            {
                List<System.Web.UI.WebControls.ListItem> hosts = new List<System.Web.UI.WebControls.ListItem>();
                PortalAliasController paCtrl = new PortalAliasController();
                foreach (PortalAliasInfo paInfo in PortalAliasController.GetPortalAliasLookup().Values) {
                    hosts.Add(new System.Web.UI.WebControls.ListItem(paInfo.HTTPAlias, paInfo.HTTPAlias));
                }
                return hosts;
            }
        }

        internal IActivationDataStore GetActivationSrc()
        {
            return new DsLicFile();
        }

        public static IRegCoreClient RegCore
        {
            get
            {
                return RegCoreClient.Get(new RegCoreServer(RegCoreServer).ApiScript, App.Info.Code, new DsLicFile(), false);
            }
        }

        public static LicensingSummary GetLicensing(string returnUrl)
        {
            var summary = new LicensingSummary();
            summary.Licenses = App.RegCore.AllActivations.Values.Cast<LicenseInfo>()
                .Distinct().ToList();

            summary.IsTrial = App.IsTrial();
            summary.IsTrialExpired = App.IsTrialExpired();
            summary.IsActivated = App.IsActivated();

            summary.UnlockTrialUrl = App.Info.BaseUrl + "/RegCore/UnlockTrial.aspx?t=" + HttpUtility.UrlEncode(typeof(App).AssemblyQualifiedName) + "&rurl=" + HttpUtility.UrlEncode(returnUrl + "#refresh");
            summary.ActivateUrl = App.Info.BaseUrl + "/RegCore/Activation.aspx?t=" + HttpUtility.UrlEncode(typeof(App).AssemblyQualifiedName) + "&rurl=" + HttpUtility.UrlEncode(returnUrl + "#refresh");

            return summary;
        }

        public static bool IsActivated()
        {
            return true;
            //return RegCore.IsActivated(App.Info.Code, App.Info.Version, HttpContext.Current.Request.Url.Host);
        }

        public static bool IsTrial()
        {
            return false;
            // RegCore.IsTrial(App.Info.Code, App.Info.Version, HttpContext.Current.Request.Url.Host);
        }

        public static int TrialDaysLeft
        {
            get
            {
                ILicenseInfo act = RegCore.GetValidActivation(App.Info.Code, App.Info.Version, HttpContext.Current.Request.Url.Host);
                if (act != null)
                    return act.RegCode.DaysLeft;

                return -1;
            }
        }

        public static string CurrentRegistrationCode
        {
            get
            {
                ILicenseInfo act = RegCore.GetValidActivation(App.Info.Code, App.Info.Version, HttpContext.Current.Request.Url.Host);
                if (act != null)
                    return act.RegistrationCode;

                return "";
            }
        }

        public static bool IsTrialExpired()
        {
            return RegCore.IsTrialExpired(App.Info.Code, App.Info.Version, HttpContext.Current.Request.Url.Host);
        }

        public static void Activate(string regCode, string host, string actCode)
        {
            if (string.IsNullOrEmpty(actCode)) {
                RegCore.Activate(regCode, App.Info.Code, App.Info.Version, host, App.Info.Key);
            } else {
                RegCore.Activate(regCode, App.Info.Code, App.Info.Version, host, App.Info.Key, actCode);
            }
        }

        #endregion


        #region Initialization

        public TypedLogger<RotatorSettings> Logger { get; set; }

        private App()
        {

            // app init
            Info = new AppInfo() {
                Name = "Dynamic Rotator",
                Code = "ADROT",
                Key = "<RSAKeyValue><Modulus>xjeQuuf4zC2gbVI0ZJJnKagUgmeFH8klB27NK80DhxcBaJkw/naUJl1N9195kxUyznRf8uwSkjt9sZfmGQplu3gYz+X3GFCcVhABZsXyO+vNAdkyU+F6KkX5wL4/AAfmpKbqhsYt/z3abPInaRWG1Mk6uoUSv0bkAXsvLWOjUZs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",

                Version = System.Reflection.Assembly.GetAssembly(typeof(App)).GetName().Version.ToString(2) + ".0",
                Build = System.Reflection.Assembly.GetAssembly(typeof(App)).GetName().Version.ToString(3),

                BasePath = RootPath + "\\DesktopModules\\DnnSharp\\DynamicRotator",
                BaseUrl = RootUrl + "/DesktopModules/DnnSharp/DynamicRotator",
                ProductUrl = "http://www.dnnsharp.com/dnn/modules/banner/dynamic-rotator",
                BuildDate = DateTime.Parse(Resources.date, new CultureInfo("en-us")),

#if DEBUG
                IsDebug = true,
#else
                IsDebug = false,
#endif
            };

            // IsActivated depends on AppInfo, that's why we put it down here
            Info.IsActivated = App.IsActivated();

            // app init
            //InitLogging();

            // init container
            Container = new LiteContainer();
            Container.RegisterProperty("ConnectionString", () => DotNetNuke.Common.Utilities.Config.GetConnectionString());
            //Container.RegisterProperty("License", () => LicenseFactory.Get(LicenseFilePath, Version, ProductKey));
            //Container.RegisterService<IFileStorage>(() => new LocalFileStorage());

            // also populate the static AppContainer
            LiteContainer.AppContainer.RegisterProperty("RootUrl", () => App.RootUrl);
            LiteContainer.AppContainer.RegisterProperty("RootPath", () => App.RootPath);

            //Logger = new TypedLogger<RotatorSettings>();
            //Logger.FnLevel = (RotatorSettings data, eLogLevel currentMinLevel) => {
            //    return data == null || data.IsDebug.Value ? eLogLevel.Debug : eLogLevel.Info;
            //};

            //Logger.Targets.Add(new DnnTarget<RotatorSettings>());
            //Logger.Targets.Add(new SimpleFileTarget<RotatorSettings>(
            //    // fn to get file path
            //    (RotatorSettings data) => {
            //        var fileName = "log." + DateTime.Now.ToString("yyyy-MM-dd") + ".txt.resources";
            //        if (data == null)
            //            return string.Format("{0}\\Portals\\_default\\Logs\\ActionForm\\{1}", App.RootPath, fileName);

            //        var portalHome = new PortalSettings(data.PortalId).HomeDirectoryMapPath;
            //        return string.Format("{0}\\Logs\\ActionForm\\{1}", portalHome, fileName);
            //    },
            //    // fn to format message
            //    (eLogLevel level, ApiSettings data, string message) => {
            //        return string.Format("{0} | {1} | {2} | {3}",
            //            DateTime.Now.ToString("HH:MM:ss"),
            //            level.ToString(),
            //            data == null ? "" : string.Format("{0} #{1}", data.Module.ModuleTitle, data.ModuleId),
            //            message);
            //    }));
        }

        #endregion


        const string MasterCacheKey = "DnnSharp.DynamicRotator";
        public static string GetMasterCacheKey()
        {
            if (HttpRuntime.Cache[MasterCacheKey] == null)
                HttpRuntime.Cache.Insert(MasterCacheKey, new object());

            return MasterCacheKey;
        }

        public LiteContainer Container { get; private set; }

    }
}
