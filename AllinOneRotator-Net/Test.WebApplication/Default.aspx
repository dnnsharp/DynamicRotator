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
        AutoStartSlideShow="False" RoundCornerMaskColor="#333399" ShowTopTitle="False" 
        SlideButtonsNumberColor="#6600CC" SlideButtonsType="RoundNoNumbers">
    </cc1:AllinOneRotator>
    </form>
</body>
</html>
