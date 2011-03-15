<%@ Page Language="C#" AutoEventWireup="True" Inherits="avt.DynamicFlashRotator.Net.ActivationWnd" EnableViewState = "true" Codebehind="Activation.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>DynamicRotator Activation</title>
    <link type = "text/css" rel = "stylesheet" href = "<%=TemplateSourceDirectory %>/act/style.css" />
    
    <style type = "text/css">
        
    </style>
</head>

<body id="Body" runat="server">

<form runat = "server" id = "Form" method = "post" onsubmit = "">


<div class = "act_wnd">

    <div class = "title">Activate DynamicRotator</div>
    <div class = "help">
        <a class = "help_link" href = "http://docs.avatar-soft.ro/doku.php?id=general:activation">Help</a>
    </div>
    <div style = "clear: both;"></div>
    
    <asp:Panel runat = "server" ID = "pnlSuccess" Visible = "false" style ="height: 260px; padding-top: 30px;">
        <br />
        <b>Activation Successfull!</b><br /><br />
        <div style = "text-align: left">
            This copy of DynamicRotator has been successfully activated.<br /><br />
            For additional information, support, user manuals and other resources please check our website at <a href="http://www.avatar-soft.ro">www.avatar-soft.ro</a>
            or the <a href = "<%= avt.DynamicFlashRotator.Net.Settings.RotatorSettings.DocSrv%>">documentation server</a>.
            <br /><br /><br /><br /><br /><br />
        </div>
    </asp:Panel>

    <div style = "height: 20px;  margin-top: 30px;">
        <asp:CustomValidator runat = "server" ID = "validateActivation" CssClass = "lblerr" Text = "" Display = "Static" ></asp:CustomValidator>
    </div>

    <div style = "height: 290px;" runat = "server" id = "pnlActivateForm">
        
        <div class = "instructions">
            Please input the registration code you've received on email.<br />
            If you don't have one yet, <a href = "<%= avt.DynamicFlashRotator.Net.Settings.RotatorSettings.BuyLink %>&aspnet=true">click here</a> to purchase a new license.
        </div>
        
        <div class = "reg_code" runat = "server" id = "pnlRegCode" style = "margin-top: 20px;">
            <div class = "field_title">
                Registration code <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass = "lblerr" runat = "server" ControlToValidate = "txtRegistrationCode" Text = "required"></asp:RequiredFieldValidator>
            </div>

            <asp:TextBox runat = "server" ID = "txtRegistrationCode" CssClass = "input_field"></asp:TextBox>
            <div class = "small_desc">Please input the registration code you've received on email</div>
            <div style = "clear: both;"></div>
        </div>
        
        <div runat = "server" id = "pnlHosts" style = "margin-top: 20px;">
            <div class = "field_title">
                Select Host <asp:RequiredFieldValidator CssClass = "lblerr" runat = "server" ControlToValidate = "tbHost" Text = "required"></asp:RequiredFieldValidator>
            </div>
            <asp:LinkButton runat = "server" CssClass = "btn_next" OnClick = "OnActivate">Activate</asp:LinkButton>
            <asp:TextBox runat = "server" ID = "tbHost" CssClass = "input_field" style = "width: 280px;"></asp:TextBox>
            <div class = "small_desc">Domain name or IP address depending on license.</div>
            <div style = "clear: both;"></div>
        </div>
        
    </div>
    

    <div style = "height: 290px;" runat = "server" id = "pnlActivateManually" visible = "false">
        <br /><br />
        <b>Instructions for manual activation:</b>
        <br /><br />
        <ol>
            <li>Go to <a runat = "server" id = "btnActivationTool" href = "">Manual Activation Tool at www.avatar-soft.ro</a></li>
            <li>Login or create a new account if you don't already have one</li>
            <li>Review information in the form and click Activate when ready</li>
            <li>Copy the .lic file to your website /bin folder</li>
        </ol>
    </div>
    
    <div class = "btns">
        <div style = "float: left; margin: 0px;" runat = "server" id = "pnlActivateManuallyBtn">
            <asp:LinkButton runat = "server" OnClick = "OnActivateManually">Activate Manually</asp:LinkButton><br />
            <div style = "font-size: 10px; color: #525252;">
                Choose this option if automatic activation fails.<br />
            </div>
        </div>
        <a runat = "server" id = "lnkPurchase" class = "btn_purchase">Purchase</a>
        <asp:LinkButton ID="btnClose" runat = "server" CssClass = "btn_cancel" OnClick = "OnCloseSA" CausesValidation = "false">Cancel</asp:LinkButton>
        <div style = "clear: both;"></div>
    </div>

</div>


</form>
</body>
</html>