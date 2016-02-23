<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="TakeBreak.aspx.cs" Inherits="CheckInOut" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function noBack() { window.history.forward(); }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack(); }
        window.onunload = function () { void (0); }
    </script>
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Break Details"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblProjName" runat="server" Text="Project Name*" />
                <asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" Width="253px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDetail"
                    Display="Dynamic" ErrorMessage="Please Fill Detail of Break." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div>
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="padding-top: 5px; text-align: center;">
                <asp:Button ID="btnBreakStart" runat="server" Text="Break Start" OnClick="btnBreakStart_Click" />
                <asp:Button ID="btnBreakStop" runat="server" Text="Break Stop" OnClick="btnBreakStop_Click"
                    Enabled="False" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                    ValidationGroup="aa" />
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
