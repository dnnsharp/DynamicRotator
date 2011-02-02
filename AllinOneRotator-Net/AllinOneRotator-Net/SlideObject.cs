using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Drawing.Design;

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

    public class SlideObjectInfo
    {
        string _ObjectUrl = "/path/to/resource.ext";
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


        public override string ToString()
        {
            return Path.GetFileName(ObjectUrl);
        }

    }
}
