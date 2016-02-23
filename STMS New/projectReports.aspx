<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="projectReports.aspx.cs" Inherits="projectReports" %>

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


        function previewImg(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#MainContent_ContentPlaceHolder1_picEmp').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

       
    </script>
    <div style="float: right; width: 82%; background-color: aliceblue; min-height: 420px;
        height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%;">
            <div align="center" style="padding-top: 2%; height: 48px;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Project Report"></asp:Label></b>
            </div>
            <div class="MiddleContentDiv" style="padding-left: 3%;">
                <asp:RadioButton ID="rbtnAll" runat="server" Text="All Team" GroupName="TrackType"
                    AutoPostBack="True" Checked="True" />
                <asp:RadioButton ID="rbtnTeamWise" runat="server" Text="Team Wise" GroupName="TrackType"
                    AutoPostBack="True" />
                <asp:RadioButton ID="rbtnSingleEmp" runat="server" Text="Employee Wise" GroupName="TrackType"
                    AutoPostBack="True" />
            </div>
            <div class="MiddleContentDiv" style="padding-left: 3%;">
                <asp:RadioButton ID="RadioButton1" runat="server" Text="Daily Reports" GroupName="RType"
                    AutoPostBack="True" Checked="True" />
                <asp:RadioButton ID="RadioButton2" runat="server" Text="Weekly Reports" GroupName="RType"
                    AutoPostBack="True" />
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblTeam" runat="server" Text="Select Team*"></asp:Label>
                <asp:DropDownList ID="cmbName" runat="server" CssClass="combobox" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div style="padding-top: 5px; display: inline-flex; width: 100%; padding-left: 25%;">
                <div class="listbox">
                    <div>
                        <asp:Label ID="lblDeptName" runat="server" Text="Project Name*"></asp:Label></div>
                    <div style="overflow: auto">
                        <asp:ListBox ID="lstDeptName" runat="server" Style="height: 170px; width: 100%;"
                            AutoPostBack="True"></asp:ListBox>
                    </div>
                </div>
                <div class="listbox">
                    <div>
                        <asp:Label ID="lblTeamName" runat="server" Text="Task Name*"></asp:Label></div>
                    <div style="overflow: auto">
                        <asp:ListBox ID="lstTeamName" runat="server" Style="height: 170px; width: 100%;"
                            SelectionMode="Multiple" AutoPostBack="True"></asp:ListBox>
                    </div>
                </div>
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblStartDate" runat="server" Text="From Date*"
                    Height="17px"></asp:Label>
                <asp:TextBox ID="dtpStartDate" runat="server"></asp:TextBox><img src="Images/calender.png" />
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblEndDate" runat="server" Text="To Date"></asp:Label>
                <asp:TextBox ID="dtpEndDate" runat="server"></asp:TextBox><img src="Images/calender.png" />
            </div>
            <div class="MiddleContentDiv">
                <asp:Label CssClass="lblWidth" ID="lblOutputPath" runat="server" Text="Set output path folder*"></asp:Label>
                <asp:TextBox ID="txtOutputPath" runat="server" CssClass="combobox"></asp:TextBox>
                <asp:Button ID="btnBrowse" runat="server" Text="Browse" />
            </div>
        </div>
        <div class="EmpMasterConDiv">
            &nbsp;</div>
        <div style="padding-top: 25px; text-align: center;">
            <asp:Button ID="btnReport" runat="server" Text="Report" />
            <asp:Button ID="Button1" runat="server" Text="Status Report" />
            <asp:Button ID="Button2" runat="server" Text="Line Graph" />
            <asp:Button ID="Button3" runat="server" Text="Reason Report" />
            <asp:Button ID="Button4" runat="server" Text="Group Report" />
            <asp:Button ID="btnClose" runat="server" Text="Close" />
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
