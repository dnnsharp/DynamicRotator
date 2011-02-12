using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Data;
using avt.AllinOneRotator.Net.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Xml.Xsl;
using System.IO;

namespace avt.AllinOneRotator.Net.Settings
{
    public class RotatorSettings
    {
        string RotatorId;

        public RotatorSettings()
        {
            
        }

        public void Init(string rotatorId)
        {
            RotatorId = rotatorId;
        }

        #region Custom Properties

        Unit _Width = 950;
        public Unit Width { get { return _Width; } set { _Width = value; } }

        Unit _Height = 250;
        public Unit Height { get { return _Height; } set { _Height = value; } }

        bool _AutoStartSlideShow = true;
        public bool AutoStartSlideShow { get { return _AutoStartSlideShow; } set { _AutoStartSlideShow = value; } }

        bool _UseRoundCornersMask = true;
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

        #endregion


        #region Slides

        SlideCollection _Slides = new SlideCollection();
        public SlideCollection Slides { get { return _Slides; } }

        #endregion


        public void LoadFromDB(string connStr, string dbOwner, string objQualifier)
        {
            if (connStr.IndexOf(';') == -1) {
                // this is a name from web.config connnections
                if (ConfigurationManager.ConnectionStrings[connStr] == null) {
                    throw new ArgumentException("Runtime Configuration is enabled but the connection string name is invalid!");
                }
                connStr = ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }

            DataProvider.Instance().ConnStr = connStr;
            DataProvider.Instance().DbOwner = dbOwner;
            DataProvider.Instance().ObjQualifier = objQualifier;
            DataProvider.Instance().Init();

            using (IDataReader dr = DataProvider.Instance().GetSettings(RotatorId)) {
                while (dr.Read()) {

                    string val = "";
                    try { val = dr["SettingValue"].ToString(); } catch { }

                    switch (dr["SettingName"].ToString()) {
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
                    }
                }
                dr.Close();
            }

            // load slides
            using (IDataReader dr = DataProvider.Instance().GetSlides(RotatorId)) {
                while (dr.Read()) {
                    SlideInfo slide = new SlideInfo();
                    slide.Load(dr);
                    Slides.Add(slide);
                }
                dr.Close();
            }
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


        public static string JsonEncode(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";

            char c;
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            string t;

            //sb.Append('"');
            for (i = 0; i < len; i += 1) {
                c = s[i];
                if ((c == '\\') || (c == '"') || (c == '>') || (c == '\'')) {
                    sb.Append('\\');
                    sb.Append(c);
                } else if (c == '\b')
                    sb.Append("\\b");
                else if (c == '\t')
                    sb.Append("\\t");
                else if (c == '\n')
                    sb.Append("\\n");
                else if (c == '\f')
                    sb.Append("\\f");
                else if (c == '\r')
                    sb.Append("\\r");
                else {
                    if (c < ' ') {
                        //t = "000" + Integer.toHexString(c); 
                        string tmp = new string(c, 1);
                        t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    } else {
                        sb.Append(c);
                    }
                }
            }
            //sb.Append('"');
            return sb.ToString();
        }

    }
}
