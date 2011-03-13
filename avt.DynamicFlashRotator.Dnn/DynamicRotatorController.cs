using System;
using System.Collections.Generic;
using System.Web;
using DotNetNuke.Entities.Modules;
using System.Text;
using System.Xml;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net;
using avt.DynamicFlashRotator.Net.Data;

namespace avt.DynamicFlashRotator.Dnn
{
    public class DynamicRotatorController : IPortable
    {

        #region IPortable Members

        public string ExportModule(int ModuleID)
        {
            StringBuilder strXML = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter Writer = XmlWriter.Create(strXML, settings);
            
            RotatorSettings.Init(new DnnConfiguration());
            RotatorSettings Settings = new RotatorSettings();
            Settings.LoadFromDB(ModuleID.ToString());

            Settings.SaveToPortableXml(Writer, ModuleID.ToString());

            Writer.Close();

            return strXML.ToString();
        }

        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            RotatorSettings.Init(new DnnConfiguration());
            RotatorSettings.LoadFromPortableXml(DotNetNuke.Common.Globals.GetContent(Content, "RotatorSettings"), ModuleID.ToString());
        }

        #endregion
    }
}

