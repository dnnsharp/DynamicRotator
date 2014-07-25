using DnnSharp.Common;
using DnnSharp.Common.Api;
using DnnSharp.Common.Dnn;
using DnnSharp.Common.Licensing.v1;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Xml.XPath;

namespace DnnSharp.DynamicRotator.Core.Services
{
    /// <summary>
    /// This file exists here because .ashx files do not support CodeFile attribute
    /// </summary>
    public class AdminApi : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // extract some common variables
            var portal = PortalControllerEx.GetCurrentPortal(context);

            // register the API and some dependency injection
            var api = new ApiContext();
            api.Container.RegisterProperty("portalAlias", () => portal.PortalAlias);
            api.Container.RegisterProperty("portalSettings", () => portal);
            api.Container.RegisterProperty("portalId", () => portal == null ? -1 : portal.PortalId);
            api.Container.RegisterProperty("moduleInfo", () => new ModuleController().GetModule(ConvertUtils.Cast<int>(context.Request.QueryString["mid"], -1)));
            api.Container.RegisterProperty("moduleId", () => ConvertUtils.Cast<int>(context.Request.QueryString["mid"], -1));

            api.Execute(this, context);
        }

        #endregion


        [WebMethod(DefaultResponseType = eResponseType.Json)]
        public bool Refresh()
        {
            //App.Instance.ClearCache();
            return true;
        }

        [WebMethod(DefaultResponseType = eResponseType.Json, RequiredEditPermissions = false)]
        public Dictionary<string, string> GetLocalization(string locale)
        {
            var resourceFolder = Path.Combine(App.Info.BasePath, "App_LocalResources");
            var resourceFile = Path.Combine(resourceFolder, "Form." + locale + ".resx");
            if (!File.Exists(resourceFile))
                resourceFile = Path.Combine(resourceFolder, "Form.resx");

            var doc = new XPathDocument(resourceFile);
            var resources = new Dictionary<string, string>();
            foreach (XPathNavigator nav in doc.CreateNavigator().Select("root/data")) {
                if (nav.NodeType != XPathNodeType.Comment) {
                    var selectSingleNode = nav.SelectSingleNode("value");
                    if (selectSingleNode != null) {
                        resources[nav.GetAttribute("name", String.Empty)] = selectSingleNode.Value;
                    }
                }
            }

            return resources;
        }



        [WebMethod(DefaultResponseType = eResponseType.Json, RequiredEditPermissions = true)]
        public LicensingSummary GetLicensing(ModuleInfo moduleInfo)
        {
            var summary = new LicensingSummary();
            summary.Licenses = App.RegCore.AllActivations.Values.Cast<LicenseInfo>()
                .Distinct().ToList();

            summary.IsTrial = App.IsTrial();
            summary.IsTrialExpired = App.IsTrialExpired();
            summary.IsActivated = App.IsActivated();

            var returnUrl = moduleInfo == null ? "/" : Globals.NavigateURL(moduleInfo.TabID);

            summary.UnlockTrialUrl = App.Info.BaseUrl + "/RegCore/UnlockTrial.aspx?t=" + HttpUtility.UrlEncode(typeof(App).AssemblyQualifiedName) + "&rurl=" + HttpUtility.UrlEncode(returnUrl + "#refresh");
            summary.ActivateUrl = App.Info.BaseUrl + "/RegCore/Activation.aspx?t=" + HttpUtility.UrlEncode(typeof(App).AssemblyQualifiedName) + "&rurl=" + HttpUtility.UrlEncode(returnUrl + "#refresh");

            return summary;
        }

    }
}
