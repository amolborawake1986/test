using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class CheckInOut : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (int i = 0; i <= 23; i++)
            {
                if (i.ToString().Length == 1)
                {
                    cmbOutHrs.Items.Add("0" + i.ToString());
                }
                else
                {
                    cmbOutHrs.Items.Add(i.ToString());
                }
            }

            for (int i = 0; i <= 59; i++)
            {
                if (i.ToString().Length == 1)
                {
                    cmbOutMin.Items.Add("0" + i.ToString());
                }
                else
                {
                    cmbOutMin.Items.Add(i.ToString());
                }
            }
        }


    }

}