using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class BreakTimeMaster : System.Web.UI.Page
{
    
    static int breakTimeID;
    static ArrayList idList;
    static double fromTime, toTime;
    public void selectdata()
    {
        string qryStr = "select FromTime,ToTime,Detail,BreakTime from tbl_BreakTimeMaster";
        gv_BreakMaster.DataSource = Connection.loadData(qryStr);
        gv_BreakMaster.DataBind();
    }
    void clearField()
    {
        txtDetail.Text = String.Empty;
        cmbFromHrs.SelectedValue = "- -";
        cmbFromMin.SelectedValue = "- -";
        cmbToHrs.SelectedValue = "- -";
        cmbToMin.SelectedValue = "- -";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_BreakTimeMaster", Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            idList = new ArrayList();
            while (dr.Read())
            {
                idList.Add(dr[0]);
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
            fromTime = Convert.ToDouble(cmbFromHrs.Text.ToString() + "." + cmbFromMin.Text.ToString());
            toTime = Convert.ToDouble(cmbToHrs.Text.ToString() + "." + cmbToMin.Text.ToString());
            if (fromTime > toTime)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('From time should less than To time.')", true);
            }
            else
            {
                string strfromTime = cmbFromHrs.Text.ToString() + ":" + cmbFromMin.Text.ToString();
                string strToTime = cmbToHrs.Text.ToString() + ":" + cmbToMin.Text.ToString();

                string breakTime = (TimeSpan.Parse(strToTime).Subtract(TimeSpan.Parse(strfromTime))).ToString();

                if (btnAdd.Text == "Add")
                {
                    string qry = "insert into tbl_BreakTimeMaster(FromTime,ToTime,Detail,BreakTime) values('" + strfromTime + "','" + strToTime + "','" + txtDetail.Text.ToString() + "','" + breakTime + "')";
                    Connection.updateData(qry);
                }
                else if (btnAdd.Text == "Modify")
                {
                    string update_qry = "Update tbl_BreakTimeMaster set FromTime='" + strfromTime + "',ToTime='" + strToTime + "',Detail='" + txtDetail.Text.ToString() + "',BreakTime='" + breakTime + "' where Id=" + idList[gv_BreakMaster.SelectedIndex];
                    Connection.updateData(update_qry);
                    btnAdd.Text = "Add";
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Details saved successfully');</script>");
                clearField();
            }
            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }

    protected void gv_BreakMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Connection.HighLightSelectedRow(gv_BreakMaster);
            breakTimeID = gv_BreakMaster.SelectedIndex;
            string fromtime = gv_BreakMaster.SelectedRow.Cells[0].Text.ToString();
            string[] splitFromTime = fromtime.Split(':');
            int i = 0;
            foreach (string word in splitFromTime)
            {
                if (i == 0)
                {
                    cmbFromHrs.Text = word;

                }
                if (i != 0)
                    cmbFromMin.Text = word;
                i++;
            }

            string totime = gv_BreakMaster.SelectedRow.Cells[1].Text.ToString();
            string[] splitToTime = totime.Split(':');
            i = 0;
            foreach (string word in splitToTime)
            {
                if (i == 0)
                {
                    cmbToHrs.Text = word;

                }
                if (i != 0)
                    cmbToMin.Text = word;
                i++;
            }



            txtDetail.Text = gv_BreakMaster.SelectedRow.Cells[2].Text.ToString();
            btnAdd.Text = "Modify";
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_BreakMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_BreakMaster, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (this.IsValid)
            Response.Redirect("EmployeeMaster.aspx");
    }
}