using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avt.DynamicFlashRotator.Net.Data;
using System.Configuration;
using System.Data;
using avt.DynamicFlashRotator.Net.Settings;
using System.Drawing;
using System.Xml;
using avt.DynamicFlashRotator.Net.Services;

namespace avt.DynamicFlashRotator.Net.WebManage
{
    public partial class ManageRotator : System.Web.UI.Page
    {
        protected SlideInfo DefaultSlide = new SlideInfo();
        protected SlideObjectInfo DefaultObject = new SlideObjectInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            // check settings
            if (RotatorSettings.Configuration == null) {
                string sessionKey = "avt.DynamicRotator." + Request.QueryString["controlId"];
                if (Session[sessionKey] != null) {
                    RotatorSettings.Init(new AspNetConfiguration());
                } else {
                    // don't have it in the query string, let's go back to previous page
                    if (!string.IsNullOrEmpty(Request.QueryString["rurl"])) {
                        Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["rurl"]));
                    } else {
                        Response.Redirect("~/");
                    }
                    return;
                }
            }

            if (!RotatorSettings.Configuration.HasAccess(Request.QueryString["controlId"])) {
                if (!string.IsNullOrEmpty(Request.QueryString["rurl"])) {
                    Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["rurl"]));
                } else {
                    Response.Redirect("~/");
                }
                return;
            }


            if (!Page.IsPostBack) {

                // check activation
                lblTabActivateContents.Visible = lblTabActivate.Visible = !avt.DynamicFlashRotator.Net.Settings.RotatorSettings.IsActivated();

                ddSlideButtonsType.Items.Add(new ListItem("Square (with numbers)", eSlideButtonsType.SquareWithNumbers.ToString()));
                ddSlideButtonsType.Items.Add(new ListItem("Round (no numbers)", eSlideButtonsType.RoundNoNumbers.ToString()));

                // load enums for slides
                ddLinkTarget.Items.Add(new ListItem("Same Window", eLinkTarget._self.ToString()));
                ddLinkTarget.Items.Add(new ListItem("New Window", eLinkTarget._blank.ToString()));
                ddLinkTarget.Items.Add(new ListItem("Parent Window", eLinkTarget._parent.ToString()));
                ddLinkTarget.Items.Add(new ListItem("Named Window...", "other"));
                try { ddLinkTarget.SelectedValue = DefaultSlide.Target.ToString(); } catch { }

                ddAppearMode.DataSource = Enum.GetNames(typeof(eAppearMode));
                ddAppearMode.DataBind();

                // load enums for objects

                ddVerticalAlgin.DataSource = Enum.GetNames(typeof(eVerticalAlign));
                ddVerticalAlgin.DataBind();

                ddObjAppearFromText.DataSource = Enum.GetNames(typeof(eHorizontadDirs));
                ddObjAppearFromText.DataBind();
                //try { ddObjAppearFromText.SelectedValue = DefaultObject.SlideFrom.ToString(); } catch { }

                ddObjAppearFromImage.DataSource = Enum.GetNames(typeof(eAllDirs));
                ddObjAppearFromImage.DataBind();
                //try { ddObjAppearFromImage.SelectedValue = DefaultObject.SlideFrom.ToString(); } catch { }

                ddObjMoveType.DataSource = Enum.GetNames(typeof(eMoveType));
                ddObjMoveType.DataBind();
                //try { ddObjMoveType.SelectedValue = DefaultObject.SlideMoveType.ToString(); } catch { }

                ddObjEasingType.DataSource = Enum.GetNames(typeof(eEasing));
                ddObjEasingType.DataBind();
                //try { ddObjEasingType.SelectedValue = DefaultObject.SlideEasingType.ToString(); } catch { }

                ddObjEffect.DataSource = Enum.GetNames(typeof(eEffect));
                ddObjEffect.DataBind();
                //try { ddObjEffect.SelectedValue = DefaultObject.EffectAfterSlide.ToString(); } catch { }

                // load settings
                RotatorSettings settings = new RotatorSettings();
                // settings.Init(Request.QueryString["controlId"], new AspNetConfiguration());
                settings.LoadFromDB(Request.QueryString["controlId"]);

                lblControlName.Text = RotatorSettings.Configuration.FormatTitle(Request.QueryString["controlId"]);
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
            settings.LoadFromDB(Request.QueryString["controlId"]);

            DataProvider.Instance().Init(RotatorSettings.Configuration);

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

            List<int> existingSlideObjects = new List<int>();

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
                    slide.Target = xmlSlide["linkTarget"].InnerText;
                    //slide.UseTextsBackground = xmlSlide["useTextsBk"].InnerText == "true";
                    slide.UseTextsBackground = true;
                    slide.ClickAnywhere = xmlSlide["linkClickAnywhere"].InnerText == "true";
                    

                    //slide.Mp3Url = xmlSlide["mp3Url"].InnerText;
                    //slide.ShowPlayer = xmlSlide["mp3ShowPlayer"].InnerText == "true";
                    //slide.IconColor = Color.FromArgb(Convert.ToInt32(xmlSlide["mp3IconColor"].InnerText.Replace("#", "0x"), 16));

                    slide.ViewOrder = Convert.ToInt32(xmlSlide["viewOrder"].InnerText);

                    slide.Save();

                    // save slide objects
                    foreach (SlideObjectInfo slideObj in slide.SlideObjects) {
                        existingSlideObjects.Add(slideObj.Id);
                    }
                    
                    if (xmlSlide["slideObjects"] != null) {
                        int viewOrder = 0;
                        foreach (XmlElement xmlSlideObj in xmlSlide["slideObjects"].SelectNodes("obj")) {
                            int slideObjId = Convert.ToInt32(xmlSlideObj["id"].InnerText);
                            if (slideObjId > 0) {
                                existingSlideObjects.Remove(slideObjId);
                            }

                            SlideObjectInfo slideObj = new SlideObjectInfo();
                            if (slideObjId > 0) {
                                slideObj.Id = slideObjId;
                            }

                            slideObj.SlideId = slide.Id;
                            slideObj.Name = xmlSlideObj["name"].InnerText;
                            slideObj.Link = xmlSlideObj["linkUrl"].InnerText;
                            slideObj.Text = xmlSlideObj["htmlContents"].InnerText;
                            slideObj.ObjectType = (eObjectType)Enum.Parse(typeof(eObjectType), xmlSlideObj["itemType"].InnerText, true);
                            try { slideObj.ObjectUrl = xmlSlideObj["resUrl"].InnerText; } catch { }
                            try { slideObj.TimeDelay = Convert.ToInt32(xmlSlideObj["delay"].InnerText); } catch { }
                            try { slideObj.TransitionDuration = Convert.ToInt32(xmlSlideObj["duration"].InnerText); } catch { }
                            try { slideObj.Opacity = Convert.ToInt32(xmlSlideObj["opacity"].InnerText); } catch { }
                            try { slideObj.Xposition = Convert.ToInt32(xmlSlideObj["posx"].InnerText); } catch { }
                            try { slideObj.Yposition = Convert.ToInt32(xmlSlideObj["posy"].InnerText); } catch { }
                            try { slideObj.Width = Convert.ToInt32(xmlSlideObj["width"].InnerText); } catch { }
                            try { slideObj.VerticalAlign = (eVerticalAlign)Enum.Parse(typeof(eVerticalAlign), xmlSlideObj["valign"].InnerText, true); } catch { }
                            
                            try { slideObj.GlowSize = Convert.ToInt32(xmlSlideObj["glowSize"].InnerText); } catch { }
                            try { slideObj.GlowStrength = Convert.ToInt32(xmlSlideObj["glowStrength"].InnerText); } catch { }
                            try { slideObj.GlowColor = Color.FromArgb(Convert.ToInt32(xmlSlideObj["glowColor"].InnerText.Replace("#", "0x"), 16)); } catch { }

                            try { slideObj.TextColor = Color.FromArgb(Convert.ToInt32(xmlSlideObj["textColor"].InnerText.Replace("#", "0x"), 16)); } catch { }
                            try { slideObj.TextBackgroundColor = Color.FromArgb(Convert.ToInt32(xmlSlideObj["textBackgroundColor"].InnerText.Replace("#", "0x"), 16)); } catch { }
                            try { slideObj.TextBackgroundOpacity = Convert.ToInt32(xmlSlideObj["textBackgroundOpacity"].InnerText); } catch { }
                            try { slideObj.TextBackgroundPadding = Convert.ToInt32(xmlSlideObj["textBackgroundPadding"].InnerText); } catch { }

                            try { slideObj.AppearMode = (eAppearMode)Enum.Parse(typeof(eAppearMode), xmlSlideObj["appearMode"].InnerText, true); } catch { }
                            try { slideObj.SlideFrom = (eAllDirs)Enum.Parse(typeof(eAllDirs), xmlSlideObj["slideFrom"].InnerText, true); } catch { }
                            try { slideObj.SlideMoveType = (eMoveType)Enum.Parse(typeof(eMoveType), xmlSlideObj["slideMoveType"].InnerText, true); } catch { }
                            try { slideObj.SlideEasingType = (eEasing)Enum.Parse(typeof(eEasing), xmlSlideObj["slideEasingType"].InnerText, true); } catch { }
                            try { slideObj.EffectAfterSlide = (eEffect)Enum.Parse(typeof(eEffect), xmlSlideObj["effectAfterSlide"].InnerText, true); } catch { }

                            slideObj.ViewOrder = viewOrder++;
                            slideObj.Save();
                        }
                    }
                }
            }

            // delete the rest of slide objects
            foreach (int slideObjectId in existingSlideObjects) {
                DataProvider.Instance().RemoveSlideObject(slideObjectId);
            }

            // delete the rest of slides
            foreach (int slideId in existingSlides) {
                DataProvider.Instance().RemoveSlide(slideId);
            }
            

            Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["rurl"]));
        }


        #region Helpers

        protected string ColorToHex(Color color)
        {
            return avt.DynamicFlashRotator.Net.ColorExt.ColorToHexString(color);
        }

        #endregion

    }
}