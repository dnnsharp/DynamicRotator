using System.Web;
using System.Web.UI;
using avt.DynamicFlashRotator.Net.Settings;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;

namespace avt.DynamicFlashRotator.Dnn
{

    public partial class Rotator : PortalModuleBase, IActionable
    {
        private void Page_Init(object sender, System.EventArgs e)
        {
            RotatorSettings.Init(new DnnConfiguration());
            DynamicRotator.OverrideId = ModuleId.ToString();
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack) {
                
            }
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
                string managePath = TemplateSourceDirectory + "/ManageRotator.aspx?controlId=" + ModuleId.ToString() + "&rurl=" + HttpUtility.UrlEncode(Request.RawUrl);

                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), "Manage Slides", DotNetNuke.Entities.Modules.Actions.ModuleActionType.AddContent, "", "icon_hostsettings_16px.gif", managePath + "#tabs-main-slides", false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
                Actions.Add(GetNextActionID(), "Rotator Settings", DotNetNuke.Entities.Modules.Actions.ModuleActionType.AddContent, "", "icon_sitesettings_16px.gif", managePath, false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

    }

}

