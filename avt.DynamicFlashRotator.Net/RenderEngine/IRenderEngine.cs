using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using DnnSharp.DynamicRotator.Core.Settings;

namespace DnnSharp.DynamicRotator.Core.RenderEngine
{
    public interface IRenderEngine
    {
        void OnLoad(DynamicRotator rotator);
        void Render(DynamicRotator rotator, HtmlTextWriter output);
    }
}
