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
using System.Text.RegularExpressions;

public partial class Vendor_Unblock : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Unblock));
                if (!Page.IsPostBack)
                {
                    lnkText.Text = "1";
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username1.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
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
            Response.Redirect("../../Master.aspx?M_ID=" + 3);
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
        string html = data.get_block_Vendor(username, name, pageno, rpp);
        return html;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
           Response.Redirect("../../Master.aspx?M_ID=" + 3);
        }
    }

    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                string data = split_data.Text;
                string[] sp_data = data.Split('-');
                string isSaved = string.Empty;
                for (int i = 0; i < sp_data.Length; i++)
                {
                    string[] row_data = sp_data[i].Split('$');
                    txt_Vendor_Code1.Text = Convert.ToString(row_data[0]);
                    txt_Vendor_Email1.Text = Convert.ToString(row_data[1]);
                    txt_Password1.Text = Convert.ToString(row_data[2]);
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);

                    string remark = string.Empty;
                    string refData = string.Empty;
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                           
                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Your account is unblock and your account details are below.</font></pre><p/> <pre><font size='3'>User Name: " + txt_Vendor_Code1.Text + "</font></pre> <pre><font size='3'>Password : " + txt_Password1.Text + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                    isSaved = (string)ActionController.ExecuteAction("", "Vendor_Unblock.aspx", "update", ref refData, txt_Vendor_Code1.Text, txt_Vendor_Email1.Text,msg);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else if (sp_data.Length == 1)
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Vendor is unblock succefully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                }
                if (isSaved != null || isSaved != "false" && sp_data.Length > 1)
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Vendors are unblock succefully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                }
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
 
}