using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using avt.DynamicFlashRotator.Net.Settings;

namespace avt.DynamicFlashRotator.Net.RenderEngine
{
    public class jQueryEngine : IRenderEngine
    {

        public void OnLoad(DynamicRotator rotator)
        {
            // include the rotator
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot_jQuery_1_5_1", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "avt.DynamicFlashRotator.Net.js.jquery-1.5.1.js"));
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot_jQueryUi_1_8_11_eff", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "avt.DynamicFlashRotator.Net.js.jquery-ui-1.8.11.eff.js"));
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot_jQuery-hoverIntent", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "avt.DynamicFlashRotator.Net.js.jquery.hoverIntent.js"));
            rotator.Page.ClientScript.RegisterClientScriptInclude("avtRot-jQuery-rotator", rotator.Page.ClientScript.GetWebResourceUrl(rotator.GetType(), "avt.DynamicFlashRotator.Net.js.avt-jQuery-rotator.js"));
        }

        public void Render(DynamicRotator rotator, HtmlTextWriter output)
        {
            output.Write("<div class='avtRot'></div>");

            output.WriteBeginTag("script");
            output.WriteAttribute("type", "text/javascript");
            output.Write(HtmlTextWriter.TagRightChar);
            output.Write(string.Format("avtRot_jQuery_1_5_1(document).ready(function($) {{ $('#{0} .avtRot').avtRot({1}); }});", rotator.ClientID, rotator.Settings.ToJson()));
            output.WriteEndTag("script");
        }
    }
}
