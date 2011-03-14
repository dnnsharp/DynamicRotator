<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ManageRotatorBase.ascx.cs" Inherits="avt.DynamicFlashRotator.Net.WebManage.ManageRotatorBase"%>

<asp:HiddenField runat="server" ID ="hdnSlideXml" />
<asp:HiddenField runat="server" ID ="hdnLastUpdate" />

<div style= "border-bottom: 2px solid #D2D2FF;">
    <div style="width: 1000px; margin: auto;">
        <div class="btnPane" style="display: none;">
            <a href = "<%= ReturnUrl %>" style="color: #525252; padding: 1px 10px; margin-right: 10px; font-weight: normal;" >Cancel</a>
            <asp:Button runat="server" ID="btnSaveTop" OnClick="SaveSettings" class="ui-state-default" style="padding: 4px 10px;" Text="Save" OnClientClick="if (!save()) return false;" UseSubmitBehavior="false" />
        </div>

        <h1 class="manageTitle" style="font-weight:normal; color: #626262; font-family: georgia; font-size: 18px; font-weight: normal; letter-spacing: 1px;">
            <i>Settings for</i> <asp:Label runat="server" ID = "lblControlName" style="color: #C77405; font-weight:bold;"></asp:Label>
        </h1>
        <div style="clear: both;"></div>
    </div>
</div>

<div style="background-color: #fafafa;">
    <div id = "mainLoading" style="width: 1000px; margin: auto; padding: 10xp; background: none repeat scroll 0 0 white; ">
        <div style = "padding: 20px; font-style: italic;">
            Loading... Please wait... 
        </div>
    </div>
    <div id = "mainTabs" style = "width: 1000px; margin: auto; background: none repeat scroll 0 0 white; display: none;">
    <ul>
        <li><a href="#tabs-main-settings">General Settings</a></li>
        <li><a href="#tabs-main-slides">Slides</a></li>
        <li><a href="#tabs-main-portable">Import/Export</a></li>
        <%--<li><a href="#tabs-main-presets">Presets</a></li>
        <li><a href="#tabs-main-library">Object Library</a></li>--%>
        <li><a href="#tabs-main-customize">Order Customization</a></li>
        <asp:Literal runat="server" ID = "lblTabActivate">
            <li><a href="#tabs-main-activate">Activate</a></li>
        </asp:Literal>
    </ul>

    <div id = "tabs-main-settings">
        <table style="">
            <tr>
                <td class="settingField" style="width: 200px;">
                    <asp:Label runat="server" AssociatedControlID="tbWidth">Size (width x height)</asp:Label>
                </td>
                <td class="settingField">
                        <asp:TextBox runat = "server" ID="tbWidth" class="tbNumber" /> X
                        <asp:TextBox runat = "server" ID="tbHeight" class="tbNumber" />
                        pixels
                </td>
                <td class = "grayed_desc">
                    Determines width and height of the flash rotator in pixels
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
                    If this option is selected, the flash control is transparent so it takes the color of the HTML page
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
                    The Timer Bar appears above the slide butttons; it's a visual indicator of Slide Duration option
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
                <td class="settingField ctlSliderBtnLbl">
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
                <td class="settingField ctlRoundCornderLbl">
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
                <td class="settingField ctlTopTitleLbl">
                    <asp:Label runat="server" AssociatedControlID="tbTopTitleBackground">Top Title Background</asp:Label>
                </td>
                <td class="settingField">
                        <span><asp:TextBox runat = "server" ID="tbTopTitleBackground" class="tbColor ctlTopTitle" />&nbsp;&nbsp;&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td class="settingField ctlTopTitleLbl">
                    <asp:Label runat="server" AssociatedControlID="tbTopTitleBgTransparency">Top Title Bg Transparency</asp:Label>
                </td>
                <td class="settingField ctlTopTitleWidget">
                        <asp:TextBox runat = "server" ID="tbTopTitleBgTransparency" style="width:30px;" class="tbNumber tbRange ctlTopTitle" />
                </td>
            </tr>
            <tr>
                <td class="settingField ctlTopTitleLbl">
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
                    <%--<a href = "#" class="slideOptsGroup ui-state-default" onclick="return false;">Music</a>--%>
                </div>

                <div class="pnlSlideOpts pnlSlideOptsGeneral">
                    <div style = "margin: 8px;">
                        <span class="slideId">-1</span>
                        <div class = "fieldRow ui-widget-content">
                            <b>Slide Title: </b> 
                            <input type="text" class = "tbSlideTitle tooltip_hover" style = "width: 320px;" value="<%= DefaultSlide.Title %>" title="The slide title is displayed when hovering the navigation buttons.<br />This field can contain My Tokens." />
                        </div>
                        <div class = "fieldRow ui-widget-content tooltip_hover" style="clear: left;" title="This option can be used to specify the slide background as a vertical gradient or a solid color (by setting the two fields to same value).">
                            <b>Background Gradient: </b>
                            from <span><input type="text" style = "width: 60px;" class="tbColor tbColortbBkGradFrom" value="<%= ColorToHex(DefaultSlide.BackgroundGradientFrom) %>" />&nbsp;&nbsp;&nbsp;</span>
                            to <span><input type="text" style = "width: 60px;" class="tbColor tbColortbBkGradTo" value="<%= ColorToHex(DefaultSlide.BackgroundGradientTo) %>" />&nbsp;&nbsp;&nbsp;</span>
                        </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                            <b>Duration: </b>
                            <input type="text" style = "width: 24px;" class="tbDuration tooltip_hover" value="<%= DefaultSlide.DurationSeconds %>" title="Duration determines the number of seconds the slide will be displayed." /> seconds
                        </div>
                    </div>
                </div>

                <div class="pnlSlideOpts pnlSlideOptsLink">
                    <div style = "margin: 8px;">
                        <div class = "fieldRow ui-widget-content" style="clear: left;">
                            <b>Open </b> 
                            <select class = "ddUrl ddLinkUrl tooltip_hover" title="Use <i>other</i> when the protocol for your link is not in the dropdown or when you're specifying a relative URL.">
                                <option>http://</option>
                                <option>https://</option>
                                <option>ftp://</option>
                                <option>other</option>
                            </select>
                            <input type="text" class = "tbUrl tbLinkUrl tooltip_hover" style = "width: 200px;" value="<%= DefaultSlide.SlideUrl %>" title="This field determines the page to redirect to when the user clicks the slide link. Use <i>Named Window...</i> option to specify your own window name." />
                            <b>in </b> 
                            <asp:DropDownList runat="server" id="ddLinkTarget" class="ddLinkTarget tooltip_hover" style="width:130px" onchange="jQuery(this).toggle(jQuery(this).val() != 'other'); jQuery(this).parents('.fieldRow:first').find('.pnlLinkTarget').toggle(jQuery(this).val() == 'other').filter(':visible').find(':input').focus().select();" title="Use this option to determine the browser window where the link will be opened."></asp:DropDownList>
                            <span class = "pnlLinkTarget" style="display:none;">
                                <input type="text" class = "tbLinkTarget tooltip_hover" style = "width: 116px; " value="<%= DefaultSlide.Target %>" title="This fields determines the page to redirect to when the user clicks the slide link. Click link next to this field to go back to standard targets drop down." />
                                <a href = "javascript: ;" onclick="jQuery(this).parents('.pnlLinkTarget:first').hide(); jQuery(this).parents('.fieldRow:first').find('.ddLinkTarget').show().val('_self');">^</a>
                            </span>
                        </div>
                        <div class = "fieldRow ui-widget-content" style="clear: left;">
                            <label style="font-weight:bold;" class = "tooltip_hover" title="When this option is checked the entire slide becomes a link. Otherwise, use the Link Button option below.">
                                <input type="checkbox" class="cbLinkAnywhere" style = "" <%= DefaultSlide.ClickAnywhere ? "checked=\"checked\"" : "" %> />
                                <b>Click Anywhere inside the Slide to open link</b> 
                            </label>
                        </div>
                        <div class = "fieldRow ui-widget-content" style="clear: left;">
                            <label style="font-weight:bold;" class = "tooltip_hover" title="When this option is on, a Link Button is added below the slide text that when clicked activates the URL specified above.">
                                <input type="checkbox" class="cbLinkCaption" style = "" onclick="checkLinkCaption(jQuery(this).parents('.pnlSlideOptsLink:first'), this.checked);" />
                                <b>Display link button under slide text</b> 
                            </label>
                        </div>
                            
                        <div class = "fieldRow ui-widget-content pnlLinkButtonCaption" style="clear: left; padding-left: 30px;">
                            <b>Button Caption: </b> 
                            <input type="text" class="tbLinkCaption tooltip_hover" style = "width: 200px;" value="<%= DefaultSlide.ButtonCaption %>" title="This fields determines the label of the Link Button." />
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

                <%--<div class="pnlSlideOpts pnlSlideOptsMp3">
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
                </div>--%>

                <div style = "clear: both;"></div>
            </li>
        </ul>

        <ul id = "slides">
                
        </ul>
            
        <div style="margin: 16px 4px">
            <a href = "#" onclick="addSlide(true); return false;" class="ui-state-default" style="padding: 6px 12px;"><img src="<%= TemplateSourceDirectory %>/res/img/add.gif" border="0" /> Add Slide</a>
        </div>

    </div>

    <%--<div id = "tabs-main-presets">
        test
    </div>

    <div id = "tabs-main-library">
        test
    </div>--%>
    <div id = "tabs-main-portable" style="margin: 15px 25px;">
        <h2 style="color: #C77405;">Import Data</h2>
        <div style="color: #525252; font-style: italic;">Copy/paste the XML data you exported from another Dynamic Rotator control.</div>

        <asp:TextBox runat = "server" ID = "tbImportData" TextMode="MultiLine" style="width: 600px; height: 200px;" ValidationGroup="vldImport"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbImportData" Text="&laquo;required" ValidationGroup="vldImport"></asp:RequiredFieldValidator>
        <asp:CustomValidator runat="server" ID = "validImportXml" ControlToValidate="tbImportData" Text="&laquo;required" ValidationGroup="vldImport"></asp:CustomValidator>
        <br />
        <asp:Button runat="server" OnClick="ImportData" class="ui-state-default" style="padding: 4px 10px;" Text="Import" UseSubmitBehavior="false" CausesValidation="true" ValidationGroup="vldImport" />
        <br /><br />

        <h2 style="color: #C77405;">Export Data</h2>
        <div style="color: #525252; font-style: italic;">Use this function to export configuration in an XML structure that can be imported in other Dynamic Rotator controls.</div>
        <asp:TextBox runat = "server" ID = "tbExportData" TextMode="MultiLine" style="width: 600px; height: 200px;" ReadOnly="true" Visible="false"></asp:TextBox>
        <br />
        <asp:Button runat="server" OnClick="ExportData" class="ui-state-default" style="padding: 4px 10px;" Text="Export" UseSubmitBehavior="false" CausesValidation="false" />
    </div>

    <div id = "tabs-main-customize" style="margin: 15px 25px;">
        <h2 style="color: #C77405;">We can design your banner/rotator!</h2>
        <div>
            Please choose the option that best suits you requirement.
            <br /><br />

            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td class="cstmOptName" colspan="3">3 Slides + editing for up to 6 images</td>
                    <td class="cstmOptName" colspan="3">5 Slides + editing for up to 10 images</td>
                    <td class="cstmOptName">More...</td>
                </tr>
                <tr>
                    <td class="cstmOptNameSub">Your Graphics*</td>
                    <td class="cstmOptNameSub">Research **</td>
                    <td class="cstmOptNameSub">Full Dev***</td>
                    <td class="cstmOptNameSub">Your Graphics*</td>
                    <td class="cstmOptNameSub">Research **</td>
                    <td class="cstmOptNameSub">Full Dev***</td>
                    <td class="cstmOptNameSub" rowspan="2" style="font-weight:normal;">
                        If your requirement is not covered by these options, please <a href ="http://www.avatar-soft.ro/Contact.aspx">Contact Us</a> for a quote.
                    </td>
                </tr>
                <tr>
                    <td class="cstmOptPrice">$39.95</td>
                    <td class="cstmOptPrice">$64.95</td>
                    <td class="cstmOptPrice">$99.95</td>
                    <td class="cstmOptPrice">$59.95</td>
                    <td class="cstmOptPrice">$99.95</td>
                    <td class="cstmOptPrice">$149.95</td>
                </tr>
                <tr>
                    <td class="cstOptBtn"><a href = "https://www.plimus.com/jsp/buynow.jsp?contractId=2953628" class="ui-state-default" style="padding: 4px 10px;">Checkout</a></td>
                    <td class="cstOptBtn"><a href = "https://www.plimus.com/jsp/buynow.jsp?contractId=2953668" class="ui-state-default" style="padding: 4px 10px;">Checkout</a></td>
                    <td class="cstOptBtn"><a href = "https://www.plimus.com/jsp/buynow.jsp?contractId=2953670" class="ui-state-default" style="padding: 4px 10px;">Checkout</a></td>
                    <td class="cstOptBtn"><a href = "https://www.plimus.com/jsp/buynow.jsp?contractId=2953672" class="ui-state-default" style="padding: 4px 10px;">Checkout</a></td>
                    <td class="cstOptBtn"><a href = "https://www.plimus.com/jsp/buynow.jsp?contractId=2953674" class="ui-state-default" style="padding: 4px 10px;">Checkout</a></td>
                    <td class="cstOptBtn"><a href = "https://www.plimus.com/jsp/buynow.jsp?contractId=2953676" class="ui-state-default" style="padding: 4px 10px;">Checkout</a></td>
                    <td class="cstOptBtn"><a href="http://www.avatar-soft.ro/Contact.aspx" class="ui-state-default" style="padding: 4px 10px;">Contact Us</a></td>
                </tr>
            </table>

            <br />
                
            <div style = "margin: 6px;">
                <span style="color: #C77405;font-weight:bold;">*</span>
                Select this option if already have the graphics, you just need them edited and everything put together into the banner/rotator.
            </div>

            <div style = "margin: 6px;">
                <span style="color: #C77405;font-weight:bold;">**</span>
                We'll do the research for you to find the graphics that match your ideas. We will send links so you can license them.
            </div>

            <div style = "margin: 6px;">
                <span style="color: #C77405;font-weight:bold;">***</span>
                We will take care of everything, including licensing the graphics and create additional vectorial graphics such as icons or small flash animations.
            </div>

            <br /><br />
            <b>
                Once you made your purchase, please send us the invoice number and your requirement at support@avatar-soft.ro and we'll reply back you within 24 hours.
                Make ure your requirement is structured (Slide 1: This text, this link, this pictures... Slide 2: ....). We will deliver the work as a zip package that can be imported into your copy. 
            </b>
            <br /><br />
        </div>

    </div>

    <asp:Label runat="server" ID = "lblTabActivateContents">
        <div id = "tabs-main-activate" style="margin: 15px 25px;">
            <h2 style="color: #C77405;">This copy of Dynamic Rotator .NET is Not Activated!</h2>
            <div>
                This means that when you're not accessing this website on localhost you will get random trial notifications.
            </div>

            <br /><br />
            <div>
                To activate this package you need a License Key. If you don't have one yet you can <a style="color: #1C94C4;" href = "<%= avt.DynamicFlashRotator.Net.Settings.RotatorSettings.BuyLink %>">Purchase a License from Snowcovered</a>.

                <br /><br />
                If you already have a license, proceed to <a href = "<%= TemplateSourceDirectory %>/Activation.aspx?rurl=<%= Server.UrlEncode(Request.RawUrl) %>" style="color: #1C94C4; font-weight: bold;">Activation Wizard</a>.
            </div>
        </div>
    </asp:Label>
</div>
</div>

<div class="footer">
    <div style="width: 1000px; margin: auto;">
        <div style="float: left;">
            Version <%= avt.DynamicFlashRotator.Net.Settings.RotatorSettings.VersionAll %> by <a href = "http://www.avatar-soft.ro" style="color: #C77405;">Avatar Software</a>
            <br />
            <a href = "http://www.avatar-soft.ro/dotnetnuke-modules/dnn-banner/flash/dynamic-rotator.aspx">Read more about Dynamic Redirect .NET...</a>
        </div>
        <div class="btnPane" style="display: none;">
            <a href = "<%= ReturnUrl %>" style="color: #525252; padding: 1px 10px; margin-right: 10px; font-weight: normal;" >Cancel</a>
            <asp:Button runat="server" ID="btnSaveBottom" OnClick="SaveSettings" class="ui-state-default" style="padding: 4px 10px;" Text="Save" OnClientClick="if (!save()) return false;" UseSubmitBehavior="false" />
        </div>
        <div style="clear:both;"></div>
    </div>
</div>

<div id = "dlgObjectSettings" style="overflow:visible; display: none;">

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
                        <td class = "ui-widget-content tooltip_hover" title="The name is used only in administration screen to quickly identify objects.">
                            <input type="text" class = "tbObjName tbRequired" style = "width: 200px;" />
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content">
                        <td class = "ui-widget-content hdr">Position:</td>
                        <td class = "ui-widget-content tooltip_hover" title="These coordinates determine the final position of the object, after the effects and transitions have completed.">
                            <b>X</b> <input type="text" style = "width: 40px;" class="tbObjPosX tbNumber" />
                            <b>Y</b> <input type="text" style = "width: 40px;" class="tbObjPosY tbNumber" />
                            <asp:DropDownList runat="server" ID = "ddVerticalAlgin" class = "ddVerticalAlgin" style="display:none;">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content objFieldTextOnly">
                        <td class = "ui-widget-content hdr">Width:</td>
                        <td class = "ui-widget-content tooltip_hover" title="Use this option to specify the text width which determines where the text goes on next line.<br/> If not specified, the text takes full available width.">
                            <input type="text" style = "width: 60px;" class="tbObjWidth tbNumber" /> pixels
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content objFieldImgOnly">
                        <td class = "ui-widget-content hdr">Link URL:</td>
                        <td class = "ui-widget-content">
                            <select class = "ddUrl ddLinkUrl tooltip_hover" title="Use <i>other</i> when the protocol for your link is not in the dropdown or when you're specifying a relative URL.">
                                <option>http://</option>
                                <option>https://</option>
                                <option>ftp://</option>
                                <option>other</option>
                            </select>
                            <input type="text" class = "tbUrl tbLinkUrl tooltip_hover" style = "width: 270px;" title="When specified, Dynamic Rotator will open this link when the user clicks inside this object's area." />
                        </td>
                    </tr>
                        
                    <tr class=""><td colspan="3" style="font-size:5px;">&nbsp;</td></tr>
                    <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>
                        
                    <tr class = "objFieldRow ui-widget-content objFieldImgOnly">
                        <td class = "ui-widget-content hdr">Resource URL:</td>
                        <td class = "ui-widget-content">
                            <select class = "ddUrl ddObjectUrl tooltip_hover" title="Use <i>other</i> when the protocol for your link is not in the dropdown or when you're specifying a relative URL.">
                                <option>http://</option>
                                <option>https://</option>
                                <option>ftp://</option>
                                <option>other</option>
                            </select>
                            <input type="text" class = "tbUrl tbObjectUrl tbRequired tooltip_hover" style = "width: 270px;" title="Instruct Dynamic Rotator where to find this resource." />
                            <div style="text-align: right; margin-right:6px;">
                                <a href = "#" onclick="browseServerForResources(jQuery(this).parents('.objFieldRow')); return false;" style="color: #EB8F00;">Browse Server &raquo;</a>
                            </div>
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content objFieldTextOnly">
                        <td class = "ui-widget-content hdr">Text:</td>
                        <td class = "ui-widget-content"><textarea class = "tbObjText tooltip_hover" style = "width: 380px; height: 60px;" title="Note that you can use html tags (such as <i>i,b,u,a,font,etc</i>) to format the text.<br />This field can contain My Tokens."></textarea></td>
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
                        <td class = "ui-widget-content tooltip_hover" title="Use this option to have the object fade into the background.<br/>You can also use this option to hide objects you're not ready to delete by setting it to 0.">
                            <input type="text" class = "tbObjOpacity tbNumber tbRange" style = "width: 30px;" />
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content">
                        <td class = "ui-widget-content hdr">Glow:</td>
                        <td class = "ui-widget-content tooltip_hover" style="text-align: center;" title="The glow is a border displayed around the object. If you put a dark color you'll obtain a shadow effect. <br/> To disable this option, set the size and strength to 0.">
                            <b>Size:</b> <input type="text" class = "tbNumber tbObjGlowSize" style = "width: 30px;"  />
                            <b>Color:</b> <span><input type="text" style = "width: 60px;" class="tbColor tbObjGlowColor" />&nbsp;&nbsp;&nbsp;</span>
                            <b>Strength:</b> <input type="text" class = "tbNumber tbObjGlowStrength" style = "width: 30px;" />
                        </td>
                    </tr>
                </table>

                <table width="480px" cellpadding="0" cellspacing="2" border="0" style = "margin-top:8px;" class = "objFieldTextOnly">
                    <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>

                    <tr class = "objFieldRow ui-widget-content objFieldTextOnly">
                        <td class = "ui-widget-content hdr">Text Color:</td>
                        <td class = "ui-widget-content tooltip_hover" title="Remember that you can override this color for parts of the content by using html tags in the content (for example put <i>This is &amp;lt;font color=&quot;#ff0000;&quot;&amp;gt;red&amp;lt;font&amp;gt;</i>).">
                            <span><input type="text" style = "width: 60px;" class="tbColor tbTextColor" />&nbsp;&nbsp;&nbsp;</span>
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content objFieldTextOnly">
                        <td class = "ui-widget-content hdr">Text Background:</td>
                        <td class = "ui-widget-content tooltip_hover" style="" title="Optionally, you can display the text inside a colored rectangle. Use these field to determine the background color and opacity.<br/>To disable this option, set opacity to 0.">
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
                        <td class = "ui-widget-content tooltip_hover" title="When the text background is specified, use the option to determine how much space exists between the background border and the text inside.">
                            <input type="text" class = "tbTextPadding tbNumber" style = "width: 60px;" /></td>
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
                                            <input type="text" class = "tbObjDelay tbNumber tooltip_hover" style = "width: 60px;" title="This option determines how many seconds to wait before the object enters the scene." /> seconds
                                        </div>
                                    </td>
                                    <td style="padding-left: 10px;">
                                        <b>Duration: </b> 
                                        <input type="text" class = "tbObjDuration tbNumber tooltip_hover" style = "width: 60px;" title="Use this option to specify the duration of transition effects." /> seconds
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
                        <td class = "ui-widget-content tooltip_hover" style="width: 320px;" title="Use this option to determined how the object enters the scene.">
                            <%--<asp:DropDownList runat="server" ID = "ddAppearMode" class = "ddAppearMode" " style="width:160px" ></asp:DropDownList>--%>
                            <asp:RadioButtonList runat="server" ID = "ddAppearMode" class = "ddAppearMode" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table width="480px" cellpadding="0" cellspacing="2" border="0" id = "pnlObjSlideParams" style="display:none; margin-top: 8px;">
                    <tr class="rowSepGray"><td colspan="2">&nbsp;</td></tr>

                    <tr class = "objFieldRow ui-widget-content">
                        <td class = "ui-widget-content hdr">Appear from:</td>
                        <td class = "ui-widget-content tooltip_hover" style="width: 320px;" title="When object enter the scene with the slide effect, use this option to determine the direction it slides from.">
                            <asp:RadioButtonList runat="server" ID = "ddObjAppearFromText" class = "ddObjAppearFrom ddObjAppearFromText" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                            <asp:RadioButtonList runat="server" ID = "ddObjAppearFromImage" class = "ddObjAppearFrom ddObjAppearFromImage" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content">
                        <td class = "ui-widget-content hdr">Move Type:</td>
                        <td class = "ui-widget-content tooltip_hover" style="width: 320px;" title="Specify initial movement type when object enters the scene with slide effect.">
                            <asp:RadioButtonList runat="server" ID = "ddObjMoveType" class = "ddObjMoveType" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content">
                        <td class = "ui-widget-content hdr">Easing Type:</td>
                        <td class = "ui-widget-content tooltip_hover" style="width: 320px;" title="Easing determines how the movement of the object varies after it enters the scene.">
                            <asp:RadioButtonList runat="server" ID = "ddObjEasingType" class = "ddObjEasingType" RepeatDirection="Horizontal" CellSpacing="4"></asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class = "objFieldRow ui-widget-content">
                        <td class = "ui-widget-content hdr">Effect:</td>
                        <td class = "ui-widget-content tooltip_hover" style="width: 320px;" title="Optionally, specify an effect to apply to the object.">
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


<div id = "dlgFileBrowserResource" style="overflow:visible; display: none;">
    <div class="fileLoader"></div>
    <div style="clear:both;"></div>
    <div class = "folderPane" style ="float: left; padding: 6px; width: 220px; height: 300px; overflow: auto; border: 1px solid #e2e2e2;">
        <a href = "#" onclick="getFiles(jQuery(this).attr('')); generateBreads(jQuery(this)); return false;" class="folderRoot"><%= avt.DynamicFlashRotator.Net.Settings.RotatorSettings.Configuration.BrowseServerForResources.RootName %></a>
    </div>
    <div style ="height: 300px; padding: 6px; overflow: auto; border: 1px solid #e2e2e2;">
        <div style="padding: 4px; background-color: #f2f2f2; border: 1px solid #c2c2c2;" class="breadPane">
                
        </div>
        <div style="margin: 10px 16px;" class="filePane">
                
        </div>
    </div>
    <div style="clear:both;"></div>
    <div style="font-style: italic; color: #626262;">
        Hover file names for preview, click to select them...
    </div>
</div>


<script type="text/javascript">
<%-- Initialization -------------------------------------------------------------------------- --%>

    var g_isDragging = false;

    jQuery(document).ready(function() {
        jQuery("#mainLoading").hide();
        jQuery("#mainTabs").tabs({ }).show();
        <%= _ActiveTab != -1 ? "jQuery('#mainTabs').tabs('select', " + _ActiveTab + ");" : "" %>
            
        jQuery(".btnPane").show();

        applyColorpicker(jQuery("#tabs-main-settings"));
        applyUrlFormatters(jQuery("#objSettingsTabs"));

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
                    
                applyColorpicker(jQuery(this));
                applyUrlFormatters(jQuery(this));
            });

        var slides = <%= hdnSlideXml.Value %>;
        for (var i  = 0; i < slides.length; i++) {
            loadSlide(slides[i]);
        }

        jQuery("#dlgFileBrowserResource").dialog({
            bgiframe: false,
            autoOpen: false,
            title: "Browse Server",
            width: 800,
            modal: true,
            resizable: false,
            closeOnEscape: true,

            buttons: {
                'Close': function() {
                    jQuery("#dlgFileBrowserResource").dialog('close');
                }
            }
        });


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

        jQuery("#dlgObjectSettings").find("input").keyup(function(event) {
            if (event.keyCode==13) { 
                jQuery("#dlgObjectSettings").parents(".ui-dialog:first").find(".ui-dialog-buttonpane").find("button:first").click();
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
            
        initAllTooltips();

        jQuery("#<%= cbShowBottomButtons.ClientID %>").change(function() {
            if (this.checked) {
                jQuery('.ctlSliderBtn').removeAttr('disabled');
                jQuery(".ctlSliderBtnLbl").removeClass("ui-state-disabled");
            } else {
                jQuery('.ctlSliderBtn').attr('disabled','disabled');
                jQuery(".ctlSliderBtnLbl").addClass("ui-state-disabled");
            }
        }).change();

        jQuery("#<%= cbUseRoundCornersMask.ClientID %>").change(function() {
            if (this.checked) { 
                jQuery('.ctlRoundCornder').removeAttr('disabled'); 
                jQuery(".ctlRoundCornderLbl").removeClass("ui-state-disabled");
            } else {
                jQuery('.ctlRoundCornder').attr('disabled','disabled');
                jQuery(".ctlRoundCornderLbl").addClass("ui-state-disabled");
            }
        }).change();

        jQuery("#<%= cbShowTopTitle.ClientID %>").change(function() {
            if (this.checked) {
                jQuery('.ctlTopTitle').removeAttr('disabled');
                jQuery(".ctlTopTitleWidget .ui-state-disabled").removeClass("ui-state-disabled");
                jQuery(".ctlTopTitleLbl").removeClass("ui-state-disabled");
            } else {
                jQuery('.ctlTopTitle').attr('disabled','disabled');
                jQuery(".ctlTopTitleWidget .ui-widget").addClass("ui-state-disabled");
                jQuery(".ctlTopTitleLbl").addClass("ui-state-disabled");
            }
        }).change();

    });

</script>

<script type="text/javascript">
<%-- Keep alive and check updates -------------------------------------------------------------------------- --%>

var g_lastUpdate;
var g_checkUpdateTimer;

jQuery(document).ready(function() {
    
    g_lastUpdate = parseInt(jQuery("#<%= hdnLastUpdate.ClientID %>").val());

    g_checkUpdateTimer = setInterval(function() {
        jQuery.post("<%= TemplateSourceDirectory %>/AdminApi.aspx?controlId=<%= Request.QueryString["controlId"]%>&cmd=checkupdate", { 
                
            }, function(data) {
                if (data.error) {
                    alert("An error has occured: " + data.error);
                } else {
                    var newTime = parseInt(data.lastUpdate);
                    if (newTime > g_lastUpdate) {
                        g_lastUpdate = newTime;
                        // clearInterval(g_checkUpdateTimer);
                        alert("Warning: The server holds a more recent version of the rotator. Either discard your changes by refreshing this page or you can overwrite server changes by saving your current settings.");
                    }
                }
            }, "json");
    }, 5000);
});

</script>

<script type="text/javascript">
<%-- Slide Functions -------------------------------------------------------------------------- --%>

    function updateSlideIndexes() {
        var index = 1;
        jQuery("#slides").find(".slideIndex").each(function() {
            jQuery(this).text(index++);
        });
    }

    function addSlide(bNew) {
        var _item = jQuery("#slideTpl").clone().removeAttr("id");
        _item.appendTo(jQuery("#slides"));

        initAllTooltips(_item);

        _item.find(".slideExtraBtn,.btnAddObject").button();
        //_item.find(".pnlSlideOptsGeneral").show();
            
        applyColorpicker(_item);
        applyUrlFormatters(_item);
        updateSlideIndexes();
            
        if (bNew) {
            _item.find(".slideOptsGroup").eq(0).click();
            _item.find(".tbSlideTitle").focus().select().btOff().btOn();
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
                jQuery(ui.item).addClass("slideObjectHover");
            },

            stop: function(event, ui) {
                jQuery(".slideObjectHover").removeClass("slideObjectHover");
                setTimeout(function() { g_isDragging = false; }, 100);
            }
                 
        }).disableSelection();

        // update color previews
        applyColorPreviews(slideRoot);
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
            _new.find(".slideObject").eq(iobj).find(".objTitle").text(_new.find(".slideObject").eq(iobj)[0].objData.name);
            iobj++;
        });

        initAllTooltips(_new);
    }

    function deleteSlide(slideRoot) {
        if (!confirm("Are you sure you want to delete this slide?\nData will be permanently lost after you hit Save...")) {
            return;
        }

        slideRoot.remove();
        updateSlideIndexes();
    }

</script>

<script type="text/javascript">
<%-- Slide Object Functions -------------------------------------------------------------------------- --%>
        
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
                if (g_isDragging)
                    return;
                jQuery(this).addClass("slideObjectHover");
            })
            .mouseout(function() {
                if (g_isDragging)
                    return;
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
            slideRoot.find(".btnAddObjectText").attr("title", "Temporary Limitation: slides can only contain one text object.").bt(btOpts);
        }
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
            _dlg.find(".tbObjWidth").val(slideObjItem[0].objData.width > 0 ? slideObjItem[0].objData.width : "");
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
            _dlg.find(".tbObjWidth").val("");
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

</script>

<script type="text/javascript">
<%-- Save Routines -------------------------------------------------------------------------- --%>

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
            width: dlg.find(".tbObjWidth").val(),
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
        jQuery("#<%=hdnSlideXml.ClientID %>").val(encodeXml(x));

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

</script>

<script type="text/javascript">
<%-- Hide/Disabled Controls scripts -------------------------------------------------------------------------- --%>

    function checkLinkCaption(_pnlRoot, useIt) {
        var pnl = _pnlRoot.find('.pnlLinkButtonCaption'); 
        pnl.find('*').toggleClass('ui-state-disabled',!useIt); 
        useIt ? pnl.find(':input').removeAttr('disabled') : pnl.find(':input').attr('disabled','disabled');
        if (useIt)
            pnl.find(':input').focus().select();
    }

</script>

<script type="text/javascript">
    <%-- File Helpers -------------------------------------------------------------------------- --%>

    function browseServerForResources(propRoot) {
        var _dlg = jQuery("#dlgFileBrowserResource");
        _dlg.dialog("open");

        jQuery(".selFile").removeClass("selFile");
        propRoot.addClass("selFile");

        _dlg[0].select = function(url) {
            var propRoot = jQuery(".selFile");
            propRoot.find(".ddUrl").val("other");
            propRoot.find(".tbUrl").val(url);
            propRoot.removeClass("selFile");
        };

        if (_dlg.find(".folderPane ul").size() == 0) {
            loadFolders("", jQuery("#dlgFileBrowserResource").find(".folderPane"));
            getFiles("");
            generateBreads(jQuery("#dlgFileBrowserResource").find(".folderRoot"));
        }
    }

    function loadFolders(parentFolder, appendTo) {
            
        var _dlg = jQuery("#dlgFileBrowserResource");
        _dlg.find(".fileLoader").show().css("opacity", 0.8);

        jQuery.post("<%= TemplateSourceDirectory %>/AdminApi.aspx?controlId=<%= Request.QueryString["controlId"]%>&cmd=listfolders&resPath=<%= Server.UrlEncode(Request.QueryString["resPath"]) %>", { 
            relPath: parentFolder
        }, function(data) {
            if (data.error) {
                alert("An error has occured: " + data.error);
            } else {
                appendTo.append("<ul></ul>");
                var _lst = appendTo.children("ul");
                
                for (var i = 0; i<data.length; i++) {
                    var _f = jQuery("<li><a href='#' onclick='expandFolder(jQuery(this).parent()); return false;' class='expand'>&nbsp;+&nbsp;</a><a href='#' onclick='getFiles(jQuery(this).attr(\"relPath\")); generateBreads(jQuery(this)); return false;' class='folder' relPath='"+ data[i].relPath +"'>"+ data[i].name +"</a></li>");
                    if (!data[i].hasChildren) {
                        _f.find(".expand").css("visibility", "hidden");
                    }
                    _lst.append(_f);
                }
            }
            jQuery("#dlgFileBrowserResource").find(".fileLoader").fadeOut();
        }, "json");
    }

    function expandFolder(_folder) {
        if (_folder.children(".expand").hasClass("opened")) {
            _folder.children("ul").slideUp("fast");
            _folder.children(".expand").removeClass("opened").html("&nbsp;+&nbsp;");
        } else {
            _folder.children(".expand").addClass("opened").html("&nbsp;-&nbsp;");
            if (_folder.children("ul").size() == 0) {
                loadFolders(_folder.children(".folder").attr("relPath"), _folder);
            } else {
                _folder.children("ul").slideDown("fast");
            }
        }
    }

    function getFiles(_folder) {
            
        var _dlg = jQuery("#dlgFileBrowserResource");
        _dlg.find(".fileLoader").show().css("opacity", 0.8);
        jQuery("#dlgFileBrowserResource").find(".filePane").empty();

        jQuery.post("<%= TemplateSourceDirectory %>/AdminApi.aspx?controlId=<%= Request.QueryString["controlId"]%>&cmd=listfiles&resPath=<%= Server.UrlEncode(Request.QueryString["resPath"]) %>", { 
            //relPath: _folder.children(".folder").attr("relPath")
            relPath: _folder
        }, function(data) {
            if (data.error) {
                alert("An error has occured: " + data.error);
            } else {
                var _filePane = jQuery("#dlgFileBrowserResource").find(".filePane").empty();
                if (data.length == 0) {
                    _filePane.append("<div style = 'margin: 40px; font-style: italic; color: #626262;'>No image or swf files in this folder...</div>");
                } else {
                    _filePane.append("<ul></ul>");
                    var _lst = _filePane.children("ul");
                
                    for (var i = 0; i<data.length; i++) {
                        var _f = jQuery("<li><a href='<%= avt.DynamicFlashRotator.Net.Settings.RotatorSettings.Configuration.BrowseServerForResources.RootFolder.Url %>"+ data[i].fullUrl +"' onclick='selectImage(jQuery(this)); return false;' onmouseover='previewImage(jQuery(this));' onmouseout='hidePreviewImage(jQuery(this));' class='file' relPath='"+ data[i].relPath +"'>"+ data[i].name +"</a></li>");
                        _lst.append(_f);
                    }
                }
            }
            jQuery("#dlgFileBrowserResource").find(".fileLoader").fadeOut();
        }, "json");
    }

    function generateBreads(_folder) {
        if (_folder.hasClass("bread")) { // click came from a bread
            _folder.nextAll().remove();
            return;
        }

        var _breadPane = jQuery("#dlgFileBrowserResource").find(".breadPane").empty();
        _breadPane.append(_folder.clone().addClass("bread"));
        _folder.parents("ul").prev("a").each(function() {
            _breadPane.prepend("<span>&nbsp;/&nbsp;</span>");
            _breadPane.prepend(jQuery(this).clone().addClass("bread"));
        });
    }

    function previewImage(_img) {
        if (_img.next(".imgPreview").size() > 0) {
            _img.next(".imgPreview").show();
        } else {
            if (_img.attr("href").indexOf(".swf") == _img.attr("href").length-4) {
                // _img.after("<div class='imgPreview' style='margin-left: "+ (_img.outerWidth() + 60) +"px; margin-top: -40px;'><img src='"+ _img.attr("href") +"' /></div>");
                return;
            } else {
                _img.after("<div class='imgPreview' style='margin-left: "+ (_img.outerWidth() + 60) +"px; margin-top: -40px;'><img src='"+ _img.attr("href") +"' /></div>");
            }
        }
    }

    function hidePreviewImage(_img) {
        _img.next(".imgPreview").remove();
    }

    function selectImage(_img) {
        jQuery("#dlgFileBrowserResource").dialog('close');
        jQuery("#dlgFileBrowserResource")[0].select(_img.attr("href"));
    }

</script>

<script type="text/javascript"> 
    <%-- Tooltip Helpers -------------------------------------------------------------------------- --%>

    var btOpts = {
        fill: 'rgba(0, 0, 0, .8)',
        cssStyles: {"color":"#ffb465","font-weight":"normal", "font-size":"11px"},
        strokeStyle: "#FBCB09",
        strokeWidth: 1,
        spikeLength: 10,
        spikeGirth: 20,
        padding: 16,
        cornerRadius: 10,
        width: 300
    }

    function initAllTooltips(parent) {
        initTooltips(parent, "hover", "hover", "right");
        initTooltips(parent, "click","click","right");
        initTooltips(parent, ['focus', 'blur']);
        initTooltips(parent, "hover", "hover_v", ["bottom", "top"]);
        initTooltips(parent, ['focus', 'blur'], "focus_v", ["bottom", "top"]);
    }

    function initTooltips(parent, action, cssClass, pos) {
    
        if (!cssClass)
            cssClass = action;
        
        if (!pos)
            pos = ["most"];
        
        if (!parent)
            parent = jQuery("body");
            
        parent.find('.tooltip_' + cssClass).bt(jQuery.extend({}, btOpts, {
                offsetParent: jQuery("body"),
                trigger: action,
                positions: pos
            })
        );
    }

</script>

<script type="text/javascript"> 
    <%-- Color Pickers and Previews Helpers -------------------------------------------------------------------------- --%>

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

</script>

<script type="text/javascript"> 
    <%-- URL Helpers -------------------------------------------------------------------------- --%>

    function formatUrl(tb,dd) {
        if (jQuery.trim(tb.val()).length == 0)
            return "";
        if (!dd)
            dd = tb.parents(".fieldRow.:first").find(".ddUrl");
        if (dd.val() == "other") {
            return tb.val();
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

    function applyUrlFormatters(rootElement) {
            
        rootElement.find(".tbUrl").each(function() {
            jQuery(this).bind('keyup', function(e) {
                var tb = jQuery(this);
                var dd = tb.prev();
                var url = tb.val();

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
                    if (url.indexOf("://") > 0) {
                        dd.val("other");
                        return;
                    }

                    // leave unchanged
                    return;
                }

                fillUrl(tb, dd, url);
            });
        });
    }

</script>
