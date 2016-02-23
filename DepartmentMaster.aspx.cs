using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;


public partial class DepartmentMaster : System.Web.UI.Page
{
    static int DeptID;
    static ArrayList idList;
    static ArrayList nameList;

    public void selectdata()
    {
        string qryStr = "select Name from tbl_DepartmentMaster";

        gv_DeptMaster.DataSource = Connection.loadData(qryStr);
        gv_DeptMaster.DataBind();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DepartmentMaster", Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            idList = new ArrayList();
            nameList = new ArrayList();

            while (dr.Read())
            {
                idList.Add(dr[0]);
                nameList.Add(dr[1]);
            }

            dr.Close();
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
        txtDeptName.Text = string.Empty;
        btnAdd.Text = "Add";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "Add")
            {
                string qry = "insert into tbl_DepartmentMaster(Name) values('" + txtDeptName.Text.ToString() + "')";
                Connection.updateData(qry);
            }
            else if (btnAdd.Text == "Modify")
            {
                string update_qry = "Update tbl_DepartmentMaster set Name='" + txtDeptName.Text.ToString() + "' where DeptID=" + idList[gv_DeptMaster.SelectedIndex];
                Connection.updateData(update_qry);
                txtDeptName.Text = string.Empty;
                btnAdd.Text = "Add";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Details saved successfully');</script>");
            txtDeptName.Text = String.Empty;
            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }


    protected void gv_DeptMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Connection.HighLightSelectedRow(gv_DeptMaster);
            DeptID = gv_DeptMaster.SelectedIndex;
            txtDeptName.Text = gv_DeptMaster.SelectedRow.Cells[0].Text.ToString();
            btnAdd.Text = "Modify";
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_DeptMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_DeptMaster, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }
}