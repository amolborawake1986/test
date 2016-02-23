<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(window).load(function () {
            $('#nivo-slider').nivoSlider();
        });
    </script>
    <div style="padding-left: 16px;">
        <%--<div class="SliderDiv">--%>
        <div class=" SliderDiv">
            <div id="nivo-slider" class="nivoSlider" style="margin-top: 12%;">
                <img src="Images/1.gif" alt="" />
                <img src="Images/2.png" alt="" />
                <img src="Images/4.jpg" alt="" />
                <img src="Images/6.jpg" alt="" />
                <img src="Images/7.jpg" alt="" />
                <img src="Images/8.jpg" alt="" />
                
            </div>
        </div>
        <%--</div>--%>
        <div class="loginMainDiv">
            <div style="text-align: center;">
                <div style="font-size: large; padding-top: 23%;">
                    <b>Login</b>
                </div>
                <div style="width: 80%;">
                    User Name
                </div>
                <div>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </div>
                <div style="width: 80%;">
                    Password
                </div>
                <div>
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div style="width: 90%; padding-top: 5px; padding-left: 25PX;">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
                <div style="width: 90%; padding-top: 5px; padding-left: 25PX;">
                    <asp:Label ID="lbl_Error" runat="server" Text="Please enter valid User Name and Password."
                        ForeColor="Red" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
