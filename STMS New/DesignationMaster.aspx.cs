using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
public partial class DesignationMaster : System.Web.UI.Page
{
    static int DesigID;
    static ArrayList idList;
    static ArrayList nameList;
    static ArrayList levelID;
    public void selectdata()
    {
        string qryStr = "select Name,LevelID from tbl_DesignationMaster";
        gv_DesigMaster.DataSource = Connection.loadData(qryStr);
        gv_DesigMaster.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DesignationMaster", Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            idList = new ArrayList();
            nameList = new ArrayList();
            levelID = new ArrayList();
            while (dr.Read())
            {
                idList.Add(dr[0]);
                nameList.Add(dr[1]);
                levelID.Add(dr[2]);
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
        txtDesignation.Text = String.Empty;
        txtLevelID.Text = String.Empty;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "Add")
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DesignationMaster where LevelID = " + Convert.ToInt16(txtLevelID.Text) + "", Connection.conn);
                Connection.conn.Open();
                int count = (int)cmd.ExecuteNonQuery();
                Connection.conn.Close();
                if (count >= 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Level ID already exists. Please enter another Level ID');</script>");
                }
                else
                {
                    string qry = "insert into tbl_DesignationMaster(Name,LevelID) values('" + txtDesignation.Text.ToString() + "'," + txtLevelID.Text.ToString() + ")";
                    Connection.updateData(qry);
                }

            }
            else if (btnAdd.Text == "Modify")
            {
                string update_qry = "Update tbl_DesignationMaster set Name='" + txtDesignation.Text.ToString() + "',LevelID=" + txtLevelID.Text.ToString() + " where DesigID=" + idList[gv_DesigMaster.SelectedIndex];
                Connection.updateData(update_qry);
                btnAdd.Text = "Add";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Details saved successfully');</script>");
            txtDesignation.Text = String.Empty;
            txtLevelID.Text = String.Empty;
            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_DesigMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Connection.HighLightSelectedRow(gv_DesigMaster);
            DesigID = gv_DesigMaster.SelectedIndex;
            txtDesignation.Text = gv_DesigMaster.SelectedRow.Cells[0].Text.ToString();
            txtLevelID.Text = gv_DesigMaster.SelectedRow.Cells[1].Text.ToString();

            btnAdd.Text = "Modify";
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_DesigMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_DesigMaster, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
        e.Row.Cells[1].Visible = false;
    }
}