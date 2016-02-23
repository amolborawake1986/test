<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="SetChkInOut.aspx.cs" Inherits="SetChkInOut" EnableEventValidation="false" %>

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
    <div style="float: left; width: 82%; background-color: aliceblue; min-height: 420px;
        display: inline-flex; height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%; width: 75%; display: inline-block;">
            <div align="center" style="padding-top: 2%; height: 48px; width: 100%;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Set Check In-Out"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblSelectDate" runat="server" Text="Select Date*"></asp:Label>
                <asp:TextBox ID="dtpDate" runat="server"></asp:TextBox><img src="Images/calender.png" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpDate"
                    Display="Dynamic" ErrorMessage="Select Date" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv" style="padding-left: 25%;">
                <asp:RadioButton ID="rbtEmpwise" runat="server" Text="Employeewise" GroupName="Group"
                    Checked="true" AutoPostBack="True" OnCheckedChanged="rbtEmpwise_CheckedChanged" />
                <asp:RadioButton ID="rbtnTeamWise" runat="server" Text="Team Wise" GroupName="Group"
                    AutoPostBack="True" OnCheckedChanged="rbtnTeamWise_CheckedChanged" />
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblName" runat="server" Text="Employee Name*"></asp:Label>
                <asp:DropDownList CssClass="combobox" ID="cmbTeamName" runat="server"
                    OnSelectedIndexChanged="cmbTeamName_SelectedIndexChanged">
                    
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="cmbTeamName"
                    Display="Dynamic" ErrorMessage="Please Select." ForeColor="Red" InitialValue="-- Select --"></asp:RequiredFieldValidator>
                    
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="text-align: center;" class="MiddleContentDiv">
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
            </div>
            <div style="overflow:scroll">
                <br />
                <asp:GridView ID="gv_SetChkInOut" HorizontalAlign="Center" runat="server" CellPadding="4"
                    ForeColor="#333333" GridLines="Horizontal"  ShowHeaderWhenEmpty="True" 
                    AutoGenerateColumns="False" 
                    onselectedindexchanged="gv_SetChkInOut_SelectedIndexChanged" 
                    onrowdatabound="gv_SetChkInOut_RowDataBound">
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
                        <asp:BoundField HeaderText="User Name" DataField="User Name" />
                        <asp:BoundField HeaderText="Project Name" DataField="Project Name" />
                        <asp:BoundField HeaderText="Task Name" DataField="Task Name" />
                        <asp:BoundField HeaderText="Check In Time" DataField="CheckInTime" />
                        <asp:BoundField HeaderText="Check Out Time" DataField="CheckOutTime" />
                        <asp:BoundField HeaderText="Inddor/Outdoor" DataField="InddorOutdoor" />
                        <%--<asp:BoundField HeaderText="Missing Time Details" DataField=""/>--%>
                        <asp:TemplateField HeaderText="Missing Time Details" ControlStyle-Font-Underline="true" ControlStyle-ForeColor="ActiveCaptionText">
                            <ItemTemplate>
                                <%--<asp:HyperLink ID="hyperlink" runat="server" Text="Details" NavigateUrl="#" />--%>
                                <label style="text-decoration: underline;color: blue;">Details</label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="DayCheckInOutID" DataField="DayCheckInOutID" />
                        <asp:BoundField HeaderText="Date" DataField="Date" />
                        <asp:BoundField HeaderText="UserID" DataField="UserID" />
                        <asp:BoundField HeaderText="ProjID" DataField="ProjID" />
                        <asp:BoundField HeaderText="TaskID" DataField="TaskID" />
                        <asp:BoundField HeaderText="SpentTime" DataField="SpentTime" />
                        <asp:BoundField HeaderText="MissingFlag" DataField="MissingFlag" />
                        <asp:BoundField HeaderText="Status" DataField="Status" />
                        <%--<asp:BoundField HeaderText="Update" DataField="" />--%>
                        <asp:TemplateField HeaderText="Update" ControlStyle-Font-Underline="true" ControlStyle-ForeColor="ActiveCaptionText">
                            <ItemTemplate>
                                <asp:Button ID="gridUpdateButton" runat="server" Text="Update" NavigateUrl="#" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="CheckInOutID" DataField="CheckInOutID" />
                    </Columns>
                </asp:GridView>
            </div>
            <div style="text-align: center;" class="EmpMasterConDiv">
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
