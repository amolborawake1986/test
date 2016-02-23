<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="DepartmentMaster.aspx.cs" Inherits="DepartmentMaster" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Department Master"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblDeptName" runat="server" Text="Department Name*"></asp:Label>
                <asp:TextBox ID="txtDeptName" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDeptName"
                    Display="Dynamic" ErrorMessage="Please Enter Department" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="padding-top: 5px; text-align: center;">
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" ValidationGroup="UnvalidatedControls" />
                <asp:Button ID="btnClose" runat="server" Text="Close" />
            </div>
            <div style="text-align: center; overflow:scroll; width: 97%;" class="EmpMasterConDiv">
                <div style="height: 200px;">
                    <asp:GridView ID="gv_DeptMaster" runat="server" CellPadding="4" ForeColor="#333333"
                        GridLines="Horizontal"  ShowHeaderWhenEmpty="True" onrowdatabound="gv_DeptMaster_RowDataBound" HorizontalAlign="Center"
                        onselectedindexchanged="gv_DeptMaster_SelectedIndexChanged">
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
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
