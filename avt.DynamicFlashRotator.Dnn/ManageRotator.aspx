<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ManageRotator.aspx.cs" Inherits="avt.DynamicFlashRotator.Net.WebManage.ManageRotator" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage ALLinONE Rotator Settings</title>
    <link type ="text/css" rel="stylesheet" href = "<%=TemplateSourceDirectory + "/res/ui-lightness/jquery-ui.css"%>" />
    <link type ="text/css" rel="stylesheet" href = "<%=TemplateSourceDirectory + "/res/js/colorpicker/css/colorpicker.css"%>" />
    <link type ="text/css" rel="stylesheet" href = "<%=TemplateSourceDirectory + "/res/jquery.bt.css"%>" />
    <link type ="text/css" rel="stylesheet" href = "<%=TemplateSourceDirectory + "/res/manage.css"%>" />

    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/jquery.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/jquery-ui.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/excanvas.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/jquery.bt.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/colorpicker/js/colorpicker.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/colorpicker/js/eye.js"></script>
    <script type = "text/javascript" src = "<%=TemplateSourceDirectory %>/res/js/colorpicker/js/utils.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:HiddenField runat="server" ID ="hdnSlideXml" />

    <div>
        <div class="btnPane">
            <a href = "<%= System.Web.HttpUtility.UrlDecode(Request.QueryString["rurl"]) %>" style="color: #525252; padding: 1px 10px; margin-right: 10px; font-weight: normal;" >Cancel</a>
            <asp:Button runat="server" OnClick="SaveSettings" class="ui-state-default" style="padding: 4px 10px;" Text="Save" OnClientClick="return save();" />
        </div>

        <h1 class="manageTitle" style="font-weight:normal; color: #626262;">Settings for <asp:Label runat="server" ID = "lblControlName" style="color: #C77405; font-weight:bold;"></asp:Label></h1>
        <div style="clear: both;"></div>
    </div>

    <div id = "mainTabs">
        <ul>
            <li><a href="#tabs-main-settings">General Settings</a></li>
            <li><a href="#tabs-main-slides">Slides</a></li>
            <li><a href="#tabs-main-presets">Presets</a></li>
            <li><a href="#tabs-main-library">Object Library</a></li>
            <li><a href="#tabs-main-customize">Order Customization</a></li>
            <li><a href="#tabs-main-activate">Activate</a></li>
        </ul>

        <div id = "tabs-main-settings">
            <table style="">
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbWidth">Width</asp:Label>
                    </td>
                    <td class="settingField">
                         <asp:TextBox runat = "server" ID="tbWidth" class="tbNumber" /> px
                    </td>
                    <td class = "grayed_desc">
                        Determines width of the flash rotator
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbHeight">Height</asp:Label>
                    </td>
                    <td class="settingField">
                         <asp:TextBox runat = "server" ID="tbHeight" class="tbNumber" /> px
                    </td>
                    <td class = "grayed_desc">
                        Determines height of the flash rotator
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
                        If this option is selected, the flash control background is transparent so it takes the color of the HTML page
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
                        When a slide is changing a nice fade effect is playing whose color is determined by this field
                    </td>
                </tr>

                <tr class="rowSep"><td colspan="3">&nbsp;</td></tr>

                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbAutoStartSlideShow">Auto Start SlideShow</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbAutoStartSlideShow" />
                    </td>
                    <td class = "grayed_desc">
                        Select this option if you want the slideshow to start automatically.
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbShowPlayPauseControls">Show Play / Pause Controls</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbShowPlayPauseControls" />
                    </td>
                    <td class = "grayed_desc">
                        Use this option to determine if the user is able to control the slideshow
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
                        The Timer Bar appears above the slider buttton; it's a visual indicator of Slide Duration option
                    </td>
                </tr>

                <tr class="rowSep"><td colspan="3">&nbsp;</td></tr>

                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbShowBottomButtons">Show Slide Buttons</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbShowBottomButtons" />
                    </td>
                    <td class = "grayed_desc">
                        The Slide Buttons are used by the user to navigate to any slide
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="ddSlideButtonsType">Slide Buttons Type</asp:Label>
                    </td>
                    <td class="settingField">
                         <asp:DropDownList runat = "server" ID="ddSlideButtonsType" CssClass="ctlSliderBtn" />
                    </td>
                    <td class = "grayed_desc">
                        This option determines how the slide buttons are rendered (they either are square and display numbers or are round buttons)
                    </td>
                </tr>

                <tr class="rowSep"><td colspan="3">&nbsp;</td></tr>

                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbSlideButtonsColor">Buttons Color</asp:Label>
                    </td>
                    <td class="settingField">
                         <span><asp:TextBox runat = "server" ID="tbSlideButtonsColor" class="tbColor" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class = "grayed_desc">
                        Customize color for slide buttons (and play/pause button too)
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbSlideButtonsNumberColor">Buttons Text Color</asp:Label>
                    </td>
                    <td class="settingField">
                         <span><asp:TextBox runat = "server" ID="tbSlideButtonsNumberColor" class="tbColor" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class = "grayed_desc">
                        Use this option to determine color for play/pause button and for slide buttons when the Slide Button Type is set to display numbers
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbSlideButtonsXoffset">Buttons Offset</asp:Label>
                    </td>
                    <td class="settingField">
                         X: <asp:TextBox runat = "server" ID="tbSlideButtonsXoffset" class="tbNumber" />
                         Y: <asp:TextBox runat = "server" ID="tbSlideButtonsYoffset" class="tbNumber" />
                    </td>
                    <td class = "grayed_desc">
                        Distance between left and bottom margins and the buttons
                    </td>
                </tr>

                <tr class="rowSep"><td colspan="3">&nbsp;</td></tr>

                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbUseRoundCornersMask">Use Round Corners Mask</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbUseRoundCornersMask" />
                    </td>
                    <td class = "grayed_desc" rowspan="2">
                        Use this option if you want the rotator to have rounded corners.<br />
                        Make sure to setup the color so it matches the background of your web page.
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbRoundCornerMaskColor">Round Corner Mask Color</asp:Label>
                    </td>
                    <td class="settingField">
                        <span><asp:TextBox runat = "server" ID="tbRoundCornerMaskColor" class="tbColor ctlRoundCornder" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                </tr>

                <tr class="rowSep"><td colspan="3">&nbsp;</td></tr>

                
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="cbShowTopTitle">Show Top Title</asp:Label>
                    </td>
                    <td class="settingField">
                        <asp:CheckBox runat = "server" ID="cbShowTopTitle" />
                    </td>
                    <td class = "grayed_desc" rowspan="4" valign="top" style="padding-top: 20px;">
                        Show or hide the top part with the slide title when the mouse is over a slide button<br />
                        Customize appearance of the slide title using the fields below...
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbTopTitleBackground">Top Title Background</asp:Label>
                    </td>
                    <td class="settingField">
                         <span><asp:TextBox runat = "server" ID="tbTopTitleBackground" class="tbColor ctlTopTitle" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbTopTitleBgTransparency">Top Title Bg Transparency</asp:Label>
                    </td>
                    <td class="settingField">
                         <asp:TextBox runat = "server" ID="tbTopTitleBgTransparency" style="width:30px;" class="tbNumber tbRange ctlTopTitle" />
                    </td>
                </tr>
                <tr>
                    <td class="settingField">
                        <asp:Label runat="server" AssociatedControlID="tbTopTitleTextColor">Top Title Text Color</asp:Label>
                    </td>
                    <td class="settingField">
                        <span><asp:TextBox runat = "server" ID="tbTopTitleTextColor" class="tbColor ctlTopTitle" />&nbsp;&nbsp;&nbsp;</span>
                    </td>
                </tr>
            </table>
        </div>

        <div id = "tabs-main-slides">
            
            <ul style="display:none;">
                <li id = "slideTpl" class="slideRoot">
                    <div class="dragHandle">
                        <div style="margin: 6px;">
                            <div class="slideIndex">1</div>
                            <br />
                            drag here
                        </div>
                    </div>

                    <div class="pnlSlideExtra">
                        <a href = "#" class="slideExtraBtn" onclick="cloneSlide(jQuery(this).parents('.slideRoot:first')); return false;" style="margin-top: 70px">Clone</a><a href = "#" class="slideExtraBtn" onclick="deleteSlide(jQuery(this).parents('.slideRoot:first')); return false;">Delete</a>
                    </div>

                    <div class = "pnlSlideOptGroups">
                        <a href = "#" class="slideOptsGroup ui-state-default" style="margin-top: 10px" onclick="return false;">General</a>
                        <a href = "#" class="slideOptsGroup ui-state-default" onclick="return false;">Slide Link</a>
                        <a href = "#" class="slideOptsGroup ui-state-hover" onclick="return false;">Graphics / Content</a>
                        <a href = "#" class="slideOptsGroup ui-state-default" onclick="return false;">Music</a>
                    </div>

                    <div class="pnlSlideOpts pnlSlideOptsGeneral">
                        <div style = "margin: 8px;">
                            <span class="slideId">-1</span>
                            <div class = "fieldRow ui-widget-content">
                                <b>Slide Title: </b> 
                                <input type="text" class = "tbSlideTitle" style = "width: 320px;" value="<%= DefaultSlide.Title %>" />
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Background Gradient: </b>
                                from <span><input type="text" style = "width: 60px;" class="tbColor tbColortbBkGradFrom" value="<%= ColorToHex(DefaultSlide.BackgroundGradientFrom) %>" />&nbsp;&nbsp;&nbsp;</span>
                                to <span><input type="text" style = "width: 60px;" class="tbColor tbColortbBkGradTo" value="<%= ColorToHex(DefaultSlide.BackgroundGradientTo) %>" />&nbsp;&nbsp;&nbsp;</span>
                            </div>
                             <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Duration: </b>
                                <input type="text" style = "width: 24px;" class="tbDuration" value="<%= DefaultSlide.DurationSeconds %>" /> seconds
                            </div>
                        </div>
                    </div>

                    <div class="pnlSlideOpts pnlSlideOptsLink">
                        <div style = "margin: 8px;">
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Open </b> 
                                <select class = "ddUrl ddLinkUrl">
                                    <option>http://</option>
                                    <option>https://</option>
                                    <option>ftp://</option>
                                    <option>other</option>
                                </select>
                                <input type="text" class = "tbUrl tbLinkUrl" style = "width: 200px;" value="<%= DefaultSlide.SlideUrl %>" />
                                <b>in </b> 
                                <asp:DropDownList runat="server" id="ddLinkTarget" class="ddLinkTarget" style="width:130px" onchange="jQuery(this).toggle(jQuery(this).val() != 'other'); jQuery(this).parents('.fieldRow:first').find('.pnlLinkTarget').toggle(jQuery(this).val() == 'other').filter(':visible').find(':input').focus().select();"></asp:DropDownList>
                                <span class = "pnlLinkTarget" style="display:none;">
                                    <input type="text" class = "tbLinkTarget" style = "width: 116px; " value="<%= DefaultSlide.Target %>" />
                                    <a href = "javascript: ;" onclick="jQuery(this).parents('.pnlLinkTarget:first').hide(); jQuery(this).parents('.fieldRow:first').find('.ddLinkTarget').show().val('_self');">^</a>
                                </span>
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <label style="font-weight:bold;">
                                    <input type="checkbox" class="cbLinkAnywhere" style = "" <%= DefaultSlide.ClickAnywhere ? "checked=\"checked\"" : "" %> />
                                    <b>Click Anywhere inside the Slide to open link</b> 
                                </label>
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <label style="font-weight:bold;">
                                    <input type="checkbox" class="cbLinkCaption" style = "" onclick="checkLinkCaption(jQuery(this).parents('.pnlSlideOptsLink:first'), this.checked);" />
                                    <b>Display link button under slide text</b> 
                                </label>
                            </div>
                            
                            <div class = "fieldRow ui-widget-content pnlLinkButtonCaption" style="clear: left; padding-left: 30px;">
                                <b>Button Caption: </b> 
                                <input type="text" class="tbLinkCaption" style = "width: 200px;" value="<%= DefaultSlide.ButtonCaption %>" />
                            </div>

                        </div>
                    </div>

                    <div class="pnlSlideOpts pnlSlideOptsContent">
                        
                        <div style = "height: 84px; margin: 8px 8px 2px 120px; overflow: auto; padding-left: 40px; position: absolute;" class="pnlSlideObjList_empty">
                            <div style = "padding: 16px 0 0 30px; font-size: 16px; color: #b2b2b2;">
                                No objects defined for this slide!<br />
                                Use buttons below to add objects...
                            </div>
                        </div>

                        <ul style = "height: 84px; margin: 8px 8px 2px 8px; overflow: auto; padding-left: 40px; " class="pnlSlideObjList">
                        </ul>
                        
                        <div style = "background-color: #F2F2FF; border-top: 1px solid #C2C2C2; padding: 3px 4px 2px 186px;">
                            <a href = "#" onclick="addSlideText(jQuery(this).parents('.slideRoot:first')); return false;" class="btnAddObject btnAddObjectText"><img src="<%= TemplateSourceDirectory %>/res/img/add.gif" border="0" /> Add Text</a>
                            <a href = "#" onclick="addSlideImage(jQuery(this).parents('.slideRoot:first')); return false;" class="btnAddObject btnAddObjectImage"><img src="<%= TemplateSourceDirectory %>/res/img/add.gif" border="0" /> Add Image/Flash</a>
                        </div>
                    </div>

                    <div class="pnlSlideOpts pnlSlideOptsMp3">
                        <div style = "margin: 8px;">
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>MP3 URL: </b> 
                                <select class = "ddUrl ddMp3Url">
                                    <option>http://</option>
                                    <option>https://</option>
                                    <option>ftp://</option>
                                    <option>other</option>
                                </select>
                                <input type="text" style = "width: 260px;" class = "tbUrl tbMp3Url" value="<%= DefaultSlide.Mp3Url %>" />
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Icon Color: </b> 
                                <span><input type="text" style = "width: 60px;" class="tbColor tbMp3IconColor" value="<%= ColorToHex(DefaultSlide.IconColor) %>" />&nbsp;&nbsp;&nbsp;</span>
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Show Player: </b> 
                                <input type="checkbox" class="cbMp3ShowPlayer" <%= DefaultSlide.ShowPlayer ? "checked='checked'" :"" %> />
                            </div>
                        </div>
                    </div>

                    <div style = "clear: both;"></div>
                </li>
            </ul>

            <ul id = "slides">
                
            </ul>
            <a href = "#" onclick="addSlide(true); return false;"><img src="<%= TemplateSourceDirectory %>/res/img/add.gif" border="0" /> Add Slide</a>
        </div>

        <div id = "tabs-main-presets">
            test
        </div>

        <div id = "tabs-main-library">
            test
        </div>

        <div id = "tabs-main-customize">
            test
        </div>

        <div id = "tabs-main-activate">
            test
        </div>

    </div>


    <div id = "dlgObjectSettings" style="overflow:visible;">

        <div id = "objSettingsTabs">
            <ul>
                <li><a href="#obj-settings-general">General</a></li>
                <li><a href="#obj-settings-effect">Effects</a></li>
                <li><a href="#obj-settings-transition">Transitions</a></li>
            </ul>

            <div id = "obj-settings-general">
                <div style = "margin: 8px;">
                    <span class="objectId">-1</span>
                    <span class="itemType"></span>
                    
                    <table>
                        <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>

                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Name:</td>
                            <td class = "ui-widget-content"><input type="text" class = "tbObjName tbRequired" style = "width: 200px;" /></td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Position:</td>
                            <td class = "ui-widget-content">
                                <b>X</b> <input type="text" style = "width: 40px;" class="tbObjPosX tbNumber" />
                                <b>Y</b> <input type="text" style = "width: 40px;" class="tbObjPosY tbNumber" />
                                <asp:DropDownList runat="server" ID = "ddVerticalAlgin" class = "ddVerticalAlgin" style="display:none;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content objFieldImgOnly">
                            <td class = "ui-widget-content hdr">Link URL:</td>
                            <td class = "ui-widget-content">
                                <select class = "ddUrl ddLinkUrl">
                                    <option>http://</option>
                                    <option>https://</option>
                                    <option>ftp://</option>
                                    <option>other</option>
                                </select>
                                <input type="text" class = "tbUrl tbLinkUrl" style = "width: 300px;" />
                            </td>
                        </tr>
                        
                        <tr class=""><td colspan="3" style="font-size:5px;">&nbsp;</td></tr>
                        <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>
                        
                        <tr class = "objFieldRow ui-widget-content objFieldImgOnly">
                            <td class = "ui-widget-content hdr">Resource URL:</td>
                            <td class = "ui-widget-content">
                                <select class = "ddUrl ddObjectUrl">
                                    <option>http://</option>
                                    <option>https://</option>
                                    <option>ftp://</option>
                                    <option>other</option>
                                </select>
                                <input type="text" class = "tbUrl tbObjectUrl tbRequired" style = "width: 300px;" />
                            </td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content objFieldTextOnly">
                            <td class = "ui-widget-content hdr">Text:</td>
                            <td class = "ui-widget-content"><textarea class = "tbObjText" style = "width: 380px; height: 60px;"></textarea></td>
                        </tr>
                        
                    </table>
                    
                    <br />
                </div>
            </div>

            <div id = "obj-settings-effect">
                <div style = "margin: 8px;">
                    <table width="480px" cellpadding="0" cellspacing="2" border="0">
                        <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>
                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Opacity:</td>
                            <td class = "ui-widget-content"><input type="text" class = "tbObjOpacity tbNumber tbRange" style = "width: 30px;" /></td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Glow:</td>
                            <td class = "ui-widget-content" style="text-align: center">
                                <b>Size:</b> <input type="text" class = "tbObjGlowSize" style = "width: 30px;" />
                                <b>Color:</b> <span><input type="text" style = "width: 60px;" class="tbColor tbObjGlowColor" />&nbsp;&nbsp;&nbsp;</span>
                                <b>Strength:</b> <input type="text" class = "tbObjGlowStrength" style = "width: 30px;" />
                            </td>
                        </tr>
                    </table>

                    <table width="480px" cellpadding="0" cellspacing="2" border="0" style = "margin-top:8px;" class = "objFieldTextOnly">
                        <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>

                        <tr class = "objFieldRow ui-widget-content objFieldTextOnly">
                            <td class = "ui-widget-content hdr">Text Color:</td>
                            <td class = "ui-widget-content"><span><input type="text" style = "width: 60px;" class="tbColor tbTextColor" />&nbsp;&nbsp;&nbsp;</span></td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content objFieldTextOnly">
                            <td class = "ui-widget-content hdr">Text Background:</td>
                            <td class = "ui-widget-content" style="">
                                <table style="width:300px;margin-left:20px;">
                                    <tr>
                                        <td>Color</td>
                                        <td style="width:220px"><span><input type="text" style = "width: 60px;" class="tbColor tbBgTextColor" />&nbsp;&nbsp;&nbsp;</span></td>
                                    </tr>
                                    <tr>
                                        <td>Opacity</td>
                                        <td><input type="text" class = "tbTextBgOpacity tbNumber tbRange" style = "width: 30px;" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content objFieldTextOnly">
                            <td class = "ui-widget-content hdr">Text Padding:</td>
                            <td class = "ui-widget-content"><input type="text" class = "tbTextPadding tbNumber" style = "width: 60px;" /></td>
                        </tr>
                    </table>
                </div>
            </div>

            <div id = "obj-settings-transition">
                <div style = "margin: 8px;">
                    <table width="480px" cellpadding="0" cellspacing="2" border="0">
                        <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>
                        
                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr" colspan="2">
                                <table style="margin:0; width:100%;">
                                    <tr>
                                        <td>
                                            <div class = "objFieldImgOnly" style="border-right: 1px solid #d2d2d2;">
                                                <b>Delay: </b> 
                                                <input type="text" class = "tbObjDelay tbNumber" style = "width: 60px;" /> seconds
                                            </div>
                                        </td>
                                        <td style="padding-left: 10px;">
                                            <b>Duration: </b> 
                                            <input type="text" class = "tbObjDuration tbNumber" style = "width: 60px;" /> seconds
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table width="480px" cellpadding="0" cellspacing="2" border="0" style="margin-top: 8px;">
                        <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>
                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Appear with:</td>
                            <td class = "ui-widget-content" style="width: 320px;">
                                <%--<asp:DropDownList runat="server" ID = "ddAppearMode" class = "ddAppearMode" " style="width:160px" ></asp:DropDownList>--%>
                                <asp:RadioButtonList runat="server" ID = "ddAppearMode" class = "ddAppearMode" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>

                    <table width="480px" cellpadding="0" cellspacing="2" border="0" id = "pnlObjSlideParams" style="display:none; margin-top: 8px;">
                        <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>

                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Appear from:</td>
                            <td class = "ui-widget-content" style="width: 320px;">
                                <asp:RadioButtonList runat="server" ID = "ddObjAppearFromText" class = "ddObjAppearFrom ddObjAppearFromText" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                                <asp:RadioButtonList runat="server" ID = "ddObjAppearFromImage" class = "ddObjAppearFrom ddObjAppearFromImage" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Move Type:</td>
                            <td class = "ui-widget-content" style="width: 320px;">
                                <asp:RadioButtonList runat="server" ID = "ddObjMoveType" class = "ddObjMoveType" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Easing Type:</td>
                            <td class = "ui-widget-content" style="width: 320px;">
                                <asp:RadioButtonList runat="server" ID = "ddObjEasingType" class = "ddObjEasingType" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class = "objFieldRow ui-widget-content">
                            <td class = "ui-widget-content hdr">Effect:</td>
                            <td class = "ui-widget-content" style="width: 320px;">
                                <asp:DropDownList runat="server" ID = "ddObjEffect" class = "ddObjEffect" style="width: 160px"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>

        </div>

        <div id = "slideObjErr" style="position:absolute;margin-top:-12px;">
            There are errors in your settings!
            Correct them then click Save again...
        </div>

        <a href = "#" onclick="deleteSlideObject(); return false;" id="btnDeleteObj" style="color:red;position:absolute;display:block;bottom: -45px;">Delete This Object</a>

    </div>


    <div class="footer">
        <div class="btnPane">
            <a href = "<%= System.Web.HttpUtility.UrlDecode(Request.QueryString["rurl"]) %>" style="color: #525252; padding: 1px 10px; margin-right: 10px; font-weight: normal;" >Cancel</a>
            <asp:Button runat="server" OnClick="SaveSettings" class="ui-state-default" style="padding: 4px 10px;" Text="Save" OnClientClick="return save();" />
        </div>
        <div style="clear:both;"></div>
    </div>

    </form>



    <script type="text/javascript">
    
        var btOpts = {
            fill: "#FDF9E1",
            cssStyles: {"color":"#C77405","font-weight":"bold"},
            strokeStyle: "#FBCB09",
            strokeWidth: 1,
            spikeLength: 10,
            spikeGirth: 20
        }

        var g_isDragging = false;

        jQuery(document).ready(function() {
            jQuery("#mainTabs").tabs();

            applyColorpicker(jQuery("#tabs-main-settings"));

            jQuery("#<%= cbShowBottomButtons.ClientID %>").change(function() {
                this.checked ? jQuery('.ctlSliderBtn').removeAttr('disabled'):jQuery('.ctlSliderBtn').attr('disabled','disabled');
            }).change();

            jQuery("#<%= cbUseRoundCornersMask.ClientID %>").change(function() {
                this.checked ? jQuery('.ctlRoundCornder').removeAttr('disabled'):jQuery('.ctlRoundCornder').attr('disabled','disabled');
            }).change();

            jQuery("#<%= cbShowTopTitle.ClientID %>").change(function() {
                this.checked ? jQuery('.ctlTopTitle').removeAttr('disabled'):jQuery('.ctlTopTitle').attr('disabled','disabled');
            }).change();

            // init slides
            jQuery("#slides").sortable({
                handle: ".dragHandle",
                placeholder: 'slidePlaceholder',
                update: function(event, ui) {
                    updateSlideIndexes();
                }
            });

            jQuery(".slideOptsGroup")
                .mouseover(function() {
                    if (jQuery(this).hasClass("ui-state-hover"))
                        return;
                    jQuery(this).removeClass("ui-state-default").addClass("ui-state-active");
                })
                .mouseout(function() {
                    if (jQuery(this).hasClass("ui-state-hover"))
                        return;
                    jQuery(this).removeClass("ui-state-active").addClass("ui-state-default");
                })
                .click(function() {
                    jQuery(this).parents(".pnlSlideOptGroups:first").find(".slideOptsGroup").removeClass("ui-state-hover").addClass("ui-state-default");
                    jQuery(this).addClass("ui-state-hover").removeClass("ui-state-default").removeClass("ui-state-active");
                    
                    var _root = jQuery(this).parents(".slideRoot:first");
                    _root.find(".pnlSlideOpts").hide();
                    _root.find(".pnlSlideOpts").eq(_root.find(".slideOptsGroup").index(jQuery(this))).show();
                });

            var slides = <%= hdnSlideXml.Value %>;
            for (var i  = 0; i < slides.length; i++) {
                loadSlide(slides[i]);
            }

            jQuery("#dlgObjectSettings").dialog({
                bgiframe: false,
                autoOpen: false,
                title: "Object Settings",
                width: 600,
                modal: true,
                resizable: false,
                closeOnEscape: true,

                buttons: {
                    'Save': function() {
                        var _dlg = jQuery("#dlgObjectSettings");

                        _dlg.find("#slideObjErr").hide();
                        _dlg.find(".tbErr").removeClass("tbErr");

                        // validate
                        var isValid = true;
                        _dlg.find(".tbRequired").each(function() { // .not(_dlg.find(".itemType").text().toLowerCase() == "text" ? ".objFieldImgOnly" :".objFieldTextOnly")
                            
                            if (_dlg.find(".itemType").text().toLowerCase() == "text") {
                                if (jQuery(this).parents(".objFieldImgOnly").size() > 0)
                                    return;
                            } else {
                                if (jQuery(this).parents(".objFieldTextOnly").size() > 0)
                                    return;
                            }

                            if (jQuery.trim(jQuery(this).val()).length == 0) {
                                jQuery(this).addClass("tbErr");
                                
                                // make sure the tab with the error is visible
                                if (isValid) {
                                    _dlg.find("[href='#"+jQuery(this).parents(".ui-tabs-panel:first").attr("id")+"']").click();
                                    jQuery(this).focus();
                                    isValid = false;
                                }
                            }
                        });

                        if (!isValid) {
                            jQuery("#slideObjErr").show();
                            return;
                        }

                        var slideObj = saveSlideObjectAsJson(_dlg);
                        if (_dlg[0].slideObjItem) {
                            _dlg[0].slideObjItem[0].objData = slideObj;
                            _dlg[0].slideObjItem.find(".objTitle").text(slideObj.name);
                        } else {
                            appendSlideObject(slideObj);
                        }

                        // reset to first tab
                        jQuery("#objSettingsTabs").tabs("select",0);

                        jQuery("#dlgObjectSettings").dialog('close');
                    },
                    'Cancel': function() {
                        // reset to first tab
                        jQuery("#objSettingsTabs").tabs("select",0);

                        jQuery("#dlgObjectSettings").dialog('close');
                    }
                },
                close: function() {
                }

            });

            jQuery("#objSettingsTabs").tabs({
                show: function(event, ui) { 
                    if (ui.index == 1) {
                        try {
                            var _dlg = jQuery("#dlgObjectSettings");
                            _dlg.find(".tbObjOpacity").next("div").children("div:first").slider("value", jQuery(".tbObjOpacity").val());
                            _dlg.find(".tbTextBgOpacity").next("div").children("div:first").slider("value", jQuery(".tbTextBgOpacity").val());
                        } catch (e) {}
                    }
                    
                }
            });
            applyColorpicker(jQuery("#objSettingsTabs"));

            jQuery(".tbRange").each(function() {
                var _s = jQuery("<div style='margin-left: 10px; margin-right:"+(jQuery(this).width()+20)+"px;padding-top:5px;'><div></div></div>");
                _s.find("div").slider({
                    value: jQuery(this).val(),
                    orientation: "horizontal",
                    min: jQuery(this).attr("min"),
                    max: jQuery(this).attr("max"),
                    range: "min",
                    animate: true,
                    slide: function(event, ui) {
                        jQuery(this).parent().prev("input").val(ui.value);
                    }
                });
                jQuery(this).css("float", "right");
                jQuery(this).after(_s);
                jQuery(this).keyup(function() {
                    if (isNaN(parseInt(jQuery(this).val()))) {
                        return;
                    }
                    try {
                        jQuery(this).next().find("div").slider("value", jQuery(this).val());
                    } catch (e) {}
                });
            });


            jQuery(".ddAppearMode").find(":input").change(function() {
                jQuery(this).val() == 'Slide' ? jQuery('#pnlObjSlideParams').show() : jQuery('#pnlObjSlideParams').hide();
            });

        });


        function appendSlideObject(slideObj, slideRoot) {
            if (!slideRoot) {
                slideRoot = jQuery(".slideRootActive");
            }

            var _item = jQuery("<li class='slideObject'></li>");
            // determine type
            if (slideObj.itemType.toLowerCase() == "text") {
                _item.addClass("slideObjectText");
            } else if (slideObj.itemType.toLowerCase() == "image") {
                var resUrl = jQuery.trim(slideObj.resUrl);
                if (resUrl.indexOf(".swf") == resUrl.length - 4) {
                    _item.addClass("slideObjectSwf");
                } else {
                    _item.addClass("slideObjectImg");
                }
            }

            _item.append("<div class='dragObj'><img width='20' src='<%= TemplateSourceDirectory %>/res/img/drag.png' /></div>");
            _item.append("<div style='text-align: center;' class='objTitle'>" + slideObj.name + "</div>");

           slideRoot.find(".pnlSlideObjList").append(_item);
           _item[0].objData = slideObj;

            _item
                .mouseover(function() {
                    jQuery(this).addClass("slideObjectHover");
                })
                .mouseout(function() {
                    jQuery(this).removeClass("slideObjectHover");
                })
                .click(function() {
                    if (g_isDragging === true)
                        return;
                    openSlideObjectSettings(jQuery(this).parents(".slideRoot:first"), this.objData.itemType, jQuery(this));
                });

            checkSlideObjects(slideRoot);
            
        }

        function checkSlideObjects(slideRoot) {
            if (slideRoot.find(".slideObject").size() == 0) {
                slideRoot.find(".pnlSlideObjList_empty").show();
            } else {
                slideRoot.find(".pnlSlideObjList_empty").hide();
            }

            if(slideRoot.find(".slideObjectText").size() == 0) {
                slideRoot.find(".btnAddObjectText").removeClass("ui-state-disabled");
                slideRoot.find(".btnAddObjectText").removeAttr("bt-xtitle").btOff();
            } else {
                slideRoot.find(".btnAddObjectText").addClass("ui-state-disabled");
                slideRoot.find(".btnAddObjectText").attr("title", "Limitation with version 1.0: slides can only contain one text object.").bt(btOpts);
            }
        }

        function updateSlideIndexes() {
            var index = 1;
            jQuery("#slides").find(".slideIndex").each(function() {
                jQuery(this).text(index++);
            });
        }

        function addSlide(bNew) {
            var _item = jQuery("#slideTpl").clone().removeAttr("id");
            _item.appendTo(jQuery("#slides"));

            _item.find(".slideExtraBtn,.btnAddObject").button();
            //_item.find(".pnlSlideOptsGeneral").show();
            
            applyColorpicker(_item);

            updateSlideIndexes();
            
            if (bNew) {
                _item.find(".slideOptsGroup").eq(0).click();
                _item.find(".tbSlideTitle").focus().select();
            } else {
                _item.find(".pnlSlideOptsContent").show();
            }

            return _item;
        }

        function loadSlide(slide) {
            var slideRoot = addSlide();

            slideRoot.find(".slideId").text(slide.id);

            slideRoot.find(".tbSlideTitle").val(slide.title);
            slideRoot.find(".tbDuration").val(slide.duration);
            slideRoot.find(".tbColortbBkGradFrom").val(slide.bkGradFrom);
            slideRoot.find(".tbColortbBkGradTo").val(slide.bkGradTo);

            fillUrl(slideRoot.find(".tbLinkUrl"), slideRoot.find(".ddLinkUrl"), slide.linkUrl);
            if (slide.linkCaption.length > 0) {
                slideRoot.find(".tbLinkCaption").val(slide.linkCaption);
                slideRoot.find(".cbLinkCaption").attr("checked","checked");
            } else {
                checkLinkCaption(slideRoot.find('.pnlSlideOptsLink:first'), false);
            }
            
            
            if (slide.linkTarget != '_self' && slide.linkTarget != '_blank' && slide.linkTarget != '_parent') {
                slideRoot.find(".tbLinkTarget").val(slide.linkTarget);
                slideRoot.find(".ddLinkTarget").val("other").change();
            } else {
                slideRoot.find(".ddLinkTarget,.tbLinkTarget").val(slide.linkTarget);
            }

            slide.linkClickAnywhere ? slideRoot.find(".cbLinkAnywhere").attr("checked","checked") : slideRoot.find(".cbLinkAnywhere").removeAttr("checked");

            //slide.useTextsBk == "true" ? slideRoot.find(".cbUseTextsBackground").attr("checked","checked") : slideRoot.find(".cbUseTextsBackground").removeAttr("checked");

            fillUrl(slideRoot.find(".tbMp3Url"), slideRoot.find(".ddMp3Url"), slide.mp3Url);
            slideRoot.find(".tbMp3IconColor").val(slide.mp3IconColor);
            slide.mp3ShowPlayer ? slideRoot.find(".cbMp3ShowPlayer").attr("checked","checked") : slideRoot.find(".cbMp3ShowPlayer").removeAttr("checked");

            for (var i =0; i < slide.slideObjects.length; i++) {
                appendSlideObject(slide.slideObjects[i], slideRoot);
            }
            slideRoot.find(".pnlSlideObjList").sortable({
                connectWith: ".pnlSlideObjList",
                handle: ".dragObj",
                placeholder: 'objPlaceholder',
                cancel: ".slideObjectText",

                start: function(event, ui) {
                    g_isDragging = true;
                },

                stop: function(event, ui) {
                    jQuery(".slideObjectHover").removeClass("slideObjectHover");
                    setTimeout(function() { g_isDragging = false; }, 100);
                }
                 
            }).disableSelection();
        }

        function cloneSlide(slideRoot) {
            var _new = slideRoot.clone();
            _new.find(".tbSlideTitle").val(_new.find(".tbSlideTitle").val() + " - Copy");
            _new.find(".slideId").text("-1");
            slideRoot.after(_new);
            _new.find(".slideOptsGroup:first").click();
            _new.find(".tbSlideTitle").focus().select();
            updateSlideIndexes();

            // also clone all object data
            var iobj = 0;
            slideRoot.find(".slideObject").each(function() {
                _new.find(".slideObject").eq(iobj)[0].objData = jQuery.extend({}, this.objData, {id:-1, name: this.objData.name + " - Copy"});
                _new.find(".slideObject").text(_new.find(".slideObject").eq(iobj)[0].objData.name);
                iobj++;
            });
        }

        function deleteSlide(slideRoot) {
            if (!confirm("Are you sure you want to delete this slide?\nData will be permanently lost after you hit Save...")) {
                return;
            }

            slideRoot.remove();
            updateSlideIndexes();
        }

        function deleteSlideObject(slideOjectRoot) {
            if (!slideOjectRoot) {
                slideOjectRoot = jQuery("#dlgObjectSettings")[0].slideObjItem;
            }
            if (confirm("Are you sure you want to delete this Slide Object?")) {
                var slideRoot = slideOjectRoot.parents(".slideRoot:first");
                slideOjectRoot.remove();
                checkSlideObjects(slideRoot);
                jQuery("#dlgObjectSettings").dialog('close');
            }
            
        }

        function addSlideText(slideRoot) {
            if (slideRoot.find(".btnAddObjectText").hasClass("ui-state-disabled"))
                return;
            openSlideObjectSettings(slideRoot, "text");
        }

        function addSlideImage(slideRoot) {
            openSlideObjectSettings(slideRoot, "image");
        }

        function openSlideObjectSettings(slideRoot, itemType, slideObjItem) {
            var _dlg = jQuery("#dlgObjectSettings");

            _dlg.find("#slideObjErr").hide();
            _dlg.find(".tbErr").removeClass("tbErr");

            _dlg.find(".itemType").text(itemType);

            _dlg.find(".objFieldImgOnly, .objFieldTextOnly, .ddObjAppearFrom").show();
            if (itemType.toLowerCase() =="text") {
                _dlg.find(".objFieldImgOnly").hide();
                _dlg.dialog("option", "title", "Text Settings");
            } else {
                _dlg.find(".objFieldTextOnly").hide();
                _dlg.dialog("option", "title", "Image/Flash Settings");
            }
            
            _dlg[0].slideObjItem = null;
            if (slideObjItem) {
                _dlg.find(".objectId").text(slideObjItem[0].objData.id);
                _dlg.find(".tbObjName").val(slideObjItem[0].objData.name);
                fillUrl(_dlg.find(".tbLinkUrl"), _dlg.find(".ddLinkUrl"), slideObjItem[0].objData.linkUrl);
                _dlg.find(".tbObjText").val(slideObjItem[0].objData.htmlContents);
                fillUrl(_dlg.find(".tbObjectUrl"), _dlg.find(".ddObjectUrl"), slideObjItem[0].objData.resUrl);
                _dlg.find(".tbObjDelay").val(slideObjItem[0].objData.delay);
                _dlg.find(".tbObjDuration").val(slideObjItem[0].objData.duration);
                _dlg.find(".tbObjOpacity").val(slideObjItem[0].objData.opacity);
                _dlg.find(".tbObjPosX").val(slideObjItem[0].objData.posx);
                _dlg.find(".tbObjPosY").val(slideObjItem[0].objData.posy);
                _dlg.find(".ddVerticalAlgin").val(slideObjItem[0].objData.valign);
                _dlg.find(".tbObjGlowSize").val(slideObjItem[0].objData.glowSize);
                _dlg.find(".tbObjGlowColor").val(slideObjItem[0].objData.glowColor);
                _dlg.find(".tbObjGlowStrength").val(slideObjItem[0].objData.glowStrength);

                _dlg.find(".tbTextColor").val(slideObjItem[0].objData.textColor);
                _dlg.find(".tbBgTextColor").val(slideObjItem[0].objData.textBackgroundColor);
                _dlg.find(".tbTextBgOpacity").val(slideObjItem[0].objData.textBackgroundOpacity);
                _dlg.find(".tbTextPadding").val(slideObjItem[0].objData.textBackgroundPadding);

                //_dlg.find(".ddAppearMode").val(slideObjItem[0].objData.appearMode).change();
                _dlg.find(".ddAppearMode").find("[value="+slideObjItem[0].objData.appearMode+"]").attr("checked", "checked").change();

                if (itemType.toLowerCase() =="text") {
                    _dlg.find(".ddObjAppearFromText").find("[value="+slideObjItem[0].objData.slideFrom+"]").attr("checked", "checked");
                    _dlg.find(".ddObjAppearFromImage").hide();
                } else {
                    _dlg.find(".ddObjAppearFromImage").find("[value="+slideObjItem[0].objData.slideFrom+"]").attr("checked", "checked");
                    _dlg.find(".ddObjAppearFromText").hide();
                }
                
                _dlg.find(".ddObjMoveType").find("[value="+slideObjItem[0].objData.slideMoveType+"]").attr("checked", "checked");
                _dlg.find(".ddObjEasingType").find("[value="+slideObjItem[0].objData.slideEasingType+"]").attr("checked", "checked");
                _dlg.find(".ddObjEffect").val(slideObjItem[0].objData.effectAfterSlide);
                
                _dlg[0].slideObjItem = slideObjItem;
                jQuery("#btnDeleteObj").show();
            } else {
                // load defaults
                _dlg.find(".objectId").text("-1");
                
                if (itemType.toLowerCase() =="text") {
                    _dlg.find(".tbObjName").val("New Text");
                } else {
                    _dlg.find(".tbObjName").val("New Graphic");
                }

                fillUrl(_dlg.find(".tbLinkUrl"), _dlg.find(".ddLinkUrl"), "<%= DefaultObject.Link %>");
                _dlg.find(".tbObjText").val("<%= DefaultObject.Text %>");
                fillUrl(_dlg.find(".tbObjectUrl"), _dlg.find(".ddObjectUrl"), "<%= DefaultObject.ObjectUrl %>");
                _dlg.find(".tbObjDelay").val("<%= DefaultObject.TimeDelay %>");
                _dlg.find(".tbObjDuration").val("<%= DefaultObject.TransitionDuration %>");
                _dlg.find(".tbObjOpacity").val("<%= DefaultObject.Opacity %>");
                _dlg.find(".tbObjPosX").val(<%= DefaultObject.Xposition %>);
                _dlg.find(".tbObjPosY").val(<%= DefaultObject.Yposition %>);
                _dlg.find(".ddVerticalAlgin").val("<%= DefaultObject.VerticalAlign.ToString() %>");
                _dlg.find(".tbObjGlowSize").val(<%= DefaultObject.GlowSize %>);
                _dlg.find(".tbObjGlowColor").val("<%= avt.DynamicFlashRotator.Net.ColorExt.ColorToHexString(DefaultObject.GlowColor) %>");
                _dlg.find(".tbObjGlowStrength").val(<%= DefaultObject.GlowStrength %>);

                _dlg.find(".tbTextColor").val("<%= avt.DynamicFlashRotator.Net.ColorExt.ColorToHexString(DefaultObject.TextColor) %>");
                _dlg.find(".tbBgTextColor").val("<%= avt.DynamicFlashRotator.Net.ColorExt.ColorToHexString(DefaultObject.TextBackgroundColor) %>");
                _dlg.find(".tbTextBgOpacity").val(<%= DefaultObject.TextBackgroundOpacity %>);
                _dlg.find(".tbTextPadding").val(<%= DefaultObject.TextBackgroundPadding %>);

                _dlg.find(".ddAppearMode").find("[value=<%= DefaultObject.AppearMode.ToString() %>]").attr("checked", "checked").change();

                if (itemType.toLowerCase() =="text") {
                    _dlg.find(".ddObjAppearFromText").find("[value=<%= DefaultObject.SlideFrom.ToString() %>]").attr("checked", "checked");
                    _dlg.find(".ddObjAppearFromImage").hide();
                } else {
                    _dlg.find(".ddObjAppearFromImage").find("[value=<%= DefaultObject.SlideFrom.ToString() %>]").attr("checked", "checked");
                    _dlg.find(".ddObjAppearFromText").hide();
                }
                
                _dlg.find(".ddObjMoveType").find("[value=<%= DefaultObject.SlideMoveType.ToString() %>]").attr("checked", "checked");
                _dlg.find(".ddObjEasingType").find("[value=<%= DefaultObject.SlideEasingType.ToString() %>]").attr("checked", "checked");
                _dlg.find(".ddObjEffect").val("<%= DefaultObject.EffectAfterSlide.ToString() %>");
                jQuery("#btnDeleteObj").hide();
            }

            jQuery("#objSettingsTabs").tabs("select",0);
            _dlg.dialog("open");

            _dlg.find(".tbObjName").focus().select();
            
            jQuery(".ui-button").removeClass("ui-state-focus");
            jQuery(".slideRoot").removeClass("slideRootActive");
            slideRoot.addClass("slideRootActive");

            applyColorPreviews(_dlg);
        }

        function saveSlideObjectAsJson(dlg) {
            return { 
                id: dlg.find(".objectId").text(),
                name: dlg.find(".tbObjName").val(),
                linkUrl:formatUrl(dlg.find(".tbLinkUrl"), dlg.find(".ddLinkUrl")),
                htmlContents: dlg.find(".tbObjText").val(),
                resUrl:formatUrl(dlg.find(".tbObjectUrl"), dlg.find(".ddObjectUrl")),
                itemType: dlg.find(".itemType").text(),
                delay: dlg.find(".tbObjDelay").val(),
                duration: dlg.find(".tbObjDuration").val(),
                opacity: dlg.find(".tbObjOpacity").val(),
                posx: dlg.find(".tbObjPosX").val(),
                posy: dlg.find(".tbObjPosY").val(),
                valign: dlg.find(".ddVerticalAlgin").val(),
                
                glowSize: dlg.find(".tbObjGlowSize").val(),
                glowColor: dlg.find(".tbObjGlowColor").val(),
                glowStrength: dlg.find(".tbObjGlowStrength").val(),

                textColor: dlg.find(".tbTextColor").val(),
                textBackgroundColor: dlg.find(".tbBgTextColor").val(),
                textBackgroundOpacity: dlg.find(".tbTextBgOpacity").val(),
                textBackgroundPadding: dlg.find(".tbTextPadding").val(),

                appearMode: dlg.find(".ddAppearMode").find(":checked").val(),
                slideFrom: dlg.find(".itemType").text().toLowerCase() == "text" ? dlg.find(".ddObjAppearFromText").find(":checked").val() : dlg.find(".ddObjAppearFromImage").find(":checked").val(),
                slideMoveType: dlg.find(".ddObjMoveType").find(":checked").val(),
                slideEasingType: dlg.find(".ddObjEasingType").find(":checked").val(),
                effectAfterSlide: dlg.find(".ddObjEffect").val()
            };
        }


        function save() {
            var bErr = false;
            jQuery(".fieldRowErr").removeClass("fieldRowErr");
            jQuery(".slideRoot").not("#slideTpl").each(function() {
                if (!validateSlide(jQuery(this))) {
                    bErr = true;
                }
            });

            if (bErr)
                return false;

            var x = "<slides>";
            jQuery(".slideRoot").not("#slideTpl").each(function() {
                x += saveSlideToXml(jQuery(this));
            });
            x += "</slides>";
            jQuery("#<%=hdnSlideXml.ClientID %>").val(x);

            return true;
        }

        function validateSlide(slideRoot) {
            if (isNaN(parseInt(jQuery.trim(slideRoot.find(".tbDuration").val())))) {
                slideRoot.find(".tbDuration").parents(".fieldRow:first").addClass("fieldRowErr");
                return false;
            }
            return true;
        }

        function saveSlideToXml(slideRoot) {
            var x = "<slide>";
            x += "<id>"+ slideRoot.find(".slideId").text() +"</id>";
            x += "<viewOrder>"+ jQuery(".slideRoot").index(slideRoot) +"</viewOrder>";

            x += "<title>"+ encodeXml(slideRoot.find(".tbSlideTitle").val()) +"</title>";
            x += "<duration>"+ slideRoot.find(".tbDuration").val() +"</duration>";
            x += "<bkGradFrom>"+ encodeXml(slideRoot.find(".tbColortbBkGradFrom").val()) +"</bkGradFrom>";
            x += "<bkGradTo>"+ encodeXml(slideRoot.find(".tbColortbBkGradTo").val()) +"</bkGradTo>";

            x += "<linkUrl>"+ encodeXml(formatUrl(slideRoot.find(".tbLinkUrl"))) +"</linkUrl>";
            x += "<linkCaption>"+ ( slideRoot.find(".cbLinkCaption:checked").size() == 0 ? "" : encodeXml(slideRoot.find(".tbLinkCaption").val()) ) +"</linkCaption>";
            x += "<linkTarget>"+ encodeXml(slideRoot.find(".ddLinkTarget").val() == "other" ? slideRoot.find(".tbLinkTarget").val() : slideRoot.find(".ddLinkTarget").val()) +"</linkTarget>";
            x += "<linkClickAnywhere>"+ (slideRoot.find(".cbLinkAnywhere:checked").size() > 0 ? "true" : "false") +"</linkClickAnywhere>";
            //x += "<useTextsBk>"+ (slideRoot.find(".cbUseTextsBackground").val() ? "true" : "false") +"</useTextsBk>";

            x += "<mp3Url>"+ encodeXml(formatUrl(slideRoot.find(".tbMp3Url"))) +"</mp3Url>";
            x += "<mp3IconColor>"+ encodeXml(slideRoot.find(".tbMp3IconColor").val()) +"</mp3IconColor>";
            x += "<mp3ShowPlayer>"+ (slideRoot.find(".cbMp3ShowPlayer").val() ? "true" : "false") +"</mp3ShowPlayer>";

            // save objects
            x += "<slideObjects>";
            slideRoot.find(".slideObject").each(function() {
                x += "<obj>";
                for (var key in this.objData) {
                    x += "<"+key+">"+ encodeXml(this.objData[key].toString()) +"</"+key+">";
                }
                x += "</obj>";
            });
            x += "</slideObjects>";

            x += "</slide>";
            return x;
        }


        // utility functions

        function encodeXml(sXml) {
            if (!sXml)
                return "";
            sXml = sXml.toString();
            return sXml.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/"/g,"&quot;").replace(/'/g,"&apos;");
        }

        function formatUrl(tb,dd) {
            if (jQuery.trim(tb.val()).length == 0)
                return "";
            if (!dd)
                dd = tb.parents(".fieldRow.:first").find(".ddUrl");
            if (dd.val() == "other") {
                return tb;
            }
            
            return dd.val() + tb.val();
        }

        function fillUrl(tb, dd, url) {
            if (!url) {
                tb.val("");
                return;
            }

            if (url.indexOf("https://") == 0) {
                dd.val("https://");
                tb.val(url.substr("https://".length));
            } else if (url.indexOf("http://") == 0) {
                dd.val("http://");
                tb.val(url.substr("http://".length));
            } else if (url.indexOf("ftp://") == 0) {
                dd.val("ftp://");
                tb.val(url.substr("ftp://".length));
            } else {
                dd.val("other");
                tb.val(url);
            }
        }

        function applyColorpicker(rootElement) {
            applyColorPreviews(rootElement);
            rootElement.find(".tbColor").each(function() {
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
        }

        function applyColorPreviews(_root) {
            _root.find(".tbColor").each(function() {
                jQuery(this).parent().css("background-color", jQuery(this).val());
            });
        }

        function checkLinkCaption(_pnlRoot, useIt) {
            var pnl = _pnlRoot.find('.pnlLinkButtonCaption'); 
            pnl.find('*').toggleClass('ui-state-disabled',!useIt); 
            useIt ? pnl.find(':input').removeAttr('disabled') : pnl.find(':input').attr('disabled','disabled');
            if (useIt)
                pnl.find(':input').focus().select();
        }

    </script>

</body>
</html>
