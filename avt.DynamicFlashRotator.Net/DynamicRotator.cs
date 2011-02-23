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
using avt.DynamicFlashRotator.Net.Data;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net.Services;

namespace avt.DynamicFlashRotator.Net
{
    public enum eSlideButtonsType {
        SquareWithNumbers = 1,
        RoundNoNumbers = 2
    }

    [ToolboxData("<{0}:DynamicRotator runat=server></{0}:DynamicRotator>")]
    public class DynamicRotator : WebControl
    {
        RotatorSettings Settings;

        public DynamicRotator()
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
           
            // check settings
            if (RotatorSettings.Configuration == null) {
                if (!string.IsNullOrEmpty(DbConnectionString)) {
                    RotatorSettings.Init(new AspNetConfiguration());
                    RotatorSettings.Init(new AspNetConfiguration(DbConnectionString, DbOwner, DbObjectQualifier));
                } else {
                    // don't have DbConnectionString
                }
            }
            

            // merge dynamic settings
            if (EnableRuntimeConfiguration) {
                Settings.LoadFromDB(RealId);
            }

            if (Page.Request.Params["controlId"] == RealId) {
                if (Page.Request.Params["avtadrot"] == "settings") {
                    Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Page.Response.Cache.SetNoStore();
                    Page.Response.Write(Settings.ToXml());
                    //Page.Response.ContentType = "text/xml";
                    Page.Response.ContentType = "text/xml; charset=utf-8";
                    Page.Response.End();
                    return;
                }

                if (Page.Request.Params["avtadrot"] == "content") {
                    Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Page.Response.Cache.SetNoStore(); 
                    Page.Response.Write(GetSlidesXml());
                    Page.Response.ContentType = "text/xml; charset=utf-8"; 
                    //Page.Response.ContentType = "text/xml";
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
        }

        string _OverrideId = null;
        public string OverrideId { get { return _OverrideId; } set { _OverrideId = value; } }
        public string RealId { get { return string.IsNullOrEmpty(OverrideId) ? this.ID : OverrideId; } }

        #region Runtime Configuration

        bool _EnableRuntimeConfiguration = false;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Enable runtime configuration using the Web Interface")]
        public bool EnableRuntimeConfiguration { get { return _EnableRuntimeConfiguration; } set { _EnableRuntimeConfiguration = value; } }

        string _DbConnectionString = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Specify the name of a connection string from web.config to allow runtime manipulation of Rotator Settings using the Web Interface.")]
        public string DbConnectionString { get { return _DbConnectionString; } set { _DbConnectionString = value; } }

        string _DbOwner = "dbo";
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Database Owner")]
        public string DbOwner { get { return _DbOwner; } set { _DbOwner = value; } }

        string _DbObjectQualifier = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Object qualifier (prefix for Dynamic Rotator tables)")]
        public string DbObjectQualifier { get { return _DbObjectQualifier; } set { _DbObjectQualifier = value; } }

        string _ManageUrl = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [UrlProperty()]
        [Description("Provide the URL to where you unpacked ManageRotator.aspx that cames with your Dynamic Rotator copy.")]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ManageUrl { get { return _ManageUrl; } set { _ManageUrl = value; } }

        #endregion


        #region Custom Properties

        [Category("Layout")]
        public override Unit Width { get { return Settings.Width; } set { Settings.Width = value; } }

        [Category("Layout")]
        public override Unit Height { get { return Settings.Height; } set { Settings.Height = value; } }

        [Category("Dynamic Rotator")]
        public bool AutoStartSlideShow { get { return Settings.AutoStartSlideShow; } set { Settings.AutoStartSlideShow = value; } }

        [Category("Dynamic Rotator")]
        public bool UseRoundCornersMask { get { return Settings.UseRoundCornersMask; } set { Settings.UseRoundCornersMask = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        public Color RoundCornerMaskColor { get { return Settings.RoundCornerMaskColor; } set { Settings.RoundCornerMaskColor = value; } }

        [Category("Dynamic Rotator")]
        public bool ShowBottomButtons { get { return Settings.ShowBottomButtons; } set { Settings.ShowBottomButtons = value; } }

        [Category("Dynamic Rotator")]
        public bool ShowPlayPauseControls { get { return Settings.ShowPlayPauseControls; } set { Settings.ShowPlayPauseControls = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        public Color FadeColor { get { return Settings.FadeColor; } set { Settings.FadeColor = value; } }
        
        [Category("Dynamic Rotator")]
        public bool ShowTopTitle { get { return Settings.ShowTopTitle; } set { Settings.ShowTopTitle = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        public Color TopTitleBackground { get { return Settings.TopTitleBackground; } set { Settings.TopTitleBackground = value; } }

        [Category("Dynamic Rotator")]
        public int TopTitleBgTransparency { get { return Settings.TopTitleBgTransparency; } set { Settings.TopTitleBgTransparency = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        public Color TopTitleTextColor { get { return Settings.TopTitleTextColor; } set { Settings.TopTitleTextColor = value; } }

        [Category("Dynamic Rotator")]
        public bool ShowTimerBar { get { return Settings.ShowTimerBar; } set { Settings.ShowTimerBar = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        public Color SlideButtonsColor { get { return Settings.SlideButtonsColor; } set { Settings.SlideButtonsColor = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        public Color SlideButtonsNumberColor { get { return Settings.SlideButtonsNumberColor; } set { Settings.SlideButtonsNumberColor = value; } }

        [Category("Dynamic Rotator")]
        public eSlideButtonsType SlideButtonsType { get { return Settings.SlideButtonsType; } set { Settings.SlideButtonsType = value; } }

        [Category("Dynamic Rotator")]
        public int SlideButtonsXoffset { get { return Settings.SlideButtonsXoffset; } set { Settings.SlideButtonsXoffset = value; } }

        [Category("Dynamic Rotator")]
        public int SlideButtonsYoffset { get { return Settings.SlideButtonsYoffset; } set { Settings.SlideButtonsYoffset = value; } }

        [Category("Dynamic Rotator")]
        public bool TransparentBackground { get { return Settings.TransparentBackground; } set { Settings.TransparentBackground = value; } }

        #endregion


        #region Slides

        [DefaultValue("")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Dynamic Rotator - Slides")]
        [Editor("avt.DynamicRotator.Net.SlideCollectionEditor,avt.DynamicRotator.Net", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public SlideCollection Slides { get { return Settings.Slides; } }

        string GetSlidesXml()
        {
            StringBuilder strXML = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.Encoding = Encoding.UTF8;
            XmlWriter Writer = XmlWriter.Create(strXML, settings);

            if (!RotatorSettings.IsActivated()) {
                Random r = new Random();
                if (r.Next(20) == 1) {
                    
                    SlideObjectInfo trialText = new SlideObjectInfo();
                    trialText.ObjectType = eObjectType.Text;
                    trialText.Text = "<font size='20px' color='#C77405'>Powered by <font size='30px'><i>Dynamic Rotator .NET</i></font> from Avatar Software</font><br/><font color='#525252;' size='14px'><i>This slide is randomly displayed to inform you that this copy is not yet licensed.</i></font>";
                    trialText.Yposition = 70;
                    trialText.Xposition = 240;

                    SlideObjectInfo logoObj = new SlideObjectInfo();
                    logoObj.ObjectType = eObjectType.Image;
                    logoObj.ObjectUrl = "http://www.avatar-soft.ro/Portals/0/product_logo/Dynamic-Rotator.png";
                    logoObj.Yposition = 30;
                    logoObj.Xposition = 20;
                    logoObj.SlideFrom = eAllDirs.Left;
                    logoObj.EffectAfterSlide = eEffect.Zoom;
                    logoObj.TransitionDuration = 1;
                    
                    SlideInfo trialSlide = new SlideInfo();
                    trialSlide.SlideObjects.Add(trialText);
                    trialSlide.SlideObjects.Add(logoObj);
                    trialSlide.SlideUrl = "http://www.avatar-soft.ro/dotnetnuke/modules/flash/dynamic-rotator.aspx";
                    trialSlide.ButtonCaption = "Read More...";

                    Slides.Clear();
                    Slides.Add(trialSlide);
                }
            }

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
                output.Write("<div style = 'width: " + base.Width + "; height: " + base.Height + "; border: 1px solid #929292; background-color: #c2c2c2;'>DynamicRotator.NET</div>");
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
            string flashUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "avt.DynamicFlashRotator.Net.flash.rotator-v2-5.swf");
            string timestamp = Settings.LastUpdate.ToFileTime().ToString();

            string settingsUrl = Page.Request.RawUrl;
            settingsUrl += (settingsUrl.IndexOf('?') > 0 ? (settingsUrl.IndexOf('?') != settingsUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=settings&t=" + timestamp;
            settingsUrl += "&controlId=" + RealId;
            settingsUrl = HttpUtility.UrlEncode(settingsUrl);

            string contentUrl = Page.Request.RawUrl;
            contentUrl += (contentUrl.IndexOf('?') > 0 ? (contentUrl.IndexOf('?') != contentUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=content&t=" + timestamp;
            contentUrl += "&controlId=" + RealId;
            contentUrl = HttpUtility.UrlEncode(contentUrl);

            string transitionsUrl = Page.Request.RawUrl;
            transitionsUrl += (transitionsUrl.IndexOf('?') > 0 ? (transitionsUrl.IndexOf('?') != transitionsUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=transitions&t=" + timestamp;
            transitionsUrl += "&controlId=" + RealId;
            transitionsUrl = HttpUtility.UrlEncode(transitionsUrl);

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

            if (EnableRuntimeConfiguration && RotatorSettings.Configuration.ShowManageLinks() && RotatorSettings.Configuration.HasAccess(RealId)) {
                string manageUrl = Page.ResolveUrl(ManageUrl);
                manageUrl += "?controlId=" + RealId;
                manageUrl += "&connStr=" + DbConnectionString;
                manageUrl += "&dbOwner=" + DbOwner;
                manageUrl += "&objQualifier=" + DbObjectQualifier;
                manageUrl += "&rurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl);
                output.Write("<br />");
                output.Write("<a href='" + manageUrl + "#tabs-main-slides'>Manage Slides</a>");
                output.Write("<br />");
                output.Write("<a href='" + manageUrl + "'>Manage Rotator Settings</a>");
            }
        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    string resourceName = "avt.DynamicRotator.Net.flash.AC_RunActiveContent.js";

        //    ClientScriptManager cs = this.Page.ClientScript;
        //    cs.RegisterClientScriptResource(GetType(), resourceName);
        //}

    }
}
