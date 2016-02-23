using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MonthlyReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnBrowse_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOutputPath.Text != "")
            {
                string filePath = "@" + txtOutputPath.Text.ToString();
                System.Diagnostics.Process.Start(txtOutputPath.Text);
            }
        }
        catch
        {

        }
    }
}