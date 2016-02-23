using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using System.IO;
using System.Data;

public partial class MonthlyReport : System.Web.UI.Page
{
    private object missing = Type.Missing;
    Microsoft.Office.Interop.Excel.Application xla;
    public void getEmployeeNames()
    {
        string qry = "SELECT tbl_UserMaster.UserID,concat(FirstName ,' ', MiddleName,' ' , SurnameName) as 'Employee Name',tbl_UserMaster.Name FROM tbl_EmployeeMaster,tbl_UserMaster where tbl_EmployeeMaster.EmpID=tbl_UserMaster.EmpID";

        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
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
        xla = new Microsoft.Office.Interop.Excel.Application();
        if (!Page.IsPostBack)
        {
            getEmployeeNames();
        }
    }
    protected void btnBrowse_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtOutputPath.Text != "")
            //{
                //string filePath = "@" + txtOutputPath.Text.ToString();
                string filePath = @"D:\Report.xls";
                System.Diagnostics.Process.Start(filePath);
            //}
        }
        catch
        {

        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string month = cmbYear.SelectedItem.Text.ToString() + cmbMonth.SelectedItem.Value.ToString();
            string tblName = "tbl_ChkInOutDetails" + month;
            string qry = "SELECT " + tblName + ".Date, tbl_ProjectMaster.Name as 'Project Name',tbl_TaskMaster.Name as 'Task Name', " + tblName + ".CheckInTime," + tblName + ".CheckOutTime,SUM(" + tblName + ".SpentTime) as 'Total Working Time' from tbl_ProjectMaster,tbl_TaskMaster," + tblName + " where tbl_ProjectMaster.ProjID=" + tblName + ".ProjID and tbl_TaskMaster.TaskID=" + tblName + ".TaskID and " + tblName + ".UserID=" + int.Parse(cmbEmpName.SelectedItem.Value.ToString()) + " GROUP by tbl_ProjectMaster.Name";

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            Connection.conn.Close();


            Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;

            Microsoft.Office.Interop.Excel.ChartObjects chartObjs = (Microsoft.Office.Interop.Excel.ChartObjects)ws.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject chartObj = chartObjs.Add(100, 20, 300, 200);
            Microsoft.Office.Interop.Excel.Chart xlChart = chartObj.Chart;


            ws.Cells[1, 1] = "Project Name";
            ws.Cells[1, 2] = "Working Hours";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ws.Cells[i + 2, 1] = dt.Rows[i][1].ToString();
                ws.Cells[i + 2, 2] = double.Parse(dt.Rows[i][5].ToString());
            }

            int LastRecordRow = ws.Cells.Find("*", System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

            // int nInLastCol = xlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Microsoft.Office.Interop.Excel.XlSearchOrder.xlByColumns, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;
            Microsoft.Office.Interop.Excel.Range rg = ws.get_Range("A1", "B" + LastRecordRow);
            xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlPie;
            xlChart.SetSourceData(rg, Type.Missing);

            wb.SaveAs(@"D:\Report.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wb.Close(true, Type.Missing, Type.Missing);
            lblMsg.Visible = true;
            xla.Quit();
            releaseObject(ws);
            releaseObject(wb);
            releaseObject(xla);
        }
        catch
        {
            Connection.conn.Close();
        }
    }

    private static void releaseObject(object obj)
    {
        try
        {

            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);

            obj = null;

        }

        catch (Exception ex)
        {

            obj = null;

            Console.WriteLine("Exception Occured while releasing object " + ex.ToString());

        }

        finally
        {

            GC.Collect();

        }
    }

}