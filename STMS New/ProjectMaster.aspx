<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="ProjectMaster.aspx.cs" Inherits="ProjectMaster" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=dtpStartDate.ClientID %>,#<%=dtpEndDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: true,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });

       
    </script>
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Project Master"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblProjectName" runat="server" Text="Project Name*"></asp:Label>
                <asp:TextBox ID="txtProjName" runat="server" MaxLength="150"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtProjName"
                    Display="Dynamic" ErrorMessage="Please Enter Project Name." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblStartDate" runat="server" Text="Start Date*"></asp:Label>
                <asp:TextBox ID="dtpStartDate" runat="server"></asp:TextBox><img
                    src="Images/calender.png" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpStartDate"
                    Display="Dynamic" ErrorMessage="Select Start Date." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
                <asp:TextBox ID="dtpEndDate" runat="server"></asp:TextBox><img src="Images/calender.png" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpStartDate"
                    ControlToValidate="dtpEndDate" Display="Dynamic" ErrorMessage="Date must be greater than Start Date"
                    ForeColor="Red" Operator="GreaterThan" Type="Date"></asp:CompareValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblStatus" runat="server" Text="Status"></asp:Label><asp:CheckBox
                    ID="chkStatus" runat="server" Text="Active/Inactive" />
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="padding-top: 5px; text-align: center;">
                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" ValidationGroup="UnvalidatedControl" />
                <asp:Button ID="btnClose" runat="server" Text="Close" ValidationGroup="UnvalidatedControl" />
            </div>
            <div style="text-align: center; overflow:scroll; width: 97%;" class="EmpMasterConDiv">
                
                <div style="height: 200px;">
                <asp:GridView ID="gv_ProjectMaster" runat="server" CellPadding="4" ForeColor="#333333" 
                        GridLines="Horizontal"  ShowHeaderWhenEmpty="True" onrowdatabound="gv_ProjectMaster_RowDataBound" HorizontalAlign="Center"
                        onselectedindexchanged="gv_ProjectMaster_SelectedIndexChanged">
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
