using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using avt.DynamicFlashRotator.Net.Settings;
using avt.DynamicFlashRotator.Net;
using System.Xml;
using System.Text;
using System.Collections;

namespace avt.DynamicFlashRotator.Dnn
{
    /// <summary>
    /// Summary description for Config
    /// </summary>
    public class Config : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //int mid;
            //if (!int.TryParse(context.Request["mid"], out mid)) {
            //    context.Response.Write("Invalid module!");
            //    return;
            //}

            var  settings = new RotatorSettings();
            if (!settings.LoadFromDB(context.Request["controlId"])) {
                settings.LoadMiniTutorialWebManage();
            }

            if (context.Request.Params["avtadrot"] == "settings") {
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.Cache.SetNoStore();
                context.Response.Write(settings.ToXml());
                //Page.Response.ContentType = "text/xml";
                context.Response.ContentType = "text/xml; charset=utf-8";
                return;
            }

            if (context.Request.Params["avtadrot"] == "content") {
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.Cache.SetNoStore();
                context.Response.Write(GetSlidesXml(settings));
                context.Response.ContentType = "text/xml; charset=utf-8";
                //Page.Response.ContentType = "text/xml";
                return;
            }

            if (context.Request.Params["avtadrot"] == "transitions") {
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.Cache.SetNoStore();
                context.Response.Write(@"<?xml version=""1.0"" encoding=""utf-8""?>
    <picturesTransitions>
        <transition theName=""Blinds"" theEasing=""Strong"" theStrips=""20"" theDimension=""1""/>
        <trasition ></trasition>
        <transition theName=""Fly"" theEasing=""Strong"" theStartPoint=""9""/>
        <transition theName=""Iris"" theEasing=""Bounce"" theStartPoint=""1"" theShape=""CIRCLE""/>
        <transition theName=""Photo"" theEasing=""Elastic""/>
        <transition theName=""PixelDissolve"" theEasing=""Strong"" theXsections=""20"" theYsections=""20""/>
        <transition theName=""Rotate"" theEasing=""Strong"" theDegrees=""720""/>
        <transition theName=""Squeeze"" theEasing=""Strong"" theDimension=""1""/>
        <transition theName=""Wipe"" theEasing=""Strong"" theStartPoint=""1""/>
        <transition theName=""Zoom"" theEasing=""Back""/>
    </picturesTransitions>
    "
                    );
                context.Response.ContentType = "text/xml";
                return;
            }
        }

        string GetSlidesXml(RotatorSettings settings)
        {

            StringBuilder strXML = new StringBuilder();
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.OmitXmlDeclaration = true;
            xmlSettings.Encoding = Encoding.UTF8;
            XmlWriter Writer = XmlWriter.Create(strXML, xmlSettings);

            var slides = settings.Slides;
            if (!settings.IsActivated() || settings.IsTrialExpired()) {

                SlideObjectInfo trialText = new SlideObjectInfo();
                trialText.ObjectType = eObjectType.Text;
                if (settings.IsTrialExpired()) {
                    trialText.Text = "<font size='20px' style='font-size:20px;' color='#C77405'><font size='30px' style='font-size:30px;'><i>Dynamic Rotator .NET</i></font> Trial Expired!</font>";
                } else {
                    trialText.Text = "<font size='20px' style='font-size:20px;' color='#C77405'><font size='30px' style='font-size:30px;'><i>Dynamic Rotator .NET</i></font><br/>Use admin to Unlock 30 Day Trial or Activate for production.</font>";
                }
                trialText.Yposition = 70;
                trialText.Xposition = 240;

                SlideObjectInfo logoObj = new SlideObjectInfo();
                logoObj.ObjectType = eObjectType.Image;
                logoObj.ObjectUrl = "http://www.avatar-soft.ro/Portals/0/product_logo/Dynamic-Rotator.png";
                logoObj.Yposition = 30;
                logoObj.Xposition = 20;
                logoObj.SlideFrom = eAllDirs.Left;
                logoObj.EffectAfterSlide = eEffect.Zoom;
                logoObj.TransitionDuration = 1;

                SlideInfo trialSlide = new SlideInfo();
                trialSlide.SlideObjects.Add(trialText);
                trialSlide.SlideObjects.Add(logoObj);
                trialSlide.SlideUrl = "http://www.avatar-soft.ro/dotnetnuke-modules/dnn-banner/flash/dynamic-rotator.aspx";
                trialSlide.ButtonCaption = "Read More...";
                trialSlide.Settings = settings;

                slides.Clear();
                slides.Add(trialSlide);
            }

            Writer.WriteStartElement("ads");
            IList finalSlides = settings.RandomOrder ? ShuffleSlides(slides) : slides;
            foreach (SlideInfo slide in finalSlides)
                slide.ToXml(Writer);
            Writer.WriteEndElement(); // "ads";

            Writer.Close();

            return strXML.ToString();
        }

        static IList ShuffleSlides(SlideCollection slides)
        {
            List<SlideInfo> suffledSlides = new List<SlideInfo>();
            foreach (SlideInfo s in slides)
                suffledSlides.Add(s);

            Random rng = new Random();
            int n = suffledSlides.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                SlideInfo value = suffledSlides[k];
                suffledSlides[k] = suffledSlides[n];
                suffledSlides[n] = value;
            }
            return suffledSlides;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}