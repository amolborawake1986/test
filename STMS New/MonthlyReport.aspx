<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="MonthlyReport.aspx.cs" Inherits="MonthlyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Monthly Report"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblMonthStartFrom" runat="server" Text="Month Starts From*"></asp:Label>
                <asp:RadioButton ID="RadioButton1" runat="server" Text="1st" 
                    GroupName="MonthStart" Checked="True" />
                <asp:RadioButton ID="RadioButton2" runat="server" Text="21st" GroupName="MonthStart" />
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblTeamName" runat="server" Text="Team Name*"></asp:Label>
                <asp:DropDownList ID="cmbTeamName" runat="server" CssClass="combobox">
                    <asp:ListItem>test</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="cmbTeamName"
                    Display="Dynamic" ErrorMessage="Please Select Team Name." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpName" runat="server" Text="Employee Name*"></asp:Label>
                <asp:ListBox ID="lstEmpName" runat="server" Style="width: 192px; height: 100px;">
                    <asp:ListItem>test</asp:ListItem>
                </asp:ListBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstEmpName"
                    Display="Dynamic" ErrorMessage="Please Select Employee Name." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblMonth" runat="server" Text="Month*"></asp:Label>
                <asp:DropDownList ID="cmbMonth" runat="server" CssClass="combobox">
                    <asp:ListItem>test</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbMonth"
                    Display="Dynamic" ErrorMessage="Please Select Month." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblYear" runat="server" Text="Year*"></asp:Label>
                <asp:DropDownList ID="cmbYear" runat="server" CssClass="combobox">
                    <asp:ListItem>test</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbYear"
                    Display="Dynamic" ErrorMessage="Please select Year." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblOutputPath" runat="server" Text="Set output path folder*"></asp:Label>
                <asp:TextBox ID="txtOutputPath" runat="server" CssClass="combobox"></asp:TextBox>
                <asp:Button ID="btnBrowse" runat="server" Text="Browse" 
                    onclick="btnBrowse_Click" />
            </div>
        </div>
        <div class="EmpMasterConDiv">
                &nbsp;</div>
        <div style="padding-top: 25px; text-align: center;">
            <asp:Button ID="btnReport" runat="server" Text="Report" />
            <asp:Button ID="btnClose" runat="server" Text="Close" />
        </div>
        <div style="padding-top: 5px; text-align: center;">
            Data Will diaplay here below.
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
