using System;
using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using avt.DynamicFlashRotator.Net.RegCore.Storage;
using System.Collections.Generic;

using avt.DynamicFlashRotator.Net.RegCore;

namespace avt.DynamicFlashRotator.Net.WebManage.RegCore.WebClient
{
    public partial class ActivationWnd : System.Web.UI.Page // PageBase
    {
        IRegCoreClient _RegCoreClient;
        protected RegCoreApp _RegCoreApp;
        protected RegCoreServer _RegCoreServer;

        protected List<ListItem> _hosts;

        protected void Page_Init(Object Sender, EventArgs args)
        {
            try {
                string strDataType = Request.QueryString["t"];
                Type dataType = Type.GetType(strDataType);

                if (!(bool)dataType.GetProperty("IsAdmin", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null)) {
                    throw new Exception("Access denied!");
                }

                _RegCoreClient = (IRegCoreClient)dataType.GetProperty("RegCore", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null);

                _RegCoreServer = new RegCoreServer((string)dataType.GetProperty("RegCoreServer", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null, null));
                _RegCoreApp = new RegCoreApp();
                _RegCoreApp.FromController(dataType);

                _hosts = (List<ListItem>)dataType.GetProperty("Hosts").GetValue(Activator.CreateInstance(dataType), null);

            } catch {
                // TODO: log this?
                try {
                    Response.Redirect(Request.QueryString["rurl"]);
                } catch {
                    Response.Redirect(ResolveUrl("~/"));
                }
                return;
            }

            //PortalSettings portalSettings = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings();

            //// check that user has rights
            //if (!PortalSecurity.IsInRole(portalSettings.AdministratorRoleName)) {
            //    Response.Write("Access denied!");
            //    Response.End();
            //}
        }

        protected void Page_Load(Object Sender, EventArgs args)
        {
            if (!Page.IsPostBack) {
                pnlSuccess.Visible = false;
                lnkPurchase.HRef = _RegCoreServer.GetBuyUrl(_RegCoreApp);
            }
        }

        protected void OnNext(Object Sender, EventArgs args)
        {
            txtRegistrationCode.Text = txtRegistrationCode.Text.Trim();

            RegCode regCode;
            try {
                regCode = new RegCode(txtRegistrationCode.Text);
                //if (!regCode.IsValid(RedirectController.Version)) {
                //    throw new Exception();
                //}
            } catch {
                validateActivation.Text = "The registration code you supplied is invalid.";
                validateActivation.IsValid = false;
                return;
            }

            pnlHosts.Visible = true;
            pnlActivateManuallyBtn.Visible = true;

            switch (regCode.VariantCode) {
                case "DOM":
                    FillDomains();
                    break;
                case "3DOM":
                    FillDomains();
                    break;
                case "10DOM":
                    FillDomains();
                    break;
                case "XDOM":
                    FillDomains();
                    break;
                case "SRV":
                    FillDomainsForServers();
                    break;
                case "30DAY":
                    FillAll();
                    break;
                default:
                    FillAll();
                    break;
            }

            btnNext.Visible = false;
        }

        protected void OnActivateManually(Object Sender, EventArgs args)
        {
            pnlActivateForm.Visible = false;
            pnlActivateManually.Visible = true;
            pnlActivateManuallyBtn.Visible = false;

            string host = ddHosts.SelectedValue;
            if (!string.IsNullOrEmpty(tbHost.Text.Trim())) {
                host = tbHost.Text.Trim();
            }

            btnActivationTool.HRef = "http://www.avatar-soft.ro/MyAccount/ManualActivation/tabid/180/Default.aspx?regkey=" + txtRegistrationCode.Text + "&host=" + host;
        }

        protected void OnActivate(object Sender, EventArgs args)
        {
            txtRegistrationCode.Text = txtRegistrationCode.Text.Trim();

            string host = ddHosts.SelectedValue;
            if (!string.IsNullOrEmpty(tbHost.Text.Trim())) {
                host = tbHost.Text.Trim();
            }

            try {
                _RegCoreClient.Activate(txtRegistrationCode.Text, _RegCoreApp.ProductCode, _RegCoreApp.Version, host, _RegCoreApp.ProductKey);
            } catch (Exception ex) {
                // error
                validateActivation.IsValid = false;
                if (ex.InnerException != null && ex.InnerException.GetType() == typeof(UnauthorizedAccessException)) {
                    validateActivation.Text = ex.InnerException.Message;
                    validateActivation.Text += "<br/>Put text below in the license file: <br/><textarea readonly='readonly' style = 'position: absolute; width: 520px; height: 140px;' wrap='off'>" + ex.Message + "</textarea>";
                } else {
                    validateActivation.Text = ex.Message;
                }
                return;
            }

            // activation succesfull
            pnlSuccess.Visible = true;
            pnlActivateForm.Visible = false;
            pnlActivateManually.Visible = false;
            btnClose.Text = "Close";
            lnkPurchase.Visible = false;

        }

        protected void OnManualActivate(object Sender, EventArgs args)
        {
            txtRegistrationCode.Text = txtRegistrationCode.Text.Trim();

            string host = ddHosts.SelectedValue;
            if (!string.IsNullOrEmpty(tbHost.Text.Trim())) {
                host = tbHost.Text.Trim();
            }

            try {
                _RegCoreClient.Activate(txtRegistrationCode.Text, _RegCoreApp.ProductCode, _RegCoreApp.Version, host, _RegCoreApp.ProductKey, tbActCode.Text.Trim());
            } catch (Exception ex) {
                // error
                validateActivation.Text = "Invalid Activation for host " + tbHost.Text;//ex.Message;
                validateActivation.IsValid = false;
                return;
            }

            // activation succesfull
            pnlSuccess.Visible = true;
            pnlActivateForm.Visible = false;
            pnlActivateManually.Visible = false;
            btnClose.Text = "Close";
            lnkPurchase.Visible = false;
        }



        #region Helpers

        private void FillDomains()
        {
            ddHosts.Items.Clear();

            foreach (ListItem li in _hosts) {
                string httpAlias = li.Value;
                bool isIP = (Regex.Match(httpAlias, ".*\\d+\\.\\d+\\.\\d+\\.\\d+.*").Length > 0);
                if (isIP) {
                    continue; // this is IP based alias
                }

                // remove port, if exists
                if (httpAlias.LastIndexOf(":") != -1) httpAlias = httpAlias.Substring(0, httpAlias.IndexOf(":"));

                // remove path
                if (httpAlias.LastIndexOf("/") != -1) httpAlias = httpAlias.Substring(0, httpAlias.IndexOf("/"));

                // remove www.
                httpAlias = StripSubdomains(httpAlias);

                if (httpAlias.IndexOf("localhost") == 0) {
                    continue;
                }

                if (ddHosts.Items.FindByText(httpAlias) != null) {
                    continue; // item already exists
                }

                ddHosts.Items.Add(new ListItem(httpAlias, httpAlias));
            }

            if (Request.QueryString["aurl"] != null)
                ddHosts.Items.Add(new ListItem(Request.QueryString["aurl"], Request.QueryString["aurl"]));
        }

        private void FillDomainsForServers()
        {
            ddHosts.Items.Clear();
            foreach (ListItem li in _hosts) {
                FillIp(li.Value);
            }
            
            if (Request.QueryString["aurl"] != null)
                ddHosts.Items.Add(new ListItem(Request.QueryString["aurl"], Request.QueryString["aurl"]));

        }

        private void FillIp(string httpAlias)
        {
            bool isIP = (Regex.Match(httpAlias, ".*\\d+\\.\\d+\\.\\d+\\.\\d+.*").Length > 0);
            if (!isIP) {
                // translate it to IP
                httpAlias = Regex.Match(httpAlias, ".*\\d+\\.\\d+\\.\\d+\\.\\d+.*").Value;
                try {
                    foreach (IPAddress addr in System.Net.Dns.GetHostEntry(httpAlias).AddressList) {
                        try {
                            //if (addr.ToString().IndexOf(":") > 0)
                            //    continue; // IP6
                            if (ddHosts.Items.FindByValue(addr.ToString()) == null)
                                ddHosts.Items.Add(new ListItem(addr.ToString(), addr.ToString()));
                            //FillIp(addr.ToString());
                        } catch { continue; }
                    }
                } catch { }
                return;
            }

            // remove port, if exists
            if (httpAlias.LastIndexOf(":") != -1) httpAlias = httpAlias.Substring(0, httpAlias.IndexOf(":"));

            // remove path
            if (httpAlias.LastIndexOf("/") != -1) httpAlias = httpAlias.Substring(0, httpAlias.IndexOf("/"));

            // remove www.
            httpAlias = StripSubdomains(httpAlias);

            if (httpAlias == "localhost" || httpAlias.IndexOf("127.0.0.1") == 0) {
                return;
            }

            if (ddHosts.Items.FindByText(httpAlias) != null) {
                return; // item already exists
            }

            ddHosts.Items.Add(new ListItem(httpAlias, httpAlias));
        }

        private void FillAll()
        {
            ddHosts.Items.Clear();

            foreach (ListItem li in _hosts) {
                string httpAlias = li.Value;

                // remove port, if exists
                if (httpAlias.LastIndexOf(":") != -1) httpAlias = httpAlias.Substring(0, httpAlias.IndexOf(":"));

                // remove path
                if (httpAlias.LastIndexOf("/") != -1) httpAlias = httpAlias.Substring(0, httpAlias.IndexOf("/"));

                // remove www.
                httpAlias = StripSubdomains(httpAlias);

                if (httpAlias.IndexOf("localhost") == 0 || httpAlias.IndexOf("127.0.0.1") == 0) {
                    continue;
                }

                if (ddHosts.Items.FindByText(httpAlias) != null) {
                    continue; // item already exists
                }

                ddHosts.Items.Add(new ListItem(httpAlias, httpAlias));
            }

            if (Request.QueryString["aurl"] != null)
                ddHosts.Items.Add(new ListItem(Request.QueryString["aurl"], Request.QueryString["aurl"]));
        }

        string StripSubdomains(string httpAlias)
        {
            string[] allowedSubdomains = new string[] { "www.", "www1.", "www2.", "dev.", "test.", "staging." };
            foreach (string subDom in allowedSubdomains) {
                if (httpAlias.IndexOf(subDom) == 0 || httpAlias.IndexOf("http://" + subDom) == 0 || httpAlias.IndexOf("https://" + subDom) == 0)
                    return httpAlias.Substring(httpAlias.IndexOf(subDom) + subDom.Length);
            }
            return httpAlias;
        }


        protected void OnCloseSA(object sender, EventArgs e)
        {
            if (Request.QueryString["rurl"] != null)
                Response.Redirect(Server.UrlDecode(Request.QueryString["rurl"]));
            else
                Response.Redirect("~/");
        }



        #endregion

    }
}