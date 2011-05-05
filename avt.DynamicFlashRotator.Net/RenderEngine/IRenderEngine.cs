using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using avt.DynamicFlashRotator.Net.Settings;

namespace avt.DynamicFlashRotator.Net.RenderEngine
{
    public interface IRenderEngine
    {
        void OnLoad(DynamicRotator rotator);
        void Render(DynamicRotator rotator, HtmlTextWriter output);
    }
}
