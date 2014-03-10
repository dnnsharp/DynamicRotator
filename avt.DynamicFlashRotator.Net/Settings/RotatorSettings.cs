using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Data;
using avt.DynamicFlashRotator.Net.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Xml.Xsl;
using System.IO;
using avt.DynamicFlashRotator.Net.Services;
using System.Web;
using avt.DynamicFlashRotator.Net.Serialization;
using avt.DynamicFlashRotator.Net.RenderEngine;
using System.Globalization;

namespace avt.DynamicFlashRotator.Net.Settings
{

    public enum eRenderEngine
    {
        Flash,
        jQuery
    }


    public class RotatorSettings
    {
        //string RotatorId;

        public RotatorSettings()
        {

        }

        static IConfiguration _ConfigurationProvider = null;
        public static IConfiguration Configuration
        {
            get
            {
                lock (typeof(RotatorSettings)) {
                    return _ConfigurationProvider;
                }
            }
        }

        public static void Init(IConfiguration config)
        {
            lock (typeof(RotatorSettings)) {
                _ConfigurationProvider = config;
            }
        }

        //public void Init(string rotatorId, IConfiguration config)
        //{
        //    RotatorId = rotatorId;
        //    Config = config;
        //}

        #region Custom Properties

        Unit _Width = 950;
        public Unit Width { get { return _Width; } set { _Width = value; } }

        Unit _Height = 250;
        public Unit Height { get { return _Height; } set { _Height = value; } }

        public string Direction { get; set; }


        eRenderEngine _RenderEngine = eRenderEngine.jQuery;
        public eRenderEngine RenderEngine { get { return _RenderEngine; } set { _RenderEngine = value; } }

        string _FallbackImage = "";
        public string FallbackImage { get { return _FallbackImage; } set { _FallbackImage = value; } }

        bool _AutoStartSlideShow = true;
        public bool AutoStartSlideShow { get { return _AutoStartSlideShow; } set { _AutoStartSlideShow = value; } }

        bool _ShowBottomButtons = true;
        public bool ShowBottomButtons { get { return _ShowBottomButtons; } set { _ShowBottomButtons = value; } }

        bool _ShowPlayPauseControls = true;
        public bool ShowPlayPauseControls { get { return _ShowPlayPauseControls; } set { _ShowPlayPauseControls = value; } }

        Color _BackgroundColor = Color.Transparent;
        public Color BackgroundColor { get { return _BackgroundColor; } set { _BackgroundColor = value; } }

        bool _ShowTopTitle = true;
        public bool ShowTopTitle { get { return _ShowTopTitle; } set { _ShowTopTitle = value; } }

        Color _TopTitleBackground = Color.Black;
        public Color TopTitleBackground { get { return _TopTitleBackground; } set { _TopTitleBackground = value; } }

        int _TopTitleBgTransparency = 70;
        public int TopTitleBgTransparency { get { return _TopTitleBgTransparency; } set { _TopTitleBgTransparency = value; } }

        Color _TopTitleTextColor = Color.White;
        public Color TopTitleTextColor { get { return _TopTitleTextColor; } set { _TopTitleTextColor = value; } }

        bool _ShowTimerBar = true;
        public bool ShowTimerBar { get { return _ShowTimerBar; } set { _ShowTimerBar = value; } }

        bool _RandomOrder = false;
        public bool RandomOrder { get { return _RandomOrder; } set { _RandomOrder = value; } }

        Color _SlideButtonsColor = Color.FromArgb(0x12, 0x12, 0x12);
        public Color SlideButtonsColor { get { return _SlideButtonsColor; } set { _SlideButtonsColor = value; } }

        Color _SlideButtonsNumberColor = Color.White;
        public Color SlideButtonsNumberColor { get { return _SlideButtonsNumberColor; } set { _SlideButtonsNumberColor = value; } }

        eSlideButtonsType _SmallButtonsType = eSlideButtonsType.SquareWithNumbers;
        public eSlideButtonsType SlideButtonsType { get { return _SmallButtonsType; } set { _SmallButtonsType = value; } }

        int _SmallButtonsXoffset = 20;
        public int SlideButtonsXoffset { get { return _SmallButtonsXoffset; } set { _SmallButtonsXoffset = value; } }

        int _SmallButtonsYoffset = 35;
        public int SlideButtonsYoffset { get { return _SmallButtonsYoffset; } set { _SmallButtonsYoffset = value; } }

        //bool _TransparentBackground = false;
        //public bool TransparentBackground { get { return _TransparentBackground; } set { _TransparentBackground = value; } }

        public bool DebugMode { get { return RotatorSettings.Configuration.IsDebug(); } }

        DateTime _LastUpdate = DateTime.Now;
        public DateTime LastUpdate { get { return _LastUpdate; } set { _LastUpdate = value; } }

        #endregion


        #region Slides

        SlideCollection _Slides = new SlideCollection();
        public SlideCollection Slides { get { return _Slides; } }

        public void LoadMiniTutorialWebManage()
        {
            Width = 800;
            SlideButtonsType = eSlideButtonsType.SquareWithNumbers;
            Slides.Clear();

            // First Slide
            // --------------------------------------------------------------------------------------------

            SlideObjectInfo slide1Text = new SlideObjectInfo();
            slide1Text.ObjectType = eObjectType.Text;
            slide1Text.Text = "<font size='20px' style='font-size:20px;' color='#e24242'>This <i>Dynamic Rotator</i> has not been configured yet!<font size='30px' style='font-size:30px;'></font><br/>";
            slide1Text.Text += "<font size='14px' style='font-size:14px;' color='#C77405'>Start by locating the Manage links below the rotator...</font>";
            slide1Text.Yposition = 140;
            slide1Text.Xposition = 280;
            slide1Text.TextBackgroundColor = Color.FromArgb(0xC77405);
            slide1Text.TextBackgroundOpacity = 20;
            slide1Text.SlideFrom = eAllDirs.Left;

            SlideObjectInfo slide1Img = new SlideObjectInfo();
            slide1Img.ObjectType = eObjectType.Image;
            slide1Img.ObjectUrl = "http://www.dnnsharp.com/Portals/0/banner-tutorial/banner-tutorial-manage.png";
            slide1Img.Yposition = 30;
            slide1Img.Xposition = 20;
            slide1Img.SlideFrom = eAllDirs.Right;
            slide1Img.TransitionDuration = 1;
            slide1Img.Opacity = 40;
            slide1Img.GlowColor = Color.FromArgb(0xC77405);
            slide1Img.GlowSize = 2;

            SlideInfo slide1 = new SlideInfo();
            slide1.Settings = this;
            slide1.SlideObjects.Add(slide1Text);
            slide1.SlideObjects.Add(slide1Img);
            Slides.Add(slide1);

            // Second Slide
            // --------------------------------------------------------------------------------------------

            SlideObjectInfo slide2Text = new SlideObjectInfo();
            slide2Text.ObjectType = eObjectType.Text;
            slide2Text.Text = "<font size='20px' style='font-size:20px;' color='#e24242'>Add Slides and Content!</font><br/><br/>";
            slide2Text.Text += "<font size='14px' style='font-size:14px;' color='#C77405'>Use the Administration Console to add <br/>as many slides as you need which can contain <br/>text, images and other flash movies.</font><br /><br />";
            slide2Text.Yposition = 40;
            slide2Text.Xposition = 20;
            slide2Text.TextBackgroundColor = Color.FromArgb(0xC77405);
            slide2Text.TextBackgroundOpacity = 20;
            slide2Text.SlideFrom = eAllDirs.Left;

            SlideObjectInfo slide2Img = new SlideObjectInfo();
            slide2Img.ObjectType = eObjectType.Image;
            slide2Img.ObjectUrl = "http://www.dnnsharp.com/Portals/0/banner-tutorial/banner-tutorial-slides.png";
            slide2Img.Yposition = 10;
            slide2Img.Xposition = 300;
            slide2Img.SlideFrom = eAllDirs.Right;
            slide2Img.TransitionDuration = 1;
            slide2Img.Opacity = 40;
            slide2Img.GlowColor = Color.FromArgb(0xC77405);
            slide2Img.GlowSize = 2;

            SlideInfo slide2 = new SlideInfo();
            slide2.Settings = this;
            slide2.SlideObjects.Add(slide2Text);
            slide2.SlideObjects.Add(slide2Img);

            Slides.Add(slide2);


            // Third Slide
            // --------------------------------------------------------------------------------------------

            SlideObjectInfo slide3Text = new SlideObjectInfo();
            slide3Text.ObjectType = eObjectType.Text;
            slide3Text.Text = "<font size='20px' style='font-size:20px;' color='#C77405'><font size='30px' style='font-size:30px;'><i>Dynamic Rotator .NET</i></font> from Avatar Software</font><br/>";
            slide3Text.Text += "<font color='#525252;' size='14px' style='font-size:14px;'><i>Explore thousands of possibilities easily achieveable with our <br />simple and powerful Administration Console...</i></font>";
            slide3Text.Yposition = 60;
            slide3Text.Xposition = 50;

            SlideObjectInfo slide3Img = new SlideObjectInfo();
            slide3Img.ObjectType = eObjectType.Image;
            slide3Img.ObjectUrl = "http://www.dnnsharp.com/Portals/0/product_logo/Dynamic-Rotator.png";
            slide3Img.Yposition = 30;
            slide3Img.Xposition = 550;
            slide3Img.SlideFrom = eAllDirs.Left;
            slide3Img.EffectAfterSlide = eEffect.Zoom;
            slide3Img.TransitionDuration = 1;


            SlideInfo slide3 = new SlideInfo();
            slide3.Settings = this;
            slide3.SlideObjects.Add(slide3Text);
            slide3.SlideObjects.Add(slide3Img);
            slide3.SlideUrl = "http://www.dnnsharp.com/dotnetnuke-modules/dnn-banner/flash/dynamic-rotator.aspx";
            slide3.ButtonCaption = "Visit Dynamic Rotator .NET Homepage";

            Slides.Add(slide3);
        }

        #endregion

        public IRenderEngine FrontEndRenderEngine
        {
            get
            {
                return new jQueryEngine();
                //switch (RenderEngine) {
                //    case eRenderEngine.jQuery:
                //        return new jQueryEngine();
                //    default:
                //        return new FlashEngine();
                //}
            }
        }

        public bool LoadFromDB(string RotatorId) // string connStr, string dbOwner, string objQualifier)
        {
            DataProvider.Instance().Init(Configuration);
            bool hasValues = false;
            using (IDataReader dr = DataProvider.Instance().GetSettings(RotatorId)) {

                while (dr.Read()) {

                    hasValues = true;

                    string val = "";
                    try { val = dr["SettingValue"].ToString(); } catch { }

                    switch (dr["SettingName"].ToString()) {
                        case "Width":
                            try {
                                Width = Convert.ToInt32(val);
                            } catch { }
                            break;
                        case "Height":
                            try {
                                Height = Convert.ToInt32(val);
                            } catch { }
                            break;
                        case "Direction":
                            try {
                                Direction = val;
                            } catch { }
                            break;
                        //case "RenderEngine":
                        //    try {
                        //        RenderEngine = (eRenderEngine)Enum.Parse(typeof(eRenderEngine), val, true);
                        //    } catch { RenderEngine = eRenderEngine.Flash; }
                        //    break;
                        case "FallbackImage":
                            FallbackImage = val;
                            break;
                        case "AutoStartSlideShow":
                            AutoStartSlideShow = val == "true";
                            break;
                        case "ShowBottomButtons":
                            ShowBottomButtons = val == "true";
                            break;
                        case "ShowPlayPauseControls":
                            ShowPlayPauseControls = val == "true";
                            break;
                        case "BackgroundColor":
                            try {
                                BackgroundColor = System.Drawing.Color.FromArgb(Convert.ToInt32(val.Replace("#", "0x"), 16));
                            } catch { }
                            break;
                        case "ShowTopTitle":
                            ShowTopTitle = val == "true";
                            break;
                        case "TopTitleBackground":
                            try {
                                TopTitleBackground = System.Drawing.Color.FromArgb(Convert.ToInt32(val.Replace("#", "0x"), 16));
                            } catch { }
                            break;
                        case "TopTitleBgTransparency":
                            try {
                                TopTitleBgTransparency = Convert.ToInt32(val);
                            } catch { }
                            break;
                        case "TopTitleTextColor":
                            try {
                                TopTitleTextColor = System.Drawing.Color.FromArgb(Convert.ToInt32(val.Replace("#", "0x"), 16));
                            } catch { }
                            break;
                        case "ShowTimerBar":
                            ShowTimerBar = val == "true";
                            break;
                        case "RandomOrder":
                            RandomOrder = val == "true";
                            break;
                        case "SlideButtonsColor":
                            try {
                                SlideButtonsColor = System.Drawing.Color.FromArgb(Convert.ToInt32(val.Replace("#", "0x"), 16));
                            } catch { }
                            break;
                        case "SlideButtonsNumberColor":
                            try {
                                SlideButtonsNumberColor = System.Drawing.Color.FromArgb(Convert.ToInt32(val.Replace("#", "0x"), 16));
                            } catch { }
                            break;
                        case "SlideButtonsType":
                            try {
                                SlideButtonsType = (eSlideButtonsType)Enum.Parse(typeof(eSlideButtonsType), val, true);
                            } catch { }
                            break;
                        case "SlideButtonsXoffset":
                            try {
                                SlideButtonsXoffset = Convert.ToInt32(val);
                            } catch { }
                            break;
                        case "SlideButtonsYoffset":
                            try {
                                SlideButtonsYoffset = Convert.ToInt32(val);
                            } catch { }
                            break;
                        //case "TransparentBackground":
                        //    TransparentBackground = val == "true";
                        //    break;
                        //case "_DebugMode":
                        //    _DebugMode = val == "true";
                        //    break;
                        case "LastUpdate":
                            try {
                                LastUpdate = DateTime.Parse(val, new CultureInfo("en-US").DateTimeFormat);
                            } catch { LastUpdate = DateTime.Now; }
                            break;
                    }
                }
                dr.Close();
            }

            // load slides
            _Slides = new SlideCollection();
            using (IDataReader dr = DataProvider.Instance().GetSlides(RotatorId)) {
                while (dr.Read()) {
                    SlideInfo slide = new SlideInfo();
                    slide.Load(dr);
                    slide.Settings = this;
                    Slides.Add(slide);
                }
                dr.Close();
            }

            return hasValues;
        }


        public static void LoadFromPortableXml(XmlNode rootNode, string controlId)
        {
            DataProvider.Instance().Init(RotatorSettings.Configuration);

            // load settings
            try { DataProvider.Instance().UpdateSetting(controlId, "Width", rootNode["Width"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "Height", rootNode["Height"].InnerText); } catch { }
            //try { DataProvider.Instance().UpdateSetting(controlId, "RenderEngine", rootNode["RenderEngine"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "FallbackImage", rootNode["FallbackImage"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "AutoStartSlideShow", rootNode["AutoStartSlideShow"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "ShowBottomButtons", rootNode["ShowBottomButtons"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "ShowPlayPauseControls", rootNode["ShowPlayPauseControls"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "BackgroundColor", rootNode["BackgroundColor"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "ShowTopTitle", rootNode["ShowTopTitle"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "TopTitleBackground", rootNode["TopTitleBackground"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "TopTitleBgTransparency", rootNode["TopTitleBgTransparency"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "TopTitleTextColor", rootNode["TopTitleTextColor"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "ShowTimerBar", rootNode["ShowTimerBar"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "RandomOrder", rootNode["RandomOrder"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "SlideButtonsColor", rootNode["SlideButtonsColor"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "SlideButtonsNumberColor", rootNode["SlideButtonsNumberColor"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "SlideButtonsType", rootNode["SlideButtonsType"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "SlideButtonsXoffset", rootNode["SlideButtonsXoffset"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "SlideButtonsYoffset", rootNode["SlideButtonsYoffset"].InnerText); } catch { }
            //try { DataProvider.Instance().UpdateSetting(controlId, "TransparentBackground", rootNode["TransparentBackground"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "LastUpdate", rootNode["LastUpdate"].InnerText); } catch { }

            // load slides
            DataProvider.Instance().RemoveSlides(controlId);
            foreach (XmlElement xmlSlide in rootNode["Slides"].SelectNodes("Slide")) {
                SlideInfo slide = new SlideInfo();
                slide.LoadFromPortableXml(xmlSlide, controlId);
            }

            // load slide objects
        }

        public void SaveToPortableXml(XmlWriter Writer, string controlId)
        {
            Writer.WriteStartElement("RotatorSettings");

            // save settings
            Writer.WriteElementString("Width", Width.Value.ToString());
            Writer.WriteElementString("Height", Height.Value.ToString());
            //Writer.WriteElementString("RenderEngine", RenderEngine.ToString());
            Writer.WriteElementString("FallbackImage", FallbackImage);
            Writer.WriteElementString("AutoStartSlideShow", AutoStartSlideShow ? "true" : "false");
            Writer.WriteElementString("ShowBottomButtons", ShowBottomButtons ? "true" : "false");
            Writer.WriteElementString("ShowPlayPauseControls", ShowPlayPauseControls ? "true" : "false");
            Writer.WriteElementString("BackgroundColor", ColorExt.ColorToHexString(BackgroundColor));
            Writer.WriteElementString("ShowTopTitle", ShowTopTitle ? "true" : "false");
            Writer.WriteElementString("TopTitleBackground", ColorExt.ColorToHexString(TopTitleBackground));
            Writer.WriteElementString("TopTitleBgTransparency", TopTitleBgTransparency.ToString());
            Writer.WriteElementString("TopTitleTextColor", ColorExt.ColorToHexString(TopTitleTextColor));
            Writer.WriteElementString("ShowTimerBar", ShowTimerBar ? "true" : "false");
            Writer.WriteElementString("RandomOrder", RandomOrder ? "true" : "false");
            Writer.WriteElementString("SlideButtonsColor", ColorExt.ColorToHexString(SlideButtonsColor));
            Writer.WriteElementString("SlideButtonsNumberColor", ColorExt.ColorToHexString(SlideButtonsNumberColor));
            Writer.WriteElementString("SlideButtonsType", ((int)SlideButtonsType).ToString());
            Writer.WriteElementString("SlideButtonsXoffset", SlideButtonsXoffset.ToString());
            Writer.WriteElementString("SlideButtonsYoffset", SlideButtonsYoffset.ToString());
            //Writer.WriteElementString("TransparentBackground", TransparentBackground ? "true" : "false");

            // save slides
            Writer.WriteStartElement("Slides");
            foreach (SlideInfo slide in Slides) {
                slide.SaveToPortableXml(Writer, controlId);
            }
            Writer.WriteEndElement(); // Slides

            Writer.WriteEndElement(); // RotatorSettings
        }


        public string SlidesToDesignerJson()
        {
            StringBuilder sbJson = new StringBuilder();

            sbJson.Append("[");
            foreach (SlideInfo slide in Slides) {
                sbJson.Append(slide.ToDesignerJson());
                sbJson.Append(",");
            }
            if (sbJson[sbJson.Length - 1] == ',') {
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");

            return sbJson.ToString();
        }

        //public string ToXml()
        //{
        //    StringBuilder strXML = new StringBuilder();
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.Indent = true;
        //    settings.OmitXmlDeclaration = false;
        //    settings.Encoding = Encoding.UTF8;
        //    XmlWriter Writer = XmlWriter.Create(strXML, settings);

        //    Writer.WriteStartElement("settings");
        //    Writer.WriteElementString("stageWidth", Width.Value.ToString());
        //    Writer.WriteElementString("stageHeight", Height.Value.ToString());
        //    //Writer.WriteElementString("renderEngine", RenderEngine.ToString());
        //    Writer.WriteElementString("startSlideShow", AutoStartSlideShow ? "yes" : "no");
        //    Writer.WriteElementString("showBottomButtons", ShowBottomButtons ? "yes" : "no");
        //    Writer.WriteElementString("showPlayPauseControls", ShowPlayPauseControls ? "yes" : "no");
        //    Writer.WriteElementString("backgroundColor", ColorExt.ColorToHexString(BackgroundColor).Replace("#", "0x"));
        //    Writer.WriteElementString("showTopTitle", ShowTopTitle ? "yes" : "no");
        //    Writer.WriteElementString("topTitleBackground", ColorExt.ColorToHexString(TopTitleBackground).Replace("#", "0x"));
        //    Writer.WriteElementString("topTitleBgTransparency", TopTitleBgTransparency.ToString());
        //    Writer.WriteElementString("topTitleTextColor", ColorExt.ColorToHexString(TopTitleTextColor).Replace("#", "0x"));
        //    Writer.WriteElementString("showTimerBar", ShowTimerBar ? "yes" : "no");
        //    //Writer.WriteElementString("randomOrder", RandomOrder ? "yes" : "no");
        //    Writer.WriteElementString("smallButtonsColor", ColorExt.ColorToHexString(SlideButtonsColor).Replace("#", "0x"));
        //    Writer.WriteElementString("smallButtonsNumberColor", ColorExt.ColorToHexString(SlideButtonsNumberColor).Replace("#", "0x"));
        //    Writer.WriteElementString("smallButtonsType", ((int)SlideButtonsType).ToString());
        //    Writer.WriteElementString("smallButtonsXoffset", SlideButtonsXoffset.ToString());
        //    Writer.WriteElementString("smallButtonsYoffset", SlideButtonsYoffset.ToString());
        //    //Writer.WriteElementString("transparentBackground", TransparentBackground ? "yes" : "no");

        //    Writer.WriteElementString("DebugMode", DebugMode ? "on" : "off");
        //    Writer.WriteEndElement(); // "settings";

        //    Writer.Close();

        //    return strXML.ToString();
        //}


        public string ToJson()
        {
            JsonResponseWriter rw = new JsonResponseWriter();

            rw.BeginObject("settings");
            rw.WriteProperty("stageWidth", Width.Value);
            rw.WriteProperty("stageHeight", Height.Value);
            rw.WriteProperty("direction", Direction);
            //rw.WriteProperty("renderEngine", RenderEngine.ToString());
            rw.WriteProperty("startSlideShow", AutoStartSlideShow ? "yes" : "no");
            rw.WriteProperty("showBottomButtons", ShowBottomButtons ? "yes" : "no");
            rw.WriteProperty("showPlayPauseControls", ShowPlayPauseControls ? "yes" : "no");
            rw.WriteProperty("backgroundColor", ColorExt.ColorToHexString(BackgroundColor));
            rw.WriteProperty("showTopTitle", ShowTopTitle ? "yes" : "no");
            rw.WriteProperty("topTitleBackground", ColorExt.ColorToHexString(TopTitleBackground));
            rw.WriteProperty("topTitleBgTransparency", TopTitleBgTransparency.ToString());
            rw.WriteProperty("topTitleTextColor", ColorExt.ColorToHexString(TopTitleTextColor));
            rw.WriteProperty("showTimerBar", ShowTimerBar ? "yes" : "no");
            rw.WriteProperty("randomOrder", RandomOrder ? "yes" : "no");
            rw.WriteProperty("smallButtonsColor", ColorExt.ColorToHexString(SlideButtonsColor));
            rw.WriteProperty("smallButtonsNumberColor", ColorExt.ColorToHexString(SlideButtonsNumberColor));
            rw.WriteProperty("smallButtonsType", (int)SlideButtonsType);
            rw.WriteProperty("smallButtonsXoffset", SlideButtonsXoffset);
            rw.WriteProperty("smallButtonsYoffset", SlideButtonsYoffset);
            //rw.WriteProperty("transparentBackground", TransparentBackground ? "yes" : "no");

            rw.WriteProperty("DebugMode", DebugMode ? "on" : "off");
            rw.BeginArray("slides");
            foreach (SlideInfo slide in Slides) {
                rw.WritePropertyLiteral("", slide.ToDesignerJson());
            }
            rw.EndArray(); // "slides"

            rw.EndObject(); // "settings";

            return rw.ToString();
        }

        public static string JsonEncode(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";

            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);

            // sb.Append('"');
            char[] charArray = s.ToCharArray();
            for (int i = 0; i < charArray.Length; i++) {
                char c = charArray[i];
                if (c == '"') {
                    sb.Append("\\\"");
                } else if (c == '\\') {
                    sb.Append("\\\\");
                } else if (c == '/') {
                    sb.Append("\\/");
                } else if (c == '\b') {
                    sb.Append("\\b");
                } else if (c == '\f') {
                    sb.Append("\\f");
                } else if (c == '\n') {
                    sb.Append("\\n");
                } else if (c == '\r') {
                    sb.Append("\\r");
                } else if (c == '\t') {
                    sb.Append("\\t");
                } else {
                    int codepoint = Convert.ToInt32(c);
                    if ((codepoint >= 32) && (codepoint <= 126)) {
                        sb.Append(c);
                    } else {
                        sb.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                    }
                }
            }
            //sb.Append('"');
            return sb.ToString();
        }


        #region RegCore

        //public static bool IsAdmin { get { return DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo().IsInRole(DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings().AdministratorRoleName); } }

        static bool _IsAdmin = true; // TODO:
        public static bool IsAdmin { get { return _IsAdmin; } set { _IsAdmin = value; } }
        public static string RegCoreServer { get { return "http://www.dnnsharp.com/DesktopModules/RegCore/"; } }
        public static string ProductName { get { return "Dynamic Rotator .NET"; } }
        public static string ProductCode { get { return "ADROT"; } }
        public static string ProductKey { get { return "<RSAKeyValue><Modulus>xjeQuuf4zC2gbVI0ZJJnKagUgmeFH8klB27NK80DhxcBaJkw/naUJl1N9195kxUyznRf8uwSkjt9sZfmGQplu3gYz+X3GFCcVhABZsXyO+vNAdkyU+F6KkX5wL4/AAfmpKbqhsYt/z3abPInaRWG1Mk6uoUSv0bkAXsvLWOjUZs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"; } }
        public static string Version { get { return "1.3"; } }
        public static string Build
        {
            get
            {
                var version = System.Reflection.Assembly.GetAssembly(typeof(RotatorSettings)).GetName().Version;
                return version.ToString().Substring(0, version.ToString().LastIndexOf('.'));
            }
        }

        static public string DocSrv = RegCoreServer + "/Api.aspx?cmd=doc&product=" + ProductCode + "&version=" + Version;
        static public string BuyLink = RegCoreServer + "/Api.aspx?cmd=buy&product=" + ProductCode + "&version=" + Version;

        //List<ListItem> _Hosts = new List<ListItem>();
        //public List<ListItem> Hosts {
        //    get { return _Hosts; }
        //    set { _Hosts = value; }
        //}

        #endregion

    }
}
