using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


public partial class CheckInOut : System.Web.UI.Page
{
    static List<Tuple<string, string, string>> projectList;
    string date = "";
    string endDate = "";

    public void getEmployeeNames()
    {
        // string qry = "SELECT tbl_UserMaster.UserID,concat(FirstName ,' ', MiddleName,' ' , SurnameName) as 'Employee Name',tbl_UserMaster.Name FROM tbl_EmployeeMaster,tbl_UserMaster where tbl_EmployeeMaster.EmpID=tbl_UserMaster.EmpID";
        string qry = "SELECT DISTINCT tbl_ProjectMaster.ProjID,tbl_ProjectMaster.StartDate,tbl_ProjectMaster.EndDate,tbl_ProjectMaster.Name as 'Project Name' FROM tbl_User_Project,tbl_ProjectMaster WHERE tbl_ProjectMaster.ProjID=tbl_User_Project.ProjID";
        //string qry = "SELECT tbl_UserMaster.UserID,concat(FirstName ,' ', MiddleName,' ' , SurnameName) as 'Employee Name',tbl_UserMaster.Name FROM tbl_EmployeeMaster,tbl_UserMaster where tbl_EmployeeMaster.EmpID=tbl_UserMaster.EmpID";

        //Connection.conn.Close();
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
        Connection.conn.Open();

        // cmd.ExecuteReader();

        MySqlDataReader reader = cmd.ExecuteReader();
        projectList = new List<Tuple<string, string, string>>();
        while (reader.Read())
        {
            projectList.Add(new Tuple<string, string, string>(reader["Project Name"] as string, reader["StartDate"] as string, reader["EndDate"] as string));
        }
        reader.Close();
        cmbEmpName.DataSource = cmd.ExecuteReader();
        cmbEmpName.DataTextField = "Project Name";
        cmbEmpName.DataValueField = "ProjID";
        cmbEmpName.DataBind();

        cmbEmpName.Items.Insert(0, "-- Select --");
        Connection.conn.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            getEmployeeNames();
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {

        try
        {
            date = "";// projectList[cmbEmpName.SelectedIndex - 1].Item2.ToString();//"04/02/2015";
            endDate = "";// projectList[cmbEmpName.SelectedIndex - 1].Item3.ToString();// "12/12/2016";

            string APBFlg = cmbSelected.SelectedValue;

            int ProjID = Convert.ToInt16(cmbEmpName.SelectedItem.Value.ToString());

            string qry = "SELECT DISTINCT tbl_TaskMaster.TaskID,tbl_TaskMaster.PlanEnd,tbl_TaskMaster.PlanStart,tbl_TaskMaster.ActulalStart,tbl_TaskMaster.ActualEnd,tbl_TaskMaster.percentageOfComplete,tbl_TaskMaster.Name as 'Task Name' FROM tbl_User_Project,tbl_TaskMaster WHERE tbl_TaskMaster.TaskID=tbl_User_Project.TaskID and tbl_User_Project.ProjID=" + ProjID + " and tbl_User_Project.UserID=" + Session["loggedInUserID"];
            /*DataSet ds = new DataSet();//
            MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(qry, Connection.conn);
          //da.SelectCommand.CommandTimeout = 1000;
            da.Fill(ds);*/

            //New Start 2016/02/09
            List<Tuple<string, string, string, string, string, string, string>> taskDetailsArray = new List<Tuple<string, string, string, string, string, string, string>>();

            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
            Connection.conn.Open();
            try
            {
                MySqlDataReader dataRead = command.ExecuteReader();
                string taskid;
                string planDate;
                while (dataRead.Read())
                {
                    taskid = dataRead["TaskID"].ToString();
                    taskDetailsArray.Add(new Tuple<string, string, string, string, string, string, string>
                         (taskid as string,
                         dataRead["Task Name"] as string,
                         dataRead["PlanStart"] as string,
                         dataRead["PlanEnd"] as string,
                         dataRead["ActulalStart"] as string,
                         dataRead["ActualEnd"] as string,
                         dataRead["percentageOfComplete"] as string));

                    //Plan Start date

                    planDate = dataRead["PlanStart"] as string;
                    if (date.Equals("")) { date = planDate; }
                    string[] dateFor = new string[3];
                    dateFor = date.Split('/');
                    DateTime startComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                    dateFor = planDate.Split('/');
                    DateTime compareDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));

                    if (DateTime.Compare(compareDate, startComDate) <= 0)//&& DateTime.Compare(compareDate, endComDate) < 0)
                    {
                        date = planDate;
                    }
                    planDate = dataRead["PlanEnd"] as string;
                    if (endDate.Equals("")) { endDate = planDate; }
                    dateFor = planDate.Split('/');
                    compareDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));

                    dateFor = endDate.Split('/');
                    DateTime endComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                    dateFor = planDate.Split('/');

                    if (DateTime.Compare(compareDate, endComDate) >= 0)//&& DateTime.Compare(compareDate, endComDate) < 0)
                    {
                        endDate = planDate;
                    }
                    //Plan End date
                }
            }
            catch
            {
                Connection.conn.Close();
            }
            Connection.conn.Close();

            //New Work

            string[] a = new string[3];
            a = date.Split('/');

            string[] a1 = new string[3];
            a1 = endDate.Split('/');

            DateTime start_Date = new DateTime(Convert.ToInt32(a[2]), Convert.ToInt32(a[1]), Convert.ToInt32(a[0]));
            DateTime end_Date = new DateTime(Convert.ToInt32(a1[2]), Convert.ToInt32(a1[1]), Convert.ToInt32(a1[0]));

            //Coloums
            int months = (end_Date.Year - start_Date.Year) * 12 + end_Date.Month - start_Date.Month;

            int noOfDays = DateTime.DaysInMonth(Convert.ToInt32(a[2]), Convert.ToInt32(a[1]));
            DataTable dt = new DataTable();
            dt.Columns.Add("Task Names");
            int startDay = start_Date.AddDays((DayOfWeek.Monday - start_Date.DayOfWeek)).AddDays(7).Day;
            List<string> colNames = new List<string>();
            for (int count = 0; count <= months; count++)
            {
                for (int i = startDay; i <= noOfDays; i = i + 7)
                {
                    string str = count.ToString();
                    string ColumnName = i.ToString();     // + str;
                    if (ColumnName.Length == 1) ColumnName = "0" + ColumnName;
                    if (start_Date.Month.ToString().Length != 1)
                    {
                        ColumnName = ColumnName + "/" + start_Date.Month;
                    }
                    else
                    {
                        ColumnName = ColumnName + "/0" + start_Date.Month;
                    }
                    DataColumn col = new DataColumn(ColumnName);
                    col.DataType = typeof(string);
                    dt.Columns.Add(col);
                    ColumnName = ColumnName + "/" + start_Date.Year;
                    colNames.Add(ColumnName);
                }

                DateTime nextdate = new DateTime(start_Date.Year, start_Date.Month, 1).AddMonths(1);
                start_Date = nextdate;
                noOfDays = DateTime.DaysInMonth(nextdate.Year, nextdate.Month);

                startDay = start_Date.AddDays((DayOfWeek.Monday - start_Date.DayOfWeek)).AddDays(7).Day;
                if (start_Date.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    startDay = start_Date.AddDays((DayOfWeek.Monday - start_Date.DayOfWeek)).Day;
                }
                if (start_Date.DayOfWeek.Equals(DayOfWeek.Monday))
                {
                    startDay = start_Date.Day;      //AddDays((DayOfWeek.Monday - start_Date.DayOfWeek)).Day;
                }
            }

            Connection.conn.Close();

            a = new string[3];

            a = date.Split('/');

            start_Date = new DateTime(Convert.ToInt32(a[2]), Convert.ToInt32(a[1]), Convert.ToInt32(a[0]));

            List<Tuple<string, string, string>> taskChInTimenew = new List<Tuple<string, string, string>>();
            string QUERY = "select * from tbl_ChkInOutDetails";
            string query = "";
            for (int m = 0; m < months; m++)
            {

                if (start_Date.Month.ToString().Length == 1)
                {
                    query = QUERY + start_Date.Year.ToString() + "0" + start_Date.Month.ToString();
                }
                else
                {
                    query = QUERY + start_Date.Year.ToString() + start_Date.Month.ToString();
                }

                MySql.Data.MySqlClient.MySqlCommand comd = new MySql.Data.MySqlClient.MySqlCommand(query, Connection.conn);
                Connection.conn.Open();
                try
                {
                    MySqlDataReader dataRead = comd.ExecuteReader();
                    string taskid;
                    while (dataRead.Read())
                    {
                        taskid = dataRead["TaskID"].ToString();
                        taskChInTimenew.Add(new Tuple<string, string, string>(dataRead["Date"] as string, taskid as string, dataRead["CheckInTime"] as string));
                    }
                }
                catch
                {
                    Connection.conn.Close();
                }
                Connection.conn.Close();
                DateTime nextdate = new DateTime(start_Date.Year, start_Date.Month, 1).AddMonths(1);
                start_Date = nextdate;
            }


            //Rows

            /*DataView view = ds.Tables[0].DefaultView;
            DataTable tbl_ProjectMaster = view.ToTable(true, "Task Name"); 
            string currentTaskID ="";
            bool flg = false;
            foreach (DataRow drProjName in tbl_ProjectMaster.Rows)
            {
                DataRow drnewRow = dt.NewRow();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    flg = false;
                    TimeSpan ts = TimeSpan.Zero;
                    if (drProjName["Task Name"].Equals(dr["Task Name"]))
                    {
                        currentTaskID = dr["TaskID"].ToString();
                           drnewRow["Task Names"] = dr["Task Name"];
                           flg = true;
                    }
                    if (flg)
                    {
                        for (int row = 0; row < taskChInTimenew.Count; row++)
                        {
                            if (currentTaskID.Equals(taskChInTimenew[row].Item2.ToString()))
                            {
                                for (int col = 0; col < colNames.Count - 1; col++)
                                {
                                    string[] dateFor = new string[3];
                                    dateFor = colNames[col].Split('/');
                                    DateTime startComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                                    dateFor = colNames[col + 1].Split('/');
                                    DateTime endComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                                    dateFor = taskChInTimenew[row].Item1.ToString().Split('/');
                                    DateTime compareDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                                    if (DateTime.Compare(compareDate, startComDate) >= 0 && DateTime.Compare(compareDate, endComDate) < 0)
                                    {
                                        dateFor = colNames[col].Split('/');
                                        drnewRow[dateFor[0] + "/" + dateFor[1]] = "CheCk";
                                        break;
                                    }
                                }
                            }
                            else
                            { //break;
                            }
                        }
                    }
                }
                dt.Rows.Add(drnewRow);

            }*/
            string currentTaskID;
            for (int i = 0; i < taskDetailsArray.Count; i++)
            {
                DataRow drnewRow = dt.NewRow();
                currentTaskID = taskDetailsArray[i].Item1.ToString();
                drnewRow["Task Names"] = taskDetailsArray[i].Item2.ToString(); ;

                string[] dateSplit = new string[3];
                dateSplit = taskDetailsArray[i].Item3.ToString().Split('/');
                DateTime planStartDate = new DateTime(Convert.ToInt32(dateSplit[2]), Convert.ToInt32(dateSplit[1]), Convert.ToInt32(dateSplit[0]));
                dateSplit = taskDetailsArray[i].Item4.ToString().Split('/');
                DateTime planEndDate = new DateTime(Convert.ToInt32(dateSplit[2]), Convert.ToInt32(dateSplit[1]), Convert.ToInt32(dateSplit[0]));
                if (APBFlg.Equals("Plan") || APBFlg.Equals("Both"))
                {
                    for (int j = 0; j < colNames.Count - 1; j++)
                    {
                        string[] dateFor = new string[3];
                        dateFor = colNames[j].Split('/');
                        DateTime compareDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));

                        if (DateTime.Compare(compareDate, planStartDate) >= 0 && DateTime.Compare(compareDate, planEndDate) <= 0)
                        {
                            dateFor = colNames[j].Split('/');
                            drnewRow[dateFor[0] + "/" + dateFor[1]] = "Green";
                        }
                    }
                }
                for (int row = 0; row < taskChInTimenew.Count; row++)
                {
                    int col = 0;
                    if (currentTaskID.Equals(taskChInTimenew[row].Item2.ToString()))
                    {
                        for (; col < colNames.Count - 1; col++)
                        {
                            string[] dateFor = new string[3];
                            dateFor = colNames[col].Split('/');
                            DateTime startComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                            dateFor = colNames[col + 1].Split('/');
                            DateTime endComDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));
                            dateFor = taskChInTimenew[row].Item1.ToString().Split('/');
                            DateTime compareDate = new DateTime(Convert.ToInt32(dateFor[2]), Convert.ToInt32(dateFor[1]), Convert.ToInt32(dateFor[0]));


                            if (DateTime.Compare(compareDate, startComDate) >= 0 && DateTime.Compare(compareDate, endComDate) < 0)
                            {
                                if (DateTime.Compare(compareDate, planStartDate) >= 0 && DateTime.Compare(compareDate, planEndDate) > 0)
                                {
                                    if (APBFlg.Equals("Both"))
                                    {
                                        dateFor = colNames[col].Split('/');
                                        drnewRow[dateFor[0] + "/" + dateFor[1]] = "Red";
                                    }
                                }
                                else
                                {
                                    if (APBFlg.Equals("Actual") || APBFlg.Equals("Both"))
                                    {
                                        dateFor = colNames[col].Split('/');
                                        drnewRow[dateFor[0] + "/" + dateFor[1]] = "Blue";
                                    }
                                }
                                break;
                            }
                            /*if (DateTime.Compare(compareDate, planStartDate) >= 0 && DateTime.Compare(compareDate, planEndDate) < 0)
                            {
                                dateFor = colNames[col].Split('/');
                                drnewRow[dateFor[0] + "/" + dateFor[1]] = "Green";
                            }
                            else*/


                        }
                    }
                    else
                    {
                    }
                }


                dt.Rows.Add(drnewRow);
            }

            //End Rows
            gv_TimesheetMonthly.DataSource = dt;
            gv_TimesheetMonthly.DataBind();
            for (int i = 0; i < gv_TimesheetMonthly.Rows.Count; i++)
            {
                for (int col = 1; col < gv_TimesheetMonthly.Rows[i].Cells.Count; col++)
                {

                    if (gv_TimesheetMonthly.Rows[i].Cells[col].Text.Equals("Blue"))
                    {
                        gv_TimesheetMonthly.Rows[i].Cells[col].BackColor = System.Drawing.Color.Blue;
                        gv_TimesheetMonthly.Rows[i].Cells[col].ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (gv_TimesheetMonthly.Rows[i].Cells[col].Text.Equals("Green"))
                    {
                        gv_TimesheetMonthly.Rows[i].Cells[col].BackColor = System.Drawing.Color.Green;
                        gv_TimesheetMonthly.Rows[i].Cells[col].ForeColor = System.Drawing.Color.Green;
                    }
                    else if (gv_TimesheetMonthly.Rows[i].Cells[col].Text.Equals("Red"))
                    {
                        gv_TimesheetMonthly.Rows[i].Cells[col].BackColor = System.Drawing.Color.Red;
                        gv_TimesheetMonthly.Rows[i].Cells[col].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        catch
        {
            Connection.conn.Close();
        }

    }

    protected void gv_TimesheetMonthly_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (cmbEmpName.SelectedIndex == 0) return;
            // string date = projectList[cmbEmpName.SelectedIndex-1].Item2.ToString();//"04/02/2014";
            // string endDate = projectList[cmbEmpName.SelectedIndex-1].Item3.ToString();// "24/01/2016";

            string[] a = new string[3];
            a = date.Split('/');

            string[] a1 = new string[3];
            a1 = endDate.Split('/');

            DateTime start_Date = new DateTime(Convert.ToInt32(a[2]), Convert.ToInt32(a[1]), Convert.ToInt32(a[0]));
            DateTime end_Date = new DateTime(Convert.ToInt32(a1[2]), Convert.ToInt32(a1[1]), Convert.ToInt32(a1[0]));

            int noOfMonth = (end_Date.Year - start_Date.Year) * 12 + end_Date.Month - start_Date.Month;


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            String[] months = new string[12] { "jan", "Feb", "Mar", "April", "May", "Jun", "jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;

            HeaderGridRow.Cells.Add(HeaderCell);
            int startMonth = start_Date.Month;
            for (int i = 0; i <= noOfMonth; i++)
            {
                HeaderCell = new TableCell();
                HeaderCell.Text = months[startMonth - 1] + start_Date.Year;

                int startDay = start_Date.AddDays(-(start_Date.DayOfWeek - DayOfWeek.Monday)).AddDays(7).Day;
                if (start_Date.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    startDay = start_Date.AddDays((DayOfWeek.Monday - start_Date.DayOfWeek)).Day;
                }
                if (start_Date.DayOfWeek.Equals(DayOfWeek.Monday))
                {
                    startDay = start_Date.Day;//AddDays((DayOfWeek.Monday - start_Date.DayOfWeek)).Day;
                }
                int noOfWeek = 0;
                for (int j = startDay; j <= DateTime.DaysInMonth(start_Date.Year, start_Date.Month); j = j + 7)
                {
                    noOfWeek++;
                }
                HeaderCell.ColumnSpan = noOfWeek;

                HeaderGridRow.Cells.Add(HeaderCell);

                DateTime nextdate = new DateTime(start_Date.Year, start_Date.Month, 1).AddMonths(1);
                start_Date = nextdate;
                startMonth = nextdate.Month;
            }
            gv_TimesheetMonthly.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void gv_TimesheetMonthly_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        //if (e.Row.RowType == DataControlRowType.DataRow) {
        //    for (int i = 1; i < gv_TimesheetMonthly.Columns.Count - 1; i++)
        //    {
        //        if (gv_TimesheetMonthly.Rows.c[i].HorizontalAlign = HorizontalAlign.Center) ;
        //        {
        //            ((TextBox)e.Row.Cells[i].Controls[0]).MaxLength = 2;
        //        }

        //    }
        //}
    }
}