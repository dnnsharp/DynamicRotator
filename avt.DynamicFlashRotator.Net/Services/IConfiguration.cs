using System;
using System.Collections.Generic;
using System.Text;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net.Services.Authentication;

namespace avt.DynamicFlashRotator.Net.Services
{
    public interface IConfiguration
    {
        string ConnStr { get; }
        string DbOwner { get; }
        string ObjQualifier { get; }

        bool ShowManageLinks();
        bool HasAccess(string controlId);
        bool HasAccess(string controlId, IList<IAdminAuthentication> authLayers);
         
        bool IsDebug();

        string FormatTitle(string controlId);
        string Tokenize(string controlId, string content);

        FileBrowser BrowseServerForResources { get; }
    }
}
