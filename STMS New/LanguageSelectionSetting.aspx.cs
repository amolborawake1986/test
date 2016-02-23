using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LanguageSelectionSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MySql.Data.MySqlClient.MySqlCommand cmdLanguageMaster = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_LanguageMaster", Connection.conn);
            MySql.Data.MySqlClient.MySqlCommand cmdFormMaster = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_FormMaster", Connection.conn);


            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader drLanguageMaster = cmdLanguageMaster.ExecuteReader();

            cmbLanguage.Items.Clear();

            while (drLanguageMaster.Read())
            {
                cmbLanguage.Items.Add(drLanguageMaster[1].ToString());

            }
            Connection.conn.Close();

            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader drFormMaster = cmdFormMaster.ExecuteReader();
            cmbFormName.Items.Clear();
            while (drFormMaster.Read())
            {
                cmbFormName.Items.Add(drFormMaster[1].ToString());

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
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void btnTransAll_Click(object sender, EventArgs e)
    {

    }
}