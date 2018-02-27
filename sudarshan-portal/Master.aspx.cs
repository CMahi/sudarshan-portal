using System;
using System.Web;
using System.Web.UI;
using FSL.Controller;
using AjaxPro;
using FSL.Logging;
using FSL.Message;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Configuration;
using WFE;
using InfoSoftGlobal;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

public partial class Portal_SCIL_Sub_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string isData = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Portal_SCIL_Sub_Menu));
                if (!Page.IsPostBack)
                {
                    txt_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    if (Session["USER_ADID"] != null)
                    {
                        if (Request.QueryString["M_ID"] != null)
                        {
                            txtMId.Text = Convert.ToString(Request.QueryString["M_ID"]);
                            getHeader();
                            getMasterData();
                        }
                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Portal/SCIL/Home.aspx");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    protected void getHeader()
    {
        try
        {
            string isdata = string.Empty;
            lblHeader.Text = (String)ActionController.ExecuteAction("", "Master.aspx", "getheader", ref isdata, txtMId.Text);
            
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }


    protected void getMasterData()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Master.aspx", "getmasterparent", ref isdata, Convert.ToString(Session["USER_ADID"]), txtMId.Text);
            dlMenu.DataSource = dt;
            dlMenu.DataBind();

        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void dlMenu_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label parentid = (Label)e.Item.FindControl("lblUser");
        Repeater rpt = (Repeater)e.Item.FindControl("dlRpt");
        string isdata = string.Empty;
        DataTable dts = (DataTable)ActionController.ExecuteAction("", "Master.aspx", "getmasterchild", ref isdata, Convert.ToString(Session["USER_ADID"]), txtMId.Text, parentid.Text);
        rpt.DataSource = dts;
        rpt.DataBind();
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "cmd_Url") // check command is cmd_delete
        {
            string obj_url = Convert.ToString(e.CommandArgument);
            string url = lblHeader.Text + obj_url;
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('" + url + "','frmset_WorkArea');}</script>");
        }
    }

}