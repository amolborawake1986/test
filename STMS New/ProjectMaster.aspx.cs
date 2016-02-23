using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class ProjectMaster : System.Web.UI.Page
{
    static int ProjID;
    static ArrayList idList;
    static ArrayList nameList;
    public void selectdata()
    {
        string qryStr = "select Name as 'Project Name',StartDate as 'Start Date',EndDate as 'End Date',Status from tbl_ProjectMaster";
        gv_ProjectMaster.DataSource = Connection.loadData(qryStr);
        gv_ProjectMaster.DataBind();

    }
    void clearField()
    {
        txtProjName.Text = String.Empty;
        dtpStartDate.Text = String.Empty;
        dtpEndDate.Text = String.Empty;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            dtpStartDate.Attributes.Add("readonly", "readonly");
            dtpEndDate.Attributes.Add("readonly", "readonly");
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_ProjectMaster", Connection.conn);
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
        clearField();
        btnAdd.Text = "Add";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int statusValue = 0;
            if (chkStatus.Checked == true)
            {
                statusValue = 1;
            }
            if (btnAdd.Text == "Add")
            {
                string qry = "insert into tbl_ProjectMaster(Name,StartDate,EndDate,Status) values('" + txtProjName.Text.ToString() + "','" + dtpStartDate.Text.ToString() + "','" + dtpEndDate.Text.ToString() + "'," + statusValue + ")";
                Connection.updateData(qry);

            }
            else if (btnAdd.Text == "Modify")
            {
                string update_qry = "Update tbl_ProjectMaster set Name='" + txtProjName.Text.ToString() + "',StartDate='" + dtpStartDate.Text.ToString() + "',EndDate='" + dtpEndDate.Text.ToString() + "',Status=" + statusValue + " where ProjID=" + idList[gv_ProjectMaster.SelectedIndex]; ;
                Connection.updateData(update_qry);
                btnAdd.Text = "Add";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Details saved successfully');</script>");
            clearField();
            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_ProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Connection.HighLightSelectedRow(gv_ProjectMaster);
            ProjID = gv_ProjectMaster.SelectedIndex;
            txtProjName.Text = gv_ProjectMaster.SelectedRow.Cells[0].Text.ToString();
            dtpStartDate.Text = gv_ProjectMaster.SelectedRow.Cells[1].Text.ToString();
            dtpEndDate.Text = gv_ProjectMaster.SelectedRow.Cells[2].Text.ToString();
            string statusValue = gv_ProjectMaster.SelectedRow.Cells[3].Text.ToString();
            if (statusValue == "Inactive")
                chkStatus.Checked = false;
            else if (statusValue == "Active")
                chkStatus.Checked = true;

            btnAdd.Text = "Modify";
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_ProjectMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text.ToString() == "0")
                e.Row.Cells[3].Text = "Inactive";
            else if (e.Row.Cells[3].Text.ToString() == "1")
                e.Row.Cells[3].Text = "Active";

            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_ProjectMaster, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }
}