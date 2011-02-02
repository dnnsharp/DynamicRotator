using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing.Design;

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

    public enum eVerticalAlgin {
        Top,
        Middle,
        Bottom
    }

    public enum eLinkTarget {
        Self,
        New
    }

    public class SlideInfo
    {
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


        #region Text

        Color _GlowColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Text.Effects")]
        public Color GlowColor { get { return _GlowColor; } set { _GlowColor = value; } }

        int _GlowSize = 10;
        [Category("Text.Effects")]
        public int GlowSize { get { return _GlowSize; } set { _GlowSize = value; } }

        int _GlowStrength = 3;
        [Category("Text.Effects")]
        public int GlowStrength { get { return _GlowStrength; } set { _GlowStrength = value; } }

        int _TextTransparency = 10;
        [Category("Text.Effects")]
        public int TextTransparency { get { return _TextTransparency; } set { _TextTransparency = value; } }

        
        eMoveType _MoveType = eMoveType.Strong;
        [Category("Text.Transition")]
        public eMoveType MoveType { get { return _MoveType; } set { _MoveType = value; } }

        eEasing _EasingType = eEasing.Out;
        [Category("Text.Transition")]
        public eEasing EasingType { get { return _EasingType; } set { _EasingType = value; } }

        int _TransitionDuration = 2;
        [Category("Text.Transition")]
        public int TransitionDuration { get { return _TransitionDuration; } set { _TransitionDuration = value; } }

        bool _JustFade = false;
        [Category("Text.Transition")]
        public bool JustFade { get { return _JustFade; } set { _JustFade = value; } }

        eHorizontadDirs _AppearFrom = eHorizontadDirs.Right;
        [Category("Text.Transition")]
        public eHorizontadDirs AppearFrom { get { return _AppearFrom; } set { _AppearFrom = value; } }


        int _FinalXposition = 15;
        [Category("Text.Position")]
        public int FinalXposition { get { return _FinalXposition; } set { _FinalXposition = value; } }

        eVerticalAlgin _VerticalAlign = eVerticalAlgin.Middle;
        [Category("Text.Position")]
        public eVerticalAlgin VerticalAlign { get { return _VerticalAlign; } set { _VerticalAlign = value; } }

        int _TextPadding = 5;
        [Category("Text.Position")]
        public int TextPadding { get { return _TextPadding; } set { _TextPadding = value; } }


        bool _UseBackground = false;
        [Category("Text.Background")]
        public bool UseBackground { get { return _UseBackground; } set { _UseBackground = value; } }

        Color _TextBackgroundColor = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Text.Background")]
        public Color TextBackgroundColor { get { return _TextBackgroundColor; } set { _TextBackgroundColor = value; } }

        int _TextBackgroundTransparency = 70;
        [Category("Text.Background")]
        public int TextBackgroundTransparency { get { return _TextBackgroundTransparency; } set { _TextBackgroundTransparency = value; } }

        #endregion


        #region Objects

        SlideObjectCollection _SlideObjects = new SlideObjectCollection();

        [DefaultValue("")]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
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


        public override string ToString()
        {
            return Title;
        }
    }
}
