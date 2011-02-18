using System;
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
using avt.AllinOneRotator.Net.Services;

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
                settings.Init(Request.QueryString["controlId"], new AspNetConfiguration());
                settings.LoadFromDB();

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
            settings.Init(Request.QueryString["controlId"], new AspNetConfiguration());
            settings.LoadFromDB();

            string connStr = Request.QueryString["connStr"];
            if (connStr.IndexOf(';') == -1) {
                // this is a name from web.config connnections
                if (ConfigurationManager.ConnectionStrings[connStr] == null) {
                    throw new ArgumentException("Runtime Configuration is enabled but the connection string name is invalid!");
                }
                connStr = ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }

            DataProvider.Instance().Init(new AspNetConfiguration());

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
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "LastUpdate", DateTime.Now.ToString());

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

                    SlideInfo slide;
                    if (slideId > 0) {
                        slide = SlideInfo.Get(slideId);
                    } else {
                        slide = new SlideInfo();
                    }

                    slide.ControlId = Request.QueryString["controlId"];
                    slide.Title = xmlSlide["title"].InnerText;
                    slide.DurationSeconds = Convert.ToInt32(xmlSlide["duration"].InnerText);
                    slide.BackgroundGradientFrom = Color.FromArgb(Convert.ToInt32(xmlSlide["bkGradFrom"].InnerText.Replace("#", "0x"), 16));
                    slide.BackgroundGradientTo = Color.FromArgb(Convert.ToInt32(xmlSlide["bkGradTo"].InnerText.Replace("#", "0x"), 16)); 
                    slide.SlideUrl = xmlSlide["linkUrl"].InnerText;
                    slide.ButtonCaption = xmlSlide["linkCaption"].InnerText;
                    slide.Target = (eLinkTarget) Enum.Parse(typeof(eLinkTarget), xmlSlide["linkTarget"].InnerText);
                    slide.UseTextsBackground = xmlSlide["useTextsBk"].InnerText == "true";

                    slide.Mp3Url = xmlSlide["mp3Url"].InnerText;
                    slide.ShowPlayer = xmlSlide["mp3ShowPlayer"].InnerText == "true";
                    slide.IconColor = Color.FromArgb(Convert.ToInt32(xmlSlide["mp3IconColor"].InnerText.Replace("#", "0x"), 16));

                    slide.ViewOrder = Convert.ToInt32(xmlSlide["viewOrder"].InnerText);

                    slide.Save();

                    // save slide objects
                    List<int> existingSlideObjects = new List<int>();
                    foreach (SlideObjectInfo slideObj in slide.SlideObjects) {
                        existingSlideObjects.Add(slideObj.Id);
                    }
                    
                    if (xmlSlide["slideObjects"] != null) {
                        foreach (XmlElement xmlSlideObj in xmlSlide["slideObjects"].SelectNodes("obj")) {
                            int slideObjId = Convert.ToInt32(xmlSlideObj["id"].InnerText);
                            if (slideObjId > 0) {
                                existingSlideObjects.Remove(slideObjId);
                            }

                            SlideObjectInfo slideObj;
                            if (slideObjId > 0) {
                                slideObj = slide.GetObject(slideObjId);
                            } else {
                                slideObj = new SlideObjectInfo();
                            }

                            slideObj.SlideId = slide.Id;
                            slideObj.Name = xmlSlideObj["name"].InnerText;
                            slideObj.ObjectType = (eObjectType)Enum.Parse(typeof(eObjectType), xmlSlideObj["itemType"].InnerText, true);
                            slideObj.ObjectUrl = xmlSlideObj["resUrl"].InnerText;
                            slideObj.TimeDelay = Convert.ToInt32(xmlSlideObj["delay"].InnerText);
                            slideObj.TransitionDuration = Convert.ToInt32(xmlSlideObj["duration"].InnerText);
                            slideObj.Opacity = Convert.ToInt32(xmlSlideObj["opacity"].InnerText);
                            slideObj.Xposition = Convert.ToInt32(xmlSlideObj["posx"].InnerText);
                            slideObj.Yposition = Convert.ToInt32(xmlSlideObj["posy"].InnerText);
                            // slideObj.VerticalAlign = Convert.ToInt32(xmlSlideObj["posy"].InnerText);
                            slideObj.GlowSize = Convert.ToInt32(xmlSlideObj["glowSize"].InnerText);
                            slideObj.GlowStrength = Convert.ToInt32(xmlSlideObj["glowStrength"].InnerText);
                            slideObj.GlowColor = Color.FromArgb(Convert.ToInt32(xmlSlideObj["glowColor"].InnerText.Replace("#", "0x"), 16));
                            slideObj.Save();
                        }
                    }

                    // delete the rest
                    foreach (int slideObjectId in existingSlideObjects) {
                        DataProvider.Instance().RemoveSlideObject(slideObjectId);
                    }
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