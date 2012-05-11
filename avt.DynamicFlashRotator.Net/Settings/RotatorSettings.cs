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
using avt.DynamicFlashRotator.Net.RegCore.Storage;
using avt.DynamicFlashRotator.Net.RegCore;

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
        public static IConfiguration Configuration {
            get {
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

        eRenderEngine _RenderEngine = eRenderEngine.Flash;
        public eRenderEngine RenderEngine { get { return _RenderEngine; } set { _RenderEngine = value; } }

        string _FallbackImage = "";
        public string FallbackImage { get { return _FallbackImage; } set { _FallbackImage = value; } }

        bool _AutoStartSlideShow = true;
        public bool AutoStartSlideShow { get { return _AutoStartSlideShow; } set { _AutoStartSlideShow = value; } }

        bool _UseRoundCornersMask = false;
        public bool UseRoundCornersMask { get { return _UseRoundCornersMask; } set { _UseRoundCornersMask = value; } }

        Color _RoundCornerMaskColor = Color.White;
        public Color RoundCornerMaskColor { get { return _RoundCornerMaskColor; } set { _RoundCornerMaskColor = value; } }

        bool _ShowBottomButtons = true;
        public bool ShowBottomButtons { get { return _ShowBottomButtons; } set { _ShowBottomButtons = value; } }

        bool _ShowPlayPauseControls = true;
        public bool ShowPlayPauseControls { get { return _ShowPlayPauseControls; } set { _ShowPlayPauseControls = value; } }

        Color _FadeColor = Color.White;
        public Color FadeColor { get { return _FadeColor; } set { _FadeColor = value; } }

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

        bool _TransparentBackground = false;
        public bool TransparentBackground { get { return _TransparentBackground; } set { _TransparentBackground = value; } }

        public bool DebugMode { get { return RotatorSettings.Configuration.IsDebug(); } }

        DateTime _LastUpdate = DateTime.Now;
        public DateTime LastUpdate { get { return _LastUpdate; } set { _LastUpdate = value; } }

        #endregion


        #region Slides

        SlideCollection _Slides = new SlideCollection();
        public SlideCollection Slides { get { return _Slides; } }

        #endregion

        public IRenderEngine FrontEndRenderEngine {
            get {
                switch (RenderEngine) {
                    case eRenderEngine.jQuery:
                        return new jQueryEngine();
                    default:
                        return new FlashEngine();
                }
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
                        case "RenderEngine":
                            try {
                                RenderEngine = (eRenderEngine)Enum.Parse(typeof(eRenderEngine), val, true);
                            } catch { RenderEngine = eRenderEngine.Flash; }
                            break;
                        case "FallbackImage":
                            FallbackImage = val;
                            break;
                        case "AutoStartSlideShow":
                            AutoStartSlideShow = val == "true";
                            break;
                        case "UseRoundCornersMask":
                            UseRoundCornersMask = val == "true";
                            break;
                        case "RoundCornerMaskColor":
                            try {
                                RoundCornerMaskColor = System.Drawing.Color.FromArgb(Convert.ToInt32(val.Replace("#", "0x"), 16));
                            } catch { }
                            break;
                        case "ShowBottomButtons":
                            ShowBottomButtons = val == "true";
                            break;
                        case "ShowPlayPauseControls":
                            ShowPlayPauseControls = val == "true";
                            break;
                        case "FadeColor":
                            try {
                                FadeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(val.Replace("#", "0x"), 16));
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
                        case "TransparentBackground":
                            TransparentBackground = val == "true";
                            break;
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
            try { DataProvider.Instance().UpdateSetting(controlId, "RenderEngine", rootNode["RenderEngine"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "FallbackImage", rootNode["FallbackImage"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "AutoStartSlideShow", rootNode["AutoStartSlideShow"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "UseRoundCornersMask", rootNode["UseRoundCornersMask"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "RoundCornerMaskColor", rootNode["RoundCornerMaskColor"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "ShowBottomButtons", rootNode["ShowBottomButtons"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "ShowPlayPauseControls", rootNode["ShowPlayPauseControls"].InnerText); } catch { }
            try { DataProvider.Instance().UpdateSetting(controlId, "FadeColor", rootNode["FadeColor"].InnerText); } catch { }
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
            try { DataProvider.Instance().UpdateSetting(controlId, "TransparentBackground", rootNode["TransparentBackground"].InnerText); } catch { }
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
            Writer.WriteElementString("RenderEngine", RenderEngine.ToString());
            Writer.WriteElementString("FallbackImage", FallbackImage);
            Writer.WriteElementString("AutoStartSlideShow", AutoStartSlideShow ? "true" : "false");
            Writer.WriteElementString("UseRoundCornersMask", UseRoundCornersMask ? "true" : "false");
            Writer.WriteElementString("RoundCornerMaskColor", ColorExt.ColorToHexString(RoundCornerMaskColor));
            Writer.WriteElementString("ShowBottomButtons", ShowBottomButtons ? "true" : "false");
            Writer.WriteElementString("ShowPlayPauseControls", ShowPlayPauseControls ? "true" : "false");
            Writer.WriteElementString("FadeColor", ColorExt.ColorToHexString(FadeColor));
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
            Writer.WriteElementString("TransparentBackground", TransparentBackground ? "true" : "false");

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

        public string ToXml()
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
            //Writer.WriteElementString("renderEngine", RenderEngine.ToString());
            Writer.WriteElementString("startSlideShow", AutoStartSlideShow ? "yes" : "no");
            Writer.WriteElementString("useRoundCornersMask", UseRoundCornersMask ? "yes" : "no");
            Writer.WriteElementString("roundCornerMaskColor", ColorExt.ColorToHexString(RoundCornerMaskColor).Replace("#","0x"));
            Writer.WriteElementString("showBottomButtons", ShowBottomButtons ? "yes" : "no");
            Writer.WriteElementString("showPlayPauseControls", ShowPlayPauseControls ? "yes" : "no");
            Writer.WriteElementString("fadeColor", ColorExt.ColorToHexString(FadeColor).Replace("#", "0x"));
            Writer.WriteElementString("showTopTitle", ShowTopTitle ? "yes" : "no");
            Writer.WriteElementString("topTitleBackground", ColorExt.ColorToHexString(TopTitleBackground).Replace("#", "0x"));
            Writer.WriteElementString("topTitleBgTransparency", TopTitleBgTransparency.ToString());
            Writer.WriteElementString("topTitleTextColor", ColorExt.ColorToHexString(TopTitleTextColor).Replace("#", "0x"));
            Writer.WriteElementString("showTimerBar", ShowTimerBar ? "yes" : "no");
            //Writer.WriteElementString("randomOrder", RandomOrder ? "yes" : "no");
            Writer.WriteElementString("smallButtonsColor", ColorExt.ColorToHexString(SlideButtonsColor).Replace("#", "0x"));
            Writer.WriteElementString("smallButtonsNumberColor", ColorExt.ColorToHexString(SlideButtonsNumberColor).Replace("#", "0x"));
            Writer.WriteElementString("smallButtonsType", ((int)SlideButtonsType).ToString());
            Writer.WriteElementString("smallButtonsXoffset", SlideButtonsXoffset.ToString());
            Writer.WriteElementString("smallButtonsYoffset", SlideButtonsYoffset.ToString());
            Writer.WriteElementString("transparentBackground", TransparentBackground ? "yes" : "no");

            Writer.WriteElementString("DebugMode", DebugMode ? "on" : "off");
            Writer.WriteEndElement(); // "settings";

            Writer.Close();

            return strXML.ToString();
        }


        public string ToJson()
        {
            JsonResponseWriter rw = new JsonResponseWriter();

            rw.BeginObject("settings");
            rw.WriteProperty("stageWidth", Width.Value);
            rw.WriteProperty("stageHeight", Height.Value);
            //rw.WriteProperty("renderEngine", RenderEngine.ToString());
            rw.WriteProperty("startSlideShow", AutoStartSlideShow ? "yes" : "no");
            rw.WriteProperty("useRoundCornersMask", UseRoundCornersMask ? "yes" : "no");
            rw.WriteProperty("roundCornerMaskColor", ColorExt.ColorToHexString(RoundCornerMaskColor));
            rw.WriteProperty("showBottomButtons", ShowBottomButtons ? "yes" : "no");
            rw.WriteProperty("showPlayPauseControls", ShowPlayPauseControls ? "yes" : "no");
            rw.WriteProperty("fadeColor", ColorExt.ColorToHexString(FadeColor));
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
            rw.WriteProperty("transparentBackground", TransparentBackground ? "yes" : "no");

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
        public static string Version { get { return "1.2.0"; } }
        public static string Build { get { return Version + "-rc2"; } }

        static public string DocSrv = RegCoreServer + "/Api.aspx?cmd=doc&product=" + ProductCode + "&version=" + Version;
        static public string BuyLink = RegCoreServer + "/Api.aspx?cmd=buy&product=" + ProductCode + "&version=" + Version;

        List<ListItem> _Hosts = new List<ListItem>();
        public List<ListItem> Hosts {
            get { return _Hosts; }
            set { _Hosts = value; }
        }

        internal IActivationDataStore GetActivationSrc()
        {
            return new DsLicFile();
        }

        public static IRegCoreClient RegCore {
            get {
                return RegCoreClient.Get(new RegCoreServer(RegCoreServer).ApiScript, ProductCode, new DsLicFile(), false);
            }
        }

        public bool IsActivated()
        {
            return RegCore.IsActivated(ProductCode, Version, HttpContext.Current.Request.Url.Host);
        }

        public bool IsTrial()
        {
            return RegCore.IsTrial(ProductCode, Version, HttpContext.Current.Request.Url.Host);
        }

        public bool IsTrialExpired()
        {
            return RegCore.IsTrialExpired(ProductCode, Version, HttpContext.Current.Request.Url.Host);
        }

        public void Activate(string regCode, string host, string actCode)
        {
            if (string.IsNullOrEmpty(actCode)) {
                RegCore.Activate(regCode, ProductCode, Version, host, ProductKey);
            } else {
                RegCore.Activate(regCode, ProductCode, Version, host, ProductKey, actCode);
            }
        }

        #endregion

    }
}
