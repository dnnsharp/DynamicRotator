using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing.Design;
using System.Xml;
using System.Data;
using avt.AllinOneRotator.Net.Settings;
using avt.AllinOneRotator.Net.Services;
using avt.AllinOneRotator.Net.Data;

namespace avt.AllinOneRotator.Net
{
    public enum eMoveType {
        Strong, 
        Bounce,
        Elastic
    }

    public enum eEasing {
        Out,
        In,
        InOut
    }

    public enum eAllDirs
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public enum eHorizontadDirs {
        Left,
        Right
    }

    //public enum eVerticalAlgin {
    //    Top,
    //    Middle,
    //    Bottom
    //}

    public enum eLinkTarget {
        Self,
        New
    }


    public class SlideInfo
    {
        public SlideInfo()
        {
        }


        int _Id = -1;
        public int Id { get { return _Id; } set { _Id = value; } }

        string _ControlId;
        public string ControlId { get { return _ControlId; } set { _ControlId = value; } }

        int _ViewOrder = 0;
        public int ViewOrder { get { return _ViewOrder; } set { _ViewOrder = value; } }
        

        #region General

        string _Title = "New Slide Title";
        [Category("General")]
        public string Title { get { return _Title; } set { _Title = value; } }

        int _DurationSeconds = 10;
        [Category("General")]
        public int DurationSeconds { get { return _DurationSeconds; } set { _DurationSeconds = value; } }

        string _TextContent = "Content of new slide, supports <i>HTML</i>.";
        [Category("General")]
        public string TextContent { get { return _TextContent; } set { _TextContent = value; } }


        Color _BackgroundGradientFrom = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Slide.Background")]
        public Color BackgroundGradientFrom { get { return _BackgroundGradientFrom; } set { _BackgroundGradientFrom = value; } }

        Color _BackgroundGradientTo = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Slide.Background")]
        public Color BackgroundGradientTo { get { return _BackgroundGradientTo; } set { _BackgroundGradientTo = value; } }

        #endregion


        //#region Text

        //Color _GlowColor = Color.White;
        //[TypeConverter(typeof(WebColorConverter))]
        //[Category("Text.Effects")]
        //public Color GlowColor { get { return _GlowColor; } set { _GlowColor = value; } }

        //int _GlowSize = 10;
        //[Category("Text.Effects")]
        //public int GlowSize { get { return _GlowSize; } set { _GlowSize = value; } }

        //int _GlowStrength = 3;
        //[Category("Text.Effects")]
        //public int GlowStrength { get { return _GlowStrength; } set { _GlowStrength = value; } }

        //int _TextTransparency = 10;
        //[Category("Text.Effects")]
        //public int TextTransparency { get { return _TextTransparency; } set { _TextTransparency = value; } }

        
        //eMoveType _MoveType = eMoveType.Strong;
        //[Category("Text.Transition")]
        //public eMoveType MoveType { get { return _MoveType; } set { _MoveType = value; } }

        //eEasing _EasingType = eEasing.Out;
        //[Category("Text.Transition")]
        //public eEasing EasingType { get { return _EasingType; } set { _EasingType = value; } }

        //int _TransitionDuration = 2;
        //[Category("Text.Transition")]
        //public int TransitionDuration { get { return _TransitionDuration; } set { _TransitionDuration = value; } }

        //bool _JustFade = false;
        //[Category("Text.Transition")]
        //public bool JustFade { get { return _JustFade; } set { _JustFade = value; } }

        //eHorizontadDirs _AppearFrom = eHorizontadDirs.Right;
        //[Category("Text.Transition")]
        //public eHorizontadDirs AppearFrom { get { return _AppearFrom; } set { _AppearFrom = value; } }


        //int _FinalXposition = 15;
        //[Category("Text.Position")]
        //public int FinalXposition { get { return _FinalXposition; } set { _FinalXposition = value; } }

        //eVerticalAlgin _VerticalAlign = eVerticalAlgin.Middle;
        //[Category("Text.Position")]
        //public eVerticalAlgin VerticalAlign { get { return _VerticalAlign; } set { _VerticalAlign = value; } }

        //int _TextPadding = 5;
        //[Category("Text.Position")]
        //public int TextPadding { get { return _TextPadding; } set { _TextPadding = value; } }


        //bool _UseBackground = false;
        //[Category("Text.Background")]
        //public bool UseBackground { get { return _UseBackground; } set { _UseBackground = value; } }

        //Color _TextBackgroundColor = Color.White;
        //[TypeConverter(typeof(WebColorConverter))]
        //[Category("Text.Background")]
        //public Color TextBackgroundColor { get { return _TextBackgroundColor; } set { _TextBackgroundColor = value; } }

        //int _TextBackgroundTransparency = 70;
        //[Category("Text.Background")]
        //public int TextBackgroundTransparency { get { return _TextBackgroundTransparency; } set { _TextBackgroundTransparency = value; } }

        //#endregion


        #region Objects

        SlideObjectCollection _SlideObjects = new SlideObjectCollection();

        [DefaultValue("")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Category("Slide.Objects")]
        [Editor("avt.AllinOneRotator.Net.SlideObjectCollectionEditor,avt.AllinOneRotator.Net", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public SlideObjectCollection SlideObjects { get { return _SlideObjects; } }

        #endregion


        #region Link

        string _SlideUrl = null;
        [UrlProperty()]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Category("Slide.Link")]
        public string SlideUrl { get { return _SlideUrl; } set { _SlideUrl = value; } }

        string _ButtonCaption = null;
        [Category("Slide.Link")]
        public string ButtonCaption { get { return _ButtonCaption; } set { _ButtonCaption = value; } }

        eLinkTarget _Target = eLinkTarget.Self;
        [Category("Slide.Link")]
        public eLinkTarget Target { get { return _Target; } set { _Target = value; } }

        bool _UseTextsBackground = true;
        [Category("Slide.Link")]
        public bool UseTextsBackground { get { return _UseTextsBackground; } set { _UseTextsBackground = value; } }

        #endregion  


        #region MP3

        string _Mp3Url = null;
        [UrlProperty()]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Category("Slide.MP3")]
        public string Mp3Url { get { return _Mp3Url; } set { _Mp3Url = value; } }

        bool _ShowPlayer = true;
        [Category("Slide.MP3")]
        public bool ShowPlayer { get { return _ShowPlayer; } set { _ShowPlayer = value; } }

        Color _IconColor = Color.Black;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Slide.MP3")]
        public Color IconColor { get { return _IconColor; } set { _IconColor = value; } }

        #endregion

        public SlideObjectInfo GetObject(int objectId)
        {
            if (objectId <= 0)
                return null;

            foreach (SlideObjectInfo slideObject in SlideObjects) {
                if (slideObject.Id == objectId)
                    return slideObject;
            }

            return null;
        }

        public void Load(IDataReader dr)
        {
            Id = Convert.ToInt32(dr["SlideId"].ToString());
            ControlId = dr["ControlId"].ToString();
            
            try { Title = dr["Title"].ToString(); } catch { }
            try { DurationSeconds = Convert.ToInt32(dr["DurationSeconds"].ToString()); } catch { }
            try { BackgroundGradientFrom = System.Drawing.Color.FromArgb(Convert.ToInt32(dr["BackgroundGradientFrom"].ToString().Replace("#", "0x"), 16)); } catch { }
            try { BackgroundGradientTo = System.Drawing.Color.FromArgb(Convert.ToInt32(dr["BackgroundGradientTo"].ToString().Replace("#", "0x"), 16)); } catch { }

            try { SlideUrl = dr["Link_Url"].ToString(); } catch { }
            try { ButtonCaption = dr["Link_Caption"].ToString(); } catch { }
            try { Target = (eLinkTarget) Enum.Parse(typeof(eLinkTarget), dr["Link_Target"].ToString(), true); } catch { }
            try { UseTextsBackground = dr["Link_UseTextsBackground"].ToString() == "true"; } catch { }

            try { Mp3Url = dr["Mp3_Url"].ToString(); } catch { }
            try { ShowPlayer = dr["Mp3_ShowPlayer"].ToString() == "true"; } catch { }
            try { IconColor = System.Drawing.Color.FromArgb(Convert.ToInt32(dr["Mp3_IconColor"].ToString().Replace("#", "0x"), 16)); } catch { }

            try { ViewOrder = Convert.ToInt32(dr["ViewOrder"].ToString()); } catch { }

            using (IDataReader drObj = DataProvider.Instance().GetSlideObjects(Id)) {
                while (drObj.Read()) {
                    SlideObjects.Add(SlideObjectInfo.FromDataReader(drObj));
                }
                drObj.Close();
            }
        }

        public static SlideInfo Get(int slideId)
        {
            SlideInfo slide = null;
            using (IDataReader dr = DataProvider.Instance().GetSlide(slideId)) {
                if (dr.Read()) {
                    slide = new SlideInfo();
                    slide.Load(dr);
                }
                dr.Close();
            }

            //if (slide == null)
            //    return null;

            //using (IDataReader dr = DataProvider.Instance().GetSlideObjects(slide.Id)) {
            //    while (dr.Read()) {
            //        slide.SlideObjects.Add(SlideObjectInfo.FromDataReader(dr));
            //    }
            //    dr.Close();
            //}

            return slide;
        }

        public void Save()
        {
            Id = DataProvider.Instance().UpdateSlide(
                Id,
                ControlId,
                Title,
                DurationSeconds,
                ColorExt.ColorToHexString(BackgroundGradientFrom),
                ColorExt.ColorToHexString(BackgroundGradientTo),

                SlideUrl,
                ButtonCaption,
                Target.ToString(),
                UseTextsBackground,

                Mp3Url,
                ShowPlayer,
                ColorExt.ColorToHexString(IconColor),

                ViewOrder
            );
        }


        public override string ToString()
        {
            return Title;
        }

        public string ToDesignerJson()
        {
            StringBuilder sbJson = new StringBuilder();
            
            sbJson.Append("{");
            sbJson.AppendFormat("id:{0},", Id.ToString());
            sbJson.AppendFormat("title:\"{0}\",", RotatorSettings.JsonEncode(Title));
            sbJson.AppendFormat("duration:{0},", DurationSeconds.ToString());
            sbJson.AppendFormat("bkGradFrom:\"{0}\",", ColorExt.ColorToHexString(BackgroundGradientFrom));
            sbJson.AppendFormat("bkGradTo:\"{0}\",", ColorExt.ColorToHexString(BackgroundGradientTo));

            sbJson.AppendFormat("linkUrl:\"{0}\",", SlideUrl);
            sbJson.AppendFormat("linkCaption:\"{0}\",", ButtonCaption);
            sbJson.AppendFormat("linkTarget:\"{0}\",", Target.ToString());
            sbJson.AppendFormat("useTextsBk:{0},", UseTextsBackground ? "true" : "false");

            sbJson.AppendFormat("mp3Url:\"{0}\",", Mp3Url);
            sbJson.AppendFormat("mp3IconColor:\"{0}\",", ColorExt.ColorToHexString(IconColor));
            sbJson.AppendFormat("mp3ShowPlayer:{0},", ShowPlayer ? "true" : "false");

            sbJson.Append("slideObjects:[");
            foreach (SlideObjectInfo slideObject in SlideObjects) {
                sbJson.Append(slideObject.ToStringJson());
                sbJson.Append(",");
            }
            if (sbJson[sbJson.Length - 1] == ',') {
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");

            sbJson.Append("}");

            return sbJson.ToString();
        }

        public void ToXml(XmlWriter Writer)
        {
            Writer.WriteStartElement("ad");

            // slide node attributes
            Writer.WriteAttributeString("theTime", DurationSeconds.ToString());
            Writer.WriteAttributeString("theTitle", Title.ToString());

            // background node and attributes
            Writer.WriteStartElement("background");
            Writer.WriteAttributeString("gradient1", ColorExt.ColorToHexString(BackgroundGradientFrom).Replace("#","0x"));
            Writer.WriteAttributeString("gradient2", ColorExt.ColorToHexString(BackgroundGradientTo).Replace("#", "0x"));
            Writer.WriteEndElement(); //  ("background");

            //// text node and attributes
            //Writer.WriteStartElement("theText");
            //Writer.WriteAttributeString("glowSize", GlowSize.ToString());
            //Writer.WriteAttributeString("glowColor", ColorExt.ColorToHexString(GlowColor).Replace("#", "0x"));
            //Writer.WriteAttributeString("glowStrength", GlowStrength.ToString());
            //Writer.WriteAttributeString("moveType", MoveType.ToString());
            //Writer.WriteAttributeString("easingType", EasingType.ToString());
            //Writer.WriteAttributeString("transitionDuration", TransitionDuration.ToString());
            //Writer.WriteAttributeString("justFade", JustFade ? "yes" : "no");
            //Writer.WriteAttributeString("appearFrom", AppearFrom.ToString().ToLower());
            //Writer.WriteAttributeString("finalXposition", FinalXposition.ToString());
            //Writer.WriteAttributeString("verticalAlign", VerticalAlign.ToString().ToLower());
            //Writer.WriteCData(TextContent);
            //Writer.WriteEndElement(); // ("theText");
            bool bTextFound = false;
            foreach (SlideObjectInfo slideObj in SlideObjects) {
                if (slideObj.ObjectType == eObjectType.Text) {
                    slideObj.ToXml(Writer);
                    bTextFound = true;
                    break;
                }
            }
            if (!bTextFound) {
                Writer.WriteElementString("theText", "");
            }

            // slide objects
            Writer.WriteStartElement("pictures"); 
            foreach (SlideObjectInfo slideObj in SlideObjects) {
                if (slideObj.ObjectType != eObjectType.Text) {
                    slideObj.ToXml(Writer);
                }
            }
            Writer.WriteEndElement(); // ("pictures");

            // link node and attributes
            Writer.WriteStartElement("link");

            Writer.WriteAttributeString("useLink", string.IsNullOrEmpty(SlideUrl) ? "no" : "yes");
            Writer.WriteAttributeString("theTarget", Target == eLinkTarget.New ? "_blank" : "_parent");
            if (!string.IsNullOrEmpty(ButtonCaption)) {
                Writer.WriteAttributeString("showBtn", "yes");
                Writer.WriteAttributeString("btnName", ButtonCaption);
            }
            Writer.WriteAttributeString("useTextsBackground", UseTextsBackground ? "yes" : "no");

            Writer.WriteString(SlideUrl);

            Writer.WriteEndElement(); // ("link");

            // mp3 note and attributes
            Writer.WriteStartElement("mp3");
            //if (!string.IsNullOrEmpty(Mp3Url)) {
                Writer.WriteAttributeString("file", Mp3Url);
                Writer.WriteAttributeString("player", ShowPlayer ? "on" : "off");
                Writer.WriteAttributeString("iconColor", ColorExt.ColorToHexString(IconColor));
            //}
            Writer.WriteEndElement(); // ("mp3");

            Writer.WriteEndElement(); // ("ad");
        }
    }
}
