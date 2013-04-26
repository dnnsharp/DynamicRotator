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
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using avt.DynamicFlashRotator.Net.Services.Authentication;
using avt.DynamicFlashRotator.Net.RenderEngine;
using System.Collections;

namespace avt.DynamicFlashRotator.Net
{
    public enum eSlideButtonsType {
        SquareWithNumbers = 1,
        RoundNoNumbers = 2
    }

    [ToolboxData("<{0}:DynamicRotator runat=server></{0}:DynamicRotator>")]
    [Designer(typeof(avt.DynamicFlashRotator.Net.DynamicRotatorDesigner))]
    public class DynamicRotator : WebControl
    {
        public RotatorSettings Settings;

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
            if (ConfigUrlBase == null)
                ConfigUrlBase = Page.Request.RawUrl;

            // check settings
            if (RotatorSettings.Configuration == null) {
                if (!string.IsNullOrEmpty(DbConnectionString)) {
                    RotatorSettings.Init(new AspNetConfiguration(DbConnectionString, DbOwner, DbObjectQualifier, SecurityAllowAspRole, SecurityAllowIps, SecurityAllowInvokeType));
                } else {
                    // don't have DbConnectionString
                }
            }
            

            // merge dynamic settings
            if (AllowRuntimeConfiguration) {
                if (RotatorSettings.Configuration == null) {
                    throw new ArgumentException("AllowRuntimeConfiguration is enabled but you haven't specified the name of the database connection string to use!"); ;
                }
                if (!Settings.LoadFromDB(RealId)) {
                    LoadMiniTutorialWebManage();
                }
            } else {
                if (Slides.Count == 0) {
                    ShowSlideMessage("There are no slides to display...");
                } else {
                    foreach (SlideInfo slide in Slides) {
                        slide.Settings = Settings;
                    }
                }
            }

            Settings.FrontEndRenderEngine.OnLoad(this);

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
        [Browsable(false)]
        public string OverrideId { get { return _OverrideId; } set { _OverrideId = value; } }

        // Real ID returns the control ID for Asp.NET or for DNN it return the ModuleId.ToString() - since the control ID would be id of the server control embeded in DNN module
        [Browsable(false)]
        public string RealId { get { return string.IsNullOrEmpty(OverrideId) ? this.ID : OverrideId; } }

        public string ConfigUrlBase { get; set; }

        #region Runtime Configuration

        bool _AllowRuntimeConfiguration = false;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Enable runtime configuration using the Web Interface")]
        [DisplayName("Allow Runtime Configuration")]
        public bool AllowRuntimeConfiguration { get { return _AllowRuntimeConfiguration; } set { _AllowRuntimeConfiguration = value; } }

        string _DbConnectionString = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Specify the name of a connection string from web.config to allow runtime manipulation of Rotator Settings using the Web Interface.")]
        [DisplayName("Database Connection String")]
        public string DbConnectionString { get { return _DbConnectionString; } set { _DbConnectionString = value; } }

        string _DbOwner = "dbo";
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Database Owner")]
        [DisplayName("Database Owner")]
        public string DbOwner { get { return _DbOwner; } set { _DbOwner = value; } }

        string _DbObjectQualifier = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Object qualifier (prefix for Dynamic Rotator tables)")]
        [DisplayName("Object qualifier")]
        public string DbObjectQualifier { get { return _DbObjectQualifier; } set { _DbObjectQualifier = value; } }

        string _ManageUrl = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [UrlProperty()]
        [Description("Provide the URL to where you unpacked ManageRotator.aspx that cames with your Dynamic Rotator copy.")]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DisplayName("Manage URL")]
        public string ManageUrl { get { return _ManageUrl; } set { _ManageUrl = value; } }

        string _ResourceUrl = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Specify relative folder where resources should load from using the File Browser (for example /images)")]
        [DisplayName("Resource URL")]
        public string ResourceUrl { get { return _ResourceUrl; } set { _ResourceUrl = value; } }

        string _SecurityAllowAspRole = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Specify which Asp.NET role is authorized to edit rotator configuration at runtime. Note that if other authentication methods are present they all must matched.")]
        [DisplayName("Security: Allow Asp.NET Role")]
        public string SecurityAllowAspRole { get { return _SecurityAllowAspRole; } set { _SecurityAllowAspRole = value; } }

        string _SecurityAllowIps = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("Specify a list of IP addresses separated by semicolor(;) from which users are authorized to edit rotator configuration at runtime. Note that if other authentication methods are present they all must matched.")]
        [DisplayName("Security: Allow IP Addresses")]
        public string SecurityAllowIps { get { return _SecurityAllowIps; } set { _SecurityAllowIps = value; } }

        string _SecurityAllowInvokeType = null;
        [Category("Dynamic Rotator - Runtime Configuration")]
        [Description("If you implemented your own authentication handler from IAuthenticationProxy, provide it's type here (the format is \"Fully.Qualified.ClassName,AssemblyName\"). Note that if other authentication methods are present they all must matched.")]
        [DisplayName("Security: Invoke Custom Type")]
        public string SecurityAllowInvokeType { get { return _SecurityAllowInvokeType; } set { _SecurityAllowInvokeType = value; } }


        List<IAdminAuthentication> GetSecurityLayers(string controlId)
        {
            List<IAdminAuthentication> security = new List<IAdminAuthentication>();
            if (!string.IsNullOrEmpty(SecurityAllowAspRole))
                security.Add(new AllowAspRole(SecurityAllowAspRole, controlId));
            if (!string.IsNullOrEmpty(SecurityAllowIps))
                security.Add(new AllowIps(SecurityAllowIps, controlId));
            if (!string.IsNullOrEmpty(SecurityAllowInvokeType))
                security.Add(new AllowInvokeType(SecurityAllowInvokeType, controlId));
            return security;
        }

        #endregion


        #region Custom Properties

        [Category("Layout")]
        [Description("Determines width of the flash rotator in pixels")]
        public override Unit Width { get { return Settings.Width; } set { Settings.Width = value; } }

        [Category("Layout")]
        [Description("Determines height of the flash rotator in pixels")]
        public override Unit Height { get { return Settings.Height; } set { Settings.Height = value; } }

        [Category("Dynamic Rotator")]
        [Description("Select this option if you want the slideshow to start automatically")]
        [DisplayName("Auto Start Slideshow")]
        public bool AutoStartSlideShow { get { return Settings.AutoStartSlideShow; } set { Settings.AutoStartSlideShow = value; } }

        [Category("Dynamic Rotator")]
        [Description("The Slide Buttons are used by the user to navigate to any slide")]
        [DisplayName("Show Bottom Buttons")]
        public bool ShowBottomButtons { get { return Settings.ShowBottomButtons; } set { Settings.ShowBottomButtons = value; } }

        [Category("Dynamic Rotator")]
        [Description("Use this option to determine if the user is able to control the slideshow")]
        [DisplayName("Show Play Pause Controls")]
        public bool ShowPlayPauseControls { get { return Settings.ShowPlayPauseControls; } set { Settings.ShowPlayPauseControls = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        [Description("Background of the control")]
        [DisplayName("Background Color")]
        public Color BackgroundColor { get { return Settings.BackgroundColor; } set { Settings.BackgroundColor = value; } }
        
        [Category("Dynamic Rotator")]
        [Description("Show or hide the top part with the slide title when the mouse is over a slide button.")]
        [DisplayName("Show Top Title")]
        public bool ShowTopTitle { get { return Settings.ShowTopTitle; } set { Settings.ShowTopTitle = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        [Description("Background color for the top title text")]
        [DisplayName("Top Title Background")]
        public Color TopTitleBackground { get { return Settings.TopTitleBackground; } set { Settings.TopTitleBackground = value; } }

        [Category("Dynamic Rotator")]
        [Description("Transparecy of the background for the top title text")]
        [DisplayName("Top Title Text Color")]
        public int TopTitleBgTransparency { get { return Settings.TopTitleBgTransparency; } set { Settings.TopTitleBgTransparency = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        [Description("Color of the top title text")]
        [DisplayName("Top Title Text Color")]
        public Color TopTitleTextColor { get { return Settings.TopTitleTextColor; } set { Settings.TopTitleTextColor = value; } }

        [Category("Dynamic Rotator")]
        [Description("The Timer Bar appears above the slide butttons. It's a visual indicator of Slide Duration option")]
        [DisplayName("Show Timer Bar")]
        public bool ShowTimerBar { get { return Settings.ShowTimerBar; } set { Settings.ShowTimerBar = value; } }

        [Category("Dynamic Rotator")]
        [Description("This option makes slides appear in different order each time the page is loaded")]
        [DisplayName("Random Order")]
        public bool RandomOrder { get { return Settings.RandomOrder; } set { Settings.RandomOrder = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        [Description("Customize color for slide buttons (and play/pause button too)")]
        [DisplayName("Slide Buttons Color")]
        public Color SlideButtonsColor { get { return Settings.SlideButtonsColor; } set { Settings.SlideButtonsColor = value; } }

        [TypeConverter(typeof(WebColorConverter))]
        [Category("Dynamic Rotator")]
        [Description("Use this option to determine color for play/pause symbols and for slide button numbers when the Slide Button Type is set to display numbers")]
        [DisplayName("Slide Buttons Text Color")]
        public Color SlideButtonsNumberColor { get { return Settings.SlideButtonsNumberColor; } set { Settings.SlideButtonsNumberColor = value; } }

        [Category("Dynamic Rotator")]
        [Description("This option determines how the slide buttons are rendered (they either are square and display numbers or are round buttons)")]
        [DisplayName("Slide Buttons Type")]
        public eSlideButtonsType SlideButtonsType { get { return Settings.SlideButtonsType; } set { Settings.SlideButtonsType = value; } }

        [Category("Dynamic Rotator")]
        [Description("Distance between left and bottom margins and the buttons")]
        [DisplayName("Slide Buttons X offset")]
        public int SlideButtonsXoffset { get { return Settings.SlideButtonsXoffset; } set { Settings.SlideButtonsXoffset = value; } }

        [Category("Dynamic Rotator")]
        [Description("Distance between left and bottom margins and the buttons")]
        [DisplayName("Slide Buttons Y offset")]
        public int SlideButtonsYoffset { get { return Settings.SlideButtonsYoffset; } set { Settings.SlideButtonsYoffset = value; } }

        //[Category("Dynamic Rotator")]
        //[Description("If this option is selected, the flash control is transparent so it takes the color of the HTML page")]
        //[DisplayName("Transparent Background")]
        //public bool TransparentBackground { get { return Settings.TransparentBackground; } set { Settings.TransparentBackground = value; } }

        #endregion


        #region Slides

        [DefaultValue((string)null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Dynamic Rotator - Slides")]
        [Editor(typeof(avt.DynamicFlashRotator.Net.SlideCollectionEditor), typeof(UITypeEditor))]
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

            if (!Settings.IsActivated() || Settings.IsTrialExpired()) {
                    
                SlideObjectInfo trialText = new SlideObjectInfo();
                trialText.ObjectType = eObjectType.Text;
                if (Settings.IsTrialExpired()) {
                    trialText.Text = "<font size='20px' style='font-size:20px;' color='#C77405'><font size='30px' style='font-size:30px;'><i>Dynamic Rotator .NET</i></font> Trial Expired!</font>";
                } else {
                    trialText.Text = "<font size='20px' style='font-size:20px;' color='#C77405'><font size='30px' style='font-size:30px;'><i>Dynamic Rotator .NET</i></font><br/>Use admin to Unlock 30 Day Trial or Activate for production.</font>";
                }
                trialText.Yposition = 70;
                trialText.Xposition = 240;

                SlideObjectInfo logoObj = new SlideObjectInfo();
                logoObj.ObjectType = eObjectType.Image;
                logoObj.ObjectUrl = "http://www.dnnsharp.com/Portals/0/product_logo/Dynamic-Rotator.png";
                logoObj.Yposition = 30;
                logoObj.Xposition = 20;
                logoObj.SlideFrom = eAllDirs.Left;
                logoObj.EffectAfterSlide = eEffect.Zoom;
                logoObj.TransitionDuration = 1;
                    
                SlideInfo trialSlide = new SlideInfo();
                trialSlide.SlideObjects.Add(trialText);
                trialSlide.SlideObjects.Add(logoObj);
                trialSlide.SlideUrl = "http://www.dnnsharp.com/dotnetnuke-modules/dnn-banner/flash/dynamic-rotator.aspx";
                trialSlide.ButtonCaption = "Read More...";
                trialSlide.Settings = Settings;

                Slides.Clear();
                Slides.Add(trialSlide);
            }

            Writer.WriteStartElement("ads");
            IList slides = Settings.RandomOrder ? ShuffleSlides(Slides) : Slides;
            foreach (SlideInfo slide in slides) 
                slide.ToXml(Writer);
            Writer.WriteEndElement(); // "ads";

            Writer.Close();

            return strXML.ToString();
        }

        static IList ShuffleSlides(SlideCollection slides)
        {
            List<SlideInfo> suffledSlides = new List<SlideInfo>();
            foreach (SlideInfo s in slides)
                suffledSlides.Add(s);

            Random rng = new Random();
            int n = suffledSlides.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                SlideInfo value = suffledSlides[k];
                suffledSlides[k] = suffledSlides[n];
                suffledSlides[n] = value;
            }
            return suffledSlides;
        }

        #endregion


        protected override void RenderContents(HtmlTextWriter output)
        {
            if (base.DesignMode) {
                output.Write("<div style = 'width: " + Width + "; height: " + Height + "; border: 1px solid #929292; background-color: #c2c2c2;'>DynamicRotator.NET</div>");
            } else {
                RenderFrontEnd(output);
            }
        }

        void RenderFrontEnd(HtmlTextWriter output)
        {
            Settings.FrontEndRenderEngine.Render(this, output);

            IList<IAdminAuthentication> security = GetSecurityLayers(RealId);
            if (AllowRuntimeConfiguration && RotatorSettings.Configuration.ShowManageLinks() && RotatorSettings.Configuration.HasAccess(RealId, security)) {
                
                // if (RotatorSettings.Configuration.ShowManageLinks()) { // this pretty much means it's a Asp.NET control for now

                // save data in session
                Dictionary<string, string> prvData = new Dictionary<string, string>();
                prvData["DbConnectionString"] = DbConnectionString;
                prvData["DbOwner"] = DbOwner;
                prvData["DbObjectQualifier"] = DbObjectQualifier;
                if (!string.IsNullOrEmpty(ResourceUrl))
                    prvData["ResourceUrl"] = ResourceUrl;

                prvData["SecurityAllowAspRole"] = SecurityAllowAspRole;
                prvData["SecurityAllowIps"] = SecurityAllowIps;
                prvData["SecurityAllowInvokeType"] = SecurityAllowInvokeType;

                HttpContext.Current.Session["avt.DynamicRotator." + RealId] = prvData;

                string manageUrl = Page.ResolveUrl(ManageUrl);
                manageUrl += "?controlId=" + RealId;
                //manageUrl += "&connStr=" + DbConnectionString;
                //manageUrl += "&dbOwner=" + DbOwner;
                //manageUrl += "&objQualifier=" + DbObjectQualifier;
                //if (!string.IsNullOrEmpty(ResourceUrl))
                //    manageUrl += "&resPath=" + ResourceUrl;
                manageUrl += "&rurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl);
                output.Write("<br />");

                if (!Settings.IsActivated() || Settings.IsTrialExpired()) {
                    output.Write("<a href='" + manageUrl + "' style='color: #CB2027;font-weight: bold;text-decoration: underline;'>Unlock 30 Day Trial or Activate for Production</a>");
                } else {
                    output.Write("<a href='" + manageUrl + "#tabs-main-slides' style='color: #CB2027;font-weight: bold;text-decoration: none;'>Manage Slides</a>");
                    output.Write("&nbsp;&nbsp;|&nbsp;&nbsp;");
                    output.Write("<a href='" + manageUrl + "' style='color: #CB2027;font-weight: bold;text-decoration: none;'>Rotator Settings</a>");
                }

                if (security.Count == 0) {
                    output.Write("<div style='color:red;'>WARNING! Everybody can configure this rotator, setup security using <i>SecurityAllowAspRole, SecurityAllowIps or SecurityAllowInvokeType</i> attributes of the Dynamic Rotator control.<br/>Read <a href='http://rotator.dnnsharp.com'>Security Layers for Asp.NET Control</a> articles for more information.</div>");
                }
            }
        }

        void LoadMiniTutorialWebManage()
        {
            Settings.LoadMiniTutorialWebManage();
        }



        void ShowSlideMessage(string message)
        {
            AutoStartSlideShow = false;
            ShowBottomButtons = false;
            ShowPlayPauseControls = false;

            Slides.Clear();

            SlideObjectInfo slide1Text = new SlideObjectInfo();
            slide1Text.ObjectType = eObjectType.Text;
            slide1Text.Text = "<font size='20px' style='font-size:20px;' color='#e24242'>" + message + "</font>";
            slide1Text.Yposition = 20;
            slide1Text.Xposition = 20;
            slide1Text.SlideFrom = eAllDirs.Left;

            SlideInfo slide1 = new SlideInfo();
            slide1.Settings = Settings;
            slide1.SlideObjects.Add(slide1Text);
            Slides.Add(slide1);
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
