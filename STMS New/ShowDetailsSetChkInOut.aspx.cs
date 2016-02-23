using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShowDetailsSetChkInOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string qryStr = "select * from tbl_LogUserMissingTime where chkInOutID=" + Session["selected_CheckInOutID"];
            gv_Details.DataSource = Connection.loadData(qryStr);
            gv_Details.DataBind();
        }
        catch
        {
            
        }
    }

}