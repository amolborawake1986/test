<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="LanguageSelectionSetting.aspx.cs" Inherits="LanguageSelectionSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Language Selection Setting"></asp:Label></b>
            </div>
            
            
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblLangSel" runat="server" Text="Language Selection*"></asp:Label><asp:DropDownList
                    ID="cmbLanguage" runat="server" CssClass="combobox">
                    <asp:ListItem>test</asp:ListItem>
                </asp:DropDownList>
            </div>
             <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblFormName" runat="server" Text="Select Form Name*"></asp:Label><asp:DropDownList
                    ID="cmbFormName" runat="server" CssClass="combobox">
                     <asp:ListItem>test</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="padding-top: 5px; text-align: center;">
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" 
                    onclick="btnClose_Click" />
                <asp:Button ID="btnTransAll" runat="server" Text="Translate All" 
                    onclick="btnTransAll_Click" />
            </div>
            <asp:GridView ID="gv_LanguageSelectionSetting" runat="server" CellPadding="4" ForeColor="#333333"
                GridLines="Horizontal"  ShowHeaderWhenEmpty="True">
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
    <div style="clear: both;">
    </div>
</asp:Content>

