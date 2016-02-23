<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="DemoReport.aspx.cs" Inherits="MonthlyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Report"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblTeamName" runat="server" 
                    Text="Employee Name*"></asp:Label>
                <asp:DropDownList ID="cmbEmpName" runat="server" CssClass="combobox">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="cmbEmpName"
                    Display="Dynamic" ErrorMessage="Please Select Employee Name." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblMonth" runat="server" Text="Month*"></asp:Label>
                <asp:DropDownList ID="cmbMonth" runat="server">
                    <asp:ListItem Selected="True" Value="01">Jan</asp:ListItem>
                    <asp:ListItem Value="02">Feb</asp:ListItem>
                    <asp:ListItem Value="03">Mar</asp:ListItem>
                    <asp:ListItem Value="04">Apr</asp:ListItem>
                    <asp:ListItem Value="05">May</asp:ListItem>
                    <asp:ListItem Value="06">Jun</asp:ListItem>
                    <asp:ListItem Value="07">Jul</asp:ListItem>
                    <asp:ListItem Value="08">Aug</asp:ListItem>
                    <asp:ListItem Value="09">Sept</asp:ListItem>
                    <asp:ListItem Value="10">Oct</asp:ListItem>
                    <asp:ListItem Value="11">Nov</asp:ListItem>
                    <asp:ListItem Value="12">Dec</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbMonth"
                    Display="Dynamic" ErrorMessage="Please Select Month." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblYear" runat="server" Text="Year*"></asp:Label>
                <asp:DropDownList ID="cmbYear" runat="server">
                    <asp:ListItem>2013</asp:ListItem>
                    <asp:ListItem>2014</asp:ListItem>
                    <asp:ListItem>2015</asp:ListItem>
                    <asp:ListItem>2016</asp:ListItem>
                    <asp:ListItem>2017</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2010</asp:ListItem>
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
            <div class="MiddleContentDiv">
                <asp:Label  ID="lblMsg" runat="server" 
                    Text="Report Genereted Succefully." Visible="False" ForeColor="#009933"></asp:Label>
                
            </div>
        </div>
        <div class="EmpMasterConDiv">
                &nbsp;</div>
        <div style="padding-top: 25px; text-align: center;">
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click" />
            <asp:Button ID="btnClose" runat="server" Text="Close" />
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
