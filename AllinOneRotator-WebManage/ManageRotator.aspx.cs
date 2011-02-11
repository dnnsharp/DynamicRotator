using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avt.AllinOneRotator.Net.Data;
using System.Configuration;
using System.Data;
using avt.AllinOneRotator.Net.Settings;

namespace avt.AllinOneRotator.Net.WebManage
{
    public partial class ManageRotator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                ddSlideButtonsType.DataSource = Enum.GetNames(typeof(eSlideButtonsType));
                ddSlideButtonsType.DataBind();

                // load settings

                RotatorSettings settings = new RotatorSettings();
                settings.Init(Request.QueryString["controlId"]);
                settings.LoadFromDB(Request.QueryString["connStr"], Request.QueryString["dbOwner"], Request.QueryString["objQualifier"]);

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
            }
        }

        protected void SaveSettings(object sender, EventArgs e)
        {
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

            Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["rurl"]));
        }
        
    }
}