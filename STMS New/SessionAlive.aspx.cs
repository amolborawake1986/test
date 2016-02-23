using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SessionAlive : System.Web.UI.Page
{
    protected string WindowStatusText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 120));
        MetaRefresh.Attributes["content"] = Convert.ToString((Session.Timeout * 60) - 60) + ";url=SessionAlive.aspx?q=" + DateTime.Now.Ticks;

        WindowStatusText = "Last refresh " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

    }
}