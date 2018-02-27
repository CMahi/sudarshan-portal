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

public partial class TaskDetails : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(TaskDetails));
                if (!Page.IsPostBack)
                {
                    lnkText.Text = "1";
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username1.Text=txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        generateQueue();
                    }
                }
                ddlUser.Value = txt_Username.Text;
              
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Home.aspx");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void generateQueue()
    {
         int rpp = Convert.ToInt32(ddlRecords.SelectedItem.Text);
         int pageno = Convert.ToInt16(lnkText.Text);
         string tblHTML = getUserData(txt_Search.Text.Trim(), pageno, rpp);
         div_InvoiceDetails.InnerHtml = tblHTML;
    }

  
    protected void ddlRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlText1.Text = ddlText.Text = ddlRecords.SelectedItem.Text;
        generateQueue();
    }
   
[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillSearch(string name, int rpp)
    {
        string tblHTML = getUserData(name, 1, rpp);
        return Convert.ToString(tblHTML);
    }

[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
public string fillGoToPage1(string name, int pageno, int rpp)
{
    string tblHTML = getUserData(name, pageno, rpp);
    return Convert.ToString(tblHTML);
}

[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
public string fillGoToPage(int pageno, int rpp)
{
    string tblHTML = getUserData("", pageno, rpp);
    return Convert.ToString(tblHTML);
}


protected string getUserData(string name, int pageno, int rpp)
{
    GetData data = new GetData();
    string username = Convert.ToString(Session["USER_ADID"]);
    string html = data.get_TasksData(username, name, pageno, rpp);
    return html;
}

}

