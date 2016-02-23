<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="CheckOutOtherDay.aspx.cs" Inherits="CheckInOut" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="You have not check out the following task. Please check out it."></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" Width="147px" ID="lblOutTime" runat="server" Text="Out Time"></asp:Label>
                <asp:DropDownList ID="cmbOutHrs" runat="server" Width="50px" 
                    ValidationGroup="Group1" >
                </asp:DropDownList>
                <asp:Label ID="lblToHrs" runat="server" Text="Hrs."></asp:Label>
                <asp:DropDownList ID="cmbOutMin" runat="server" Width="50px" 
                    ValidationGroup="Group1">
                </asp:DropDownList>
                <asp:Label ID="lblToMin" runat="server" Text="Min."></asp:Label>
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbOutHrs"
                    Display="Dynamic" ErrorMessage="Please Select outtime Hrs. " ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="cmbOutMin"
                    Display="Dynamic" ErrorMessage="Select OutTime Min." ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                   Display="Dynamic" ForeColor="Red" 
                    ErrorMessage="Outtime Should be greater than Intime." 
                    ControlToCompare="cmbInHrs" ControlToValidate="cmbOutHrs" 
                    Operator="GreaterThanEqual" ></asp:CompareValidator>
            </div>
        </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="padding-top: 5px; text-align: center;">
                <asp:Button ID="btnChkOut" runat="server" Text="Check Out"  />
            </div>
            <div style="overflow: scroll">
                <br />
                <asp:GridView ID="gv_CheckInOut" HorizontalAlign="Center" runat="server" CellPadding="4"
                    ForeColor="#333333" GridLines="Horizontal" ShowHeaderWhenEmpty="True" >
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
