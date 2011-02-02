using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace avt.AllinOneRotator.Net
{
    public class SlideInfo
    {
        string _Title = "New Slide Title";
        [Category("General")]
        public string Title { get { return _Title; } set { _Title = value; } }

        int _DurationSeconds = 10;
        [Category("General")]
        public int DurationSeconds { get { return _DurationSeconds; } set { _DurationSeconds = value; } }

        
        Color _BackgroundGradientFrom = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Background")]
        public Color BackgroundGradientFrom { get { return _BackgroundGradientFrom; } set { _BackgroundGradientFrom = value; } }

        Color _BackgroundGradientTo = Color.White;
        [TypeConverter(typeof(WebColorConverter))]
        [Category("Background")]
        public Color BackgroundGradientTo { get { return _BackgroundGradientTo; } set { _BackgroundGradientTo = value; } }


        string _Text = "Content of new slide, supports <i>HTML</i>.";
        [Category("Text")]
        public string Text { get { return _Text; } set { _Text = value; } }

        int _GlowSize = 10;
        [Category("Text")]
        public int GlowSize { get { return _GlowSize; } set { _GlowSize = value; } }


    }
}
