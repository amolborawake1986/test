<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowDetails.aspx.cs" Inherits="ShowDetails" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:GridView ID="gv_Details" HorizontalAlign="Center" runat="server" CellPadding="4"
            ForeColor="#333333" GridLines="Horizontal" ShowHeaderWhenEmpty="True" 
            AutoGenerateColumns="False" onrowdatabound="gv_Details_RowDataBound" 
            onselectedindexchanged="gv_Details_SelectedIndexChanged">
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
                <asp:BoundField HeaderText="Ckeck Intime" DataField="CheckInTime" />
                <asp:BoundField HeaderText="Check Outtime" DataField="CheckOutTime" />
                <asp:BoundField HeaderText="Spent Time" DataField="SpentTime" />
                <asp:BoundField HeaderText="Indoor/Outdoor" DataField="InddorOutdoor" />
                <%--<asp:BoundField HeaderText="Details" DataField="" />--%>
                <asp:TemplateField HeaderText="Show Signature" ControlStyle-ForeColor="ActiveCaptionText">
                    <ItemTemplate>
                        <%--<asp:HyperLink ID="hyperlink" runat="server" Text="Details" NavigateUrl="#" />--%>
                        <label style="text-decoration: underline; color: blue;">
                            Show Signature</label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="CheckInOutID" DataField="CheckInOutID" />
                <asp:BoundField HeaderText="Added Later" DataField="AddedLater" />
                <%--<asp:BoundField HeaderText="Approve/Not Approve" DataField="" />--%>
            </Columns>
        </asp:GridView>
        <div align="center">
            <input type="button" onclick="JavaScript:window.close();" value="Close" />
        </div>
    </form>
</body>
</html>
