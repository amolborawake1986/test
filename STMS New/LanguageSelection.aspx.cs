using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LanguageSelection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtEmpCode.Attributes.Add("readonly", "readonly");
            txtEmpName.Attributes.Add("readonly", "readonly");
            txtEmpCode.Text = Session["loggedInEmpCode"].ToString();
            txtEmpName.Text = Session["loggedInEmpName"].ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_LanguageMaster", Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();
            cmbLanguage.Items.Clear();
            while (dr.Read())
            {
                cmbLanguage.Items.Add(dr[1].ToString());
            }

            Connection.conn.Close();
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}