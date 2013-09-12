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
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net.Services;
using avt.DynamicFlashRotator.Net.Data;

namespace avt.DynamicFlashRotator.Net
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
        other,
        _self,
        _blank,
        _parent
    }

    
    public class SlideInfo
    {
        public SlideInfo()
        {
        }

        RotatorSettings _Settings;
        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RotatorSettings Settings { get { return _Settings; } set { _Settings = value; } }

        int _Id = -1;
        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Id { get { return _Id; } set { _Id = value; } }

        string _ControlId;
        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ControlId { get { return _ControlId; } set { _ControlId = value; } }

        int _ViewOrder = 0;
        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ViewOrder { get { return _ViewOrder; } set { _ViewOrder = value; } }
        

        #region General

        string _Title = "New Slide Title";
        [Category("General")]
        [Description("Slide Title")]
        public string Title { get { return _Title; } set { _Title = value; } }

        int _DurationSeconds = 10;
        [Category("General")]
        public int DurationSeconds { get { return _DurationSeconds; } set { _DurationSeconds = value; } }

        string _Effect = "Fade";
        [Category("General")]
        [Description("The effect with which the slide appears and disappears from the scene.")]
        public string Effect { get { return _Effect; } set { _Effect = value; } }

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

        Color _BtnTextColor = Color.White;
        [Category("Slide.Link")]
        public Color BtnTextColor { get { return _BtnTextColor; } set { _BtnTextColor = value; } }

        Color _BtnBackColor = Color.FromArgb(0xFF, 0x21, 0x21, 0x82);
        [Category("Slide.Link")]
        public Color BtnBackColor { get { return _BtnBackColor; } set { _BtnBackColor = value; } }

        string _Target = "_self";
        [Category("Slide.Link")]
        public string Target { get { return _Target; } set { _Target = value; } }

        bool _UseTextsBackground = true;
        [Category("Slide.Link")]
        public bool UseTextsBackground { get { return _UseTextsBackground; } set { _UseTextsBackground = value; } }

        bool _ClickAnywhere = true;
        [Category("Slide.Link")]
        public bool ClickAnywhere { get { return _ClickAnywhere; } set { _ClickAnywhere = value; } }

        #endregion  


        #region MP3

        string _Mp3Url = null;
        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [UrlProperty()]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Category("Slide.MP3")]
        public string Mp3Url { get { return _Mp3Url; } set { _Mp3Url = value; } }

        bool _ShowPlayer = true;
        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Slide.MP3")]
        public bool ShowPlayer { get { return _ShowPlayer; } set { _ShowPlayer = value; } }

        Color _IconColor = Color.Black;
        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
            try { Effect = dr["Effect"].ToString(); } catch { }
            
            BackgroundGradientFrom = ColorExt.Parse(dr["BackgroundGradientFrom"], BackgroundGradientFrom);
            BackgroundGradientTo = ColorExt.Parse(dr["BackgroundGradientTo"], BackgroundGradientTo);

            try { SlideUrl = dr["Link_Url"].ToString(); } catch { }
            try { ButtonCaption = dr["Link_Caption"].ToString(); } catch { }
            BtnTextColor = ColorExt.Parse(dr["BtnTextColor"], BtnTextColor);
            BtnBackColor = ColorExt.Parse(dr["BtnBackColor"], BtnBackColor);
            try { Target = dr["Link_Target"].ToString(); } catch { }
            try { UseTextsBackground = dr["Link_UseTextsBackground"].ToString() == "1"; } catch { }
            try { ClickAnywhere = dr["Link_ClickAnywhere"].ToString() == "1"; } catch { }

            try { Mp3Url = dr["Mp3_Url"].ToString(); } catch { }
            try { ShowPlayer = dr["Mp3_ShowPlayer"].ToString() == "1"; } catch { }
            IconColor = ColorExt.Parse(dr["Mp3_IconColor"], IconColor);

            try { ViewOrder = Convert.ToInt32(dr["ViewOrder"].ToString()); } catch { }

            using (IDataReader drObj = DataProvider.Instance().GetSlideObjects(Id)) {
                while (drObj.Read()) {
                    SlideObjects.Add(SlideObjectInfo.FromDataReader(drObj, this));
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
                Effect,
                ColorExt.ColorToHexString(BackgroundGradientFrom),
                ColorExt.ColorToHexString(BackgroundGradientTo),

                SlideUrl,
                ButtonCaption,
                ColorExt.ColorToHexString(BtnTextColor),
                ColorExt.ColorToHexString(BtnBackColor),
                Target,
                UseTextsBackground,
                ClickAnywhere,

                Mp3Url,
                ShowPlayer,
                ColorExt.ColorToHexString(IconColor),

                ViewOrder
            );

            //// also save all slide objects
            //foreach (SlideObjectInfo obj in SlideObjects) {
            //    obj.SlideId = Id;
            //    obj.Slide = this;
            //    obj.Save();
            //}
        }


        public override string ToString()
        {
            return Title;
        }

        public string ToDesignerJson()
        {
            StringBuilder sbJson = new StringBuilder();
            
            sbJson.Append("{");
            sbJson.AppendFormat("\"id\":{0},", Id.ToString());
            sbJson.AppendFormat("\"title\":\"{0}\",", RotatorSettings.JsonEncode(Title));
            sbJson.AppendFormat("\"titleTokenized\":\"{0}\",", RotatorSettings.JsonEncode(RotatorSettings.Configuration.Tokenize(ControlId, Title)));
            sbJson.AppendFormat("\"duration\":{0},", DurationSeconds.ToString());
            sbJson.AppendFormat("\"effect\":\"{0}\",", Effect ?? "None");
            sbJson.AppendFormat("\"bkGradFrom\":\"{0}\",", ColorExt.ColorToHexString(BackgroundGradientFrom));
            sbJson.AppendFormat("\"bkGradTo\":\"{0}\",", ColorExt.ColorToHexString(BackgroundGradientTo));

            sbJson.AppendFormat("\"linkUrl\":\"{0}\",", SlideUrl);
            sbJson.AppendFormat("\"linkUrlTokenized\":\"{0}\",", RotatorSettings.Configuration.Tokenize(ControlId, SlideUrl));
            sbJson.AppendFormat("\"linkCaption\":\"{0}\",", ButtonCaption);
            sbJson.AppendFormat("\"btnTextColor\":\"{0}\",", ColorExt.ColorToHexString(BtnTextColor));
            sbJson.AppendFormat("\"btnBackColor\":\"{0}\",", ColorExt.ColorToHexString(BtnBackColor));
            sbJson.AppendFormat("\"linkTarget\":\"{0}\",", Target);
            sbJson.AppendFormat("\"useTextsBk\":{0},", UseTextsBackground ? "true" : "false");
            sbJson.AppendFormat("\"linkClickAnywhere\":{0},", ClickAnywhere ? "true" : "false");

            sbJson.AppendFormat("\"mp3Url\":\"{0}\",", Mp3Url);
            sbJson.AppendFormat("\"mp3IconColor\":\"{0}\",", ColorExt.ColorToHexString(IconColor));
            sbJson.AppendFormat("\"mp3ShowPlayer\":{0},", ShowPlayer ? "true" : "false");

            sbJson.Append("\"slideObjects\":[");
            foreach (SlideObjectInfo slideObject in SlideObjects) {
                sbJson.Append(slideObject.ToStringJson(ControlId));
                sbJson.Append(",");
            }
            if (sbJson[sbJson.Length - 1] == ',') {
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");

            sbJson.Append("}");

            return sbJson.ToString();
        }


        public void LoadFromPortableXml(XmlNode rootNode, string controlId)
        {
            ControlId = controlId;

            try { Title = rootNode["Title"].InnerText; } catch { }
            try { DurationSeconds = Convert.ToInt32(rootNode["DurationSeconds"].InnerText); } catch { }
            try { Effect = rootNode["Effect"].InnerText; } catch { }

            BackgroundGradientFrom = ColorExt.Parse(rootNode["BackgroundGradientFrom"], BackgroundGradientFrom);
            BackgroundGradientTo = ColorExt.Parse(rootNode["BackgroundGradientTo"], BackgroundGradientTo);

            try { SlideUrl = rootNode["Link_Url"].InnerText; } catch { }
            try { ButtonCaption = rootNode["Link_Caption"].InnerText; } catch { }
            BtnTextColor = ColorExt.Parse(rootNode["BtnTextColor"], BtnTextColor);
            BtnBackColor = ColorExt.Parse(rootNode["BtnBackColor"], BtnBackColor);
            try { Target = rootNode["Link_Target"].InnerText; } catch { }
            try { UseTextsBackground = rootNode["Link_UseTextsBackground"].InnerText == "true"; } catch { }
            try { ClickAnywhere = rootNode["Link_ClickAnywhere"].InnerText == "true"; } catch { }

            //try { Mp3Url = rootNode["Mp3_Url"].InnerText; } catch { }
            //try { ShowPlayer = rootNode["Mp3_ShowPlayer"].InnerText == "true"; } catch { }
            //try { IconColor = ColorExt.Parse(rootNode["Mp3_IconColor"].InnerText.Replace("#", "0x"), 16)); } catch { }

            try { ViewOrder = Convert.ToInt32(rootNode["ViewOrder"].InnerText); } catch { }
            Save();

            foreach (XmlElement xmlSlideObj in rootNode["Objects"].SelectNodes("Object")) {
                SlideObjectInfo obj = new SlideObjectInfo();
                obj.LoadFromPortableXml(xmlSlideObj, controlId);
                obj.SlideId = Id;
                obj.Save();
            }
        }

        public void SaveToPortableXml(XmlWriter Writer, string controlId)
        {
            Writer.WriteStartElement("Slide");

            // slide node attributes
            Writer.WriteElementString("DurationSeconds", DurationSeconds.ToString());
            Writer.WriteElementString("Effect", Effect ?? "None");
            Writer.WriteElementString("Title", Title);

            // background node and attributes
            Writer.WriteElementString("BackgroundGradientFrom", ColorExt.ColorToHexString(BackgroundGradientFrom));
            Writer.WriteElementString("BackgroundGradientTo", ColorExt.ColorToHexString(BackgroundGradientTo));

            // link node and attributes
            Writer.WriteElementString("Link_Url", SlideUrl);
            Writer.WriteElementString("Link_Caption", ButtonCaption);
            Writer.WriteElementString("BtnTextColor", ColorExt.ColorToHexString(BtnTextColor));
            Writer.WriteElementString("BtnBackColor", ColorExt.ColorToHexString(BtnBackColor));
            Writer.WriteElementString("Link_Target", Target);
            Writer.WriteElementString("Link_UseTextsBackground", UseTextsBackground ? "true" : "false");
            Writer.WriteElementString("Link_ClickAnywhere", ClickAnywhere ? "true" : "false");

            Writer.WriteElementString("ViewOrder", ViewOrder.ToString());

            Writer.WriteStartElement("Objects");
            foreach (SlideObjectInfo slideObj in SlideObjects) {
                slideObj.SaveToPortableXml(Writer, controlId);
            }
            Writer.WriteEndElement(); // ("Objects");

            Writer.WriteEndElement(); // Slide
        }


        //public void ToXml(XmlWriter Writer)
        //{
        //    Writer.WriteStartElement("ad");

        //    // slide node attributes
        //    Writer.WriteAttributeString("theTime", DurationSeconds.ToString());
        //    Writer.WriteAttributeString("theTitle", RotatorSettings.Configuration.Tokenize(ControlId, Title));

        //    // background node and attributes
        //    Writer.WriteStartElement("background");
        //    Writer.WriteAttributeString("gradient1", ColorExt.ColorToHexString(BackgroundGradientFrom).Replace("#","0x"));
        //    Writer.WriteAttributeString("gradient2", ColorExt.ColorToHexString(BackgroundGradientTo).Replace("#", "0x"));
        //    Writer.WriteEndElement(); //  ("background");

            
        //    bool bTextFound = false;
        //    foreach (SlideObjectInfo slideObj in SlideObjects) {
        //        if (slideObj.ObjectType == eObjectType.Text) {
        //            slideObj.ToXml(ControlId, Writer, this);
        //            bTextFound = true;
        //            break;
        //        }
        //    }
        //    if (!bTextFound) {
        //        // text default node and attributes so it doesn't crash
        //        SlideObjectInfo emptyText = new SlideObjectInfo();
        //        emptyText.ObjectType = eObjectType.Text;
        //        emptyText.Text = " ";
        //        emptyText.ToXml(ControlId, Writer, this);
        //    }

        //    // slide objects
        //    Writer.WriteStartElement("pictures"); 
        //    foreach (SlideObjectInfo slideObj in SlideObjects) {
        //        if (slideObj.ObjectType != eObjectType.Text) {
        //            slideObj.ToXml(ControlId, Writer, this);
        //        }
        //    }
        //    Writer.WriteEndElement(); // ("pictures");

        //    // link node and attributes
        //    Writer.WriteStartElement("link");
        //    Writer.WriteAttributeString("theTarget", Target);
        //    if (!string.IsNullOrEmpty(ButtonCaption) && !string.IsNullOrEmpty(SlideUrl)) {
        //        Writer.WriteAttributeString("btnName", RotatorSettings.Configuration.Tokenize(ControlId, ButtonCaption));
        //        Writer.WriteAttributeString("showBtn", "yes");
        //        Writer.WriteAttributeString("btnTextColor", ColorExt.ColorToHexString(BtnTextColor).Replace("#", "0x"));
        //        Writer.WriteAttributeString("btnBackColor", ColorExt.ColorToHexString(BtnBackColor).Replace("#", "0x"));
        //    } else {
        //        Writer.WriteAttributeString("btnName", "");
        //        Writer.WriteAttributeString("showBtn", "no");
        //    }
        //    Writer.WriteAttributeString("useLink", string.IsNullOrEmpty(SlideUrl) || !ClickAnywhere ? "no" : "yes");
        //    Writer.WriteAttributeString("useTextsBackground", UseTextsBackground ? "yes" : "no");

        //    Writer.WriteString(RotatorSettings.Configuration.Tokenize(ControlId, SlideUrl));

        //    Writer.WriteEndElement(); // ("link");

        //    // mp3 note and attributes
        //    Writer.WriteStartElement("mp3");
        //    //if (!string.IsNullOrEmpty(Mp3Url)) {
        //        Writer.WriteAttributeString("file", "");//Mp3Url);
        //        Writer.WriteAttributeString("player", "off");//ShowPlayer ? "on" : "off");
        //        Writer.WriteAttributeString("iconColor", ColorExt.ColorToHexString(IconColor));
        //    //}
        //    Writer.WriteEndElement(); // ("mp3");

        //    Writer.WriteEndElement(); // ("ad");
        //}
    }
}
