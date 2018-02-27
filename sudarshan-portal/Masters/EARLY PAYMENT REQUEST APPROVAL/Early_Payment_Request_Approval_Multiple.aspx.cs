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

public partial class Early_Payment_Request_Approval_Multiple : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Early_Payment_Request_Approval_Multiple));
                if (!Page.IsPostBack)
                {
                    lnkText.Text = "1";
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username1.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        txtProcessID.Text = "10";
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
        string html = data.get_MultipleEarlyRequests(username, name, pageno, rpp);
        return html;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            //Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
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
                string refData = string.Empty;
                string remark = string.Empty;
                string data=split_data.Text;
                string[] sp_data = data.Split('@');
                string[] ven_data = mail_data.Text.Split('|');
                int succeed = 0;
                for (int i = 0; i < sp_data.Length; i++)
                {
                    Init_Email.Text = txtEmailID.Text;
                    string[] row_data = sp_data[i].Split('$');
                    txt_Dispatch.Text = Convert.ToString(row_data[0]);
                    txtProcessID.Text = Convert.ToString(row_data[1]);
                    txtInstanceID.Text = Convert.ToString(row_data[2]);
                    txtWIID.Text = Convert.ToString(row_data[3]);
                    txt_Request.Text = Convert.ToString(row_data[4]);
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);

                    DataTable dtV = (DataTable)ActionController.ExecuteAction("", "Edit_Profile.aspx", "getvendordetails", ref refData, ven_data[i]);
                    if (dtV != null)
                    {
                        if (dtV.Rows.Count > 0)
                        {
                            Init_Email.Text = Convert.ToString(dtV.Rows[0]["Email"]).Trim();
                        }
                    }
                    
                    
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                    txt_Condition.Text = "2";
                    //string isSaved = "true";
                    string isSaved = (string)ActionController.ExecuteAction("", "Early_Payment_Request_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txt_Dispatch.Text, txt_Username.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "104", "EARLY PAYMENT REQUEST APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        //bool isCreate = true;
                        if (isCreate)
                        {
                            succeed = succeed + 1;
                            try
                            {
                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "EARLY PAYMENT REQUEST APPROVAL", "USER", txt_Username.Text, "SUBMIT", remark, "0", "0");

                                string uniqueno = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "getuniqueno", ref isInserted, txt_Dispatch.Text);
                                string pono = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request_Approval_Multiple.aspx", "getpono", ref isInserted, txt_Dispatch.Text);
                                DataTable db = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getmailid", ref isInserted, pono);
                                string[] Dval1 = new string[db.Rows.Count];
                                Dval1[0] = "";
                                string mailid = "";
                                if (db != null)
                                {
                                    if (db.Rows.Count > 0)
                                    {
                                        for (int l = 0; l < db.Rows.Count; l++)
                                        {
                                            if (mailid == "")
                                            {
                                                mailid = Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                            }
                                            else
                                            {
                                                mailid = mailid + ',' + Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                            }
                                        }
                                    }
                                }
                                mailid = Init_Email.Text + mailid;

                                //string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>The Early Payment Request has been Approved.</font></pre><p/> <pre><font size='3'>Unique No: " + uniqueno + "</font></pre> <pre><font size='3'>Dispatch No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre> <pre><font size='3'>The Early Payment Request has been Approved.</font></pre> <pre><font size='3'>Unique No: " + uniqueno + "</font></pre> <pre><font size='3'>Dispatch No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Approved By: " + txt_Username.Text.Trim() + "</font></pre></p><pre><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre>Reporting Admin</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "EARLY PAYMENT REQUEST APPROVAL", "SUBMIT", Init_Email.Text, txtEmailID.Text, msg, "Dispatch No: " + txt_Request.Text);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Early Payment Request has been Approved...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }

                    }
                }
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + succeed + " out of " + sp_data.Length + " - Early Payment Request has been Approved...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
 string refData = string.Empty;
                string data = split_data.Text;
                string[] sp_data = data.Split('@');
		string[] ven_data = mail_data.Text.Split('|');
                int succeed = 0;
                for (int i = 0; i < sp_data.Length; i++)
                {
		    Init_Email.Text=txtEmailID.Text;
                    string[] row_data = sp_data[i].Split('$');
                    txt_Dispatch.Text = Convert.ToString(row_data[0]);
                    txtProcessID.Text = Convert.ToString(row_data[1]);
                    txtInstanceID.Text = Convert.ToString(row_data[2]);
                    txtWIID.Text = Convert.ToString(row_data[3]);
                    txt_Request.Text = Convert.ToString(row_data[4]);
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);

		    DataTable dtV = (DataTable)ActionController.ExecuteAction("", "Edit_Profile.aspx", "getvendordetails", ref refData, ven_data[i]);
                    if (dtV != null)
                    {
                        if (dtV.Rows.Count > 0)
                        {
                            Init_Email.Text = Convert.ToString(dtV.Rows[0]["Email"]).Trim();
                        }
                    }

                   
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                    txt_Condition.Text = "3";
                    string remark = string.Empty;
                    //string isSaved = "true";
                    string isSaved = (string)ActionController.ExecuteAction("", "Early_Payment_Request_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txt_Dispatch.Text, txt_Username.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        
                        string[] Dval = new string[1];
                        Dval[0] = "";
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "105", "EARLY PAYMENT REQUEST APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        //bool isCreate = true;
                        if (isCreate)
                        {
                            succeed = succeed + 1;
                            try
                            {
                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "EARLY PAYMENT REQUEST APPROVAL", "USER", txt_Username.Text, "SUBMIT", remark, "0", "0");

                                string uniqueno = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "getuniqueno", ref isInserted, txt_Dispatch.Text);

                                string pono = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request_Approval_Multiple.aspx", "getpono", ref isInserted, txt_Dispatch.Text);
                                DataTable db = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getmailid", ref isInserted, pono);
                                string[] Dval1 = new string[db.Rows.Count];
                                Dval1[0] = "";
                                string mailid = "";
                                if (db != null)
                                {
                                    if (db.Rows.Count > 0)
                                    {
                                        for (int l = 0; l < db.Rows.Count; l++)
                                        {
                                            if (mailid == "")
                                            {
                                                mailid = Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                            }
                                            else
                                            {
                                                mailid = mailid + ',' + Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                            }
                                        }
                                    }
                                }
                                mailid = Init_Email.Text + mailid;

                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>The Early Payment Request has been Rejected.</font></pre><pre><font size='3'>Unique No: " + uniqueno + "</font></pre> <pre><font size='3'>Dispatch No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Rejected By: " + txt_Username.Text.Trim() + "</font></pre><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre>Reporting Admin</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                //string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>The Early Payment Request has been Rejected.</font></pre><p/> <pre><font size='3'>Unique No: " + uniqueno + "</font></pre> <pre><font size='3'>Dispatch No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "EARLY_PAYMENT_REQUEST", "SUBMIT", Init_Email.Text, txtEmailID.Text, msg, "Dispatch No: " + txt_Request.Text);
                            }
                            catch (Exception ex)
                            {
                                // throw;
                                FSL.Logging.Logger.WriteEventLog(false, ex);
                            }
                            finally
                            {
                                //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Early Payment Request has been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                    }
                }
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + succeed + " out of "+ sp_data.Length+ " - Early Payment Request has been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

}