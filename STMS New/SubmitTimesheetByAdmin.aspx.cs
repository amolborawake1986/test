using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;

public partial class SubmitTimesheetByAdmin : System.Web.UI.Page
{
    string InTime, OutTime;
    static int selectedUserID;

    string[] a = new string[3];
    string selectedDate = "";
    string tbl_ChkInOutDetailsName = "";
    string tbl_TimesheetName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            dtpDate.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                cmbInHrs.Items.Add("- -");
                cmbOutHrs.Items.Add("- -");
                cmbInMin.Items.Add("- -");
                cmbOutMin.Items.Add("- -");
                for (int i = 0; i <= 23; i++)
                {
                    cmbInHrs.Items.Add(i.ToString());
                    cmbOutHrs.Items.Add(i.ToString());
                }

                for (int i = 0; i <= 59; i++)
                {
                    cmbInMin.Items.Add(i.ToString());
                    cmbOutMin.Items.Add(i.ToString());
                }

                string qry = "SELECT tbl_UserMaster.UserID,tbl_EmployeeMaster.EmpID,concat(FirstName ,' ', MiddleName,' ' , SurnameName) as 'Employee Name' FROM tbl_EmployeeMaster,tbl_UserMaster where tbl_EmployeeMaster.EmpID=tbl_UserMaster.EmpID";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                Connection.conn.Open();
                cmbEmployee.Items.Clear();
                cmbEmployee.DataSource = cmd.ExecuteReader();
                cmbEmployee.DataTextField = "Employee Name";
                cmbEmployee.DataValueField = "UserID";
                cmbEmployee.DataBind();
                Connection.conn.Close();
            }
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void dtpDate_TextChanged(object sender, EventArgs e)
    {
        if (dtpDate.Text != "")
        {
            btnSubmitTimesheet.Enabled = true;
            btnShow.Enabled = true;

            //selectedDate = dtpDate.Text.ToString();
            //a = selectedDate.Split('/');
            //tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();
            //tbl_TimesheetName = "tbl_Timesheet" + a[2].ToString() + a[1].ToString();
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            selectedDate = dtpDate.Text.ToString();
            a = selectedDate.Split('/');
            tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();
            tbl_TimesheetName = "tbl_Timesheet" + a[2].ToString() + a[1].ToString();

            selectedUserID = int.Parse(cmbEmployee.SelectedItem.Value);
            string qry = "SELECT " + tbl_ChkInOutDetailsName + ".CheckInOutID," + tbl_ChkInOutDetailsName + ".DayCheckInOutID," + tbl_ChkInOutDetailsName + ".Date," + tbl_ChkInOutDetailsName + ".UserID," + tbl_ChkInOutDetailsName + ".ProjID," + tbl_ChkInOutDetailsName + ".TaskID," + tbl_ChkInOutDetailsName + ".InddorOutdoor," + tbl_ChkInOutDetailsName + ".CheckInTime," + tbl_ChkInOutDetailsName + ".CheckOutTime," + tbl_ChkInOutDetailsName + ".AddedLater," + tbl_ChkInOutDetailsName + ".SpentTime,tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.Name as 'Task Name' FROM STMS." + tbl_ChkInOutDetailsName + ",tbl_ProjectMaster,tbl_TaskMaster where " + tbl_ChkInOutDetailsName + ".ProjID=tbl_ProjectMaster.ProjID and " + tbl_ChkInOutDetailsName + ".TaskID=tbl_TaskMaster.TaskID and date='" + dtpDate.Text.ToString() + "' and " + tbl_ChkInOutDetailsName + ".UserID=" + selectedUserID + " and " + tbl_ChkInOutDetailsName + ".CheckOutTime is not null";
            gv_SubmitByAdmin.DataSource = Connection.loadData(qry);
            gv_SubmitByAdmin.DataBind();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnSubmitTimesheet_Click(object sender, EventArgs e)
    {
        try
        {
            int approveStatusFlag = 0;
            string recordFoundDate = "";

            selectedDate = dtpDate.Text.ToString();
            a = selectedDate.Split('/');
            tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();
            tbl_TimesheetName = "tbl_Timesheet" + a[2].ToString() + a[1].ToString();

            InTime = cmbInHrs.Text.ToString() + ":" + cmbInMin.Text.ToString();
            OutTime = cmbOutHrs.Text.ToString() + ":" + cmbOutMin.Text.ToString();

            string qry = "SELECT Date FROM " + tbl_ChkInOutDetailsName + " where Date='" + dtpDate.Text.ToString() + "' and UserID=" + selectedUserID + " and CheckOutTime is not null";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                recordFoundDate = dr["Date"].ToString();
            }
            Connection.conn.Close();


            string qry1 = "SELECT * FROM " + tbl_TimesheetName + " where UserID=" + selectedUserID + " and date='" + dtpDate.Text.ToString() + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(qry1, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                approveStatusFlag = (int)dr1["ApproveStatus"];
            }


            if (dtpDate.Text.ToString() == recordFoundDate)
            {
                if (dr1.HasRows == false)
                {
                    dr1.Close();
                    // string str_dayChkInOutID = selectedUserID.ToString() + Convert.ToDateTime(dtpDate.Text).ToString("dd") + Convert.ToDateTime(dtpDate.Text).ToString("MM") + Convert.ToDateTime(dtpDate.Text).ToString("yyyy");
                    string str_dayChkInOutID = selectedUserID.ToString() + a[0].ToString() + a[1].ToString() + a[2].ToString();
                    int dayChkInOutID = int.Parse(str_dayChkInOutID);

                    string qry2 = "insert into " + tbl_TimesheetName + "(UserID,DayCheckInOutID,Date,Intime,Outtime,ApproveStatus) values(" + selectedUserID + "," + dayChkInOutID + ",'" + dtpDate.Text.ToString() + "','" + InTime + "','" + OutTime + "',0)";
                    Connection.updateData(qry2);
                }
                else
                {
                    dr1.Close();
                    if (approveStatusFlag == 1)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Timesheet already approved.');</script>");
                    }
                    else
                    {
                        string qry2 = "Update " + Connection.tbl_TimesheetName + " set Intime='" + InTime + "', Outtime='" + OutTime + "' where UserID=" + selectedUserID + " and Date='" + dtpDate.Text.ToString() + "'";
                        Connection.updateData(qry2);
                    }
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Check In Out record Not found');</script>");
            }
            Connection.conn.Close();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_SubmitByAdmin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[4].Text.ToString() == "0")
            e.Row.Cells[4].Text = "Indoor";
        else if (e.Row.Cells[4].Text.ToString() == "1")
            e.Row.Cells[4].Text = "Outdoor";

        if (e.Row.Cells[6].Text.ToString() == "&nbsp;" )
            e.Row.Cells[6].Text = "NO";
        //else if (e.Row.Cells[6].Text.ToString() == "Added Later")
        //    e.Row.Cells[6].ForeColor = Color.Red;
    }
}