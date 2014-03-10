using System.Web;
using System.Web.UI;
using avt.DynamicFlashRotator.Net.Settings;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Portals;
using System.Collections;
using System.IO;
using avt.DynamicFlashRotator.Dnn.DnnSf.Licensing.v2;

namespace avt.DynamicFlashRotator.Dnn
{

    public partial class Rotator : PortalModuleBase, IActionable
    {
        protected bool IsAdmin
        {
            get
            {
                bool isAdmin = false;
                if (PortalSettings.UserMode == DotNetNuke.Entities.Portals.PortalSettings.Mode.Edit &&
                    ((ModuleId != -1 && PortalSecurity.HasNecessaryPermission(SecurityAccessLevel.Edit, PortalSettings, ModuleConfiguration)) || PortalSecurity.IsInRole(PortalSettings.AdministratorRoleName))) {
                    isAdmin = true;
                }

                return isAdmin;
            }
        }

        private void Page_Init(object sender, System.EventArgs e)
        {
            RotatorSettings.Init(new DnnConfiguration());
            DynamicRotator.OverrideId = ModuleId.ToString();
            DynamicRotator.ConfigUrlBase = TemplateSourceDirectory + "/Config.ashx";
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            RotatorSettings rotatorSettings = new RotatorSettings();
            var licStatus = RotatorSettings.Configuration.LicenseStatus;
            if (licStatus.Type == LicenseStatus.eType.Error) {
                this.pnlAdmin.InnerText = licStatus.Message;
                DynamicRotator.Visible = false;
            }

            //Control ctrlAct = LoadControl(TemplateSourceDirectory.TrimEnd('/') + "/RegCore/QuickStatusAndLink.ascx");
            //(ctrlAct as IRegCoreComponent).InitRegCore(IsAdmin, RotatorSettings.RegCoreServer, RotatorSettings.ProductName, RotatorSettings.ProductCode, RotatorSettings.ProductKey, RotatorSettings.Version, TemplateSourceDirectory.TrimEnd('/') + "/RegCore/", typeof(DynamicRotatorController));
            //this.pnlAdmin.Controls.Add(ctrlAct);

            //if (!rotatorSettings.IsActivated() || rotatorSettings.IsTrialExpired()) {
            //    DynamicRotator.Visible = false;
            //}

        }

       

        //private bool HasAdminRights()
        //{
        //    if (PortalSettings.UserMode == DotNetNuke.Entities.Portals.PortalSettings.Mode.Edit &&
        //            ((ModuleId != -1 && PortalSecurity.HasNecessaryPermission(SecurityAccessLevel.Edit, PortalSettings, ModuleConfiguration)) ||
        //            (PortalSecurity.IsInRole(PortalSettings.AdministratorRoleName)))) {
        //        return true;
        //    }

        //    return false;
        //}

        public ModuleActionCollection ModuleActions
        {
            get {

                var license = LicenseFactory.Get(RotatorSettings.Configuration.LicenseFilePath, RotatorSettings.Version, RotatorSettings.ProductKey);
                var licStatus = license.Status;
                if (licStatus.Type == LicenseStatus.eType.Error) {
                    return new ModuleActionCollection();
                }

                //RotatorSettings rotatorSettings = new RotatorSettings();
                //if (!rotatorSettings.IsActivated() || rotatorSettings.IsTrialExpired()) {
                //    return new ModuleActionCollection();
                //}

                string managePath = TemplateSourceDirectory + "/ManageRotator.aspx?controlId=" + ModuleId.ToString() + "&rurl=" + HttpUtility.UrlEncode(Request.RawUrl) + "&portalid=" + PortalId;
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), "Manage Slides", DotNetNuke.Entities.Modules.Actions.ModuleActionType.AddContent, "", "icon_hostsettings_16px.gif", managePath +"#tabs-main-slides", false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
                Actions.Add(GetNextActionID(), "Rotator Settings", DotNetNuke.Entities.Modules.Actions.ModuleActionType.AddContent, "", "icon_sitesettings_16px.gif", managePath, false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

    }

}

