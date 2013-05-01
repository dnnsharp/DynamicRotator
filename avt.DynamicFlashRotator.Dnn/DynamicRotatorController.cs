using System;
using System.Collections.Generic;
using System.Web;
using DotNetNuke.Entities.Modules;
using System.Text;
using System.Xml;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net;
using avt.DynamicFlashRotator.Net.Data;
using System.Web.UI.WebControls;
using avt.DynamicFlashRotator.Net.RegCore.Storage;
using avt.DynamicFlashRotator.Net.RegCore;
using DotNetNuke.Entities.Portals;
using System.Collections;
using DotNetNuke.Services.Search;
using DotNetNuke.Common.Utilities;

namespace avt.DynamicFlashRotator.Dnn
{
    public class DynamicRotatorController : IPortable, IUpgradeable, ISearchable
    {

        #region RegCore

        public static bool IsAdmin { get { return DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo().IsInRole(DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().AdministratorRoleName); } }
        public static string RegCoreServer { get { return RotatorSettings.RegCoreServer; } }
        public static string ProductName { get { return RotatorSettings.ProductName; } }
        public static string ProductCode { get { return RotatorSettings.ProductCode; } }
        public static string ProductKey { get { return RotatorSettings.ProductKey; } }
        public static string Version { get { return RotatorSettings.Version; } }
        public static string Build { get { return RotatorSettings.Build; } }

        static public string DocSrv = RegCoreServer + "/Api.aspx?cmd=doc&product=" + ProductCode + "&version=" + Version;
        static public string BuyLink = RegCoreServer + "/Api.aspx?cmd=buy&product=" + ProductCode + "&version=" + Version;

        public List<ListItem> Hosts {
            get {
                List<ListItem> hosts = new List<ListItem>();
                PortalAliasController paCtrl = new PortalAliasController();
                foreach (DictionaryEntry de in paCtrl.GetPortalAliases()) {
                    PortalAliasInfo paInfo = (PortalAliasInfo)de.Value;
                    hosts.Add(new ListItem(paInfo.HTTPAlias, paInfo.HTTPAlias));
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
                return RegCoreClient.Get(new RegCoreServer(RegCoreServer).ApiScript, ProductCode, new DsLicFile(), false);
            }
        }

        public bool IsActivated()
        {
            return RegCore.IsActivated(ProductCode, Version, HttpContext.Current.Request.Url.Host);
        }

        public bool IsTrial()
        {
            return RegCore.IsTrial(ProductCode, Version, HttpContext.Current.Request.Url.Host);
        }

        public bool IsTrialExpired()
        {
            return RegCore.IsTrialExpired(ProductCode, Version, HttpContext.Current.Request.Url.Host);
        }

        public void Activate(string regCode, string host, string actCode)
        {
            if (string.IsNullOrEmpty(actCode)) {
                RegCore.Activate(regCode, ProductCode, Version, host, ProductKey);
            } else {
                RegCore.Activate(regCode, ProductCode, Version, host, ProductKey, actCode);
            }
        }

        #endregion



        #region IPortable Members

        public string ExportModule(int ModuleID)
        {
            StringBuilder strXML = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter Writer = XmlWriter.Create(strXML, settings);
            
            RotatorSettings.Init(new DnnConfiguration());
            RotatorSettings Settings = new RotatorSettings();
            Settings.LoadFromDB(ModuleID.ToString());

            Settings.SaveToPortableXml(Writer, ModuleID.ToString());

            Writer.Close();

            return strXML.ToString();
        }

        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            RotatorSettings.Init(new DnnConfiguration());
            RotatorSettings.LoadFromPortableXml(DotNetNuke.Common.Globals.GetContent(Content, "RotatorSettings"), ModuleID.ToString());
        }

        #endregion

        public string UpgradeModule(string Version)
        {
            // update activation
            RegCore.Upgrade(ProductCode, Version, ProductKey, false);

            return "Done";
        }

        public SearchItemInfoCollection GetSearchItems(ModuleInfo ModInfo)
        {
            RotatorSettings.Init(new DnnConfiguration());
            RotatorSettings settings = new RotatorSettings();
            settings.LoadFromDB(ModInfo.ModuleID.ToString());

            var SearchItemCollection = new SearchItemInfoCollection();

            foreach (SlideInfo slide in settings.Slides)
                SearchItemCollection.Add(IndexSlide(ModInfo, slide));

            return SearchItemCollection;
        }

        SearchItemInfo IndexSlide(ModuleInfo ModInfo, SlideInfo slide)
        {
            var title = slide.Title;
            if (string.IsNullOrEmpty(title))
                title = "Unnamed slide";

            var sb = new StringBuilder();
            foreach (SlideObjectInfo obj in slide.SlideObjects) {
                if (!string.IsNullOrEmpty(obj.Text))
                    sb.AppendFormat("{0} ", obj.Text);
            }

            return new SearchItemInfo(
                title,
                sb.ToString().Substring(0, Math.Max(100, sb.Length)), // description
                -1, // objContent.LastModifiedByUserID,
                DateTime.Now, // objContent.LastModifiedOnDate,
                ModInfo.ModuleID,
                slide.Id.ToString(),
                title + " " + sb.ToString(),
                "",
                Null.NullInteger);
        }

    }
}

