using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using avt.DynamicFlashRotator.Net.Settings;

namespace avt.DynamicFlashRotator.Net.RenderEngine
{
    public class FlashEngine : IRenderEngine
    {
        public void OnLoad(DynamicRotator rotator)
        {
        }

        public void Render(DynamicRotator rotator, HtmlTextWriter output)
        {
            // render the flash
            string flashUrl = rotator.Page.ClientScript.GetWebResourceUrl(GetType(), "avt.DynamicFlashRotator.Net.flash.rotator-v2-5.swf");
            string timestamp = rotator.Settings.LastUpdate.ToFileTime().ToString();

            string settingsUrl = rotator.ConfigUrlBase;
            settingsUrl += (settingsUrl.IndexOf('?') > 0 ? (settingsUrl.IndexOf('?') != settingsUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=settings&t=" + timestamp;
            settingsUrl += "&controlId=" + rotator.RealId;
            settingsUrl = HttpUtility.UrlEncode(settingsUrl);

            string contentUrl = rotator.ConfigUrlBase;
            contentUrl += (contentUrl.IndexOf('?') > 0 ? (contentUrl.IndexOf('?') != contentUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=content&t=" + timestamp;
            contentUrl += "&controlId=" + rotator.RealId;
            contentUrl = HttpUtility.UrlEncode(contentUrl);

            string transitionsUrl = rotator.ConfigUrlBase;
            transitionsUrl += (transitionsUrl.IndexOf('?') > 0 ? (transitionsUrl.IndexOf('?') != transitionsUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=transitions&t=" + timestamp;
            transitionsUrl += "&controlId=" + rotator.RealId;
            transitionsUrl = HttpUtility.UrlEncode(transitionsUrl);

            output.Write(
                string.Format(
                //"<script type=\"text/javascript\">AC_FL_RunContent( 'codebase','{0}://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','950','height','250','src','" + flashUrl + "?settingsxml=settings_v2_simple.xml&contentxml=content_v2_simple.xml&transitionsxml=transitions.xml','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','" + flashUrl + "?settingsxml=settings_v2_simple.xml&contentxml=content_v2_simple.xml&transitionsxml=transitions.xml' ); //end AC code</script>" +
                //"<noscript>" +
                "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"{0}://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\"" + rotator.Settings.Width.Value + "\" height=\"" + rotator.Settings.Height.Value + "\">" +
                    "<param name=\"movie\" value=\"" + flashUrl + "&settingsxml=" + settingsUrl + "&contentxml=" + contentUrl + "&transitionsxml=" + transitionsUrl + "\">" +
                    "<param name=\"quality\" value=\"high\">" +
                    "<param name=\"wmode\" value=\"transparent\">" +
                //(Settings.TransparentBackground ? "<param name=\"wmode\" value=\"transparent\">" : "") +
                    "<embed src=\"" + flashUrl + "&settingsxml=" + settingsUrl + "&contentxml=" + contentUrl + "&transitionsxml=" + transitionsUrl + "\" quality=\"high\" pluginspage=\"http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" width=\"" + rotator.Settings.Width.Value + "\" height=\"" + rotator.Settings.Height.Value + "\" wmode=\"transparent\"></embed>" +
                "</object>"
                // + "</noscript>"
                , HttpContext.Current.Request.Url.Scheme)
            );

            // include code for when flash is not installed
            if (!string.IsNullOrEmpty(rotator.Settings.FallbackImage)) {
                output.Write(
                    @"<script type='text/javascript'>

                    var hasFlash = false;
                    try {
                      var fo = new ActiveXObject('ShockwaveFlash.ShockwaveFlash');
                      if(fo) hasFlash = true;
                    }catch(e){
                      if(navigator.mimeTypes ['application/x-shockwave-flash'] != undefined) hasFlash = true;
                    }

                    if (!hasFlash) {
                        var img = document.createElement('IMG');
                        img.src = '"+ rotator.Settings.FallbackImage + @"';
                        var rot = document.getElementById('" + rotator.ClientID + @"');
                        rot.removeChild(rot.childNodes[0]);
                        document.getElementById('" + rotator.ClientID +@"').appendChild(img);
                    }

                    </script>"
                );
            }
        }
    }
}
