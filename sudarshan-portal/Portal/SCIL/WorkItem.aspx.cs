using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AjaxPro;
using FSL.Logging;
using FSL.Controller;
using FSL.Message;

public partial class WorkItem : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(WorkItem));
                
               
                    if (!IsPostBack)
                    {
                        sp_LoginUser.InnerHtml = Session["USER_NAME"].ToString();
                        frmset_WorkArea.Attributes["Src"] = "../SCIL/Home.aspx";
                    }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btnLogout(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                Session.Abandon();
                Response.Cookies["LoginId"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btngoto_home(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                frmset_WorkArea.Attributes["Src"] = "../SCIL/Home.aspx";
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

}

