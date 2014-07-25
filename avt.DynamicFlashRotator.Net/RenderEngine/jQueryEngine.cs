using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using DnnSharp.DynamicRotator.Core.Settings;

namespace DnnSharp.DynamicRotator.Core.RenderEngine
{
    public class jQueryEngine : IRenderEngine
    {

        public void OnLoad(DynamicRotator rotator)
        {
            // include the rotator
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot_jQuery", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "DnnSharp.DynamicRotator.Core.js.jquery-1.9.1.js"));
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot_color", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "DnnSharp.DynamicRotator.Core.js.color.js"));
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot_jQueryUi_1_8_11_eff", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "DnnSharp.DynamicRotator.Core.js.jquery-ui-1.8.11.eff.js"));
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot_jQuery-hoverIntent", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "DnnSharp.DynamicRotator.Core.js.jquery.hoverIntent.js"));
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot-jQuery-rotator", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "DnnSharp.DynamicRotator.Core.js.avt-jQuery-rotator.js"));
        }

        public void Render(DynamicRotator rotator, HtmlTextWriter output)
        {
            output.Write("<div class='avtRot' style=\"direction: " + rotator.Settings.Direction + ";\"></div>");

            output.WriteBeginTag("script");
            output.WriteAttribute("type", "text/javascript");
            output.Write(HtmlTextWriter.TagRightChar);
            output.Write(string.Format("avtRot_jQuery(document).ready(function($) {{ $('#{0} .avtRot').avtRot({1}); }});", rotator.ClientID, rotator.Settings.ToJson()));
            output.WriteEndTag("script");
        }
    }
}
