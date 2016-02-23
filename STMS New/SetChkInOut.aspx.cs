using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
public partial class SetChkInOut : System.Web.UI.Page
{
    static Dictionary<int, string> list;
    static int selectedID;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            dtpDate.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                rbtEmpwise_CheckedChanged(sender, e);
            }
        }
        catch
        {
            Connection.conn.Close();
        }

    }
    protected void rbtEmpwise_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string qry = string.Empty;
            if (rbtEmpwise.Checked == true)
            {
                lblName.Text = "Employee Name";
                qry = "SELECT UserID,Name FROM STMS.tbl_UserMaster";

            }
            else if (rbtnTeamWise.Checked == true)
            {
                lblName.Text = "Team Name";
                qry = "SELECT TeamID,Name FROM tbl_TeamMaster";
            }

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            cmbTeamName.Items.Clear();
            list = new Dictionary<int, string>();
            cmbTeamName.Items.Add("-- Select --");
            if (rbtEmpwise.Checked == true)
                cmbTeamName.Items.Add("All");
            while (dr.Read())
            {
                list.Add(int.Parse(dr[0].ToString()), dr[1].ToString());
                cmbTeamName.Items.Add(dr[1].ToString());
            }
            Connection.conn.Close();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void rbtnTeamWise_CheckedChanged(object sender, EventArgs e)
    {
        rbtEmpwise_CheckedChanged(sender, e);

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string qry = "";

            string[] a = new string[3];
            string selectedDate = dtpDate.Text.ToString();
            a = selectedDate.Split('/');
            string tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();


            if (rbtEmpwise.Checked == true)
            {
                if (cmbTeamName.SelectedValue.ToString() == "All")
                    //qry = "Select CheckInOutID,DayCheckInOutID,Date,tbl_ChkInOutDetails.UserID,ProjID,TaskID,InddorOutdoor,CheckInTime,CheckOutTime, SpentTime, MissingFlag, Name, Status From tbl_ChkInOutDetails, tbl_UserMaster Where tbl_UserMaster.UserID=tbl_ChkInOutDetails.UserID  And CheckOutTime is not null";
                    qry = "Select CheckInOutID,DayCheckInOutID,Date," + tbl_ChkInOutDetailsName + ".UserID,tbl_ProjectMaster.ProjID,tbl_ProjectMaster.Name as 'Project Name' ,tbl_TaskMaster.TaskID,tbl_TaskMaster.Name as 'Task Name',InddorOutdoor,CheckInTime,CheckOutTime, SpentTime, MissingFlag, tbl_UserMaster.Name as 'User Name',tbl_UserMaster.Status From " + tbl_ChkInOutDetailsName + ", tbl_UserMaster, tbl_ProjectMaster,tbl_TaskMaster Where tbl_UserMaster.UserID=" + tbl_ChkInOutDetailsName + ".UserID And tbl_TaskMaster.TaskID=" + tbl_ChkInOutDetailsName + ".TaskID And tbl_TaskMaster.ProjID=tbl_ProjectMaster.ProjID And CheckOutTime is not null";
                else
                    //qry = "Select CheckInOutID,DayCheckInOutID,Date,tbl_ChkInOutDetails.UserID,ProjID,TaskID,InddorOutdoor,CheckInTime,CheckOutTime, SpentTime, MissingFlag, Name, Status From tbl_ChkInOutDetails, tbl_UserMaster Where tbl_UserMaster.UserID=tbl_ChkInOutDetails.UserID And tbl_ChkInOutDetails.UserID=" + selectedID + " And  CheckOutTime is not null";
                    qry = "Select CheckInOutID,DayCheckInOutID,Date," + tbl_ChkInOutDetailsName + ".UserID,tbl_ProjectMaster.ProjID,tbl_ProjectMaster.Name as 'Project Name' ,tbl_TaskMaster.TaskID,tbl_TaskMaster.Name as 'Task Name',InddorOutdoor,CheckInTime,CheckOutTime, SpentTime, MissingFlag, tbl_UserMaster.Name as 'User Name',tbl_UserMaster.Status From " + tbl_ChkInOutDetailsName + ", tbl_UserMaster, tbl_ProjectMaster,tbl_TaskMaster Where tbl_UserMaster.UserID=" + tbl_ChkInOutDetailsName + ".UserID And tbl_TaskMaster.TaskID=" + tbl_ChkInOutDetailsName + ".TaskID And tbl_TaskMaster.ProjID=tbl_ProjectMaster.ProjID And " + tbl_ChkInOutDetailsName + ".UserID=" + selectedID + " And CheckOutTime is not null";
            }
            else if (rbtnTeamWise.Checked == true)
            {
                //if (list != null)
                qry = "Select Distinct CheckInOutID,DayCheckInOutID,Date," + tbl_ChkInOutDetailsName + ".UserID,tbl_ProjectMaster.ProjID,tbl_ProjectMaster.Name as 'Project Name' ,tbl_TaskMaster.TaskID,tbl_TaskMaster.Name as 'Task Name',InddorOutdoor,CheckInTime,CheckOutTime, SpentTime, MissingFlag, tbl_UserMaster.Name as 'User Name', tbl_UserMaster.Status From " + tbl_ChkInOutDetailsName + ", tbl_UserMaster, tbl_User_Team,tbl_ProjectMaster,tbl_TaskMaster Where tbl_UserMaster.UserID=" + tbl_ChkInOutDetailsName + ".UserID And tbl_User_Team.UserID=" +tbl_ChkInOutDetailsName + ".UserID And tbl_User_Team.TeamID=101 And CheckOutTime is not null And tbl_TaskMaster.TaskID=" + tbl_ChkInOutDetailsName + ".TaskID And tbl_TaskMaster.ProjID=tbl_ProjectMaster.ProjID And tbl_User_Team.TeamID=" + selectedID + " And CheckOutTime is not null";
                //else
                //    qry = "Select Distinct CheckInOutID,DayCheckInOutID,Date,tbl_ChkInOutDetails.UserID,ProjID,TaskID,InddorOutdoor,CheckInTime,CheckOutTime, SpentTime, MissingFlag, Name, Status From tbl_ChkInOutDetails, tbl_UserMaster, tbl_User_Team Where tbl_UserMaster.UserID=tbl_ChkInOutDetails.UserID And tbl_User_Team.UserID=tbl_ChkInOutDetails.UserID And CheckOutTime is not null";

            }
            qry += " And " + tbl_ChkInOutDetailsName + ".Date='" + dtpDate.Text.ToString() + "'";

            gv_SetChkInOut.DataSource = Connection.loadData(qry);
            gv_SetChkInOut.DataBind();

            gv_SetChkInOut.Columns[7].Visible = false;
            gv_SetChkInOut.Columns[8].Visible = false;
            gv_SetChkInOut.Columns[9].Visible = false;
            gv_SetChkInOut.Columns[10].Visible = false;
            gv_SetChkInOut.Columns[11].Visible = false;
            gv_SetChkInOut.Columns[12].Visible = false;
            gv_SetChkInOut.Columns[13].Visible = false;
            gv_SetChkInOut.Columns[14].Visible = false;
            gv_SetChkInOut.Columns[16].Visible = false;
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void cmbTeamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cmbTeamName.Text != "All")
            {
                if (rbtEmpwise.Checked == true)
                    selectedID = list.Keys.ToArray()[cmbTeamName.SelectedIndex - 2];
                else if (rbtnTeamWise.Checked == true)
                    selectedID = list.Keys.ToArray()[cmbTeamName.SelectedIndex - 1];
            }
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_SetChkInOut_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["selected_CheckInOutID"] = int.Parse(gv_SetChkInOut.SelectedRow.Cells[16].Text.ToString());
            string Fullurl = "ShowDetailsSetChkInOut.aspx/";
            OpenNewBrowserWindow(Fullurl, this);
        }
        catch
        {

        }
    }
    public static void OpenNewBrowserWindow(string Url, Control control)
    {
        ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" + Url + "');", true);
    }

    protected void gv_SetChkInOut_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.ToString() == "0")
                e.Row.Cells[5].Text = "Indoor";
            else if (e.Row.Cells[5].Text.ToString() == "1")
                e.Row.Cells[5].Text = "Outdoor";

            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_SetChkInOut, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Select Record for Details.";
        }
    }
}