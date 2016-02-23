<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeMaster.aspx.cs" Inherits="EmployeeMaster"  EnableEventValidation="false" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=dtpDOB.ClientID %>,#<%=dtpDOJ.ClientID %>,#<%=dtpDOR.ClientID %>").dynDateTime({
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
    <div style="float: left; width: 82%; background-color: aliceblue; min-height: 420px;
        display: inline-flex; height: auto; padding-bottom: 5%;">
        <div style="padding-left: 5%; width: 75%; display: inline-block;">
            <div align="center" style="padding-top: 2%; height: 48px; width: 100%;">
                <b>
                    <asp:Label ID="lblHeadings" runat="server" Text="Employee Master"></asp:Label></b>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpID" runat="server" Text="Employee Code*">
                </asp:Label><asp:TextBox ID="txtEmpCode" runat="server" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpCode"
                    Display="Dynamic" ErrorMessage="Please Enter valid Employee Code" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                <div>
                    <asp:Label CssClass="lblWidth" ID="lblEmpName" runat="server" Text="Name*"></asp:Label>
                    <asp:TextBox ID="txtFName" runat="server" placeholder="First Name" Width="18%" MaxLength="100"></asp:TextBox>
                    <asp:TextBox ID="txtMName" runat="server" placeholder="Middle Name" Width="18%" MaxLength="100"></asp:TextBox>
                    <asp:TextBox ID="txtLName" runat="server" placeholder="Last Name" Width="18%" MaxLength="100"></asp:TextBox>
                </div>
                <div style="padding-left: 26%;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFName"
                        Display="Dynamic" ErrorMessage="Enter First Name. " ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtMName"
                        Display="Dynamic" ErrorMessage="Enter Middle Name. " ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtLName"
                        Display="Dynamic" ErrorMessage="Enter Last Name. " ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpAdd" runat="server" Text="Address*"></asp:Label>
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Style="width: 51%;
                    height: 75px;" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress"
                    Display="Dynamic" ErrorMessage="Please Enter Address" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpPhnNo" runat="server" Text="Phone No.*"></asp:Label>
                <asp:TextBox ID="txtPhnNmber" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhnNmber"
                    Display="Dynamic" ErrorMessage="Please Enter Phone No." ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhnNmber"
                    ErrorMessage="Enter 10 digit number." ForeColor="Red" ValidationExpression="^[0-9]{10}"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="Label2" runat="server" Text="Mail ID*"></asp:Label>
                <asp:TextBox ID="txtMailID" runat="server" Style="width: 33%;" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMailID"
                    Display="Dynamic" ErrorMessage="Please Enter Mail ID" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMailID"
                    ErrorMessage="Enter valid mail ID." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpDOB" runat="server" Text="Date Of Birth*"></asp:Label>
                <asp:TextBox ID="dtpDOB" runat="server"></asp:TextBox><img src="Images/calender.png" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpDOB"
                    Display="Dynamic" ErrorMessage="Select Date of Birth" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="Label3" runat="server" Text="Qualification*"></asp:Label>
                <asp:TextBox ID="txtQual" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQual"
                    Display="Dynamic" ErrorMessage="Please Enter Qualification" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpGender" runat="server" Text="Gender*"></asp:Label>
                <asp:RadioButton ID="rbtnMale" runat="server" Text="Male" GroupName="Gender" Checked="true" /><asp:RadioButton
                    ID="rbtnFemale" runat="server" Text="Female" GroupName="Gender" />
            </div>
            
            
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblBloodgroup" runat="server" Text="Blood Group*"></asp:Label>
                <asp:DropDownList CssClass="combobox" ID="cmbBloodGroup" runat="server">
                    <asp:ListItem>Please Select</asp:ListItem>
                    <asp:ListItem>A+</asp:ListItem>
                    <asp:ListItem>A-</asp:ListItem>
                    <asp:ListItem>B+</asp:ListItem>
                    <asp:ListItem>B-</asp:ListItem>
                    <asp:ListItem>AB+</asp:ListItem>
                    <asp:ListItem>AB-</asp:ListItem>
                    <asp:ListItem>O+</asp:ListItem>
                    <asp:ListItem>O-</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cmbBloodGroup"
                    Display="Dynamic" ErrorMessage="Please Select Blood Group" ForeColor="Red" InitialValue="Please Select"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpDOJ" runat="server" Text="Date of Joining*"></asp:Label>
                <asp:TextBox ID="dtpDOJ" runat="server"></asp:TextBox><img src="Images/calender.png" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="dtpDOJ"
                    Display="Dynamic" ErrorMessage="Select Date of Joining" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpDOB"
                    ControlToValidate="dtpDOJ" EnableClientScript="true" Display="Dynamic" ErrorMessage="Date must be greater than Birthdate"
                    ForeColor="Red" Operator="GreaterThan" Type="Date"></asp:CompareValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpDOR" runat="server" Text="Date of Relieve"></asp:Label>
                <asp:TextBox ID="dtpDOR" runat="server"></asp:TextBox><img src="Images/calender.png" />
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpDOJ"
                    ControlToValidate="dtpDOR" Display="Dynamic" ErrorMessage="Date must be greater than Joining date"
                    ForeColor="Red" Operator="GreaterThan" Type="Date"></asp:CompareValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpROR" runat="server" Text="Reason of Relieve"></asp:Label>
                <asp:TextBox ID="txtROR" runat="server" TextMode="MultiLine" Style="width: 51%; height: 75px;"
                    MaxLength="200"></asp:TextBox>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblStatus" runat="server" Text="Status*"></asp:Label>
                <asp:CheckBox ID="chkStatus" runat="server" Text="Active" />
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="Label1" runat="server" Text="Department*"></asp:Label>
                <asp:DropDownList CssClass="combobox" ID="cmbDept" runat="server">
                                    <asp:ListItem>Please Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="cmbDept"
                    Display="Dynamic" ErrorMessage="Please Select Department" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblEmpDesig" runat="server" Text="Designation*"></asp:Label>
                <asp:DropDownList CssClass="combobox" ID="cmbDesig" runat="server" 
                    AutoPostBack="True" onselectedindexchanged="cmbDesig_SelectedIndexChanged">
                    <asp:ListItem>Please Select</asp:ListItem>
                 </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cmbDesig"
                    Display="Dynamic" ErrorMessage="Please Select Designation" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblUnderDesig" runat="server" Text="Under Designation*"></asp:Label>
                <asp:DropDownList CssClass="combobox" ID="cmbUnderDesig" runat="server" 
                    AutoPostBack="True" onselectedindexchanged="cmbUnderDesig_SelectedIndexChanged">
                     <asp:ListItem>Please Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="cmbUnderDesig"
                    Display="Dynamic" ErrorMessage="Please select Under Designation" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                <asp:Label CssClass="lblWidth" ID="lblUnderEmpName" runat="server" Text="Under Employee Name*"></asp:Label>
                <asp:DropDownList CssClass="combobox" ID="cmbUnderEmpName" runat="server" 
                    AutoPostBack="True">
                    <asp:ListItem>Please Select</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="cmbUnderEmpName"
                    Display="Dynamic" ErrorMessage="Please select Under Employee Name" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="EmpMasterConDiv">
                &nbsp;</div>
            <div style="text-align: center;" class="EmpMasterConDiv">
                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" ValidationGroup="UnvalidatedControl" />
                <asp:Button ID="btnClose" runat="server" Text="Close" />
            </div>
            <div style="text-align: center; overflow:scroll; width: 119%;" class="EmpMasterConDiv">
                
                <div style="height: 200px;">
                <asp:GridView ID="gv_EmpMaster" runat="server" CellPadding="4" ForeColor="#333333" 
                        HorizontalAlign="Center"
                        onselectedindexchanged="gv_EmpMaster_SelectedIndexChanged" 
                        onrowdatabound="gv_EmpMaster_RowDataBound" GridLines="Horizontal"  ShowHeaderWhenEmpty="True">
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
            <div style="text-align: center;" class="EmpMasterConDiv">
            </div>
        </div>
        <div style="float: right; height: 0%; padding-top: 8%; width: 19%; text-align: left;
            display: inline-block;">
            <asp:Image ID="picEmp" runat="server" ImageUrl="~/Images/Avatar.jpg" Style="width: 120px;
                height: 120px; border: solid; border-color: darkgray; border-width: 1px;" />
            <div style="padding-left: 12%;">
                <input onchange="javascript: previewImg(this);" id="fileUpProfPic" type="file" style="width: 86px;" />
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
