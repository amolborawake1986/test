<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowDetailsSetChkInOut.aspx.cs" Inherits="ShowDetailsSetChkInOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:GridView ID="gv_Details" runat="server">
        </asp:GridView>
        <div align="center">
           <input type="button" onclick="JavaScript:window.close();" value="Close"/>
    </div>
    </form>
</body>
</html>
