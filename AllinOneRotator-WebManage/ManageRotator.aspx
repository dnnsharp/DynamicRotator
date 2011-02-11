<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ManageRotator.aspx.cs" Inherits="avt.AllinOneRotator.Net.WebManage.ManageRotator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage ALLinONE Rotator Settings</title>
    <link type ="text/css" rel="stylesheet" href = "<%=TemplateSourceDirectory + "/res/ui-lightness/jquery-ui.css"%>" />
    <link type ="text/css" rel="stylesheet" href = "<%=TemplateSourceDirectory + "/res/js/colorpicker/css/colorpicker.css"%>" />
    <link type ="text/css" rel="stylesheet" href = "<%=TemplateSourceDirectory + "/res/manage.css"%>" />

    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/jQuery.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/jQuery-ui.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/colorpicker/js/colorpicker.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/colorpicker/js/eye.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/colorpicker/js/utils.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <div class="btnPane">
            <a href = "<%= System.Web.HttpUtility.UrlDecode(Request.QueryString["rurl"]) %>" style="color: #d24242; padding: 1px 10px; margin-right: 10px; font-weight: normal;" >Cancel</a>
            <asp:Button runat="server" OnClick="SaveSettings" class="ui-state-default" style="padding: 4px 10px;" Text="Save" />
        </div>

        <h1 class="manageTitle">Settings for <i>Control1</i></h1>
        <div style="clear: both;"></div>
    </div>

    <div id = "mainTabs">
        <ul>
            <li><a href="#tabs-main-settings">General Settings</a></li>
            <li><a href="#tabs-main-slides">Slides</a></li>
            <li><a href="#tabs-main-activate">Activate</a></li>
        </ul>

        <div id = "tabs-main-settings">
            
            <table>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbAutoStartSlideShow">Auto Start SlideShow</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbAutoStartSlideShow" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbUseRoundCornersMask">Use Round Corners Mask</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbUseRoundCornersMask" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbRoundCornerMaskColor">Round Corner Mask Color</asp:Label>
                    </td>
                    <td class="settingField">
                        <span><asp:TextBox runat = "server" ID="tbRoundCornerMaskColor" class="tbColor" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbShowBottomButtons">Show Bottom Buttons</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbShowBottomButtons" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbShowPlayPauseControls">Show Play Pause Controls</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbShowPlayPauseControls" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbFadeColor">Fade Color</asp:Label>
                    </td>
                    <td class="settingField">
                        <span><asp:TextBox runat = "server" ID="tbFadeColor" class="tbColor" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbShowTopTitle">Show Top Title</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbShowTopTitle" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbTopTitleBackground">Top Title Background</asp:Label>
                    </td>
                    <td class="settingField">
                         <span><asp:TextBox runat = "server" ID="tbTopTitleBackground" class="tbColor" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbTopTitleBgTransparency">Top Title Bg Transparency</asp:Label>
                    </td>
                    <td class="settingField">
                         <asp:TextBox runat = "server" ID="tbTopTitleBgTransparency" class="tbNumber" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbTopTitleTextColor">Top Title Text Color</asp:Label>
                    </td>
                    <td class="settingField">
                        <span><asp:TextBox runat = "server" ID="tbTopTitleTextColor" class="tbColor" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbShowTimerBar">Show Timer Bar</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbShowTimerBar" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbSlideButtonsColor">Slide Buttons Color</asp:Label>
                    </td>
                    <td class="settingField">
                         <span><asp:TextBox runat = "server" ID="tbSlideButtonsColor" class="tbColor" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbSlideButtonsNumberColor">Slide Buttons Number Color</asp:Label>
                    </td>
                    <td class="settingField">
                         <span><asp:TextBox runat = "server" ID="tbSlideButtonsNumberColor" class="tbColor" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="ddSlideButtonsType">Slide Buttons Type</asp:Label>
                    </td>
                    <td class="settingField">
                         <asp:DropDownList runat = "server" ID="ddSlideButtonsType" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbSlideButtonsXoffset">Slide Buttons X Offset</asp:Label>
                    </td>
                    <td class="settingField">
                         <asp:TextBox runat = "server" ID="tbSlideButtonsXoffset" class="tbNumber" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbSlideButtonsYoffset">Slide Buttons Y Offset</asp:Label>
                    </td>
                    <td class="settingField">
                         <asp:TextBox runat = "server" ID="tbSlideButtonsYoffset" class="tbNumber" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbTransparentBackground">Transparent Background</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbTransparentBackground" />
                    </td>
                    <td class = "grayed_desc">
                        Description Lorem Ipsum
                    </td>
                </tr>
            </table>
        </div>

        <div id = "tabs-main-slides">
            test
        </div>

        <div id = "tabs-main-activate">
            test
        </div>

    </div>


    <div class="footer">
        <div class="btnPane">
            <a href = "<%= System.Web.HttpUtility.UrlDecode(Request.QueryString["rurl"]) %>" style="color: #d24242; padding: 1px 10px; margin-right: 10px; font-weight: normal;" >Cancel</a>
            <asp:Button runat="server" OnClick="SaveSettings" class="ui-state-default" style="padding: 4px 10px;" Text="Save" />
        </div>
        <div style="clear:both;"></div>
    </div>

    </form>

    <script type="text/javascript">
    
        jQuery(document).ready(function() {
            jQuery("#mainTabs").tabs();

            jQuery(".tbColor").each(function() {
                var _this = jQuery(this);
                _this.ColorPicker({
                    onSubmit: function(hsb, hex, rgb) {
                        _this.val("#" + hex);
                        _this.parent().css("background-color", "#"+hex);
                        jQuery(".colorpicker").hide();
                    },
                    onBeforeShow: function () {
                        jQuery(this).ColorPickerSetColor(this.value);
                    }
                });
            });
            
            jQuery(".colorpicker").css("z-index", "1100");
        });

    </script>

</body>
</html>
