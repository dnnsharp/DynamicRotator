<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="Test.WebApplication._Default" %>

<%@ Register assembly="avt.AllinOneRotator.Net" namespace="avt.AllinOneRotator.Net" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <cc1:AllinOneRotator ID="AllinOneRotator1" runat="server" 
        AutoStartSlideShow="True" RoundCornerMaskColor="#333399" ShowTopTitle="False" 
        SlideButtonsNumberColor="#6600CC" SlideButtonsType="RoundNoNumbers" 
        DbConnectionString="SiteSqlServer" EnableRuntimeConfiguration="True">
        <Slides>
            <cc1:SlideInfo AppearFrom="Right" BackgroundGradientFrom="#003399" 
                BackgroundGradientTo="#9900CC" DurationSeconds="10" EasingType="Out" 
                FinalXposition="15" GlowColor="White" GlowSize="10" GlowStrength="3" 
                IconColor="Black" JustFade="False" MoveType="Strong" ShowPlayer="True" 
                Target="Self" TextBackgroundColor="White" TextBackgroundTransparency="70" 
                TextContent="Content of new slide, supports &lt;i&gt;HTML&lt;/i&gt;." 
                TextPadding="5" TextTransparency="10" Title="New Slide Title" 
                TransitionDuration="2" UseBackground="False" UseTextsBackground="True" 
                VerticalAlign="Middle">
                <SlideObjects>
                    <cc1:SlideObjectInfo AppearFrom="Top" EasingType="Out" Effect="None" 
                        GlowColor="White" GlowSize="10" GlowStrength="3" JustFade="False" 
                        MoveType="Strong" ObjectUrl="/images/image1.png" TimeDelay="1" 
                        TransitionDuration="2" Xposition="15" Yposition="15" />
                </SlideObjects>
            </cc1:SlideInfo>
            <cc1:SlideInfo AppearFrom="Right" BackgroundGradientFrom="White" 
                BackgroundGradientTo="White" DurationSeconds="10" EasingType="Out" 
                FinalXposition="15" GlowColor="#FF3300" GlowSize="10" GlowStrength="3" 
                IconColor="Black" JustFade="False" MoveType="Strong" ShowPlayer="True" 
                Target="Self" TextBackgroundColor="White" TextBackgroundTransparency="70" 
                TextContent="Content of new slide, supports &lt;i&gt;HTML&lt;/i&gt;." 
                TextPadding="5" TextTransparency="10" Title="New Slide Title" 
                TransitionDuration="2" UseBackground="False" UseTextsBackground="True" 
                VerticalAlign="Middle">
                <SlideObjects>
                    <cc1:SlideObjectInfo AppearFrom="Top" EasingType="Out" Effect="None" 
                        GlowColor="White" GlowSize="10" GlowStrength="3" JustFade="False" 
                        MoveType="Strong" ObjectUrl="/images/image1.png" TimeDelay="1" 
                        TransitionDuration="2" Xposition="15" Yposition="15" />
                </SlideObjects>
            </cc1:SlideInfo>
        </Slides>
        
    </cc1:AllinOneRotator>
    </form>
</body>
</html>
