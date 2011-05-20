using System;
using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Framework;
using DotNetNuke.Security;
using avt.DynamicFlashRotator.Net;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net.RegCore;


namespace avt.DynamicFlashRotator.Dnn
{
    public partial class ActivationWnd : System.Web.UI.Page // PageBase
    {

        protected void Page_Init(Object Sender, EventArgs args)
        {
            PortalSettings portalSettings = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings();

            // check that user has rights
            if (!PortalSecurity.IsInRole(portalSettings.AdministratorRoleName)) {
                Response.Write("Access denied!");
                Response.End();
            }
        }

        protected void Page_Load(Object Sender, EventArgs args)
        {
            if (!Page.IsPostBack) {
                pnlSuccess.Visible = false;
                lnkPurchase.HRef =  RotatorSettings.BuyLink;
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

            //trDomains.Visible = true;
            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "init1", "alert('" + regCode.ProductCode + "');", true);

            switch (regCode.VariantCode) {
                case "DOM":
                    FillHosts();
                    break;
                case "3DOM":
                    FillHosts();
                    break;
                case "10DOM":
                    FillHosts();
                    break;
                case "XDOM":
                    FillHosts();
                    break;
                case "SRV":
                    FillDomains();
                    break;
                case "30DAY":
                    FillAll();
                    break;
                default:
                    FillAll();
                    break;
            }

            //btnActivate.Visible = true;
            btnNext.Visible = false;
            ////txtRegistrationCode.ReadOnly = true;
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

            //RotatorSettings rotator = new RotatorSettings();
            //rotator.LoadFromDB(Request.QueryString["controlId"]);

            string host = ddHosts.SelectedValue;
            if (!string.IsNullOrEmpty(tbHost.Text.Trim())) {
                host = tbHost.Text.Trim();
            }

            try {
                RotatorSettings.Activate(txtRegistrationCode.Text, host, null);
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

            //RotatorSettings rotator = new RotatorSettings();
            //rotator.LoadFromDB(Request.QueryString["controlId"]);

            string host = ddHosts.SelectedValue;
            if (!string.IsNullOrEmpty(tbHost.Text.Trim())) {
                host = tbHost.Text.Trim();
            }

            try {
                RotatorSettings.Activate(txtRegistrationCode.Text, host, tbActCode.Text.Trim());
            } catch (Exception ex) {
                // error
                validateActivation.Text = "Invalid Activation for host " + host + "(internal error " + ex.Message + ")";
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

        private void FillHosts()
        {
            ddHosts.Items.Clear();

            PortalAliasController paCtrl = new PortalAliasController();
            foreach (DictionaryEntry de in paCtrl.GetPortalAliases()) {
                PortalAliasInfo paInfo = (PortalAliasInfo)de.Value;
                string httpAlias = paInfo.HTTPAlias;
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
                //string[] allowedSubdomains = new string[] { "www.", "www1.", "www2.", "dev.", "test.", "staging." };
                //foreach (string subDom in allowedSubdomains) {
                //    if (httpAlias.IndexOf(subDom) == 0 || httpAlias.IndexOf("http://" + subDom) == 0 || httpAlias.IndexOf("https://" + subDom) == 0)
                //        httpAlias = httpAlias.Substring(httpAlias.IndexOf(subDom) + subDom.Length);
                //}
                //if (httpAlias.IndexOf("www.") == 0 || httpAlias.IndexOf("http://www.") == 0 || httpAlias.IndexOf("https://www.") == 0) httpAlias = httpAlias.Substring(httpAlias.IndexOf("www.") + 4);
                //if (httpAlias.IndexOf("dev.") == 0 || httpAlias.IndexOf("http://dev.") == 0 || httpAlias.IndexOf("https://dev.") == 0) httpAlias = httpAlias.Substring(httpAlias.IndexOf("dev.") + 4);
                //if (httpAlias.IndexOf("test.") == 0 || httpAlias.IndexOf("http://test.") == 0 || httpAlias.IndexOf("https://test.") == 0) httpAlias = httpAlias.Substring(httpAlias.IndexOf("test.") + 5);
                //if (httpAlias.IndexOf("staging.") == 0 || httpAlias.IndexOf("http://staging.") == 0 || httpAlias.IndexOf("https://staging.") == 0) httpAlias = httpAlias.Substring(httpAlias.IndexOf("staging.") + 8);


                //if (httpAlias.IndexOf("www.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("www.") + 4);
                //if (httpAlias.IndexOf("dev.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("dev.") + 4);
                //if (httpAlias.IndexOf("staging.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("staging.") + 8);

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

        private void FillDomains()
        {
            ddHosts.Items.Clear();

            PortalAliasController paCtrl = new PortalAliasController();
            foreach (DictionaryEntry de in paCtrl.GetPortalAliases()) {
                PortalAliasInfo paInfo = (PortalAliasInfo)de.Value;
                FillIp(paInfo.HTTPAlias);
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
            //string[] allowedSubdomains = new string[] { "www.", "www1.", "www2.", "dev.", "test.", "staging." };
            //foreach (string subDom in allowedSubdomains) {
            //    if (httpAlias.IndexOf(subDom) == 0 || httpAlias.IndexOf("http://" + subDom) == 0 || httpAlias.IndexOf("https://" + subDom) == 0)
            //        httpAlias = httpAlias.Substring(httpAlias.IndexOf(subDom) + subDom.Length);
            //}

            //if (httpAlias.IndexOf("www.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("www.") + 4);
            //if (httpAlias.IndexOf("dev.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("dev.") + 4);
            //if (httpAlias.IndexOf("staging.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("staging.") + 8);

            if (httpAlias.IndexOf("127.0.0.1") == 0) {
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

            PortalAliasController paCtrl = new PortalAliasController();
            foreach (DictionaryEntry de in paCtrl.GetPortalAliases()) {
                PortalAliasInfo paInfo = (PortalAliasInfo)de.Value;
                string httpAlias = paInfo.HTTPAlias;
                bool isIP = (Regex.Match(httpAlias, ".*\\d+\\.\\d+\\.\\d+\\.\\d+.*").Length > 0);

                // remove port, if exists
                if (httpAlias.LastIndexOf(":") != -1) httpAlias = httpAlias.Substring(0, httpAlias.IndexOf(":"));

                // remove path
                if (httpAlias.LastIndexOf("/") != -1) httpAlias = httpAlias.Substring(0, httpAlias.IndexOf("/"));

                // remove www.
                httpAlias = StripSubdomains(httpAlias);
                //string[] allowedSubdomains = new string[] { "www.", "www1.", "www2.", "dev.", "test.", "staging." };
                //foreach (string subDom in allowedSubdomains) {
                //    if (httpAlias.IndexOf(subDom) == 0 || httpAlias.IndexOf("http://" + subDom) == 0 || httpAlias.IndexOf("https://" + subDom) == 0)
                //        httpAlias = httpAlias.Substring(httpAlias.IndexOf(subDom) + subDom.Length);
                //}
                //if (!isIP && httpAlias.IndexOf("www.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("www.") + 4);
                //if (!isIP && httpAlias.IndexOf("dev.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("dev.") + 4);
                //if (!isIP && httpAlias.IndexOf("staging.") != -1) httpAlias = httpAlias.Substring(httpAlias.IndexOf("staging.") + 8);

                if (httpAlias.IndexOf("127.0.0.1") == 0) {
                    continue;
                }

                if (ddHosts.Items.FindByText(httpAlias) != null) {
                    continue; // item already exists
                }

                ddHosts.Items.Add(new ListItem(httpAlias, httpAlias));
            }
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