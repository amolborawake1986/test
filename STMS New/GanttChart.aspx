<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="GanttChart.aspx.cs" Inherits="CheckInOut" EnableEventValidation="false" %>

<script runat="server">

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Weekly Timesheet Preview"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpName" runat="server" Text="Peoject Name*"></asp:Label><asp:DropDownList
                    ID="cmbEmpName" runat="server" CssClass="combobox" 
                    >
                    <asp:ListItem>-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbEmpName"
                    Display="Dynamic" ErrorMessage="Please Select Project Name." ForeColor="Red" InitialValue="-- Select --"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="Label1" runat="server" Text="SelectSheet*"></asp:Label>
                <asp:DropDownList
                    ID="cmbSelected" runat="server" CssClass="combobox" 
                   >
                   <asp:ListItem>Both</asp:ListItem>
                   <asp:ListItem>Actual</asp:ListItem>
                   <asp:ListItem>Plan</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblSelectDate" runat="server" 
                    Text="Select Date*" Visible="False"></asp:Label>
                <asp:DropDownList ID="cmbMonth" runat="server" Visible="False">
                    <asp:ListItem Selected="True" Value="01">Jan</asp:ListItem>
                    <asp:ListItem Value="02">Feb</asp:ListItem>
                    <asp:ListItem Value="03">Mar</asp:ListItem>
                    <asp:ListItem Value="04">Apr</asp:ListItem>
                    <asp:ListItem Value="05">May</asp:ListItem>
                    <asp:ListItem Value="06">Jun</asp:ListItem>
                    <asp:ListItem Value="07">Jul</asp:ListItem>
                    <asp:ListItem Value="08">Aug</asp:ListItem>
                    <asp:ListItem Value="09">Sept</asp:ListItem>
                    <asp:ListItem Value="10">Oct</asp:ListItem>
                    <asp:ListItem Value="11">Nov</asp:ListItem>
                    <asp:ListItem Value="12">Dec</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cmbYear" runat="server" Visible="False">
                    <asp:ListItem>2013</asp:ListItem>
                    <asp:ListItem>2014</asp:ListItem>
                    <asp:ListItem>2015</asp:ListItem>
                    <asp:ListItem>2016</asp:ListItem>
                    <asp:ListItem>2017</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2010</asp:ListItem>
                </asp:DropDownList>
                &nbsp;</div>
            <div style="padding-top: 5px; text-align: center;">
                <asp:Button ID="btnPreview" runat="server" Text="Preview" 
                    OnClick="btnPreview_Click" /></div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="overflow: scroll">
                <br />
                <asp:GridView ID="gv_TimesheetMonthly" HorizontalAlign="Center" runat="server" CellPadding="4"
                    ForeColor="#333333" ShowHeaderWhenEmpty="True" ShowFooter="True" 
                    onrowcreated="gv_TimesheetMonthly_RowCreated" onrowdatabound="gv_TimesheetMonthly_RowDataBound" 
                   >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <%--<Columns>
                        <asp:BoundField HeaderText="Task Name" DataField="Task Name" />
                        <asp:BoundField HeaderText="01" DataField="01"/>
                        <asp:BoundField HeaderText="02" DataField="02"/>
                        <asp:BoundField HeaderText="03" DataField="03"/>
                        <asp:BoundField HeaderText="04" DataField="04"/>
                        <asp:BoundField HeaderText="05" DataField="05"/>
                        <asp:BoundField HeaderText="06" DataField="06"/>
                        <asp:BoundField HeaderText="07" DataField="07"/>
                        <asp:BoundField HeaderText="08" DataField="08"/>
                        <asp:BoundField HeaderText="09" DataField="09"/>
                        <asp:BoundField HeaderText="10" DataField="10"/>
                        <asp:BoundField HeaderText="11" DataField="11" />
                        <asp:BoundField HeaderText="12" DataField="12"/>
                        <asp:BoundField HeaderText="13" DataField="13"/>
                        <asp:BoundField HeaderText="14" DataField="14"/>
                        <asp:BoundField HeaderText="15" DataField="15"/>
                        <asp:BoundField HeaderText="16" DataField="16"/>
                        <asp:BoundField HeaderText="17" DataField="17"/>
                        <asp:BoundField HeaderText="18" DataField="18"/>
                        <asp:BoundField HeaderText="19" DataField="19"/>
                        <asp:BoundField HeaderText="20" DataField="20"/>
                        <asp:BoundField HeaderText="21" DataField="21"/>
                        <asp:BoundField HeaderText="22" DataField="22"/>
                        <asp:BoundField HeaderText="23" DataField="23"/>
                        <asp:BoundField HeaderText="24" DataField="24"/>
                        <asp:BoundField HeaderText="25" DataField="25"/>
                        <asp:BoundField HeaderText="26" DataField="26"/>
                        <asp:BoundField HeaderText="27" DataField="27"/>
                        <asp:BoundField HeaderText="28" DataField="28"/>
                        <asp:BoundField HeaderText="29" DataField="29"/>
                        <asp:BoundField HeaderText="30" DataField="30"/>
                        <asp:BoundField HeaderText="31" DataField="31"/>
                    </Columns>--%>
                    <EditRowStyle BackColor="#999999" HorizontalAlign="Center"/>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                     <HeaderStyle BackColor="#5D7B9D" HorizontalAlign="Center"/>
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
