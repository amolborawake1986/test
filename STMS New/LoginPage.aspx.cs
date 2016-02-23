using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginPage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Connection.CheckForInternetConnection() == true)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_UserMaster where Name='" + txtUserName.Text.Trim().ToString() + "' and Password='" + txtPwd.Text.Trim().ToString() + "'", Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                //Connection.loggedInUserID = Convert.ToInt16(dr[0].ToString());
                Session["loggedInUserID"] = Convert.ToInt16(dr[0].ToString());
                //Connection.loggedInEmpCode = Convert.ToInt16(dr[1].ToString());
                Session["loggedInEmpCode"] = Convert.ToInt16(dr[1].ToString());
                //Connection.loggedInEmpName = dr[2].ToString();
                Session["loggedInEmpName"] = dr[2].ToString();
                Connection.conn.Close();
                Session["UserName"] = txtUserName.Text.Trim().ToString();
                Response.Redirect("CheckInOut.aspx");

            }
            if (dr.HasRows == false)
            {
                lbl_Error.Visible = true;
            }
            Connection.conn.Close();
        }
        else
        {
            lbl_Error.Visible = true;
            lbl_Error.Text = "Please connect to Internet Connection.";
        }

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtUserName.Text = "";
        txtPwd.Text = "";
        txtUserName.Focus();
    }
}
