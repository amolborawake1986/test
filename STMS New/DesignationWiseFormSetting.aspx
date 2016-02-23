<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="DesignationWiseFormSetting.aspx.cs" Inherits="DesignationWiseFormSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Designation wise Form Setting"></asp:Label></b>
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
                        <asp:Label ID="lblDesigName" runat="server" Text="Designation Name*"></asp:Label></div>
                    <div style="overflow:auto">
                        <asp:ListBox ID="lstDesigName" runat="server" Style="height: 170px;width: 100%;" AutoPostBack="True"
                            OnSelectedIndexChanged="lstDesigName_SelectedIndexChanged"></asp:ListBox>
                    </div>
                </div>
                <div class="listbox">
                    <div>
                        <asp:Label ID="lblFormName" runat="server" Text="Form Name*"></asp:Label></div>
                    <div style="overflow:auto">
                        <asp:ListBox ID="lstFormName" runat="server" Style="height: 170px;width: 100%;" AutoPostBack="True"
                            SelectionMode="Multiple"></asp:ListBox>
                    </div>
                </div>
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="padding-top: 25px; text-align: center;">
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" />
                <asp:Button ID="btnClose" runat="server" Text="Close" />
            </div>
            <div>
                <br />
                <asp:GridView ID="gv_DesignationWiseFormSetting" HorizontalAlign="Center" runat="server"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
