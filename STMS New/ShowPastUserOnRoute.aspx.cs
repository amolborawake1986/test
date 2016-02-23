using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;

public partial class ShowPastUserOnRoute : System.Web.UI.Page
{
    static int i;

    string[] a = new string[3];
    string selectedDate = "";
    string tbl_ChkInOutDetailsName = "";


    public void getEmployeeNames()
    {
        string qry = "SELECT tbl_UserMaster.UserID,concat(FirstName ,' ', MiddleName,' ' , SurnameName) as 'Employee Name',tbl_UserMaster.Name FROM tbl_EmployeeMaster,tbl_UserMaster where tbl_EmployeeMaster.EmpID=tbl_UserMaster.EmpID";

        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
        Connection.conn.Open();
        cmbBox.Items.Clear();
        cmbBox.DataSource = cmd.ExecuteReader();
        cmbBox.DataTextField = "Name";
        cmbBox.DataValueField = "UserID";
        cmbBox.DataBind();
        Connection.conn.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dtpDate.Attributes.Add("readonly", "readonly");

    }


    protected void btnShowRecord_Click(object sender, EventArgs e)
    {
        if (dtpDate.Text.ToString() != "")
        {
            selectedDate = dtpDate.Text.ToString();
            a = selectedDate.Split('/');
            tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();
        }
        else
            tbl_ChkInOutDetailsName = Connection.tbl_ChkInOutDetailsName;

        string qryStr = "";
        if (rbtnDateWise.Checked == true)
        {
            //qryStr = "SELECT  tbl_UserMaster.Name ," + Connection.tbl_ChkInOutDetailsName + ".Date,tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.Name as 'Task Name'," + Connection.tbl_ChkInOutDetailsName + ".CheckInTime," + Connection.tbl_ChkInOutDetailsName + ".CheckOutTime," + Connection.tbl_ChkInOutDetailsName + ".CheckInOutID from tbl_UserMaster,tbl_ProjectMaster,tbl_TaskMaster," + Connection.tbl_ChkInOutDetailsName + " where " + Connection.tbl_ChkInOutDetailsName + ".ProjID=tbl_ProjectMaster.ProjID and " + Connection.tbl_ChkInOutDetailsName + ".TaskID=tbl_TaskMaster.TaskID and " + Connection.tbl_ChkInOutDetailsName + ".UserID=tbl_UserMaster.UserID and " + Connection.tbl_ChkInOutDetailsName + ".Date='" + dtpDate.Text.ToString() + "'";
            qryStr = "SELECT  tbl_UserMaster.Name ," + tbl_ChkInOutDetailsName + ".Date,tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.Name as 'Task Name'," + tbl_ChkInOutDetailsName + ".CheckInTime," + tbl_ChkInOutDetailsName + ".CheckOutTime," + tbl_ChkInOutDetailsName + ".CheckInOutID from tbl_UserMaster,tbl_ProjectMaster,tbl_TaskMaster," + tbl_ChkInOutDetailsName + " where " + tbl_ChkInOutDetailsName + ".ProjID=tbl_ProjectMaster.ProjID and " + tbl_ChkInOutDetailsName + ".TaskID=tbl_TaskMaster.TaskID and " + tbl_ChkInOutDetailsName + ".UserID=tbl_UserMaster.UserID and " + tbl_ChkInOutDetailsName + ".Date='" + dtpDate.Text.ToString() + "'";
            gv_Details.Columns[0].HeaderText = "Name";
        }
        else if (rbtnEmpWise.Checked == true)
        {
            RequiredFieldValidator6.Visible = false;
            qryStr = "SELECT  " + Connection.tbl_ChkInOutDetailsName + ".Date as 'Name',tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.Name as 'Task Name'," + Connection.tbl_ChkInOutDetailsName + ".CheckInTime," + Connection.tbl_ChkInOutDetailsName + ".CheckOutTime," + Connection.tbl_ChkInOutDetailsName + ".CheckInOutID from tbl_UserMaster,tbl_ProjectMaster,tbl_TaskMaster," + Connection.tbl_ChkInOutDetailsName + " where " + Connection.tbl_ChkInOutDetailsName + ".ProjID=tbl_ProjectMaster.ProjID and " + Connection.tbl_ChkInOutDetailsName + ".TaskID=tbl_TaskMaster.TaskID and " + Connection.tbl_ChkInOutDetailsName + ".UserID=tbl_UserMaster.UserID and  " + Connection.tbl_ChkInOutDetailsName + ".UserID=" + int.Parse(cmbBox.SelectedValue.ToString());
            gv_Details.Columns[0].HeaderText = "Date";
        }
        gv_Details.DataSource = null;
        gv_Details.DataBind();
        gv_Details.DataSource = Connection.loadData(qryStr);
        gv_Details.DataBind();
        //gv_Details.Columns[6].Visible = false;
        //DataTable dt1 = new DataTable();
        //js.Text = GPSLib.PlotGPSPoints(dt1);
    }

    protected void rbtnDateWise_CheckedChanged(object sender, EventArgs e)
    {

        if (rbtnDateWise.Checked == true)
        {
            lbl.Text = "Select Date*";
            imgCal.Visible = true;
            cmbBox.Visible = false;
            dtpDate.Visible = true;
        }
        else if (rbtnEmpWise.Checked == true)
        {
            lbl.Text = "Select Employee*";
            imgCal.Visible = false;
            cmbBox.Visible = true;
            dtpDate.Visible = false;
            dtpDate.Text = "";

            getEmployeeNames();
        }
    }
    protected void rbtnEmpWise_CheckedChanged(object sender, EventArgs e)
    {
        rbtnDateWise_CheckedChanged(sender, e);
    }
    protected void gv_Details_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //Connection.CheckInOutIDForSign = int.Parse(gv_Details.SelectedRow.Cells[6].Text.ToString());
            if (dtpDate.Text.ToString() != "")
            {
                selectedDate = dtpDate.Text.ToString();
                a = selectedDate.Split('/');
                tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();
            }
            else
                tbl_ChkInOutDetailsName = Connection.tbl_ChkInOutDetailsName;

            //selectedDate = dtpDate.Text.ToString();
            //a = selectedDate.Split('/');
            //tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + a[2].ToString() + a[1].ToString();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Latitude"));
            dt.Columns.Add(new DataColumn("Longitude"));

            DataRow row = dt.NewRow();
            int selectedID;
            selectedID = int.Parse(gv_Details.SelectedRow.Cells[6].Text.ToString());

            string qry = "";
            //if (rbtnDateWise.Checked == true)
            //{
            //qry = "Select LocationRouteDetails From " + Connection.tbl_ChkInOutDetailsName + " Where " + Connection.tbl_ChkInOutDetailsName + ".CheckInOutID=" + selectedID;
            qry = "Select LocationRouteDetails From " + tbl_ChkInOutDetailsName + " Where " + tbl_ChkInOutDetailsName + ".CheckInOutID=" + selectedID;
            //}
            //if (rbtnEmpWise.Checked == true)
            //{
            // qry = "Select LocationRouteDetails From tbl_ChkInOutDetails, tbl_UserMaster, tbl_User_Team Where tbl_UserMaster.UserID=tbl_ChkInOutDetails.UserID And CheckOutTime is null And InddorOutdoor=1 And tbl_User_Team.UserID=tbl_ChkInOutDetails.UserID And tbl_User_Team.TeamID=" + selectedID;
            //}

            // string qry = "SELECT LocationRouteDetails FROM tbl_ChkInOutDetails where CheckInOutID=33";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
            Connection.conn.Open();
            byte[] buffer = (byte[])cmd.ExecuteScalar();
            Connection.conn.Close();

            string value = ASCIIEncoding.ASCII.GetString(buffer);



            string[] Temp_latLandPoints = value.Split('\n');

            string[] TempLatLongValueArray;
            ArrayList LatLongValueArray = new ArrayList();
            for (int j = 0; j < Temp_latLandPoints.Length - 1; j++)
            {
                TempLatLongValueArray = Temp_latLandPoints[j].Split(',');
                LatLongValueArray.Add(TempLatLongValueArray[0].ToString());
                LatLongValueArray.Add(TempLatLongValueArray[1].ToString());
            }


            for (i = 0; i < LatLongValueArray.Count - 1; i++)
            {
                row = dt.NewRow();
                row["Latitude"] = LatLongValueArray[i];
                i = i + 1;
                row["Longitude"] = LatLongValueArray[i];
                dt.Rows.Add(row);
            }

            i = 0;





            //row = dt.NewRow();
            //row["Latitude"] = 35.6716525;
            //row["Longitude"] = 139.7965393;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.671538;
            //row["Longitude"] = 139.7965618;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6713794;
            //row["Longitude"] = 139.7972272;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6713572;
            //row["Longitude"] = 139.797305;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6713505;
            //row["Longitude"] = 139.797294;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6656458;
            //row["Longitude"] = 139.851101;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6662089;
            //row["Longitude"] = 139.8455136;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6656458;
            //row["Longitude"] = 139.851101;
            //dt.Rows.Add(row);//

            //row = dt.NewRow();
            //row["Latitude"] = 35.6695956;
            //row["Longitude"] = 139.8069388;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6695959;
            //row["Longitude"] = 139.8069383;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6695959;
            //row["Longitude"] = 139.8069383;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6655423;
            //row["Longitude"] = 139.8560403;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6696814;
            //row["Longitude"] = 139.8187974;
            //dt.Rows.Add(row);//

            //row = dt.NewRow();
            //row["Latitude"] = 35.6696512;
            //row["Longitude"] = 139.818846;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6696478;
            //row["Longitude"] = 139.8188452;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6696392;
            //row["Longitude"] = 139.8188596;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6696342;
            //row["Longitude"] = 139.818868;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Latitude"] = 35.6648913;
            //row["Longitude"] = 139.86032;
            //dt.Rows.Add(row);//

            js.Text = GPSLib.PlotGPSPoints(dt);


        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void gv_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv_Details, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Select Record for Details.";
        }
    }
}