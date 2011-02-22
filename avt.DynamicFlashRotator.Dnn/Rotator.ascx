<%@ Control language="C#" Inherits="avt.DynamicFlashRotator.Dnn.Rotator" AutoEventWireup="true" Explicit="True" CodeFile="Rotator.ascx.cs" %>

<%@ Register assembly="avt.AllinOneRotator.Net" namespace="avt.AllinOneRotator.Net" tagprefix="avt" %>

<avt:AllinOneRotator ID="AllinOneRotator1" runat="server" 
    AutoStartSlideShow="True" RoundCornerMaskColor="#333399" ShowTopTitle="False" 
    SlideButtonsNumberColor="#6600CC" SlideButtonsType="RoundNoNumbers" 
    DbConnectionString="SiteSqlServer" EnableRuntimeConfiguration="True" 
    ManageUrl="~/Controls/Rotator/ManageRotator.aspx" Width="800px"> 
</avt:AllinOneRotator>

