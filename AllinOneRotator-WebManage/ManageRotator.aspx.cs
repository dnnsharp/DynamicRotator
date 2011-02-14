﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avt.AllinOneRotator.Net.Data;
using System.Configuration;
using System.Data;
using avt.AllinOneRotator.Net.Settings;
using System.Drawing;
using System.Xml;

namespace avt.AllinOneRotator.Net.WebManage
{
    public partial class ManageRotator : System.Web.UI.Page
    {
        protected SlideInfo DefaultSlide = new SlideInfo();
        protected SlideObjectInfo DefaultObject = new SlideObjectInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {

                ddSlideButtonsType.DataSource = Enum.GetNames(typeof(eSlideButtonsType));
                ddSlideButtonsType.DataBind();

                // load enums for slides
                ddLinkTarget.DataSource = Enum.GetNames(typeof(eLinkTarget));
                ddLinkTarget.DataBind();
                try { ddLinkTarget.SelectedValue = DefaultSlide.Target.ToString(); } catch { }

                // load enums for objects
                ddObjAppearFromText.DataSource = Enum.GetNames(typeof(eHorizontadDirs));
                ddObjAppearFromText.DataBind();
                try { ddObjAppearFromText.SelectedValue = DefaultObject.AppearFrom.ToString(); } catch { }

                ddObjAppearFromImage.DataSource = Enum.GetNames(typeof(eAllDirs));
                ddObjAppearFromImage.DataBind();
                try { ddObjAppearFromImage.SelectedValue = DefaultObject.AppearFrom.ToString(); } catch { }

                ddObjMoveType.DataSource = Enum.GetNames(typeof(eMoveType));
                ddObjMoveType.DataBind();
                try { ddObjMoveType.SelectedValue = DefaultObject.MoveType.ToString(); } catch { }

                ddObjEasingType.DataSource = Enum.GetNames(typeof(eEasing));
                ddObjEasingType.DataBind();
                try { ddObjEasingType.SelectedValue = DefaultObject.EasingType.ToString(); } catch { }

                ddObjEffect.DataSource = Enum.GetNames(typeof(eEffect));
                ddObjEffect.DataBind();
                try { ddObjEffect.SelectedValue = DefaultObject.Effect.ToString(); } catch { }

                // load settings

                RotatorSettings settings = new RotatorSettings();
                settings.Init(Request.QueryString["controlId"]);
                settings.LoadFromDB(Request.QueryString["connStr"], Request.QueryString["dbOwner"], Request.QueryString["objQualifier"]);

                tbWidth.Text = settings.Width.Value.ToString();
                tbHeight.Text = settings.Height.Value.ToString();
                cbAutoStartSlideShow.Checked = settings.AutoStartSlideShow;
                cbUseRoundCornersMask.Checked = settings.UseRoundCornersMask;
                tbRoundCornerMaskColor.Text = ColorExt.ColorToHexString(settings.RoundCornerMaskColor);
                cbShowBottomButtons.Checked = settings.ShowBottomButtons;
                cbShowPlayPauseControls.Checked = settings.ShowPlayPauseControls;
                tbFadeColor.Text = ColorExt.ColorToHexString(settings.FadeColor);
                cbShowTopTitle.Checked = settings.ShowTopTitle;
                tbTopTitleBackground.Text = ColorExt.ColorToHexString(settings.TopTitleBackground);
                tbTopTitleBgTransparency.Text = settings.TopTitleBgTransparency.ToString();
                tbTopTitleTextColor.Text = ColorExt.ColorToHexString(settings.TopTitleTextColor);
                cbShowTimerBar.Checked = settings.ShowTimerBar;
                tbSlideButtonsColor.Text = ColorExt.ColorToHexString(settings.SlideButtonsColor);
                tbSlideButtonsNumberColor.Text = ColorExt.ColorToHexString(settings.SlideButtonsNumberColor);
                try { ddSlideButtonsType.SelectedValue = settings.SlideButtonsType.ToString(); } catch { }
                tbSlideButtonsXoffset.Text = settings.SlideButtonsXoffset.ToString();
                tbSlideButtonsYoffset.Text = settings.SlideButtonsYoffset.ToString();
                cbTransparentBackground.Checked = settings.TransparentBackground;

                // load slides
                hdnSlideXml.Value = settings.SlidesToDesignerJson();
            }
        }

        protected void SaveSettings(object sender, EventArgs e)
        {
            RotatorSettings settings = new RotatorSettings();
            settings.Init(Request.QueryString["controlId"]);
            settings.LoadFromDB(Request.QueryString["connStr"], Request.QueryString["dbOwner"], Request.QueryString["objQualifier"]);

            string connStr = Request.QueryString["connStr"];
            if (connStr.IndexOf(';') == -1) {
                // this is a name from web.config connnections
                if (ConfigurationManager.ConnectionStrings[connStr] == null) {
                    throw new ArgumentException("Runtime Configuration is enabled but the connection string name is invalid!");
                }
                connStr = ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }

            DataProvider.Instance().ConnStr = connStr;
            DataProvider.Instance().DbOwner = Request.QueryString["dbOwner"];
            DataProvider.Instance().ObjQualifier = Request.QueryString["objQualifier"];
            DataProvider.Instance().Init();

            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "Width", tbWidth.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "Height", tbHeight.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "AutoStartSlideShow", cbAutoStartSlideShow.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "UseRoundCornersMask", cbUseRoundCornersMask.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "RoundCornerMaskColor", tbRoundCornerMaskColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "ShowBottomButtons", cbShowBottomButtons.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "ShowPlayPauseControls", cbShowPlayPauseControls.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "FadeColor", tbFadeColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "ShowTopTitle", cbShowTopTitle.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "TopTitleBackground", tbTopTitleBackground.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "TopTitleBgTransparency", tbTopTitleBgTransparency.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "TopTitleTextColor", tbTopTitleTextColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "ShowTimerBar", cbShowTimerBar.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsColor", tbSlideButtonsColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsNumberColor", tbSlideButtonsNumberColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsType", ddSlideButtonsType.SelectedValue);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsXoffset", tbSlideButtonsXoffset.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsYoffset", tbSlideButtonsYoffset.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "TransparentBackground", cbTransparentBackground.Checked ? "true" : "false");

            // save slides

            List<int> existingSlides = new List<int>();
            foreach (SlideInfo slide in settings.Slides) {
                existingSlides.Add(slide.Id);
            }

            XmlDocument xmlDocSlides = null;
            //try {
                xmlDocSlides = new XmlDocument();
                xmlDocSlides.LoadXml(hdnSlideXml.Value);
            //} catch { xmlDocSlides = null; }

            if (xmlDocSlides != null) {
                foreach (XmlElement xmlSlide in xmlDocSlides.DocumentElement.SelectNodes("slide")) {
                    
                    int slideId = Convert.ToInt32(xmlSlide["id"].InnerText);
                    if (slideId > 0) {
                        existingSlides.Remove(slideId);
                    }

                    slideId = DataProvider.Instance().UpdateSlide(
                        slideId,
                        Request.QueryString["controlId"],
                        xmlSlide["title"].InnerText,
                        Convert.ToInt32(xmlSlide["duration"].InnerText),
                        xmlSlide["bkGradFrom"].InnerText,
                        xmlSlide["bkGradTo"].InnerText,

                        xmlSlide["linkUrl"].InnerText,
                        xmlSlide["linkCaption"].InnerText,
                        xmlSlide["linkTarget"].InnerText,
                        xmlSlide["useTextsBk"].InnerText == "true",

                        xmlSlide["mp3Url"].InnerText,
                        xmlSlide["mp3ShowPlayer"].InnerText == "true",
                        xmlSlide["mp3IconColor"].InnerText
                    );

                    //IDataReader slideData = DataProvider.Instance().GetSlide(slideId);
                    //slideData.Read();
                    //SlideInfo slide = new SlideInfo();
                    //slide.Load(slideData);
                    //slideData.Close();

                    //// save slide objects
                    //List<int> existingSlideObjects = new List<int>();
                    //foreach (SlideObjectInfo slideObj in slide.SlideObjects) {
                    //    existingSlideObjects.Add(slideObj.Id);
                    //}
                    
                }
            }

            // delete the rest
            foreach (int slideId in existingSlides) {
                DataProvider.Instance().RemoveSlide(slideId);
            }
            

            Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["rurl"]));
        }


        #region Helpers

        protected string ColorToHex(Color color)
        {
            return avt.AllinOneRotator.Net.ColorExt.ColorToHexString(color);
        }

        #endregion

    }
}