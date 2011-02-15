using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing;
using System.Drawing.Design;
using System.Configuration;
using avt.AllinOneRotator.Net.Data;
using avt.AllinOneRotator.Net.Settings;
using avt.AllinOneRotator.Net.Services;

namespace avt.AllinOneRotator.Net
{
    public enum eSlideButtonsType {
        SquareWithNumbers = 1,
        RoundNoNumbers = 2
    }

    [ToolboxData("<{0}:AllinOneRotator runat=server></{0}:AllinOneRotator>")]
    public class AllinOneRotator : WebControl
    {
        RotatorSettings Settings;

        public AllinOneRotator()
        {
            Settings = new RotatorSettings();

            Settings.Width = new Unit(950, UnitType.Pixel);
            Settings.Height = new Unit(250, UnitType.Pixel);

        }

        protected override void  OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Settings.Init(ID, new AspNetConfiguration(DbConnectionString, DbOwner, DbObjectQualifier));

            // merge dynamic settings
            if (EnableRuntimeConfiguration) {
                Settings.LoadFromDB();
            }

            if (Page.Request.Params["avtadrot"] == "settings") {
                Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Page.Response.Cache.SetNoStore();
                Page.Response.Write(Settings.ToXml());
                Page.Response.ContentType = "text/xml";
                Page.Response.End();
                return;
            }

            if (Page.Request.Params["avtadrot"] == "content") {
                Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Page.Response.Cache.SetNoStore(); 
                Page.Response.Write(GetSlidesXml());
                Page.Response.ContentType = "text/xml";
                Page.Response.End();
                return;
            }

            if (Page.Request.Params["avtadrot"] == "transitions") {
                Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Page.Response.Cache.SetNoStore(); 
                Page.Response.Write(@"<?xml version=""1.0"" encoding=""utf-8""?>
<picturesTransitions>
    <transition theName=""Blinds"" theEasing=""Strong"" theStrips=""20"" theDimension=""1""/>
    <trasition ></trasition>
    <transition theName=""Fly"" theEasing=""Strong"" theStartPoint=""9""/>
    <transition theName=""Iris"" theEasing=""Bounce"" theStartPoint=""1"" theShape=""CIRCLE""/>
    <transition theName=""Photo"" theEasing=""Elastic""/>
    <transition theName=""PixelDissolve"" theEasing=""Strong"" theXsections=""20"" theYsections=""20""/>
    <transition theName=""Rotate"" theEasing=""Strong"" theDegrees=""720""/>
    <transition theName=""Squeeze"" theEasing=""Strong"" theDimension=""1""/>
    <transition theName=""Wipe"" theEasing=""Strong"" theStartPoint=""1""/>
    <transition theName=""Zoom"" theEasing=""Back""/>
</picturesTransitions>
"
                    );
                Page.Response.ContentType = "text/xml";
                Page.Response.End();
                return;
            }
            
        }


        #region Runtime Configuration

        bool _EnableRuntimeConfiguration = false;
        [Category("ALLinOne Rotator - Runtime Configuration")]
        [Description("Enable runtime configuration using the Web Interface")]
        public bool EnableRuntimeConfiguration { get { return _EnableRuntimeConfiguration; } set { _EnableRuntimeConfiguration = value; } }

        string _DbConnectionString = null;
        [Category("ALLinOne Rotator - Runtime Configuration")]
        [Description("Specify the name of a connection string from web.config to allow runtime manipulation of Rotator Settings using the Web Interface.")]
        public string DbConnectionString { get { return _DbConnectionString; } set { _DbConnectionString = value; } }

        string _DbOwner = "dbo";
        [Category("ALLinOne Rotator - Runtime Configuration")]
        [Description("Database Owner")]
        public string DbOwner { get { return _DbOwner; } set { _DbOwner = value; } }

        string _DbObjectQualifier = null;
        [Category("ALLinOne Rotator - Runtime Configuration")]
        [Description("Object qualifier (prefix for ALLinONE Rotator tables)")]
        public string DbObjectQualifier { get { return _DbObjectQualifier; } set { _DbObjectQualifier = value; } }

        string _ManageUrl = null;
        [Category("ALLinOne Rotator - Runtime Configuration")]
        [UrlProperty()]
        [Description("Provide the URL to where you unpacked ManageRotator.aspx that cames with your ALLinONE Rotator copy.")]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ManageUrl { get { return _ManageUrl; } set { _ManageUrl = value; } }

        #endregion


        #region Custom Properties

        [Category("Layout")]
        public override Unit Width { get { return Settings.Width; } set { Settings.Width = value; } }

        [Category("Layout")]
        public override Unit Height { get { return Settings.Height; } set { Settings.Height = value; } }

        [Category("ALLinOne Rotator")]
        public bool AutoStartSlideShow { get { return Settings.AutoStartSlideShow; } set { Settings.AutoStartSlideShow = value; } }

        [Category("ALLinOne Rotator")]
        public bool UseRoundCornersMask { get { return Settings.UseRoundCornersMask; } set { Settings.UseRoundCornersMask = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color RoundCornerMaskColor { get { return Settings.RoundCornerMaskColor; } set { Settings.RoundCornerMaskColor = value; } }

        [Category("ALLinOne Rotator")]
        public bool ShowBottomButtons { get { return Settings.ShowBottomButtons; } set { Settings.ShowBottomButtons = value; } }

        [Category("ALLinOne Rotator")]
        public bool ShowPlayPauseControls { get { return Settings.ShowPlayPauseControls; } set { Settings.ShowPlayPauseControls = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color FadeColor { get { return Settings.FadeColor; } set { Settings.FadeColor = value; } }
        
        [Category("ALLinOne Rotator")]
        public bool ShowTopTitle { get { return Settings.ShowTopTitle; } set { Settings.ShowTopTitle = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color TopTitleBackground { get { return Settings.TopTitleBackground; } set { Settings.TopTitleBackground = value; } }

        [Category("ALLinOne Rotator")]
        public int TopTitleBgTransparency { get { return Settings.TopTitleBgTransparency; } set { Settings.TopTitleBgTransparency = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color TopTitleTextColor { get { return Settings.TopTitleTextColor; } set { Settings.TopTitleTextColor = value; } }

        [Category("ALLinOne Rotator")]
        public bool ShowTimerBar { get { return Settings.ShowTimerBar; } set { Settings.ShowTimerBar = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color SlideButtonsColor { get { return Settings.SlideButtonsColor; } set { Settings.SlideButtonsColor = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color SlideButtonsNumberColor { get { return Settings.SlideButtonsNumberColor; } set { Settings.SlideButtonsNumberColor = value; } }

        [Category("ALLinOne Rotator")]
        public eSlideButtonsType SlideButtonsType { get { return Settings.SlideButtonsType; } set { Settings.SlideButtonsType = value; } }

        [Category("ALLinOne Rotator")]
        public int SlideButtonsXoffset { get { return Settings.SlideButtonsXoffset; } set { Settings.SlideButtonsXoffset = value; } }

        [Category("ALLinOne Rotator")]
        public int SlideButtonsYoffset { get { return Settings.SlideButtonsYoffset; } set { Settings.SlideButtonsYoffset = value; } }

        [Category("ALLinOne Rotator")]
        public bool TransparentBackground { get { return Settings.TransparentBackground; } set { Settings.TransparentBackground = value; } }

        #endregion


        #region Slides

        [DefaultValue("")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("ALLinOne Rotator - Slides")]
        [Editor("avt.AllinOneRotator.Net.SlideCollectionEditor,avt.AllinOneRotator.Net", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public SlideCollection Slides { get { return Settings.Slides; } }

        string GetSlidesXml()
        {
            StringBuilder strXML = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
            settings.Encoding = Encoding.UTF8;
            XmlWriter Writer = XmlWriter.Create(strXML, settings);

            Writer.WriteStartElement("ads");
            foreach (SlideInfo slide in Slides) {
                slide.ToXml(Writer);
            }
            Writer.WriteEndElement(); // "ads";

            Writer.Close();

            return strXML.ToString();
        }

        #endregion


        protected override void RenderContents(HtmlTextWriter output)
        {
            if (base.DesignMode) {
                output.Write("<div style = 'width: " + base.Width + "; height: " + base.Height + "; border: 1px solid #929292; background-color: #c2c2c2;'>ALLinOneRotator.NET</div>");
            } else {
                RenderFrontEnd(output);
            }
        }

        void RenderFrontEnd(HtmlTextWriter output)
        {
            // add include
            //Page.ClientScript.RegisterClientScriptInclude("AC_RunActiveContent", TemplateSourceDirectory + "/flash/AC_RunActiveContent.js");
            //Page.ClientScript.RegisterClientScriptBlock(GetType(), "AC_FL_RunContent", "AC_FL_RunContent = 0;", true);

            // render the flash
            string flashUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "avt.AllinOneRotator.Net.flash.rotator-v2-5.swf");
            string timestamp = Settings.LastUpdate.ToFileTime().ToString();

            string settingsUrl = Page.Request.RawUrl;
            settingsUrl += (settingsUrl.IndexOf('?') > 0 ? (settingsUrl.IndexOf('?') != settingsUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=settings&t=" + timestamp;

            string contentUrl = Page.Request.RawUrl;
            contentUrl += (contentUrl.IndexOf('?') > 0 ? (contentUrl.IndexOf('?') != contentUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=content&t=" + timestamp;

            string transitionsUrl = Page.Request.RawUrl;
            transitionsUrl += (transitionsUrl.IndexOf('?') > 0 ? (transitionsUrl.IndexOf('?') != transitionsUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=transitions&t=" + timestamp;

            output.Write(
                //"<script type=\"text/javascript\">AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','950','height','250','src','" + flashUrl + "?settingsxml=settings_v2_simple.xml&contentxml=content_v2_simple.xml&transitionsxml=transitions.xml','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','" + flashUrl + "?settingsxml=settings_v2_simple.xml&contentxml=content_v2_simple.xml&transitionsxml=transitions.xml' ); //end AC code</script>" +
                //"<noscript>" +
                "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\"" + Width.Value + "\" height=\"" + Height.Value + "\">" +
                    "<param name=\"movie\" value=\"" + flashUrl + "&settingsxml=" + settingsUrl + "&contentxml=" + contentUrl + "&transitionsxml=" + transitionsUrl + "\">" +
                    "<param name=\"quality\" value=\"high\">" +
                    (Settings.TransparentBackground ? "<param name=\"wmode\" value=\"transparent\">" : "") +
                    "<embed src=\"" + flashUrl + "&settingsxml=" + settingsUrl + "&contentxml=" + contentUrl + "&transitionsxml=" + transitionsUrl + "\" quality=\"high\" pluginspage=\"http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" width=\"" + Width.Value + "\" height=\"" + Height.Value + "\""+ (Settings.TransparentBackground ? " wmode=\"transparent\"" : "") +"></embed>" +
                "</object>"
                // + "</noscript>"
            );

            if (EnableRuntimeConfiguration) {
                string manageUrl = Page.ResolveUrl(ManageUrl);
                manageUrl += "?controlId=" + ID;
                manageUrl += "&connStr=" + DbConnectionString;
                manageUrl += "&dbOwner=" + DbOwner;
                manageUrl += "&objQualifier=" + DbObjectQualifier;
                manageUrl += "&rurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl);
                output.Write("<a href='" + manageUrl + "'>Modify Rotator Settings</a>");
            }
        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    string resourceName = "avt.AllinOneRotator.Net.flash.AC_RunActiveContent.js";

        //    ClientScriptManager cs = this.Page.ClientScript;
        //    cs.RegisterClientScriptResource(GetType(), resourceName);
        //}

    }
}
