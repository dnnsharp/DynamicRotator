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

        eMoveType _MoveType = eMoveType.Strong;
        [Category("Transition")]
        public eMoveType MoveType { get { return _MoveType; } set { _MoveType = value; } }

        eEasing _EasingType = eEasing.Out;
        [Category("Transition")]
        public eEasing EasingType { get { return _EasingType; } set { _EasingType = value; } }

        int _TransitionDuration = 2;
        [Category("Transition")]
        public int TransitionDuration { get { return _TransitionDuration; } set { _TransitionDuration = value; } }

        bool _JustFade = false;
        [Category("Transition")]
        public bool JustFade { get { return _JustFade; } set { _JustFade = value; } }

        eAllDirs _AppearFrom = eAllDirs.Top;
        [Category("Transition")]
        public eAllDirs AppearFrom { get { return _AppearFrom; } set { _AppearFrom = value; } }

        eEffect _Effect = eEffect.None;
        [Category("Transition")]
        public eEffect Effect { get { return _Effect; } set { _Effect = value; } }

        string _Link = null;
        [UrlProperty()]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Link { get { return _Link; } set { _Link = value; } }


        public void Save()
        {
            Id = DataProvider.Instance().UpdateSlideObject(
                Id, SlideId, ObjectType.ToString(), Name, ObjectUrl,
                TimeDelay, TransitionDuration,
                Opacity,
                Xposition, Yposition, VerticalAlign.ToString().ToLower(),
                GlowSize, GlowStrength, ColorExt.ColorToHexString(GlowColor)
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
            Writer.WriteStartElement("picture");

            Writer.WriteAttributeString("Xposition", Xposition.ToString());
            Writer.WriteAttributeString("Yposition", Yposition.ToString());

            Writer.WriteAttributeString("timeDelay", TimeDelay.ToString());
            Writer.WriteAttributeString("appearFrom", AppearFrom.ToString().ToLower());
            Writer.WriteAttributeString("justFade", JustFade ? "yes" : "no");

            if (Effect != eEffect.None) {
                Writer.WriteAttributeString("useEffect", "yes");
                Writer.WriteAttributeString("effect", Effect.ToString());
            } else {
                Writer.WriteAttributeString("useEffect", "no");
                Writer.WriteAttributeString("effect", "");
            }

            //if (!string.IsNullOrEmpty(Link))
            Writer.WriteAttributeString("theLink", Link);

            Writer.WriteAttributeString("glowSize", GlowSize.ToString());
            Writer.WriteAttributeString("glowColor", ColorExt.ColorToHexString(GlowColor).Replace("#", "0x"));
            Writer.WriteAttributeString("glowStrength", GlowStrength.ToString());

            Writer.WriteAttributeString("moveType", MoveType.ToString());
            Writer.WriteAttributeString("easingType", EasingType.ToString());
            Writer.WriteAttributeString("transitionDuration", TransitionDuration.ToString());

            Writer.WriteAttributeString("itemTransparency", Opacity.ToString());

            Writer.WriteString(ObjectUrl);
            Writer.WriteEndElement(); // ("picture");
        }

        public string ToStringJson()
        {
            StringBuilder sbJson = new StringBuilder();

            sbJson.Append("{");
            sbJson.AppendFormat("id:{0},", Id.ToString());
            sbJson.AppendFormat("name:\"{0}\",", RotatorSettings.JsonEncode(Name));
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
            sbJson.AppendFormat("glowColor:\"{0}\"", ColorExt.ColorToHexString(GlowColor));
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
            try { slideObject.ObjectUrl = dr["ResourceUrl"].ToString(); } catch { }
            try { slideObject.TimeDelay = Convert.ToInt32(dr["DelaySeconds"].ToString()); } catch { }
            try { slideObject.TransitionDuration = Convert.ToInt32(dr["DurationSeconds"].ToString()); } catch { }
            try { slideObject.Opacity = Convert.ToInt32(dr["Opacity"].ToString()); } catch { }
            try { slideObject.Xposition = Convert.ToInt32(dr["PositionX"].ToString()); } catch { }
            try { slideObject.Yposition = Convert.ToInt32(dr["PositionY"].ToString()); } catch { }
            try { slideObject.VerticalAlign = (eVerticalAlign)Enum.Parse(typeof(eVerticalAlign), dr["VerticalAlign"].ToString()); } catch { slideObject.VerticalAlign = eVerticalAlign.Middle; }
            try { slideObject.GlowSize = Convert.ToInt32(dr["GlowSize"].ToString()); } catch { }
            try { slideObject.GlowStrength = Convert.ToInt32(dr["GlowStrength"].ToString()); } catch { }
            try { slideObject.GlowColor = System.Drawing.Color.FromArgb(Convert.ToInt32(dr["GlowColor"].ToString().Replace("#", "0x"), 16)); } catch { }

            return slideObject;
        }
    }
}
