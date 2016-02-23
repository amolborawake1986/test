<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="FormTimesheetApprove.aspx.cs" Inherits="FormTimesheetApprove" EnableEventValidation="false" %>

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
                    <asp:Label ID="lblHeadings" runat="server" Text="Form Timesheet Approve"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblSelectDate" runat="server" Text="Select Date*"></asp:Label>
                <asp:TextBox ID="dtpDate" runat="server"></asp:TextBox><img src="Images/calender.png" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpDate"
                    Display="Dynamic" ErrorMessage="Select Date" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv" style="display: flex;">
                <asp:Label CssClass="lblWidth" ID="lblSelectEmp" runat="server" Text="Select Employee*"></asp:Label><asp:ListBox
                    ID="lstEmpName" runat="server" Style="width: 152px; margin-left: 1%; height: 140px;">
                </asp:ListBox>
            </div>
        </div>
        <div class="MiddleContentDiv" style="padding-left: 4%;">
            <asp:RadioButton ID="rbtnNotApprove" runat="server" GroupName="ApproveType" Text="Not Approved"
                Checked="True" />
            <asp:RadioButton ID="rbtnApprove" runat="server" GroupName="ApproveType" Text="Approved" />
            <asp:RadioButton ID="rbtnBoth" runat="server" GroupName="ApproveType" Text="Both (Appoved/Not Approved)" />
        </div>
        <div style="padding-top: 25px; text-align: center;">
            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
        </div>
        <div style="overflow: scroll">
            <br />
            <asp:GridView ID="gv_FormTimesheetApprove" HorizontalAlign="Center" runat="server"
                CellPadding="4" ForeColor="#333333" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                AutoGenerateColumns="False" OnRowDataBound="gv_FormTimesheetApprove_RowDataBound"
                OnSelectedIndexChanged="gv_FormTimesheetApprove_SelectedIndexChanged">
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
                    <asp:BoundField HeaderText="TSID" DataField="TSID" />
                    <asp:BoundField HeaderText="User Name" DataField="Name" />
                    <asp:BoundField HeaderText="Intime" DataField="Intime" />
                    <asp:BoundField HeaderText="Outtime" DataField="Outtime" />
                    <%--<asp:BoundField HeaderText="Details" DataField="" />--%>
                    <asp:TemplateField HeaderText="Details" ControlStyle-ForeColor="ActiveCaptionText">
                        <ItemTemplate>
                            <%--<asp:HyperLink ID="hyperlink" runat="server" Text="Details" NavigateUrl="#" />--%>
                            <label style="text-decoration: underline; color: blue;">
                                Details</label>
                        </ItemTemplate>
                        <ControlStyle ForeColor="ActiveCaptionText"></ControlStyle>
                    </asp:TemplateField>
                    <%--<asp:BoundField HeaderText="Approve/Not Approve" DataField="" />--%>
                    <asp:TemplateField HeaderText="Approve/Not Approve">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("ApproveStatus")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="" DataField="DayCheckInOutID"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="padding-top: 25px; text-align: center;">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
            <asp:Button ID="btnClose" runat="server" Text="Close" ValidationGroup="notValidate" />
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
