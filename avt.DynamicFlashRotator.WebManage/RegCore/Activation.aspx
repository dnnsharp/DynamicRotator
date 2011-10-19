<%@ Page Language="C#" AutoEventWireup="true" Inherits="avt.DynamicFlashRotator.Net.WebManage.RegCore.WebClient.ActivationWnd" EnableViewState = "true" CodeFile="Activation.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title><%= _RegCoreApp.ProductName %> Activation</title>
    <link type = "text/css" rel = "stylesheet" href = "res/style.css" />
</head>

<body id="Body" runat="server">

<form runat = "server" id = "Form" method = "post" onsubmit = "">


<div class = "act_wnd">

    <div class = "title">Activate <%= _RegCoreApp.ProductName %></div>
    <div class = "help">
        <a class = "help_link" href = "http://docs.avatar-soft.ro/doku.php?id=general:activation">Help</a>
    </div>
    <div style = "clear: both;"></div>
    
    <asp:Panel runat = "server" ID = "pnlSuccess" Visible = "false" style ="height: 260px; padding-top: 30px;">
        <br />
        <b>Activation Successfull!</b><br /><br />
        <div style = "text-align: left">
            This copy of <%= _RegCoreApp.ProductName %> has been successfully activated.<br /><br />
            For additional information, support, user manuals and other resources please check our website at <a href="http://www.dnnsharp.com">www.dnnsharp.com</a>
            or the <a href = "<%= _RegCoreServer.GetDocUrl(_RegCoreApp) %>">documentation server</a>.
            <br /><br /><br /><br /><br /><br />
        </div>
    </asp:Panel>

    <div style = "height: 20px;  margin-top: 30px;">
        <asp:CustomValidator runat = "server" ID = "validateActivation" CssClass = "lblerr" Text = "" Display = "Static" ></asp:CustomValidator>
    </div>

    <div style = "height: 290px;" runat = "server" id = "pnlActivateForm">
        
        <div class = "instructions">
            Please input the registration code you've received on email.<br />
            If you don't have one yet, <a href = "<%= _RegCoreServer.GetBuyUrl(_RegCoreApp) %>">click here</a> to purchase a new license.
        </div>
        
        <div class = "reg_code" runat = "server" id = "pnlRegCode" style = "margin-top: 20px;">
            <div class = "field_title">
                Registration code <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass = "lblerr" runat = "server" ControlToValidate = "txtRegistrationCode" Text = "required"></asp:RequiredFieldValidator>
            </div>
            <asp:LinkButton ID="btnNext" runat = "server" CssClass = "btn_next" OnClick = "OnNext">Next</asp:LinkButton>
            <asp:TextBox runat = "server" ID = "txtRegistrationCode" CssClass = "input_field"></asp:TextBox>
            <div class = "small_desc">Please input the registration code you've received on email</div>
            <div style = "clear: both;"></div>
        </div>
        
        <div runat = "server" id = "pnlHosts" visible = "false" style = "margin-top: 20px;">
            <div class = "field_title">
                Select Host <asp:CustomValidator ID="reqHost" CssClass = "lblerr" runat = "server" Text = "required"></asp:CustomValidator>
            </div>
            <asp:LinkButton ID="LinkButton1" runat = "server" CssClass = "btn_next" OnClick = "OnActivate">Activate</asp:LinkButton>
            <asp:DropDownList runat = "server" ID = "ddHosts" CssClass = "input_field"></asp:DropDownList>
            <div class = "small_desc">Choose the host to activate for or manually input host if it's not present in the list (for example domain.com or ip address xxx.xxx.xxx.xxx)</div>
            Other: <asp:TextBox runat = "server" ID = "tbHost" CssClass = "input_field" style = "width: 280px;"></asp:TextBox>
            
            <div style = "clear: both;"></div>
        </div>
        
    </div>
    

    <div style = "height: 290px;" runat = "server" id = "pnlActivateManually" visible = "false">
        <br /><br />
        <b>Instructions for manual activation:</b>
        <br /><br />
        <ol>
            <li>Go to <a runat = "server" id = "btnActivationTool" href = "">Manual Activation Tool at www.dnnsharp.com</a></li>
            <li>Login or create a new account if you don't already have one</li>
            <li>Review information in the form and click Activate when ready</li>
            <li>Copy &amp; Paste activation code in the field below</li>
        </ol>

        <asp:TextBox runat = "server" ID = "tbActCode" Textmode = "MultiLine" style = "width: 460px; height: 120px;"></asp:TextBox>
        <br />
        <asp:LinkButton runat = "server" OnClick = "OnManualActivate">Activate</asp:LinkButton>
    </div>
    
    <div class = "btns">
        <div style = "float: left; margin: 0px;" runat = "server" id = "pnlActivateManuallyBtn" visible = "false">
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