using System;
using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using avt.DynamicFlashRotator.Net;
using avt.DynamicFlashRotator.Net.Settings;


namespace avt.DynamicFlashRotator.Net
{
    public partial class ActivationWnd : System.Web.UI.Page 
    {
        
        protected void Page_Init(Object Sender, EventArgs args)
        {
            // TODO: security
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
                lnkPurchase.HRef =  RotatorSettings.BuyLink;
            }
        }

        protected void OnActivateManually(Object Sender, EventArgs args)
        {
            pnlActivateForm.Visible = false;
            pnlActivateManually.Visible = true;
            pnlActivateManuallyBtn.Visible = false;

            string host = tbHost.Text.Trim();
            btnActivationTool.HRef = "http://www.avatar-soft.ro/MyAccount/ManualActivation/tabid/180/Default.aspx?regkey=" + txtRegistrationCode.Text + "&host=" + host;
        }

        protected void OnActivate(object Sender, EventArgs args)
        {
            txtRegistrationCode.Text = txtRegistrationCode.Text.Trim();

            AvtRegistrationCode regCode;
            try {
                regCode = new AvtRegistrationCode(txtRegistrationCode.Text);
                //if (!regCode.IsValid(RedirectController.Version)) {
                //    throw new Exception();
                //}
            } catch {
                validateActivation.Text = "The registration code you supplied is invalid.";
                validateActivation.IsValid = false;
                return;
            }

            txtRegistrationCode.Text = txtRegistrationCode.Text.Trim();

            string host = tbHost.Text.Trim();

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


        #region Helpers


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