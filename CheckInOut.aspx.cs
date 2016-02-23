using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class CheckInOut : System.Web.UI.Page
{
    static int ProjID = 0, TaskID = 0;
    static int IndoorOutdoor = 0;
    string InTime, OutTime;
    static int selectedID;
    static int notCheckedOutID;
    TimeSpan totalTimeSpent = TimeSpan.Zero;
    //static ArrayList List_ProjID;
    //static ArrayList List_ProjName;
    //static ArrayList List_TaskID;
    //static ArrayList List_TaskName;
    static string firstCheckIn = "";
    static string lastCheckUut = "";
    string tbl_TimesheetName = "";
    string tbl_ChkInOutDetailsName = "";
    static string currentDate = (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("dd/MM/yyyy").ToString();
    string[] a = new string[3];

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            totalTimeSpent = TimeSpan.Zero;
            a = currentDate.Split('/');
            tbl_TimesheetName = "tbl_Timesheet" + a[2].ToString() + a[1].ToString();
            tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();

            checkBreakDetail();
            if (rbtnCurrentWorking.Checked == true)
            {
                dtpDate.Visible = false;
                cmbInHrs.Visible = false;
                cmbInMin.Visible = false;
                cmbOutHrs.Visible = false;
                cmbOutMin.Visible = false;
                lblSelectDate.Visible = false;
                CompareValidator1.Visible = false;
                cmbInMin.Visible = false;lblFromMin.Visible = false; lblFromHrs.Visible = false; cmbInHrs.Visible = false; lblInTime.Visible = false;
                lblOutTime.Visible = false; cmbOutHrs.Visible = false; lblToHrs.Visible = false; cmbOutMin.Visible = false; lblToMin.Visible = false; CompareValidator1.Visible = false;
                txt_NoOfStrok.Visible = false;
                cmbInHrs.SelectedIndex = 0;
                cmbOutHrs.SelectedIndex = 0;
                cmbInMin.SelectedIndex = 0;
                cmbOutMin.SelectedIndex = 0;
            }
            else if (rbtnFillPrevious.Checked == true)
            {
                lblSelectDate.Visible = true;
                CompareValidator1.Enabled = true;
                cmbInMin.Visible = true; lblFromMin.Visible = true; lblFromHrs.Visible = true; cmbInHrs.Visible = true; lblInTime.Visible = true;
                lblOutTime.Visible = true; cmbOutHrs.Visible = true; lblToHrs.Visible = true; cmbOutMin.Visible = true; lblToMin.Visible = true; CompareValidator1.Visible = true; 
                dtpDate.Enabled = true;
                cmbInHrs.Enabled = true;
                cmbInMin.Enabled = true;
                cmbOutHrs.Enabled = true;
                cmbOutMin.Enabled = true;
                dtpDate.Visible = true;
            }

            if (!IsPostBack)
            {


                dtpDate.Attributes.Add("readonly", "readonly");
                dtpDate.Text = (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("dd/MM/yyyy").ToString();
                //if (!IsPostBack)
                //{


                    for (int i = 0; i <= 23; i++)
                    {
                        if (i.ToString().Length == 1)
                        {
                            cmbInHrs.Items.Add("0" + i.ToString());
                            cmbOutHrs.Items.Add("0" + i.ToString());
                        }
                        else
                        {
                            cmbInHrs.Items.Add(i.ToString());
                            cmbOutHrs.Items.Add(i.ToString());
                        }
                    }

                    for (int i = 0; i <= 59; i++)
                    {
                        if (i.ToString().Length == 1)
                        {
                            cmbInMin.Items.Add("0" + i.ToString());
                            cmbOutMin.Items.Add("0" + i.ToString());
                        }
                        else
                        {
                            cmbInMin.Items.Add(i.ToString());
                            cmbOutMin.Items.Add(i.ToString());
                        }
                    }
                //}



                //List_ProjID = new ArrayList();
                //List_ProjName = new ArrayList();
                //List_TaskID = new ArrayList();
                //List_TaskName = new ArrayList();

                //string qry = "SELECT tbl_ProjectMaster.ProjID,tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.TaskID,tbl_TaskMaster.Name as 'Task Name' FROM tbl_ProjectMaster,tbl_TaskMaster where tbl_ProjectMaster.ProjID=tbl_TaskMaster.ProjID order by tbl_ProjectMaster.ProjID";
                string qry = "SELECT DISTINCT tbl_ProjectMaster.ProjID,tbl_ProjectMaster.Name as 'Project Name' FROM tbl_User_Project,tbl_ProjectMaster WHERE tbl_ProjectMaster.ProjID=tbl_User_Project.ProjID and tbl_User_Project.UserID=" + Session["loggedInUserID"];
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                Connection.conn.Open();
                //MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();

                //while (dr.Read())
                //{
                //    List_ProjID.Add(dr["ProjID"]);
                //    List_ProjName.Add(dr["Project Name"]);
                //    List_TaskID.Add(dr["TaskID"]);
                //    List_TaskName.Add(dr["Task Name"]);
                //    if (cmbProjName.Items.Contains(new ListItem(dr["Project Name"].ToString())) == false)
                //    {
                //        cmbProjName.Items.Add(dr["Project Name"].ToString());
                //    }

                //}
                cmbProjName.DataSource = cmd.ExecuteReader();
                cmbProjName.DataTextField = "Project Name";
                cmbProjName.DataValueField = "ProjID";
                cmbProjName.DataBind();
                cmbProjName.Items.Insert(0, "-- Select --");
                Connection.conn.Close();
                selectdata();
            }
        }
        catch
        {
            Connection.conn.Close();
        }


    }

    void checkBreakDetail()
    {
        string date = (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("dd/MM/yyyy");
        string qry = "Select * from " + Connection.tbl_BreakTimeName + " where Date='" + date + "' and UserID=" + Session["loggedInUserID"].ToString() + " and BreakEndTime IS NULL";
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
        DataTable dt = new DataTable();
        Connection.conn.Open();
        dt.Load(cmd.ExecuteReader());
        Connection.conn.Close();
        if (dt.Rows.Count != 0)
        {
            Server.Transfer("TakeBreak.aspx");
        }
    }

    public void selectdata()
    {
         string qry = "SELECT tbl_ProjectMaster.ProjID,tbl_TaskMaster.TaskID," + Connection.tbl_ChkInOutDetailsName + ".CheckInOutID,tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.Name as 'Task Name'," + Connection.tbl_ChkInOutDetailsName + ".CheckInTime," + Connection.tbl_ChkInOutDetailsName + ".CheckOutTime," + Connection.tbl_ChkInOutDetailsName + ".SpentTime," + Connection.tbl_ChkInOutDetailsName + ".InddorOutdoor," + Connection.tbl_ChkInOutDetailsName + ".AddedLater from tbl_ProjectMaster inner join " + Connection.tbl_ChkInOutDetailsName + " on tbl_ProjectMaster.ProjID=" + Connection.tbl_ChkInOutDetailsName + ".ProjID inner join tbl_TaskMaster on tbl_TaskMaster.TaskID=" + Connection.tbl_ChkInOutDetailsName + ".TaskID and " + Connection.tbl_ChkInOutDetailsName + ".Date='" + (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("dd/MM/yyyy") + "' and UserID=" + Session["loggedInUserID"];
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
        Connection.conn.Open();
        DataTable dt = new DataTable();
        dt.Load(cmd.ExecuteReader());
        // MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
        gv_CheckInOut.DataSource = dt;
        gv_CheckInOut.DataBind();

        if (dt.Rows.Count == 0)
        {
            btnChkOut.Enabled = false;
            btnChkIn.Enabled = true;
            btnUpdate.Enabled = true;
            btnTakeBreak.Enabled = false;
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            firstCheckIn = dt.Rows[0]["CheckInTime"].ToString();
            if (dt.Rows[dt.Rows.Count - 1]["CheckOutTime"].ToString() != "")
            {
                lastCheckUut = dt.Rows[dt.Rows.Count - 1]["CheckOutTime"].ToString();
            }


            if (dt.Rows[i]["CheckOutTime"].ToString() == "")
            {
                btnChkOut.Enabled = true;
                btnChkIn.Enabled = false;
                txt_NoOfStrok.Visible = true;
                btnUpdate.Enabled = false;
                btnTakeBreak.Enabled = true;
                btnChangeTask.Enabled = true;
                //cmbProjName.SelectedIndex = cmbProjName.Items.IndexOf(new ListItem(dt.Rows[i]["Project Name"].ToString()));
                //cmbProjName.Enabled = false;
                //cmbTaskName.Items.Clear();

                //ArrayList TaskSublist = List_TaskName.GetRange(List_ProjName.IndexOf(cmbProjName.SelectedValue), List_ProjName.LastIndexOf(cmbProjName.SelectedValue) - List_ProjName.IndexOf(cmbProjName.SelectedValue) + 1);
                //cmbTaskName.Items.Add("-- Select --");
                //for (int j = 0; j < TaskSublist.Count; j++)
                //{
                //    cmbTaskName.Items.Add(TaskSublist[j].ToString());
                //}
                //cmbTaskName.SelectedIndex = cmbTaskName.Items.IndexOf(new ListItem(dt.Rows[i]["Task Name"].ToString()));
                //cmbTaskName.Enabled = false;
                notCheckedOutID = (int)dt.Rows[i]["CheckInOutID"];
            }
            else
            {
                btnChkOut.Enabled = false;
                btnChkIn.Enabled = true;
                // btnUpdate.Enabled = true;
                btnTakeBreak.Enabled = false;
                btnChangeTask.Enabled = false;
            }
        }
        Connection.conn.Close();


    }


    protected void cmbProjName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cmbTaskName.Items.Clear();

            //ArrayList TaskSublist = List_TaskName.GetRange(List_ProjName.IndexOf(cmbProjName.SelectedValue), List_ProjName.LastIndexOf(cmbProjName.SelectedValue) - List_ProjName.IndexOf(cmbProjName.SelectedValue) + 1);
            //cmbTaskName.Items.Add("-- Select --");
            //for (int i = 0; i < TaskSublist.Count; i++)
            //{
            //    cmbTaskName.Items.Add(TaskSublist[i].ToString());
            //}

            ProjID = Convert.ToInt16(cmbProjName.SelectedItem.Value.ToString());

            string qry = "SELECT DISTINCT tbl_TaskMaster.TaskID,tbl_TaskMaster.Name as 'Task Name' FROM tbl_User_Project,tbl_TaskMaster WHERE tbl_TaskMaster.TaskID=tbl_User_Project.TaskID and tbl_User_Project.ProjID=" + ProjID + " and tbl_User_Project.UserID=" + Session["loggedInUserID"];
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
            Connection.conn.Open();

            cmbTaskName.DataSource = cmd.ExecuteReader();
            cmbTaskName.DataTextField = "Task Name";
            cmbTaskName.DataValueField = "TaskID";
            cmbTaskName.DataBind();
            cmbTaskName.Items.Insert(0, "-- Select --");
            Connection.conn.Close();
            //ProjID = (int)List_ProjID[List_ProjName.IndexOf(cmbProjName.SelectedValue)];
            selectdata();

        }
        catch
        {
            Connection.conn.Close();
        }

    }
    protected void btnChkIn_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnCurrentWorking.Checked == true)
            {
                btnChkIn.Enabled = false;
                btnChkOut.Enabled = true;
                //cmbProjName.Enabled = false;
                //cmbTaskName.Enabled = false;
                btnTakeBreak.Enabled = true;
                txt_NoOfStrok.Visible = true;
                txt_NoOfStrok.Focus();
                //string str_dayChkInOutID = Connection.loggedInUserID.ToString() + System.DateTime.Now.ToString("dd") + System.DateTime.Now.ToString("MM") + System.DateTime.Now.ToString("yyyy");

                string str_dayChkInOutID = Session["loggedInUserID"].ToString() + (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("ddMMyyyy");
                int dayChkInOutID = int.Parse(str_dayChkInOutID);
                TaskID = Convert.ToInt16(cmbTaskName.SelectedItem.Value.ToString());
                string Intime = (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("HH:mm"); // to manage server time
                //string Intime = (System.DateTime.Now).ToString();
                //string qry = "insert into tbl_ChkInOutDetails(DayCheckInOutID,Date,UserID,ProjID,TaskID,InddorOutdoor,CheckInTime) values(" + dayChkInOutID + ",'" + DateTime.Today.ToString("dd/MM/yyyy") + "'," + Connection.loggedInUserID + "," + ProjID + "," + TaskID + "," + IndoorOutdoor + ",'" + System.DateTime.Now.ToShortTimeString() + "'); SELECT LAST_INSERT_ID();";
                string qry = "insert into " + Connection.tbl_ChkInOutDetailsName + "(DayCheckInOutID,Date,UserID,ProjID,TaskID,InddorOutdoor,CheckInTime) values(" + dayChkInOutID + ",'" + (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("dd/MM/yyyy") + "'," + Session["loggedInUserID"] + "," + ProjID + "," + TaskID + "," + IndoorOutdoor + ",'" + Intime + "'); SELECT LAST_INSERT_ID();";

                MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                Connection.conn.Open();
                notCheckedOutID = Convert.ToInt32(cmd2.ExecuteScalar());
                Connection.conn.Close();
                selectdata();

                //Nakul Actual start and End Date
                string actualStartDate = "";
                string actualEndDate = "";
                string checkInDate = (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("dd/MM/yyyy");
                qry = "select tbl_TaskMaster.ActulalStart, tbl_TaskMaster.ActualEnd from tbl_TaskMaster where ProjID = " + ProjID + " and TaskID = " + TaskID;
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                Connection.conn.Open();
                MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    actualStartDate = dr[0].ToString();
                    actualEndDate = dr[1].ToString();
                }
                Connection.conn.Close();
                //Nakul Actual start Date
                  string[] dateFor = new string[3];
                  dateFor = checkInDate.Split('/');
                  DateTime chekInComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                  dateFor = actualStartDate.Split('/');
                  if (dateFor.Count() == 3)
                  {
                      DateTime startComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                      if (DateTime.Compare(chekInComDate, startComDate) < 0)
                      {
                          string update_qry = "Update tbl_TaskMaster set ActulalStart ='" + checkInDate + "' where TaskID =" + TaskID;
                          Connection.updateData(update_qry);
                      }
                  }
                  else {
                      string update_qry = "Update tbl_TaskMaster set ActulalStart ='" + checkInDate + "' where TaskID =" + TaskID;
                      Connection.updateData(update_qry);
                  }
                  //Nakul Actual End Date
                   dateFor = actualEndDate.Split('/');
                   if (dateFor.Count() == 3)
                   {
                       DateTime endComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                       if (DateTime.Compare(chekInComDate, endComDate) > 0)
                       {
                           string update_qry = "Update tbl_TaskMaster set ActualEnd ='" + checkInDate + "' where TaskID =" + TaskID;
                           Connection.updateData(update_qry);
                       }
                   }
                   else
                   {
                       string update_qry = "Update tbl_TaskMaster set ActualEnd ='" + checkInDate + "' where TaskID =" + TaskID;
                       Connection.updateData(update_qry);
                   }
                   //End 
                  
            }
            else if (rbtnFillPrevious.Checked == true)
            {

                string date = dtpDate.Text.ToString();
                string[] a = new string[3];
                a = date.Split('/');

                string str_dayChkInOutID = Session["loggedInUserID"].ToString() + a[0].ToString() + a[1].ToString() + a[2].ToString();//Convert.ToDateTime(dtpDate.Text).ToString("ddMMyyyy");
                int dayChkInOutID = int.Parse(str_dayChkInOutID);


                string tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();

                string Intime = Convert.ToDateTime(cmbInHrs.SelectedItem.Text.ToString() + ":" + cmbInMin.SelectedItem.Text.ToString()).ToString("HH:mm");//(System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("HH:mm"); // to manage server time
                string Outtime = Convert.ToDateTime(cmbOutHrs.SelectedItem.Text.ToString() + ":" + cmbOutMin.SelectedItem.Text.ToString()).ToString("HH:mm");
                TaskID = Convert.ToInt16(cmbTaskName.SelectedItem.Value.ToString());
                string difference = DateTime.Parse(Outtime).Subtract(DateTime.Parse(Intime)).ToString("t");
                string spentTime1 = DateTime.Parse(difference).ToString("HH:mm");
                double spentTime = double.Parse(spentTime1.Replace(":", "."));


                string qry = "insert into " + tbl_ChkInOutDetailsName + "(DayCheckInOutID,Date,UserID,ProjID,TaskID,InddorOutdoor,CheckInTime,CheckOutTime,SpentTime,AddedLater) values(" + dayChkInOutID + ",'" + dtpDate.Text.ToString() + "'," + Session["loggedInUserID"] + "," + ProjID + "," + TaskID + "," + IndoorOutdoor + ",'" + Intime + "','" + Outtime + "','" + spentTime + "','Added Later');";

                MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                Connection.conn.Open();
                cmd2.ExecuteNonQuery();
                Connection.conn.Close();

                //to show data

                qry = "SELECT tbl_ProjectMaster.ProjID,tbl_TaskMaster.TaskID," + tbl_ChkInOutDetailsName + ".CheckInOutID,tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.Name as 'Task Name'," + tbl_ChkInOutDetailsName + ".CheckInTime," + tbl_ChkInOutDetailsName + ".CheckOutTime," + tbl_ChkInOutDetailsName + ".SpentTime," + tbl_ChkInOutDetailsName + ".InddorOutdoor," + tbl_ChkInOutDetailsName + ".AddedLater from tbl_ProjectMaster inner join " + tbl_ChkInOutDetailsName + " on tbl_ProjectMaster.ProjID=" + tbl_ChkInOutDetailsName + ".ProjID inner join tbl_TaskMaster on tbl_TaskMaster.TaskID=" + tbl_ChkInOutDetailsName + ".TaskID and " + tbl_ChkInOutDetailsName + ".Date='" + date + "' and UserID=" + Session["loggedInUserID"];
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                Connection.conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                // MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
                gv_CheckInOut.DataSource = dt;
                gv_CheckInOut.DataBind();

                //Nakul Actual start and End Date
                string actualStartDate = "";
                string actualEndDate = "";
                string checkInDate = dtpDate.Text.ToString();
                qry = "select tbl_TaskMaster.ActulalStart, tbl_TaskMaster.ActualEnd from tbl_TaskMaster where ProjID = " + ProjID + " and TaskID = " + TaskID;
                MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                Connection.conn.Open();
                MySql.Data.MySqlClient.MySqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    actualStartDate = dr[0].ToString();
                    actualEndDate = dr[1].ToString();
                }
                Connection.conn.Close();
                //Nakul Actual start Date
                string[] dateFor = new string[3];
                dateFor = checkInDate.Split('/');
                DateTime chekInComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                dateFor = actualStartDate.Split('/');
                if (dateFor.Count() == 3)
                {
                    DateTime startComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                    if (DateTime.Compare(chekInComDate, startComDate) < 0)
                    {
                        string update_qry = "Update tbl_TaskMaster set ActulalStart ='" + checkInDate + "' where TaskID =" + TaskID;
                        Connection.updateData(update_qry);
                    }
                }
                else
                {
                    string update_qry = "Update tbl_TaskMaster set ActulalStart ='" + checkInDate + "' where TaskID =" + TaskID;
                    Connection.updateData(update_qry);
                }
                //Nakul Actual End Date
                dateFor = actualEndDate.Split('/');
                if (dateFor.Count() == 3)
                {
                    DateTime endComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                    if (DateTime.Compare(chekInComDate, endComDate) > 0)
                    {
                        string update_qry = "Update tbl_TaskMaster set ActualEnd ='" + checkInDate + "' where TaskID =" + TaskID;
                        Connection.updateData(update_qry);
                    }
                }
                else
                {
                    string update_qry = "Update tbl_TaskMaster set ActualEnd ='" + checkInDate + "' where TaskID =" + TaskID;
                    Connection.updateData(update_qry);
                }
                //End 
            }


        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnChkOut_Click(object sender, EventArgs e)
    {
        if (txt_NoOfStrok.Text == "")
        {
            string display = "please Enter Number of Strock";
            ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            txt_NoOfStrok.Visible = true;
            txt_NoOfStrok.Focus();
           
        }

        else
        {

            try
            {
                int approveStatusFlag = 0;
                string recordFoundDate = "";
                checkoutMethod();
                selectdata();
                btnChkIn.Enabled = true;
                //selectedDate = dtpDate.Text.ToString();
                //a = selectedDate.Split('/');
                //tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();
                //tbl_TimesheetName = "tbl_Timesheet" + a[2].ToString() + a[1].ToString();

                InTime = firstCheckIn;//cmbInHrs.Text.ToString() + ":" + cmbInMin.Text.ToString();
                OutTime = lastCheckUut;//cmbOutHrs.Text.ToString() + ":" + cmbOutMin.Text.ToString();

                string qry = "SELECT Date,TaskID FROM " + tbl_ChkInOutDetailsName + " where Date='" + currentDate + "' and UserID=" + Session["loggedInUserID"] + " and CheckOutTime is not null";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                Connection.conn.Open();
                MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    recordFoundDate = dr["Date"].ToString();
                    TaskID = (int)dr["TaskID"];//Nakul get TaskId
                }
                Connection.conn.Close();


                string qry1 = "SELECT * FROM " + tbl_TimesheetName + " where UserID=" + Session["loggedInUserID"] + " and date='" + currentDate + "'";
                MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(qry1, Connection.conn);
                Connection.conn.Open();
                MySql.Data.MySqlClient.MySqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    approveStatusFlag = (int)dr1["ApproveStatus"];
                }


                if (currentDate == recordFoundDate)
                {
                    if (dr1.HasRows == false)
                    {
                        dr1.Close();
                        //string str_dayChkInOutID = Session["loggedInUserID"].ToString() + Convert.ToDateTime(dtpDate.Text).ToString("dd") + Convert.ToDateTime(dtpDate.Text).ToString("MM") + Convert.ToDateTime(dtpDate.Text).ToString("yyyy");
                        string str_dayChkInOutID = Session["loggedInUserID"].ToString() + a[0].ToString() + a[1].ToString() + a[2].ToString();
                        int dayChkInOutID = int.Parse(str_dayChkInOutID);

                        string qry2 = "insert into " + tbl_TimesheetName + "(UserID,DayCheckInOutID,Date,Intime,Outtime,ApproveStatus) values(" + Session["loggedInUserID"] + "," + dayChkInOutID + ",'" + currentDate + "','" + InTime + "','" + OutTime + "',0)";
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
                            string qry2 = "Update " + tbl_TimesheetName + " set Intime='" + InTime + "', Outtime='" + OutTime + "' where UserID=" + Session["loggedInUserID"] + " and Date='" + currentDate + "'";
                            Connection.updateData(qry2);
                        }
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Check In Out record Not found');</script>");
                }
                //Nakul add percentage Of Complete
                string update_qry = "Update tbl_TaskMaster set percentageOfComplete ='" + txt_NoOfStrok.Text + "' where TaskID =" + TaskID;
                Connection.updateData(update_qry);
                Connection.conn.Close();
                //End 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Timesheet Submited Successfully.');</script>");
                //showData();
            }
            catch
            {
                Connection.conn.Close();
            }
        }
    }
    //protected void gv_CheckInOut_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (btnUpdate.Enabled == true)
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Please Checkout first.');</script>");
    //        }
    //        else
    //        {
    //            Connection.HighLightSelectedRow(gv_CheckInOut);
    //            selectedID = int.Parse(gv_CheckInOut.SelectedRow.Cells[2].Text.ToString());
    //            cmbProjName.SelectedIndex = cmbProjName.Items.IndexOf(new ListItem(gv_CheckInOut.SelectedRow.Cells[3].Text.ToString()));
    //            cmbTaskName.Items.Clear();

    //            ArrayList TaskSublist = List_TaskName.GetRange(List_ProjName.IndexOf(cmbProjName.SelectedValue), List_ProjName.LastIndexOf(cmbProjName.SelectedValue) - List_ProjName.IndexOf(cmbProjName.SelectedValue) + 1);
    //            cmbTaskName.Items.Add("-- Select --");
    //            for (int j = 0; j < TaskSublist.Count; j++)
    //            {
    //                cmbTaskName.Items.Add(TaskSublist[j].ToString());
    //            }
    //            cmbTaskName.SelectedIndex = cmbTaskName.Items.IndexOf(new ListItem(gv_CheckInOut.SelectedRow.Cells[4].Text.ToString()));

    //            btnUpdate.Enabled = true;
    //            btnChkIn.Enabled = false;
    //            btnChkOut.Enabled = false;
    //        }
    //    }
    //    catch
    //    {
    //        Connection.conn.Close();
    //    }
    //}

    protected void gv_CheckInOut_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[8].Text.ToString() == "0")
                    e.Row.Cells[8].Text = "Indoor";
                else if (e.Row.Cells[8].Text.ToString() == "1")
                    e.Row.Cells[8].Text = "Outdoor";

                if (e.Row.Cells[9].Text.ToString() == "&nbsp;")
                    e.Row.Cells[9].Text = "NO";
                //else if (e.Row.Cells[9].Text.ToString() == "Added Later")
                //    e.Row.Cells[9].ForeColor = Color.Red;

                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_CheckInOut, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = new Label();
                lbl.Text = "Worked Time =";
                e.Row.Cells[6].Controls.Add(lbl);
                Label lbl1 = new Label();
                lbl1.Text = totalTimeSpent.ToString();
                e.Row.Cells[7].Controls.Add(lbl1);
                               
            }

            if (e.Row.Cells[7].Text.ToString() != "SpentTime" && e.Row.Cells[7].Text.ToString() != "&nbsp;")
            {
                string s = e.Row.Cells[7].Text.ToString();
                if (s.Length < 2)
                {
                    s = "0" + e.Row.Cells[7].Text.ToString() + ".00";
                    e.Row.Cells[7].Text = s;
                }
                else
                {
                    string[] temp = new string[2];
                    string hr, mm;
                    temp = s.Split('.');
                    if (temp[0].ToString().Length < 2)
                        hr = "0" + temp[0].ToString();
                    else
                        hr = temp[0].ToString();

                    if (temp[1].ToString().Length < 2)
                        mm = temp[1].ToString() + "0";
                    else
                        mm = temp[1].ToString();

                    s = hr + "." + mm;
                    e.Row.Cells[7].Text = s;

                }
                string a = s.Replace(".", ":");
                totalTimeSpent = totalTimeSpent.Add(TimeSpan.Parse(a));

            }
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            btnUpdate.Enabled = false;
            btnChkIn.Enabled = true;

            string update_qry = "Update " + Connection.tbl_ChkInOutDetailsName + " set ProjID=" + ProjID + ",TaskID=" + TaskID + ",InddorOutdoor=" + IndoorOutdoor + " where CheckInOutID=" + selectedID;
            Connection.updateData(update_qry);
            selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_CheckInOut_RowCreated(object sender, GridViewRowEventArgs e) //To hide the coloumn from gridview
    {
        try
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;

        }
        catch
        {

        }
    }
    protected void cmbTaskName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //TaskID = (int)List_TaskID[List_TaskName.IndexOf(cmbTaskName.SelectedValue)];
        }
        catch
        {
        }
    }
    protected void rbtnIndoor_CheckedChanged(object sender, EventArgs e)
    {
        IndoorOutdoor = 0;

    }
    protected void rbtnOutdoor_CheckedChanged(object sender, EventArgs e)
    {
        IndoorOutdoor = 1;
    }
    protected void btnTakeBreak_Click(object sender, EventArgs e)
    {
        Server.Transfer("TakeBreak.aspx");
    }
    protected void btnChangeTask_Click(object sender, EventArgs e)
    {
        //if (btnChangeTask.Text == "Change Task")
        //{
       
        checkoutMethod();
        //    btnChangeTask.Text = "Save";
        //    btnChangeTask.Enabled = true;
        //    btnChkIn.Enabled = false;
        //    btnChkOut.Enabled = true;
        //}
        //else if (btnChangeTask.Text == "Save")
        //{    

        //gv_CheckInOut = new GridView();//to prenvent double timespent addition
        btnChkIn_Click(sender, e);
        //    btnChangeTask.Text = "Change Task";
        //}
    }
    public void checkoutMethod()
    {
        try
        {
            //btnChkIn.Enabled = true;
            btnChkOut.Enabled = false;
            cmbProjName.Enabled = true;
            cmbTaskName.Enabled = true;
            btnTakeBreak.Enabled = false;
            string Intime = string.Empty;
            ArrayList breakFromTime = new ArrayList();
            ArrayList breakToTime = new ArrayList();

            string qry1 = "SELECT FromTime,ToTime FROM tbl_BreakTimeMaster";
            MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(qry1, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                breakFromTime.Add(dr1[0].ToString());
                breakToTime.Add(dr1[1].ToString());
            }
            Connection.conn.Close();

            string qry = "SELECT CheckInTime FROM " + Connection.tbl_ChkInOutDetailsName + " where CheckInOutID=" + notCheckedOutID;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Intime = dr[0].ToString();
            }
            Connection.conn.Close();


            string Outtime = (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("HH:mm"); // to manage server time
            string difference = DateTime.Parse(Outtime).Subtract(DateTime.Parse(Intime)).ToString("t");
            string breakTime = "00:00";


            for (int i = 0; i < breakFromTime.Count; i++)
            {
                if (DateTime.Parse(breakFromTime[i].ToString()) > DateTime.Parse(Intime) && DateTime.Parse(breakToTime[i].ToString()) < DateTime.Parse(Outtime))
                {
                    breakTime = TimeSpan.Parse(breakTime.ToString()).Add((TimeSpan.Parse(breakToTime[i].ToString()).Subtract(TimeSpan.Parse(breakFromTime[i].ToString())))).ToString();
                }
            }
            difference = DateTime.Parse(difference).Subtract(DateTime.Parse(breakTime)).ToString("t");

            string spentTime1 = DateTime.Parse(difference).ToString("HH:mm");
            double spentTime = double.Parse(spentTime1.Replace(":", "."));
            //string update_qry = "Update tbl_ChkInOutDetails set CheckOutTime='" + System.DateTime.Now.ToShortTimeString() + "' where CheckInOutID=" + notCheckedOutID;
            string update_qry = "Update " + Connection.tbl_ChkInOutDetailsName + " set CheckOutTime='" + Outtime + "', SpentTime=" + spentTime + " where CheckInOutID=" + notCheckedOutID;
            Connection.updateData(update_qry);
            //selectdata();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void rbtnFillPrevious_CheckedChanged(object sender, EventArgs e)
    {
        btnChkIn.Enabled = true;
    }
    protected void rbtnCurrentWorking_CheckedChanged(object sender, EventArgs e)
    {
        dtpDate.Enabled = false;
        cmbInHrs.Enabled = false;
        cmbInMin.Enabled = false;
        cmbOutHrs.Enabled = false;
        cmbOutMin.Enabled = false;
        dtpDate.Text = currentDate;
        cmbInHrs.SelectedIndex = 0;
        cmbOutHrs.SelectedIndex = 0;
        cmbInMin.SelectedIndex = 0;
        cmbOutMin.SelectedIndex = 0;
        selectdata();
    }
   
}