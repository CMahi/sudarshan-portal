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
using System.Web.Services;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

public partial class Early_Payment_Request : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    string Body = string.Empty;
    CryptoGraphy crypt = new CryptoGraphy();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Early_Payment_Request));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                        {
                            txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                            txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        }
                       
                        lnkText.Text = "1";
                        ddlText.Text = ddlRecords.SelectedItem.Text;
                       GenerateInvoiceHTML(txt_Search.Text.Trim());
                       app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    }
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    private void GenerateInvoiceHTML(string search_data)
    {
        StringBuilder HTML = new StringBuilder();
        string str = string.Empty;
        try
        {
            string username = Convert.ToString(Session["USER_ADID"]);

            string isdata = string.Empty;
            HTML = create_string(search_data);
            div_InvoiceDetails.InnerHtml = HTML.ToString();

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
        }
    }

     protected void btnRequest_Click(object sender, EventArgs e)
     {
         /********************************************************************** Early Payment Request *************************************************************************************/
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                
                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;
                txt_Condition.Text = "1";
                string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, "102");
                txtInstanceID.Text = instanceID;
                string isSaved = (string)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "insert", ref refData, Convert.ToInt32(txt_Condition.Text), Convert.ToInt32(txt_Dispatch.Text), Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text));
                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = refData.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {

                    txt_Request.Text = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "getrequestid", ref isInserted, txt_Dispatch.Text);
                    
                    DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getaccapprover", ref ISValid, "EARLY PAYMENT ACCOUNTS APPROVER");
                    string[] Dval = new string[DTAP.Rows.Count];
                    Dval[0] = "";
                    if (DTAP.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTAP.Rows.Count; i++)
                        {
                            Dval[i] = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                            if (txtApproverEmail.Text == "")
                            {
                                txtApproverEmail.Text = Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                            }
                            else
                            {
                                txtApproverEmail.Text = txtApproverEmail.Text + ',' + Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                            }
                        }
                    }
                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "103", "EARLY PAYMENT REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, "0", ref isInserted);
                    if (isCreate)
                    {
                        try
                        {
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "EARLY PAYMENT REQUEST", "DATASLOT", txt_Username.Text, "SUBMIT", "OK", "0", "0");
                            string emailid = string.Empty;
                            //for (int i = 0; i < DTAP.Rows.Count; i++)
                            //{
                                string uniqueno = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "getuniqueno", ref isInserted, txt_Dispatch.Text);
				string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre> <pre><font size='3'>The Vendor Early Payment Request Sent Successfully.</font></pre> <pre><font size='3'>Unique No: " + uniqueno + "</font></pre> <pre><font size='3'>Dispatch No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Approved By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:<a data-cke-saved-href={" + mail + "} href={" + mail + "}>" + mail + "</a></font></pre></br><pre>Regards</pre><pre>Reporting Admin</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "EARLY PAYMENT REQUEST", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Dispatch No: " + txt_Request.Text);
                            //}
                           
                        }
                        catch (Exception ex)
                        {
                           // throw;
                            FSL.Logging.Logger.WriteEventLog(false, ex);
                        }
                        finally
                        {
                            GenerateInvoiceHTML(txt_Search.Text.Trim());
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Early Payment Request has been Sent Successfully...!');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                }
            }
         }
         catch (Exception ex)
         {
             FSL.Logging.Logger.WriteEventLog(false, ex);
         }
     }

     protected StringBuilder create_string(string search_data)
     {
         /***********************************************************************Fetch Early Payment Request and bind to table************************************************************************************/
         StringBuilder HTML = new StringBuilder();
         int pageno = Convert.ToInt16(lnkText.Text);
         int ddl = Convert.ToInt32(ddlRecords.SelectedItem.Text);
         HTML = getUserData(search_data, pageno, ddl);
         return HTML;
     }

     protected void btnUpdate_Click(object sender, EventArgs e)
     {
         GenerateInvoiceHTML(txt_Search.Text.Trim());
     }

     [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
     public string GetCurrentTime(int name)
     {
         GetData getData = new GetData();
         string data = getData.get_Dispatch_Details(name);
         return data;
     }

     [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
     public string GetFileNames(int name)
     {
         string filename = string.Empty;
         string isdata = string.Empty;
         try
         {
             filename = (string)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getname", ref isdata, name);
         }
         catch(Exception ex)
         {
             FSL.Logging.Logger.WriteEventLog(false, ex);
         }

         return filename;
     }

     protected void ddlRecords_SelectedIndexChanged(object sender, EventArgs e)
     {
         GenerateInvoiceHTML(txt_Search.Text.Trim());
     }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
     public string fillSearch(string name, int rpp)
     {
         StringBuilder html = getUserData(name, 1, rpp);
         return Convert.ToString(html);
     }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillGoToPage1(string name, int pageno, int rpp)
    {
        StringBuilder html = getUserData(name, pageno, rpp);
        return Convert.ToString(html);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillGoToPage(int pageno, int rpp)
    {
        StringBuilder html = getUserData("", pageno, rpp);
        return Convert.ToString(html);
    }

    protected StringBuilder getUserData(string name, int pageno, int rpp)
    {
        GetData data = new GetData();
        string username = Convert.ToString(Session["USER_ADID"]);
        StringBuilder html = data.get_EarlyRequestData(username, name, pageno, rpp);
        return html;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Portal/HomePage.aspx");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
}