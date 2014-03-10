<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="Test.WebApplication._Default" %>

<%@ Register Assembly="avt.DynamicFlashRotator.Net" Namespace="avt.DynamicFlashRotator.Net" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dynamic Rotator .NET Test</title>
</head>
<body style="background-color: #f2f2ff;">
    <form id="form1" runat="server" submitdisabledcontrols="True">

        <cc1:DynamicRotator
            runat="server" ID="ctlRotator"
            AllowRuntimeConfiguration="True"
            DbConnectionString="SiteSqlServer"
            DbOwner="dbo"
            DbObjectQualifier=""
            ManageUrl="~/Controls/Rotator/ManageRotator.aspx"
            SecurityAllowAspRole=""
            SecurityAllowIps=""
            SecurityAllowInvokeType="">
        </cc1:DynamicRotator>

        <div dir="rtl">
            <cc1:DynamicRotator
                runat="server" ID="ctlRotator2"
                AllowRuntimeConfiguration="True"
                DbConnectionString="SiteSqlServer"
                DbOwner="dbo"
                DbObjectQualifier=""
                ManageUrl="~/Controls/Rotator/ManageRotator.aspx"
                SecurityAllowAspRole=""
                SecurityAllowIps=""
                SecurityAllowInvokeType="">
            </cc1:DynamicRotator>
        </div>
    </form>
</body>
</html>
