<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="~/ShowPastUserOnRoute.aspx.cs" Inherits="ShowPastUserOnRoute" EnableEventValidation="false" %>

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
    <asp:Literal ID="js" runat="server"></asp:Literal>
    <body onload="initialize()">
        <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
            height: auto; padding-bottom: 5%;">
            <div style="padding-left: 5%;">
                <div align="center" style="padding-top: 2%; height: 48px;">
                    <b>
                        <asp:Label ID="lblHeadings" runat="server" Text="Previous Route of User"></asp:Label></b>
                </div>
                <div class="MiddleContentDiv" style="padding-left: 19%;">
                    <asp:RadioButton ID="rbtnDateWise" runat="server" Text="Date Wise" GroupName="TrackType"
                        AutoPostBack="True" Checked="True" OnCheckedChanged="rbtnDateWise_CheckedChanged" />
                    <asp:RadioButton ID="rbtnEmpWise" runat="server" Text="Employee Wise" GroupName="TrackType"
                        AutoPostBack="True" OnCheckedChanged="rbtnEmpWise_CheckedChanged" />
                </div>
                <div class="MiddleContentDiv">
                    <asp:Label CssClass="lblWidth" ID="lbl" runat="server" Text="Select Date*"></asp:Label>
                    <asp:DropDownList ID="cmbBox" runat="server" CssClass="combobox" Visible="False">
                    </asp:DropDownList>
                    <asp:TextBox ID="dtpDate" runat="server"></asp:TextBox>
                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/calender.png" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpDate"
                    Display="Dynamic" ErrorMessage="Select Date." ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="EmpMasterConDiv">
                    &nbsp;</div>
                <div style="padding-top: 5px; text-align: center;">
                    <asp:Button ID="btnShowRecord" runat="server" Text="Show Record" OnClick="btnShowRecord_Click" />
                </div>
                <div style="padding-top: 5px; text-align: center; overflow: scroll;">
                    <asp:GridView ID="gv_Details" HorizontalAlign="Center" runat="server" CellPadding="4"
                        ForeColor="#333333" GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                        OnRowDataBound="gv_Details_RowDataBound" OnSelectedIndexChanged="gv_Details_SelectedIndexChanged" EmptyDataText="No data Found">
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
                            <asp:BoundField HeaderText="Name" DataField="Name" />
                            <asp:BoundField HeaderText="Project Name" DataField="Project Name" />
                            <asp:BoundField HeaderText="Task Name" DataField="Task Name" />
                            <asp:BoundField HeaderText="Ckeck Intime" DataField="CheckInTime" />
                            <asp:BoundField HeaderText="Check Outtime" DataField="CheckOutTime" />                            
                            <asp:TemplateField HeaderText="View On Map" ControlStyle-ForeColor="ActiveCaptionText">
                                <ItemTemplate>
                                    <label style="text-decoration: underline; color: blue;">
                                        Show On Map</label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="CheckInOutID" DataField="CheckInOutID"  />
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="padding-top: 5px; text-align: center; width: 97%; height: 500px">
                    <div id="map_canvas" style="width: 100%; height: 100%;">
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </body>
</asp:Content>
