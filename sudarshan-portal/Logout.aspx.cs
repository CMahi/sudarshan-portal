using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FSL.Controller;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        try { Session.Abandon(); }
        catch (Exception) { } 
        finally {Response.Redirect("Login.aspx", false); }
    }
}
