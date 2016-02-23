<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="DepartmentTeamSetting.aspx.cs" Inherits="DepartmentTeamSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Department Team Setting"></asp:Label></b>
            </div>
            <%--<div style="padding-top: 5px; display: -webkit-inline-box;">
                <div style="display: inline;">
                    User Name<asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
                </div>
                <div style="display: inline;">
                    Project Name<asp:ListBox ID="ListBox2" runat="server"></asp:ListBox>
                </div>
            </div>--%>
            <div style="padding-top: 5px; display: inline-flex; width: 100%; padding-left: 25%;">
                <div class="listbox">
                    <div>
                        <asp:Label ID="lblDeptName" runat="server" Text="Department Name*"></asp:Label></div>
                    <div style="overflow: auto">
                        <asp:ListBox ID="lstDeptName" runat="server" Style="height: 170px; width: 100%;"
                            OnSelectedIndexChanged="lstDeptName_SelectedIndexChanged" AutoPostBack="True">
                        </asp:ListBox>
                    </div>
                </div>
                <div class="listbox">
                    <div>
                        <asp:Label ID="lblTeamName" runat="server" Text="Team Name*"></asp:Label></div>
                    <div style="overflow:auto">
                        <asp:ListBox ID="lstTeamName" runat="server" Style="height: 170px;width: 100%;" SelectionMode="Multiple" AutoPostBack="True"></asp:ListBox>
                    </div>
                </div>
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="padding-top: 25px; text-align: center;">
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
            </div>
            <div>
                <br />
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
