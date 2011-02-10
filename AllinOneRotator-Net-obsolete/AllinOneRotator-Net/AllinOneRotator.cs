﻿using System;
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

namespace avt.AllinOneRotator.Net
{
    public enum eSlideButtonsType {
        SquareWithNumbers = 1,
        RoundNoNumbers = 2
    }

    [ToolboxData("<{0}:AllinOneRotator runat=server></{0}:AllinOneRotator>")]
    public class AllinOneRotator : WebControl
    {
        public AllinOneRotator()
        {
            base.Width = new Unit(950, UnitType.Pixel);
            base.Height = new Unit(250, UnitType.Pixel);

            // string connStr = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Page.Request.Params["avtadrot"] == "settings") {
                Page.Response.Write(GetSettingsXml());
                Page.Response.ContentType = "text/xml";
                Page.Response.End();
                return;
            }

            if (Page.Request.Params["avtadrot"] == "content") {
                Page.Response.Write(GetSlidesXml());
                Page.Response.ContentType = "text/xml";
                Page.Response.End();
                return;
            }

            if (Page.Request.Params["avtadrot"] == "transitions") {
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
        [Description("Specify a connection string (either the name of the connection string from web.config or directly the connection string) to allow runtime manipulation of Rotator Settings using the Web Interface.")]
        public string DbConnectionString { get { return _DbConnectionString; } set { _DbConnectionString = value; } }

        string _ManageUrl = null;
        [UrlProperty()]
        [Description("Provide the URL to where you unpacked ManageRotator.aspx that cames with your ALLinONE Rotator copy.")]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ManageUrl { get { return _ManageUrl; } set { _ManageUrl = value; } }

        #endregion

        #region Custom Properties

        bool _AutoStartSlideShow = true;
        [Category("ALLinOne Rotator")]
        public bool AutoStartSlideShow { get { return _AutoStartSlideShow; } set { _AutoStartSlideShow = value; } }

        bool _UseRoundCornersMask = true;
        [Category("ALLinOne Rotator")]
        public bool UseRoundCornersMask { get { return _UseRoundCornersMask; } set { _UseRoundCornersMask = value; } }

        Color _RoundCornerMaskColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color RoundCornerMaskColor { get { return _RoundCornerMaskColor; } set { _RoundCornerMaskColor = value; } }

        bool _ShowBottomButtons = true;
        [Category("ALLinOne Rotator")]
        public bool ShowBottomButtons { get { return _ShowBottomButtons; } set { _ShowBottomButtons = value; } }

        bool _ShowPlayPauseControls = true;
        [Category("ALLinOne Rotator")]
        public bool ShowPlayPauseControls { get { return _ShowPlayPauseControls; } set { _ShowPlayPauseControls = value; } }

        Color _FadeColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color FadeColor { get { return _FadeColor; } set { _FadeColor = value; } }
        
        bool _ShowTopTitle = true;
        [Category("ALLinOne Rotator")]
        public bool ShowTopTitle { get { return _ShowTopTitle; } set { _ShowTopTitle = value; } }

        Color _TopTitleBackground = Color.Black;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color TopTitleBackground { get { return _TopTitleBackground; } set { _TopTitleBackground = value; } }

        int _TopTitleBgTransparency = 70;
        [Category("ALLinOne Rotator")]
        public int TopTitleBgTransparency { get { return _TopTitleBgTransparency; } set { _TopTitleBgTransparency = value; } }

        Color _TopTitleTextColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color TopTitleTextColor { get { return _TopTitleTextColor; } set { _TopTitleTextColor = value; } }

        bool _ShowTimerBar = true;
        [Category("ALLinOne Rotator")]
        public bool ShowTimerBar { get { return _ShowTimerBar; } set { _ShowTimerBar = value; } }

        Color _SmallButtonsColor = Color.FromArgb(0x12, 0x12, 0x12);
        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color SlideButtonsColor { get { return _SmallButtonsColor; } set { _SmallButtonsColor = value; } }

        Color _SmallButtonsNumberColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("ALLinOne Rotator")]
        public Color SlideButtonsNumberColor { get { return _SmallButtonsNumberColor; } set { _SmallButtonsNumberColor = value; } }

        eSlideButtonsType _SmallButtonsType = eSlideButtonsType.SquareWithNumbers;
        [Category("ALLinOne Rotator")]
        public eSlideButtonsType SlideButtonsType { get { return _SmallButtonsType; } set { _SmallButtonsType = value; } }

        int _SmallButtonsXoffset = 20;
        [Category("ALLinOne Rotator")]
        public int SlideButtonsXoffset { get { return _SmallButtonsXoffset; } set { _SmallButtonsXoffset = value; } }

        int _SmallButtonsYoffset = 35;
        [Category("ALLinOne Rotator")]
        public int SlideButtonsYoffset { get { return _SmallButtonsYoffset; } set { _SmallButtonsYoffset = value; } }

        bool _TransparentBackground = false;
        [Category("ALLinOne Rotator")]
        public bool TransparentBackground { get { return _TransparentBackground; } set { _TransparentBackground = value; } }


        string GetSettingsXml()
        {
            StringBuilder strXML = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
            settings.Encoding = Encoding.UTF8;
            XmlWriter Writer = XmlWriter.Create(strXML, settings);

            Writer.WriteStartElement("settings");
            Writer.WriteElementString("stageWidth", Width.Value.ToString());
            Writer.WriteElementString("stageHeight", Height.Value.ToString());
            Writer.WriteElementString("startSlideShow", AutoStartSlideShow ? "yes" : "no");
            Writer.WriteElementString("useRoundCornersMask", UseRoundCornersMask ? "yes" : "no");
            Writer.WriteElementString("roundCornerMaskColor", ColorExt.ColorToHexString(RoundCornerMaskColor));
            Writer.WriteElementString("showBottomButtons", ShowBottomButtons ? "yes" : "no");
            Writer.WriteElementString("showPlayPauseControls", ShowPlayPauseControls ? "yes" : "no");
            Writer.WriteElementString("fadeColor", ColorExt.ColorToHexString(FadeColor));
            Writer.WriteElementString("showTopTitle", ShowTopTitle ? "yes" : "no");
            Writer.WriteElementString("topTitleBackground", ColorExt.ColorToHexString(TopTitleBackground));
            Writer.WriteElementString("topTitleBgTransparency", TopTitleBgTransparency.ToString());
            Writer.WriteElementString("topTitleTextColor", ColorExt.ColorToHexString(TopTitleTextColor));
            Writer.WriteElementString("showTimerBar", ShowTimerBar ? "yes" : "no");
            Writer.WriteElementString("smallButtonsColor", ColorExt.ColorToHexString(SlideButtonsColor));
            Writer.WriteElementString("smallButtonsNumberColor", ColorExt.ColorToHexString(SlideButtonsNumberColor));
            Writer.WriteElementString("smallButtonsType", ((int)SlideButtonsType).ToString());
            Writer.WriteElementString("smallButtonsXoffset", SlideButtonsXoffset.ToString());
            Writer.WriteElementString("smallButtonsYoffset", SlideButtonsYoffset.ToString());
            Writer.WriteElementString("transparentBackground", TransparentBackground ? "yes" : "no");
            Writer.WriteEndElement(); // "settings";

            Writer.Close();

            return strXML.ToString();
        }

        #endregion

        #region Slides

        SlideCollection _Slides = new SlideCollection();

        [DefaultValue("")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("ALLinOne Rotator - Slides")]
        [Editor("avt.AllinOneRotator.Net.SlideCollectionEditor,avt.AllinOneRotator.Net", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public SlideCollection Slides { get { return _Slides; } }

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
                if (Page.Request.Params["avtadrot"] == "manage") {
                    new avt.AllinOneRotator.Net.WebManage.Main().Render(output);
                    return;
                }

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

            string settingsUrl = Page.Request.RawUrl;
            settingsUrl += (settingsUrl.IndexOf('?') > 0 ? (settingsUrl.IndexOf('?') != settingsUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=settings";

            string contentUrl = Page.Request.RawUrl;
            contentUrl += (contentUrl.IndexOf('?') > 0 ? (contentUrl.IndexOf('?') != contentUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=content";

            string transitionsUrl = Page.Request.RawUrl;
            transitionsUrl += (transitionsUrl.IndexOf('?') > 0 ? (transitionsUrl.IndexOf('?') != transitionsUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=transitions";

            output.Write(
                //"<script type=\"text/javascript\">AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','950','height','250','src','" + flashUrl + "?settingsxml=settings_v2_simple.xml&contentxml=content_v2_simple.xml&transitionsxml=transitions.xml','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','" + flashUrl + "?settingsxml=settings_v2_simple.xml&contentxml=content_v2_simple.xml&transitionsxml=transitions.xml' ); //end AC code</script>" +
                //"<noscript>" +
                "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\"" + Width.Value + "\" height=\"" + Height.Value + "\">" +
                    "<param name=\"movie\" value=\"" + flashUrl + "&settingsxml=" + settingsUrl + "&contentxml=" + contentUrl + "&transitionsxml=" + transitionsUrl + "\">" +
                    "<param name=\"quality\" value=\"high\">" +
                    "<embed src=\"" + flashUrl + "&settingsxml=" + settingsUrl + "&contentxml=" + contentUrl + "&transitionsxml=" + transitionsUrl + "\" quality=\"high\" pluginspage=\"http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" width=\"" + Width.Value + "\" height=\"" + Height.Value + "\"></embed>" +
                "</object>"
                // + "</noscript>"
            );

            if (EnableRuntimeConfiguration) {
                string manageUrl = Page.Request.RawUrl;
                manageUrl += (manageUrl.IndexOf('?') > 0 ? (manageUrl.IndexOf('?') != manageUrl.Length - 1 ? "&" : "") : "?") + "avtadrot=manage";
                manageUrl += ("&controlId=" + ID);
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