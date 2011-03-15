<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ManageRotator.aspx.cs" Inherits="avt.DynamicFlashRotator.Net.WebManage.ManageRotator" ValidateRequest="false" %>
<%@ Register TagPrefix="avt" TagName="ManageDynamicRototator" Src="ManageRotatorBase.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Dynamic Rotator.NET Settings</title>
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
    
        <avt:ManageDynamicRototator runat="server" id = "ctlManageRotator"></avt:ManageDynamicRototator>

    </form>

</body>
</html>
