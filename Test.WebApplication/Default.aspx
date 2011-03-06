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
        ManageUrl="~/Controls/Rotator/ManageRotator.aspx" Width="800px" ResourceUrl = "/images"> 
    </cc1:DynamicRotator>
    <br /><br />
    <cc1:DynamicRotator ID="AllinOneRotator2" runat="server" 
        AutoStartSlideShow="True" RoundCornerMaskColor="#333399" ShowTopTitle="False" 
        SlideButtonsNumberColor="#6600CC" SlideButtonsType="RoundNoNumbers" 
        DbConnectionString="SiteSqlServer" EnableRuntimeConfiguration="True" 
        ManageUrl="~/Controls/Rotator/ManageRotator.aspx" Width="800px"> 
    </cc1:DynamicRotator>
     <cc1:DynamicRotator ID="AllinOneRotator3" runat="server" 
        AutoStartSlideShow="True" RoundCornerMaskColor="#333399" ShowTopTitle="False" 
        SlideButtonsNumberColor="#6600CC" SlideButtonsType="RoundNoNumbers" 
        DbConnectionString="SiteSqlServer" EnableRuntimeConfiguration="True" 
        ManageUrl="~/Controls/Rotator/ManageRotator.aspx" Width="800px"> 
    </cc1:DynamicRotator>
    </form>
</body>
</html>
