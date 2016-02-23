using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
public partial class HolidayMaster : System.Web.UI.Page
{
    static int HolyID;
    static ArrayList idList;
    static ArrayList noteList;
    public void selectdata()
    {
        string qryStr = "select HolydayDate,Note from tbl_HolydayMaster";
        gv_HolidayMaster.DataSource = Connection.loadData(qryStr);
        gv_HolidayMaster.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try{

        dtpHolidayDate.Attributes.Add("readonly", "readonly");
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_HolydayMaster", Connection.conn);
        Connection.conn.Open();
        MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
        idList = new ArrayList();
        noteList = new ArrayList();
        while (dr.Read())
        {
            idList.Add(dr[0]);
            noteList.Add(dr[2]);
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
        dtpHolidayDate.Text = String.Empty;
        txtHolidayNote.Text = String.Empty;
        btnAdd.Text = "Modify";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try{
        if (btnAdd.Text == "Add")
        {
            string qry = "insert into tbl_HolydayMaster(HolydayDate,Note) values('" + dtpHolidayDate.Text.ToString() + "','"+txtHolidayNote.Text.ToString()+"')";
            Connection.updateData(qry);
        }
        else if (btnAdd.Text == "Modify")
        {
            string update_qry = "Update tbl_HolydayMaster set HolydayDate='" + dtpHolidayDate.Text.ToString() + "',Note='" + txtHolidayNote.Text.ToString() + "' where HolyID=" + idList[gv_HolidayMaster.SelectedIndex];
            Connection.updateData(update_qry);
            btnAdd.Text = "Add";
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Details saved successfully');</script>");
        dtpHolidayDate.Text = string.Empty;
        txtHolidayNote.Text = string.Empty;
        selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }


    protected void gv_HolidayMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_HolidayMaster, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }
    protected void gv_HolidayMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try{
        Connection.HighLightSelectedRow(gv_HolidayMaster);
        HolyID = gv_HolidayMaster.SelectedIndex;
        dtpHolidayDate.Text = gv_HolidayMaster.SelectedRow.Cells[0].Text.ToString();
        txtHolidayNote.Text = gv_HolidayMaster.SelectedRow.Cells[1].Text.ToString();

        btnAdd.Text = "Modify";
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeMaster.aspx");
    }
}