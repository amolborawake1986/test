<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="LanguageSelection.aspx.cs" Inherits="LanguageSelection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Language Selection"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpCode" runat="server" Text="Employee Code"></asp:Label>
                <asp:TextBox ID="txtEmpCode" runat="server"></asp:TextBox>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpName" runat="server" Text="Employee Name"></asp:Label>
                <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblLangSel" runat="server" Text="Language Selection"></asp:Label>
                <asp:DropDownList ID="cmbLanguage" runat="server" CssClass="combobox">
                </asp:DropDownList>
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="padding-top: 5px; text-align: center;">
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" />
            </div>
            <%--<div style="padding-top: 5px; text-align: center;">
                Data Will diaplay here below.
            </div>--%>
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
