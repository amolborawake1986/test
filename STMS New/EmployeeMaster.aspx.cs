using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Collections;
public partial class EmployeeMaster : System.Web.UI.Page
{
    static string gender = null;
    static int statusValue = 0;
    static ArrayList idList;
    static ArrayList DeptIdList;
    static ArrayList DeptNameList;
    static ArrayList EmpDeptIdList;
    static ArrayList DesigIdList;
    static ArrayList DesigNameList;
    static ArrayList EmpEmpIdList;
    static ArrayList EmpDesigIdList;
    static ArrayList EmpUnderEmpIdList;
    public void clearAllFields()
    {
        txtEmpCode.Text = String.Empty;
        txtFName.Text = String.Empty;
        txtMName.Text = String.Empty;
        txtLName.Text = String.Empty;
        txtAddress.Text = String.Empty;
        txtPhnNmber.Text = String.Empty;
        txtMailID.Text = String.Empty;
        dtpDOB.Text = String.Empty;
        txtQual.Text = String.Empty;
        cmbBloodGroup.SelectedIndex = 0;
        dtpDOJ.Text = String.Empty;
        dtpDOR.Text = String.Empty;
        txtROR.Text = String.Empty;
        cmbDept.SelectedIndex = 0;
        cmbDesig.SelectedIndex = 0;
        cmbUnderDesig.SelectedIndex = 0;
        cmbUnderEmpName.SelectedIndex = 0;
    }

    public void selectdata()
    {
        string qryStr = "select EmployeeCode,concat(FirstName,' ',MiddleName,' ',SurnameName) As EmployeeName,Address,PhoneNo,MailID,DateOfBirth,Qualification,Gender,BloodGroup,DateOfJoining,DateOfLeave,ReasonOfLeave,Status from tbl_EmployeeMaster";
        gv_EmpMaster.DataSource = Connection.loadData(qryStr);
        gv_EmpMaster.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            dtpDOB.Attributes.Add("readonly", "readonly");
            dtpDOJ.Attributes.Add("readonly", "readonly");
            dtpDOR.Attributes.Add("readonly", "readonly");

            if (!IsPostBack)
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DepartmentMaster", Connection.conn);
                Connection.conn.Open();
                //MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();

                DeptIdList = new ArrayList();
                DeptNameList = new ArrayList();

                //while (dr.Read())
                //{
                //    cmbDept.Items.Add(dr[1].ToString());   //To fill value in Department Combo box
                //    DeptIdList.Add(dr[0]);
                //    DeptNameList.Add(dr[1]);

                //}
                //dr.Close();

                cmbDept.DataSource = cmd.ExecuteReader();
                cmbDept.DataTextField = "Name";
                cmbDept.DataValueField = "DeptID";
                cmbDept.DataBind();
                Connection.conn.Close();


                cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DesignationMaster order by LevelID ASC", Connection.conn);
                // dr = cmd.ExecuteReader();
                idList = new ArrayList();
                DesigIdList = new ArrayList();
                DesigNameList = new ArrayList();
                //while (dr.Read())
                //{
                //    cmbDesig.Items.Add(dr[1].ToString());  //To fill value in Designation Combo box
                //    cmbUnderDesig.Items.Add(dr[1].ToString());
                //    idList.Add(dr[0]);
                //    DesigIdList.Add(dr[0]);
                //    DesigNameList.Add(dr[1]);

                //}
                //dr.Close();
                Connection.conn.Open();
                cmbDesig.DataSource = cmd.ExecuteReader();
                cmbDesig.DataTextField = "Name";
                cmbDesig.DataValueField = "DesigID";
                cmbDesig.DataBind();
                Connection.conn.Close();

                cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DesignationMaster order by LevelID ASC", Connection.conn);
                Connection.conn.Open();
                cmbUnderDesig.DataSource = cmd.ExecuteReader();
                cmbUnderDesig.DataTextField = "Name";
                cmbUnderDesig.DataValueField = "DesigID";
                cmbUnderDesig.DataBind();
                Connection.conn.Close();

                cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_EmployeeMaster", Connection.conn);
                //dr = cmd.ExecuteReader();
                EmpDeptIdList = new ArrayList();
                EmpDesigIdList = new ArrayList();
                EmpUnderEmpIdList = new ArrayList();
                EmpEmpIdList = new ArrayList();
                //while (dr.Read())
                //{
                //    EmpEmpIdList.Add(dr[0]);
                //    EmpDeptIdList.Add(dr[1]);
                //    EmpDesigIdList.Add(dr[3]);
                //    EmpUnderEmpIdList.Add(dr[2]);

                //    string EmpFullName = dr[4].ToString() + " " + dr[5].ToString() + " " + dr[6].ToString();
                //    cmbUnderEmpName.Items.Add(EmpFullName);

                //}
                //dr.Close();
                Connection.conn.Open();
                cmbUnderEmpName.DataSource = cmd.ExecuteReader();
                cmbUnderEmpName.DataTextField = "FirstName";
                cmbUnderEmpName.DataValueField = "EmpID";
                cmbUnderEmpName.DataBind();
                Connection.conn.Close();
            }
            Connection.conn.Close();

            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearAllFields();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnMale.Checked == true)
                gender = "M";
            else if (rbtnFemale.Checked == true)
                gender = "F";

            if (chkStatus.Checked == true)
                statusValue = 1;
            if (btnAdd.Text == "Add")
            {
                string qry = "insert into tbl_EmployeeMaster(DeptID,UnderEmpID,DesigID,FirstName,MiddleName,SurnameName,MailID,DateOfBirth,Address,Qualification,Gender,PhoneNo,BloodGroup,DateOfJoining,DateOfLeave,ReasonOfLeave,Status,EmployeeCode,LanguageSelectionId) values(" + Convert.ToInt16(cmbDept.SelectedValue) + " , " + Convert.ToInt16(cmbUnderEmpName.SelectedValue) + " , " + Convert.ToInt16(cmbDesig.SelectedValue) + "  ,'" + txtFName.Text.ToString() + "','" + txtMName.Text.ToString() + "','" + txtLName.Text.ToString() + "','" + txtMailID.Text.ToString() + "','" + dtpDOB.Text.ToString() + "','" + txtAddress.Text.ToString() + "','" + txtQual.Text.ToString() + "','" + gender + "','" + txtPhnNmber.Text.ToString() + "','" + cmbBloodGroup.Text.ToString() + "','" + dtpDOJ.Text.ToString() + "','" + dtpDOR.Text.ToString() + "','" + txtROR.Text.ToString() + "'," + statusValue + ", '" + txtEmpCode.Text.ToString() + "',0)";
                Connection.updateData(qry);
            }
            else if (btnAdd.Text == "Modify")
            {
                string update_qry = "Update tbl_EmployeeMaster set DeptID=" + Convert.ToInt16(cmbDept.SelectedValue) + ",UnderEmpID=" + Convert.ToInt16(cmbUnderEmpName.SelectedValue) + ",DesigID =  " + Convert.ToInt16(cmbDesig.SelectedValue) + ",FirstName ='" + txtFName.Text.ToString() + "',MiddleName='" + txtMName.Text.ToString() + "',SurnameName ='" + txtLName.Text.ToString() + "',MailID ='" + txtMailID.Text.ToString() + "',DateOfBirth ='" + dtpDOB.Text.ToString() + "',Address ='" + txtAddress.Text.ToString() + "', Qualification ='" + txtQual.Text.ToString() + "' ,Gender='" + gender + "',PhoneNo='" + txtPhnNmber.Text.ToString() + "',BloodGroup='" + cmbBloodGroup.Text.ToString() + "',DateOfJoining ='" + dtpDOJ.Text.ToString() + "',DateOfLeave ='" + dtpDOR.Text.ToString() + "',ReasonOfLeave='" + txtROR.Text.ToString() + "',Status=" + statusValue + ",EmployeeCode='" + txtEmpCode.Text.ToString() + "' where EmpID=" + EmpEmpIdList[gv_EmpMaster.SelectedIndex];
                Connection.updateData(update_qry);
                btnAdd.Text = "Add";
            }
            clearAllFields();
            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_EmpMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Connection.HighLightSelectedRow(gv_EmpMaster);
            txtEmpCode.Text = gv_EmpMaster.SelectedRow.Cells[0].Text.ToString();
            string empFullName = gv_EmpMaster.SelectedRow.Cells[1].Text.ToString();

            string[] splitToTime = empFullName.Split(' ');
            int i = 0;
            foreach (string word in splitToTime)
            {
                if (i == 0)
                {
                    txtFName.Text = word;
                }
                if (i == 1)
                {
                    txtMName.Text = word;
                }
                if (i == 2)
                {
                    txtLName.Text = word;
                }
                i++;
            }
            txtAddress.Text = gv_EmpMaster.SelectedRow.Cells[2].Text.ToString();
            txtPhnNmber.Text = gv_EmpMaster.SelectedRow.Cells[3].Text.ToString();
            txtMailID.Text = gv_EmpMaster.SelectedRow.Cells[4].Text.ToString();
            dtpDOB.Text = gv_EmpMaster.SelectedRow.Cells[5].Text.ToString();
            txtQual.Text = gv_EmpMaster.SelectedRow.Cells[6].Text.ToString();
            if (gv_EmpMaster.SelectedRow.Cells[7].Text.ToString() == "M")
            {
                rbtnMale.Checked = true;
                rbtnFemale.Checked = false;
            }
            else if (gv_EmpMaster.SelectedRow.Cells[7].Text.ToString() == "F")
            {
                rbtnFemale.Checked = true;
                rbtnMale.Checked = false;
            }

            cmbBloodGroup.Text = gv_EmpMaster.SelectedRow.Cells[8].Text.ToString();
            dtpDOJ.Text = gv_EmpMaster.SelectedRow.Cells[9].Text.ToString();
            if (gv_EmpMaster.SelectedRow.Cells[10].Text.ToString() != "&nbsp;")
            {
                dtpDOR.Text = gv_EmpMaster.SelectedRow.Cells[10].Text.ToString();
            }
            if (gv_EmpMaster.SelectedRow.Cells[11].Text.ToString() != "&nbsp;")
            {
                txtROR.Text = gv_EmpMaster.SelectedRow.Cells[11].Text.ToString();
            }
            if (gv_EmpMaster.SelectedRow.Cells[12].Text.ToString() == "Active")
            {
                chkStatus.Checked = true;
            }
            else if (gv_EmpMaster.SelectedRow.Cells[12].Text.ToString() == "Inactive")
            {
                chkStatus.Checked = false;
            }


            //cmbDept.SelectedValue = gv_EmpMaster.SelectedRow.Cells[1].Text.ToString();
            //cmbDesig.SelectedValue = gv_EmpMaster.SelectedRow.Cells[3].ToString(); 

            //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DesignationMaster where DesigID = " + desigId + "", Connection.conn);
            //Connection.conn.Open();
            //MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    cmbDesig.Text = dr["Name"].ToString();
            //}
            //dr.Close();


            //int UnderId = Convert.ToInt16(gv_EmpMaster.SelectedRow.Cells[2].ToString());
            //int selIndexId = EmpEmpIdList.IndexOf(UnderId);
            //int UnderDesigId = (int)EmpDesigIdList[selIndexId];

            //cmd = new MySql.Data.MySqlClient.MySqlCommand("select Name from tbl_DesignationMaster where DesigID = " + UnderDesigId + "", Connection.conn);
            //dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    cmbUnderDesig.Text = dr["Name"].ToString();
            //}
            //dr.Close();
            //cmbUnderDesig.SelectedValue = gv_EmpMaster.SelectedRow.Cells[3].ToString();


            //cmd = new MySql.Data.MySqlClient.MySqlCommand("select concat(FirstName,' ',MiddleName,' ',SurnameName) As EmployeeName from tbl_EmployeeMaster where UnderEmpID = " + UnderId + "", Connection.conn);
            //dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    cmbUnderEmpName.Text = dr["EmployeeName"].ToString();
            //}
            //dr.Close();
           // cmbUnderEmpName.SelectedValue= cmbUnderEmpName.Items.FindByValue(gv_EmpMaster.SelectedRow.Cells[2].ToString()).ToString();
            //Connection.conn.Close();
            // int underDesigId =(int)EmpDesigIdList[selIndexId];
            /* string underEmpDesig = DesigNameList[DesigIdList.IndexOf(underDesigId)].ToString();
             cmbUnderDesig.Text = underEmpDesig;
            */

            //cmbUnderDesig.Text = UnderDesigNameList[gv_EmpMaster.SelectedIndex].ToString();
            //cmbUnderDesig.Text = gv_EmpMaster.SelectedRow.Cells[18].Text.ToString();
            btnAdd.Text = "Modify";
        }
        catch
        {
            Connection.conn.Close();
        }
    }

    protected void cmbDesig_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            cmbUnderDesig.Items.Clear();
            //  string qry = "select LevelID from tbl_DesignationMaster where LevelID = " + cmbDesig.SelectedIndex;

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DesignationMaster where LevelID< " + Convert.ToInt16(cmbDesig.SelectedValue), Connection.conn);

            Connection.conn.Open();
            //MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();

            //while (dr.Read())
            //{
            //    cmbUnderDesig.Items.Add(dr[1].ToString());  //To fill value in Under Designation Combo box
            //}
            //dr.Close();
            cmbUnderDesig.Items.Clear();
            cmbUnderDesig.DataSource = cmd.ExecuteReader();
            cmbUnderDesig.DataTextField = "Name";
            cmbUnderDesig.DataValueField = "DesigID";
            cmbUnderDesig.DataBind();
            Connection.conn.Close();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void cmbUnderDesig_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //cmbUnderEmpName.Items.Clear();

            //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DesignationMaster where DesigID =" + idList[cmbUnderDesig.SelectedIndex], Connection.conn);
            //Connection.conn.Open();
            //MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            //ArrayList selectedID = new ArrayList();

            //while (dr.Read())
            //{
            //    selectedID.Add(dr[0]);

            //}
            //dr.Close();
            //for (int i = 0; i < selectedID.Count; i++)
            //{
            //    cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_EmployeeMaster where DesigID = " + selectedID[i], Connection.conn);


            //    dr = cmd.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        string EmpFullName = dr[4].ToString() + " " + dr[5].ToString() + " " + dr[6].ToString();
            //        cmbUnderEmpName.Items.Add(EmpFullName);

            //    }
            //}
            //dr.Close();
            cmbUnderEmpName.Items.Clear();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_EmployeeMaster where DesigID = " + Convert.ToInt16(cmbUnderDesig.SelectedValue), Connection.conn);
            Connection.conn.Open();
            cmbUnderEmpName.DataSource = cmd.ExecuteReader();
            cmbUnderEmpName.DataTextField = "FirstName";
            cmbUnderEmpName.DataValueField = "EmpID";
            cmbUnderEmpName.DataBind();
            Connection.conn.Close();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_EmpMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[12].Text.ToString() == "0")
                e.Row.Cells[12].Text = "Inactive";
            else if (e.Row.Cells[12].Text.ToString() == "1")
                e.Row.Cells[12].Text = "Active";

            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_EmpMaster, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }

    }
}