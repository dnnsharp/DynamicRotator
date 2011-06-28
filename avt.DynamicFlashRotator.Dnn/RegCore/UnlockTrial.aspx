<%@ Page Language="C#" AutoEventWireup="True" CodeFile="UnlockTrial.aspx.cs" Inherits="avt.DynamicFlashRotator.Net.RegCore.WebClient.UnlockTrial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unlock <%= _RegCoreApp.ProductName%> 30 Days Trial</title>
    <link type = "text/css" rel = "stylesheet" href = "res/style.css" />
</head>
<body>
    <form runat="server">

    <div class = "act_wnd">
        <div class = "title">Unlock <%= _RegCoreApp.ProductName%> 30 Days Trial</div>
        <div>

            <div style="margin: 80px 0px;">

                <div style="color:#108CB2;">
                    Unlock full functionalities for 30 days so you can evaluate how this software meets the requirement.
                </div>

                <div style = "margin:20px;" runat="server" id="pnlUnlockTrial">
                    <b>Email Address:</b>
                    <asp:RequiredFieldValidator runat="server" ID="vldUnlockTrial_EmailReq" ControlToValidate="tbTrialEmail" Text="Email is required" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="vldUnlockTrial_EmailFormat" ControlToValidate="tbTrialEmail" Text="Invalid email" Display="Dynamic" ValidationExpression="^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$"></asp:RegularExpressionValidator>
                    <br />
                    <asp:TextBox runat ="server" ID = "tbTrialEmail" style="width: 360px; font-size:13px;"></asp:TextBox>
                    <asp:Button runat="server" ID = "btnUnlockTrial" Text="Unlock &gt;" OnClick="OnUnlockTrial" />
                    <div style="font-size: 12px; color: #525252;">
                        We respect your privacy and will protect your email address.
                    </div>
                </div>

                <div style = "margin:20px; color: #ea2222; font-weight: bold; font-size: 16px;" runat="server" id="pnlTrialExpired">
                    TRIAL EXPIRED
                </div>

            </div>

            <div style="border: 1px solid #108CB2; padding: 10px; background-color:White; margin: 4px; margin-top: 160px;">
                <div>
                    Ready to own a license? <br />
                    <a href = "<%= _RegCoreServer.GetBuyUrl(_RegCoreApp) %>">Purchase a <%= _RegCoreApp.ProductName %> license here</a>
                    or <a href="http://www.avatar-soft.ro/Contact.aspx">Contact Us</a> for more information...
                </div>
            </div>

        </div>

    </div>
    </form>
</body>
</html>