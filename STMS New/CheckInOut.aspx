<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="CheckInOut.aspx.cs" Inherits="CheckInOut" EnableEventValidation="false" %>

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
                    <asp:Label ID="lblHeadings" runat="server" Text="Check In-Out"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv" style="padding-left: 13%;">
                <asp:RadioButton ID="rbtnCurrentWorking" runat="server" Text="Current Working" GroupName="FillType"
                    Checked="True" AutoPostBack="True" OnCheckedChanged="rbtnCurrentWorking_CheckedChanged" />
                <asp:RadioButton ID="rbtnFillPrevious" runat="server" Text="Fill Previous" GroupName="FillType"
                    AutoPostBack="True" OnCheckedChanged="rbtnFillPrevious_CheckedChanged" />
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblProjName" runat="server" Text="Project Name*"></asp:Label><asp:DropDownList
                    ID="cmbProjName" runat="server" CssClass="combobox" OnSelectedIndexChanged="cmbProjName_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem>-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbProjName"
                    Display="Dynamic" ErrorMessage="Please Select Project." ForeColor="Red" InitialValue="-- Select --"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblTaskName" runat="server" Text="Task Name*"></asp:Label><asp:DropDownList
                    ID="cmbTaskName" runat="server" CssClass="combobox" OnSelectedIndexChanged="cmbTaskName_SelectedIndexChanged">
                    <asp:ListItem>-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cmbTaskName"
                    Display="Dynamic" ErrorMessage="Please Select Task." ForeColor="Red" InitialValue="-- Select --"></asp:RequiredFieldValidator>
            </div>
            <div class="MiddleContentDiv" style="padding-left: 13%;">
                <asp:RadioButton ID="rbtnIndoor" runat="server" Text="Indoor" GroupName="CheckInOutType"
                    Checked="True" OnCheckedChanged="rbtnIndoor_CheckedChanged" />
                <asp:RadioButton ID="rbtnOutdoor" runat="server" Text="Outdoor" GroupName="CheckInOutType"
                    OnCheckedChanged="rbtnOutdoor_CheckedChanged" />
            </div>
              <div class="MiddleContentDiv">
                 <asp:Label CssClass="lblWidth" ID="Label1" runat="server" Text="Percentage Of Complete*"></asp:Label>
                 <asp:TextBox ID="txt_NoOfStrok" runat="server"></asp:TextBox>
                 </div>
                 <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblSelectDate" runat="server" 
                    Text="Select Date*"></asp:Label>
                <asp:TextBox ID="dtpDate" runat="server" Enabled="False">
                </asp:TextBox><%--</asp:TextBox><img
                    src="Images/calender.png" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpDate"
                    Display="Dynamic" ErrorMessage="Select Date" ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                    </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" Width="147px" ID="lblInTime" runat="server" 
                    Text="InTime" Enabled="False"></asp:Label>
                <asp:DropDownList ID="cmbInHrs" runat="server" Width="50px" 
                    ValidationGroup="Group1" Enabled="False">
                </asp:DropDownList>
                <asp:Label ID="lblFromHrs" runat="server" Text="Hrs." Enabled="False" 
                    Visible="False"></asp:Label>
                <asp:DropDownList ID="cmbInMin" runat="server" Width="50px" 
                    ValidationGroup="Group1" Enabled="False">
                </asp:DropDownList>
                <asp:Label ID="lblFromMin" runat="server" Text="Min." Enabled="False"></asp:Label>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbInHrs"
                    Display="Dynamic" ErrorMessage="Please Select Intime Hrs." ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbInMin"
                    Display="Dynamic" ErrorMessage="Select Intime Min." ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" Width="147px" ID="lblOutTime" runat="server" 
                    Text="Out Time" Enabled="False"></asp:Label>
                <asp:DropDownList ID="cmbOutHrs" runat="server" Width="50px" 
                    ValidationGroup="Group1" Enabled="False">
                </asp:DropDownList>
                <asp:Label ID="lblToHrs" runat="server" Text="Hrs." Enabled="False"></asp:Label>
                <asp:DropDownList ID="cmbOutMin" runat="server" Width="50px" 
                    ValidationGroup="Group1" Enabled="False">
                </asp:DropDownList>
                <asp:Label ID="lblToMin" runat="server" Text="Min." Enabled="False" 
                    Visible="False"></asp:Label>
                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbOutHrs"
                    Display="Dynamic" ErrorMessage="Please Select outtime Hrs. " ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="cmbOutMin"
                    Display="Dynamic" ErrorMessage="Select OutTime Min." ForeColor="Red" 
                    InitialValue="- -" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ForeColor="Red"
                    ErrorMessage="Outtime Should be greater than Intime." ControlToCompare="cmbInHrs"
                    ControlToValidate="cmbOutHrs" Operator="GreaterThanEqual" Enabled="False"></asp:CompareValidator>
            </div>
        </div>
        <div class="EmpMasterConDiv">
            &nbsp;</div>
        <div style="padding-top: 5px; text-align: center;">
            <asp:Button ID="btnChkIn" runat="server" Text="Check In" OnClick="btnChkIn_Click" />
            <asp:Button ID="btnChangeTask" runat="server" Text="Change Task" Enabled="False"
                OnClick="btnChangeTask_Click" />
            <asp:Button ID="btnChkOut" runat="server" Text="Check Out" 
                OnClick="btnChkOut_Click" CausesValidation="False" />
            <asp:Button ID="btnTakeBreak" runat="server" Text="Take Break" OnClick="btnTakeBreak_Click"
                Visible="False" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Enabled="False" Visible="False" />
        </div>
        <div style="overflow: scroll">
            <br />
            <asp:GridView ID="gv_CheckInOut" HorizontalAlign="Center" runat="server" CellPadding="4"
                ForeColor="#333333" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnRowCreated="gv_CheckInOut_RowCreated"
                OnRowDataBound="gv_CheckInOut_RowDataBound" ShowFooter="True">
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
