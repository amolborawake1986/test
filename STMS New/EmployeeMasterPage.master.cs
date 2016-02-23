using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class EmployeeMasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Connection.CheckForInternetConnection() == true)
        {
            lblDateTime.Text = (System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("dd/MM/yyyy");
            //lblDateTime.Text = (System.DateTime.UtcNow.AddHours(12.49)).ToString("dd/MM/yyyy");

            try
            {
                lblWecomeLable.Text = "Welcome " + Session["UserName"].ToString();
                Connection.tblNameYearMonth=(System.DateTime.Now.AddHours(12).AddMinutes(30)).ToString("yyyyMM"); // to manage server time
                Connection.tbl_ChkInOutDetailsName = "tbl_ChkInOutDetails" + Connection.tblNameYearMonth; //find table by month
                Connection.tbl_TimesheetName = "tbl_Timesheet" + Connection.tblNameYearMonth; //find table by month;
                Connection.tbl_BreakTimeName = "tbl_BreakTime" + Connection.tblNameYearMonth; 

                string qry1 = "SELECT DesigID FROM tbl_EmployeeMaster where EmpID=" + Session["loggedInEmpCode"];
                Connection.conn.Open();
                MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(qry1, Connection.conn);
                int DesiID = int.Parse(cmd1.ExecuteScalar().ToString());
                Connection.conn.Close();

                string qry = "SELECT distinct tbl_FormMaster.Name FROM tbl_FormMaster where tbl_FormMaster.FormID NOT IN(select tbl_Designation_Form.FormID from tbl_Designation_Form where DesigID=" + DesiID + ")";
                Connection.conn.Open();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(qry, Connection.conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                Connection.conn.Close();
            Reset:
                ArrayList toRemoveMainItemIndex = new ArrayList();
                ArrayList toRemoveSubItemIndex = new ArrayList();
                for (int i = 0; i < Menu1.Items.Count; i++)
                {
                    for (int j = 0; j < Menu1.Items[i].ChildItems.Count; j++)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            if (Menu1.Items[i].ChildItems[j].Value != null)
                            {
                                if (Convert.ToString(dt.Rows[k]["Name"]) == Convert.ToString(Menu1.Items[i].ChildItems[j].Value))
                                {
                                    toRemoveMainItemIndex.Add(i);

                                    toRemoveSubItemIndex.Add(j);
                                }
                            }
                        }
                    }
                }

                for (int l = 0; l < toRemoveMainItemIndex.Count; l++)
                {
                    int m = int.Parse(toRemoveMainItemIndex[l].ToString());
                    int n = int.Parse(toRemoveSubItemIndex[l].ToString());
                    Menu1.Items[m].ChildItems.RemoveAt(n);
                    goto Reset;
                }

            mainReset:
                for (int q = 0; q < Menu1.Items.Count; q++)
                {
                    if (Menu1.Items[q].ChildItems.Count == 0)
                    {
                        Menu1.Items.RemoveAt(q);
                        goto mainReset;
                    }
                }
            }
            catch
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Session is Expired. Please Login.')", true);
                Response.Redirect("LoginPage.aspx");
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Check Internet connection.')", true);
        }
    }

    protected void lblLogOutLabel_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("LoginPage.aspx");
    }
   
}
