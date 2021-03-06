﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
public partial class ProjectTeamSetting : System.Web.UI.Page
{
    static ArrayList idList;
    static ArrayList nameList;
    static ArrayList arrTeamId;
    bool isIndexChanged = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_ProjectMaster", Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();

            if (lstProjectName.Items.Count == 0)
            {

                idList = new ArrayList();
                nameList = new ArrayList();

                // lstDeptName.Items.Clear();
                while (dr.Read())
                {


                    lstProjectName.Items.Add(dr[1].ToString());

                    idList.Add(dr[0]);
                    nameList.Add(dr[1]);
                }


            }

            dr.Close();
            Connection.conn.Close();

            cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from tbl_TeamMaster", Connection.conn);
            Connection.conn.Open();
            dr = cmd.ExecuteReader();
            if (lstTeamName.Items.Count == 0)
            {
                arrTeamId = new ArrayList();

                while (dr.Read())
                {
                    lstTeamName.Items.Add(dr[1].ToString());
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


    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int totalItemCnt = lstTeamName.Items.Count;
            int selectedItemCnt = lstTeamName.GetSelectedIndices().Count();

            string selItemDeptListBox = lstProjectName.SelectedItem.ToString();

            if (selectedItemCnt == 0)
            {
                btnAdd.Text = "Add";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Please select items from Team Name');</script>");
            }
            else
            {
                btnAdd.Text = "Update";

                MySql.Data.MySqlClient.MySqlCommand cmd;
                Connection.conn.Open();

                cmd = new MySql.Data.MySqlClient.MySqlCommand("DELETE from tbl_Project_Team where ProjectID = " + idList[lstProjectName.GetSelectedIndices()[0]], Connection.conn);

                cmd.ExecuteNonQuery();

                string query = "";

                for (int i = 0; i < lstTeamName.GetSelectedIndices().Count(); i++)
                {
                    query = query + "INSERT into tbl_Project_Team values(" + idList[lstProjectName.GetSelectedIndices()[0]] + ", " + arrTeamId[lstTeamName.GetSelectedIndices()[i]] + ")" + ";";
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
    protected void lstProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstTeamName.ClearSelection();

            isIndexChanged = true;

            int ProjectId = (int)idList[lstProjectName.SelectedIndex];
            string ProjectName = nameList[lstProjectName.SelectedIndex].ToString();

            ArrayList teamIdData = new ArrayList();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select TeamID from tbl_Project_Team where ProjectID = " + ProjectId, Connection.conn);
            Connection.conn.Open();
            MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();

            int x = 0;
            while (dr.Read())
            {
                teamIdData.Add(dr["TeamID"]);
                x++;
            }

            dr.Close();
            Connection.conn.Close();

            string query = "";
            ArrayList teamNameData = new ArrayList();
            for (int i = 0; i < x; i++)
            {
                query = "select Name from tbl_TeamMaster where TeamID = " + teamIdData[i];
                cmd = new MySql.Data.MySqlClient.MySqlCommand(query, Connection.conn);
                Connection.conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    for (int j = 0; j < lstTeamName.Items.Count; j++)
                    {
                        if (dr["Name"].ToString() == lstTeamName.Items[j].ToString())
                        {
                            lstTeamName.Items[j].Selected = true;
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
}