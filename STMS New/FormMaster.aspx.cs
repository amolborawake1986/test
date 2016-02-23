using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class FormMaster : System.Web.UI.Page
{
    static int formID;
    static ArrayList idList;
    static ArrayList nameList;
    public void selectdata()
    {
        string qryStr = "select Name from tbl_FormMaster";
        gv_FormMaster.DataSource = Connection.loadData(qryStr);
        gv_FormMaster.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_FormMaster", Connection.conn);
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "Add")
            {
                string qry = "insert into tbl_FormMaster(Name) values('" + txtFormName.Text.ToString() + "')";
                Connection.updateData(qry);
            }
            else if (btnAdd.Text == "Modify")
            {
                string update_qry = "Update tbl_FormMaster set Name='" + txtFormName.Text.ToString() + "' where FormID=" + idList[gv_FormMaster.SelectedIndex];
                Connection.updateData(update_qry);
                btnAdd.Text = "Add";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Details saved successfully');</script>");
            txtFormName.Text = string.Empty;
            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_FormMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Connection.HighLightSelectedRow(gv_FormMaster);
            formID = gv_FormMaster.SelectedIndex;
            txtFormName.Text = gv_FormMaster.SelectedRow.Cells[0].Text.ToString();

            btnAdd.Text = "Modify";
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_FormMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_FormMaster, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtFormName.Text = string.Empty;
        btnAdd.Text = "Add";
    }
}