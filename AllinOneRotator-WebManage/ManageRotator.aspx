<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ManageRotator.aspx.cs" Inherits="avt.AllinOneRotator.Net.WebManage.ManageRotator" ValidateRequest="false" %>

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
    
    <asp:HiddenField runat="server" ID ="hdnSlideXml" />

    <div>
        <div class="btnPane">
            <a href = "<%= System.Web.HttpUtility.UrlDecode(Request.QueryString["rurl"]) %>" style="color: #525252; padding: 1px 10px; margin-right: 10px; font-weight: normal;" >Cancel</a>
            <asp:Button runat="server" OnClick="SaveSettings" class="ui-state-default" style="padding: 4px 10px;" Text="Save" OnClientClick="return save();" />
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
            
            <ul style="display:none;">
                <li id = "slideTpl" class="slideRoot">
                    <div class="dragHandle">
                        <div style="margin: 6px;">
                            <div class="slideIndex">1</div>
                            <br />
                            drag here
                        </div>
                    </div>

                    <div class = "pnlSlideOptGroups">
                        <a href = "#" class="slideOptsGroup ui-state-hover" style="margin-top: 10px" onclick="return false;">General</a>
                        <a href = "#" class="slideOptsGroup ui-state-default" onclick="return false;">Slide Link</a>
                        <a href = "#" class="slideOptsGroup ui-state-default" onclick="return false;">Graphics / Content</a>
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
                                from <input type="text" style = "width: 60px;" class="tbColor tbColortbBkGradFrom" value="<%= ColorToHex(DefaultSlide.BackgroundGradientFrom) %>" />
                                to <input type="text" style = "width: 60px;" class="tbColor tbColortbBkGradTo" value="<%= ColorToHex(DefaultSlide.BackgroundGradientTo) %>" />
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
                                <b>Slide URL: </b> 
                                <select class = "ddUrl ddLinkUrl">
                                    <option>http://</option>
                                    <option>https://</option>
                                    <option>ftp://</option>
                                    <option>other</option>
                                </select>
                                <input type="text" class = "tbUrl tbLinkUrl" style = "width: 260px;" value="<%= DefaultSlide.SlideUrl %>" />
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Link Caption: </b> 
                                <input type="text" class="tbLinkCaption" style = "width: 200px;" value="<%= DefaultSlide.ButtonCaption %>" />
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Link Target: </b> 
                                <asp:DropDownList runat="server" id="ddLinkTarget" class="ddLinkTarget"></asp:DropDownList>
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Use Texts Background: </b> 
                                <input type="checkbox" class = "cbUseTextsBackground" style = "" <%= DefaultSlide.UseTextsBackground ? "checked='checked'" :"" %> />
                            </div>
                        </div>
                    </div>

                    <div class="pnlSlideOpts pnlSlideOptsContent">
                        <div style = "margin: 8px;">
                            Content
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
                                <input type="text" style = "width: 60px;" class="tbColor tbMp3IconColor" value="<%= ColorToHex(DefaultSlide.IconColor) %>" />
                            </div>
                            <div class = "fieldRow ui-widget-content" style="clear: left;">
                                <b>Show Player: </b> 
                                <input type="checkbox" class="cbMp3ShowPlayer" <%= DefaultSlide.ShowPlayer ? "checked='checked'" :"" %> />
                            </div>
                        </div>
                    </div>

                    <div class="pnlSlideExtra">
                        <a href = "#" class="slideExtraBtn" onclick="cloneSlide(jQuery(this).parents('.slideRoot:first')); return false;" style="margin-top: 70px">Clone</a>
                        <br />
                        <a href = "#" class="slideExtraBtn" onclick="deleteSlide(jQuery(this).parents('.slideRoot:first')); return false;">Delete</a>
                    </div>

                    <div style = "clear: both;"></div>
                </li>
            </ul>

            <ul id = "slides">
                
            </ul>
            <a href = "#" onclick="addSlide(); return false;"><img src="<%= TemplateSourceDirectory %>/res/img/add.gif" border="0" /> Add Slide</a>
        </div>

        <div id = "tabs-main-activate">
            test
        </div>

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
    
        jQuery(document).ready(function() {
            jQuery("#mainTabs").tabs();

            applyColorpicker(jQuery("#tabs-main-settings"));

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

            var slides = eval('<%= hdnSlideXml.Value %>');
            for (var i  = 0; i < slides.length; i++) {
                loadSlide(slides[i]);
            }
            
            //addSlide();
            //addSlide();
        });

        function applyColorpicker(rootElement) {
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

        function updateSlideIndexes() {
            var index = 1;
            jQuery("#slides").find(".slideIndex").each(function() {
                jQuery(this).text(index++);
            });
        }

        function addSlide() {
            var _item = jQuery("#slideTpl").clone().removeAttr("id");
            _item.appendTo(jQuery("#slides"));

            _item.find(".slideExtraBtn").button();
            _item.find(".pnlSlideOptsGeneral").show();
            applyColorpicker(_item);

            updateSlideIndexes();
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
            slideRoot.find(".tbLinkCaption").val(slide.linkCaption);
            slideRoot.find(".ddLinkTarget").val(slide.linkTarget);
            slide.useTextsBk == "true" ? slideRoot.find(".cbUseTextsBackground").attr("checked","checked") : slideRoot.find(".cbUseTextsBackground").removeAttr("checked");

            fillUrl(slideRoot.find(".tbMp3Url"), slideRoot.find(".ddMp3Url"), slide.mp3Url);
            slideRoot.find(".tbMp3IconColor").val(slide.mp3IconColor);
            slide.mp3ShowPlayer == "true" ? slideRoot.find(".cbMp3ShowPlayer").attr("checked","checked") : slideRoot.find(".cbMp3ShowPlayer").removeAttr("checked");
        }

        function cloneSlide(slideRoot) {
            var _new = slideRoot.clone();
            _new.find(".tbSlideTitle").val(_new.find(".tbSlideTitle").val() + " - Copy");
            slideRoot.after(_new);
            _new.find(".slideOptsGroup:first").click();
            updateSlideIndexes();
        }

        function deleteSlide(slideRoot) {
            if (!confirm("Are you sure you want to delete this slide?\nData will be permanently lost after you hit Save...")) {
                return;
            }

            slideRoot.remove();
            updateSlideIndexes();
        }

        function save() {
            var bErr = false;
            jQuery(".fieldRowErr").removeClass("fieldRowErr");
            jQuery(".slideRoot:visible").each(function() {
                if (!validateSlide(jQuery(this))) {
                    bErr = true;
                }
            });

            if (bErr)
                return false;

            var x = "<slides>";
            jQuery(".slideRoot:visible").each(function() {
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
            
            x += "<title>"+ encodeXml(slideRoot.find(".tbSlideTitle").val()) +"</title>";
            x += "<duration>"+ slideRoot.find(".tbDuration").val() +"</duration>";
            x += "<bkGradFrom>"+ encodeXml(slideRoot.find(".tbColortbBkGradFrom").val()) +"</bkGradFrom>";
            x += "<bkGradTo>"+ encodeXml(slideRoot.find(".tbColortbBkGradTo").val()) +"</bkGradTo>";

            x += "<linkUrl>"+ encodeXml(formatUrl(slideRoot.find(".tbLinkUrl"))) +"</linkUrl>";
            x += "<linkCaption>"+ encodeXml(slideRoot.find(".tbLinkCaption").val()) +"</linkCaption>";
            x += "<linkTarget>"+ encodeXml(slideRoot.find(".ddLinkTarget").val()) +"</linkTarget>";
            x += "<useTextsBk>"+ (slideRoot.find(".cbUseTextsBackground").val() ? "true" : "false") +"</useTextsBk>";

            x += "<mp3Url>"+ encodeXml(formatUrl(slideRoot.find(".tbMp3Url"))) +"</mp3Url>";
            x += "<mp3IconColor>"+ encodeXml(slideRoot.find(".tbMp3IconColor").val()) +"</mp3IconColor>";
            x += "<mp3ShowPlayer>"+ (slideRoot.find(".cbMp3ShowPlayer").val() ? "true" : "false") +"</mp3ShowPlayer>";

            x += "</slide>";
            alert(x);
            return x;
        }

        function encodeXml(sXml) {
            if (!sXml)
                return "";
            sXml = sXml.toString();
            return sXml.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/"/g,"&quot;").replace(/'/g,"&apos;");
        }

        function formatUrl(tb) {
            if (jQuery.trim(tb.val()).length == 0)
                return "";
            var dd = tb.parents(".fieldRow.:first").find(".ddUrl");
            if (dd.val() == "other") {
                return tb;
            }
            
            return dd.val() + tb.val();
        }

        function fillUrl(tb, dd, url) {
            if (!url) {
                return;
            }

            if (url.indexOf("https://") == 0) {
                dd.val("https://");
                tb.val(url.substr("https://".length+1));
            } else if (url.indexOf("http://") == 0) {
                dd.val("http://");
                tb.val(url.substr("http://".length+1));
            } else if (url.indexOf("ftp://") == 0) {
                dd.val("ftp://");
                tb.val(url.substr("ftp://".length+1));
            } else {
                dd.val("other");
                tb.val(url);
            }
        }

    </script>

</body>
</html>
