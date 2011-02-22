using DotNetNuke;
using System.Web.UI;
using DotNetNuke.Entities.Modules;
using System.IO;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Diagnostics;
using DotNetNuke.Security;
using System.Web.UI.WebControls;
using System.Xml.Xsl;
using System.Xml;
using System.Text;
using DotNetNuke.Framework;
using System.Web.UI.HtmlControls;
using DotNetNuke.Entities.Modules.Actions;

namespace avt.DynamicFlashRotator.Dnn
{

    public partial class Rotator : PortalModuleBase, IActionable
    {
        private void Page_Init(object sender, System.EventArgs e)
        {
            
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack) {
                
            }
        }


        private bool HasAdminRights()
        {
            if (PortalSettings.UserMode == DotNetNuke.Entities.Portals.PortalSettings.Mode.Edit &&
                    ((ModuleId != -1 && PortalSecurity.HasNecessaryPermission(SecurityAccessLevel.Edit, PortalSettings, ModuleConfiguration)) ||
                    (PortalSecurity.IsInRole(PortalSettings.AdministratorRoleName)))) {
                return true;
            }

            return false;
        }

       

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), "Manage Slides", DotNetNuke.Entities.Modules.Actions.ModuleActionType.AddContent, "", "icon_hostsettings_16px.gif", EditUrl("Edit"), false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
                Actions.Add(GetNextActionID(), "Rotator Settings", DotNetNuke.Entities.Modules.Actions.ModuleActionType.AddContent, "", "icon_sitesettings_16px.gif", EditUrl("Manage"), false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

    }

}

