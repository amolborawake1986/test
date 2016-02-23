<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="SubmitTimesheetByAdmin.aspx.cs" Inherits="SubmitTimesheetByAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=dtpDate.ClientID %>").dynDateTime({
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
                    <asp:Label ID="lblHeadings" runat="server" Text="Submit Timesheet Of User By Admin"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmployee" runat="server" Text="Select Employee*"></asp:Label>
                <asp:DropDownList ID="cmbEmployee" runat="server" CssClass="combobox" >
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="cmbEmployee"
                    Display="Dynamic" ErrorMessage="Please Select Employee." ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblSelectDate" runat="server" Text="Select Date*"></asp:Label>
                <asp:TextBox ID="dtpDate" runat="server" AutoPostBack="True" 
                    ontextchanged="dtpDate_TextChanged"></asp:TextBox><img src="Images/calender.png" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpDate"
                    Display="Dynamic" ErrorMessage="Please Select Date" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" Width="147px" ID="lblInTime" runat="server" Text="InTime"></asp:Label>
                <asp:DropDownList ID="cmbInHrs" runat="server" Width="50px" 
                    ValidationGroup="group1">
                </asp:DropDownList>
                <asp:Label ID="lblFromHrs" runat="server" Text="Hrs."></asp:Label>
                <asp:DropDownList ID="cmbInMin" runat="server" Width="50px" 
                    ValidationGroup="group1">
                </asp:DropDownList>
                <asp:Label ID="lblFromMin" runat="server" Text="Min."></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbInHrs"
                    Display="Dynamic" ErrorMessage="Please Select Intime Hrs." ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="group1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbInMin"
                    Display="Dynamic" ErrorMessage="Select Intime Min." ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="group1"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" Width="147px" ID="lblOutTime" runat="server" Text="Out Time"></asp:Label>
                <asp:DropDownList ID="cmbOutHrs" runat="server" Width="50px" 
                    ValidationGroup="group1">
                </asp:DropDownList>
                <asp:Label ID="lblToHrs" runat="server" Text="Hrs."></asp:Label>
                <asp:DropDownList ID="cmbOutMin" runat="server" Width="50px" 
                    ValidationGroup="group1">
                </asp:DropDownList>
                <asp:Label ID="lblToMin" runat="server" Text="Min."></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbOutHrs"
                    Display="Dynamic" ErrorMessage="Please Select outtime Hrs. " ForeColor="Red"
                    InitialValue="- -" ValidationGroup="group1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbOutMin"
                    Display="Dynamic" ErrorMessage="Select OutTime Min." ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="group1"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="EmpMasterConDiv">
                &nbsp;</div>
        <div style="padding-top: 25px; text-align: center;">
            <asp:Button ID="btnShow" runat="server" Text="Show" Enabled="False" 
                onclick="btnShow_Click" />
            <asp:Button ID="btnSubmitTimesheet" runat="server" Text="Submit Timesheet" 
                Enabled="False" ValidationGroup="group1" 
                onclick="btnSubmitTimesheet_Click" />
            <asp:Button ID="btnClose" runat="server" Text="Close" />
        </div>
                 <div style="overflow:scroll">
            <br />
            <asp:GridView ID="gv_SubmitByAdmin" HorizontalAlign="Center" runat="server" 
                 CellPadding="4" ForeColor="#333333"
                GridLines="Horizontal"  ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                         onrowdatabound="gv_SubmitByAdmin_RowDataBound">
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
                <Columns>
                <asp:BoundField HeaderText="Project Name" DataField="Project Name" />
                <asp:BoundField HeaderText="Task Name" DataField="Task Name" />
                <asp:BoundField HeaderText="Check In Time(Hrs.Min)" DataField="CheckInTime" />
                <asp:BoundField HeaderText="Check Out Time(Hrs.Min)" DataField="CheckOutTime" />
                <asp:BoundField HeaderText="Indoor/Outdoor" DataField="InddorOutdoor" />
                <asp:BoundField HeaderText="Spend Time" DataField="SpentTime" />
                <asp:BoundField HeaderText="Added Later" DataField="AddedLater" />

                </Columns>
            </asp:GridView>
           </div>

    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
