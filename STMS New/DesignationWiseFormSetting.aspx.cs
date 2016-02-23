using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class DesignationWiseFormSetting : System.Web.UI.Page
{

    static ArrayList idList;
    static ArrayList nameList;
    static ArrayList arrTeamId;
    bool isIndexChanged = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_DesignationMaster", Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();

            if (lstDesigName.Items.Count == 0)
            {

                idList = new ArrayList();
                nameList = new ArrayList();
                while (dr.Read())
                {
                    lstDesigName.Items.Add(dr[1].ToString());

                    idList.Add(dr[0]);
                    nameList.Add(dr[1]);
                }
            }

            dr.Close();
            Connection.conn.Close();

            cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_FormMaster", Connection.conn);
            Connection.conn.Open();
            dr = cmd.ExecuteReader();
            if (lstFormName.Items.Count == 0)
            {
                arrTeamId = new ArrayList();

                while (dr.Read())
                {
                    lstFormName.Items.Add(dr[1].ToString());
                    arrTeamId.Add(dr[0]);
                }
            }
            dr.Close();
            Connection.conn.Close();
        }
        catch
        {
            Connection.conn.Close();
        }
    }



    protected void lstDesigName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstFormName.ClearSelection();

            isIndexChanged = true;

            int DesigId = (int)idList[lstDesigName.SelectedIndex];
            string DesigName = nameList[lstDesigName.SelectedIndex].ToString();

            ArrayList formIdData = new ArrayList();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select FormID from tbl_Designation_Form where DesigID = " + DesigId, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();

            int x = 0;
            while (dr.Read())
            {
                formIdData.Add(dr["FormID"]);
                x++;
            }

            dr.Close();
            Connection.conn.Close();

            string query = "";
            ArrayList teamNameData = new ArrayList();
            for (int i = 0; i < x; i++)
            {
                query = "select Name from tbl_FormMaster where FormID = " + formIdData[i];
                cmd = new MySql.Data.MySqlClient.MySqlCommand(query, Connection.conn);
                Connection.conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    for (int j = 0; j < lstFormName.Items.Count; j++)
                    {
                        if (dr["Name"].ToString() == lstFormName.Items[j].ToString())
                        {
                            lstFormName.Items[j].Selected = true;
                            btnAdd.Text = "Update";
                        }
                    }
                }
                dr.Close();
                Connection.conn.Close();
            }
        }
        catch
        {
            Connection.conn.Close();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int totalItemCnt = lstFormName.Items.Count;
            int selectedItemCnt = lstFormName.GetSelectedIndices().Count();

            string selItemDeptListBox = lstDesigName.SelectedItem.ToString();

            if (selectedItemCnt == 0)
            {
                btnAdd.Text = "Add";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Please select items from Form Name');</script>");
            }
            else
            {
                btnAdd.Text = "Update";

                MySql.Data.MySqlClient.MySqlCommand cmd;
                Connection.conn.Open();

                cmd = new MySql.Data.MySqlClient.MySqlCommand("DELETE from tbl_Designation_Form where DesigID = " + idList[lstDesigName.GetSelectedIndices()[0]], Connection.conn);

                cmd.ExecuteNonQuery();

                string query = "";

                for (int i = 0; i < lstFormName.GetSelectedIndices().Count(); i++)
                {
                    query = query + "INSERT into tbl_Designation_Form values(" + idList[lstDesigName.GetSelectedIndices()[0]] + ", " + arrTeamId[lstFormName.GetSelectedIndices()[i]] + ")" + ";";
                }

                cmd = new MySql.Data.MySqlClient.MySqlCommand(query, Connection.conn);
                cmd.ExecuteNonQuery();

                Connection.conn.Close();
            }
        }
        catch
        {
            Connection.conn.Close();
        }
    }
}