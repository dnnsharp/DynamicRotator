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
    <cc1:DynamicRotator ID="AllinOneRotator1" runat="server" 
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
            <cc1:SlideInfo BackgroundGradientFrom="White" BackgroundGradientTo="White" 
                ClickAnywhere="True" DurationSeconds="10" Target="_self" 
                TextContent="Content of new slide, supports &lt;i&gt;HTML&lt;/i&gt;." 
                Title="Slide 1" UseTextsBackground="True">
                <SlideObjects>
                    <cc1:SlideObjectInfo AppearMode="Slide" EffectAfterSlide="None" 
                        GlowColor="#FFCC99" GlowSize="10" GlowStrength="3" Name="New Slide Object" 
                        ObjectType="Text" ObjectUrl="" Opacity="100" SlideEasingType="Out" 
                        SlideFrom="Left" SlideMoveType="Strong" 
                        Text="Slide text, supports &lt;font color='#ff0000'&gt;HTML&lt;/font&gt;" 
                        TextBackgroundColor="White" TextBackgroundOpacity="0" TextBackgroundPadding="5" 
                        TextColor="#CC00CC" TimeDelay="1" TransitionDuration="2" VerticalAlign="Middle" 
                        ViewOrder="0" Width="-1" Xposition="15" Yposition="15" />
                    <cc1:SlideObjectInfo AppearMode="Slide" EffectAfterSlide="None" 
                        GlowColor="White" GlowSize="10" GlowStrength="3" Name="New Slide Object" 
                        ObjectType="Image" ObjectUrl="~/images/large_img1.jpg" Opacity="100" 
                        SlideEasingType="Out" SlideFrom="Right" SlideMoveType="Strong" 
                        Text="Slide text, supports &lt;font color='#ff0000'&gt;HTML&lt;/font&gt;" 
                        TextBackgroundColor="White" TextBackgroundOpacity="0" TextBackgroundPadding="5" 
                        TextColor="#424242" TimeDelay="1" TransitionDuration="2" VerticalAlign="Middle" 
                        ViewOrder="0" Width="-1" Xposition="15" Yposition="15" />
                </SlideObjects>
            </cc1:SlideInfo>
            <cc1:SlideInfo BackgroundGradientFrom="White" BackgroundGradientTo="White" 
                ClickAnywhere="True" DurationSeconds="10" Target="_self" 
                TextContent="Content of new slide, supports &lt;i&gt;HTML&lt;/i&gt;." 
                Title="Slide2" UseTextsBackground="True">
            </cc1:SlideInfo>
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
