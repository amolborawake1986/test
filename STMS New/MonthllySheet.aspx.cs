using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public partial class CheckInOut : System.Web.UI.Page
{

    public void getEmployeeNames()
    {
        string qry = "SELECT tbl_UserMaster.UserID,concat(FirstName ,' ', MiddleName,' ' , SurnameName) as 'Employee Name',tbl_UserMaster.Name FROM tbl_EmployeeMaster,tbl_UserMaster where tbl_EmployeeMaster.EmpID=tbl_UserMaster.EmpID";

        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
        Connection.conn.Close();
        Connection.conn.Open();
        cmbEmpName.Items.Clear();
        cmbEmpName.DataSource = cmd.ExecuteReader();
        cmbEmpName.DataTextField = "Employee Name";
        cmbEmpName.DataValueField = "UserID";
        cmbEmpName.DataBind();
        Connection.conn.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            getEmployeeNames();
        }
    }

    protected void btnPreviewMonth_Click(object sender, EventArgs e)
    {
        try
        {
            string tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + cmbYear.SelectedItem.Value.ToString() + cmbMonth.SelectedItem.Value.ToString();
            Connection.conn.Open();
            string dateformat = string.Empty;
            DataSet ds = new DataSet();

            string qry = "SELECT tbl_ProjectMaster.Name as 'Project Name'," + tbl_ChkInOutDetailsName + ".SpentTime as 'SpentTime'," + tbl_ChkInOutDetailsName + ".Date FROM tbl_ProjectMaster," + tbl_ChkInOutDetailsName + " WHERE tbl_ProjectMaster.ProjID=" + tbl_ChkInOutDetailsName + ".ProjID and " + tbl_ChkInOutDetailsName + ".UserID=" + int.Parse(cmbEmpName.SelectedItem.Value.ToString());//+ " and " + tbl_ChkInOutDetailsName + ".Date='" + dateformat + "/07/2015' group by tbl_ProjectMaster.Name"; //group by "  + Connection.tbl_ChkInOutDetailsName + ".Date";
            MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(qry, Connection.conn);
            da.SelectCommand.CommandTimeout = 120;
            da.Fill(ds);
            //gv_TimesheetMonthly.DataSource = ds;
            //gv_TimesheetMonthly.DataBind();

            DataTable dt = new DataTable();
            dt.Columns.Add("Project Name");
            for (int i = 1; i <= 31; i++)
            {
                string day = string.Empty;
                if (i.ToString().Length == 1)
                    day = "0" + i.ToString();
                else
                    day = i.ToString();

                string ColumnName = day;// +"/" + cmbMonth.SelectedItem.Value.ToString() + "/" + cmbYear.SelectedItem.Value.ToString();
                dt.Columns.Add(ColumnName);
            }
            DataView view = ds.Tables[0].DefaultView;
            DataTable distinctValues = view.ToTable(true, "Project Name");

            foreach (DataRow drProjName in distinctValues.Rows)
            {
                DataRow drnewRow = dt.NewRow();
                int flagProjName = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TimeSpan ts = TimeSpan.Zero;
                    if (drProjName["Project Name"].Equals(dr["Project Name"]))
                    {
                        if (flagProjName == 0)
                        {
                            drnewRow["Project Name"] = dr["Project Name"];
                            flagProjName = 1;
                        }
                        string[] a = new string[3];
                        a = (dr["Date"].ToString()).Split('/');
                        string newRowName = a[0].ToString();
                        string strtime = Convert.ToString((dr["SpentTime"]).ToString().Replace('.', ':'));
                        if (strtime != "")
                        {
                            ts = ts + TimeSpan.Parse(strtime);
                            drnewRow[newRowName] = (ts.ToString()).Substring(0, 5);
                        }
                    }
                }
                dt.Rows.Add(drnewRow);
            }
            gv_TimesheetMonthly.DataSource = dt;
            gv_TimesheetMonthly.DataBind();
            Connection.conn.Close();
        }
        catch
        {
            Connection.conn.Close();
        }
    }


}