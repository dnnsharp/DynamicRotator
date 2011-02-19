using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Drawing.Design;
using System.Xml;
using System.Data;
using avt.AllinOneRotator.Net.Data;
using avt.AllinOneRotator.Net.Settings;

namespace avt.AllinOneRotator.Net
{
    public enum eEffect
    {
        None,
        Blinds,
        Fly,
        Iris,
        Photo,
        PixelDissolve,
        Rotate,
        Squeeze,
        Wipe,
        Zoom
    }

    public enum eVerticalAlign {
        Top,
        Middle,
        Bottom
    }

    public enum eObjectType
    {
        Unknown,
        Text,
        Image,
        Flash
    }

    public enum eAppearMode
    {
        Fade,
        Slide
    }

    public class SlideObjectInfo
    {
        eObjectType _ObjectType = eObjectType.Unknown;
        public eObjectType ObjectType { get { return _ObjectType; } set { _ObjectType = value; } }

        int _Id = -1;
        public int Id { get { return _Id; } set { _Id = value; } }

        int _SlideId = -1;
        public int SlideId { get { return _SlideId; } set { _SlideId = value; } }


        string _Name = "New Slide Object";
        [Category("General")]
        public string Name { get { return _Name; } set { _Name = value; } }

        string _Text = "Slide text, supports <i>HTML</i>";
        [Category("General")]
        public string Text { get { return _Text; } set { _Text = value; } }


        string _ObjectUrl = "";
        [UrlProperty()]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Category("General")]
        public string ObjectUrl { get { return _ObjectUrl; } set { _ObjectUrl = value; } }

        int _Xposition = 15;
        [Category("Position")]
        public int Xposition { get { return _Xposition; } set { _Xposition = value; } }

        int _Yposition = 15;
        [Category("Position")]
        public int Yposition { get { return _Yposition; } set { _Yposition = value; } }

        eVerticalAlign _VerticalAlign = eVerticalAlign.Middle;
        [Category("Position")]
        public eVerticalAlign VerticalAlign { get { return _VerticalAlign; } set { _VerticalAlign = value; } }

        int _Opacity = 100;
        [Category("Effects")]
        public int Opacity { get { return _Opacity; } set { _Opacity = value; } }
        

        Color _GlowColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Effects")]
        public Color GlowColor { get { return _GlowColor; } set { _GlowColor = value; } }

        int _GlowSize = 10;
        [Category("Effects")]
        public int GlowSize { get { return _GlowSize; } set { _GlowSize = value; } }

        int _GlowStrength = 3;
        [Category("Effects")]
        public int GlowStrength { get { return _GlowStrength; } set { _GlowStrength = value; } }


        int _TimeDelay = 1;
        [Category("Transition")]
        public int TimeDelay { get { return _TimeDelay; } set { _TimeDelay = value; } }

        int _TransitionDuration = 2;
        [Category("Transition")]
        public int TransitionDuration { get { return _TransitionDuration; } set { _TransitionDuration = value; } }

        eAppearMode _AppearMode = eAppearMode.Slide;
        [Category("Transition")]
        public eAppearMode AppearMode { get { return _AppearMode; } set { _AppearMode = value; } }

        eAllDirs _SlideFrom = eAllDirs.Top;
        [Category("Transition")]
        public eAllDirs SlideFrom { get { return _SlideFrom; } set { _SlideFrom = value; } }

        eMoveType _SlideMoveType = eMoveType.Strong;
        [Category("Transition")]
        public eMoveType SlideMoveType { get { return _SlideMoveType; } set { _SlideMoveType = value; } }

        eEasing _SlideEasingType = eEasing.Out;
        [Category("Transition")]
        public eEasing SlideEasingType { get { return _SlideEasingType; } set { _SlideEasingType = value; } }

        eEffect _EffectAfterSlide = eEffect.None;
        [Category("Transition")]
        public eEffect EffectAfterSlide { get { return _EffectAfterSlide; } set { _EffectAfterSlide = value; } }

        string _Link = null;
        [UrlProperty()]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Link { get { return _Link; } set { _Link = value; } }


        Color _TextColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Effects")]
        public Color TextColor { get { return _TextColor; } set { _TextColor = value; } }

        Color _TextBackgroundColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Effects")]
        public Color TextBackgroundColor { get { return _TextBackgroundColor; } set { _TextBackgroundColor = value; } }

        int _TextBackgroundOpacity = 0;
        [Category("Effects")]
        public int TextBackgroundOpacity { get { return _TextBackgroundOpacity; } set { _TextBackgroundOpacity = value; } }

        int _TextBackgroundPadding = 5;
        [Category("Transition")]
        public int TextBackgroundPadding { get { return _TextBackgroundPadding; } set { _TextBackgroundPadding = value; } }


        public void Save()
        {
            Id = DataProvider.Instance().UpdateSlideObject(
                Id, SlideId, ObjectType.ToString(), Name, Text, ObjectUrl,
                TimeDelay, TransitionDuration,
                Opacity,
                Xposition, Yposition, VerticalAlign.ToString().ToLower(),
                GlowSize, GlowStrength, ColorExt.ColorToHexString(GlowColor),
                AppearMode.ToString().ToLower(), SlideFrom.ToString().ToLower(), SlideMoveType.ToString().ToLower(),
                SlideEasingType.ToString().ToLower(), EffectAfterSlide.ToString().ToLower(),
                 ColorExt.ColorToHexString(TextColor),  ColorExt.ColorToHexString(TextBackgroundColor), TextBackgroundOpacity, TextBackgroundPadding
            );
        }


        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Name))
                return Name;
            return Path.GetFileName(ObjectUrl);
        }

        public void ToXml(XmlWriter Writer)
        {
            Writer.WriteStartElement(ObjectType == eObjectType.Text ? "theText" : "picture");

            if (ObjectType == eObjectType.Text) {
                Writer.WriteAttributeString("finalXposition", Xposition.ToString());
                Writer.WriteAttributeString("verticalAlign", VerticalAlign.ToString().ToLower());
            } else {
                Writer.WriteAttributeString("Xposition", Xposition.ToString());
                Writer.WriteAttributeString("Yposition", Yposition.ToString());
            }

            Writer.WriteAttributeString("timeDelay", TimeDelay.ToString());
            Writer.WriteAttributeString("appearFrom", SlideFrom.ToString().ToLower());
            Writer.WriteAttributeString("justFade", AppearMode == eAppearMode.Fade ? "yes" : "no");

            if (ObjectType != eObjectType.Text) {
                // add effect for images and swf only
                if (EffectAfterSlide != eEffect.None) {
                    Writer.WriteAttributeString("useEffect", "yes");
                    Writer.WriteAttributeString("effect", EffectAfterSlide.ToString());
                } else {
                    Writer.WriteAttributeString("useEffect", "no");
                    Writer.WriteAttributeString("effect", "");
                }
            } else {
                // add color and backgorund for text
                if (TextBackgroundOpacity == 0) {
                    Writer.WriteAttributeString("useBackground", "no");
                } else {
                    Writer.WriteAttributeString("useBackground", "yes");
                }

                Writer.WriteAttributeString("textColor", ColorExt.ColorToHexString(TextColor).Replace("#", "0x"));
                Writer.WriteAttributeString("backgroundColor", ColorExt.ColorToHexString(TextBackgroundColor).Replace("#", "0x"));
                Writer.WriteAttributeString("backgroundTransparency", TextBackgroundOpacity.ToString());
                Writer.WriteAttributeString("backgroundPadding", TextBackgroundPadding.ToString());
            }

            //if (!string.IsNullOrEmpty(Link))
            Writer.WriteAttributeString("theLink", Link);

            Writer.WriteAttributeString("glowSize", GlowSize.ToString());
            Writer.WriteAttributeString("glowColor", ColorExt.ColorToHexString(GlowColor).Replace("#", "0x"));
            Writer.WriteAttributeString("glowStrength", GlowStrength.ToString());

            Writer.WriteAttributeString("moveType", SlideMoveType.ToString());
            Writer.WriteAttributeString("easingType", SlideEasingType.ToString());
            Writer.WriteAttributeString("transitionDuration", TransitionDuration.ToString());

            if (ObjectType == eObjectType.Text) {
                Writer.WriteAttributeString("textTransparency", Opacity.ToString());
            } else {
                Writer.WriteAttributeString("itemTransparency", Opacity.ToString());
            }

            if (ObjectType == eObjectType.Text) {
                Writer.WriteCData(Text);
            } else {
                Writer.WriteString(ObjectUrl);
            }

            Writer.WriteEndElement(); // ("text/picture");
        }

        public string ToStringJson()
        {
            StringBuilder sbJson = new StringBuilder();

            sbJson.Append("{");
            sbJson.AppendFormat("id:{0},", Id.ToString());
            sbJson.AppendFormat("name:\"{0}\",", RotatorSettings.JsonEncode(Name));
            sbJson.AppendFormat("htmlContents:\"{0}\",", RotatorSettings.JsonEncode(Text));
            sbJson.AppendFormat("itemType:\"{0}\",", ObjectType.ToString());
            sbJson.AppendFormat("resUrl:\"{0}\",", RotatorSettings.JsonEncode(ObjectUrl));
            sbJson.AppendFormat("delay:{0},", TimeDelay.ToString());
            sbJson.AppendFormat("duration:{0},", TransitionDuration.ToString());
            sbJson.AppendFormat("opacity:{0},", Opacity.ToString());
            sbJson.AppendFormat("posx:{0},", Xposition.ToString());
            sbJson.AppendFormat("posy:{0},", Yposition.ToString());
            sbJson.AppendFormat("valign:\"{0}\",", VerticalAlign.ToString());
            sbJson.AppendFormat("glowSize:{0},", GlowSize.ToString());
            sbJson.AppendFormat("glowStrength:{0},", GlowStrength.ToString());
            sbJson.AppendFormat("glowColor:\"{0}\",", ColorExt.ColorToHexString(GlowColor));
            sbJson.AppendFormat("appearMode:\"{0}\",", AppearMode.ToString());
            sbJson.AppendFormat("slideFrom:\"{0}\",", SlideFrom.ToString());
            sbJson.AppendFormat("slideMoveType:\"{0}\",", SlideMoveType.ToString());
            sbJson.AppendFormat("slideEasingType:\"{0}\",", SlideEasingType.ToString());
            sbJson.AppendFormat("effectAfterSlide:\"{0}\",", EffectAfterSlide.ToString());
            sbJson.AppendFormat("textColor:\"{0}\",", ColorExt.ColorToHexString(TextColor));
            sbJson.AppendFormat("textBackgroundColor:\"{0}\",", ColorExt.ColorToHexString(TextBackgroundColor));
            sbJson.AppendFormat("textBackgroundOpacity:{0},", TextBackgroundOpacity.ToString());
            sbJson.AppendFormat("textBackgroundPadding:{0}", TextBackgroundPadding.ToString());
            sbJson.Append("}");

            return sbJson.ToString();
        }


        public static SlideObjectInfo FromDataReader(IDataReader dr)
        {
            SlideObjectInfo slideObject = new SlideObjectInfo();
            slideObject.Id = Convert.ToInt32(dr["ObjectId"].ToString());
            slideObject.SlideId = Convert.ToInt32(dr["SlideId"].ToString());
            slideObject.ObjectType = (eObjectType)Enum.Parse(typeof(eObjectType), dr["ObjectType"].ToString(), true);
            slideObject.Name = dr["Name"].ToString();
            try { slideObject.Text = dr["Text"].ToString(); } catch { }
            try { slideObject.ObjectUrl = dr["ResourceUrl"].ToString(); } catch { }
            try { slideObject.TimeDelay = Convert.ToInt32(dr["DelaySeconds"].ToString()); } catch { }
            try { slideObject.TransitionDuration = Convert.ToInt32(dr["DurationSeconds"].ToString()); } catch { }
            try { slideObject.Opacity = Convert.ToInt32(dr["Opacity"].ToString()); } catch { }
            try { slideObject.Xposition = Convert.ToInt32(dr["PositionX"].ToString()); } catch { }
            try { slideObject.Yposition = Convert.ToInt32(dr["PositionY"].ToString()); } catch { }

            try { slideObject.VerticalAlign = (eVerticalAlign)Enum.Parse(typeof(eVerticalAlign), dr["VerticalAlign"].ToString(), true); } catch { }
            try { slideObject.GlowSize = Convert.ToInt32(dr["GlowSize"].ToString()); } catch { }
            try { slideObject.GlowStrength = Convert.ToInt32(dr["GlowStrength"].ToString()); } catch { }
            try { slideObject.GlowColor = System.Drawing.Color.FromArgb(Convert.ToInt32(dr["GlowColor"].ToString().Replace("#", "0x"), 16)); } catch { }
            try { slideObject.AppearMode = (eAppearMode)Enum.Parse(typeof(eAppearMode), dr["AppearMode"].ToString(), true); } catch { }
            try { slideObject.SlideFrom = (eAllDirs)Enum.Parse(typeof(eAllDirs), dr["SlideFrom"].ToString(), true); } catch { }
            try { slideObject.SlideMoveType = (eMoveType)Enum.Parse(typeof(eMoveType), dr["SlideMoveType"].ToString(), true); } catch { }
            try { slideObject.SlideEasingType = (eEasing)Enum.Parse(typeof(eEasing), dr["SlideEasingType"].ToString(), true); } catch { }
            try { slideObject.EffectAfterSlide = (eEffect)Enum.Parse(typeof(eEffect), dr["EffectAfterSlide"].ToString(), true); } catch { }

            try { slideObject.TextColor = System.Drawing.Color.FromArgb(Convert.ToInt32(dr["TextColor"].ToString().Replace("#", "0x"), 16)); } catch { }
            try { slideObject.TextBackgroundColor = System.Drawing.Color.FromArgb(Convert.ToInt32(dr["TextBackgroundColor"].ToString().Replace("#", "0x"), 16)); } catch { }
            try { slideObject.TextBackgroundOpacity = Convert.ToInt32(dr["TextBackgroundOpacity"].ToString()); } catch { }
            try { slideObject.TextBackgroundPadding = Convert.ToInt32(dr["TextBackgroundPadding"].ToString()); } catch { }

            return slideObject;
        }
    }
}
