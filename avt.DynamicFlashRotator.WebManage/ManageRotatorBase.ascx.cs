using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Xml;
using System.Text;
using System.Globalization;
using DnnSharp.DynamicRotator.Core;
using DnnSharp.DynamicRotator.Core.Settings;
using DnnSharp.DynamicRotator.Core.Data;

namespace avt.DynamicFlashRotator.Net.WebManage
{
    public partial class ManageRotatorBase : UserControl
    {
        protected SlideInfo DefaultSlide = new SlideInfo();
        protected SlideObjectInfo DefaultObject = new SlideObjectInfo() {
            AppearMode = eAppearMode.Fade
        };

        protected SlideObjectInfo DefaultText = new SlideObjectInfo() {
            AppearMode = eAppearMode.Slide
        };
        protected int _ActiveTab = -1;

        public DnnConfiguration Configuration { get; set; }
        public Type ControllerType { get; set; }

        public string ReturnUrl { get; set; }
        public string BuyUrl { get; set; }


        protected int LastUpdate;

        protected void Page_Load(object sender, EventArgs e)
        {
            // check settings
            if (Configuration == null || string.IsNullOrEmpty(Configuration.ConnStr)) {
                Response.Redirect("~/");
            }

            if (!Configuration.HasAccess(Request.QueryString["controlId"])) {
                if (!string.IsNullOrEmpty(Request.QueryString["rurl"])) {
                    Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["rurl"]));
                } else {
                    Response.Redirect("~/");
                }
                return;
            }

            if (!Page.IsPostBack) {

                RotatorSettings settings = new RotatorSettings();

                // check activation
                //var licStatus = RotatorSettings.Configuration.LicenseStatus;
                //lblLicenseMessage.Text = licStatus.Message;

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

                ddObjAppearFromImage.DataSource = Enum.GetNames(typeof(eAllDirs));
                ddObjAppearFromImage.DataBind();

                ddObjMoveType.DataSource = Enum.GetNames(typeof(eMoveType));
                ddObjMoveType.DataBind();

                ddObjEasingType.DataSource = Enum.GetNames(typeof(eEasing));
                ddObjEasingType.DataBind();

                ddObjEffect.DataSource = Enum.GetNames(typeof(eEffect));
                ddObjEffect.DataBind();

                //ddRenderEngine.DataSource = Enum.GetNames(typeof(eRenderEngine));
                //ddRenderEngine.DataBind();

                // load settings
                settings.LoadFromDB(Request.QueryString["controlId"]);

                lblControlName.Text = Configuration.FormatTitle(Request.QueryString["controlId"]);
                tbWidth.Text = settings.Width.Value.ToString();
                tbHeight.Text = settings.Height.Value.ToString();
                ddDirection.SelectedValue = settings.Direction;
                //ddRenderEngine.SelectedValue = settings.RenderEngine.ToString();
                tbFallBackImage.Text = settings.FallbackImage;
                cbAutoStartSlideShow.Checked = settings.AutoStartSlideShow;
                cbShowBottomButtons.Checked = settings.ShowBottomButtons;
                cbShowPlayPauseControls.Checked = settings.ShowPlayPauseControls;
                tbBackgroundColor.Text = ColorExt.ColorToHexString(settings.BackgroundColor);
                cbShowTopTitle.Checked = settings.ShowTopTitle;
                tbTopTitleBackground.Text = ColorExt.ColorToHexString(settings.TopTitleBackground);
                tbTopTitleBgTransparency.Text = settings.TopTitleBgTransparency.ToString();
                tbTopTitleTextColor.Text = ColorExt.ColorToHexString(settings.TopTitleTextColor);
                cbShowTimerBar.Checked = settings.ShowTimerBar;
                cbRandomOrder.Checked = settings.RandomOrder;
                tbSlideButtonsColor.Text = ColorExt.ColorToHexString(settings.SlideButtonsColor);
                tbSlideButtonsNumberColor.Text = ColorExt.ColorToHexString(settings.SlideButtonsNumberColor);
                try { ddSlideButtonsType.SelectedValue = settings.SlideButtonsType.ToString(); } catch { }
                tbSlideButtonsXoffset.Text = settings.SlideButtonsXoffset.ToString();
                tbSlideButtonsYoffset.Text = settings.SlideButtonsYoffset.ToString();
                //cbTransparentBackground.Checked = settings.TransparentBackground;

                hdnLastUpdate.Value = TotalMiliseconds(settings.LastUpdate).ToString();

                // load slides
                hdnSlideXml.Value = settings.SlidesToDesignerJson();
            } else {
                HandleUploads();
            }
        }

        void HandleUploads()
        {
            if (Request.Files == null || Request.Files.Count == 0 || Request.Files[0].ContentLength <= 0)
                return;

            RotatorSettings.Configuration.BrowseServerForResources.Upload(hdnFilePath.Value, Request.Files[0]);
            Response.Clear();
            Response.Write("{\"success\": true}");
            Response.End();
            //try {
            //    string path = portal.HomeDirectoryMapPath + "\\avt.NavXp.Import." + (DateTime.Now.ToFileTime() >> 16).ToString() + ".zip";
            //    Request.Files[0].SaveAs(path);
            //    Server.Execute(TemplateSourceDirectory + "/NavXpApi.aspx?fn=import_summary&format=json&portalid=" + portal.PortalID.ToString() + "&path=" + path); // Response.Write("success");
            //    try {
            //        Response.Flush();
            //        Response.End();
            //    } catch { }
            //} catch (Exception ex) {
            //    if (ex.Message.IndexOf("Thread") == -1) {
            //        Response.Write("Error uploading file (Server response: " + ex.Message + ")");
            //    }
            //}
            //bEndResponse = true;
        }

        double TotalMiliseconds(DateTime date)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            TimeSpan ts = new TimeSpan(date.Ticks - d1.Ticks);

            return ts.TotalMilliseconds;
        }

        protected void SaveSettingsAndClose(object sender, EventArgs e)
        {
            SaveSettings();
            Response.Redirect(ReturnUrl);
        }

        protected void SaveSettingsAndRefresh(object sender, EventArgs e)
        {
            SaveSettings();
            Response.Redirect(Request.RawUrl);
        }

        void SaveSettings()
        {
            RotatorSettings settings = new RotatorSettings();
            settings.LoadFromDB(Request.QueryString["controlId"]);

            DataProvider.Instance().Init(Configuration);

            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "Width", tbWidth.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "Height", tbHeight.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "Direction", ddDirection.SelectedValue);
            //DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "RenderEngine", ddRenderEngine.SelectedValue);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "FallbackImage", tbFallBackImage.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "AutoStartSlideShow", cbAutoStartSlideShow.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "ShowBottomButtons", cbShowBottomButtons.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "ShowPlayPauseControls", cbShowPlayPauseControls.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "BackgroundColor", tbBackgroundColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "ShowTopTitle", cbShowTopTitle.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "TopTitleBackground", tbTopTitleBackground.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "TopTitleBgTransparency", tbTopTitleBgTransparency.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "TopTitleTextColor", tbTopTitleTextColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "ShowTimerBar", cbShowTimerBar.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "RandomOrder", cbRandomOrder.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsColor", tbSlideButtonsColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsNumberColor", tbSlideButtonsNumberColor.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsType", ddSlideButtonsType.SelectedValue);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsXoffset", tbSlideButtonsXoffset.Text);
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "SlideButtonsYoffset", tbSlideButtonsYoffset.Text);
            //DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "TransparentBackground", cbTransparentBackground.Checked ? "true" : "false");
            DataProvider.Instance().UpdateSetting(Request.QueryString["controlId"], "LastUpdate", DateTime.Now.ToString(new CultureInfo("en-US").DateTimeFormat));

            // save slides

            List<int> existingSlides = new List<int>();
            foreach (SlideInfo slide in settings.Slides) {
                existingSlides.Add(slide.Id);
            }

            List<int> existingSlideObjects = new List<int>();

            XmlDocument xmlDocSlides = null;
            //try {
            xmlDocSlides = new XmlDocument();
            xmlDocSlides.LoadXml(Server.HtmlDecode(hdnSlideXml.Value));
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
                    slide.Effect = xmlSlide["effect"].InnerText;
                    try { slide.BackgroundGradientFrom = Color.FromArgb(Convert.ToInt32(xmlSlide["bkGradFrom"].InnerText.Replace("#", "0x"), 16)); } catch { slide.BackgroundGradientFrom = Color.Transparent; }
                    try { slide.BackgroundGradientTo = Color.FromArgb(Convert.ToInt32(xmlSlide["bkGradTo"].InnerText.Replace("#", "0x"), 16)); } catch { slide.BackgroundGradientFrom = Color.Transparent; }
                    slide.SlideUrl = xmlSlide["linkUrl"].InnerText;
                    slide.ButtonCaption = xmlSlide["linkCaption"].InnerText;
                    slide.BtnTextColor = Color.FromArgb(Convert.ToInt32(xmlSlide["btnTextColor"].InnerText.Replace("#", "0x"), 16));
                    slide.BtnBackColor = Color.FromArgb(Convert.ToInt32(xmlSlide["btnBackColor"].InnerText.Replace("#", "0x"), 16));

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
                            try { slideObj.Height = Convert.ToInt32(xmlSlideObj["height"].InnerText); } catch { }
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
        }


        protected void ImportData(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.LoadXml(tbImportData.Text);
                RotatorSettings.LoadFromPortableXml(xmlDoc.DocumentElement, Request.QueryString["controlId"]);
            } catch (Exception ex) {
                validImportXml.IsValid = false;
                validImportXml.Text = "The XML is invalid (inner exception: " + ex.Message + "<be/>" + ex.StackTrace + ")";
                _ActiveTab = 2;
                return;
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void ExportData(object sender, EventArgs e)
        {
            RotatorSettings rotatorSettings = new RotatorSettings();
            rotatorSettings.LoadFromDB(Request.QueryString["controlId"]);

            StringBuilder strXML = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter Writer = XmlWriter.Create(strXML, settings);

            rotatorSettings.SaveToPortableXml(Writer, Request.QueryString["controlId"]);
            Writer.Close();

            tbExportData.Text = strXML.ToString();
            tbExportData.Visible = true;

            _ActiveTab = 2;
        }

        #region Helpers

        protected string ColorToHex(Color color)
        {
            return ColorExt.ColorToHexString(color);
        }

        #endregion

    }
}