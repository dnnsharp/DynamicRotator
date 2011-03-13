<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="Test.WebApplication._Default" %>

<%@ Register assembly="avt.DynamicFlashRotator.Net" namespace="avt.DynamicFlashRotator.Net" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: #f2f2ff;">
    <form id="form1" runat="server">
    <div>
    
    </div>

    <cc1:DynamicRotator ID="AllinOneRotator8" runat="server" 
        AutoStartSlideShow="True" RoundCornerMaskColor="#333399" ShowTopTitle="False" 
        SlideButtonsNumberColor="#6600CC" SlideButtonsType="RoundNoNumbers" 
        DbConnectionString="SiteSqlServer" EnableRuntimeConfiguration="True" 
        ManageUrl="~/Controls/Rotator/ManageRotator.aspx" Width="562px" 
        DbOwner="dbo" FadeColor="White" Height="250px" ShowBottomButtons="True" 
        ShowPlayPauseControls="True" ShowTimerBar="True" SlideButtonsColor="#121212" 
        SlideButtonsXoffset="20" SlideButtonsYoffset="35" TopTitleBackground="Black" 
        TopTitleBgTransparency="70" TopTitleTextColor="White" 
        TransparentBackground="False" UseRoundCornersMask="False" 
        AllowRuntimeConfiguration="True" SecurityAllowAspRole="" 
        SecurityAllowIps="193.254.62.222;192.168..0.199;127.0.0.1" >
        <Slides>
            <cc1:SlideInfo Title ="New Slide Title" 
                TextContent="Content of new slide, supports &lt;i&gt;HTML&lt;/i&gt;."></cc1:SlideInfo>
            <cc1:SlideInfo BackgroundGradientFrom="White" BackgroundGradientTo="White" 
                ClickAnywhere="True" DurationSeconds="10" Target="_self" 
                TextContent="Content of new slide, supports &lt;i&gt;HTML&lt;/i&gt;." 
                Title="New Slide Title" UseTextsBackground="True">
            </cc1:SlideInfo>
        </Slides>
    </cc1:DynamicRotator>
    <br /><br />
    <cc1:DynamicRotator ID="AllinOneRotator2" runat="server" 
        AutoStartSlideShow="True" RoundCornerMaskColor="#333399" ShowTopTitle="False" 
        SlideButtonsNumberColor="#6600CC" SlideButtonsType="RoundNoNumbers" 
        DbConnectionString="SiteSqlServer" EnableRuntimeConfiguration="False" 
        ManageUrl="~/Controls/Rotator/ManageRotator.aspx" Width="800px"> 
    </cc1:DynamicRotator>
    <br /><br />
     <cc1:DynamicRotator ID="AllinOneRotator3" runat="server" 
        AutoStartSlideShow="True" RoundCornerMaskColor="#333399" ShowTopTitle="False" 
        SlideButtonsNumberColor="#6600CC" SlideButtonsType="RoundNoNumbers" 
        DbConnectionString="SiteSqlServer" EnableRuntimeConfiguration="False" 
        ManageUrl="~/Controls/Rotator/ManageRotator.aspx" Width="800px"> 
    </cc1:DynamicRotator>
    </form>
</body>
</html>
