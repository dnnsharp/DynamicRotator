using DnnSharp.DynamicRotator.Core;
using DnnSharp.Common.Licensing.v1;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DnnSharp.DynamicRotator.RegCore.WebClient
{
    public partial class QuickStatusAndLink : System.Web.UI.UserControl, IRegCoreComponent
    {
        IRegCoreClient _RegCoreClient;
        protected RegCoreApp _RegCoreApp;
        protected RegCoreServer _RegCoreServer;

        protected bool _isAdmin;

        protected string _unlockTrialUrl;
        protected string _activateUrl;


        public void InitRegCore(bool isAdmin, string regCoreServer, string productName, string productCode, string productKey, string version, string regCoreManageUrl, Type controller)
        {
            _isAdmin = isAdmin;

            _RegCoreServer = new RegCoreServer(regCoreServer);
            _RegCoreApp = new RegCoreApp(App.Info);

            _unlockTrialUrl = regCoreManageUrl.TrimEnd('/') + "/UnlockTrial.aspx?t=" + HttpUtility.UrlEncode(controller.AssemblyQualifiedName) + "&rurl=" + HttpUtility.UrlEncode(Request.RawUrl);
            _activateUrl = regCoreManageUrl.TrimEnd('/') + "/Activation.aspx?t=" + HttpUtility.UrlEncode(controller.AssemblyQualifiedName) + "&rurl=" + HttpUtility.UrlEncode(Request.RawUrl);

            _RegCoreClient = (IRegCoreClient)controller.GetProperty("RegCore", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {

                if (_RegCoreClient.IsActivated(_RegCoreApp.Info.Code, _RegCoreApp.Info.Version, Request.Url.Host) && !_RegCoreClient.IsTrial(_RegCoreApp.Info.Code, _RegCoreApp.Info.Version, Request.Url.Host))
                    return;

                // determine which case is this
                ILicenseInfo act = _RegCoreClient.GetValidActivation(_RegCoreApp.Info.Code, _RegCoreApp.Info.Version, Request.Url.Host);

                if (_isAdmin) {
                    if (act == null) {
                        SetStatus("This copy of {0} is not licensed. <a href='{1}' style='text-decoration: underline;'>Unlock 30 Days Trial</a> or <a href='{2}' style='text-decoration: underline;'>Activate for Production</a>...", _RegCoreApp.Info.Name, _unlockTrialUrl, _activateUrl);
                    } else if (act.RegCode.IsTrial) {
                        if (_RegCoreClient.IsTrialExpired(_RegCoreApp.Info.Code, _RegCoreApp.Info.Version, Request.Url.Host)) {
                            SetStatus("{0} trial expired! <a href='{1}' style='text-decoration: underline;'>Purchase License</a> or <a href='{2}' style='text-decoration: underline;'>Activate for Production</a>...", _RegCoreApp.Info.Name, _RegCoreServer.GetBuyUrl(_RegCoreApp), _activateUrl);
                        } else {
                            int trialDays = act.RegCode.DaysLeft;
                            if (trialDays <= 7) {
                                SetStatus("{0} days left from {1} trial! <a href='{2}' style='text-decoration: underline;'>Purchase License</a> or <a href='{3}' style='text-decoration: underline;'>Activate for Production</a>...", trialDays, _RegCoreApp.Info.Name, _RegCoreServer.GetBuyUrl(_RegCoreApp), _activateUrl);
                            }
                        }
                    }
                } else { // public
                    if (act == null) {
                        SetStatus("Login with admin (in Edit Mode) to unlock trial or license this copy of {0}.", _RegCoreApp.Info.Name);
                    } else if (act.RegCode.IsTrial) {
                        if (_RegCoreClient.IsTrialExpired(_RegCoreApp.Info.Code, _RegCoreApp.Info.Version, Request.Url.Host)) {
                            SetStatus("{0} trial explied!", _RegCoreApp.Info.Name);
                        }
                    }
                }
            }
        }

        void SetStatus(string status, params object[] args)
        {
            pnlStatus.InnerHtml = string.Format(status, args);
            pnlStatus.Visible = true;
        }

    }
}