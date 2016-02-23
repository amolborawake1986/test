using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class LanguageMaster : System.Web.UI.Page
{
    static int LanguageID;
    static ArrayList idList;
    static ArrayList nameList;

    public void selectdata()
    {
        string qryStr = "select Name from tbl_LanguageMaster";
        gv_LangMaster.DataSource = Connection.loadData(qryStr);
        gv_LangMaster.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_LanguageMaster", Connection.conn);
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
        txtLangName.Text = string.Empty;
        btnAdd.Text = "Add";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "Add")
            {
                string qry = "insert into tbl_LanguageMaster(Name) values('" + txtLangName.Text.ToString() + "')";
                Connection.updateData(qry);
            }
            else if (btnAdd.Text == "Modify")
            {
                string update_qry = "Update tbl_LanguageMaster set Name='" + txtLangName.Text.ToString() + "' where LanguageID=" + idList[gv_LangMaster.SelectedIndex];
                Connection.updateData(update_qry);
                btnAdd.Text = "Add";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Details saved successfully');</script>");
            txtLangName.Text = string.Empty;
            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_LangMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        Connection.HighLightSelectedRow(gv_LangMaster);
        LanguageID = gv_LangMaster.SelectedIndex;
        txtLangName.Text = gv_LangMaster.SelectedRow.Cells[0].Text.ToString();

        btnAdd.Text = "Modify";
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_LangMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_LangMaster, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }
}