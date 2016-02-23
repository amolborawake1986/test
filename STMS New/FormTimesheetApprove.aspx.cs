using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class FormTimesheetApprove : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            dtpDate.Attributes.Add("readonly", "readonly");
            if (lstEmpName.Items.Count <= 0)
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_UserMaster", Connection.conn);
                Connection.conn.Open();
                MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
                lstEmpName.Items.Clear();
                lstEmpName.Items.Add("All");
                while (dr.Read())
                {
                    lstEmpName.Items.Add(dr[2].ToString());
                }
                Connection.conn.Close();
            }
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int selectedUserID = -1;
            string selectedEmpName = string.Empty;

            if (lstEmpName.Items.Count > 0)
            {
                for (int i = 0; i < lstEmpName.Items.Count; i++)
                {
                    if (lstEmpName.Items[i].Selected)
                    {
                        selectedEmpName = lstEmpName.Items[i].Text;
                    }
                }
            }


            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("SELECT tbl_UserMaster.UserID from tbl_UserMaster where tbl_UserMaster.Name='" + selectedEmpName + "'", Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                selectedUserID = int.Parse(dr[0].ToString());
            }
            dr.Close();

            Connection.conn.Close();

            string qryStr = "";
            string[] a = new string[3];            
            string selectedDate = dtpDate.Text.ToString();
            a = selectedDate.Split('/');
            string tbl_TimesheetName = "tbl_Timesheet" + a[2].ToString() + a[1].ToString();
            //if (rbtnApprove.Checked == true)
            //{
            //    qryStr = "Select TSID, Name, Intime, Outtime, ApproveStatus,DayCheckInOutID From " + Connection.tbl_TimesheetName + ", tbl_UserMaster Where Date='" + dtpDate.Text.ToString() + "' And ApproveStatus=1 And tbl_UserMaster.UserID=" + Connection.tbl_TimesheetName + ".UserID";
            //}
            //else if (rbtnNotApprove.Checked == true)
            //{
            //    qryStr = "Select  TSID, Name, Intime, Outtime, ApproveStatus,DayCheckInOutID From " + Connection.tbl_TimesheetName + ", tbl_UserMaster Where Date='" + dtpDate.Text.ToString() + "' And ApproveStatus=0 And tbl_UserMaster.UserID=" + Connection.tbl_TimesheetName + ".UserID";
            //}
            //else
            //{
            //    qryStr = "Select TSID, Name, Intime, Outtime, ApproveStatus,DayCheckInOutID From " + Connection.tbl_TimesheetName + ", tbl_UserMaster Where Date='" + dtpDate.Text.ToString() + "' And tbl_UserMaster.UserID=" + Connection.tbl_TimesheetName + ".UserID";
            //}

            //if (Convert.ToInt32(selectedUserID) > 0)
            //{
            //    qryStr += " And " + Connection.tbl_TimesheetName + ".UserID=" + selectedUserID;
            //}


            if (rbtnApprove.Checked == true)
            {
                qryStr = "Select TSID, Name, Intime, Outtime, ApproveStatus,DayCheckInOutID From " + tbl_TimesheetName + ", tbl_UserMaster Where Date='" + dtpDate.Text.ToString() + "' And ApproveStatus=1 And tbl_UserMaster.UserID=" + tbl_TimesheetName + ".UserID";
            }
            else if (rbtnNotApprove.Checked == true)
            {
                qryStr = "Select  TSID, Name, Intime, Outtime, ApproveStatus,DayCheckInOutID From " + tbl_TimesheetName + ", tbl_UserMaster Where Date='" + dtpDate.Text.ToString() + "' And ApproveStatus=0 And tbl_UserMaster.UserID=" + tbl_TimesheetName + ".UserID";
            }
            else
            {
                qryStr = "Select TSID, Name, Intime, Outtime, ApproveStatus,DayCheckInOutID From " + tbl_TimesheetName + ", tbl_UserMaster Where Date='" + dtpDate.Text.ToString() + "' And tbl_UserMaster.UserID=" + tbl_TimesheetName + ".UserID";
            }

            if (Convert.ToInt32(selectedUserID) > 0)
            {
                qryStr += " And " + tbl_TimesheetName + ".UserID=" + selectedUserID;
            }
            gv_FormTimesheetApprove.DataSource = Connection.loadData(qryStr);
            gv_FormTimesheetApprove.DataBind();


            gv_FormTimesheetApprove.Columns[0].Visible = false;
            gv_FormTimesheetApprove.Columns[6].Visible = false;
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_FormTimesheetApprove_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_FormTimesheetApprove, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Select Record for Details.";
        }
    }
    protected void gv_FormTimesheetApprove_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Connection.HighLightSelectedRow(gv_FormTimesheetApprove);

            //Connection.selected_DayCheckInOutID = int.Parse(gv_FormTimesheetApprove.SelectedRow.Cells[6].Text.ToString());
            Session["selected_DayCheckInOutID"] = int.Parse(gv_FormTimesheetApprove.SelectedRow.Cells[6].Text.ToString());
            string Fullurl = "ShowDetails.aspx";
            OpenNewBrowserWindow(Fullurl, this);
        }
        catch
        {
            Connection.conn.Close();
        }
    }


    public static void OpenNewBrowserWindow(string Url, Control control)
    {
        ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "');", true);
    }




    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string[] a = new string[3];
            string selectedDate = dtpDate.Text.ToString();
            a = selectedDate.Split('/');
            string tbl_TimesheetName = "tbl_Timesheet" + a[2].ToString() + a[1].ToString();

            ArrayList chkValueList = new ArrayList();
            ArrayList tsIDList = new ArrayList();
            int i = 0;
            foreach (GridViewRow gvrow in gv_FormTimesheetApprove.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkStatus");
                tsIDList.Add(gv_FormTimesheetApprove.Rows[i].Cells[0].Text.ToString());
                i++;

                if (chk != null)
                {
                    if (chk.Checked == true)
                        chkValueList.Add(1);
                    else
                        chkValueList.Add(0);
                }
            }
            for (int j = 0; j < tsIDList.Count; j++)
            {
                //string update_qry = "update " + Connection.tbl_TimesheetName + " set ApproveStatus=" + int.Parse(chkValueList[j].ToString()) + " where TSID=" + Convert.ToInt16(tsIDList[j].ToString());
                string update_qry = "update " + tbl_TimesheetName + " set ApproveStatus=" + int.Parse(chkValueList[j].ToString()) + " where TSID=" + Convert.ToInt16(tsIDList[j].ToString());
                Connection.updateData(update_qry);
            }
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        dtpDate.Text = string.Empty;
    }
}