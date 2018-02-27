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


public partial class SUB_CONTRACT_PO_DC_GENRATE : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(SUB_CONTRACT_PO_DC_GENRATE));
            try
            {
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["step"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txt_Step.Text = Convert.ToString(Request.QueryString["step"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }
                    bindData();
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void bindData()
    {
        CryptoGraphy crypt = new CryptoGraphy();
        StringBuilder html = new StringBuilder();
        string isdata = string.Empty;
        try
        {
        	html.Append("<table class='table table-bordered'>");
			html.Append("<thead>");
			html.Append("<tr class='grey'><th>PO No</th><th>PO Date</th><th>PO Gross</th><th>Request Number</th><th>Request Date</th><th>Delivery Chalan</th></tr>");
			html.Append("</thead>");
			html.Append("<tbody>");
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "SUB_CONTRACT_PO_DC _GENRATE.aspx", "getacknowledgement", ref isdata, txt_Username.Text, txtWIID.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                PK_DC_ID.Text = Convert.ToString(dt.Rows[0]["PK_DC_ID"]);
                CREATED_BY.Text = Convert.ToString(dt.Rows[0]["CREATED_BY"]);
                txt_Condition.Text = Convert.ToString(dt.Rows[0]["HEADER_INFO"]);
                pono.Text = Convert.ToString(dt.Rows[0]["PO_Number"]);
                string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[0]["PO_Number"]));
                string encrypt_Chalan = crypt.Encryptdata(Convert.ToString(dt.Rows[0]["DC_ID"]));
                html.Append("<tr><td><a href='#' onclick='viewData()'><input type='text' id='encrypt_po' value='" + encrypt_Str + "' style='display:none'><input type='text' id='pk_dc_id' value='" + Convert.ToString(dt.Rows[0]["PK_DC_ID"]) + "' style='display:none'>" + Convert.ToString(dt.Rows[0]["PO_Number"]) + "</a></td><td>" + Convert.ToString(dt.Rows[0]["PO_Date"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dt.Rows[0]["PO_GV"]) + "</td><td>" + Convert.ToString(dt.Rows[0]["HEADER_INFO"]) + "</td><td>" + Convert.ToString(dt.Rows[0]["REQUEST_DATE"]) + "</td><td><a href='#' onclick=viewChalan()>Delivery Chalan</a><input type='text' id='encrypt_chalan' value='" + encrypt_Chalan + "' style='display:none'></td></tr>");
            }
			
            html.Append("</tbody>");
			html.Append("</table>");
            div_Details.InnerHtml = Convert.ToString(html);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/Vendor/Taskdetails.aspx','frmset_WorkArea');}</script>");
        }
    }
    protected void btn_updatedispatch_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                string remark = txt_Remark.Text;
                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;
                string isSaved = string.Empty;
                isSaved = (string)ActionController.ExecuteAction("", "SUB_CONTRACT_PO_DC _GENRATE.aspx", "update", ref refData, PK_DC_ID.Text);
                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = refData.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {
                    string[] Dval = new string[1];
                    Dval[0] = txt_Username.Text;
                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "107", "SUB CONTRACT PO DC ACKNOWLEDGEMENT", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Condition.Text, txtWIID.Text, ref isInserted);
                    if (isCreate)
                    {
                        try
                        {
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "SUB CONTRACT PO DC ACKNOWLEDGEMENT", "USER", txt_Username.Text, "SUBMIT", remark, "0", "0");

                            string dt = (string)ActionController.ExecuteAction("", "SUB_CONTRACT_PO_DC _GENRATE.aspx", "getmailids", ref ISValid, CREATED_BY.Text);
                            Init_Email.Text += Convert.ToString(dt);
                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>The Delivery Chalan Acknowledgement has been Completed.</font></pre><pre><font size='3'>PO No: " + pono.Text + "</font></pre> <pre><font size='3'>Request No: " + txt_Condition.Text + "</font></pre><pre><font size='3'>Approved By: " + txt_Username.Text.Trim() + "</font></pre><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre>Vendor Portal Admin</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                            
                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, txtWIID.Text, "SUB CONTRACT PO DC ACKNOWLEDGEMENT", "SUBMIT", Init_Email.Text, txtEmailID.Text.Trim(), msg, "Request No: " + txt_Condition.Text);

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert(' Acknowledgement has been completed...!');window.open('../../Portal/Vendor/Taskdetails.aspx','frmset_WorkArea');}</script>");
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
  
}