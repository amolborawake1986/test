using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class ShowDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string qryStr = "SELECT tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.Name as 'Task Name'," + Connection.tbl_ChkInOutDetailsName + ".CheckInTime," + Connection.tbl_ChkInOutDetailsName + ".CheckOutTime," + Connection.tbl_ChkInOutDetailsName + ".SpentTime," + Connection.tbl_ChkInOutDetailsName + ".InddorOutdoor," + Connection.tbl_ChkInOutDetailsName + ".SignatureData," + Connection.tbl_ChkInOutDetailsName + ".AddedLater," + Connection.tbl_ChkInOutDetailsName + ".CheckInOutID from tbl_ProjectMaster,tbl_TaskMaster," + Connection.tbl_ChkInOutDetailsName + " where " + Connection.tbl_ChkInOutDetailsName + ".ProjID=tbl_ProjectMaster.ProjID and " + Connection.tbl_ChkInOutDetailsName + ".TaskID=tbl_TaskMaster.TaskID and " + Connection.tbl_ChkInOutDetailsName + ".DayCheckInOutID=" + Session["selected_DayCheckInOutID"];
            gv_Details.DataSource = Connection.loadData(qryStr);

            gv_Details.DataBind();
            //gv_Details.Columns[7].Visible = false;
        }
        catch
        {
        }

    }

    protected void gv_Details_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Connection.HighLightSelectedRow(gv_FormTimesheetApprove);
            Session["CheckInOutIDForSign"] = int.Parse(gv_Details.SelectedRow.Cells[7].Text.ToString());
            string Fullurl = "ShowSignature.aspx";
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
    protected void gv_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.ToString() == "0")
                e.Row.Cells[5].Text = "Indoor";
            else if (e.Row.Cells[5].Text.ToString() == "1")
                e.Row.Cells[5].Text = "Outdoor";

            if (e.Row.Cells[8].Text.ToString() == "&nbsp;")
                e.Row.Cells[8].Text = "NO";
            //else if (e.Row.Cells[8].Text.ToString() == "Added Later")
            //    e.Row.Cells[8].ForeColor = Color.Red;


            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_Details, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Select Record for Details.";
        }
    }
}